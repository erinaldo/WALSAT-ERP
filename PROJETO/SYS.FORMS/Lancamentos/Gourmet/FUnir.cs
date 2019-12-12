using System;
using System.Linq;
using SYS.UTILS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.QUERYS.Lancamentos.Gourmet;
using SYS.QUERYS;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FUnir : SYS.FORMS.FBase_Cadastro
    {
        public string ID_Cartao = "";

        public FUnir()
        {
            InitializeComponent();
        }

        private List<TB_GOU_PEDIDO> VerificaPedido(string vCD_Cartao)
        {
            var db = Conexao.BancoDados;

            var lresult = (from i in db.TB_GOU_PEDIDOs
                           join y in db.TB_COM_PEDIDOs on i.ID_PEDIDO equals y.ID_PEDIDO
                           where y.TP_MOVIMENTO == "S"
                           && y.ST_PEDIDO != "F"
                           && y.ST_ATIVO == true
                           select i).AsParallel();

            if (vCD_Cartao.Length > 0)
                lresult = from i in lresult where i.ID_CARTAO == vCD_Cartao select i;

            return lresult.ToList();
        }

        private void afterTransferir(List<MPedidoItem> vItens)
        {
            if (bsCartoes == null)
                return;

            var vpedido = new MPedido();

            var transferencia = new List<MPedidoItem>();
            for (int i = 0; i < vItens.Count; i++)
                transferencia.Add(new MPedidoItem
                {
                    ID_ITEM = vItens[i].ID_ITEM,
                    ID_PEDIDO = vItens[i].ID_PEDIDO
                });

            var lresult = VerificaPedido(teNumero.Text.Trim());
            
            vpedido.ID_EMPRESA = 1;
            vpedido.ID_CLIFOR = 1;
            vpedido.ID_MESA = lresult.Count > 0 ? lresult[0].ID_MESA : "0";
            vpedido.ID_CARTAO = (bsCartoes.Current as MPedido).ID_CARTAO;
            vpedido.ST_ATIVO = vItens.Count > 0;
            vpedido.ST_PEDIDO = "O";
            vpedido.TP_MOVIMENTO = "S";

            ID_Cartao = vpedido.ID_CARTAO;

            if (lresult.Count > 0)
                vpedido.ID_PEDIDO = lresult[0].ID_PEDIDO;



            for (int i = 0; i < vItens.Count; i++)
            {
                (vItens[i] as MPedidoItem).ID_PEDIDO = 0;
                vpedido.Itens.Add(vItens[i] as MPedidoItem);
            }

            int posiscao_transacao = 0;

            new QPedido().Gravar(vpedido, ref posiscao_transacao, transferencia);

            new QPedido().VerificaMesaSemItem();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private List<MPedidoItem> BuscaItensCartoes()
        {
            var db = Conexao.BancoDados;

            var _Itens = new List<MPedidoItem>();

            for (int i = 0; i < bsCartoes.Count; i++)
            {

            var lresult = (from a in db.TB_GOU_PEDIDOs
                           join b in db.TB_COM_PEDIDOs on a.ID_PEDIDO equals b.ID_PEDIDO
                           join y in db.TB_COM_PEDIDO_ITEMs on a.ID_PEDIDO equals y.ID_PEDIDO
                           //join x in db.TB_GOU_PEDIDO_ITEMs on a.ID_PEDIDO equals x.ID_PEDIDO
                            where b.TP_MOVIMENTO == "S"
                           && b.ST_PEDIDO != "F"
                           && y.ST_ATIVO != false
                           && a.ID_CARTAO == (bsCartoes[i] as MPedido).ID_CARTAO
                           select new MPedidoItem
                           {
                               ID_PEDIDO = a.ID_PEDIDO,
                               ID_ITEM = y.ID_ITEM,
                               ID_PRODUTO = y.ID_PRODUTO ?? 0,
                               QUANTIDADE = y.QT ?? 0m,
                               VL_UNITARIO = y.VL_UNITARIO ?? 0m,
                               VL_ACRESCIMO = y.VL_ACRESCIMO ?? 0m,
                               VL_DESCONTO = y.VL_DESCONTO ?? 0m,
                               VL_SUBTOTAL = y.VL_SUBTOTAL ?? 0m,
                               ST_ATIVO = y.ST_ATIVO,
                               ST_IMPRESSO = y.TB_GOU_PEDIDO_ITEM.ST_IMPRESSO,
                               OBS = y.DS_OBSERVACAO
                           }).ToList();

            for (int q = 0; q < lresult.Count; q++)
                _Itens.Add(lresult[q]);
            }
            return _Itens;
        }

        public override void Gravar()
        {
            try
            {
                afterTransferir(BuscaItensCartoes());
            }
            catch (Exception ex)
            {
                new UTILS.SYSException(ex.Message);
            }
        }

        private void BuscaCartoes()
        {
            var db = Conexao.BancoDados;

            var _cartoes = (from i in db.TB_GOU_PEDIDOs
                            join y in db.TB_COM_PEDIDOs on i.ID_PEDIDO equals y.ID_PEDIDO
                            where y.TP_MOVIMENTO == "S"
                            && y.ST_PEDIDO != "F"
                            && y.ST_ATIVO != false
                            && i.ID_CARTAO == teNumero.Text.TrimStart('0')
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
                                VALOR_PEDIDO = y.TB_COM_PEDIDO_ITEMs.Where(p => p.ST_ATIVO == true).Sum(p => p.VL_SUBTOTAL) ?? 0m,
                            }).ToList();

            int _jaexiste = 0;
            for (int i = 0; i < bsCartoes.Count ; i++)
                if((bsCartoes[i] as MPedido).ID_CARTAO == _cartoes[0].ID_CARTAO)
                    _jaexiste += 1;
                
            if(_jaexiste == 0)
                bsCartoes.Add(new MPedido {
                ID_CARTAO = _cartoes[0].ID_CARTAO,
                ID_PEDIDO = _cartoes[0].ID_PEDIDO,
                VALOR_PEDIDO = _cartoes[0].VALOR_PEDIDO
                });
        }
       
        private void Adicionar()
        {
            try
            {
                BuscaCartoes();
                teNumero.Text = "";
                teNumero.Focus();
            }
            catch (Exception ex)
            {
                new SYSException(ex.Message);
            }
        }

        private void FUnir_Load(object sender, EventArgs e)
        {

        }

        private void teNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Adicionar();
        }
    }
}
