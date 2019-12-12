using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYS.UTILS
{
    [global::System.Serializable]
    public class SYSException : Exception
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public MessageBoxButtons Botoes { get; set; }
        public MessageBoxIcon Icone { get; set; }

        public SYSException(string mensagem, string titulo = "Erro!", MessageBoxButtons botoes = MessageBoxButtons.OK, MessageBoxIcon icone = MessageBoxIcon.Error)
        {
            this.Mensagem = mensagem;
            this.Titulo = titulo;
            this.Botoes = botoes;
            this.Icone = icone;
        }

        public void Mostrar()
        {
            XtraMessageBox.Show(this.Mensagem, this.Titulo, this.Botoes, this.Icone);
        }
    }
    public static class Excessoes
    {
        public static void Cadastrar()
        {
            throw new SYSException("Não foi possível cadastrar o registro desejado!");
        }
        public static void Alterar()
        {
            throw new SYSException("Não foi possível alterar o registro desejado!");
        }

        public static void Deletar()
        {
            throw new SYSException("Não foi possível deletar o registro desejado!");
        }

        public static void Generico(string mensagem)
        {
            throw new SYSException(mensagem);
        }

        public static void Validar(this Exception excessao, string titulo = "Erro!", MessageBoxButtons botoes = MessageBoxButtons.OK, MessageBoxIcon icone = MessageBoxIcon.Error)
        {
            if (excessao is SYSException)
                (excessao as SYSException).Mostrar();
            else
                XtraMessageBox.Show(excessao.Message, titulo, botoes, icone);
        }

        public static string Tratar(this Exception excessao)
        {
            var mensagem = excessao.InnerException != null ? excessao.InnerException.Message : excessao.Message;
            mensagem = mensagem.Replace("Ã£", "ã");
            mensagem = mensagem.Replace("Ãi", "á");
            mensagem = mensagem.Replace("Ã­", "í");

            return mensagem;
        }
    }
}