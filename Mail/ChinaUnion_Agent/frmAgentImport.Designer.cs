namespace ChinaUnion_Agent
{
    partial class frmAgentImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgentImport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgAgentFee = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgAgent = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgAgentTypeComment = new System.Windows.Forms.DataGridView();
            this.dgAgentType = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitter2 = new BSE.Windows.Forms.Splitter();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.panel1 = new BSE.Windows.Forms.Panel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.dtFeeMonth = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtAgent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentFee)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgent)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentTypeComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentType)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgAgentFee);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 251);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "月度佣金明细信息";
            // 
            // dgAgentFee
            // 
            this.dgAgentFee.AllowUserToAddRows = false;
            this.dgAgentFee.AllowUserToDeleteRows = false;
            this.dgAgentFee.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgAgentFee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAgentFee.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgAgentFee.Location = new System.Drawing.Point(3, 17);
            this.dgAgentFee.Name = "dgAgentFee";
            this.dgAgentFee.ReadOnly = true;
            this.dgAgentFee.RowHeadersWidth = 10;
            this.dgAgentFee.RowTemplate.Height = 23;
            this.dgAgentFee.Size = new System.Drawing.Size(1010, 231);
            this.dgAgentFee.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 312);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 429);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgAgent);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1008, 400);
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
            this.dgAgent.Size = new System.Drawing.Size(1002, 394);
            this.dgAgent.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgAgentTypeComment);
            this.tabPage2.Controls.Add(this.dgAgentType);
            this.tabPage2.Controls.Add(this.splitter2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1008, 400);
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
            this.dgAgentTypeComment.Size = new System.Drawing.Size(473, 394);
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
            this.dgAgentType.Size = new System.Drawing.Size(526, 394);
            this.dgAgentType.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(929, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Transparent;
            this.splitter2.Location = new System.Drawing.Point(3, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 394);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Transparent;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 309);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1016, 3);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AssociatedSplitter = null;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.CaptionFont = new System.Drawing.Font("SimSun", 11.75F, System.Drawing.FontStyle.Bold);
            this.panel1.CaptionHeight = 27;
            this.panel1.Controls.Add(this.btnDownload);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.dtFeeMonth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnImport);
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.txtAgent);
            this.panel1.Controls.Add(this.label2);
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
            this.panel1.TabIndex = 0;
            this.panel1.Text = "代理商导入";
            this.panel1.ToolTipTextCloseIcon = null;
            this.panel1.ToolTipTextExpandIconPanelCollapsed = null;
            this.panel1.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // btnDownload
            // 
            this.btnDownload.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDownload.Location = new System.Drawing.Point(844, 11);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 31);
            this.btnDownload.TabIndex = 14;
            this.btnDownload.Text = "模板下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // dtFeeMonth
            // 
            this.dtFeeMonth.CustomFormat = "yyyy-MM";
            this.dtFeeMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFeeMonth.Location = new System.Drawing.Point(613, 20);
            this.dtFeeMonth.Name = "dtFeeMonth";
            this.dtFeeMonth.Size = new System.Drawing.Size(113, 21);
            this.dtFeeMonth.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(542, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "佣金月份：";
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImport.Location = new System.Drawing.Point(754, 11);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 31);
            this.btnImport.TabIndex = 9;
            this.btnImport.Text = "上传";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Location = new System.Drawing.Point(478, 13);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(55, 28);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "...";
            this.btnSelect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtAgent
            // 
            this.txtAgent.Location = new System.Drawing.Point(115, 19);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Size = new System.Drawing.Size(360, 21);
            this.txtAgent.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "代理商文件地址：";
            // 
            // frmAgentImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAgentImport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "佣金导入";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAgentImport_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentFee)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgent)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentTypeComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentType)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAgent;
        private System.Windows.Forms.Button btnSelect;
        private BSE.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgAgentFee;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgAgent;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgAgentTypeComment;
        private BSE.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.DataGridView dgAgentType;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFeeMonth;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDownload;

    }
}