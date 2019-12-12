namespace SYS.FORMS.Lancamentos.Estoque
{
    partial class FPreco_Lancamento
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.teID_PRECO = new DevExpress.XtraEditors.TextEdit();
            this.seVL_PRECO = new DevExpress.XtraEditors.SpinEdit();
            this.rgTP_PRECO = new DevExpress.XtraEditors.RadioGroup();
            this.teNM_PRODUTO = new DevExpress.XtraEditors.TextEdit();
            this.beID_PRODUTO = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teID_PRECO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seVL_PRECO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgTP_PRECO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNM_PRODUTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beID_PRODUTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.teID_PRECO);
            this.layoutControl1.Controls.Add(this.seVL_PRECO);
            this.layoutControl1.Controls.Add(this.rgTP_PRECO);
            this.layoutControl1.Controls.Add(this.teNM_PRODUTO);
            this.layoutControl1.Controls.Add(this.beID_PRODUTO);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(408, 145);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // teID_PRECO
            // 
            this.teID_PRECO.Location = new System.Drawing.Point(12, 28);
            this.teID_PRECO.Name = "teID_PRECO";
            this.teID_PRECO.Properties.ReadOnly = true;
            this.teID_PRECO.Size = new System.Drawing.Size(117, 20);
            this.teID_PRECO.StyleController = this.layoutControl1;
            this.teID_PRECO.TabIndex = 11;
            // 
            // seVL_PRECO
            // 
            this.seVL_PRECO.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seVL_PRECO.Location = new System.Drawing.Point(12, 108);
            this.seVL_PRECO.Name = "seVL_PRECO";
            this.seVL_PRECO.Properties.AutoHeight = false;
            this.seVL_PRECO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seVL_PRECO.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.seVL_PRECO.Size = new System.Drawing.Size(117, 25);
            this.seVL_PRECO.StyleController = this.layoutControl1;
            this.seVL_PRECO.TabIndex = 10;
            // 
            // rgTP_PRECO
            // 
            this.rgTP_PRECO.Location = new System.Drawing.Point(133, 108);
            this.rgTP_PRECO.Name = "rgTP_PRECO";
            this.rgTP_PRECO.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Venda"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Custo")});
            this.rgTP_PRECO.Size = new System.Drawing.Size(263, 25);
            this.rgTP_PRECO.StyleController = this.layoutControl1;
            this.rgTP_PRECO.TabIndex = 9;
            // 
            // teNM_PRODUTO
            // 
            this.teNM_PRODUTO.Location = new System.Drawing.Point(133, 68);
            this.teNM_PRODUTO.Name = "teNM_PRODUTO";
            this.teNM_PRODUTO.Properties.ReadOnly = true;
            this.teNM_PRODUTO.Size = new System.Drawing.Size(263, 20);
            this.teNM_PRODUTO.StyleController = this.layoutControl1;
            this.teNM_PRODUTO.TabIndex = 8;
            // 
            // beID_PRODUTO
            // 
            this.beID_PRODUTO.Location = new System.Drawing.Point(12, 68);
            this.beID_PRODUTO.Name = "beID_PRODUTO";
            this.beID_PRODUTO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SYS.FORMS.Properties.Resources.find_x12, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.beID_PRODUTO.Properties.MaxLength = 10;
            this.beID_PRODUTO.Size = new System.Drawing.Size(117, 20);
            this.beID_PRODUTO.StyleController = this.layoutControl1;
            this.beID_PRODUTO.TabIndex = 7;
            this.beID_PRODUTO.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beID_PRODUTO_ButtonClick);
            this.beID_PRODUTO.Leave += new System.EventHandler(this.beID_PRODUTO_Leave);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(408, 145);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.beID_PRODUTO;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(121, 40);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(121, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(121, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Id. do produto";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teNM_PRODUTO;
            this.layoutControlItem3.Location = new System.Drawing.Point(121, 40);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(267, 40);
            this.layoutControlItem3.Text = "Nome do produto";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.rgTP_PRECO;
            this.layoutControlItem4.Location = new System.Drawing.Point(121, 80);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(267, 45);
            this.layoutControlItem4.Text = "Tipo do preço";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.seVL_PRECO;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(121, 45);
            this.layoutControlItem5.Text = "Valor";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teID_PRECO;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(121, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(121, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(121, 40);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Id. do preço";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(83, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(121, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(267, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FPreco_Lancamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(408, 185);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FPreco_Lancamento";
            this.Text = "Lançamento de preço de produto";
            this.Shown += new System.EventHandler(this.FPreco_Lancamento_Shown);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teID_PRECO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seVL_PRECO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgTP_PRECO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNM_PRODUTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beID_PRODUTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.RadioGroup rgTP_PRECO;
        private DevExpress.XtraEditors.TextEdit teNM_PRODUTO;
        private DevExpress.XtraEditors.ButtonEdit beID_PRODUTO;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SpinEdit seVL_PRECO;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit teID_PRECO;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
