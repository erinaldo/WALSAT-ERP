using System;
using SYS.QUERYS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using SYS.QUERYS.Lancamentos.Gourmet;
using System.Linq;
using SYS.QUERYS.Cadastros.Gourmet;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FMesas : SYS.FORMS.FBase
    {
        private bool transferir;
        private int vAmbienteAtual;

        public FMesas()
        {
            InitializeComponent();

            this.Shown += FMesas_Shown;

            sbFechar.Click += delegate { this.Dispose(); };

            sbTransferir.Click += delegate { transferir = true; };
        }

        void FMesas_Shown(object sender, EventArgs e)
        {
            try
            {
                tgAmbientes.Items.Clear();
                tgMesas.Items.Clear();                
                

                var ambientes = new QAmbiente().Buscar().ToList();
                ambientes.ForEach(ambiente =>
                {
                    #region Variáveis

                    var mesasAmbiente = new List<string>();
                    if (ambiente.TB_GOU_MESAs != null)
                        mesasAmbiente = ambiente.TB_GOU_MESAs.Select(b => b.ID_MESA.ToString()).ToList();

                    var mesasOcupadas = (from a in new QPedido().Buscar()
                                         where (a.TB_COM_PEDIDO.TP_MOVIMENTO ?? "").Trim().ToUpper() == "S"
                                         && (a.TB_COM_PEDIDO.ST_PEDIDO ?? "").Trim().ToUpper() != "F"
                                         && (a.TB_COM_PEDIDO.ST_ATIVO ?? false) != false
                                         && mesasAmbiente.Contains(a.ID_MESA)
                                         select new { }).Count();
                    var mesasDesocupadas = mesasAmbiente.Count() - mesasOcupadas;

                    #endregion

                    var tileAmbiente = new TileItem
                    {
                        Id = ambiente.ID_AMBIENTE,
                        BorderVisibility = TileItemBorderVisibility.Never,
                        ItemSize = TileItemSize.Wide,
                        Image = mesasDesocupadas > 0 ? global::SYS.FORMS.Properties.Resources.flag_green_x20 : global::SYS.FORMS.Properties.Resources.flag_red_x20,
                        ImageAlignment = TileItemContentAlignment.TopLeft
                    };

                    #region Topo

                    var elementoTopo = new TileItemElement
                    {
                        Text = ambiente.NM.Validar(),
                        TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight
                    };
                    elementoTopo.Appearance.Normal.Options.UseFont = true;
                    elementoTopo.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tileAmbiente.Elements.Add(elementoTopo);

                    #endregion

                    #region Centro

                    var elementoCentro = new TileItemElement
                    {
                        Text = mesasOcupadas + " ocupadas",
                        TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight
                    };
                    elementoCentro.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tileAmbiente.Elements.Add(elementoCentro);

                    #endregion

                    #region Chão

                    var elementoChao = new TileItemElement
                    {
                        Text = mesasDesocupadas + " desocupadas",
                        TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight
                    };
                    elementoChao.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tileAmbiente.Elements.Add(elementoChao);

                    #endregion

                    tileAmbiente.ItemClick += delegate
                    {
                        if (tgMesas.Items.Count > 0)
                            tgMesas.Items.Clear();

                        var mesas = new QMesa().Buscar().ToList();

                        mesas.ForEach(mesa =>
                        {
                            var tileMesa = new TileItem
                            {
                                Id = mesa.ID_MESA,
                                BorderVisibility = TileItemBorderVisibility.Never,
                                ItemSize = TileItemSize.Wide,
                                Image = (from a in new QPedido().Buscar()
                                         where (a.TB_COM_PEDIDO.TP_MOVIMENTO ?? "").Trim().ToUpper() == "S"
                                         && (a.TB_COM_PEDIDO.ST_PEDIDO ?? "").Trim().ToUpper() != "F"
                                         && a.ID_MESA == mesa.ID_MESA.ToString()
                                         select new { }).Take(1).Count() > 0 ? global::SYS.FORMS.Properties.Resources.flag_red_x20 : global::SYS.FORMS.Properties.Resources.flag_green_x20,
                                ImageAlignment = TileItemContentAlignment.TopLeft
                            };

                            #region Topo

                            var elementoTopoMesa = new TileItemElement
                            {
                                Text = mesa.NM ?? (mesa.ID_MESA < 10 ? "0" + mesa.ID_MESA.ToString() : mesa.ID_MESA.ToString()),
                                TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight
                            };
                            elementoTopoMesa.Appearance.Normal.Options.UseFont = true;
                            elementoTopoMesa.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            tileMesa.Elements.Add(elementoTopoMesa);

                            tileMesa.ItemClick += delegate
                            {
                                timer1.Enabled = false;
                                string CD_MESA = mesa.ID_MESA.ToString();
                                var vPedido = VerificaPedido(CD_MESA).ToList();

                                if (transferir && vPedido.Count > 0)
                                {
                                    var frm = new FTransferencia() { Nr_Mesa = CD_MESA, CD_Pedido = vPedido[0].ID_PEDIDO }.ShowDialog();
                                    transferir = false;
                                }
                                else
                                {
                                    var frm = new FPedido();
                                    frm.NrMesa = CD_MESA;
                                    frm.NR_pedido = vPedido.Count > 0 ? vPedido[0].ID_PEDIDO : 0;
                                    frm.ShowDialog();
                                    FMesas_Shown(null, null);
                                }

                                timer1.Enabled = true;
                            };

                            tgMesas.Items.Add(tileMesa);
                            #endregion
                        });
                    };

                    tgAmbientes.Items.Add(tileAmbiente);
                });

            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private List<TB_GOU_PEDIDO> VerificaPedido(string vID_Mesa)
        {
            var consulta = new QPedido();

            return (from i in consulta.Buscar()
                    join y in Conexao.BancoDados.TB_COM_PEDIDOs on i.ID_PEDIDO equals y.ID_PEDIDO
                    where y.TP_MOVIMENTO == "S"
                    && (y.ST_ATIVO ?? false)
                    && i.ST != "F"
                    && i.ID_MESA == vID_Mesa
                    select i).ToList();
        }

        private void tcAmbientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new QPedido().VerificaMesaSemItem();
            FMesas_Shown(null, null);
            
        }

        private void FMesas_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        
    }
}