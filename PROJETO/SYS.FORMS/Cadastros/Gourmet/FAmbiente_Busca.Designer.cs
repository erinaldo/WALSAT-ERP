namespace SYS.FORMS.Cadastros.Gourmet
{
    partial class FAmbiente_Busca
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
            this.gcAmbiente = new DevExpress.XtraGrid.GridControl();
            this.gvAmbiente = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.beIdentificador = new DevExpress.XtraEditors.ButtonEdit();
            this.teNM = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.item0 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAmbiente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAmbiente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beIdentificador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.gcAmbiente);
            this.layoutControl1.Controls.Add(this.beIdentificador);
            this.layoutControl1.Controls.Add(this.teNM);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 318);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcAmbiente
            // 
            this.gcAmbiente.Location = new System.Drawing.Point(44, 144);
            this.gcAmbiente.MainView = this.gvAmbiente;
            this.gcAmbiente.Name = "gcAmbiente";
            this.gcAmbiente.Size = new System.Drawing.Size(596, 130);
            this.gcAmbiente.TabIndex = 7;
            this.gcAmbiente.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAmbiente});
            // 
            // gvAmbiente
            // 
            this.gvAmbiente.GridControl = this.gcAmbiente;
            this.gvAmbiente.Name = "gvAmbiente";
            // 
            // beIdentificador
            // 
            this.beIdentificador.Location = new System.Drawing.Point(44, 78);
            this.beIdentificador.Name = "beIdentificador";
            this.beIdentificador.Properties.MaxLength = 10;
            this.beIdentificador.Size = new System.Drawing.Size(121, 20);
            this.beIdentificador.TabIndex = 5;
            // 
            // teNM
            // 
            this.teNM.Location = new System.Drawing.Point(169, 78);
            this.teNM.Name = "teNM";
            this.teNM.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.teNM.Properties.MaxLength = 256;
            this.teNM.Size = new System.Drawing.Size(471, 20);
            this.teNM.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.Root});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 318);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.item0});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "item0";
            this.Root.Size = new System.Drawing.Size(664, 298);
            this.Root.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.TextVisible = false;
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
            this.item0.Name = "item1";
            this.item0.Size = new System.Drawing.Size(644, 278);
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
            this.layoutControlGroup3.Size = new System.Drawing.Size(624, 82);
            this.layoutControlGroup3.Text = "Filtros de busca";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.beIdentificador;
            this.layoutControlItem2.CustomizationFormText = "Identificador do produto";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(125, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(125, 1);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Identificador do ambiente";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(123, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNM;
            this.layoutControlItem3.CustomizationFormText = "Nome do produto";
            this.layoutControlItem3.Location = new System.Drawing.Point(125, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(475, 40);
            this.layoutControlItem3.Text = "Nome do ambiente";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(123, 13);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "Produtos cadastrados";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 82);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(624, 176);
            this.layoutControlGroup4.Text = "Ambientes cadastrados";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAmbiente;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(600, 134);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FAmbiente_Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(684, 358);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FAmbiente_Busca";
            this.Text = "Busca de ambiente";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAmbiente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAmbiente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beIdentificador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcAmbiente;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAmbiente;
        private DevExpress.XtraEditors.ButtonEdit beIdentificador;
        private DevExpress.XtraEditors.TextEdit teNM;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup item0;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
