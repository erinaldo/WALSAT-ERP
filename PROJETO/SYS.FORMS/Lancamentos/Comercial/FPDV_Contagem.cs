using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;

namespace SYS.FORMS.Lancamentos.Comercial
{
    public partial class FPDV_Contagem : SYS.FORMS.FBase_Cadastro
    {
        public FPDV_Contagem()
        {
            InitializeComponent();
        }

        public override void Padroes()
        {
            try
            {
                bbiCancelar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        public override void Gravar()
        {
            try
            {
                if (seVL_TOTAL.Value < 0)
                    throw new SYSException(Mensagens.Necessario("um valor total válido!"));

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}