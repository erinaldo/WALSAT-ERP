using SYS.QUERYS;
using SYS.QUERYS.Lancamentos.Estoque;
using SYS.UTILS;
using System;
using System.Data;
using System.Linq;

namespace SYS.FORMS.Lancamentos.Estoque
{
    public partial class FPreco_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        #region Declarações

        #endregion

        #region Métodos

        public FPreco_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FPreco_Lancamento() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beID_PRODUTO.Text = adicionar.preco.ID_PRODUTO.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvPreco.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var consulta = new QPreco();

                var preco = consulta.Buscar((int)selecionado.ID_PRODUTO).FirstOrDefault();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(preco, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Padroes()
        {
            beID_PRODUTO.Padronizar(true);
            teNM_PRODUTO.Padronizar(false);

            gcPreco.Padronizar();

            bbiAlterar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDeletar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        public override void Buscar()
        {
            var consulta = (from a in new QPreco().Buscar(beID_PRODUTO.Text.ToInt32(true).Padrao())
                            join b in Conexao.BancoDados.TB_EST_PRODUTOs on a.ID_PRODUTO equals b.ID_PRODUTO
                            where (a.ST_ATIVO ?? false)
                            select new
                            {
                                ID_PRODUTO = a.ID_PRODUTO,
                                NM_PRODUTO = b.NM,
                                TP_PRECO = a.TP_PRECO == "V" ? "Venda" : "Custo",
                                VL_PRECO = a.VL_PRECO
                            });

            teNM_PRODUTO.Text.Validar(true);
            if (teNM_PRODUTO.Text.TemValor())
                consulta = consulta.Where(a => a.NM_PRODUTO.Contains(teNM_PRODUTO.Text));

            gcPreco.DataSource = consulta;
            gvPreco.BestFitColumns(true);
        }

        #endregion

        #region Eventos

        private void FPreco_Busca_Shown(object sender, EventArgs e)
        {
            Padroes();



        }

        #endregion
    }
}