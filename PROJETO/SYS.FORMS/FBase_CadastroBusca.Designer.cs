namespace SYS.FORMS
{
    partial class FBase_CadastroBusca
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiAjuda = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAdicionar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAlterar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeletar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBuscar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDetalhar = new DevExpress.XtraBars.BarButtonItem();
            this.bbiFicha = new DevExpress.XtraBars.BarButtonItem();
            this.bbiFechar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.HideBarsWhenMerging = false;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiAdicionar,
            this.bbiAlterar,
            this.bbiDeletar,
            this.bbiBuscar,
            this.bbiDetalhar,
            this.bbiFechar,
            this.bbiFicha,
            this.bbiAjuda});
            this.barManager1.MainMenu = this.bar1;
            this.barManager1.MaxItemId = 8;
            this.barManager1.MdiMenuMergeStyle = DevExpress.XtraBars.BarMdiMenuMergeStyle.Never;
            // 
            // bar1
            // 
            this.bar1.BarName = "Menu";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAjuda),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAdicionar),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAlterar),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeletar),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiBuscar, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDetalhar),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiFicha),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiFechar, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Menu";
            // 
            // bbiAjuda
            // 
            this.bbiAjuda.Caption = "(F1)\nAjuda";
            this.bbiAjuda.Glyph = global::SYS.FORMS.Properties.Resources.help_x32;
            this.bbiAjuda.Id = 7;
            this.bbiAjuda.Name = "bbiAjuda";
            this.bbiAjuda.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiAdicionar
            // 
            this.bbiAdicionar.Caption = "(F2)\nAdicionar";
            this.bbiAdicionar.Glyph = global::SYS.FORMS.Properties.Resources.add_x32;
            this.bbiAdicionar.Id = 0;
            this.bbiAdicionar.Name = "bbiAdicionar";
            this.bbiAdicionar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiAlterar
            // 
            this.bbiAlterar.Caption = "(F3)\nAlterar";
            this.bbiAlterar.Glyph = global::SYS.FORMS.Properties.Resources.refresh_update_x32;
            this.bbiAlterar.Id = 1;
            this.bbiAlterar.Name = "bbiAlterar";
            this.bbiAlterar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiDeletar
            // 
            this.bbiDeletar.Caption = "(F4)\nDeletar";
            this.bbiDeletar.Glyph = global::SYS.FORMS.Properties.Resources.delete_x32;
            this.bbiDeletar.Id = 2;
            this.bbiDeletar.Name = "bbiDeletar";
            this.bbiDeletar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiBuscar
            // 
            this.bbiBuscar.Caption = "(F5)\nBuscar";
            this.bbiBuscar.Glyph = global::SYS.FORMS.Properties.Resources.search_x32;
            this.bbiBuscar.Id = 3;
            this.bbiBuscar.Name = "bbiBuscar";
            this.bbiBuscar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiDetalhar
            // 
            this.bbiDetalhar.Caption = "(F6)\nDetalhar";
            this.bbiDetalhar.Glyph = global::SYS.FORMS.Properties.Resources.print_x32;
            this.bbiDetalhar.Id = 4;
            this.bbiDetalhar.Name = "bbiDetalhar";
            this.bbiDetalhar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiFicha
            // 
            this.bbiFicha.Caption = "(F7)\nFicha";
            this.bbiFicha.Glyph = global::SYS.FORMS.Properties.Resources.order_x321;
            this.bbiFicha.Id = 6;
            this.bbiFicha.Name = "bbiFicha";
            this.bbiFicha.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiFechar
            // 
            this.bbiFechar.Caption = "(ESC)\nFechar";
            this.bbiFechar.Glyph = global::SYS.FORMS.Properties.Resources.close_window_x32;
            this.bbiFechar.Id = 5;
            this.bbiFechar.Name = "bbiFechar";
            this.bbiFechar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(784, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 411);
            this.barDockControlBottom.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 371);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(784, 40);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 371);
            // 
            // FBase_CadastroBusca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "FBase_CadastroBusca";
            this.Text = "Busca de cadastro";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FBase_CadastroBusca_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem bbiAdicionar;
        public DevExpress.XtraBars.BarButtonItem bbiAlterar;
        public DevExpress.XtraBars.BarButtonItem bbiDeletar;
        public DevExpress.XtraBars.BarButtonItem bbiBuscar;
        public DevExpress.XtraBars.BarButtonItem bbiDetalhar;
        public DevExpress.XtraBars.BarButtonItem bbiFechar;
        public DevExpress.XtraBars.BarButtonItem bbiFicha;
        private DevExpress.XtraBars.BarButtonItem bbiAjuda;
    }
}
