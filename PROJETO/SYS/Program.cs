using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using SYS.FORMS;
using SYS.UTILS;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SYS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            //Parametros.AparenciaNome = Settings.Default.SKIN.TemValor() ? Settings.Default.SKIN : Parametros.AparenciaNome;
            UserLookAndFeel.Default.SetSkinStyle(Parametros.AparenciaNome);


            //UserLookAndFeel.Default.StyleChanged += (s, e) =>
            //{
            //    Parametros.Aparencia = (UserLookAndFeelDefault)s;
                
            //    Settings.Default.SKIN = Parametros.Aparencia.ActiveSkinName;
            //    Parametros.AparenciaNome = Parametros.Aparencia.ActiveSkinName;

            //    Settings.Default.Save();
            //};

           

            var login = new FLogin();
            if (login.ShowDialog() == DialogResult.OK)
                Application.Run(new FMenu());
        }
    }
}