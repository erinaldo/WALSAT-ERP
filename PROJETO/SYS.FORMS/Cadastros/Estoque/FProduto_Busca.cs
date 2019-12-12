using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.UTILS;
using System.Data;
using System.Linq;

namespace SYS.FORMS.Cadastros.Estoque
{
    public partial class FProduto_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        #region Métodos

        public FProduto_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FProduto_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beID_PRODUTO.Text = adicionar.Produto.ID_PRODUTO.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvProduto.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var produto = new QProduto().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefault();

                using (var alterar = new FProduto_Cadastro() { Produto = produto, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beID_PRODUTO.Text = alterar.Produto.ID_PRODUTO.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvProduto.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QProduto();
                var produto = consulta.Buscar(ID).FirstOrDefault();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(produto, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QProduto().Buscar(beID_PRODUTO.Text.ToInt32(true).Padrao())
                            let barras = Conexao.BancoDados.TB_EST_PRODUTO_BARRAs.FirstOrDefault(p => p.ID_PRODUTO.Equals(a.ID_PRODUTO)).ID_BARRA_REFERENCIA
                            select new
                            {
                                ID = a.ID_PRODUTO,
                                NM = a.NM,
                                CD_BARRA = barras
                            });

            teNM_PRODUTO.Text.Validar(true);
            if (teNM_PRODUTO.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNM_PRODUTO.Text));

            gcProduto.DataSource = consulta;
            gvProduto.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }

        public override void Padroes()
        {
            // Padronizações
            this.Padronizar();
            beID_PRODUTO.Padronizar(true);
            teNM_PRODUTO.Padronizar(false);
            teID_BARRA_REFERENCIA.Padronizar(true);

            gcProduto.Padronizar();
        }

        #endregion
    }
}