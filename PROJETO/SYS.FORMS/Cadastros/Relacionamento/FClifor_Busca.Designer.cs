namespace SYS.FORMS.Cadastros.Relacionamento
{
    partial class FClifor_Busca
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
            this.gcClifor = new DevExpress.XtraGrid.GridControl();
            this.gvClifor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CPF_CNPJ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TELEFONE_PRIMARIO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TELEFONE_SECUNDARIO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.teTelefone = new DevExpress.XtraEditors.TextEdit();
            this.teCNPJ = new DevExpress.XtraEditors.TextEdit();
            this.teCPF = new DevExpress.XtraEditors.TextEdit();
            this.teNomeClifor = new DevExpress.XtraEditors.TextEdit();
            this.teIdentificador = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcClifor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvClifor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTelefone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCNPJ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCPF.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNomeClifor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIdentificador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcClifor);
            this.layoutControl1.Controls.Add(this.teTelefone);
            this.layoutControl1.Controls.Add(this.teCNPJ);
            this.layoutControl1.Controls.Add(this.teCPF);
            this.layoutControl1.Controls.Add(this.teNomeClifor);
            this.layoutControl1.Controls.Add(this.teIdentificador);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 371);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcClifor
            // 
            this.gcClifor.Location = new System.Drawing.Point(24, 124);
            this.gcClifor.MainView = this.gvClifor;
            this.gcClifor.Name = "gcClifor";
            this.gcClifor.Size = new System.Drawing.Size(636, 223);
            this.gcClifor.TabIndex = 9;
            this.gcClifor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvClifor});
            // 
            // gvClifor
            // 
            this.gvClifor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.NM,
            this.CPF_CNPJ,
            this.TELEFONE_PRIMARIO,
            this.TELEFONE_SECUNDARIO,
            this.gridColumn6});
            this.gvClifor.GridControl = this.gcClifor;
            this.gvClifor.Name = "gvClifor";
            this.gvClifor.OptionsView.ColumnAutoWidth = false;
            // 
            // ID
            // 
            this.ID.Caption = "Identificador do clifor";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.AllowFocus = false;
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            this.ID.Width = 112;
            // 
            // NM
            // 
            this.NM.Caption = "Nome do clifor";
            this.NM.FieldName = "NM";
            this.NM.Name = "NM";
            this.NM.OptionsColumn.AllowEdit = false;
            this.NM.OptionsColumn.AllowFocus = false;
            this.NM.Visible = true;
            this.NM.VisibleIndex = 1;
            this.NM.Width = 78;
            // 
            // CPF_CNPJ
            // 
            this.CPF_CNPJ.Caption = "CPF / CNPJ";
            this.CPF_CNPJ.FieldName = "CPF_CNPJ";
            this.CPF_CNPJ.Name = "CPF_CNPJ";
            this.CPF_CNPJ.OptionsColumn.AllowEdit = false;
            this.CPF_CNPJ.OptionsColumn.AllowFocus = false;
            this.CPF_CNPJ.Visible = true;
            this.CPF_CNPJ.VisibleIndex = 2;
            // 
            // TELEFONE_PRIMARIO
            // 
            this.TELEFONE_PRIMARIO.Caption = "Telefone primário";
            this.TELEFONE_PRIMARIO.FieldName = "TELEFONE_PRIMARIO";
            this.TELEFONE_PRIMARIO.Name = "TELEFONE_PRIMARIO";
            this.TELEFONE_PRIMARIO.OptionsColumn.AllowEdit = false;
            this.TELEFONE_PRIMARIO.OptionsColumn.AllowFocus = false;
            this.TELEFONE_PRIMARIO.Visible = true;
            this.TELEFONE_PRIMARIO.VisibleIndex = 3;
            this.TELEFONE_PRIMARIO.Width = 93;
            // 
            // TELEFONE_SECUNDARIO
            // 
            this.TELEFONE_SECUNDARIO.Caption = "Telefone secundário";
            this.TELEFONE_SECUNDARIO.FieldName = "TELEFONE_SECUNDARIO";
            this.TELEFONE_SECUNDARIO.Name = "TELEFONE_SECUNDARIO";
            this.TELEFONE_SECUNDARIO.OptionsColumn.AllowEdit = false;
            this.TELEFONE_SECUNDARIO.OptionsColumn.AllowFocus = false;
            this.TELEFONE_SECUNDARIO.Visible = true;
            this.TELEFONE_SECUNDARIO.VisibleIndex = 4;
            this.TELEFONE_SECUNDARIO.Width = 107;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Status";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // teTelefone
            // 
            this.teTelefone.Location = new System.Drawing.Point(539, 58);
            this.teTelefone.Name = "teTelefone";
            this.teTelefone.Size = new System.Drawing.Size(121, 20);
            this.teTelefone.StyleController = this.layoutControl1;
            this.teTelefone.TabIndex = 8;
            // 
            // teCNPJ
            // 
            this.teCNPJ.Location = new System.Drawing.Point(414, 58);
            this.teCNPJ.Name = "teCNPJ";
            this.teCNPJ.Size = new System.Drawing.Size(121, 20);
            this.teCNPJ.StyleController = this.layoutControl1;
            this.teCNPJ.TabIndex = 7;
            // 
            // teCPF
            // 
            this.teCPF.Location = new System.Drawing.Point(289, 58);
            this.teCPF.Name = "teCPF";
            this.teCPF.Size = new System.Drawing.Size(121, 20);
            this.teCPF.StyleController = this.layoutControl1;
            this.teCPF.TabIndex = 6;
            // 
            // teNomeClifor
            // 
            this.teNomeClifor.Location = new System.Drawing.Point(149, 58);
            this.teNomeClifor.Name = "teNomeClifor";
            this.teNomeClifor.Size = new System.Drawing.Size(136, 20);
            this.teNomeClifor.StyleController = this.layoutControl1;
            this.teNomeClifor.TabIndex = 5;
            // 
            // teIdentificador
            // 
            this.teIdentificador.Location = new System.Drawing.Point(24, 58);
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
            this.layoutControlGroup3,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 371);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 82);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(664, 269);
            this.layoutControlGroup3.Text = "Clifors cadastrados";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcClifor;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(640, 227);
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(664, 82);
            this.layoutControlGroup2.Text = "Filtro de busca";
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
            this.layoutControlItem1.Text = "Identificador do clifor";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teNomeClifor;
            this.layoutControlItem2.Location = new System.Drawing.Point(125, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(140, 40);
            this.layoutControlItem2.Text = "Nome do clifor";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teCPF;
            this.layoutControlItem3.Location = new System.Drawing.Point(265, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "CPF";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teCNPJ;
            this.layoutControlItem4.Location = new System.Drawing.Point(390, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "CNPJ";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teTelefone;
            this.layoutControlItem5.Location = new System.Drawing.Point(515, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(125, 40);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(125, 40);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Telefone";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(102, 13);
            // 
            // FClifor_Busca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FClifor_Busca";
            this.Text = "Busca de clifor";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcClifor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvClifor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTelefone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCNPJ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCPF.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNomeClifor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIdentificador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.TextEdit teIdentificador;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit teNomeClifor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit teCPF;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit teCNPJ;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit teTelefone;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcClifor;
        private DevExpress.XtraGrid.Views.Grid.GridView gvClifor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn NM;
        private DevExpress.XtraGrid.Columns.GridColumn CPF_CNPJ;
        private DevExpress.XtraGrid.Columns.GridColumn TELEFONE_PRIMARIO;
        private DevExpress.XtraGrid.Columns.GridColumn TELEFONE_SECUNDARIO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;

    }
}
