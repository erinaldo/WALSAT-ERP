using SYS.QUERYS;
using SYS.QUERYS.Lancamentos.Financeiro;
using SYS.QUERYS.Lancamentos.Gourmet;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FTransferencia : SYS.FORMS.FBase
    {
        public FTransferencia()
        {
            InitializeComponent();
        }
        public string Nr_Mesa = "0";
        public string Nr_Cartão = "0";
        public int CD_Pedido;
        private decimal vlPrazo;
        private decimal vSubTotal;

        private void CalculaSubtotal()
        {
            try
            {
                decimal subtotal = 0;
                for (int i = 0; i < bsItens.Count; i++)
                    subtotal += (bsItens[i] as MPedidoItem).VL_SUBTOTAL;

                vlPrazo = new QDuplicata().BuscaTotalAprazo(CD_Pedido);

                decimal liquidado = Math.Round(new QDuplicata().BuscaTotalLiquidado(CD_Pedido), 2);

                vSubTotal = subtotal - liquidado - vlPrazo;
                seSubtotal.Value = vSubTotal;
            }
            catch (Exception)
            {
            }
        }

        private void buscaItens()
        {
            var itens = new QPedido().BuscarItens(CD_Pedido.ToString(), "", "").ToList();

            for (int i = 0; i < itens.Count; i++)
            {
                var complementos = new QPedido().BuscarComplementos(itens[i].ID_PEDIDO, itens[i].ID_ITEM).ToList();
                for (int y = 0; y < complementos.Count; y++)
                    itens[i].COMPLEMENTOS.Add(complementos[y]);
            }

            for (int i = 0; i < itens.Count; i++)
            {
                var adicionais = new QPedido().BuscarAdicionais(itens[i].ID_PEDIDO, itens[i].ID_ITEM).ToList();
                for (int y = 0; y < adicionais.Count; y++)
                    itens[i].Adicionais.Add(adicionais[y]);
            }

            if (itens.Count > 0)
                bsItens.DataSource = itens;
            else
                bsItens.Clear();

            CalculaSubtotal();
        }

        private List<TB_GOU_PEDIDO> VerificaPedido(string vCD_Mesa, string vCD_Cartao)
        {
            var db = Conexao.BancoDados;

            var lresult = (from i in db.TB_GOU_PEDIDOs
                           join y in db.TB_COM_PEDIDOs on i.ID_PEDIDO equals y.ID_PEDIDO
                           where y.TP_MOVIMENTO == "S"
                           && y.ST_PEDIDO != "F"
                           && y.ST_ATIVO == true
                           select i).AsParallel();

            if (vCD_Mesa.Length > 0)
                lresult = from i in lresult where i.ID_MESA == vCD_Mesa select i;

            if (vCD_Cartao.Length > 0)
                lresult = from i in lresult where i.ID_CARTAO == vCD_Cartao select i;

            return lresult.ToList();
        }

        private TB_GOU_MESA ExisteMesa(string vCD_Mesa)
        {
            var db = Conexao.BancoDados;

            return (from i in db.TB_GOU_MESAs
                    where i.ST_ATIVO == true
                    && i.ID_MESA == Convert.ToInt32(vCD_Mesa)
                    select i).AsParallel().First();
        }

        private void afterGravar(List<MPedidoItem> vItens)
        {
            var vpedido = new MPedido();

            var transferencia = new List<MPedidoItem>();
            for (int i = 0; i < vItens.Count; i++)
                transferencia.Add(new MPedidoItem
                {
                    ID_ITEM = vItens[i].ID_ITEM,
                    ID_PEDIDO = vItens[i].ID_PEDIDO
                });

            var lresult = VerificaPedido(teMesa.Text.Trim(), teCartao.Text.Trim());

            var existeMesa = new TB_GOU_MESA();//VERIFICA NO CADASTRO DE MESAS SE EXISTE
            if (teMesa.Text.Trim().Length > 0)
                existeMesa = ExisteMesa(teMesa.Text.Trim());

            if (existeMesa == null)
                throw new Exception("Mesa destino informada não esta cadastrada!");

            vpedido.ID_EMPRESA = 1;
            vpedido.ID_CLIFOR = 1;
            vpedido.ID_MESA = lresult.Count > 0 ? lresult[0].ID_MESA : (teMesa.Text.Trim() == "" ? "0" : teMesa.Text.Trim());
            vpedido.ID_CARTAO = lresult.Count > 0 ? lresult[0].ID_CARTAO : (teCartao.Text.Trim() == "" ? "0" : teCartao.Text.Trim());
            vpedido.ST_ATIVO = vItens.Count > 0;
            vpedido.ST_PEDIDO = "O";
            vpedido.TP_MOVIMENTO = "S";

            if (lresult.Count > 0)
                vpedido.ID_PEDIDO = lresult[0].ID_PEDIDO;

            

            for (int i = 0; i < vItens.Count; i++)
            {
                (vItens[i] as MPedidoItem).ID_PEDIDO = 0;
                vpedido.Itens.Add(vItens[i] as MPedidoItem);
            }       

            int posiscao_transacao = 0;

            new QPedido().Gravar(vpedido, ref posiscao_transacao,transferencia);

            new QPedido().VerificaMesaSemItem();

            this.Close();
        }

        private void afterTransferir()
        {
            var itens = new ListaPedidoItem();

            for (int i = 0; i < gvItens.RowCount; i++)
                itens.Add(bsItens[i] as MPedidoItem);

            var lresult = (from i in itens where i.Select == true select i).AsParallel().ToList();

            if (lresult.Count <= 0)
                throw new Exception("Selecione os itens que deseja transferir!");
            if (teCartao.Text.Trim() == "" && teMesa.Text.Trim() == "")
                throw new Exception("Informa a mesa ou cartão destino!");

            afterGravar(lresult);

        }

        private void txt_mesa_TextChanged(object sender, EventArgs e)
        {

            teCartao.Text = "";
        }

        private void txt_cartao_TextChanged(object sender, EventArgs e)
        {

            teMesa.Text = "";
        }

        private void bt_transferir_Click(object sender, EventArgs e)
        {
            try
            {
                afterTransferir();
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void Transferencia_Load(object sender, EventArgs e)
        {
            try
            {
                beAtual.Text = Nr_Mesa != "0" ? "Mesa " + Nr_Mesa : "Cartão " + Nr_Cartão;

                buscaItens();
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void txt_mesa_Leave(object sender, EventArgs e)
        {
            try
            {
                if (teMesa.Text.Trim() == Nr_Mesa)
                {
                    teMesa.Select();
                    throw new Exception("Local de Transferência deve ser diferente do atual!");
                }
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void txt_cartao_Leave(object sender, EventArgs e)
        {
            try
            {
                if (teCartao.Text.Trim() == Nr_Cartão)
                {
                    teCartao.Select();
                    throw new Exception("Local de Transferência deve ser diferente do atual!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
