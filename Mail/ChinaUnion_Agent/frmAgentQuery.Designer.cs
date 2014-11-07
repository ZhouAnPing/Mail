namespace ChinaUnion_Agent
{
    partial class frmAgentQuery
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
            this.panel1 = new BSE.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.dtFeeMonth = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgAgentFee = new System.Windows.Forms.DataGridView();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgAgentTypeComment = new System.Windows.Forms.DataGridView();
            this.dgAgentType = new System.Windows.Forms.DataGridView();
            this.splitter2 = new BSE.Windows.Forms.Splitter();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgAgent = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentFee)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentTypeComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentType)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgent)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AssociatedSplitter = null;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.CaptionFont = new System.Drawing.Font("SimSun", 11.75F, System.Drawing.FontStyle.Bold);
            this.panel1.CaptionHeight = 27;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnMail);
            this.panel1.Controls.Add(this.dtFeeMonth);
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(530, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 35);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMail
            // 
            this.btnMail.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMail.Location = new System.Drawing.Point(387, 10);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(70, 35);
            this.btnMail.TabIndex = 10;
            this.btnMail.Text = "邮件发送";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Visible = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // dtFeeMonth
            // 
            this.dtFeeMonth.CustomFormat = "yyyy-MM";
            this.dtFeeMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFeeMonth.Location = new System.Drawing.Point(108, 21);
            this.dtFeeMonth.Name = "dtFeeMonth";
            this.dtFeeMonth.Size = new System.Drawing.Size(113, 21);
            this.dtFeeMonth.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "佣金月份：";
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(244, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 35);
            this.btnQuery.TabIndex = 9;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgAgentFee);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 396);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "月度佣金明细信息";
            // 
            // dgAgentFee
            // 
            this.dgAgentFee.AllowUserToAddRows = false;
            this.dgAgentFee.AllowUserToDeleteRows = false;
            this.dgAgentFee.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgAgentFee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAgentFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAgentFee.Location = new System.Drawing.Point(3, 17);
            this.dgAgentFee.Name = "dgAgentFee";
            this.dgAgentFee.ReadOnly = true;
            this.dgAgentFee.RowHeadersWidth = 10;
            this.dgAgentFee.RowTemplate.Height = 23;
            this.dgAgentFee.Size = new System.Drawing.Size(1010, 376);
            this.dgAgentFee.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Transparent;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 454);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1016, 3);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgAgentTypeComment);
            this.tabPage2.Controls.Add(this.dgAgentType);
            this.tabPage2.Controls.Add(this.splitter2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1008, 255);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "代理商类型";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgAgentTypeComment
            // 
            this.dgAgentTypeComment.AllowUserToAddRows = false;
            this.dgAgentTypeComment.AllowUserToDeleteRows = false;
            this.dgAgentTypeComment.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgAgentTypeComment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAgentTypeComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAgentTypeComment.Location = new System.Drawing.Point(532, 3);
            this.dgAgentTypeComment.Name = "dgAgentTypeComment";
            this.dgAgentTypeComment.ReadOnly = true;
            this.dgAgentTypeComment.RowHeadersWidth = 10;
            this.dgAgentTypeComment.RowTemplate.Height = 23;
            this.dgAgentTypeComment.Size = new System.Drawing.Size(473, 249);
            this.dgAgentTypeComment.TabIndex = 8;
            // 
            // dgAgentType
            // 
            this.dgAgentType.AllowUserToAddRows = false;
            this.dgAgentType.AllowUserToDeleteRows = false;
            this.dgAgentType.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgAgentType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAgentType.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgAgentType.Location = new System.Drawing.Point(6, 3);
            this.dgAgentType.Name = "dgAgentType";
            this.dgAgentType.ReadOnly = true;
            this.dgAgentType.RowHeadersWidth = 10;
            this.dgAgentType.RowTemplate.Height = 23;
            this.dgAgentType.Size = new System.Drawing.Size(526, 249);
            this.dgAgentType.TabIndex = 6;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Transparent;
            this.splitter2.Location = new System.Drawing.Point(3, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 249);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgAgent);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1008, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "代理商信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgAgent
            // 
            this.dgAgent.AllowUserToAddRows = false;
            this.dgAgent.AllowUserToDeleteRows = false;
            this.dgAgent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgAgent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAgent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAgent.Location = new System.Drawing.Point(3, 3);
            this.dgAgent.Name = "dgAgent";
            this.dgAgent.ReadOnly = true;
            this.dgAgent.RowHeadersWidth = 10;
            this.dgAgent.RowTemplate.Height = 23;
            this.dgAgent.Size = new System.Drawing.Size(1002, 249);
            this.dgAgent.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 457);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 284);
            this.tabControl1.TabIndex = 9;
            // 
            // frmAgentQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "frmAgentQuery";
            this.ShowIcon = false;
            this.Text = "代理商佣金查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAgentQuery_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentFee)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentTypeComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentType)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgent)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BSE.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgAgentFee;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.DateTimePicker dtFeeMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgAgentTypeComment;
        private System.Windows.Forms.DataGridView dgAgentType;
        private BSE.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgAgent;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnCancel;
    }
}