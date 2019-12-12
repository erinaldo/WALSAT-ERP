namespace SYS.FORMS.Lancamentos.Gourmet
{
    partial class FCartoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCartoes));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.teUltimoAcesso = new DevExpress.XtraEditors.TextEdit();
            this.spTroco = new DevExpress.XtraEditors.SpinEdit();
            this.gcCartoes = new DevExpress.XtraGrid.GridControl();
            this.bsCartoes = new System.Windows.Forms.BindingSource(this.components);
            this.gvCartoes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID_CARTAO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVALOR_PEDIDO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDT_CADASTRO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTempoAberto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sbTransferir = new DevExpress.XtraEditors.SimpleButton();
            this.sbSelecionar = new DevExpress.XtraEditors.SimpleButton();
            this.teIDCartao = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.Lc_Troco = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teUltimoAcesso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTroco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCartoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCartoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCartoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIDCartao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_Troco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teUltimoAcesso);
            this.layoutControl1.Controls.Add(this.spTroco);
            this.layoutControl1.Controls.Add(this.gcCartoes);
            this.layoutControl1.Controls.Add(this.sbTransferir);
            this.layoutControl1.Controls.Add(this.sbSelecionar);
            this.layoutControl1.Controls.Add(this.teIDCartao);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 411);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teUltimoAcesso
            // 
            this.teUltimoAcesso.Location = new System.Drawing.Point(218, 69);
            this.teUltimoAcesso.Name = "teUltimoAcesso";
            this.teUltimoAcesso.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teUltimoAcesso.Properties.Appearance.Options.UseFont = true;
            this.teUltimoAcesso.Properties.ReadOnly = true;
            this.teUltimoAcesso.Size = new System.Drawing.Size(133, 26);
            this.teUltimoAcesso.StyleController = this.layoutControl1;
            this.teUltimoAcesso.TabIndex = 10;
            // 
            // spTroco
            // 
            this.spTroco.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spTroco.Location = new System.Drawing.Point(520, 31);
            this.spTroco.Name = "spTroco";
            this.spTroco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spTroco.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.spTroco.Properties.Appearance.Options.UseFont = true;
            this.spTroco.Properties.Appearance.Options.UseForeColor = true;
            this.spTroco.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spTroco.Properties.Mask.EditMask = "n2";
            this.spTroco.Properties.ReadOnly = true;
            this.spTroco.Size = new System.Drawing.Size(152, 48);
            this.spTroco.StyleController = this.layoutControl1;
            this.spTroco.TabIndex = 9;
            // 
            // gcCartoes
            // 
            this.gcCartoes.DataSource = this.bsCartoes;
            this.gcCartoes.Location = new System.Drawing.Point(12, 99);
            this.gcCartoes.MainView = this.gvCartoes;
            this.gcCartoes.Name = "gcCartoes";
            this.gcCartoes.Size = new System.Drawing.Size(660, 213);
            this.gcCartoes.TabIndex = 8;
            this.gcCartoes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCartoes});
            this.gcCartoes.Click += new System.EventHandler(this.gc_cartoes_Click);
            this.gcCartoes.DoubleClick += new System.EventHandler(this.gc_cartoes_DoubleClick);
            this.gcCartoes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_NrCartao_KeyDown);
            // 
            // bsCartoes
            // 
            this.bsCartoes.DataSource = typeof(SYS.QUERYS.Lancamentos.Gourmet.MPedido);
            // 
            // gvCartoes
            // 
            this.gvCartoes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID_CARTAO,
            this.colVALOR_PEDIDO,
            this.colDT_CADASTRO,
            this.colTempoAberto});
            this.gvCartoes.GridControl = this.gcCartoes;
            this.gvCartoes.Name = "gvCartoes";
            this.gvCartoes.OptionsView.ShowGroupPanel = false;
            // 
            // colID_CARTAO
            // 
            this.colID_CARTAO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colID_CARTAO.AppearanceCell.Options.UseFont = true;
            this.colID_CARTAO.Caption = "Cartão";
            this.colID_CARTAO.FieldName = "ID_CARTAO";
            this.colID_CARTAO.Name = "colID_CARTAO";
            this.colID_CARTAO.OptionsColumn.AllowEdit = false;
            this.colID_CARTAO.OptionsColumn.AllowFocus = false;
            this.colID_CARTAO.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colID_CARTAO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colID_CARTAO.OptionsColumn.AllowMove = false;
            this.colID_CARTAO.OptionsColumn.AllowSize = false;
            this.colID_CARTAO.OptionsColumn.ReadOnly = true;
            this.colID_CARTAO.Visible = true;
            this.colID_CARTAO.VisibleIndex = 0;
            this.colID_CARTAO.Width = 356;
            // 
            // colVALOR_PEDIDO
            // 
            this.colVALOR_PEDIDO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colVALOR_PEDIDO.AppearanceCell.Options.UseFont = true;
            this.colVALOR_PEDIDO.Caption = "Valor pedido";
            this.colVALOR_PEDIDO.FieldName = "VALOR_PEDIDO";
            this.colVALOR_PEDIDO.Name = "colVALOR_PEDIDO";
            this.colVALOR_PEDIDO.OptionsColumn.AllowEdit = false;
            this.colVALOR_PEDIDO.OptionsColumn.AllowFocus = false;
            this.colVALOR_PEDIDO.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colVALOR_PEDIDO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colVALOR_PEDIDO.OptionsColumn.AllowMove = false;
            this.colVALOR_PEDIDO.OptionsColumn.AllowSize = false;
            this.colVALOR_PEDIDO.OptionsColumn.ReadOnly = true;
            this.colVALOR_PEDIDO.Visible = true;
            this.colVALOR_PEDIDO.VisibleIndex = 1;
            this.colVALOR_PEDIDO.Width = 100;
            // 
            // colDT_CADASTRO
            // 
            this.colDT_CADASTRO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDT_CADASTRO.AppearanceCell.Options.UseFont = true;
            this.colDT_CADASTRO.Caption = "Data abertura";
            this.colDT_CADASTRO.FieldName = "DT_CADASTRO";
            this.colDT_CADASTRO.Name = "colDT_CADASTRO";
            this.colDT_CADASTRO.OptionsColumn.AllowEdit = false;
            this.colDT_CADASTRO.OptionsColumn.AllowFocus = false;
            this.colDT_CADASTRO.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDT_CADASTRO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDT_CADASTRO.OptionsColumn.AllowMove = false;
            this.colDT_CADASTRO.OptionsColumn.AllowSize = false;
            this.colDT_CADASTRO.OptionsColumn.ReadOnly = true;
            this.colDT_CADASTRO.Visible = true;
            this.colDT_CADASTRO.VisibleIndex = 2;
            this.colDT_CADASTRO.Width = 86;
            // 
            // colTempoAberto
            // 
            this.colTempoAberto.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTempoAberto.AppearanceCell.Options.UseFont = true;
            this.colTempoAberto.Caption = "Tempo em aberto";
            this.colTempoAberto.FieldName = "TempoAberto";
            this.colTempoAberto.Name = "colTempoAberto";
            this.colTempoAberto.OptionsColumn.AllowEdit = false;
            this.colTempoAberto.OptionsColumn.AllowFocus = false;
            this.colTempoAberto.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTempoAberto.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTempoAberto.OptionsColumn.AllowMove = false;
            this.colTempoAberto.OptionsColumn.AllowSize = false;
            this.colTempoAberto.OptionsColumn.ReadOnly = true;
            this.colTempoAberto.Visible = true;
            this.colTempoAberto.VisibleIndex = 3;
            this.colTempoAberto.Width = 100;
            // 
            // sbTransferir
            // 
            this.sbTransferir.Image = ((System.Drawing.Image)(resources.GetObject("sbTransferir.Image")));
            this.sbTransferir.Location = new System.Drawing.Point(124, 316);
            this.sbTransferir.Name = "sbTransferir";
            this.sbTransferir.Size = new System.Drawing.Size(109, 38);
            this.sbTransferir.StyleController = this.layoutControl1;
            this.sbTransferir.TabIndex = 7;
            this.sbTransferir.Text = "Transferir";
            this.sbTransferir.Click += new System.EventHandler(this.sbTransferir_Click);
            this.sbTransferir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_NrCartao_KeyDown);
            // 
            // sbSelecionar
            // 
            this.sbSelecionar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbSelecionar.Appearance.Options.UseFont = true;
            this.sbSelecionar.Image = ((System.Drawing.Image)(resources.GetObject("sbSelecionar.Image")));
            this.sbSelecionar.Location = new System.Drawing.Point(12, 316);
            this.sbSelecionar.Name = "sbSelecionar";
            this.sbSelecionar.Size = new System.Drawing.Size(108, 38);
            this.sbSelecionar.StyleController = this.layoutControl1;
            this.sbSelecionar.TabIndex = 5;
            this.sbSelecionar.Text = "Selecionar";
            this.sbSelecionar.Click += new System.EventHandler(this.sbSelecionar_Click);
            this.sbSelecionar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_NrCartao_KeyDown);
            // 
            // teIDCartao
            // 
            this.teIDCartao.Location = new System.Drawing.Point(12, 31);
            this.teIDCartao.Name = "teIDCartao";
            this.teIDCartao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teIDCartao.Properties.Appearance.Options.UseFont = true;
            this.teIDCartao.Properties.Mask.EditMask = "0000";
            this.teIDCartao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.teIDCartao.Properties.MaxLength = 4;
            this.teIDCartao.Size = new System.Drawing.Size(202, 64);
            this.teIDCartao.StyleController = this.layoutControl1;
            this.teIDCartao.TabIndex = 4;
            this.teIDCartao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_NrCartao_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.emptySpaceItem3,
            this.Lc_Troco,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 411);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teIDCartao;
            this.layoutControlItem1.Image = ((System.Drawing.Image)(resources.GetObject("layoutControlItem1.Image")));
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(206, 87);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(206, 87);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(206, 87);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Identificador do cartão";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(133, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbTransferir;
            this.layoutControlItem4.Location = new System.Drawing.Point(112, 304);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(113, 42);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(113, 42);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(552, 42);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcCartoes;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 87);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(664, 217);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbSelecionar;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 304);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(112, 42);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(112, 42);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(112, 42);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 346);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(664, 45);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Lc_Troco
            // 
            this.Lc_Troco.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lc_Troco.AppearanceItemCaption.Options.UseFont = true;
            this.Lc_Troco.Control = this.spTroco;
            this.Lc_Troco.Location = new System.Drawing.Point(508, 0);
            this.Lc_Troco.MaxSize = new System.Drawing.Size(156, 87);
            this.Lc_Troco.MinSize = new System.Drawing.Size(156, 87);
            this.Lc_Troco.Name = "Lc_Troco";
            this.Lc_Troco.Size = new System.Drawing.Size(156, 87);
            this.Lc_Troco.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.Lc_Troco.Text = "Valor do troco";
            this.Lc_Troco.TextLocation = DevExpress.Utils.Locations.Top;
            this.Lc_Troco.TextSize = new System.Drawing.Size(133, 16);
            this.Lc_Troco.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.teUltimoAcesso;
            this.layoutControlItem5.Location = new System.Drawing.Point(206, 38);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(137, 49);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(137, 49);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(137, 49);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Ultimo cartão acessado";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(133, 16);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(343, 0);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(165, 87);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(206, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(137, 38);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(137, 38);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(137, 38);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FCartoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FCartoes";
            this.Text = "Cartões";
            this.Load += new System.EventHandler(this.ListaCartoes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teUltimoAcesso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTroco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCartoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCartoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCartoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIDCartao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_Troco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sbSelecionar;
        private DevExpress.XtraEditors.TextEdit teIDCartao;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbTransferir;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gcCartoes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCartoes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.BindingSource bsCartoes;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SpinEdit spTroco;
        private DevExpress.XtraLayout.LayoutControlItem Lc_Troco;
        private DevExpress.XtraGrid.Columns.GridColumn colID_CARTAO;
        private DevExpress.XtraGrid.Columns.GridColumn colVALOR_PEDIDO;
        private DevExpress.XtraGrid.Columns.GridColumn colDT_CADASTRO;
        private DevExpress.XtraGrid.Columns.GridColumn colTempoAberto;
        private DevExpress.XtraEditors.TextEdit teUltimoAcesso;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}
