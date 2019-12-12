namespace SYS.FORMS.Lancamentos.Gourmet
{
    partial class FConsulta_Cartao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FConsulta_Cartao));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceFechados = new DevExpress.XtraEditors.CheckEdit();
            this.deData = new DevExpress.XtraEditors.DateEdit();
            this.sbJuntar = new DevExpress.XtraEditors.SimpleButton();
            this.sbFechar = new DevExpress.XtraEditors.SimpleButton();
            this.teTroco = new DevExpress.XtraEditors.TextEdit();
            this.sbSomar = new DevExpress.XtraEditors.SimpleButton();
            this.spTotal = new DevExpress.XtraEditors.SpinEdit();
            this.teIDCartao = new DevExpress.XtraEditors.TextEdit();
            this.gcItens = new DevExpress.XtraGrid.GridControl();
            this.bsItens = new System.Windows.Forms.BindingSource(this.components);
            this.gvItens = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNM_PRODUTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQUANTIDADE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVL_UNITARIO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVL_SUBTOTAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.seTotalSoma = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceFechados.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deData.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTroco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIDCartao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTotalSoma.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceFechados);
            this.layoutControl1.Controls.Add(this.deData);
            this.layoutControl1.Controls.Add(this.sbJuntar);
            this.layoutControl1.Controls.Add(this.sbFechar);
            this.layoutControl1.Controls.Add(this.teTroco);
            this.layoutControl1.Controls.Add(this.sbSomar);
            this.layoutControl1.Controls.Add(this.spTotal);
            this.layoutControl1.Controls.Add(this.teIDCartao);
            this.layoutControl1.Controls.Add(this.gcItens);
            this.layoutControl1.Controls.Add(this.seTotalSoma);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(697, 506);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceFechados
            // 
            this.ceFechados.Location = new System.Drawing.Point(12, 12);
            this.ceFechados.Name = "ceFechados";
            this.ceFechados.Properties.Caption = "Fechados";
            this.ceFechados.Size = new System.Drawing.Size(146, 19);
            this.ceFechados.StyleController = this.layoutControl1;
            this.ceFechados.TabIndex = 12;
            this.ceFechados.CheckedChanged += new System.EventHandler(this.ceFechados_CheckedChanged);
            // 
            // deData
            // 
            this.deData.EditValue = null;
            this.deData.Enabled = false;
            this.deData.Location = new System.Drawing.Point(12, 51);
            this.deData.Name = "deData";
            this.deData.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deData.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deData.Size = new System.Drawing.Size(146, 20);
            this.deData.StyleController = this.layoutControl1;
            this.deData.TabIndex = 11;
            // 
            // sbJuntar
            // 
            this.sbJuntar.Image = global::SYS.FORMS.Properties.Resources.refresh_update_x16;
            this.sbJuntar.Location = new System.Drawing.Point(334, 427);
            this.sbJuntar.Name = "sbJuntar";
            this.sbJuntar.Size = new System.Drawing.Size(102, 54);
            this.sbJuntar.StyleController = this.layoutControl1;
            this.sbJuntar.TabIndex = 10;
            this.sbJuntar.Text = "Juntar Cartões";
            this.sbJuntar.Click += new System.EventHandler(this.sbJuntar_Click);
            // 
            // sbFechar
            // 
            this.sbFechar.Image = global::SYS.FORMS.Properties.Resources.cashier_banknote_x32;
            this.sbFechar.Location = new System.Drawing.Point(440, 427);
            this.sbFechar.Name = "sbFechar";
            this.sbFechar.Size = new System.Drawing.Size(79, 54);
            this.sbFechar.StyleController = this.layoutControl1;
            this.sbFechar.TabIndex = 9;
            this.sbFechar.Text = "Fechar";
            this.sbFechar.Click += new System.EventHandler(this.sbFechar_Click);
            // 
            // teTroco
            // 
            this.teTroco.Location = new System.Drawing.Point(539, 31);
            this.teTroco.Name = "teTroco";
            this.teTroco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teTroco.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.teTroco.Properties.Appearance.Options.UseFont = true;
            this.teTroco.Properties.Appearance.Options.UseForeColor = true;
            this.teTroco.Properties.ReadOnly = true;
            this.teTroco.Size = new System.Drawing.Size(146, 26);
            this.teTroco.StyleController = this.layoutControl1;
            this.teTroco.TabIndex = 8;
            // 
            // sbSomar
            // 
            this.sbSomar.Image = ((System.Drawing.Image)(resources.GetObject("sbSomar.Image")));
            this.sbSomar.Location = new System.Drawing.Point(162, 427);
            this.sbSomar.Name = "sbSomar";
            this.sbSomar.Size = new System.Drawing.Size(64, 54);
            this.sbSomar.StyleController = this.layoutControl1;
            this.sbSomar.TabIndex = 7;
            this.sbSomar.Text = "Somar";
            this.sbSomar.Click += new System.EventHandler(this.sbSomar_Click);
            // 
            // spTotal
            // 
            this.spTotal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spTotal.Location = new System.Drawing.Point(523, 446);
            this.spTotal.Name = "spTotal";
            this.spTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spTotal.Properties.Appearance.Options.UseFont = true;
            this.spTotal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spTotal.Properties.ReadOnly = true;
            this.spTotal.Size = new System.Drawing.Size(147, 26);
            this.spTotal.StyleController = this.layoutControl1;
            this.spTotal.TabIndex = 6;
            // 
            // teIDCartao
            // 
            this.teIDCartao.Location = new System.Drawing.Point(250, 34);
            this.teIDCartao.Name = "teIDCartao";
            this.teIDCartao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teIDCartao.Properties.Appearance.Options.UseFont = true;
            this.teIDCartao.Properties.Appearance.Options.UseTextOptions = true;
            this.teIDCartao.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.teIDCartao.Properties.Mask.EditMask = "0000";
            this.teIDCartao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.teIDCartao.Size = new System.Drawing.Size(181, 32);
            this.teIDCartao.StyleController = this.layoutControl1;
            this.teIDCartao.TabIndex = 5;
            this.teIDCartao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teIDCartao_KeyDown);
            this.teIDCartao.Leave += new System.EventHandler(this.teIDCartao_Leave);
            // 
            // gcItens
            // 
            this.gcItens.DataSource = this.bsItens;
            this.gcItens.Location = new System.Drawing.Point(12, 75);
            this.gcItens.MainView = this.gvItens;
            this.gcItens.Name = "gcItens";
            this.gcItens.Size = new System.Drawing.Size(658, 348);
            this.gcItens.TabIndex = 4;
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
            this.colNM_PRODUTO,
            this.colQUANTIDADE,
            this.colVL_UNITARIO,
            this.colVL_SUBTOTAL});
            this.gvItens.GridControl = this.gcItens;
            this.gvItens.Name = "gvItens";
            this.gvItens.OptionsView.ShowGroupPanel = false;
            // 
            // colNM_PRODUTO
            // 
            this.colNM_PRODUTO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNM_PRODUTO.AppearanceHeader.Options.UseFont = true;
            this.colNM_PRODUTO.Caption = "Produto";
            this.colNM_PRODUTO.FieldName = "NM_PRODUTO";
            this.colNM_PRODUTO.Name = "colNM_PRODUTO";
            this.colNM_PRODUTO.OptionsColumn.AllowEdit = false;
            this.colNM_PRODUTO.OptionsColumn.AllowFocus = false;
            this.colNM_PRODUTO.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colNM_PRODUTO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colNM_PRODUTO.OptionsColumn.AllowMove = false;
            this.colNM_PRODUTO.OptionsColumn.AllowSize = false;
            this.colNM_PRODUTO.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNM_PRODUTO.OptionsColumn.FixedWidth = true;
            this.colNM_PRODUTO.OptionsColumn.ReadOnly = true;
            this.colNM_PRODUTO.Visible = true;
            this.colNM_PRODUTO.VisibleIndex = 0;
            this.colNM_PRODUTO.Width = 313;
            // 
            // colQUANTIDADE
            // 
            this.colQUANTIDADE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQUANTIDADE.AppearanceHeader.Options.UseFont = true;
            this.colQUANTIDADE.Caption = "Qtd";
            this.colQUANTIDADE.FieldName = "QUANTIDADE";
            this.colQUANTIDADE.Name = "colQUANTIDADE";
            this.colQUANTIDADE.OptionsColumn.AllowEdit = false;
            this.colQUANTIDADE.OptionsColumn.AllowFocus = false;
            this.colQUANTIDADE.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colQUANTIDADE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colQUANTIDADE.OptionsColumn.AllowMove = false;
            this.colQUANTIDADE.OptionsColumn.AllowSize = false;
            this.colQUANTIDADE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colQUANTIDADE.OptionsColumn.FixedWidth = true;
            this.colQUANTIDADE.OptionsColumn.ReadOnly = true;
            this.colQUANTIDADE.Visible = true;
            this.colQUANTIDADE.VisibleIndex = 1;
            // 
            // colVL_UNITARIO
            // 
            this.colVL_UNITARIO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colVL_UNITARIO.AppearanceHeader.Options.UseFont = true;
            this.colVL_UNITARIO.Caption = "Unitário";
            this.colVL_UNITARIO.FieldName = "VL_UNITARIO";
            this.colVL_UNITARIO.Name = "colVL_UNITARIO";
            this.colVL_UNITARIO.OptionsColumn.AllowEdit = false;
            this.colVL_UNITARIO.OptionsColumn.AllowFocus = false;
            this.colVL_UNITARIO.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colVL_UNITARIO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colVL_UNITARIO.OptionsColumn.AllowMove = false;
            this.colVL_UNITARIO.OptionsColumn.AllowSize = false;
            this.colVL_UNITARIO.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colVL_UNITARIO.OptionsColumn.FixedWidth = true;
            this.colVL_UNITARIO.OptionsColumn.ReadOnly = true;
            this.colVL_UNITARIO.Visible = true;
            this.colVL_UNITARIO.VisibleIndex = 2;
            // 
            // colVL_SUBTOTAL
            // 
            this.colVL_SUBTOTAL.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colVL_SUBTOTAL.AppearanceHeader.Options.UseFont = true;
            this.colVL_SUBTOTAL.Caption = "SubTotal";
            this.colVL_SUBTOTAL.FieldName = "VL_SUBTOTAL";
            this.colVL_SUBTOTAL.Name = "colVL_SUBTOTAL";
            this.colVL_SUBTOTAL.OptionsColumn.AllowEdit = false;
            this.colVL_SUBTOTAL.OptionsColumn.AllowFocus = false;
            this.colVL_SUBTOTAL.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colVL_SUBTOTAL.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colVL_SUBTOTAL.OptionsColumn.AllowMove = false;
            this.colVL_SUBTOTAL.OptionsColumn.AllowSize = false;
            this.colVL_SUBTOTAL.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colVL_SUBTOTAL.OptionsColumn.FixedWidth = true;
            this.colVL_SUBTOTAL.OptionsColumn.ReadOnly = true;
            this.colVL_SUBTOTAL.Visible = true;
            this.colVL_SUBTOTAL.VisibleIndex = 3;
            // 
            // seTotalSoma
            // 
            this.seTotalSoma.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seTotalSoma.Location = new System.Drawing.Point(12, 446);
            this.seTotalSoma.Name = "seTotalSoma";
            this.seTotalSoma.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seTotalSoma.Properties.Appearance.Options.UseFont = true;
            this.seTotalSoma.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seTotalSoma.Properties.ReadOnly = true;
            this.seTotalSoma.Size = new System.Drawing.Size(146, 26);
            this.seTotalSoma.StyleController = this.layoutControl1;
            this.seTotalSoma.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(697, 506);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItens;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 63);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(662, 352);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(662, 352);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(677, 352);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem2.Control = this.teIDCartao;
            this.layoutControlItem2.Location = new System.Drawing.Point(238, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(185, 58);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(185, 58);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(185, 63);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Número do cartão";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(146, 19);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(218, 415);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(104, 58);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 58);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(104, 71);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.spTotal;
            this.layoutControlItem3.Location = new System.Drawing.Point(511, 415);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(151, 58);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(151, 58);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(166, 71);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Total";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(146, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.seTotalSoma;
            this.layoutControlItem4.CustomizationFormText = "Total";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 415);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(150, 58);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(150, 58);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(150, 71);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Total Soma";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(146, 16);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbSomar;
            this.layoutControlItem5.Location = new System.Drawing.Point(150, 415);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(68, 58);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(68, 58);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(68, 71);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.Control = this.teTroco;
            this.layoutControlItem6.Location = new System.Drawing.Point(527, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(150, 58);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(150, 58);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(150, 63);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "Troco";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(146, 16);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(423, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(104, 58);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 58);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(104, 63);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sbFechar;
            this.layoutControlItem7.Location = new System.Drawing.Point(428, 415);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(83, 58);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(83, 58);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(83, 71);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.sbJuntar;
            this.layoutControlItem8.Location = new System.Drawing.Point(322, 415);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(106, 58);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(106, 58);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(106, 71);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.deData;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(150, 40);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(150, 40);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(150, 40);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "Data";
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(146, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.ceFechados;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(150, 23);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(150, 23);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(150, 23);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(150, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(88, 63);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(88, 63);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(88, 63);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FConsulta_Cartao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(697, 506);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FConsulta_Cartao";
            this.Text = "Consulta de Cartões";
            this.Load += new System.EventHandler(this.FConsulta_Cartao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceFechados.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deData.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTroco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIDCartao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seTotalSoma.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SpinEdit spTotal;
        private DevExpress.XtraEditors.TextEdit teIDCartao;
        private DevExpress.XtraGrid.GridControl gcItens;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItens;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource bsItens;
        private DevExpress.XtraGrid.Columns.GridColumn colNM_PRODUTO;
        private DevExpress.XtraGrid.Columns.GridColumn colQUANTIDADE;
        private DevExpress.XtraGrid.Columns.GridColumn colVL_UNITARIO;
        private DevExpress.XtraGrid.Columns.GridColumn colVL_SUBTOTAL;
        private DevExpress.XtraEditors.SpinEdit seTotalSoma;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton sbSomar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit teTroco;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton sbFechar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton sbJuntar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.CheckEdit ceFechados;
        private DevExpress.XtraEditors.DateEdit deData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}
