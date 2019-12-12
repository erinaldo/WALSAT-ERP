using SYS.QUERYS;
using System.Linq;
using SYS.QUERYS.Lancamentos.Financeiro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SYS.FORMS.Lancamentos.Financeiro
{
    public partial class FAberturaCaixa : SYS.FORMS.FBase_Cadastro
    {
        public FAberturaCaixa()
        {
            InitializeComponent();
        }
        public override void Gravar()
        {
            try
            {
                if (spValor.Value < 0)
                    throw new Exception("Valor deve ser maior ou igual a zero!");

                var caixa = new TB_FIN_CAIXA_LANCAMENTO_X_USUARIO();
                
                var busca = new QCaixaDiario().Buscar(SYS.UTILS.Parametros.ID_Usuario,DateTime.Now.ToString()).ToList();


                if (busca.Count > 0)
                {
                    if (busca[0].DT_FINAL == null)
                    {
                        caixa.DT_FINAL = DateTime.Now;
                        caixa.VL_FINAL = spValor.Value;
                    }
                }
                else
                {
                    caixa.DT_INICIAL = DateTime.Now;
                    caixa.VL_INICIAL = spValor.Value;
                }
                caixa.ID_USUARIO = SYS.UTILS.Parametros.ID_Usuario;

                int posicao_transacao = 0;
                new QCaixaDiario().Gravar(caixa, ref posicao_transacao);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();   
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}
