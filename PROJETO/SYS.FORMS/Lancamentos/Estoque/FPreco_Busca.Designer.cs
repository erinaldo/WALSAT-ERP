namespace SYS.FORMS.Lancamentos.Estoque
{
    partial class FPreco_Busca
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
            this.gcPreco = new DevExpress.XtraGrid.GridControl();
            this.gvPreco = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.beID_PRODUTO = new DevExpress.XtraEditors.ButtonEdit();
            this.teNM_PRODUTO = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPreco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPreco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beID_PRODUTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNM_PRODUTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcPreco);
            this.layoutControl1.Controls.Add(this.beID_PRODUTO);
            this.layoutControl1.Controls.Add(this.teNM_PRODUTO);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(742, 371);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcPreco
            // 
            this.gcPreco.Location = new System.Drawing.Point(24, 124);
            this.gcPreco.MainView = this.gvPreco;
            this.gcPreco.Name = "gcPreco";
            this.gcPreco.Size = new System.Drawing.Size(694, 223);
            this.gcPreco.TabIndex = 7;
            this.gcPreco.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPreco});
            // 
            // gvPreco
            // 
            this.gvPreco.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvPreco.GridControl = this.gcPreco;
            this.gvPreco.Name = "gvPreco";
            this.gvPreco.OptionsView.ColumnAutoWidth = false;
            this.gvPreco.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Id. produto";
            this.gridColumn2.FieldName = "ID_PRODUTO";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 78;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Nome do produto";
            this.gridColumn3.FieldName = "NM_PRODUTO";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 93;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Valor";
            this.gridColumn4.FieldName = "VL_PRECO";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tipo";
            this.gridColumn5.FieldName = "TP_PRECO";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // beID_PRODUTO
            // 
            this.beID_PRODUTO.Location = new System.Drawing.Point(24, 58);
            this.beID_PRODUTO.Name = "beID_PRODUTO";
            this.beID_PRODUTO.Properties.MaxLength = 10;
            this.beID_PRODUTO.Size = new System.Drawing.Size(83, 20);
            this.beID_PRODUTO.TabIndex = 5;
            // 
            // teNM_PRODUTO
            // 
            this.teNM_PRODUTO.Location = new System.Drawing.Point(111, 58);
            this.teNM_PRODUTO.Name = "teNM_PRODUTO";
            this.teNM_PRODUTO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.teNM_PRODUTO.Properties.MaxLength = 256;
            this.teNM_PRODUTO.Size = new System.Drawing.Size(607, 20);
            this.teNM_PRODUTO.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(742, 371);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Filtros de busca";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(722, 82);
            this.layoutControlGroup3.Text = "Filtros de busca";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.beID_PRODUTO;
            this.layoutControlItem2.CustomizationFormText = "Identificador do produto";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(87, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(87, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(87, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Id. produto";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNM_PRODUTO;
            this.layoutControlItem3.CustomizationFormText = "Nome do produto";
            this.layoutControlItem3.Location = new System.Drawing.Point(87, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(611, 40);
            this.layoutControlItem3.Text = "Nome do produto";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "Produtos cadastrados";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 82);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(722, 269);
            this.layoutControlGroup4.Text = "Preços cadastrados";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcPreco;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(698, 227);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FPreco_Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(742, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FPreco_Busca";
            this.Text = "Busca de preços";
            this.Shown += new System.EventHandler(this.FPreco_Busca_Shown);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPreco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPreco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beID_PRODUTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNM_PRODUTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcPreco;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPreco;
        private DevExpress.XtraEditors.ButtonEdit beID_PRODUTO;
        private DevExpress.XtraEditors.TextEdit teNM_PRODUTO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
