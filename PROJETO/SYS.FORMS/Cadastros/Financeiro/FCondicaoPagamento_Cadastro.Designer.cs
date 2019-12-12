namespace SYS.FORMS.Cadastros.Financeiro
{
    partial class FCondicaoPagamento_Cadastro
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
            this.seQT_DIASDESDOBRO = new DevExpress.XtraEditors.SpinEdit();
            this.teDS = new DevExpress.XtraEditors.TextEdit();
            this.teID_CONDICAOPAGAMENTO = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seQT_DIASDESDOBRO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teID_CONDICAOPAGAMENTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.seQT_DIASDESDOBRO);
            this.layoutControl1.Controls.Add(this.teDS);
            this.layoutControl1.Controls.Add(this.teID_CONDICAOPAGAMENTO);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(449, 66);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // seQT_DIASDESDOBRO
            // 
            this.seQT_DIASDESDOBRO.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seQT_DIASDESDOBRO.Location = new System.Drawing.Point(331, 28);
            this.seQT_DIASDESDOBRO.Name = "seQT_DIASDESDOBRO";
            this.seQT_DIASDESDOBRO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seQT_DIASDESDOBRO.Properties.Mask.EditMask = "d";
            this.seQT_DIASDESDOBRO.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.seQT_DIASDESDOBRO.Size = new System.Drawing.Size(106, 20);
            this.seQT_DIASDESDOBRO.StyleController = this.layoutControl1;
            this.seQT_DIASDESDOBRO.TabIndex = 6;
            // 
            // teDS
            // 
            this.teDS.Location = new System.Drawing.Point(99, 28);
            this.teDS.Name = "teDS";
            this.teDS.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.teDS.Properties.MaxLength = 32;
            this.teDS.Size = new System.Drawing.Size(228, 20);
            this.teDS.StyleController = this.layoutControl1;
            this.teDS.TabIndex = 5;
            // 
            // teID_CONDICAOPAGAMENTO
            // 
            this.teID_CONDICAOPAGAMENTO.Location = new System.Drawing.Point(12, 28);
            this.teID_CONDICAOPAGAMENTO.Name = "teID_CONDICAOPAGAMENTO";
            this.teID_CONDICAOPAGAMENTO.Properties.Mask.EditMask = "[0-9]+";
            this.teID_CONDICAOPAGAMENTO.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.teID_CONDICAOPAGAMENTO.Properties.MaxLength = 10;
            this.teID_CONDICAOPAGAMENTO.Size = new System.Drawing.Size(83, 20);
            this.teID_CONDICAOPAGAMENTO.StyleController = this.layoutControl1;
            this.teID_CONDICAOPAGAMENTO.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(449, 66);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teID_CONDICAOPAGAMENTO;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(87, 40);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(87, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(87, 46);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Identificador";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.seQT_DIASDESDOBRO;
            this.layoutControlItem3.Location = new System.Drawing.Point(319, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(110, 40);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(110, 40);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(110, 46);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Dias de desdobro";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teDS;
            this.layoutControlItem2.Location = new System.Drawing.Point(87, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(232, 46);
            this.layoutControlItem2.Text = "Descrição";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(83, 13);
            // 
            // FCondicaoPagamento_Cadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(449, 106);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FCondicaoPagamento_Cadastro";
            this.Text = "Cadastro de condição de pagamento";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seQT_DIASDESDOBRO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teID_CONDICAOPAGAMENTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SpinEdit seQT_DIASDESDOBRO;
        private DevExpress.XtraEditors.TextEdit teDS;
        private DevExpress.XtraEditors.TextEdit teID_CONDICAOPAGAMENTO;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
