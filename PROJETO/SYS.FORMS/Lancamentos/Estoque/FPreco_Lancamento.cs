using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.QUERYS.Lancamentos.Estoque;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SYS.FORMS.Lancamentos.Estoque
{
    public partial class FPreco_Lancamento : SYS.FORMS.FBase_Cadastro
    {
        #region Declarações

        public TB_EST_PRECO preco = null;

        #endregion

        #region Métodos

        public FPreco_Lancamento()
        {
            InitializeComponent();
        }

        public override void Padroes()
        {
            teID_PRECO.Padronizar(true);
            beID_PRODUTO.Padronizar(true);
            //seVL_PRECO.Padronizar(2, 0, decimal.MaxValue, Simbologia.Moeda);
        }

        private IQueryable Produto(bool leave)
        {
            var produto = beID_PRODUTO.Text.Trim().ToInt32(true).Padrao();

            if (leave && produto <= 0)
                return null;

            var consulta = new QProduto();
            var retorno = from a in consulta.Buscar((leave ? produto : 0))
                          select new
                          {
                              ID = a.ID_PRODUTO,
                              NM = a.NM,
                          };

            if (leave)
                retorno = retorno.Take(1);

            return retorno;
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                preco = new TB_EST_PRECO();
                preco.ID_PRECO = beID_PRODUTO.Text.ToInt32().Padrao();
                preco.ID_PRODUTO = beID_PRODUTO.Text.Trim().ToInt32().Padrao();
                preco.TP_PRECO = rgTP_PRECO.SelectedIndex == 0 ? "V" : "C";
                preco.VL_PRECO = seVL_PRECO.Value;
                preco.ST_ATIVO = true;

                var posicaoTransacao = 0;
                new QPreco().Gravar(preco, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Eventos

        private void FPreco_Lancamento_Shown(object sender, EventArgs e)
        {
            try
            {
                Padroes();

                if (Modo == Modo.Cadastrar)
                    preco = new TB_EST_PRECO();
                else if (Modo == Modo.Alterar)
                {
                    if (preco == null)
                        Excessoes.Alterar();

                    // Colocado a vericação para que quando não tiver preço no produto lançado no PDV, caia como alteração com os valores pré cadastrados.
                    if(preco.ID_PRECO.TemValor())
                    teID_PRECO.Text = preco.ID_PRECO.ToString();

                    beID_PRODUTO.Text = preco.ID_PRODUTO.ToString();
                    beID_PRODUTO_Leave(null, null);
                    rgTP_PRECO.SelectedIndex = preco.TP_PRECO.Validar() == "V" ? 0 : 1;
                    seVL_PRECO.Value = preco.VL_PRECO.Padrao();
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_PRODUTO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (var filtro = new FFiltro()
                {
                    Consulta = Produto(false),
                    Colunas = new List<Coluna>()
                    {
                        new Coluna { Nome = "ID", Descricao = "Id. do produto", Tamanho = 100},
                        new Coluna { Nome = "NM", Descricao = "Nome do produto", Tamanho = 350}
                    }
                })
                {
                    if (filtro.ShowDialog() == DialogResult.OK)
                    {
                        beID_PRODUTO.Text = (filtro.Selecionados.FirstOrDefault().ID as int?).Padrao().ToString();
                        teNM_PRODUTO.Text = (filtro.Selecionados.FirstOrDefault().NM as string).Validar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beID_PRODUTO_Leave(object sender, EventArgs e)
        {
            try
            {
                var grupo = Produto(true).FirstOrDefaultDynamic();

                beID_PRODUTO.Text = grupo != null ? (grupo.ID as int?).Padrao().ToString() : "";
                teNM_PRODUTO.Text = grupo != null ? (grupo.NM as string).Validar() : "";
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion
    }
}