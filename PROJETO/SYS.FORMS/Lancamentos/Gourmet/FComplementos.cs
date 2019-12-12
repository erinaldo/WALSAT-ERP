using System;
using SYS.UTILS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using SYS.QUERYS.Lancamentos.Gourmet;
using System.Windows.Forms;
using SYS.QUERYS;
using DevExpress.XtraEditors;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FComplementos : SYS.FORMS.FBase_Cadastro
    {
        public FComplementos()
        {
            InitializeComponent();
        }

        public int vCD_Grupo = 0;
        public int vCD_Produto = 0;
        private decimal Quantidade = 1;
        private decimal SomaComplementos = 0m;
        public string vST_Complemento;
        public string vST_TPComplemento;
        decimal quantidadeTotal = 0m;
        public MPedidoItem Produto = new MPedidoItem();
        public bool? ST_Fracao = false;
        public List<MComplemento> listaComplemento = new List<MComplemento>();
        public List<TB_EST_GRUPO_ADICIONAI> listaAdicional = new List<TB_EST_GRUPO_ADICIONAI>();

        #region Metodos

        private void CalculaTotal()
        {
            seVlProduto.Value = Math.Round(Produto.VL_UNITARIO, 2);
            teDsProduto.Text = Produto.NM_PRODUTO;
            seSoma.Value = Math.Round(SomaComplementos, 2);
            seTotal.Value = (SomaComplementos + Produto.VL_UNITARIO);
        }

        private void PreencheComplementos()
        {
            var lComplementos = new List<MComplemento>();
            var lAdicional = new List<TB_EST_GRUPO_ADICIONAI>();

            if (vST_Complemento == "C")
                lComplementos = new QComplemento().Busca(vCD_Produto, vCD_Grupo);
            else
                lAdicional = new QAdicionais().BuscarAdicionais(vCD_Grupo.ToString(), vST_TPComplemento);

            for (int i = 0; i < lComplementos.Count; i++)
            {
                lComplementos[i].ID_PRODUTO_PEDIDO = vCD_Produto;
                MComplemento item = lComplementos[i];
                DevExpress.XtraEditors.TileItem vComplemento = new TileItem();
                vComplemento.Text = lComplementos[i].NM_PRODUTO + "\n" + (lComplementos[i].VALOR.ToString() == "0.00" ? "" : lComplementos[i].VALOR.ToString());
                vComplemento.Name = lComplementos[i].ID_PRODUTO.ToString();
                vComplemento.Tag = lComplementos[i].VALOR;

                //vComplemento.TextAlignment = TileItemContentAlignment.MiddleCenter;
                //vComplemento.AppearanceItem.Normal.FontSizeDelta = 2;
                //vComplemento.ItemSize = TileItemSize.Medium;                
                vComplemento.TextAlignment = TileItemContentAlignment.MiddleCenter;
                var vComplementoElement = new TileItemElement();
                vComplementoElement.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
                vComplemento.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
                vComplemento.Elements.Add(vComplementoElement);
                vComplemento.Id = 0;
                vComplemento.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
                vComplemento.Padding = new System.Windows.Forms.Padding(-5);                
                vComplemento.AppearanceItem.Normal.BackColor = Color.LightCoral;
                vComplemento.AppearanceItem.Normal.BorderColor = Color.LightCoral;
                vComplemento.ItemClick += (s, e) => { Complemento_Click(vComplemento, item); vComplemento.AppearanceItem.Normal.BackColor = vComplemento.AppearanceItem.Normal.BackColor == Color.LightCoral ? Color.Coral : Color.LightCoral; };
                grupoComplementos.Items.Add(vComplemento);
            }

            for (int i = 0; i < lAdicional.Count; i++)
            {
                TB_EST_GRUPO_ADICIONAI item = lAdicional[i];
                DevExpress.XtraEditors.TileItem vComplemento = new TileItem();
                vComplemento.Text = lAdicional[i].DS;
                vComplemento.Name = lAdicional[i].ID_ADICIONAL.ToString();

                vComplemento.TextAlignment = TileItemContentAlignment.MiddleCenter;
                var vComplementoElement = new TileItemElement();
                vComplementoElement.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
                vComplemento.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
                vComplemento.Elements.Add(vComplementoElement);
                vComplemento.Id = 0;
                vComplemento.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
                vComplemento.Padding = new System.Windows.Forms.Padding(-5);

                //vComplemento.TextAlignment = TileItemContentAlignment.MiddleCenter;
                //vComplemento.AppearanceItem.Normal.FontSizeDelta = 5;
                //vComplemento.ItemSize = TileItemSize.Medium;
                vComplemento.AppearanceItem.Normal.BackColor = Color.LightCoral;
                vComplemento.AppearanceItem.Normal.BorderColor = Color.LightCoral;
                //vComplemento.AppearanceItem.Normal.Font.Bold.Equals(true);

                vComplemento.ItemClick += (s, e) => { AdicionalComplemento_Click(vComplemento, item); vComplemento.AppearanceItem.Normal.BackColor = vComplemento.AppearanceItem.Normal.BackColor == Color.LightCoral ? Color.Coral : Color.LightCoral; };
                grupoComplementos.Items.Add(vComplemento);
            }

        }

        private void afterGravar()
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void ValidaComplementosAdicionados()
        {
            for (int i = 0; i < grupoComplementos.Items.Count; i++)
            {
                for (int y = 0; y < listaComplemento.Count; y++)
                    if (grupoComplementos.Items[i].Name == listaComplemento[y].ID_PRODUTO.ToString())
                        grupoComplementos.Items[i].AppearanceItem.Normal.BackColor = Color.Coral;
            }

        }

        #endregion

        #region Eventos

        private void Complementos_Load(object sender, EventArgs e)
        {
            try
            {
                PreencheComplementos();
                CalculaTotal();
                if (vST_Complemento != "C")
                    pQuantidade.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ValidaComplementosAdicionados();

            }
            catch (Exception execao)
            {
                execao.Validar();
            }
        }

        private void Complemento_Click(TileItem vTileItem, MComplemento item)
        {
            try
            {
                if (vTileItem.AppearanceItem.Normal.BackColor == Color.LightCoral)
                {
                    //adiciona
                    SomaComplementos += (item.VALOR * Convert.ToDecimal(Quantidade));
                    item.VALOR = (item.VALOR * Convert.ToDecimal(Quantidade));
                    item.QUANTIDADE = Convert.ToDecimal(Quantidade);
                    listaComplemento.Add(item);

                    quantidadeTotal = 0;
                    for (int i = 0; i < listaComplemento.Count; i++)
                        quantidadeTotal += listaComplemento[i].QUANTIDADE;

                    if ((quantidadeTotal) > 1)
                    {
                        SomaComplementos += (item.VALOR * Convert.ToDecimal(Quantidade));
                        item.VALOR = (item.VALOR * Convert.ToDecimal(Quantidade));
                        item.QUANTIDADE = Convert.ToDecimal(Quantidade);
                        listaComplemento.Remove(item);
                        throw new Exception("Quantidade Fracionada ultrapassou o limite de 1!");
                    }


                }
                else
                {
                    listaComplemento.Clear();
                    SomaComplementos += (item.VALOR * Convert.ToDecimal(Quantidade));
                    item.VALOR = (item.VALOR * Convert.ToDecimal(Quantidade));
                    item.QUANTIDADE = Convert.ToDecimal(Quantidade);
                    listaComplemento.Remove(item);

                    SomaComplementos = 0;

                    for (int i = 0; i < listaComplemento.Count; i++)
                        quantidadeTotal = listaComplemento[i].QUANTIDADE;

                    for (int i = 0; i < listaComplemento.Count; i++)
                        SomaComplementos += listaComplemento[i].VALOR;
                }
                Quantidade = 1;
                CalculaTotal();
            }
            catch (Exception execao)
            {
                execao.Validar();
            }
        }

        private void AdicionalComplemento_Click(TileItem vTileItem, TB_EST_GRUPO_ADICIONAI item)
        {
            try
            {
                if (vTileItem.AppearanceItem.Normal.BackColor == Color.LightCoral)
                {
                    //adiciona
                    listaAdicional.Add(item);
                }
                else
                    listaAdicional.Remove(item);
            }
            catch (Exception excecao)
            {
                vTileItem.AppearanceItem.Normal.BackColor = Color.Coral;
                excecao.Validar();
            }
        }

        public override void Gravar()
        {
            afterGravar();	
        }

        private void bt_meio_Click(object sender, EventArgs e)
        {
            if (ST_Fracao ?? false)
                Quantidade = 0.5m;
        }

        private void bt_terco_Click(object sender, EventArgs e)
        {
            if (ST_Fracao ?? false)
                Quantidade = 0.333m;
        }

        private void bt_quarto_Click(object sender, EventArgs e)
        {
            if (ST_Fracao ?? false)
                Quantidade = 0.25m;
        }

        private void bt_quinto_Click(object sender, EventArgs e)
        {
            if (ST_Fracao ?? false)
                Quantidade = 0.2m;
        }

        private void bt_sexto_Click(object sender, EventArgs e)
        {
            if (ST_Fracao ?? false)
                Quantidade = 0.1667m;
        }

        #endregion
    }
}
