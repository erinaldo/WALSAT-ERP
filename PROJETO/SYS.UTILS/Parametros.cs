using DevExpress.LookAndFeel;
using System.IO;

namespace SYS.UTILS
{
    public static class Parametros
    {
        public static string Servidor;
        public static string Banco;
        public static string BackdoorUsuario = "SA";
        public static string BackdoorSenha = "";
        public static bool Backdoor
        {
            get
            {
                return BackdoorUsuario == ID_Usuario;
            }
        }
        public static DefaultLookAndFeel Aparencia;
        public static string AparenciaNome = "DevExpress";

        public static string ID_Usuario;
        public static string NM_Usuario;

        public static int ID_MoedaPadrao = 790; // Real Brasil, conforme tabela interna de moedas

        public static bool ST_Gourmet = true;

        public static string Versao = "1.0";

        public static string StringConexao
        {
            get
            {
                return string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", Servidor, Banco, BackdoorUsuario, BackdoorSenha);
            }
        }

        public static string CaminhoPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}