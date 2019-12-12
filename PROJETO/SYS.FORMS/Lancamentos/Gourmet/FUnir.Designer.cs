namespace SYS.FORMS.Lancamentos.Gourmet
{
    partial class FUnir
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcCartao = new DevExpress.XtraGrid.GridControl();
            this.bsCartoes = new System.Windows.Forms.BindingSource();
            this.gvCartao = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_CARTAO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVALOR_PEDIDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.teNumero = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCartao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCartoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCartao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNumero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcCartao);
            this.layoutControl1.Controls.Add(this.teNumero);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(461, 240);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcCartao
            // 
            this.gcCartao.DataSource = this.bsCartoes;
            gridLevelNode2.RelationName = "Level1";
            this.gcCartao.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gcCartao.Location = new System.Drawing.Point(12, 58);
            this.gcCartao.MainView = this.gvCartao;
            this.gcCartao.Name = "gcCartao";
            this.gcCartao.Size = new System.Drawing.Size(437, 170);
            this.gcCartao.TabIndex = 6;
            this.gcCartao.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCartao});
            // 
            // bsCartoes
            // 
            this.bsCartoes.DataSource = typeof(SYS.QUERYS.Lancamentos.Gourmet.MPedido);
            // 
            // gvCartao
            // 
            this.gvCartao.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_CARTAO,
            this.colVALOR_PEDIDO});
            this.gvCartao.GridControl = this.gcCartao;
            this.gvCartao.Name = "gvCartao";
            this.gvCartao.OptionsView.ShowGroupPanel = false;
            // 
            // colID_CARTAO
            // 
            this.colID_CARTAO.Caption = "Cartão";
            this.colID_CARTAO.FieldName = "ID_CARTAO";
            this.colID_CARTAO.Name = "colID_CARTAO";
            this.colID_CARTAO.OptionsColumn.AllowEdit = false;
            this.colID_CARTAO.OptionsColumn.AllowFocus = false;
            this.colID_CARTAO.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colID_CARTAO.OptionsColumn.AllowMove = false;
            this.colID_CARTAO.OptionsColumn.AllowSize = false;
            this.colID_CARTAO.OptionsColumn.ReadOnly = true;
            this.colID_CARTAO.Visible = true;
            this.colID_CARTAO.VisibleIndex = 1;
            // 
            // colVALOR_PEDIDO
            // 
            this.colVALOR_PEDIDO.Caption = "Valor";
            this.colVALOR_PEDIDO.FieldName = "VALOR_PEDIDO";
            this.colVALOR_PEDIDO.Name = "colVALOR_PEDIDO";
            this.colVALOR_PEDIDO.OptionsColumn.AllowEdit = false;
            this.colVALOR_PEDIDO.OptionsColumn.AllowFocus = false;
            this.colVALOR_PEDIDO.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colVALOR_PEDIDO.OptionsColumn.AllowMove = false;
            this.colVALOR_PEDIDO.OptionsColumn.AllowSize = false;
            this.colVALOR_PEDIDO.OptionsColumn.ReadOnly = true;
            this.colVALOR_PEDIDO.Visible = true;
            this.colVALOR_PEDIDO.VisibleIndex = 0;
            // 
            // teNumero
            // 
            this.teNumero.Location = new System.Drawing.Point(172, 28);
            this.teNumero.Name = "teNumero";
            this.teNumero.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teNumero.Properties.Appearance.Options.UseFont = true;
            this.teNumero.Properties.Mask.EditMask = "0000";
            this.teNumero.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.teNumero.Size = new System.Drawing.Size(110, 26);
            this.teNumero.StyleController = this.layoutControl1;
            this.teNumero.TabIndex = 4;
            this.teNumero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teNumero_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(461, 240);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teNumero;
            this.layoutControlItem1.Location = new System.Drawing.Point(160, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(114, 46);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(114, 46);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(114, 46);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Número Cartão";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcCartao;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 46);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(441, 174);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(160, 46);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(160, 46);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(160, 46);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(274, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(63, 46);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(63, 46);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(167, 46);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FUnir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(461, 280);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FUnir";
            this.Text = "Unir Cartões";
            this.Load += new System.EventHandler(this.FUnir_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCartao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCartoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCartao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNumero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcCartao;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCartao;
        private DevExpress.XtraEditors.TextEdit teNumero;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private System.Windows.Forms.BindingSource bsCartoes;
        private DevExpress.XtraGrid.Columns.GridColumn colID_CARTAO;
        private DevExpress.XtraGrid.Columns.GridColumn colVALOR_PEDIDO;
    }
}
