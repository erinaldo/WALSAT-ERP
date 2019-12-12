using SYS.UTILS;
using System;

namespace SYS.FORMS.Relatorios.Dashboard
{
    public partial class FDashboard : SYS.FORMS.FBase
    {
        public FDashboard()
        {
            InitializeComponent();
        }

        private void FDashboard_Load(object sender, EventArgs e)
        {
            tgGourmet.Visible = Parametros.ST_Gourmet ? true : false;
        }
    }
}
