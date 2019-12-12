namespace SYS.FORMS.Lancamentos.Comercial
{
    partial class FPDV_Contagem
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
            this.seVL_TOTAL = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seVL_TOTAL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.seVL_TOTAL);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(189, 69);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // seVL_TOTAL
            // 
            this.seVL_TOTAL.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seVL_TOTAL.Location = new System.Drawing.Point(12, 33);
            this.seVL_TOTAL.Name = "seVL_TOTAL";
            this.seVL_TOTAL.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.seVL_TOTAL.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seVL_TOTAL.Properties.Appearance.Options.UseBackColor = true;
            this.seVL_TOTAL.Properties.Appearance.Options.UseFont = true;
            this.seVL_TOTAL.Size = new System.Drawing.Size(165, 24);
            this.seVL_TOTAL.StyleController = this.layoutControl1;
            this.seVL_TOTAL.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.Header.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.layoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.layoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.layoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.layoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceTabPage.PageClient.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.layoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(189, 69);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.seVL_TOTAL;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(169, 49);
            this.layoutControlItem4.Text = "Valor total";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(65, 18);
            // 
            // FPDV_Contagem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(189, 109);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "FPDV_Contagem";
            this.Text = "Contagem de valores";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seVL_TOTAL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraEditors.SpinEdit seVL_TOTAL;
    }
}
