namespace SYS.FORMS.Lancamentos.Comercial
{
    partial class FPDV_DescontoAcrescimo
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
            this.seVL = new DevExpress.XtraEditors.SpinEdit();
            this.seVL_MAXIMO = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sePC_DESCONTO = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seVL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seVL_MAXIMO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sePC_DESCONTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sePC_DESCONTO);
            this.layoutControl1.Controls.Add(this.seVL);
            this.layoutControl1.Controls.Add(this.seVL_MAXIMO);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(226, 176);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // seVL
            // 
            this.seVL.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seVL.Location = new System.Drawing.Point(12, 138);
            this.seVL.Name = "seVL";
            this.seVL.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.seVL.Properties.Appearance.Options.UseFont = true;
            this.seVL.Size = new System.Drawing.Size(202, 26);
            this.seVL.StyleController = this.layoutControl1;
            this.seVL.TabIndex = 6;
            // 
            // seVL_MAXIMO
            // 
            this.seVL_MAXIMO.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seVL_MAXIMO.Location = new System.Drawing.Point(12, 34);
            this.seVL_MAXIMO.Name = "seVL_MAXIMO";
            this.seVL_MAXIMO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.seVL_MAXIMO.Properties.Appearance.Options.UseFont = true;
            this.seVL_MAXIMO.Size = new System.Drawing.Size(202, 26);
            this.seVL_MAXIMO.StyleController = this.layoutControl1;
            this.seVL_MAXIMO.TabIndex = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.Header.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.PageClient.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(226, 176);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.seVL_MAXIMO;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(206, 52);
            this.layoutControlItem2.Text = "Valor máximo";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(99, 19);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.seVL;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(206, 52);
            this.layoutControlItem3.Text = "Valor";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(99, 19);
            // 
            // sePC_DESCONTO
            // 
            this.sePC_DESCONTO.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sePC_DESCONTO.Location = new System.Drawing.Point(12, 86);
            this.sePC_DESCONTO.Name = "sePC_DESCONTO";
            this.sePC_DESCONTO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.sePC_DESCONTO.Properties.Appearance.Options.UseFont = true;
            this.sePC_DESCONTO.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.sePC_DESCONTO.Size = new System.Drawing.Size(202, 26);
            this.sePC_DESCONTO.StyleController = this.layoutControl1;
            this.sePC_DESCONTO.TabIndex = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sePC_DESCONTO;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(206, 52);
            this.layoutControlItem1.Text = "Porcentagem";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(99, 19);
            // 
            // FPDV_DescontoAcrescimo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(226, 216);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FPDV_DescontoAcrescimo";
            this.Text = "Desconto/Acréscimo";
            this.Shown += new System.EventHandler(this.FPDV_DescontoAcrescimo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seVL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seVL_MAXIMO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sePC_DESCONTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public DevExpress.XtraEditors.SpinEdit seVL_MAXIMO;
        public DevExpress.XtraEditors.SpinEdit seVL;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraEditors.SpinEdit sePC_DESCONTO;
    }
}
