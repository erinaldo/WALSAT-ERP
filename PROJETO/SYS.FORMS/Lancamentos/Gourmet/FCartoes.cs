using SYS.QUERYS;
using SYS.UTILS;
using System.Linq;
using SYS.QUERYS.Lancamentos.Gourmet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FCartoes : SYS.FORMS.FBase
    {
        public FCartoes()
        {
            InitializeComponent();
        }

        private bool transferir;

        private void BuscaCartoes()
        {
            var db = Conexao.BancoDados;

            bsCartoes.DataSource = (from i in db.TB_GOU_PEDIDOs
                                    join y in db.TB_COM_PEDIDOs on i.ID_PEDIDO equals y.ID_PEDIDO
                                     where y.TP_MOVIMENTO == "S"
                                     && y.ST_PEDIDO != "F"
                                     && y.ST_ATIVO != false
                                     && i.ID_CARTAO != "0"
                                     && i.ID_CARTAO != "000"
                                     select new MPedido
                                     {
                                         ID_PEDIDO = i.ID_PEDIDO,
                                         ID_CLIFOR = y.ID_CLIFOR,
                                         ID_EMPRESA = i.ID_EMPRESA,
                                         ID_MESA = i.ID_MESA,
                                         ID_CARTAO = i.ID_CARTAO,
                                         ST_ATIVO = y.ST_ATIVO ?? false,
                                         ST_PEDIDO = y.ST_PEDIDO,
                                         DT_CADASTRO = y.DT_CADASTRO,
                                         VALOR_PEDIDO = y.TB_COM_PEDIDO_ITEMs.Where(p=> p.ST_ATIVO == true).Sum(p=>p.VL_SUBTOTAL) ?? 0m,
                                     }).ToList().OrderByDescending(p=>p.ID_CARTAO.ToInt32());
        }

        private List<TB_GOU_PEDIDO> VerificaPedido(string vNRCartao)
        {
            var db = Conexao.BancoDados;

            return (from i in db.TB_GOU_PEDIDOs
                    join y in db.TB_COM_PEDIDOs on i.ID_PEDIDO equals y.ID_PEDIDO
                    where y.TP_MOVIMENTO == "S"
                    && y.ST_PEDIDO != "F"
                    && y.ST_ATIVO != false
                    && i.ID_CARTAO == vNRCartao
                    select i).ToList();
        }

        private void NrCartao_Enter()
        {
            if (teIDCartao.Text.Trim().Length <= 0 || teIDCartao.Text.Trim() == "000" || teIDCartao.Text.Trim() == "0000")
                return;

            string NrCartao = teIDCartao.Text.TrimStart('0');
            var vPedido = VerificaPedido(NrCartao).ToList();
            Lc_Troco.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (transferir && vPedido.Count > 0)
            {
                var frm = new FTransferencia() { Nr_Cartão = NrCartao, CD_Pedido = vPedido[0].ID_PEDIDO }.ShowDialog();
                transferir = false;
            }
            else
            {
                var frm = new FLancamento_Balanca();
                frm.NrCartao = NrCartao;
                teUltimoAcesso.Text = teIDCartao.Text.Trim();
                frm.NR_pedido = vPedido.Count > 0 ? vPedido[0].ID_PEDIDO : 0;
                frm.ShowDialog();
                BuscaCartoes();

                if (frm.Vl_Troco > 0)
                {
                    Lc_Troco.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    spTroco.Value = frm.Vl_Troco;
                }
                teIDCartao.Text = "";
                teIDCartao.Select();                
            }            
        }

        private void ListaCartoes_Load(object sender, EventArgs e)
        {
            try
            {
                new QPedido().VerificaMesaSemItem();
                BuscaCartoes();
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                ex.Validar();
            }
        }

        private void gc_cartoes_DoubleClick(object sender, EventArgs e)
        {
            if (bsCartoes.Current == null)
                return;

            timer1.Enabled = false;

            teIDCartao.Text = (bsCartoes.Current as MPedido).ID_CARTAO;
            NrCartao_Enter();

            timer1.Enabled = true;
        }

        private void txt_NrCartao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                NrCartao_Enter();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new QPedido().VerificaMesaSemItem();
            BuscaCartoes();
        }

        private void bt_transferir_Click(object sender, EventArgs e)
        {
            transferir = true;
        }

        private void ListaCartoes_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void gc_cartoes_Click(object sender, EventArgs e)
        {
            if (transferir)
            {
                if (bsCartoes.Current == null)
                    return;

                teIDCartao.Text = (bsCartoes.Current as MPedido).ID_CARTAO;
                NrCartao_Enter();
            }
        }

        private void sbTransferir_Click(object sender, EventArgs e)
        {
            transferir = true;
        }

        private void sbSelecionar_Click(object sender, EventArgs e)
        {
            if (bsCartoes.Current == null)
                return;

            timer1.Enabled = false;

            teIDCartao.Text = (bsCartoes.Current as MPedido).ID_CARTAO;
            NrCartao_Enter();

            timer1.Enabled = true;
        }
    }
}
