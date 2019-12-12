namespace SYS.FORMS.Lancamentos.Financeiro
{
    partial class FCaixa
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcCaixa = new DevExpress.XtraGrid.GridControl();
            this.gvCaixa = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ceFechados = new DevExpress.XtraEditors.CheckEdit();
            this.deCaixa = new DevExpress.XtraEditors.ButtonEdit();
            this.teUsuario = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCaixa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCaixa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceFechados.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deCaixa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUsuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcCaixa);
            this.layoutControl1.Controls.Add(this.ceFechados);
            this.layoutControl1.Controls.Add(this.deCaixa);
            this.layoutControl1.Controls.Add(this.teUsuario);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(784, 371);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcCaixa
            // 
            this.gcCaixa.Location = new System.Drawing.Point(34, 134);
            this.gcCaixa.MainView = this.gvCaixa;
            this.gcCaixa.Name = "gcCaixa";
            this.gcCaixa.Size = new System.Drawing.Size(716, 203);
            this.gcCaixa.TabIndex = 8;
            this.gcCaixa.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCaixa});
            // 
            // gvCaixa
            // 
            this.gvCaixa.GridControl = this.gcCaixa;
            this.gvCaixa.Name = "gvCaixa";
            // 
            // ceFechados
            // 
            this.ceFechados.Location = new System.Drawing.Point(682, 52);
            this.ceFechados.Name = "ceFechados";
            this.ceFechados.Properties.Caption = "Fechados";
            this.ceFechados.Size = new System.Drawing.Size(68, 19);
            this.ceFechados.StyleController = this.layoutControl1;
            this.ceFechados.TabIndex = 7;
            // 
            // deCaixa
            // 
            this.deCaixa.Location = new System.Drawing.Point(34, 68);
            this.deCaixa.Name = "deCaixa";
            this.deCaixa.Properties.MaxLength = 10;
            this.deCaixa.Size = new System.Drawing.Size(83, 20);
            this.deCaixa.StyleController = this.layoutControl1;
            this.deCaixa.TabIndex = 5;
            // 
            // teUsuario
            // 
            this.teUsuario.Location = new System.Drawing.Point(121, 68);
            this.teUsuario.Name = "teUsuario";
            this.teUsuario.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.teUsuario.Properties.MaxLength = 256;
            this.teUsuario.Size = new System.Drawing.Size(557, 20);
            this.teUsuario.StyleController = this.layoutControl1;
            this.teUsuario.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.Root});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(784, 371);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(764, 351);
            this.Root.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Filtros de busca";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(744, 82);
            this.layoutControlGroup3.Text = "Filtros de busca";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.deCaixa;
            this.layoutControlItem2.CustomizationFormText = "Identificador do produto";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(87, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(87, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(87, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Data caixa";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(51, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teUsuario;
            this.layoutControlItem3.CustomizationFormText = "Nome do produto";
            this.layoutControlItem3.Location = new System.Drawing.Point(87, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(561, 40);
            this.layoutControlItem3.Text = "Usuário";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(51, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ceFechados;
            this.layoutControlItem1.Location = new System.Drawing.Point(648, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(72, 40);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "Produtos cadastrados";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 82);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(744, 249);
            this.layoutControlGroup4.Text = "Resumo de Caixas";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcCaixa;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(720, 207);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // FCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FCaixa";
            this.Text = "Caixa por usuário";
            this.Load += new System.EventHandler(this.FCaixa_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCaixa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCaixa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceFechados.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deCaixa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUsuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcCaixa;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCaixa;
        private DevExpress.XtraEditors.CheckEdit ceFechados;
        private DevExpress.XtraEditors.ButtonEdit deCaixa;
        private DevExpress.XtraEditors.TextEdit teUsuario;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;

    }
}
