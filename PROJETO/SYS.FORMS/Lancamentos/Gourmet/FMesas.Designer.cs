namespace SYS.FORMS.Lancamentos.Gourmet
{
    partial class FMesas
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
            this.sbTransferir = new DevExpress.XtraEditors.SimpleButton();
            this.sbFechar = new DevExpress.XtraEditors.SimpleButton();
            this.tcMesas = new DevExpress.XtraEditors.TileControl();
            this.tgMesas = new DevExpress.XtraEditors.TileGroup();
            this.tcAmbientes = new DevExpress.XtraEditors.TileControl();
            this.tgAmbientes = new DevExpress.XtraEditors.TileGroup();
            this.pictureEdit3 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.timer1 = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbTransferir);
            this.layoutControl1.Controls.Add(this.sbFechar);
            this.layoutControl1.Controls.Add(this.tcMesas);
            this.layoutControl1.Controls.Add(this.tcAmbientes);
            this.layoutControl1.Controls.Add(this.pictureEdit3);
            this.layoutControl1.Controls.Add(this.pictureEdit2);
            this.layoutControl1.Controls.Add(this.pictureEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(757, 596);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbTransferir
            // 
            this.sbTransferir.Image = global::SYS.FORMS.Properties.Resources.refresh_update_x32;
            this.sbTransferir.Location = new System.Drawing.Point(547, 508);
            this.sbTransferir.Name = "sbTransferir";
            this.sbTransferir.Size = new System.Drawing.Size(93, 64);
            this.sbTransferir.StyleController = this.layoutControl1;
            this.sbTransferir.TabIndex = 11;
            this.sbTransferir.Text = "Transferir";
            this.sbTransferir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcAmbientes_KeyDown);
            // 
            // sbFechar
            // 
            this.sbFechar.Image = global::SYS.FORMS.Properties.Resources.close_window_x32;
            this.sbFechar.Location = new System.Drawing.Point(654, 508);
            this.sbFechar.Name = "sbFechar";
            this.sbFechar.Size = new System.Drawing.Size(79, 64);
            this.sbFechar.StyleController = this.layoutControl1;
            this.sbFechar.TabIndex = 11;
            this.sbFechar.Text = "Fechar";
            this.sbFechar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcAmbientes_KeyDown);
            // 
            // tcMesas
            // 
            this.tcMesas.AllowDrag = false;
            this.tcMesas.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(186)))), ((int)(((byte)(236)))));
            this.tcMesas.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tcMesas.DragSize = new System.Drawing.Size(0, 0);
            this.tcMesas.Groups.Add(this.tgMesas);
            this.tcMesas.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tcMesas.ItemSize = 60;
            this.tcMesas.Location = new System.Drawing.Point(24, 202);
            this.tcMesas.MaxId = 5;
            this.tcMesas.Name = "tcMesas";
            this.tcMesas.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tcMesas.Size = new System.Drawing.Size(709, 239);
            this.tcMesas.TabIndex = 10;
            this.tcMesas.Text = "tileControl2";
            this.tcMesas.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tcMesas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcAmbientes_KeyDown);
            // 
            // tgMesas
            // 
            this.tgMesas.Name = "tgMesas";
            // 
            // tcAmbientes
            // 
            this.tcAmbientes.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(186)))), ((int)(((byte)(236)))));
            this.tcAmbientes.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tcAmbientes.DragSize = new System.Drawing.Size(0, 0);
            this.tcAmbientes.Groups.Add(this.tgAmbientes);
            this.tcAmbientes.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tcAmbientes.ItemSize = 60;
            this.tcAmbientes.Location = new System.Drawing.Point(24, 37);
            this.tcAmbientes.MaxId = 4;
            this.tcAmbientes.Name = "tcAmbientes";
            this.tcAmbientes.Size = new System.Drawing.Size(709, 124);
            this.tcAmbientes.TabIndex = 9;
            this.tcAmbientes.Text = "tileControl1";
            this.tcAmbientes.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tcAmbientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcAmbientes_KeyDown);
            // 
            // tgAmbientes
            // 
            this.tgAmbientes.Name = "tgAmbientes";
            // 
            // pictureEdit3
            // 
            this.pictureEdit3.EditValue = global::SYS.FORMS.Properties.Resources.flag_blue_x64;
            this.pictureEdit3.Location = new System.Drawing.Point(220, 498);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit3.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit3.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit3.Size = new System.Drawing.Size(92, 74);
            this.pictureEdit3.StyleController = this.layoutControl1;
            this.pictureEdit3.TabIndex = 8;
            this.pictureEdit3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcAmbientes_KeyDown);
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = global::SYS.FORMS.Properties.Resources.flag_red_x64;
            this.pictureEdit2.Location = new System.Drawing.Point(24, 498);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Size = new System.Drawing.Size(94, 74);
            this.pictureEdit2.StyleController = this.layoutControl1;
            this.pictureEdit2.TabIndex = 7;
            this.pictureEdit2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcAmbientes_KeyDown);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::SYS.FORMS.Properties.Resources.flag_green_x64;
            this.pictureEdit1.Location = new System.Drawing.Point(122, 498);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Size = new System.Drawing.Size(94, 74);
            this.pictureEdit1.StyleController = this.layoutControl1;
            this.pictureEdit1.TabIndex = 6;
            this.pictureEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcAmbientes_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4,
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(757, 596);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.emptySpaceItem5,
            this.layoutControlItem7,
            this.emptySpaceItem3,
            this.emptySpaceItem4,
            this.emptySpaceItem2});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 445);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(737, 131);
            this.layoutControlGroup4.Text = "Legenda";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(292, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(231, 94);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(231, 94);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(231, 94);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.pictureEdit3;
            this.layoutControlItem5.Location = new System.Drawing.Point(196, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(96, 94);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(96, 94);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(96, 94);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Desocupando";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.pictureEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(98, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(98, 94);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(98, 94);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(98, 94);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Desocupada";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.pictureEdit2;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(98, 94);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(98, 94);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(98, 94);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Ocupada";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbFechar;
            this.layoutControlItem6.Location = new System.Drawing.Point(630, 26);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(83, 68);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(83, 68);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(83, 68);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(620, 0);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(10, 94);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(10, 94);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(10, 94);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sbTransferir;
            this.layoutControlItem7.Location = new System.Drawing.Point(523, 26);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(97, 68);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(97, 68);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(97, 68);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(671, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(42, 26);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(42, 26);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(42, 26);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem4.Location = new System.Drawing.Point(523, 0);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(97, 26);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(97, 26);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(97, 26);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.Text = "emptySpaceItem3";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(630, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(41, 26);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(41, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(41, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(737, 165);
            this.layoutControlGroup2.Text = "Ambientes";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tcAmbientes;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 128);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 128);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(713, 128);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 165);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(737, 280);
            this.layoutControlGroup3.Text = "Mesas";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tcMesas;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(713, 243);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FMesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(757, 596);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = true;
            this.Name = "FMesas";
            this.Text = "Mapa de mesas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FMesas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit3;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TileControl tcMesas;
        private DevExpress.XtraEditors.TileGroup tgMesas;
        private DevExpress.XtraEditors.TileControl tcAmbientes;
        private DevExpress.XtraEditors.TileGroup tgAmbientes;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbFechar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraEditors.SimpleButton sbTransferir;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private System.Windows.Forms.Timer timer1;
    }
}
