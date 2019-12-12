namespace SYS.FORMS.Cadastros.Estoque
{
    partial class FGrupo_Busca
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
            this.gcGrupo = new DevExpress.XtraGrid.GridControl();
            this.gvGrupo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.beIdentificador = new DevExpress.XtraEditors.ButtonEdit();
            this.beNome = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beIdentificador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beNome.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcGrupo);
            this.layoutControl1.Controls.Add(this.beIdentificador);
            this.layoutControl1.Controls.Add(this.beNome);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 371);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcGrupo
            // 
            this.gcGrupo.Location = new System.Drawing.Point(34, 134);
            this.gcGrupo.MainView = this.gvGrupo;
            this.gcGrupo.Name = "gcGrupo";
            this.gcGrupo.Size = new System.Drawing.Size(616, 203);
            this.gcGrupo.TabIndex = 8;
            this.gcGrupo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGrupo});
            // 
            // gvGrupo
            // 
            this.gvGrupo.GridControl = this.gcGrupo;
            this.gvGrupo.Name = "gvGrupo";
            // 
            // beIdentificador
            // 
            this.beIdentificador.Location = new System.Drawing.Point(34, 68);
            this.beIdentificador.Name = "beIdentificador";
            this.beIdentificador.Properties.MaxLength = 10;
            this.beIdentificador.Size = new System.Drawing.Size(121, 20);
            this.beIdentificador.TabIndex = 5;
            // 
            // beNome
            // 
            this.beNome.Location = new System.Drawing.Point(159, 68);
            this.beNome.Name = "beNome";
            this.beNome.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.beNome.Properties.MaxLength = 256;
            this.beNome.Size = new System.Drawing.Size(491, 20);
            this.beNome.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.Root});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 371);
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
            this.Root.Name = "item0";
            this.Root.Size = new System.Drawing.Size(664, 351);
            this.Root.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.TextVisible = false;
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
            this.layoutControlItem2.Control = this.beIdentificador;
            this.layoutControlItem2.CustomizationFormText = "Identificador do produto";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(125, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(125, 1);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Identificador do grupo";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(107, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.beNome;
            this.layoutControlItem3.CustomizationFormText = "Nome do produto";
            this.layoutControlItem3.Location = new System.Drawing.Point(125, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(495, 40);
            this.layoutControlItem3.Text = "Nome do grupo";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(107, 13);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "Produtos cadastrados";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 82);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(644, 249);
            this.layoutControlGroup4.Text = "Grupos cadastrados";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcGrupo;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(620, 207);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FGrupo_Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FGrupo_Busca";
            this.Text = "Busca de Grupo";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beIdentificador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beNome.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcGrupo;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGrupo;
        private DevExpress.XtraEditors.ButtonEdit beIdentificador;
        private DevExpress.XtraEditors.TextEdit beNome;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
