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
    public partial class FPDV_DescontoAcrescimo : SYS.FORMS.FBase_Cadastro
    {
        public enum Tipo
        {
            Desconto,
            Acrescimo
        }

        public Tipo tipo = Tipo.Acrescimo;

        public FPDV_DescontoAcrescimo()
        {
            InitializeComponent();
        }

        public override void Padroes()
        {
            this.Padronizar();
        }

        private void FPDV_DescontoAcrescimo_Shown(object sender, EventArgs e)
        {
            try
            {
                Padroes();

                Text = tipo == Tipo.Acrescimo ? "Acréscimo" : "Desconto";
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
                if (seVL.Value > seVL_MAXIMO.Value)
                    throw new SYSException(Mensagens.Necessario("um valor menor que o máximo"));

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}