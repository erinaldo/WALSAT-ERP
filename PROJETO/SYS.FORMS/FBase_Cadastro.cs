using SYS.UTILS;
using System.Windows.Forms;

namespace SYS.FORMS
{
    public partial class FBase_Cadastro : SYS.FORMS.FBase
    {
        public FBase_Cadastro()
        {
            InitializeComponent();
            

            bbiGravar.ItemClick+= delegate { Gravar();};
            bbiCancelar.ItemClick += delegate { Cancelar(); };
        }

        private void FBase_Cadastro_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2: bbiGravar.PerformClick(); return;
                case Keys.Escape: bbiCancelar.PerformClick(); return;
            }
        }

        public Modo Modo;

        public virtual void Validar() { }

        public virtual void Gravar()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }


        public virtual void Cancelar()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
