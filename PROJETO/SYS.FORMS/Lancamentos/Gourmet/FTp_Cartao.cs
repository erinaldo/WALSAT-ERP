using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FTp_Cartao : SYS.FORMS.FBase
    {
        public FTp_Cartao()
        {
            InitializeComponent();
        }

        public string Tipo = "03";

        private void sbCredito_Click(object sender, EventArgs e)
        {
            Tipo = "03";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sbDebito_Click(object sender, EventArgs e)
        {
            Tipo = "04";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
