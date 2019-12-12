namespace SYS.FORMS.Cadastros.Financeiro
{
    partial class FCondicaoPagamento_Busca
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
            this.gcCondicaoPagamento = new DevExpress.XtraGrid.GridControl();
            this.gvCondicaoPagamento = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.teDS = new DevExpress.XtraEditors.TextEdit();
            this.teID_CONDICAOPAGAMENTO = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCondicaoPagamento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCondicaoPagamento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teID_CONDICAOPAGAMENTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcCondicaoPagamento);
            this.layoutControl1.Controls.Add(this.teDS);
            this.layoutControl1.Controls.Add(this.teID_CONDICAOPAGAMENTO);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(755, 371);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcCondicaoPagamento
            // 
            this.gcCondicaoPagamento.Location = new System.Drawing.Point(24, 124);
            this.gcCondicaoPagamento.MainView = this.gvCondicaoPagamento;
            this.gcCondicaoPagamento.Name = "gcCondicaoPagamento";
            this.gcCondicaoPagamento.Size = new System.Drawing.Size(707, 223);
            this.gcCondicaoPagamento.TabIndex = 6;
            this.gcCondicaoPagamento.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCondicaoPagamento});
            // 
            // gvCondicaoPagamento
            // 
            this.gvCondicaoPagamento.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvCondicaoPagamento.GridControl = this.gcCondicaoPagamento;
            this.gvCondicaoPagamento.Name = "gvCondicaoPagamento";
            this.gvCondicaoPagamento.OptionsView.ColumnAutoWidth = false;
            this.gvCondicaoPagamento.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Identificador da condição de pagamento";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 216;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Descrição";
            this.gridColumn2.FieldName = "DS";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Quantidade de dias de desdobro";
            this.gridColumn3.FieldName = "QT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 166;
            // 
            // teDS
            // 
            this.teDS.Location = new System.Drawing.Point(221, 58);
            this.teDS.Name = "teDS";
            this.teDS.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.teDS.Size = new System.Drawing.Size(510, 20);
            this.teDS.StyleController = this.layoutControl1;
            this.teDS.TabIndex = 5;
            // 
            // teID_CONDICAOPAGAMENTO
            // 
            this.teID_CONDICAOPAGAMENTO.Location = new System.Drawing.Point(24, 58);
            this.teID_CONDICAOPAGAMENTO.Name = "teID_CONDICAOPAGAMENTO";
            this.teID_CONDICAOPAGAMENTO.Properties.Mask.EditMask = "[0-9]+";
            this.teID_CONDICAOPAGAMENTO.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.teID_CONDICAOPAGAMENTO.Properties.MaxLength = 10;
            this.teID_CONDICAOPAGAMENTO.Size = new System.Drawing.Size(193, 20);
            this.teID_CONDICAOPAGAMENTO.StyleController = this.layoutControl1;
            this.teID_CONDICAOPAGAMENTO.TabIndex = 4;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(755, 371);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(735, 82);
            this.layoutControlGroup2.Text = "Filtros de busca";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teID_CONDICAOPAGAMENTO;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(197, 40);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(197, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(197, 40);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Identificador da condição de pagamento";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(193, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teDS;
            this.layoutControlItem2.Location = new System.Drawing.Point(197, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(514, 40);
            this.layoutControlItem2.Text = "Descrição";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(193, 13);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 82);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(735, 269);
            this.layoutControlGroup3.Text = "Condições de pagamento cadastrados";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcCondicaoPagamento;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(711, 227);
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // FCondicaoPagamento_Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(755, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FCondicaoPagamento_Busca";
            this.Text = "Busca de condição de pagamento";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCondicaoPagamento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCondicaoPagamento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teID_CONDICAOPAGAMENTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.TextEdit teDS;
        private DevExpress.XtraEditors.TextEdit teID_CONDICAOPAGAMENTO;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraGrid.GridControl gcCondicaoPagamento;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCondicaoPagamento;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}
