using SYS.QUERYS.Cadastros.Relacionamento;
using SYS.UTILS;
using System.Linq;

namespace SYS.FORMS.Cadastros.Relacionamento
{
    public partial class FClifor_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        #region Métodos

        public FClifor_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FClifor_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    teIdentificador.Text = adicionar.Clifor.ID_CLIFOR.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvClifor.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var clifor = new QClifor().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefault();

                using (var alterar = new FClifor_Cadastro() { Clifor = clifor, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        teIdentificador.Text = alterar.Clifor.ID_CLIFOR.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvClifor.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();

            int ID = selecionado.ID;

            var consulta = new QClifor();

            var clifor = consulta.Buscar(ID).FirstOrDefault();

            if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
            {
                var posicaoTransacao = 0;
                consulta.Deletar(clifor, ref posicaoTransacao);
                Mensagens.Deletado();
                Buscar();
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QClifor().Buscar(teIdentificador.Text.ToInt32(true).Padrao())
                            select new
                            {
                                ID = a.ID_CLIFOR,
                                NM = a.NM,
                                CPF_CNPJ = a.CPF ?? a.CNPJ,
                                TELEFONE_PRIMARIO = "",
                                TELEFONE_SECUNDARIO = ""                                
                            });

            teNomeClifor.Text.Validar(true);
            if (teNomeClifor.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNomeClifor.Text));

            teCPF.Text.Validar(true);
            if (teCPF.Text.Validar().TemValor())
                consulta = consulta.Where(a => a.CPF_CNPJ.Contains(teCPF.Text));

            teCNPJ.Text.Validar(true);
            if (teCNPJ.Text.Validar().TemValor())
                consulta = consulta.Where(a => a.CPF_CNPJ.Contains(teCNPJ.Text));

            teTelefone.Text.Validar(true);
            if (teTelefone.Text.Validar().TemValor())
                consulta = consulta.Where(a => a.TELEFONE_PRIMARIO.Contains(teTelefone.Text) || a.TELEFONE_SECUNDARIO.Contains(teTelefone.Text));

            gcClifor.DataSource = consulta;
            gvClifor.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }

        #endregion
    }
}