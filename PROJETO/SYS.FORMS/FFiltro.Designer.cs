namespace SYS.FORMS
{
    partial class FFiltro
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
            this.sbCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.sbSelecionar = new DevExpress.XtraEditors.SimpleButton();
            this.sbAlterar = new DevExpress.XtraEditors.SimpleButton();
            this.sbCadastrar = new DevExpress.XtraEditors.SimpleButton();
            this.gcFiltro = new DevExpress.XtraGrid.GridControl();
            this.gvFiltro = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFiltro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFiltro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbCancelar);
            this.layoutControl1.Controls.Add(this.sbSelecionar);
            this.layoutControl1.Controls.Add(this.sbAlterar);
            this.layoutControl1.Controls.Add(this.sbCadastrar);
            this.layoutControl1.Controls.Add(this.gcFiltro);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(715, 304, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(684, 411);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbCancelar
            // 
            this.sbCancelar.Image = global::SYS.FORMS.Properties.Resources.cancel_x16;
            this.sbCancelar.Location = new System.Drawing.Point(602, 377);
            this.sbCancelar.Name = "sbCancelar";
            this.sbCancelar.Size = new System.Drawing.Size(70, 22);
            this.sbCancelar.StyleController = this.layoutControl1;
            this.sbCancelar.TabIndex = 8;
            this.sbCancelar.Text = "Cancelar";
            // 
            // sbSelecionar
            // 
            this.sbSelecionar.Image = global::SYS.FORMS.Properties.Resources.ok_x16;
            this.sbSelecionar.Location = new System.Drawing.Point(521, 377);
            this.sbSelecionar.Name = "sbSelecionar";
            this.sbSelecionar.Size = new System.Drawing.Size(77, 22);
            this.sbSelecionar.StyleController = this.layoutControl1;
            this.sbSelecionar.TabIndex = 7;
            this.sbSelecionar.Text = "Selecionar";
            // 
            // sbAlterar
            // 
            this.sbAlterar.Image = global::SYS.FORMS.Properties.Resources.refresh_update_x16;
            this.sbAlterar.Location = new System.Drawing.Point(92, 377);
            this.sbAlterar.Name = "sbAlterar";
            this.sbAlterar.Size = new System.Drawing.Size(61, 22);
            this.sbAlterar.StyleController = this.layoutControl1;
            this.sbAlterar.TabIndex = 6;
            this.sbAlterar.Text = "Alterar";
            // 
            // sbCadastrar
            // 
            this.sbCadastrar.Image = global::SYS.FORMS.Properties.Resources.add_x16;
            this.sbCadastrar.Location = new System.Drawing.Point(12, 377);
            this.sbCadastrar.Name = "sbCadastrar";
            this.sbCadastrar.Size = new System.Drawing.Size(76, 22);
            this.sbCadastrar.StyleController = this.layoutControl1;
            this.sbCadastrar.TabIndex = 5;
            this.sbCadastrar.Text = "Cadastrar";
            // 
            // gcFiltro
            // 
            this.gcFiltro.Location = new System.Drawing.Point(12, 12);
            this.gcFiltro.MainView = this.gvFiltro;
            this.gcFiltro.Name = "gcFiltro";
            this.gcFiltro.Size = new System.Drawing.Size(660, 361);
            this.gcFiltro.TabIndex = 4;
            this.gcFiltro.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFiltro});
            // 
            // gvFiltro
            // 
            this.gvFiltro.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.gvFiltro.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.gvFiltro.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.gvFiltro.Appearance.DetailTip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.DetailTip.Options.UseFont = true;
            this.gvFiltro.Appearance.Empty.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.Empty.Options.UseFont = true;
            this.gvFiltro.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.EvenRow.Options.UseFont = true;
            this.gvFiltro.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.FilterCloseButton.Options.UseFont = true;
            this.gvFiltro.Appearance.FilterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.FilterPanel.Options.UseFont = true;
            this.gvFiltro.Appearance.FixedLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.FixedLine.Options.UseFont = true;
            this.gvFiltro.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.FocusedCell.Options.UseFont = true;
            this.gvFiltro.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.FocusedRow.Options.UseFont = true;
            this.gvFiltro.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.FooterPanel.Options.UseFont = true;
            this.gvFiltro.Appearance.GroupButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.GroupButton.Options.UseFont = true;
            this.gvFiltro.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.GroupFooter.Options.UseFont = true;
            this.gvFiltro.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.GroupPanel.Options.UseFont = true;
            this.gvFiltro.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.GroupRow.Options.UseFont = true;
            this.gvFiltro.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvFiltro.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.HideSelectionRow.Options.UseFont = true;
            this.gvFiltro.Appearance.HorzLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.HorzLine.Options.UseFont = true;
            this.gvFiltro.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.OddRow.Options.UseFont = true;
            this.gvFiltro.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.Preview.Options.UseFont = true;
            this.gvFiltro.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.Row.Options.UseFont = true;
            this.gvFiltro.Appearance.RowSeparator.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.RowSeparator.Options.UseFont = true;
            this.gvFiltro.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.SelectedRow.Options.UseFont = true;
            this.gvFiltro.Appearance.TopNewRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.TopNewRow.Options.UseFont = true;
            this.gvFiltro.Appearance.VertLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.VertLine.Options.UseFont = true;
            this.gvFiltro.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvFiltro.Appearance.ViewCaption.Options.UseFont = true;
            this.gvFiltro.GridControl = this.gcFiltro;
            this.gvFiltro.Name = "gvFiltro";
            this.gvFiltro.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(684, 411);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcFiltro;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(664, 365);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbSelecionar;
            this.layoutControlItem4.Location = new System.Drawing.Point(509, 365);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(81, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbCancelar;
            this.layoutControlItem5.Location = new System.Drawing.Point(590, 365);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(74, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbCadastrar;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 365);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbAlterar;
            this.layoutControlItem3.Location = new System.Drawing.Point(80, 365);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(65, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(145, 365);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(364, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FFiltro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(684, 411);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl1);
            this.Name = "FFiltro";
            this.Text = "Filtro";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFiltro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFiltro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton sbCancelar;
        private DevExpress.XtraEditors.SimpleButton sbSelecionar;
        private DevExpress.XtraEditors.SimpleButton sbAlterar;
        private DevExpress.XtraEditors.SimpleButton sbCadastrar;
        private DevExpress.XtraGrid.GridControl gcFiltro;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFiltro;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}
