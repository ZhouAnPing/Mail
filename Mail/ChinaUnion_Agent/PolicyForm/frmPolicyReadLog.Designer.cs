namespace ChinaUnion_Agent.PolicyForm
{
    partial class frmPolicyReadLog
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
            this.grpAgentFee = new System.Windows.Forms.GroupBox();
            this.dgPolicyReadLog = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new BSE.Windows.Forms.Panel();
            this.txtUserKeyword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSubjectKeyword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.dtDay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.grpAgentFee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPolicyReadLog)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAgentFee
            // 
            this.grpAgentFee.Controls.Add(this.dgPolicyReadLog);
            this.grpAgentFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAgentFee.Location = new System.Drawing.Point(0, 58);
            this.grpAgentFee.Name = "grpAgentFee";
            this.grpAgentFee.Size = new System.Drawing.Size(1016, 683);
            this.grpAgentFee.TabIndex = 7;
            this.grpAgentFee.TabStop = false;
            this.grpAgentFee.Text = "日志信息";
            // 
            // dgPolicyReadLog
            // 
            this.dgPolicyReadLog.AllowUserToAddRows = false;
            this.dgPolicyReadLog.AllowUserToDeleteRows = false;
            this.dgPolicyReadLog.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgPolicyReadLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPolicyReadLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPolicyReadLog.Location = new System.Drawing.Point(3, 17);
            this.dgPolicyReadLog.Name = "dgPolicyReadLog";
            this.dgPolicyReadLog.ReadOnly = true;
            this.dgPolicyReadLog.RowHeadersWidth = 10;
            this.dgPolicyReadLog.RowTemplate.Height = 23;
            this.dgPolicyReadLog.Size = new System.Drawing.Size(1010, 663);
            this.dgPolicyReadLog.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(838, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 35);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.AssociatedSplitter = null;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.CaptionFont = new System.Drawing.Font("SimSun", 11.75F, System.Drawing.FontStyle.Bold);
            this.panel1.CaptionHeight = 27;
            this.panel1.Controls.Add(this.txtUserKeyword);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSubjectKeyword);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.dtDay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.panel1.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.panel1.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.panel1.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(244)))), ((int)(((byte)(242)))));
            this.panel1.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.panel1.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(232)))), ((int)(((byte)(228)))));
            this.panel1.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.panel1.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.panel1.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.panel1.CustomColors.CollapsedCaptionText = System.Drawing.SystemColors.ControlText;
            this.panel1.CustomColors.ContentGradientBegin = System.Drawing.SystemColors.ButtonFace;
            this.panel1.CustomColors.ContentGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panel1.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Image = null;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(27, 27);
            this.panel1.Name = "panel1";
            this.panel1.ShowCaptionbar = false;
            this.panel1.Size = new System.Drawing.Size(1016, 58);
            this.panel1.TabIndex = 1;
            this.panel1.Text = "代理商导入";
            this.panel1.ToolTipTextCloseIcon = null;
            this.panel1.ToolTipTextExpandIconPanelCollapsed = null;
            this.panel1.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // txtUserKeyword
            // 
            this.txtUserKeyword.Location = new System.Drawing.Point(311, 17);
            this.txtUserKeyword.Name = "txtUserKeyword";
            this.txtUserKeyword.Size = new System.Drawing.Size(128, 21);
            this.txtUserKeyword.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 24);
            this.label3.TabIndex = 24;
            this.label3.Text = "用户/渠道\r\n/代理商：";
            // 
            // txtSubjectKeyword
            // 
            this.txtSubjectKeyword.Location = new System.Drawing.Point(74, 17);
            this.txtSubjectKeyword.Name = "txtSubjectKeyword";
            this.txtSubjectKeyword.Size = new System.Drawing.Size(152, 21);
            this.txtSubjectKeyword.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "信息关键字：";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(749, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 35);
            this.btnExport.TabIndex = 21;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dtDay
            // 
            this.dtDay.CustomFormat = "yyyy-MM-dd";
            this.dtDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDay.Location = new System.Drawing.Point(510, 17);
            this.dtDay.Name = "dtDay";
            this.dtDay.Size = new System.Drawing.Size(82, 21);
            this.dtDay.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "阅读日期：";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(653, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 35);
            this.btnQuery.TabIndex = 9;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // frmPolicyReadLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.grpAgentFee);
            this.Controls.Add(this.panel1);
            this.Name = "frmPolicyReadLog";
            this.ShowIcon = false;
            this.Text = "信息阅读日志";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAgentQuery_Load);
            this.grpAgentFee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPolicyReadLog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BSE.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox grpAgentFee;
        private System.Windows.Forms.DataGridView dgPolicyReadLog;
        private System.Windows.Forms.DateTimePicker dtDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserKeyword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSubjectKeyword;
    }
}