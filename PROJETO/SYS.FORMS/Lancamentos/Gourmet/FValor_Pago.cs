using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FValor_Pago : SYS.FORMS.FBase
    {
        public FValor_Pago()
        {
            InitializeComponent();
        }

        public decimal Valor;
        private decimal valorInicial;

        private void FValor_Pago_Load(object sender, EventArgs e)
        {
            spValor.Focus();
            spValor.Value = Valor;
            valorInicial = Valor;
        }

        private void spValor_Leave(object sender, EventArgs e)
        {
            spValor.Focus();
        }

        private void spValor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (spValor.Value < valorInicial)
                        throw new SYSException("Valor deve ser maior que valor cobrado!", "Atenção");

                    Valor = spValor.Value;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}

