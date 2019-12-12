using SYS.QUERYS.Cadastros.Relacionamento;
using SYS.UTILS;
using System.Data;
using System.Linq;

namespace SYS.FORMS.Cadastros.Relacionamento
{
    public partial class FEndereco_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FEndereco_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FEndereco_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beID_ENDERECO.Text = adicionar.Endereco.ID_ENDERECO.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvEndereco.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID_ENDERECO;

                var endereco = new QEndereco().Buscar(ID).FirstOrDefaultDynamic();

                using (var alterar = new FEndereco_Cadastro() { Endereco = endereco, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beID_ENDERECO.Text = alterar.Endereco.ID_ENDERECO.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvEndereco.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QEndereco();

                var endereco = consulta.Buscar(ID).FirstOrDefaultDynamic();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(endereco, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var paisesUFsCidades = new QPaisUFCidade();

            var consulta = from a in (from a in new QEndereco().Buscar(beID_ENDERECO.Text.ToInt32() ?? 0)
                                      select new
                                      {
                                          ID_ENDERECO = a.ID_ENDERECO,
                                          NM_RUA = a.NM_RUA,
                                          NM_BAIRRO = a.NM_BAIRRO,
                                          NR = a.NR,
                                          ID_CIDADE = a.ID_CIDADE,
                                          ID_UF = a.ID_UNIDADEFEDERATIVA,
                                          ID_PAIS = a.ID_PAIS
                                      }).ToList().AsQueryable()// desprende do banco por causa do join que está local, e não no banco de dados

                           join b in paisesUFsCidades.Cidades on new { a.ID_CIDADE } equals new { ID_CIDADE = (int?)b.ID_CIDADE }
                           join c in paisesUFsCidades.UFs on new { a.ID_UF } equals new { ID_UF = (int?)c.ID_UF }
                           join d in paisesUFsCidades.Paises on new { a.ID_PAIS } equals new { ID_PAIS = (int?)d.ID_PAIS }
                           select new
                           {
                               a.ID_ENDERECO,
                               a.NM_RUA,
                               a.NM_BAIRRO,
                               a.NR,
                               NM_CIDADE = b.NM,
                               NM_UF = c.NM,
                               NM_PAIS = d.NM
                           };

            teNM_RUA.Text.Validar(true);
            if (teNM_RUA.Text.TemValor())
                consulta = consulta.Where(a => a.NM_RUA.Contains(teNM_RUA.Text));

            teNM_BAIRRO.Text.Validar(true);
            if (teNM_BAIRRO.Text.TemValor())
                consulta = consulta.Where(a => a.NM_BAIRRO.Contains(teNM_BAIRRO.Text));

            teNM_CIDADE.Text.Validar(true);
            if (teNM_CIDADE.Text.TemValor())
                consulta = consulta.Where(a => a.NM_CIDADE.Contains(teNM_CIDADE.Text));

            teNM_UF.Text.Validar(true);
            if (teNM_UF.Text.TemValor())
                consulta = consulta.Where(a => a.NM_UF.Contains(teNM_UF.Text));

            teNM_PAIS.Text.Validar(true);
            if (teNM_PAIS.Text.TemValor())
                consulta = consulta.Where(a => a.NM_PAIS.Contains(teNM_PAIS.Text));

            gcEndereco.DataSource = consulta;
            gvEndereco.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }
    }
}
