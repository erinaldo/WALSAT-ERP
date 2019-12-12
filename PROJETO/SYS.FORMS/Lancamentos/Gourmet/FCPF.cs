using DevExpress.XtraEditors;
using SYS.UTILS;
using System.Linq;
using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Relacionamento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Lancamentos.Gourmet
{
    public partial class FCPF : SYS.FORMS.FBase
    {
        public FCPF()
        {
            InitializeComponent();
        }

        private void teCPF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                sbOK_Click(null, null);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void sbOK_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = new QClifor().Buscar(1).ToList();

                var novoCliente = new TB_REL_CLIFOR();
                novoCliente.ID_CLIFOR = cliente[0].ID_CLIFOR;
                novoCliente.CPF = teCPF.Text.Trim().Validar();
                novoCliente.NM = cliente[0].NM;
                novoCliente.NM_FANTASIA = cliente[0].NM_FANTASIA;
                novoCliente.ST_ATIVO = true;
                novoCliente.CNPJ = cliente[0].CNPJ;
                novoCliente.IE = cliente[0].IE;
                novoCliente.RG = cliente[0].RG;              

                int posicao_transacao = 0;

                new QClifor().Gravar(novoCliente,ref posicao_transacao);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void teCPF_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void FCPF_Load(object sender, EventArgs e)
        {
            teCPF.Focus();
        }

        
    }
}
