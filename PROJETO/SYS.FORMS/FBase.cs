using SYS.UTILS;
using DevExpress.LookAndFeel;

namespace SYS.FORMS
{
    public partial class FBase : DevExpress.XtraEditors.XtraForm
    {
        public FBase()
        {
            InitializeComponent();

            UTILS.Parametros.Aparencia = this.aparencia;
        }

        public virtual void Padroes()
        {
            
        }
    }
}