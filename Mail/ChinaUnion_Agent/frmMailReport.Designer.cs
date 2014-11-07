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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReSend = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.cboFeeBatch = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSent = new System.Windows.Forms.TabPage();
            this.dgvSent = new System.Windows.Forms.DataGridView();
            this.tabOpened = new System.Windows.Forms.TabPage();
            this.dgvOpened = new System.Windows.Forms.DataGridView();
            this.tabBounced = new System.Windows.Forms.TabPage();
            this.dgvBounced = new System.Windows.Forms.DataGridView();
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
            this.panel5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabSent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSent)).BeginInit();
            this.tabOpened.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpened)).BeginInit();
            this.tabBounced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBounced)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnCancel);
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(755, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnExportExcel.Location = new System.Drawing.Point(861, 3);
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
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabSent);
            this.tabControl1.Controls.Add(this.tabOpened);
            this.tabControl1.Controls.Add(this.tabBounced);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 137);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 604);
            this.tabControl1.TabIndex = 6;
            // 
            // tabSent
            // 
            this.tabSent.Controls.Add(this.dgvSent);
            this.tabSent.Location = new System.Drawing.Point(4, 25);
            this.tabSent.Name = "tabSent";
            this.tabSent.Padding = new System.Windows.Forms.Padding(3);
            this.tabSent.Size = new System.Drawing.Size(1008, 575);
            this.tabSent.TabIndex = 0;
            this.tabSent.Text = "已发送(Sent)";
            this.tabSent.UseVisualStyleBackColor = true;
            // 
            // dgvSent
            // 
            this.dgvSent.AllowUserToAddRows = false;
            this.dgvSent.AllowUserToDeleteRows = false;
            this.dgvSent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgvSent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSent.Location = new System.Drawing.Point(3, 3);
            this.dgvSent.Name = "dgvSent";
            this.dgvSent.ReadOnly = true;
            this.dgvSent.RowHeadersWidth = 10;
            this.dgvSent.RowTemplate.Height = 23;
            this.dgvSent.Size = new System.Drawing.Size(1002, 569);
            this.dgvSent.TabIndex = 5;
            // 
            // tabOpened
            // 
            this.tabOpened.Controls.Add(this.dgvOpened);
            this.tabOpened.Location = new System.Drawing.Point(4, 25);
            this.tabOpened.Name = "tabOpened";
            this.tabOpened.Padding = new System.Windows.Forms.Padding(3);
            this.tabOpened.Size = new System.Drawing.Size(1008, 575);
            this.tabOpened.TabIndex = 1;
            this.tabOpened.Text = "已打开(Opened)";
            this.tabOpened.UseVisualStyleBackColor = true;
            // 
            // dgvOpened
            // 
            this.dgvOpened.AllowUserToAddRows = false;
            this.dgvOpened.AllowUserToDeleteRows = false;
            this.dgvOpened.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgvOpened.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpened.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOpened.Location = new System.Drawing.Point(3, 3);
            this.dgvOpened.Name = "dgvOpened";
            this.dgvOpened.ReadOnly = true;
            this.dgvOpened.RowHeadersWidth = 10;
            this.dgvOpened.RowTemplate.Height = 23;
            this.dgvOpened.Size = new System.Drawing.Size(1002, 569);
            this.dgvOpened.TabIndex = 5;
            // 
            // tabBounced
            // 
            this.tabBounced.Controls.Add(this.dgvBounced);
            this.tabBounced.Location = new System.Drawing.Point(4, 25);
            this.tabBounced.Name = "tabBounced";
            this.tabBounced.Size = new System.Drawing.Size(1008, 575);
            this.tabBounced.TabIndex = 3;
            this.tabBounced.Text = "退回(Bounced)";
            this.tabBounced.UseVisualStyleBackColor = true;
            // 
            // dgvBounced
            // 
            this.dgvBounced.AllowUserToAddRows = false;
            this.dgvBounced.AllowUserToDeleteRows = false;
            this.dgvBounced.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgvBounced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBounced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBounced.Location = new System.Drawing.Point(0, 0);
            this.dgvBounced.Name = "dgvBounced";
            this.dgvBounced.ReadOnly = true;
            this.dgvBounced.RowHeadersWidth = 10;
            this.dgvBounced.RowTemplate.Height = 23;
            this.dgvBounced.Size = new System.Drawing.Size(1008, 575);
            this.dgvBounced.TabIndex = 5;
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
            // frmMailReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Name = "frmMailReport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报告查询";
            this.Load += new System.EventHandler(this.frmMailReport_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabSent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSent)).EndInit();
            this.tabOpened.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpened)).EndInit();
            this.tabBounced.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBounced)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
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
        private System.Windows.Forms.Button btnReSend;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSent;
        private System.Windows.Forms.DataGridView dgvSent;
        private System.Windows.Forms.TabPage tabOpened;
        private System.Windows.Forms.DataGridView dgvOpened;
        private System.Windows.Forms.TabPage tabBounced;
        private System.Windows.Forms.DataGridView dgvBounced;
        private System.Windows.Forms.Button btnCancel;
    }
}