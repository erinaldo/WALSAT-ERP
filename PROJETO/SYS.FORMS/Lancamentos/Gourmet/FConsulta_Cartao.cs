using System;
using System.Linq;
using SYS.UTILS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.QUERYS;
using SYS.QUERYS.Lancamentos.Gourmet;
using DevExpress.XtraEditors;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FConsulta_Cartao : SYS.FORMS.FBase
    {
        public FConsulta_Cartao()
        {
            InitializeComponent();
        }

        private bool ST_somar;
        private int ID_Pedido = 0;
        private string ultimoCartao ="";

        private void Fechar()
        {

            if (ID_Pedido <= 0)
                return;

            var _Pedido = new SYS.QUERYS.Lancamentos.Comercial.QPedido().Buscar(ID_Pedido).ToList()[0];
            var vpedido = new MPedido();
            vpedido.ID_PEDIDO = ID_Pedido;
            vpedido.ID_CLIFOR = _Pedido.ID_CLIFOR;
            vpedido.ID_EMPRESA = _Pedido.ID_EMPRESA;
            vpedido.ST_ATIVO = _Pedido.ST_ATIVO ?? false;
            vpedido.TP_MOVIMENTO = _Pedido.TP_MOVIMENTO;
            vpedido.ID_PEDIDO_ORIGEM = _Pedido.ID_PEDIDO_ORIGEM;
            vpedido.ID_CARTAO = _Pedido.TB_GOU_PEDIDO.ID_CARTAO;
            vpedido.ID_MESA = _Pedido.TB_GOU_PEDIDO.ID_MESA;


            var frm = new FFechamento();
            frm.vPedido = vpedido;
            frm.GravaPedido = false;            
            frm.VendaDireta = false;
            frm.ShowDialog();

            teTroco.Text = frm.vl_troco.ToString("n2");

            teIDCartao.Text = ultimoCartao;
            BuscaItens();

        }

        private void BuscaItens()
        {
            var db = Conexao.BancoDados;

            List<MPedidoItem> lresult = new List<MPedidoItem>();

            if(ceFechados.Checked && deData.Text.Trim () != "")
            {
                lresult = (from i in db.TB_GOU_PEDIDOs
                           join y in db.TB_COM_PEDIDOs on i.ID_PEDIDO equals y.ID_PEDIDO
                           join x in db.TB_COM_PEDIDO_ITEMs on y.ID_PEDIDO equals x.ID_PEDIDO
                           join w in db.TB_EST_PRODUTOs on x.ID_PRODUTO equals w.ID_PRODUTO
                           where y.TP_MOVIMENTO == "S"
                           && y.ST_PEDIDO == "F"
                           && y.ST_ATIVO != false
                           && x.ST_ATIVO != false
                           && i.ID_CARTAO == teIDCartao.Text.TrimStart('0')
                           && y.DT_CADASTRO > Convert.ToDateTime(deData.Text.Trim())
                           && y.DT_CADASTRO < Convert.ToDateTime(deData.Text.Trim()).AddDays(1).AddSeconds(-1)
                           select new MPedidoItem
                           {
                               ID_PRODUTO = w.ID_PRODUTO,
                               NM_PRODUTO = w.NM,
                               QUANTIDADE = x.QT ?? 0m,
                               VL_UNITARIO = x.VL_UNITARIO ?? 0m,
                               VL_SUBTOTAL = x.VL_SUBTOTAL ?? 0m,
                               ID_PEDIDO = i.ID_PEDIDO
                           }).ToList();
            }
            else

            {
                   lresult = (from i in db.TB_GOU_PEDIDOs
                           join y in db.TB_COM_PEDIDOs on i.ID_PEDIDO equals y.ID_PEDIDO
                           join x in db.TB_COM_PEDIDO_ITEMs on y.ID_PEDIDO equals x.ID_PEDIDO
                           join w in db.TB_EST_PRODUTOs on x.ID_PRODUTO equals w.ID_PRODUTO
                           where y.TP_MOVIMENTO == "S"
                           && y.ST_PEDIDO != "F"
                           && y.ST_ATIVO != false
                           && x.ST_ATIVO != false
                           && i.ID_CARTAO == teIDCartao.Text.TrimStart('0')
                           select new MPedidoItem
                           {
                               ID_PRODUTO = w.ID_PRODUTO,
                               NM_PRODUTO = w.NM,
                               QUANTIDADE = x.QT ?? 0m,
                               VL_UNITARIO = x.VL_UNITARIO ?? 0m,
                               VL_SUBTOTAL = x.VL_SUBTOTAL ?? 0m,
                               ID_PEDIDO = i.ID_PEDIDO
                           }).ToList();
            }

            if (lresult.Count > 0)
            {               

                this.Text = "Consulta de Cartões";
                bsItens.DataSource = lresult;

                if (ST_somar)
                    seTotalSoma.Value += lresult.Sum(p => p.VL_SUBTOTAL);
                else
                    seTotalSoma.Value = lresult.Sum(p => p.VL_SUBTOTAL);

                spTotal.Value = lresult.Sum(p => p.VL_SUBTOTAL);
                this.Text += " - Cartão: " + teIDCartao.Text.TrimStart('0');
                ID_Pedido = lresult[0].ID_PEDIDO;
                ST_somar = false;
                ultimoCartao = teIDCartao.Text;
            }
            else
            {
                bsItens.DataSource = null;
                spTotal.Value = 0m;
            }
            teIDCartao.Text = "";
        }

        private void teIDCartao_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
                if (e.KeyCode == Keys.Enter && teIDCartao.Text.Trim() != "")
                {
                    BuscaItens();
                }
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void teIDCartao_Leave(object sender, EventArgs e)
        {
            teIDCartao.Focus();
        }

        private void sbSomar_Click(object sender, EventArgs e)
        {
            ST_somar = true;
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            try
            {
                Fechar();
            }
            catch (Exception ex)
            {
                new SYS.UTILS.SYSException(ex.Message);
            }
        }

        private void sbJuntar_Click(object sender, EventArgs e)
        {
            var frm = new FUnir();

            if (frm.ShowDialog() == DialogResult.OK)
                if (frm.ID_Cartao.Length > 0)
                {
                    teIDCartao.Text = frm.ID_Cartao;
                    BuscaItens();
                }
        }

        private void FConsulta_Cartao_Load(object sender, EventArgs e)
        {
            teIDCartao.Select();
        }

        private void ceFechados_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                deData.Enabled = ceFechados.Checked;
                deData.Text = Conexao.Data.ToString();
                teIDCartao.Select();
            }
            catch (Exception ex)
            {
                new SYSException(ex.Message);
            }           
        }
    }
}
