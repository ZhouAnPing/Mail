namespace ChinaUnion_Agent.OnlineExamForm
{
    partial class frmNewExam
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgExamSingleChoice = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgExamMultiChoice = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgExamJugement = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lstGroup = new System.Windows.Forms.CheckedListBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.cboAgentType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.pnlReceiver = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstAgentType = new System.Windows.Forms.CheckedListBox();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtExamName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExamSingleChoice)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExamMultiChoice)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExamJugement)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.pnlReceiver.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 539);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1060, 56);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(187, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 31);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(316, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.cboAgentType);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dtEndDate);
            this.groupBox1.Controls.Add(this.pnlReceiver);
            this.groupBox1.Controls.Add(this.dtStartDate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtExamName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1060, 539);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl1.Location = new System.Drawing.Point(437, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(620, 519);
            this.tabControl1.TabIndex = 51;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgExamSingleChoice);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 493);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "单选题";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgExamSingleChoice
            // 
            this.dgExamSingleChoice.AllowUserToAddRows = false;
            this.dgExamSingleChoice.AllowUserToDeleteRows = false;
            this.dgExamSingleChoice.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgExamSingleChoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExamSingleChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgExamSingleChoice.Location = new System.Drawing.Point(3, 3);
            this.dgExamSingleChoice.Name = "dgExamSingleChoice";
            this.dgExamSingleChoice.ReadOnly = true;
            this.dgExamSingleChoice.RowHeadersWidth = 10;
            this.dgExamSingleChoice.RowTemplate.Height = 23;
            this.dgExamSingleChoice.Size = new System.Drawing.Size(606, 487);
            this.dgExamSingleChoice.TabIndex = 49;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgExamMultiChoice);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 493);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "多选题";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgExamMultiChoice
            // 
            this.dgExamMultiChoice.AllowUserToAddRows = false;
            this.dgExamMultiChoice.AllowUserToDeleteRows = false;
            this.dgExamMultiChoice.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgExamMultiChoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExamMultiChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgExamMultiChoice.Location = new System.Drawing.Point(3, 3);
            this.dgExamMultiChoice.Name = "dgExamMultiChoice";
            this.dgExamMultiChoice.ReadOnly = true;
            this.dgExamMultiChoice.RowHeadersWidth = 10;
            this.dgExamMultiChoice.RowTemplate.Height = 23;
            this.dgExamMultiChoice.Size = new System.Drawing.Size(606, 487);
            this.dgExamMultiChoice.TabIndex = 50;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgExamJugement);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(612, 493);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "判断题";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgExamJugement
            // 
            this.dgExamJugement.AllowUserToAddRows = false;
            this.dgExamJugement.AllowUserToDeleteRows = false;
            this.dgExamJugement.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgExamJugement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExamJugement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgExamJugement.Location = new System.Drawing.Point(3, 3);
            this.dgExamJugement.Name = "dgExamJugement";
            this.dgExamJugement.ReadOnly = true;
            this.dgExamJugement.RowHeadersWidth = 10;
            this.dgExamJugement.RowTemplate.Height = 23;
            this.dgExamJugement.Size = new System.Drawing.Size(606, 487);
            this.dgExamJugement.TabIndex = 50;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lstGroup);
            this.groupBox5.Location = new System.Drawing.Point(84, 336);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(270, 156);
            this.groupBox5.TabIndex = 50;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "自定义组";
            // 
            // lstGroup
            // 
            this.lstGroup.CheckOnClick = true;
            this.lstGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstGroup.FormattingEnabled = true;
            this.lstGroup.Items.AddRange(new object[] {
            "3G专营店",
            "O2O社会直销渠道",
            "上网卡社会直销渠道",
            "互联网社会渠道",
            "公众直销渠道",
            "其他他建他营厅",
            "其他渠道",
            "其他电子社会渠道",
            "其它开户点",
            "其它电子社会直销渠道",
            "其它社会直销渠道",
            "华盛专营店",
            "卡批社会直销渠道",
            "小区社会直销渠道",
            "小型营业厅",
            "平台商",
            "平台特约店",
            "战略合作厅",
            "旗舰营业厅",
            "普通代理店",
            "普通代理点",
            "标准营业厅",
            "校园/小区代理点",
            "校园代理点",
            "校园厅",
            "校园开户点",
            "校园社会直销渠道",
            "特约店",
            "直供代理点",
            "直供点",
            "社区便利店",
            "网上营业厅",
            "自建他营厅",
            "连锁渠道他建他营厅",
            "连锁渠道代理点",
            "集团客户社会直销渠道",
            "集团直销渠道",
            "集客固网社会直销渠道",
            "集客移网社会直销渠道",
            "非连锁渠道"});
            this.lstGroup.Location = new System.Drawing.Point(3, 17);
            this.lstGroup.Name = "lstGroup";
            this.lstGroup.Size = new System.Drawing.Size(264, 136);
            this.lstGroup.TabIndex = 37;
            // 
            // btnImport
            // 
            this.btnImport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnImport.Location = new System.Drawing.Point(302, 14);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 31);
            this.btnImport.TabIndex = 49;
            this.btnImport.Text = "上传考题》";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cboAgentType
            // 
            this.cboAgentType.FormattingEnabled = true;
            this.cboAgentType.Items.AddRange(new object[] {
            "",
            "代理商联系人",
            "直供渠道联系人",
            "非直供渠道联系人"});
            this.cboAgentType.Location = new System.Drawing.Point(86, 120);
            this.cboAgentType.Name = "cboAgentType";
            this.cboAgentType.Size = new System.Drawing.Size(268, 20);
            this.cboAgentType.TabIndex = 47;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 46;
            this.label11.Text = "发布对象:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(66, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 45;
            this.label10.Text = "从";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(210, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 44;
            this.label9.Text = "到";
            // 
            // dtEndDate
            // 
            this.dtEndDate.Location = new System.Drawing.Point(228, 66);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(126, 21);
            this.dtEndDate.TabIndex = 43;
            // 
            // pnlReceiver
            // 
            this.pnlReceiver.Controls.Add(this.groupBox4);
            this.pnlReceiver.Location = new System.Drawing.Point(87, 161);
            this.pnlReceiver.Name = "pnlReceiver";
            this.pnlReceiver.Size = new System.Drawing.Size(267, 156);
            this.pnlReceiver.TabIndex = 42;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lstAgentType);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(267, 156);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "渠道类型";
            // 
            // lstAgentType
            // 
            this.lstAgentType.CheckOnClick = true;
            this.lstAgentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAgentType.FormattingEnabled = true;
            this.lstAgentType.Items.AddRange(new object[] {
            "3G专营店",
            "O2O社会直销渠道",
            "上网卡社会直销渠道",
            "互联网社会渠道",
            "公众直销渠道",
            "其他他建他营厅",
            "其他渠道",
            "其他电子社会渠道",
            "其它开户点",
            "其它电子社会直销渠道",
            "其它社会直销渠道",
            "华盛专营店",
            "卡批社会直销渠道",
            "小区社会直销渠道",
            "小型营业厅",
            "平台商",
            "平台特约店",
            "战略合作厅",
            "旗舰营业厅",
            "普通代理店",
            "普通代理点",
            "标准营业厅",
            "校园/小区代理点",
            "校园代理点",
            "校园厅",
            "校园开户点",
            "校园社会直销渠道",
            "特约店",
            "直供代理点",
            "直供点",
            "社区便利店",
            "网上营业厅",
            "自建他营厅",
            "连锁渠道他建他营厅",
            "连锁渠道代理点",
            "集团客户社会直销渠道",
            "集团直销渠道",
            "集客固网社会直销渠道",
            "集客移网社会直销渠道",
            "非连锁渠道"});
            this.lstAgentType.Location = new System.Drawing.Point(3, 17);
            this.lstAgentType.Name = "lstAgentType";
            this.lstAgentType.Size = new System.Drawing.Size(261, 136);
            this.lstAgentType.TabIndex = 37;
            // 
            // dtStartDate
            // 
            this.dtStartDate.Location = new System.Drawing.Point(84, 67);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(126, 21);
            this.dtStartDate.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 40;
            this.label7.Text = "有效期:";
            // 
            // txtExamName
            // 
            this.txtExamName.Location = new System.Drawing.Point(84, 14);
            this.txtExamName.Name = "txtExamName";
            this.txtExamName.Size = new System.Drawing.Size(206, 21);
            this.txtExamName.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // frmNewExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 595);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmNewExam";
            this.Text = "考试发布";
            this.Load += new System.EventHandler(this.frmNewExam_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgExamSingleChoice)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgExamMultiChoice)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgExamJugement)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.pnlReceiver.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExamName;
        private System.Windows.Forms.ComboBox cboAgentType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.Panel pnlReceiver;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox lstAgentType;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox lstGroup;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgExamSingleChoice;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgExamMultiChoice;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgExamJugement;
    }
}