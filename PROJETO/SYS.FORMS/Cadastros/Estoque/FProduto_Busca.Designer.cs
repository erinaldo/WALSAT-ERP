namespace SYS.FORMS.Cadastros.Estoque
{
    partial class FProduto_Busca
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
            this.teID_BARRA_REFERENCIA = new DevExpress.XtraEditors.TextEdit();
            this.teNM_PRODUTO = new DevExpress.XtraEditors.TextEdit();
            this.beID_PRODUTO = new DevExpress.XtraEditors.ButtonEdit();
            this.gcProduto = new DevExpress.XtraGrid.GridControl();
            this.gvProduto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CD_BARRA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teID_BARRA_REFERENCIA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNM_PRODUTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beID_PRODUTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teID_BARRA_REFERENCIA);
            this.layoutControl1.Controls.Add(this.teNM_PRODUTO);
            this.layoutControl1.Controls.Add(this.beID_PRODUTO);
            this.layoutControl1.Controls.Add(this.gcProduto);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(742, 371);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teID_BARRA_REFERENCIA
            // 
            this.teID_BARRA_REFERENCIA.Location = new System.Drawing.Point(597, 58);
            this.teID_BARRA_REFERENCIA.Name = "teID_BARRA_REFERENCIA";
            this.teID_BARRA_REFERENCIA.Properties.MaxLength = 13;
            this.teID_BARRA_REFERENCIA.Size = new System.Drawing.Size(121, 20);
            this.teID_BARRA_REFERENCIA.StyleController = this.layoutControl1;
            this.teID_BARRA_REFERENCIA.TabIndex = 7;
            // 
            // teNM_PRODUTO
            // 
            this.teNM_PRODUTO.Location = new System.Drawing.Point(111, 58);
            this.teNM_PRODUTO.Name = "teNM_PRODUTO";
            this.teNM_PRODUTO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.teNM_PRODUTO.Properties.MaxLength = 256;
            this.teNM_PRODUTO.Size = new System.Drawing.Size(482, 20);
            this.teNM_PRODUTO.StyleController = this.layoutControl1;
            this.teNM_PRODUTO.TabIndex = 6;
            // 
            // beID_PRODUTO
            // 
            this.beID_PRODUTO.Location = new System.Drawing.Point(24, 58);
            this.beID_PRODUTO.Name = "beID_PRODUTO";
            this.beID_PRODUTO.Properties.MaxLength = 10;
            this.beID_PRODUTO.Size = new System.Drawing.Size(83, 20);
            this.beID_PRODUTO.StyleController = this.layoutControl1;
            this.beID_PRODUTO.TabIndex = 5;
            // 
            // gcProduto
            // 
            this.gcProduto.Location = new System.Drawing.Point(24, 124);
            this.gcProduto.MainView = this.gvProduto;
            this.gcProduto.Name = "gcProduto";
            this.gcProduto.Size = new System.Drawing.Size(694, 223);
            this.gcProduto.TabIndex = 4;
            this.gcProduto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProduto});
            // 
            // gvProduto
            // 
            this.gvProduto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.NM,
            this.CD_BARRA});
            this.gvProduto.GridControl = this.gcProduto;
            this.gvProduto.Name = "gvProduto";
            this.gvProduto.OptionsView.ColumnAutoWidth = false;
            this.gvProduto.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.ID, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // ID
            // 
            this.ID.Caption = "Id. do produto";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.AllowFocus = false;
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            this.ID.Width = 93;
            // 
            // NM
            // 
            this.NM.Caption = "Nome do produto";
            this.NM.FieldName = "NM";
            this.NM.Name = "NM";
            this.NM.OptionsColumn.AllowEdit = false;
            this.NM.OptionsColumn.AllowFocus = false;
            this.NM.Visible = true;
            this.NM.VisibleIndex = 1;
            this.NM.Width = 93;
            // 
            // CD_BARRA
            // 
            this.CD_BARRA.Caption = "Código de barras";
            this.CD_BARRA.FieldName = "CD_BARRA";
            this.CD_BARRA.Name = "CD_BARRA";
            this.CD_BARRA.OptionsColumn.AllowEdit = false;
            this.CD_BARRA.OptionsColumn.AllowFocus = false;
            this.CD_BARRA.Visible = true;
            this.CD_BARRA.VisibleIndex = 2;
            this.CD_BARRA.Width = 92;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(742, 371);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(722, 82);
            this.layoutControlGroup2.Text = "Filtros de busca";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.beID_PRODUTO;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(87, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(87, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(87, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Id. do produto";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNM_PRODUTO;
            this.layoutControlItem3.Location = new System.Drawing.Point(87, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(486, 40);
            this.layoutControlItem3.Text = "Nome do produto";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teID_BARRA_REFERENCIA;
            this.layoutControlItem4.Location = new System.Drawing.Point(573, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Código de barras";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 82);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(722, 269);
            this.layoutControlGroup3.Text = "Produtos cadastrados";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcProduto;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(698, 227);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FProduto_Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(742, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FProduto_Busca";
            this.Text = "Busca de produto";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teID_BARRA_REFERENCIA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNM_PRODUTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beID_PRODUTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcProduto;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProduto;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.ButtonEdit beID_PRODUTO;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit teNM_PRODUTO;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit teID_BARRA_REFERENCIA;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn NM;
        private DevExpress.XtraGrid.Columns.GridColumn CD_BARRA;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
    }
}
