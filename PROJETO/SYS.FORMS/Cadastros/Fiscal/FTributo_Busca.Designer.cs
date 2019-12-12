namespace SYS.FORMS.Cadastros.Fiscal
{
    partial class FTributo_Busca
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
            this.ceContribuicao = new DevExpress.XtraEditors.CheckEdit();
            this.ceTaxa = new DevExpress.XtraEditors.CheckEdit();
            this.ceImposto = new DevExpress.XtraEditors.CheckEdit();
            this.teNomeTributo = new DevExpress.XtraEditors.TextEdit();
            this.gcTributo = new DevExpress.XtraGrid.GridControl();
            this.gvTributo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIMPOSTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTAXA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCONTRIBUICAO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.teIdentificador = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceContribuicao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTaxa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceImposto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNomeTributo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTributo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTributo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIdentificador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceContribuicao);
            this.layoutControl1.Controls.Add(this.ceTaxa);
            this.layoutControl1.Controls.Add(this.ceImposto);
            this.layoutControl1.Controls.Add(this.teNomeTributo);
            this.layoutControl1.Controls.Add(this.gcTributo);
            this.layoutControl1.Controls.Add(this.teIdentificador);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 42);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 369);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceContribuicao
            // 
            this.ceContribuicao.EditValue = true;
            this.ceContribuicao.Location = new System.Drawing.Point(578, 60);
            this.ceContribuicao.Name = "ceContribuicao";
            this.ceContribuicao.Properties.AutoWidth = true;
            this.ceContribuicao.Properties.Caption = "Contribuição";
            this.ceContribuicao.Size = new System.Drawing.Size(82, 19);
            this.ceContribuicao.StyleController = this.layoutControl1;
            this.ceContribuicao.TabIndex = 9;
            // 
            // ceTaxa
            // 
            this.ceTaxa.EditValue = true;
            this.ceTaxa.Location = new System.Drawing.Point(528, 60);
            this.ceTaxa.Name = "ceTaxa";
            this.ceTaxa.Properties.AutoWidth = true;
            this.ceTaxa.Properties.Caption = "Taxa";
            this.ceTaxa.Size = new System.Drawing.Size(46, 19);
            this.ceTaxa.StyleController = this.layoutControl1;
            this.ceTaxa.TabIndex = 8;
            // 
            // ceImposto
            // 
            this.ceImposto.EditValue = true;
            this.ceImposto.Location = new System.Drawing.Point(463, 60);
            this.ceImposto.Name = "ceImposto";
            this.ceImposto.Properties.AutoWidth = true;
            this.ceImposto.Properties.Caption = "Imposto";
            this.ceImposto.Size = new System.Drawing.Size(61, 19);
            this.ceImposto.StyleController = this.layoutControl1;
            this.ceImposto.TabIndex = 7;
            // 
            // teNomeTributo
            // 
            this.teNomeTributo.Location = new System.Drawing.Point(149, 59);
            this.teNomeTributo.Name = "teNomeTributo";
            this.teNomeTributo.Size = new System.Drawing.Size(310, 20);
            this.teNomeTributo.StyleController = this.layoutControl1;
            this.teNomeTributo.TabIndex = 6;
            // 
            // gcTributo
            // 
            this.gcTributo.Location = new System.Drawing.Point(24, 126);
            this.gcTributo.MainView = this.gvTributo;
            this.gcTributo.Name = "gcTributo";
            this.gcTributo.Size = new System.Drawing.Size(636, 219);
            this.gcTributo.TabIndex = 5;
            this.gcTributo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTributo});
            // 
            // gvTributo
            // 
            this.gvTributo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colNM,
            this.colIMPOSTO,
            this.colTAXA,
            this.colCONTRIBUICAO});
            this.gvTributo.GridControl = this.gcTributo;
            this.gvTributo.Name = "gvTributo";
            this.gvTributo.OptionsView.ColumnAutoWidth = false;
            // 
            // colID
            // 
            this.colID.Caption = "Identificador do tributo";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.AllowFocus = false;
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 121;
            // 
            // colNM
            // 
            this.colNM.Caption = "Nome do tributo";
            this.colNM.FieldName = "NM";
            this.colNM.Name = "colNM";
            this.colNM.OptionsColumn.AllowEdit = false;
            this.colNM.OptionsColumn.AllowFocus = false;
            this.colNM.Visible = true;
            this.colNM.VisibleIndex = 1;
            this.colNM.Width = 87;
            // 
            // colIMPOSTO
            // 
            this.colIMPOSTO.Caption = "Imposto";
            this.colIMPOSTO.FieldName = "IMPOSTO";
            this.colIMPOSTO.Name = "colIMPOSTO";
            this.colIMPOSTO.OptionsColumn.AllowEdit = false;
            this.colIMPOSTO.OptionsColumn.AllowFocus = false;
            this.colIMPOSTO.Visible = true;
            this.colIMPOSTO.VisibleIndex = 2;
            // 
            // colTAXA
            // 
            this.colTAXA.Caption = "Taxa";
            this.colTAXA.FieldName = "TAXA";
            this.colTAXA.Name = "colTAXA";
            this.colTAXA.OptionsColumn.AllowEdit = false;
            this.colTAXA.OptionsColumn.AllowFocus = false;
            this.colTAXA.Visible = true;
            this.colTAXA.VisibleIndex = 3;
            // 
            // colCONTRIBUICAO
            // 
            this.colCONTRIBUICAO.Caption = "Contribuição";
            this.colCONTRIBUICAO.FieldName = "CONTRIBUICAO";
            this.colCONTRIBUICAO.Name = "colCONTRIBUICAO";
            this.colCONTRIBUICAO.OptionsColumn.AllowEdit = false;
            this.colCONTRIBUICAO.OptionsColumn.AllowFocus = false;
            this.colCONTRIBUICAO.Visible = true;
            this.colCONTRIBUICAO.VisibleIndex = 4;
            // 
            // teIdentificador
            // 
            this.teIdentificador.Location = new System.Drawing.Point(24, 59);
            this.teIdentificador.Name = "teIdentificador";
            this.teIdentificador.Size = new System.Drawing.Size(121, 20);
            this.teIdentificador.StyleController = this.layoutControl1;
            this.teIdentificador.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 369);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(664, 83);
            this.layoutControlGroup2.Text = "Filtros de busca";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teIdentificador;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Identificador do tributo";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(111, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNomeTributo;
            this.layoutControlItem3.Location = new System.Drawing.Point(125, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(314, 40);
            this.layoutControlItem3.Text = "Nome do tributo";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(111, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ceImposto;
            this.layoutControlItem4.Location = new System.Drawing.Point(439, 17);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(65, 23);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(65, 23);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(65, 23);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(439, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(201, 17);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ceTaxa;
            this.layoutControlItem5.Location = new System.Drawing.Point(504, 17);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(50, 23);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(50, 23);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(50, 23);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ceContribuicao;
            this.layoutControlItem6.Location = new System.Drawing.Point(554, 17);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(86, 23);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(86, 23);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(86, 23);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 83);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(664, 266);
            this.layoutControlGroup3.Text = "Tributos cadastrados";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcTributo;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(640, 223);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // FTributo_Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FTributo_Busca";
            this.Text = "Busca de tributo";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceContribuicao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTaxa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceImposto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNomeTributo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTributo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTributo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIdentificador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcTributo;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTributo;
        private DevExpress.XtraEditors.TextEdit teIdentificador;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit teNomeTributo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit ceContribuicao;
        private DevExpress.XtraEditors.CheckEdit ceTaxa;
        private DevExpress.XtraEditors.CheckEdit ceImposto;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colNM;
        private DevExpress.XtraGrid.Columns.GridColumn colIMPOSTO;
        private DevExpress.XtraGrid.Columns.GridColumn colTAXA;
        private DevExpress.XtraGrid.Columns.GridColumn colCONTRIBUICAO;
    }
}
