using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.QUERYS;

namespace SYS.FORMS.Relatorios
{
    public partial class FRelancao_Venda : SYS.FORMS.FBase
    {
        public FRelancao_Venda()
        {
            InitializeComponent();

            beIDProduto.ButtonClick += delegate
            {
                try
                {
                    using (var filtro = new SYS.FORMS.FFiltro()
                    {
                        Consulta = Produtos(false),
                        Colunas = new List<SYS.FORMS.Coluna>()
                                {
                                    new SYS.FORMS.Coluna { Nome = "ID", Descricao = "Identificador do Produto", Tamanho = 100},
                                    new SYS.FORMS.Coluna { Nome = "NM",Descricao = "Nome do Produto", Tamanho = 350}
                                }
                    })
                    {
                        if (filtro.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            beIDProduto.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                            teNMProduto.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                        }
                    }
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            Action ProdutoLeave = delegate
            {
                try
                {
                    var produto = Produtos(true).FirstOrDefaultDynamic();

                    if (produto != null)
                    {
                        beIDProduto.Text = produto != null ? (produto.ID as int?).Padrao().ToString() : "";
                        teNMProduto.Text = produto != null ? (produto.NM as string).Validar() : "";
                    }
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            beIDProduto.Leave += delegate { ProdutoLeave(); };
        }

        private IQueryable Produtos(bool leave)
        {
            var produto = beIDProduto.Text.ToInt32(true).Padrao();

            if (leave && produto <= 0)
                return null;

            var consulta = new QProduto();
            var retorno = from a in consulta.Buscar((leave ? produto : 0))
                          join b in Conexao.BancoDados.TB_EST_GRUPOs on a.ID_GRUPO equals b.ID_GRUPO
                          where !b.ST_COMPLEMENTO ?? false
                          select new
                          {
                              ID = a.ID_PRODUTO,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var db = Conexao.BancoDados;

                gcItens.DataSource = null;

                if (ceAnalitico.Checked)
                {
                    var lresult = (from i in db.TB_COM_PEDIDOs
                                  join x in db.TB_COM_PEDIDO_ITEMs on i.ID_PEDIDO equals x.ID_PEDIDO
                                  join y in db.TB_EST_PRODUTOs on x.ID_PRODUTO equals y.ID_PRODUTO
                                  where (i.ST_ATIVO ?? false)  
                                  && (x.ST_ATIVO ?? false)
                                  select new
                                  {
                                      Codigo = y.ID_PRODUTO,
                                      Nome = y.NM,
                                      Unitario = x.VL_UNITARIO,
                                      Quantidade = x.QT,
                                      Subtotal = x.VL_SUBTOTAL,
                                      Data = i.DT_CADASTRO,
                                      //TP_Pagamento = i.ID_FORMAPAGAMENTO
                                  });

                    //if (!rbTodos.Checked)
                    //{
                    //    lresult = from i in lresult
                    //              where i.TP_Pagamento == (rbPrazo.Checked ? "05" : rbDinh.Checked ? "01" : "03")
                    //              || i.TP_Pagamento == (rbPrazo.Checked ? "05" : rbDinh.Checked ? "01" : "04")
                    //              select i;
                    //}

                    if (teDtInicial.Text.Trim().Length > 0)
                    {
                        var dataInicial = Convert.ToDateTime(teDtInicial.Text);
                        lresult = from i in lresult where i.Data >= dataInicial select i;
                    }
                    if (teDtIfinal.Text.Trim().Length > 0)
                    {
                        var dataFinal = Convert.ToDateTime(teDtIfinal.Text);
                        lresult = from i in lresult where i.Data <= dataFinal select i;
                    }
                    if (beIDProduto.Text.Trim().Length > 0)
                        lresult = from i in lresult where i.Codigo == Convert.ToInt32(beIDProduto.Text.Trim()) select i;

                    if (lresult.Count() > 0)
                    {
                        gcItens.DataSource = lresult;
                        spSubTotal.Value = lresult.Sum(p => p.Subtotal) ?? 0m;
                    }
                    else
                    {
                        gcItens.DataSource = null;
                        spSubTotal.Value = 0m;
                    }
                }
                else
                {
                    var lresult = (from i in db.TB_COM_PEDIDOs
                                   join x in db.TB_COM_PEDIDO_ITEMs on i.ID_PEDIDO equals x.ID_PEDIDO
                                   join y in db.TB_EST_PRODUTOs on x.ID_PRODUTO equals y.ID_PRODUTO
                                   where (i.ST_ATIVO ?? false)
                                   && (x.ST_ATIVO ?? false)
                                   group new { x, y, i } by new { y.ID_PRODUTO, y.NM, x.VL_UNITARIO, i.DT_CADASTRO.Value.Day, i.DT_CADASTRO.Value.Month, i.DT_CADASTRO.Value.Year/*,i.ID_FORMAPAGAMENTO */} into gr
                                   select new
                                   {
                                       Codigo = gr.Key.ID_PRODUTO,
                                       Nome = gr.Key.NM,
                                       Unitario = gr.Key.VL_UNITARIO,
                                       Quantidade = gr.Sum(p => p.x.QT),
                                       Subtotal = gr.Sum(p => p.x.VL_SUBTOTAL),
                                       Dia = gr.Key.Day,
                                       Mes = gr.Key.Month,
                                       Ano = gr.Key.Year,
                                       //TP_Pagamento = gr.Key.ID_FORMAPAGAMENTO
                                   });

                    //if (!rbTodos.Checked)
                    //{
                    //    lresult = from i in lresult
                    //              where i.TP_Pagamento == (rbPrazo.Checked ? "05" : rbDinh.Checked ? "01" : "03")
                    //              || i.TP_Pagamento == (rbPrazo.Checked ? "05" : rbDinh.Checked ? "01" : "04")
                    //              select i;
                    //}

                    if (teDtInicial.Text.Trim().Length > 0)
                    {
                        var dataInicial = Convert.ToDateTime(teDtInicial.Text);
                        lresult = from i in lresult where i.Dia >= dataInicial.Day && i.Mes >= dataInicial.Month && i.Ano >= dataInicial.Year select i;
                    }
                    if (teDtIfinal.Text.Trim().Length > 0)
                    {
                        var dataFinal = Convert.ToDateTime(teDtIfinal.Text);
                        lresult = from i in lresult where i.Dia <= dataFinal.Day && i.Mes <= dataFinal.Month && i.Ano <= dataFinal.Year select i;
                    }
                    if (beIDProduto.Text.Trim().Length > 0)
                        lresult = from i in lresult where i.Codigo == Convert.ToInt32(beIDProduto.Text.Trim()) select i;

                    var Fresult = from i in lresult
                                  select new
                                  {
                                      Codigo = i.Codigo,
                                      Nome = i.Nome,
                                      Unitario = i.Unitario,
                                      Quantidade = i.Quantidade,
                                      Subtotal = i.Subtotal,
                                      Data = new DateTime(i.Ano,i.Mes,i.Dia)
                                  };

                    if (Fresult.Count() > 0)
                    {
                        gcItens.DataSource = Fresult;
                        spSubTotal.Value = Fresult.Sum(p => p.Subtotal) ?? 0m;
                    }
                    else
                    {
                        gcItens.DataSource = null;
                        spSubTotal.Value = 0m;
                    }
                }

            }
            catch (Exception ex)
            {
                new SYSException(ex.Message);
            }
        }

        private void sbLimpar_Click(object sender, EventArgs e)
        {
            teDtInicial.Text = "";
            teDtIfinal.Text = "";
            beIDProduto.Text = "";
            teNMProduto.Text = "";
            teDtInicial.Focus();
        }

        private void bt_hoje_Click(object sender, EventArgs e)
        {
            teDtInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            teDtIfinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
