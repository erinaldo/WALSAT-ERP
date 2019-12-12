using SYS.UTILS;
using System;
using System.Windows.Forms;

namespace SYS.FORMS
{
    public partial class FBase_CadastroBusca : SYS.FORMS.FBase
    {
        public FBase_CadastroBusca()
        {
            InitializeComponent();

            // Invisiveis enquanto não terminarmos a parte de detalhe e ficha (que são mais demorados e menos prioritários até então)
            bbiAjuda.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiFicha.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiDetalhar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            bbiAjuda.ItemClick += delegate
            {
                try
                {
                    Ajuda();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            bbiAdicionar.ItemClick += delegate
            {
                try
                {
                    Adicionar();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            bbiAlterar.ItemClick += delegate
            {
                try
                {
                    Alterar();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            bbiDeletar.ItemClick += delegate
            {
                try
                {
                    Deletar();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            bbiBuscar.ItemClick += delegate
            {
                try
                {
                    Buscar();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            bbiDetalhar.ItemClick += delegate
            {
                try
                {
                    Detalhar();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            bbiFicha.ItemClick += delegate
            {
                try
                {
                    Ficha();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
            bbiFechar.ItemClick += delegate
            {
                try
                {
                    Fechar();
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
        }

        public virtual void Ajuda() { }
        public virtual void Adicionar() { }
        public virtual void Alterar() { }
        public virtual void Deletar() { }
        public virtual void Buscar() { }
        public virtual void Detalhar() { }
        public virtual void Ficha() { }
        public virtual void Fechar()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Dispose();
        }

        private void FBase_CadastroBusca_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1: bbiAjuda.PerformClick(); return;
                case Keys.F2: bbiAdicionar.PerformClick(); return;
                case Keys.F3: bbiAlterar.PerformClick(); return;
                case Keys.F4: bbiDeletar.PerformClick(); return;
                case Keys.F5: bbiBuscar.PerformClick(); return;
                case Keys.F6: bbiDetalhar.PerformClick(); return;
                case Keys.F7: bbiFicha.PerformClick(); return;
                case Keys.Escape: bbiFechar.PerformClick(); return;
            }
        }
    }
}