using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using SYS.UTILS;
using System.Windows.Forms;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FMotivoCancelamento : SYS.FORMS.FBase
    {
        public string DS_Motivo = "";

        public FMotivoCancelamento()
        {
            InitializeComponent();

            sbOK.Click += delegate{

                if (meMotivo.Text.TemValor())
                    DS_Motivo = meMotivo.Text.Trim();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();                
            };

            sbCancel.Click += delegate{
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            };
        }
    }
}
