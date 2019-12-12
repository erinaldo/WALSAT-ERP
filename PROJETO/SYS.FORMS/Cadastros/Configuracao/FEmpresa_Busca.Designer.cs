namespace SYS.FORMS.Cadastros.Configuracao
{
    partial class FEmpresa_Busca
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
            this.gcEmpresa = new DevExpress.XtraGrid.GridControl();
            this.gvEmpresa = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.beEmpresa = new DevExpress.XtraEditors.ButtonEdit();
            this.teNMEmpresa = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.item0 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpresa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNMEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcEmpresa);
            this.layoutControl1.Controls.Add(this.beEmpresa);
            this.layoutControl1.Controls.Add(this.teNMEmpresa);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 371);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcEmpresa
            // 
            this.gcEmpresa.Location = new System.Drawing.Point(34, 134);
            this.gcEmpresa.MainView = this.gvEmpresa;
            this.gcEmpresa.Name = "gcEmpresa";
            this.gcEmpresa.Size = new System.Drawing.Size(616, 203);
            this.gcEmpresa.TabIndex = 7;
            this.gcEmpresa.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEmpresa});
            // 
            // gvEmpresa
            // 
            this.gvEmpresa.GridControl = this.gcEmpresa;
            this.gvEmpresa.Name = "gvEmpresa";
            // 
            // beEmpresa
            // 
            this.beEmpresa.Location = new System.Drawing.Point(34, 68);
            this.beEmpresa.Name = "beEmpresa";
            this.beEmpresa.Properties.MaxLength = 10;
            this.beEmpresa.Size = new System.Drawing.Size(121, 20);
            this.beEmpresa.StyleController = this.layoutControl1;
            this.beEmpresa.TabIndex = 5;
            // 
            // teNMEmpresa
            // 
            this.teNMEmpresa.Location = new System.Drawing.Point(159, 68);
            this.teNMEmpresa.Name = "teNMEmpresa";
            this.teNMEmpresa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.teNMEmpresa.Properties.MaxLength = 256;
            this.teNMEmpresa.Size = new System.Drawing.Size(491, 20);
            this.teNMEmpresa.StyleController = this.layoutControl1;
            this.teNMEmpresa.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.item0});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 371);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // item0
            // 
            this.item0.CustomizationFormText = "Root";
            this.item0.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.item0.GroupBordersVisible = false;
            this.item0.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.item0.Location = new System.Drawing.Point(0, 0);
            this.item0.Name = "item0";
            this.item0.Size = new System.Drawing.Size(664, 351);
            this.item0.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.item0.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Filtros de busca";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(644, 82);
            this.layoutControlGroup3.Text = "Filtros de busca";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.beEmpresa;
            this.layoutControlItem2.CustomizationFormText = "Identificador do produto";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(125, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(125, 1);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Identificador Empresa";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(105, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNMEmpresa;
            this.layoutControlItem3.CustomizationFormText = "Nome do produto";
            this.layoutControlItem3.Location = new System.Drawing.Point(125, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(495, 40);
            this.layoutControlItem3.Text = "Razão da Empresa";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(105, 13);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "Produtos cadastrados";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 82);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(644, 249);
            this.layoutControlGroup4.Text = "Empresas Cadastradas";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcEmpresa;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(620, 207);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FEmpresa_Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FEmpresa_Busca";
            this.Text = "Busca de Empresa";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpresa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNMEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.ButtonEdit beEmpresa;
        private DevExpress.XtraEditors.TextEdit teNMEmpresa;
        private DevExpress.XtraLayout.LayoutControlGroup item0;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraGrid.GridControl gcEmpresa;
        private DevExpress.XtraGrid.Views.Grid.GridView gvEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
