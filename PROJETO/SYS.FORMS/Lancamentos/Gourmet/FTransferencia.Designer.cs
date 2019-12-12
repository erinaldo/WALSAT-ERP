namespace SYS.FORMS.Lancamentos.Gourmet
{
    partial class FTransferencia
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbTransferir = new DevExpress.XtraEditors.SimpleButton();
            this.teMesa = new DevExpress.XtraEditors.TextEdit();
            this.teCartao = new DevExpress.XtraEditors.TextEdit();
            this.gcItens = new DevExpress.XtraGrid.GridControl();
            this.bsItens = new System.Windows.Forms.BindingSource(this.components);
            this.gvItens = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID_PRODUTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNM_PRODUTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQUANTIDADE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVL_UNITARIO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTALCOMPLEMENTOS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVL_SUBTOTAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.seSubtotal = new DevExpress.XtraEditors.SpinEdit();
            this.beAtual = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teMesa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCartao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSubtotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beAtual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbTransferir);
            this.layoutControl1.Controls.Add(this.teMesa);
            this.layoutControl1.Controls.Add(this.teCartao);
            this.layoutControl1.Controls.Add(this.gcItens);
            this.layoutControl1.Controls.Add(this.seSubtotal);
            this.layoutControl1.Controls.Add(this.beAtual);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 285);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbTransferir
            // 
            this.sbTransferir.Image = global::SYS.FORMS.Properties.Resources.refresh_update_x32;
            this.sbTransferir.Location = new System.Drawing.Point(549, 12);
            this.sbTransferir.Name = "sbTransferir";
            this.sbTransferir.Size = new System.Drawing.Size(123, 38);
            this.sbTransferir.StyleController = this.layoutControl1;
            this.sbTransferir.TabIndex = 9;
            this.sbTransferir.Text = "Transferir";
            this.sbTransferir.Click += new System.EventHandler(this.bt_transferir_Click);
            // 
            // teMesa
            // 
            this.teMesa.Location = new System.Drawing.Point(447, 28);
            this.teMesa.Name = "teMesa";
            this.teMesa.Size = new System.Drawing.Size(98, 20);
            this.teMesa.StyleController = this.layoutControl1;
            this.teMesa.TabIndex = 8;
            this.teMesa.TextChanged += new System.EventHandler(this.txt_mesa_TextChanged);
            this.teMesa.Leave += new System.EventHandler(this.txt_mesa_Leave);
            // 
            // teCartao
            // 
            this.teCartao.Location = new System.Drawing.Point(345, 28);
            this.teCartao.Name = "teCartao";
            this.teCartao.Size = new System.Drawing.Size(98, 20);
            this.teCartao.StyleController = this.layoutControl1;
            this.teCartao.TabIndex = 7;
            this.teCartao.TextChanged += new System.EventHandler(this.txt_cartao_TextChanged);
            this.teCartao.Leave += new System.EventHandler(this.txt_cartao_Leave);
            // 
            // gcItens
            // 
            this.gcItens.DataSource = this.bsItens;
            this.gcItens.Location = new System.Drawing.Point(12, 91);
            this.gcItens.MainView = this.gvItens;
            this.gcItens.Name = "gcItens";
            this.gcItens.Size = new System.Drawing.Size(660, 182);
            this.gcItens.TabIndex = 6;
            this.gcItens.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItens});
            // 
            // bsItens
            // 
            this.bsItens.DataSource = typeof(SYS.QUERYS.Lancamentos.Gourmet.MPedidoItem);
            // 
            // gvItens
            // 
            this.gvItens.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelect,
            this.colID_PRODUTO,
            this.colNM_PRODUTO,
            this.colQUANTIDADE,
            this.colVL_UNITARIO,
            this.colTOTALCOMPLEMENTOS,
            this.colVL_SUBTOTAL});
            this.gvItens.GridControl = this.gcItens;
            this.gvItens.Name = "gvItens";
            this.gvItens.OptionsView.ShowGroupPanel = false;
            // 
            // colSelect
            // 
            this.colSelect.Caption = "Selecionar";
            this.colSelect.FieldName = "Select";
            this.colSelect.Name = "colSelect";
            this.colSelect.Visible = true;
            this.colSelect.VisibleIndex = 0;
            this.colSelect.Width = 68;
            // 
            // colID_PRODUTO
            // 
            this.colID_PRODUTO.Caption = "Código";
            this.colID_PRODUTO.FieldName = "ID_PRODUTO";
            this.colID_PRODUTO.Name = "colID_PRODUTO";
            this.colID_PRODUTO.OptionsColumn.AllowEdit = false;
            this.colID_PRODUTO.OptionsColumn.AllowFocus = false;
            this.colID_PRODUTO.OptionsColumn.ReadOnly = true;
            this.colID_PRODUTO.Visible = true;
            this.colID_PRODUTO.VisibleIndex = 1;
            this.colID_PRODUTO.Width = 64;
            // 
            // colNM_PRODUTO
            // 
            this.colNM_PRODUTO.Caption = "Descrição";
            this.colNM_PRODUTO.FieldName = "NM_PRODUTO";
            this.colNM_PRODUTO.Name = "colNM_PRODUTO";
            this.colNM_PRODUTO.OptionsColumn.AllowEdit = false;
            this.colNM_PRODUTO.OptionsColumn.AllowFocus = false;
            this.colNM_PRODUTO.OptionsColumn.ReadOnly = true;
            this.colNM_PRODUTO.Visible = true;
            this.colNM_PRODUTO.VisibleIndex = 2;
            this.colNM_PRODUTO.Width = 150;
            // 
            // colQUANTIDADE
            // 
            this.colQUANTIDADE.Caption = "Quantidade";
            this.colQUANTIDADE.FieldName = "QUANTIDADE";
            this.colQUANTIDADE.Name = "colQUANTIDADE";
            this.colQUANTIDADE.OptionsColumn.AllowEdit = false;
            this.colQUANTIDADE.OptionsColumn.AllowFocus = false;
            this.colQUANTIDADE.OptionsColumn.ReadOnly = true;
            this.colQUANTIDADE.Visible = true;
            this.colQUANTIDADE.VisibleIndex = 3;
            this.colQUANTIDADE.Width = 97;
            // 
            // colVL_UNITARIO
            // 
            this.colVL_UNITARIO.Caption = "Valor";
            this.colVL_UNITARIO.FieldName = "VL_UNITARIO";
            this.colVL_UNITARIO.Name = "colVL_UNITARIO";
            this.colVL_UNITARIO.OptionsColumn.AllowEdit = false;
            this.colVL_UNITARIO.OptionsColumn.AllowFocus = false;
            this.colVL_UNITARIO.OptionsColumn.ReadOnly = true;
            this.colVL_UNITARIO.Visible = true;
            this.colVL_UNITARIO.VisibleIndex = 4;
            this.colVL_UNITARIO.Width = 82;
            // 
            // colTOTALCOMPLEMENTOS
            // 
            this.colTOTALCOMPLEMENTOS.Caption = "Valor Complementos";
            this.colTOTALCOMPLEMENTOS.FieldName = "TOTALCOMPLEMENTOS";
            this.colTOTALCOMPLEMENTOS.Name = "colTOTALCOMPLEMENTOS";
            this.colTOTALCOMPLEMENTOS.OptionsColumn.AllowEdit = false;
            this.colTOTALCOMPLEMENTOS.OptionsColumn.AllowFocus = false;
            this.colTOTALCOMPLEMENTOS.OptionsColumn.ReadOnly = true;
            this.colTOTALCOMPLEMENTOS.Visible = true;
            this.colTOTALCOMPLEMENTOS.VisibleIndex = 5;
            this.colTOTALCOMPLEMENTOS.Width = 108;
            // 
            // colVL_SUBTOTAL
            // 
            this.colVL_SUBTOTAL.Caption = "SubTotal";
            this.colVL_SUBTOTAL.FieldName = "VL_SUBTOTAL";
            this.colVL_SUBTOTAL.Name = "colVL_SUBTOTAL";
            this.colVL_SUBTOTAL.OptionsColumn.AllowEdit = false;
            this.colVL_SUBTOTAL.OptionsColumn.AllowFocus = false;
            this.colVL_SUBTOTAL.OptionsColumn.ReadOnly = true;
            this.colVL_SUBTOTAL.Visible = true;
            this.colVL_SUBTOTAL.VisibleIndex = 6;
            this.colVL_SUBTOTAL.Width = 73;
            // 
            // seSubtotal
            // 
            this.seSubtotal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seSubtotal.Location = new System.Drawing.Point(109, 28);
            this.seSubtotal.Name = "seSubtotal";
            this.seSubtotal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seSubtotal.Properties.ReadOnly = true;
            this.seSubtotal.Size = new System.Drawing.Size(93, 20);
            this.seSubtotal.StyleController = this.layoutControl1;
            this.seSubtotal.TabIndex = 5;
            // 
            // beAtual
            // 
            this.beAtual.Location = new System.Drawing.Point(12, 28);
            this.beAtual.Name = "beAtual";
            this.beAtual.Properties.ReadOnly = true;
            this.beAtual.Size = new System.Drawing.Size(93, 20);
            this.beAtual.StyleController = this.layoutControl1;
            this.beAtual.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 285);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 42);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(664, 37);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.beAtual;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(97, 40);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(97, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(97, 42);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Atual";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.seSubtotal;
            this.layoutControlItem2.Location = new System.Drawing.Point(97, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(97, 40);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(97, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(97, 42);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "SubTotal";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(42, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(194, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(139, 42);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcItens;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 79);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(664, 186);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teCartao;
            this.layoutControlItem4.Location = new System.Drawing.Point(333, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(102, 40);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(102, 40);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(102, 42);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Cartão";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teMesa;
            this.layoutControlItem5.Location = new System.Drawing.Point(435, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(102, 40);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(102, 40);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(102, 42);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Mesa";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbTransferir;
            this.layoutControlItem6.Location = new System.Drawing.Point(537, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(127, 42);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // FTransferencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(684, 285);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FTransferencia";
            this.Load += new System.EventHandler(this.Transferencia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teMesa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teCartao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSubtotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beAtual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton sbTransferir;
        private DevExpress.XtraEditors.TextEdit teMesa;
        private DevExpress.XtraEditors.TextEdit teCartao;
        private DevExpress.XtraGrid.GridControl gcItens;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItens;
        private DevExpress.XtraEditors.SpinEdit seSubtotal;
        private DevExpress.XtraEditors.TextEdit beAtual;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private System.Windows.Forms.BindingSource bsItens;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colID_PRODUTO;
        private DevExpress.XtraGrid.Columns.GridColumn colNM_PRODUTO;
        private DevExpress.XtraGrid.Columns.GridColumn colQUANTIDADE;
        private DevExpress.XtraGrid.Columns.GridColumn colVL_UNITARIO;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTALCOMPLEMENTOS;
        private DevExpress.XtraGrid.Columns.GridColumn colVL_SUBTOTAL;
    }
}
