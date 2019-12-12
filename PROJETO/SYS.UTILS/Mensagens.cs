using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace SYS.UTILS
{
    public static class Mensagens
    {
        public static string Necessario(string necessario = "", string auxiliar = "informar")
        {
            return "É necessário " + auxiliar + " " + necessario + "!";
        }

        public static void Sucesso(string mensagem = "Gravado com sucesso!", MessageBoxButtons botoes = MessageBoxButtons.OK, MessageBoxIcon icone = MessageBoxIcon.Information)
        {
            XtraMessageBox.Show(Parametros.Aparencia.LookAndFeel, mensagem, "Sucesso!", botoes, icone);
        }

        public static void Erro(string mensagem = "Ocorreu um erro", MessageBoxButtons botoes = MessageBoxButtons.OK, MessageBoxIcon icone = MessageBoxIcon.Error)
        {
            XtraMessageBox.Show(Parametros.Aparencia.LookAndFeel, mensagem, "Erro!", botoes, icone);
        }

        public static DialogResult Pergunta(string mensagem)
        {
            return XtraMessageBox.Show(Parametros.Aparencia.LookAndFeel, mensagem, "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult Deletar()
        {
            return Pergunta("Deseja realmente deletar?");
        }

        public static void Deletado(bool cancelado = false)
        {
            Sucesso(string.Format("Registro {0} com sucesso!", (cancelado ? "cancelado" : "deletado")));
        }

        public static DialogResult Selecionar(string nome = "registro")
        {
            return XtraMessageBox.Show(Parametros.Aparencia.LookAndFeel, string.Format("Você precisa selecionar um {0} para alterar!", nome), "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}