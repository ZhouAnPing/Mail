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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuItemSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAgent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAgentManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAgentType = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReportQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarFeeImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarFeeQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarWechat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarBroadcast = new System.Windows.Forms.ToolStripButton();
            this.menuMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSystem,
            this.menuItemAgent});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1016, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuItemSystem
            // 
            this.menuItemSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLogin,
            this.menuItemLogout,
            this.menuItemExit});
            this.menuItemSystem.Name = "menuItemSystem";
            this.menuItemSystem.Size = new System.Drawing.Size(65, 20);
            this.menuItemSystem.Text = "系统管理";
            // 
            // menuItemLogin
            // 
            this.menuItemLogin.Name = "menuItemLogin";
            this.menuItemLogin.Size = new System.Drawing.Size(94, 22);
            this.menuItemLogin.Text = "登陆";
            this.menuItemLogin.Visible = false;
            // 
            // menuItemLogout
            // 
            this.menuItemLogout.Name = "menuItemLogout";
            this.menuItemLogout.Size = new System.Drawing.Size(94, 22);
            this.menuItemLogout.Text = "注销";
            this.menuItemLogout.Visible = false;
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(94, 22);
            this.menuItemExit.Text = "退出";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemAgent
            // 
            this.menuItemAgent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAgentManagement,
            this.menuItemAgentType,
            this.menuItemReportQuery});
            this.menuItemAgent.Name = "menuItemAgent";
            this.menuItemAgent.Size = new System.Drawing.Size(65, 20);
            this.menuItemAgent.Text = "佣金管理";
            // 
            // menuItemAgentManagement
            // 
            this.menuItemAgentManagement.Name = "menuItemAgentManagement";
            this.menuItemAgentManagement.Size = new System.Drawing.Size(166, 22);
            this.menuItemAgentManagement.Text = "代理商佣金导入";
            this.menuItemAgentManagement.Click += new System.EventHandler(this.menuItemAgentImport_Click);
            // 
            // menuItemAgentType
            // 
            this.menuItemAgentType.Name = "menuItemAgentType";
            this.menuItemAgentType.Size = new System.Drawing.Size(166, 22);
            this.menuItemAgentType.Text = "代理商佣金推送";
            this.menuItemAgentType.Click += new System.EventHandler(this.menuItemAgentFeeQuery_Click);
            // 
            // menuItemReportQuery
            // 
            this.menuItemReportQuery.Name = "menuItemReportQuery";
            this.menuItemReportQuery.Size = new System.Drawing.Size(166, 22);
            this.menuItemReportQuery.Text = "佣金推送报告查询";
            this.menuItemReportQuery.Click += new System.EventHandler(this.menuItemReportQuery_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.toolbarFeeImport,
            this.toolStripSeparator3,
            this.toolbarFeeQuery,
            this.toolStripSeparator4,
            this.toolbarReport,
            this.toolStripSeparator2,
            this.toolbarWechat,
            this.toolStripSeparator7,
            this.toolbarBroadcast,
            this.toolStripSeparator6,
            this.toolStripButton1,
            this.toolStripSeparator1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(94, 717);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(91, 6);
            // 
            // toolbarFeeImport
            // 
            this.toolbarFeeImport.Image = ((System.Drawing.Image)(resources.GetObject("toolbarFeeImport.Image")));
            this.toolbarFeeImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarFeeImport.Name = "toolbarFeeImport";
            this.toolbarFeeImport.Size = new System.Drawing.Size(91, 64);
            this.toolbarFeeImport.Text = "代理商佣金导入";
            this.toolbarFeeImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbarFeeImport.Click += new System.EventHandler(this.menuItemAgentImport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(91, 6);
            // 
            // toolbarFeeQuery
            // 
            this.toolbarFeeQuery.Image = ((System.Drawing.Image)(resources.GetObject("toolbarFeeQuery.Image")));
            this.toolbarFeeQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarFeeQuery.Name = "toolbarFeeQuery";
            this.toolbarFeeQuery.Size = new System.Drawing.Size(91, 64);
            this.toolbarFeeQuery.Text = "代理商佣金推送";
            this.toolbarFeeQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbarFeeQuery.Click += new System.EventHandler(this.menuItemAgentFeeQuery_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(91, 6);
            // 
            // toolbarReport
            // 
            this.toolbarReport.Image = ((System.Drawing.Image)(resources.GetObject("toolbarReport.Image")));
            this.toolbarReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarReport.Name = "toolbarReport";
            this.toolbarReport.Size = new System.Drawing.Size(91, 64);
            this.toolbarReport.Text = "报告查询";
            this.toolbarReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbarReport.Click += new System.EventHandler(this.menuItemReportQuery_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(91, 6);
            // 
            // toolbarWechat
            // 
            this.toolbarWechat.Image = ((System.Drawing.Image)(resources.GetObject("toolbarWechat.Image")));
            this.toolbarWechat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarWechat.Name = "toolbarWechat";
            this.toolbarWechat.Size = new System.Drawing.Size(91, 64);
            this.toolbarWechat.Text = "微信账号同步";
            this.toolbarWechat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbarWechat.Click += new System.EventHandler(this.toolbarWechat_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(91, 6);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(91, 64);
            this.toolStripButton1.Text = "退出";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(91, 6);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(94, 719);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(922, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(91, 6);
            // 
            // toolbarBroadcast
            // 
            this.toolbarBroadcast.Image = ((System.Drawing.Image)(resources.GetObject("toolbarBroadcast.Image")));
            this.toolbarBroadcast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarBroadcast.Name = "toolbarBroadcast";
            this.toolbarBroadcast.Size = new System.Drawing.Size(91, 64);
            this.toolbarBroadcast.Text = "微信公告";
            this.toolbarBroadcast.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbarBroadcast.Click += new System.EventHandler(this.toolbarBroadcast_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.BackgroundImage = global::ChinaUnion_Agent.Properties.Resources.Desktop;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "联通佣金管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemAgent;
        private System.Windows.Forms.ToolStripMenuItem menuItemAgentManagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemAgentType;
        private System.Windows.Forms.ToolStripMenuItem menuItemSystem;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogin;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogout;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolbarFeeImport;
        private System.Windows.Forms.ToolStripButton toolbarFeeQuery;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemReportQuery;
        private System.Windows.Forms.ToolStripButton toolbarReport;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolbarWechat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolbarBroadcast;
    }
}

