namespace ChinaUnion_Agent
{
    partial class frmMailReport
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnReSend = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.cboFeeBatch = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new BSE.Windows.Forms.Panel();
            this.progressBar8 = new BSE.Windows.Forms.ProgressBar();
            this.progressBar7 = new BSE.Windows.Forms.ProgressBar();
            this.progressBar1 = new BSE.Windows.Forms.ProgressBar();
            this.panel13 = new System.Windows.Forms.Panel();
            this.lblNotSent = new System.Windows.Forms.Label();
            this.linkNotSent = new System.Windows.Forms.LinkLabel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.lblBounced = new System.Windows.Forms.Label();
            this.linkBounced = new System.Windows.Forms.LinkLabel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblDelivered = new System.Windows.Forms.Label();
            this.linkDelivered = new System.Windows.Forms.LinkLabel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblRecipients = new System.Windows.Forms.Label();
            this.linkRecipients = new System.Windows.Forms.LinkLabel();
            this.xPanderPanelList1 = new BSE.Windows.Forms.XPanderPanelList();
            this.pnlSent = new BSE.Windows.Forms.XPanderPanel();
            this.dgvSent = new System.Windows.Forms.DataGridView();
            this.pnlOpen = new BSE.Windows.Forms.XPanderPanel();
            this.dgvOpened = new System.Windows.Forms.DataGridView();
            this.pnlSkip = new BSE.Windows.Forms.XPanderPanel();
            this.dgvSkipped = new System.Windows.Forms.DataGridView();
            this.pnlBounced = new BSE.Windows.Forms.XPanderPanel();
            this.dgvBounced = new System.Windows.Forms.DataGridView();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.xPanderPanelList1.SuspendLayout();
            this.pnlSent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSent)).BeginInit();
            this.pnlOpen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpened)).BeginInit();
            this.pnlSkip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkipped)).BeginInit();
            this.pnlBounced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBounced)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnReSend);
            this.panel5.Controls.Add(this.btnExportExcel);
            this.panel5.Controls.Add(this.cboFeeBatch);
            this.panel5.Controls.Add(this.btnQuery);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1016, 38);
            this.panel5.TabIndex = 2;
            // 
            // btnReSend
            // 
            this.btnReSend.Enabled = false;
            this.btnReSend.Location = new System.Drawing.Point(614, 4);
            this.btnReSend.Name = "btnReSend";
            this.btnReSend.Size = new System.Drawing.Size(106, 31);
            this.btnReSend.TabIndex = 11;
            this.btnReSend.Text = "未到达邮件重发";
            this.btnReSend.UseVisualStyleBackColor = true;
            this.btnReSend.Click += new System.EventHandler(this.btnReSend_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(757, 3);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(74, 31);
            this.btnExportExcel.TabIndex = 10;
            this.btnExportExcel.Text = "导出Exceel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Visible = false;
            // 
            // cboFeeBatch
            // 
            this.cboFeeBatch.FormattingEnabled = true;
            this.cboFeeBatch.Location = new System.Drawing.Point(79, 9);
            this.cboFeeBatch.Name = "cboFeeBatch";
            this.cboFeeBatch.Size = new System.Drawing.Size(416, 20);
            this.cboFeeBatch.TabIndex = 9;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(501, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(74, 31);
            this.btnQuery.TabIndex = 8;
            this.btnQuery.Text = "快速查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "发送批次：";
            // 
            // panel3
            // 
            this.panel3.AssociatedSplitter = null;
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 11.75F, System.Drawing.FontStyle.Bold);
            this.panel3.CaptionHeight = 27;
            this.panel3.Controls.Add(this.progressBar8);
            this.panel3.Controls.Add(this.progressBar7);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Controls.Add(this.panel13);
            this.panel3.Controls.Add(this.panel12);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.panel3.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.panel3.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.panel3.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.panel3.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.panel3.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.panel3.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.panel3.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.panel3.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.panel3.CustomColors.CollapsedCaptionText = System.Drawing.SystemColors.ControlText;
            this.panel3.CustomColors.ContentGradientBegin = System.Drawing.SystemColors.ButtonFace;
            this.panel3.CustomColors.ContentGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.panel3.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Image = null;
            this.panel3.Location = new System.Drawing.Point(0, 38);
            this.panel3.MinimumSize = new System.Drawing.Size(27, 27);
            this.panel3.Name = "panel3";
            this.panel3.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.panel3.ShowExpandIcon = true;
            this.panel3.Size = new System.Drawing.Size(1016, 99);
            this.panel3.TabIndex = 3;
            this.panel3.Text = "邮件发送报告";
            this.panel3.ToolTipTextCloseIcon = null;
            this.panel3.ToolTipTextExpandIconPanelCollapsed = null;
            this.panel3.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // progressBar8
            // 
            this.progressBar8.BackgroundColor = System.Drawing.Color.Maroon;
            this.progressBar8.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.progressBar8.Location = new System.Drawing.Point(444, 47);
            this.progressBar8.Maximum = 100;
            this.progressBar8.Minimum = 0;
            this.progressBar8.Name = "progressBar8";
            this.progressBar8.Size = new System.Drawing.Size(48, 5);
            this.progressBar8.TabIndex = 20;
            this.progressBar8.Value = 0;
            this.progressBar8.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            // 
            // progressBar7
            // 
            this.progressBar7.BackgroundColor = System.Drawing.Color.Maroon;
            this.progressBar7.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.progressBar7.Location = new System.Drawing.Point(286, 47);
            this.progressBar7.Maximum = 100;
            this.progressBar7.Minimum = 0;
            this.progressBar7.Name = "progressBar7";
            this.progressBar7.Size = new System.Drawing.Size(50, 5);
            this.progressBar7.TabIndex = 19;
            this.progressBar7.Value = 0;
            this.progressBar7.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            // 
            // progressBar1
            // 
            this.progressBar1.BackgroundColor = System.Drawing.Color.Green;
            this.progressBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.progressBar1.Location = new System.Drawing.Point(132, 47);
            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(52, 5);
            this.progressBar1.TabIndex = 13;
            this.progressBar1.Value = 0;
            this.progressBar1.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.lblNotSent);
            this.panel13.Controls.Add(this.linkNotSent);
            this.panel13.Location = new System.Drawing.Point(492, 35);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(100, 36);
            this.panel13.TabIndex = 9;
            // 
            // lblNotSent
            // 
            this.lblNotSent.AutoSize = true;
            this.lblNotSent.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNotSent.Location = new System.Drawing.Point(7, 17);
            this.lblNotSent.Name = "lblNotSent";
            this.lblNotSent.Size = new System.Drawing.Size(47, 12);
            this.lblNotSent.TabIndex = 1;
            this.lblNotSent.Text = "0/0.00%";
            // 
            // linkNotSent
            // 
            this.linkNotSent.AutoSize = true;
            this.linkNotSent.ForeColor = System.Drawing.SystemColors.Control;
            this.linkNotSent.LinkColor = System.Drawing.Color.White;
            this.linkNotSent.Location = new System.Drawing.Point(6, 0);
            this.linkNotSent.Name = "linkNotSent";
            this.linkNotSent.Size = new System.Drawing.Size(65, 12);
            this.linkNotSent.TabIndex = 0;
            this.linkNotSent.TabStop = true;
            this.linkNotSent.Text = "未发送数量";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.LightGray;
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.lblBounced);
            this.panel12.Controls.Add(this.linkBounced);
            this.panel12.Location = new System.Drawing.Point(341, 34);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(100, 36);
            this.panel12.TabIndex = 8;
            // 
            // lblBounced
            // 
            this.lblBounced.AutoSize = true;
            this.lblBounced.Location = new System.Drawing.Point(7, 17);
            this.lblBounced.Name = "lblBounced";
            this.lblBounced.Size = new System.Drawing.Size(47, 12);
            this.lblBounced.TabIndex = 1;
            this.lblBounced.Text = "0/0.00%";
            // 
            // linkBounced
            // 
            this.linkBounced.AutoSize = true;
            this.linkBounced.Location = new System.Drawing.Point(7, 5);
            this.linkBounced.Name = "linkBounced";
            this.linkBounced.Size = new System.Drawing.Size(53, 12);
            this.linkBounced.TabIndex = 0;
            this.linkBounced.TabStop = true;
            this.linkBounced.Text = "弹回数量";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.lblDelivered);
            this.panel7.Controls.Add(this.linkDelivered);
            this.panel7.Location = new System.Drawing.Point(184, 34);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(100, 36);
            this.panel7.TabIndex = 3;
            // 
            // lblDelivered
            // 
            this.lblDelivered.AutoSize = true;
            this.lblDelivered.Location = new System.Drawing.Point(7, 17);
            this.lblDelivered.Name = "lblDelivered";
            this.lblDelivered.Size = new System.Drawing.Size(47, 12);
            this.lblDelivered.TabIndex = 1;
            this.lblDelivered.Text = "0/0.00%";
            // 
            // linkDelivered
            // 
            this.linkDelivered.AutoSize = true;
            this.linkDelivered.Location = new System.Drawing.Point(6, 0);
            this.linkDelivered.Name = "linkDelivered";
            this.linkDelivered.Size = new System.Drawing.Size(65, 12);
            this.linkDelivered.TabIndex = 0;
            this.linkDelivered.TabStop = true;
            this.linkDelivered.Text = "已发送数量";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lblRecipients);
            this.panel6.Controls.Add(this.linkRecipients);
            this.panel6.Location = new System.Drawing.Point(31, 33);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(100, 36);
            this.panel6.TabIndex = 2;
            // 
            // lblRecipients
            // 
            this.lblRecipients.AutoSize = true;
            this.lblRecipients.Location = new System.Drawing.Point(17, 17);
            this.lblRecipients.Name = "lblRecipients";
            this.lblRecipients.Size = new System.Drawing.Size(47, 12);
            this.lblRecipients.TabIndex = 1;
            this.lblRecipients.Text = "0/0.00%";
            // 
            // linkRecipients
            // 
            this.linkRecipients.AutoSize = true;
            this.linkRecipients.Location = new System.Drawing.Point(6, 0);
            this.linkRecipients.Name = "linkRecipients";
            this.linkRecipients.Size = new System.Drawing.Size(89, 12);
            this.linkRecipients.TabIndex = 0;
            this.linkRecipients.TabStop = true;
            this.linkRecipients.Text = "需要发送的数量";
            // 
            // xPanderPanelList1
            // 
            this.xPanderPanelList1.CaptionHeight = 20;
            this.xPanderPanelList1.CaptionStyle = BSE.Windows.Forms.CaptionStyle.Normal;
            this.xPanderPanelList1.Controls.Add(this.pnlSent);
            this.xPanderPanelList1.Controls.Add(this.pnlOpen);
            this.xPanderPanelList1.Controls.Add(this.pnlSkip);
            this.xPanderPanelList1.Controls.Add(this.pnlBounced);
            this.xPanderPanelList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanderPanelList1.GradientBackground = System.Drawing.Color.Empty;
            this.xPanderPanelList1.Location = new System.Drawing.Point(0, 137);
            this.xPanderPanelList1.Name = "xPanderPanelList1";
            this.xPanderPanelList1.PanelColors = null;
            this.xPanderPanelList1.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.xPanderPanelList1.Size = new System.Drawing.Size(1016, 604);
            this.xPanderPanelList1.TabIndex = 5;
            this.xPanderPanelList1.Text = "xPanderPanelList1";
            // 
            // pnlSent
            // 
            this.pnlSent.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold);
            this.pnlSent.CaptionHeight = 20;
            this.pnlSent.Controls.Add(this.dgvSent);
            this.pnlSent.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSent.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.pnlSent.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.pnlSent.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.pnlSent.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.pnlSent.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.pnlSent.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.pnlSent.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.pnlSent.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.pnlSent.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlSent.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlSent.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlSent.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlSent.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlSent.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlSent.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlSent.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.pnlSent.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.pnlSent.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlSent.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.pnlSent.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.pnlSent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlSent.Image = null;
            this.pnlSent.Name = "pnlSent";
            this.pnlSent.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.pnlSent.Size = new System.Drawing.Size(1016, 20);
            this.pnlSent.TabIndex = 0;
            this.pnlSent.Text = "已发送(Sent)";
            this.pnlSent.ToolTipTextCloseIcon = null;
            this.pnlSent.ToolTipTextExpandIconPanelCollapsed = null;
            this.pnlSent.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // dgvSent
            // 
            this.dgvSent.AllowUserToAddRows = false;
            this.dgvSent.AllowUserToDeleteRows = false;
            this.dgvSent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSent.Location = new System.Drawing.Point(1, 20);
            this.dgvSent.Name = "dgvSent";
            this.dgvSent.ReadOnly = true;
            this.dgvSent.RowHeadersWidth = 10;
            this.dgvSent.RowTemplate.Height = 23;
            this.dgvSent.Size = new System.Drawing.Size(1014, 0);
            this.dgvSent.TabIndex = 4;
            // 
            // pnlOpen
            // 
            this.pnlOpen.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold);
            this.pnlOpen.CaptionHeight = 20;
            this.pnlOpen.Controls.Add(this.dgvOpened);
            this.pnlOpen.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.pnlOpen.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.pnlOpen.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.pnlOpen.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.pnlOpen.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.pnlOpen.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.pnlOpen.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.pnlOpen.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.pnlOpen.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.pnlOpen.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlOpen.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlOpen.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlOpen.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlOpen.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlOpen.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlOpen.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlOpen.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.pnlOpen.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.pnlOpen.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlOpen.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.pnlOpen.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.pnlOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlOpen.Image = null;
            this.pnlOpen.Name = "pnlOpen";
            this.pnlOpen.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.pnlOpen.Size = new System.Drawing.Size(1016, 20);
            this.pnlOpen.TabIndex = 1;
            this.pnlOpen.Text = "已打开(Opened)";
            this.pnlOpen.ToolTipTextCloseIcon = null;
            this.pnlOpen.ToolTipTextExpandIconPanelCollapsed = null;
            this.pnlOpen.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // dgvOpened
            // 
            this.dgvOpened.AllowUserToAddRows = false;
            this.dgvOpened.AllowUserToDeleteRows = false;
            this.dgvOpened.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpened.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOpened.Location = new System.Drawing.Point(1, 20);
            this.dgvOpened.Name = "dgvOpened";
            this.dgvOpened.ReadOnly = true;
            this.dgvOpened.RowHeadersWidth = 10;
            this.dgvOpened.RowTemplate.Height = 23;
            this.dgvOpened.Size = new System.Drawing.Size(1014, 0);
            this.dgvOpened.TabIndex = 4;
            // 
            // pnlSkip
            // 
            this.pnlSkip.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold);
            this.pnlSkip.CaptionHeight = 20;
            this.pnlSkip.Controls.Add(this.dgvSkipped);
            this.pnlSkip.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSkip.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.pnlSkip.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.pnlSkip.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.pnlSkip.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.pnlSkip.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.pnlSkip.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.pnlSkip.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.pnlSkip.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.pnlSkip.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlSkip.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlSkip.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlSkip.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlSkip.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlSkip.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlSkip.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlSkip.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.pnlSkip.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.pnlSkip.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlSkip.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.pnlSkip.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.pnlSkip.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlSkip.Image = null;
            this.pnlSkip.Name = "pnlSkip";
            this.pnlSkip.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.pnlSkip.Size = new System.Drawing.Size(1016, 20);
            this.pnlSkip.TabIndex = 2;
            this.pnlSkip.Text = "已跳过(Skipped)";
            this.pnlSkip.ToolTipTextCloseIcon = null;
            this.pnlSkip.ToolTipTextExpandIconPanelCollapsed = null;
            this.pnlSkip.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // dgvSkipped
            // 
            this.dgvSkipped.AllowUserToAddRows = false;
            this.dgvSkipped.AllowUserToDeleteRows = false;
            this.dgvSkipped.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkipped.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSkipped.Location = new System.Drawing.Point(1, 20);
            this.dgvSkipped.Name = "dgvSkipped";
            this.dgvSkipped.ReadOnly = true;
            this.dgvSkipped.RowHeadersWidth = 10;
            this.dgvSkipped.RowTemplate.Height = 23;
            this.dgvSkipped.Size = new System.Drawing.Size(1014, 0);
            this.dgvSkipped.TabIndex = 4;
            // 
            // pnlBounced
            // 
            this.pnlBounced.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold);
            this.pnlBounced.CaptionHeight = 20;
            this.pnlBounced.Controls.Add(this.dgvBounced);
            this.pnlBounced.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBounced.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.pnlBounced.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.pnlBounced.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.pnlBounced.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.pnlBounced.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.pnlBounced.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.pnlBounced.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.pnlBounced.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.pnlBounced.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlBounced.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlBounced.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlBounced.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pnlBounced.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlBounced.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlBounced.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.pnlBounced.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.pnlBounced.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.pnlBounced.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlBounced.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.pnlBounced.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.pnlBounced.Expand = true;
            this.pnlBounced.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlBounced.Image = null;
            this.pnlBounced.Name = "pnlBounced";
            this.pnlBounced.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.pnlBounced.Size = new System.Drawing.Size(1016, 544);
            this.pnlBounced.TabIndex = 3;
            this.pnlBounced.Text = "退回(Bounced)";
            this.pnlBounced.ToolTipTextCloseIcon = null;
            this.pnlBounced.ToolTipTextExpandIconPanelCollapsed = null;
            this.pnlBounced.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // dgvBounced
            // 
            this.dgvBounced.AllowUserToAddRows = false;
            this.dgvBounced.AllowUserToDeleteRows = false;
            this.dgvBounced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBounced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBounced.Location = new System.Drawing.Point(1, 20);
            this.dgvBounced.Name = "dgvBounced";
            this.dgvBounced.ReadOnly = true;
            this.dgvBounced.RowHeadersWidth = 10;
            this.dgvBounced.RowTemplate.Height = 23;
            this.dgvBounced.Size = new System.Drawing.Size(1014, 524);
            this.dgvBounced.TabIndex = 4;
            // 
            // frmMailReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.xPanderPanelList1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Name = "frmMailReport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报告查询";
            this.Load += new System.EventHandler(this.frmMailReport_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.xPanderPanelList1.ResumeLayout(false);
            this.pnlSent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSent)).EndInit();
            this.pnlOpen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpened)).EndInit();
            this.pnlSkip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkipped)).EndInit();
            this.pnlBounced.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBounced)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.ComboBox cboFeeBatch;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label5;
        private BSE.Windows.Forms.Panel panel3;
        private BSE.Windows.Forms.ProgressBar progressBar8;
        private BSE.Windows.Forms.ProgressBar progressBar7;
        private BSE.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label lblNotSent;
        private System.Windows.Forms.LinkLabel linkNotSent;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label lblBounced;
        private System.Windows.Forms.LinkLabel linkBounced;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblDelivered;
        private System.Windows.Forms.LinkLabel linkDelivered;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblRecipients;
        private System.Windows.Forms.LinkLabel linkRecipients;
        private BSE.Windows.Forms.XPanderPanelList xPanderPanelList1;
        private BSE.Windows.Forms.XPanderPanel pnlSent;
        private System.Windows.Forms.DataGridView dgvSent;
        private BSE.Windows.Forms.XPanderPanel pnlOpen;
        private System.Windows.Forms.DataGridView dgvOpened;
        private BSE.Windows.Forms.XPanderPanel pnlSkip;
        private System.Windows.Forms.DataGridView dgvSkipped;
        private BSE.Windows.Forms.XPanderPanel pnlBounced;
        private System.Windows.Forms.DataGridView dgvBounced;
        private System.Windows.Forms.Button btnReSend;
    }
}