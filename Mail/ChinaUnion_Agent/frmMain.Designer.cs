namespace ChinaUnion_Agent
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuItemSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ToolBarPannelList = new BSE.Windows.Forms.XPanderPanelList();
            this.CommissionManagement = new BSE.Windows.Forms.XPanderPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.ErrorCodeManagment = new BSE.Windows.Forms.XPanderPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarErrorBroadcast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.InvoiceManagement = new BSE.Windows.Forms.XPanderPanel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInvoiceImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInvoiceManagement = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPaymentImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPaymentManagement = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInvoiceWechatPublish = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMain.SuspendLayout();
            this.ToolBarPannelList.SuspendLayout();
            this.CommissionManagement.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ErrorCodeManagment.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.InvoiceManagement.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSystem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1016, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuItemSystem
            // 
            this.menuItemSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLog,
            this.menuItemExit});
            this.menuItemSystem.Name = "menuItemSystem";
            this.menuItemSystem.Size = new System.Drawing.Size(65, 20);
            this.menuItemSystem.Text = "系统管理";
            // 
            // menuItemLog
            // 
            this.menuItemLog.Name = "menuItemLog";
            this.menuItemLog.Size = new System.Drawing.Size(118, 22);
            this.menuItemLog.Text = "版本日志";
            this.menuItemLog.Click += new System.EventHandler(this.menuItemLog_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(118, 22);
            this.menuItemExit.Text = "退出";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 719);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "author-48.png");
            this.imageList1.Images.SetKeyName(1, "3.png");
            this.imageList1.Images.SetKeyName(2, "12.png");
            this.imageList1.Images.SetKeyName(3, "2.png");
            this.imageList1.Images.SetKeyName(4, "4.png");
            this.imageList1.Images.SetKeyName(5, "1.png");
            this.imageList1.Images.SetKeyName(6, "5.png");
            this.imageList1.Images.SetKeyName(7, "6.png");
            this.imageList1.Images.SetKeyName(8, "7.png");
            this.imageList1.Images.SetKeyName(9, "8.png");
            this.imageList1.Images.SetKeyName(10, "9.png");
            this.imageList1.Images.SetKeyName(11, "10.png");
            this.imageList1.Images.SetKeyName(12, "11.png");
            // 
            // ToolBarPannelList
            // 
            this.ToolBarPannelList.BackgroundImage = global::ChinaUnion_Agent.Properties.Resources.桌面1;
            this.ToolBarPannelList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ToolBarPannelList.CaptionHeight = 30;
            this.ToolBarPannelList.CaptionStyle = BSE.Windows.Forms.CaptionStyle.Flat;
            this.ToolBarPannelList.Controls.Add(this.CommissionManagement);
            this.ToolBarPannelList.Controls.Add(this.ErrorCodeManagment);
            this.ToolBarPannelList.Controls.Add(this.InvoiceManagement);
            this.ToolBarPannelList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ToolBarPannelList.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.ToolBarPannelList.GradientBackground = System.Drawing.Color.Empty;
            this.ToolBarPannelList.Location = new System.Drawing.Point(0, 24);
            this.ToolBarPannelList.Name = "ToolBarPannelList";
            this.ToolBarPannelList.PanelColors = null;
            this.ToolBarPannelList.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.ToolBarPannelList.ShowGradientBackground = true;
            this.ToolBarPannelList.Size = new System.Drawing.Size(134, 695);
            this.ToolBarPannelList.TabIndex = 6;
            this.ToolBarPannelList.Text = "xPanderPanelList1";
            // 
            // CommissionManagement
            // 
            this.CommissionManagement.CaptionFont = new System.Drawing.Font("SimSun", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.CommissionManagement.CaptionHeight = 40;
            this.CommissionManagement.Controls.Add(this.toolStrip1);
            this.CommissionManagement.CustomColors.BackColor = System.Drawing.Color.Blue;
            this.CommissionManagement.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.CommissionManagement.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.CommissionManagement.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.CommissionManagement.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.CommissionManagement.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.CommissionManagement.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.CommissionManagement.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(244)))), ((int)(((byte)(242)))));
            this.CommissionManagement.CustomColors.CaptionGradientEnd = System.Drawing.Color.Blue;
            this.CommissionManagement.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.CommissionManagement.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.CommissionManagement.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.CommissionManagement.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.CommissionManagement.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.CommissionManagement.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.CommissionManagement.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.CommissionManagement.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.CommissionManagement.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.CommissionManagement.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.CommissionManagement.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(244)))), ((int)(((byte)(242)))));
            this.CommissionManagement.CustomColors.InnerBorderColor = System.Drawing.Color.SkyBlue;
            this.CommissionManagement.Font = new System.Drawing.Font("SimSun", 10F);
            this.CommissionManagement.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CommissionManagement.Image = null;
            this.CommissionManagement.Name = "CommissionManagement";
            this.CommissionManagement.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.CommissionManagement.Size = new System.Drawing.Size(134, 40);
            this.CommissionManagement.TabIndex = 0;
            this.CommissionManagement.Text = "佣金受理管理";
            this.CommissionManagement.ToolTipTextCloseIcon = null;
            this.CommissionManagement.ToolTipTextExpandIconPanelCollapsed = null;
            this.CommissionManagement.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImage = global::ChinaUnion_Agent.Properties.Resources.Desktop;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.toolStripButton2,
            this.toolStripSeparator3,
            this.toolStripButton12,
            this.toolStripSeparator5,
            this.toolStripButton3,
            this.toolStripSeparator4,
            this.toolStripButton15,
            this.toolStripSeparator10,
            this.toolStripButton7,
            this.toolStripSeparator7,
            this.toolStripButton17});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(1, 40);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(132, 0);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(93, 64);
            this.toolStripButton1.Text = "代理商佣金导入";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.CommissionImport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(93, 64);
            this.toolStripButton2.Text = "代理商佣金发布";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.CommissionPublish_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(93, 64);
            this.toolStripButton12.Text = "代理商报告查询";
            this.toolStripButton12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton12.Click += new System.EventHandler(this.CommissionOpenReport_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(93, 64);
            this.toolStripButton3.Text = "代理商信息导入";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.ChannelImport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton15.Image")));
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(81, 64);
            this.toolStripButton15.Text = "微信账号同步";
            this.toolStripButton15.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton15.ToolTipText = "代理商微信同步";
            this.toolStripButton15.Click += new System.EventHandler(this.ChannelWechatSync_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(81, 64);
            this.toolStripButton7.Text = "微信账号查询";
            this.toolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton7.Click += new System.EventHandler(this.ChannelWechatQuery_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton17.Image")));
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(81, 64);
            this.toolStripButton17.Text = "微信公告发布";
            this.toolStripButton17.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton17.Click += new System.EventHandler(this.ChannelWechatBroadcast_Click);
            // 
            // ErrorCodeManagment
            // 
            this.ErrorCodeManagment.CaptionFont = new System.Drawing.Font("SimSun", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.ErrorCodeManagment.CaptionHeight = 40;
            this.ErrorCodeManagment.Controls.Add(this.toolStrip2);
            this.ErrorCodeManagment.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.ErrorCodeManagment.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.ErrorCodeManagment.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.ErrorCodeManagment.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.ErrorCodeManagment.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.ErrorCodeManagment.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.ErrorCodeManagment.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.ErrorCodeManagment.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(244)))), ((int)(((byte)(242)))));
            this.ErrorCodeManagment.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.ErrorCodeManagment.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.ErrorCodeManagment.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.ErrorCodeManagment.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.ErrorCodeManagment.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.ErrorCodeManagment.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.ErrorCodeManagment.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.ErrorCodeManagment.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.ErrorCodeManagment.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.ErrorCodeManagment.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.ErrorCodeManagment.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.ErrorCodeManagment.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(244)))), ((int)(((byte)(242)))));
            this.ErrorCodeManagment.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.ErrorCodeManagment.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ErrorCodeManagment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ErrorCodeManagment.Image = null;
            this.ErrorCodeManagment.Name = "ErrorCodeManagment";
            this.ErrorCodeManagment.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.ErrorCodeManagment.Size = new System.Drawing.Size(134, 40);
            this.ErrorCodeManagment.TabIndex = 1;
            this.ErrorCodeManagment.Text = "报错受理管理";
            this.ErrorCodeManagment.ToolTipTextCloseIcon = null;
            this.ErrorCodeManagment.ToolTipTextExpandIconPanelCollapsed = null;
            this.ErrorCodeManagment.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackgroundImage = global::ChinaUnion_Agent.Properties.Resources.Desktop;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator11,
            this.toolStripButton5,
            this.toolStripSeparator15,
            this.toolStripButton6,
            this.toolStripSeparator16,
            this.toolStripButton8,
            this.toolStripSeparator9,
            this.toolStripButton4,
            this.toolStripSeparator8,
            this.toolbarErrorBroadcast,
            this.toolStripSeparator6});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(1, 40);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(132, 0);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(81, 64);
            this.toolStripButton5.Text = "报错信息导入";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton5.Click += new System.EventHandler(this.ErrorCodeImport_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(81, 64);
            this.toolStripButton6.Text = "报错信息查询";
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton6.Click += new System.EventHandler(this.ErrorCodeQuery_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(81, 64);
            this.toolStripButton8.Text = "微信账号导入";
            this.toolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton8.Click += new System.EventHandler(this.ErrorWechatImport_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(81, 64);
            this.toolStripButton4.Text = "微信账号查询";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton4.Click += new System.EventHandler(this.ErrorWechatQuery_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(130, 6);
            // 
            // toolbarErrorBroadcast
            // 
            this.toolbarErrorBroadcast.Image = ((System.Drawing.Image)(resources.GetObject("toolbarErrorBroadcast.Image")));
            this.toolbarErrorBroadcast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarErrorBroadcast.Name = "toolbarErrorBroadcast";
            this.toolbarErrorBroadcast.Size = new System.Drawing.Size(81, 64);
            this.toolbarErrorBroadcast.Text = "微信公告发布";
            this.toolbarErrorBroadcast.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbarErrorBroadcast.ToolTipText = "微信公告";
            this.toolbarErrorBroadcast.Click += new System.EventHandler(this.ErrorWehatBroadcast_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(130, 6);
            // 
            // InvoiceManagement
            // 
            this.InvoiceManagement.CaptionFont = new System.Drawing.Font("SimSun", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.InvoiceManagement.CaptionHeight = 40;
            this.InvoiceManagement.Controls.Add(this.toolStrip3);
            this.InvoiceManagement.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.InvoiceManagement.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.InvoiceManagement.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.InvoiceManagement.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.InvoiceManagement.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.InvoiceManagement.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.InvoiceManagement.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.InvoiceManagement.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(244)))), ((int)(((byte)(242)))));
            this.InvoiceManagement.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.InvoiceManagement.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.InvoiceManagement.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.InvoiceManagement.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.InvoiceManagement.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(146)))), ((int)(((byte)(181)))));
            this.InvoiceManagement.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.InvoiceManagement.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.InvoiceManagement.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.InvoiceManagement.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.InvoiceManagement.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.InvoiceManagement.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.InvoiceManagement.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(244)))), ((int)(((byte)(242)))));
            this.InvoiceManagement.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.InvoiceManagement.Expand = true;
            this.InvoiceManagement.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.InvoiceManagement.ForeColor = System.Drawing.SystemColors.ControlText;
            this.InvoiceManagement.Image = null;
            this.InvoiceManagement.Name = "InvoiceManagement";
            this.InvoiceManagement.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.InvoiceManagement.Size = new System.Drawing.Size(134, 615);
            this.InvoiceManagement.TabIndex = 2;
            this.InvoiceManagement.Text = "发票受理管理";
            this.InvoiceManagement.ToolTipTextCloseIcon = null;
            this.InvoiceManagement.ToolTipTextExpandIconPanelCollapsed = null;
            this.InvoiceManagement.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // toolStrip3
            // 
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.BackgroundImage = global::ChinaUnion_Agent.Properties.Resources.Desktop;
            this.toolStrip3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator12,
            this.btnInvoiceImport,
            this.toolStripSeparator13,
            this.btnInvoiceManagement,
            this.toolStripSeparator14,
            this.btnPaymentImport,
            this.toolStripSeparator17,
            this.btnPaymentManagement,
            this.toolStripSeparator18,
            this.btnInvoiceWechatPublish,
            this.toolStripSeparator19});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(1, 40);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(132, 575);
            this.toolStrip3.TabIndex = 5;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(130, 6);
            // 
            // btnInvoiceImport
            // 
            this.btnInvoiceImport.Image = ((System.Drawing.Image)(resources.GetObject("btnInvoiceImport.Image")));
            this.btnInvoiceImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInvoiceImport.Name = "btnInvoiceImport";
            this.btnInvoiceImport.Size = new System.Drawing.Size(130, 64);
            this.btnInvoiceImport.Text = "发票信息导入";
            this.btnInvoiceImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInvoiceImport.Click += new System.EventHandler(this.btnInvoiceImport_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(130, 6);
            // 
            // btnInvoiceManagement
            // 
            this.btnInvoiceManagement.Image = ((System.Drawing.Image)(resources.GetObject("btnInvoiceManagement.Image")));
            this.btnInvoiceManagement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInvoiceManagement.Name = "btnInvoiceManagement";
            this.btnInvoiceManagement.Size = new System.Drawing.Size(130, 64);
            this.btnInvoiceManagement.Text = "发票信息管理";
            this.btnInvoiceManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInvoiceManagement.Click += new System.EventHandler(this.btnInvoiceManagement_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(130, 6);
            // 
            // btnPaymentImport
            // 
            this.btnPaymentImport.Image = ((System.Drawing.Image)(resources.GetObject("btnPaymentImport.Image")));
            this.btnPaymentImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaymentImport.Name = "btnPaymentImport";
            this.btnPaymentImport.Size = new System.Drawing.Size(130, 64);
            this.btnPaymentImport.Text = "支付信息导入";
            this.btnPaymentImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPaymentImport.Click += new System.EventHandler(this.btnPaymentImport_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(130, 6);
            // 
            // btnPaymentManagement
            // 
            this.btnPaymentManagement.Image = ((System.Drawing.Image)(resources.GetObject("btnPaymentManagement.Image")));
            this.btnPaymentManagement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaymentManagement.Name = "btnPaymentManagement";
            this.btnPaymentManagement.Size = new System.Drawing.Size(130, 64);
            this.btnPaymentManagement.Text = "支付信息管理";
            this.btnPaymentManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPaymentManagement.Click += new System.EventHandler(this.btnPaymentManagement_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(130, 6);
            // 
            // btnInvoiceWechatPublish
            // 
            this.btnInvoiceWechatPublish.Image = ((System.Drawing.Image)(resources.GetObject("btnInvoiceWechatPublish.Image")));
            this.btnInvoiceWechatPublish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInvoiceWechatPublish.Name = "btnInvoiceWechatPublish";
            this.btnInvoiceWechatPublish.Size = new System.Drawing.Size(130, 64);
            this.btnInvoiceWechatPublish.Text = "微信公告发布";
            this.btnInvoiceWechatPublish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInvoiceWechatPublish.ToolTipText = "微信公告公告";
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(130, 6);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.ToolBarPannelList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上海联通合作伙伴支撑平台";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ToolBarPannelList.ResumeLayout(false);
            this.CommissionManagement.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ErrorCodeManagment.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.InvoiceManagement.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemSystem;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private BSE.Windows.Forms.XPanderPanelList ToolBarPannelList;
        private BSE.Windows.Forms.XPanderPanel CommissionManagement;
        private BSE.Windows.Forms.XPanderPanel ErrorCodeManagment;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolbarErrorBroadcast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripMenuItem menuItemLog;
        private BSE.Windows.Forms.XPanderPanel InvoiceManagement;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton btnInvoiceImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton btnInvoiceManagement;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton btnPaymentImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripButton btnPaymentManagement;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripButton btnInvoiceWechatPublish;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
    }
}

