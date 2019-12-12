using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Configuracao;
using SYS.UTILS;
using SYS.UTILS.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SYS.FORMS
{
    public partial class FLogin : SYS.FORMS.FBase
    {
        public FLogin()
        {
            InitializeComponent();

            UTILS.Extensoes.Padronizar(this);

            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    if (tcgAbas.SelectedTabPageName == lcgDadosLogin.Name)
                        sbEntrar.PerformClick();
                    else if (tcgAbas.SelectedTabPageName == lcgDadosAmbiente.Name)
                    {
                        sbTestar.PerformClick();
                        sbGravar.PerformClick();
                    }
            };

            Shown += delegate
             {
                 try
                 {
                     teServidor.Text = Settings.Default.SERVIDOR;
                     teBancoDados.Text = Settings.Default.BANCO;

                     tcgAbas.SelectedTabPage = (!teServidor.Text.TemValor() || !teBancoDados.Text.TemValor()) ? lcgDadosAmbiente : lcgDadosLogin;

                     if (tcgAbas.SelectedTabPageName == lcgDadosLogin.Name)
                         teUsuario.Focus();
                     else if (tcgAbas.SelectedTabPageName == lcgDadosAmbiente.Name)
                         teServidor.Focus();
                 }
                 catch (Exception excessao)
                 {
                     excessao.Validar();
                 }
             };

            sbEntrar.Click += delegate
            {
                try
                {
                    var validacao = new SYSException(Mensagens.Necessario("usuário/senha válidos"));

                    if (!teServidor.Text.Validar(true).TemValor() || !teBancoDados.Text.Validar(true).TemValor())
                        throw new SYSException(Mensagens.Necessario("servidor/banco de dados válidos"));

                    if (!teUsuario.Text.Validar(true).TemValor() || !teSenha.Text.Validar().TemValor())
                        throw validacao;

                    Settings.Default.SERVIDOR = teServidor.Text.Validar();
                    Settings.Default.BANCO = teBancoDados.Text.Validar();

                    Parametros.Servidor = teServidor.Text.Validar();
                    Parametros.Banco = teBancoDados.Text.Validar();
                    Parametros.ID_Usuario = teUsuario.Text.Validar();
                    Parametros.ST_Gourmet = true;

                    if (Conexao.Testar())
                    {
                        var usuario = new QUsuario();
                        var posicaoTransacao = 0;
                        usuario.Gravar(null, ref posicaoTransacao, true);

                        var existe = usuario.Buscar(teUsuario.Text.Validar(), teSenha.Text.Validar(), teUsuario.Text.Validar() == Parametros.BackdoorUsuario);

                        if (existe != null && existe.Count() == 1)
                        {
                            Parametros.NM_Usuario = Parametros.Backdoor ? "SYSADMIN" : (existe.FirstOrDefault().TB_REL_CLIFOR ?? new TB_REL_CLIFOR()).NM.Validar(true);
                            DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                            throw validacao;
                    }
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };

            sbTestar.Click += delegate
            {
                try {
                    Parametros.Servidor = teServidor.Text.Validar();
                    Parametros.Banco = teBancoDados.Text.Validar();

                    if (Conexao.Testar())
                        Mensagens.Sucesso("Conexão estabelecida com sucesso");
                }
                catch(Exception excessao)
                {
                    excessao.Validar();
                }
            };

            sbGravar.Click += delegate
            {
                try
                {
                    Settings.Default.SERVIDOR = teServidor.Text.Validar(true);
                    Settings.Default.BANCO = teBancoDados.Text.Validar(true);
                    Settings.Default.Save();

                    Mensagens.Sucesso();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
        }
    }
}