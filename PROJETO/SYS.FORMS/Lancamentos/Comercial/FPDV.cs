using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;
using DevExpress.XtraEditors;
using SYS.QUERYS.Cadastros;
using System.Linq;
using SYS.QUERYS;
using SYS.QUERYS.Lancamentos.Financeiro;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.QUERYS.Lancamentos.Estoque;
using SYS.FORMS.Lancamentos.Estoque;
using SYS.FORMS.Lancamentos.Comercial;

namespace SYS.FORMS.Comercial.Lancamentos
{
    public partial class FPDV : SYS.FORMS.FBase
    {
        #region Declarações

        private decimal vlDesconto = 0m;
        private decimal vlAcresimo = 0m;

        #endregion

        #region Métodos

        public FPDV()
        {
            InitializeComponent();



            #region Botões

            #region sbBuscarProduto




            #endregion

            #endregion
        }

        public override void Padroes()
        {
            this.Padronizar();

            beID_EMPRESA.Padronizar(true);
            teNM_EMPRESA.Padronizar(false);
            teID_BARRA_REFERENCIA.Padronizar(true);
            beID_PRODUTO.Padronizar(true);
            teNM_PRODUTO.Padronizar(false);
            teNM_MARCA.Padronizar(false);
        }

        #endregion

        #region Eventos
        private void FPDV_Shown(object sender, EventArgs e)
        {
            try
            {
                auim.Hide();

                var posicaoTransacao = 0;

                var caixaGeral = new QCaixaGeral();
                if (!caixaGeral.CaixaAberto())
                    caixaGeral.Abrir(ref posicaoTransacao);


            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        private void FPDV_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    auim.Show();

                    var finalizarAjuda = new Timer() { Interval = 3000 };
                    finalizarAjuda.Tick += delegate
                    {
                        auim.Hide();
                        finalizarAjuda.Stop();
                        finalizarAjuda.Dispose();
                    };
                    finalizarAjuda.Start();

                    return;
                }

                auim.Hide();

                if (e.KeyCode == Keys.F2)
                    beID_PRODUTO_ButtonClick(null, null);
                else if (e.KeyCode == Keys.F3)
                    sbAdicionarProduto.PerformClick();
                else if (e.KeyCode == Keys.F4)
                    sbRemoverProduto.PerformClick();
                else if (e.KeyCode == Keys.F5)
                    sbAbrirCaixa.PerformClick();
                else if (e.KeyCode == Keys.F6)
                    sbFecharCaixa.PerformClick();
                else if (e.KeyCode == Keys.F7)
                    sbAdicionarDinheiro.PerformClick();
                else if (e.KeyCode == Keys.F8)
                    sbRemoverDinheiro.PerformClick();
                else if (e.KeyCode == Keys.F9)
                    sbIniciarVenda.PerformClick();
                else if (e.KeyCode == Keys.F10)
                    sbFinalizarVenda.PerformClick();
                //else if (e.KeyCode == Keys.F11)
                //    sbTrocarUsuario.PerformClick();
                //else if (e.KeyCode == Keys.F12)
                //    sbFecharPDV.PerformClick();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private IQueryable Produtos(bool leave)
        {
            var produto = beID_PRODUTO.Text.ToInt32(true).Padrao();

            if (leave && produto <= 0)
                return null;

            var consulta = new QProduto();
            var id_empresa = beID_EMPRESA.Text.ToInt32().Padrao();

            var retorno = from a in consulta.Buscar((leave ? produto : 0))

                          let marca = a.ID_MARCA != null ? new QMarca().Buscar(a.ID_MARCA.Padrao()).FirstOrDefault() : (TB_EST_MARCA)null
                          let estoque = new QEstoque().BuscarSaldo(a.ID_PRODUTO, id_empresa)

                          where a.ST_ATIVO.Padrao()
                          select new
                          {
                              ID_PRODUTO = a.ID_PRODUTO,
                              NM_PRODUTO = a.NM,
                              NM_MARCA = marca.NM,
                              QT_ESTOQUE = 0m
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }
        private void beID_PRODUTO_Leave(object sender, EventArgs e)
        {
            try
            {
                PreencherProduto(Produtos(true));
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void PreencherProduto(dynamic produto, bool novaVenda = false)
        {
            var idProduto = (produto.Selecionados.FirstOrDefault().ID_PRODUTO as int?).Padrao();

            if (idProduto.TemValor())
            {
                beID_PRODUTO.Text = idProduto.ToString();
                teNM_PRODUTO.Text = (produto.Selecionados.FirstOrDefault().NM_PRODUTO as string).Validar();
                teNM_MARCA.Text = (produto.Selecionados.FirstOrDefault().NM_MARCA as string).Validar();
                seQT.Value = 1;
                seVL_UNITARIO.Value = (produto.Selecionados.FirstOrDefault().VL_UNITARIO as decimal?).Padrao();
            }
            else
            {
                beID_PRODUTO.Text = "";
                teNM_PRODUTO.Text = "";
                teNM_MARCA.Text = "";
                seQT.Value = 0;
                seVL_UNITARIO.Value = 0;

                if (novaVenda)
                {
                    seQT_PRODUTOS.Value = 0;
                    seVL_SUBTOTAL.Value = 0;
                    seVL_TOTAL.Value = 0;
                }
            }
        }

        private void beID_PRODUTO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new FFiltro()
                {
                    Consulta = Produtos(false),
                    Colunas = new List<Coluna>()
                    {
                        new Coluna { Nome = "ID_PRODUTO", Descricao = "Id. do produto", Tamanho = 100},
                        new Coluna { Nome = "NM_PRODUTO", Descricao = "Nome do produto", Tamanho = 350},
                        new Coluna { Nome = "NM_MARCA", Descricao = "Nome da marca", Tamanho = 350},
                        new Coluna { Nome = "QT_ESTOQUE", Descricao = "Qt. em estoque", Tamanho = 350},
                        new Coluna { Nome = "VL_UNITARIO", Descricao = "Vl. unitário", Tamanho = 350},
                    }
                })
                {
                    if (filtro.ShowDialog() == DialogResult.OK)
                        PreencherProduto(filtro.Selecionados.FirstOrDefault());
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void sbAdicionarProduto_Click(object sender, EventArgs e)
        {
            try
            {
                if (seVL_UNITARIO.Value <= 0)
                {
                    XtraMessageBox.Show(Mensagens.Necessario("o preço do produto " + beID_PRODUTO.Text.Trim() + " - " + teNM_PRODUTO.Text.Trim(), "lançar"));

                    using (var preco = new FPreco_Lancamento
                    {
                        preco = new TB_EST_PRECO
                        {
                            ID_PRODUTO = beID_PRODUTO.Text.ToInt32().Padrao(),
                            TP_PRECO = "V"
                        },
                        Modo = Modo.Alterar
                    })
                    {
                        if (preco.ShowDialog() != DialogResult.OK)
                            return;
                    }
                }

                if (seQT.Value <= 0)
                {
                    seQT.Focus();
                    throw new SYSException(Mensagens.Necessario("a quantidade"));
                }

                var existentes = gvProdutos.DataSource as BindingList<dynamic>;

                existentes.Add(new
                {
                    ID_POSICAO = existentes.Count + 1,
                    NM_PRODUTO = teNM_PRODUTO.Text.Validar(),
                    QT = seQT.Value.ENUS(2),
                    VL = seVL_SUBTOTAL.Value.ENUS(2)
                });

                vlDesconto = 0m;
                vlAcresimo = 0m;

                seQT.Value = existentes.Count;
                seVL_TOTAL.Value = existentes.Sum(a => Convert.ToDecimal(a.VL));

                gcProdutos.DataSource = existentes;
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void sbRemoverProduto_Click(object sender, EventArgs e)
        {
            try
            {
                var selecionado = gvProdutos.GetSelectedRow();

                if (selecionado == null)
                    return;
                
                var existentes = gvProdutos.DataSource as BindingList<dynamic>;
                existentes.Remove(selecionado);

                gcProdutos.DataSource = existentes;
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void sbDesconto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!beID_PRODUTO.Text.TemValor())
                    throw new SYSException(Mensagens.Necessario("o produto antes do desconto"));

                using (var desconto = new FPDV_DescontoAcrescimo())
                {
                    desconto.seVL_MAXIMO.Value = seVL_UNITARIO.Value * seQT.Value;
                    vlDesconto = desconto.seVL.Value;

                    if (desconto.ShowDialog() == DialogResult.OK)
                    {
                        seVL_SUBTOTAL.Value = (seVL_UNITARIO.Value * seQT.Value) + vlAcresimo - vlDesconto;
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void sbAcrescimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!beID_PRODUTO.Text.TemValor())
                    throw new SYSException(Mensagens.Necessario("o produto antes do acréscimo"));

                using (var acrescimo = new FPDV_DescontoAcrescimo())
                {
                    acrescimo.seVL_MAXIMO.Value = seVL_UNITARIO.Value * seQT.Value;
                    vlAcresimo = acrescimo.seVL.Value;

                    if (acrescimo.ShowDialog() == DialogResult.OK)
                        seVL_SUBTOTAL.Value = (seVL_UNITARIO.Value * seQT.Value) + vlAcresimo - vlDesconto;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void sbAbrirCaixa_Click(object sender, EventArgs e)
        {
            try
            {
                using (var contagem = new FPDV_Contagem()
                {
                    
                })
                {
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void sbIniciarVenda_Click(object sender, EventArgs e)
        {
            try
            {
                gcProdutos.DataSource = new BindingList<dynamic>();

                beID_PRODUTO.Text = "";
                beID_PRODUTO_Leave(null, null);
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}