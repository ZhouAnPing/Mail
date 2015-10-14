namespace ChinaUnion_Agent.Analysis
{
    partial class frmReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExportUser = new System.Windows.Forms.Button();
            this.lblUnSyncUser = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgUser = new System.Windows.Forms.DataGridView();
            this.lblUnAttentionUser = new System.Windows.Forms.LinkLabel();
            this.label16 = new System.Windows.Forms.Label();
            this.lblAttentionUser = new System.Windows.Forms.LinkLabel();
            this.lblAllUser = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grpActive = new System.Windows.Forms.GroupBox();
            this.dgWechatActiveLog = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMonthActive = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblYearActive = new System.Windows.Forms.LinkLabel();
            this.lblWeekActive = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grpUsedUser = new System.Windows.Forms.GroupBox();
            this.dgUsedUser = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblUsedMonth = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblUsedYear = new System.Windows.Forms.LinkLabel();
            this.lblUsedWeek = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtUsedEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtUsedStartTime = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.btnExportUsed = new System.Windows.Forms.Button();
            this.btnQueryUsed = new System.Windows.Forms.Button();
            this.cboTypeUsered = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.grpActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWechatActiveLog)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grpUsedUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUsedUser)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1006, 741);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(998, 715);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "微信覆盖率(备注：已关注用户）";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExportUser);
            this.groupBox2.Controls.Add(this.lblUnSyncUser);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dgUser);
            this.groupBox2.Controls.Add(this.lblUnAttentionUser);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.lblAttentionUser);
            this.groupBox2.Controls.Add(this.lblAllUser);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(992, 709);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnExportUser
            // 
            this.btnExportUser.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExportUser.Location = new System.Drawing.Point(786, 20);
            this.btnExportUser.Name = "btnExportUser";
            this.btnExportUser.Size = new System.Drawing.Size(58, 31);
            this.btnExportUser.TabIndex = 56;
            this.btnExportUser.Text = "导出";
            this.btnExportUser.UseVisualStyleBackColor = true;
            this.btnExportUser.Click += new System.EventHandler(this.btnExportUser_Click);
            // 
            // lblUnSyncUser
            // 
            this.lblUnSyncUser.AutoSize = true;
            this.lblUnSyncUser.Location = new System.Drawing.Point(665, 31);
            this.lblUnSyncUser.Name = "lblUnSyncUser";
            this.lblUnSyncUser.Size = new System.Drawing.Size(65, 12);
            this.lblUnSyncUser.TabIndex = 55;
            this.lblUnSyncUser.TabStop = true;
            this.lblUnSyncUser.Text = "linkLabel3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(569, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 54;
            this.label1.Text = "未同步成功用户:";
            // 
            // dgUser
            // 
            this.dgUser.AllowUserToAddRows = false;
            this.dgUser.AllowUserToDeleteRows = false;
            this.dgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgUser.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUser.Location = new System.Drawing.Point(3, 57);
            this.dgUser.Name = "dgUser";
            this.dgUser.ReadOnly = true;
            this.dgUser.RowHeadersWidth = 10;
            this.dgUser.RowTemplate.Height = 23;
            this.dgUser.Size = new System.Drawing.Size(986, 649);
            this.dgUser.TabIndex = 53;
            // 
            // lblUnAttentionUser
            // 
            this.lblUnAttentionUser.AutoSize = true;
            this.lblUnAttentionUser.Location = new System.Drawing.Point(449, 31);
            this.lblUnAttentionUser.Name = "lblUnAttentionUser";
            this.lblUnAttentionUser.Size = new System.Drawing.Size(65, 12);
            this.lblUnAttentionUser.TabIndex = 52;
            this.lblUnAttentionUser.TabStop = true;
            this.lblUnAttentionUser.Text = "linkLabel3";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(377, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 12);
            this.label16.TabIndex = 51;
            this.label16.Text = "未关注用户:";
            // 
            // lblAttentionUser
            // 
            this.lblAttentionUser.AutoSize = true;
            this.lblAttentionUser.Location = new System.Drawing.Point(257, 31);
            this.lblAttentionUser.Name = "lblAttentionUser";
            this.lblAttentionUser.Size = new System.Drawing.Size(65, 12);
            this.lblAttentionUser.TabIndex = 50;
            this.lblAttentionUser.TabStop = true;
            this.lblAttentionUser.Text = "linkLabel2";
            // 
            // lblAllUser
            // 
            this.lblAllUser.AutoSize = true;
            this.lblAllUser.Location = new System.Drawing.Point(65, 31);
            this.lblAllUser.Name = "lblAllUser";
            this.lblAllUser.Size = new System.Drawing.Size(65, 12);
            this.lblAllUser.TabIndex = 49;
            this.lblAllUser.TabStop = true;
            this.lblAllUser.Text = "linkLabel1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 48;
            this.label4.Text = "已关注用户:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "总用户:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grpActive);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(998, 715);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "微信活跃度";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grpActive
            // 
            this.grpActive.Controls.Add(this.dgWechatActiveLog);
            this.grpActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpActive.Location = new System.Drawing.Point(3, 103);
            this.grpActive.Name = "grpActive";
            this.grpActive.Size = new System.Drawing.Size(992, 609);
            this.grpActive.TabIndex = 70;
            this.grpActive.TabStop = false;
            this.grpActive.Text = "活跃次数";
            // 
            // dgWechatActiveLog
            // 
            this.dgWechatActiveLog.AllowUserToAddRows = false;
            this.dgWechatActiveLog.AllowUserToDeleteRows = false;
            this.dgWechatActiveLog.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgWechatActiveLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgWechatActiveLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWechatActiveLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgWechatActiveLog.Location = new System.Drawing.Point(3, 17);
            this.dgWechatActiveLog.Name = "dgWechatActiveLog";
            this.dgWechatActiveLog.ReadOnly = true;
            this.dgWechatActiveLog.RowHeadersWidth = 10;
            this.dgWechatActiveLog.RowTemplate.Height = 23;
            this.dgWechatActiveLog.Size = new System.Drawing.Size(986, 589);
            this.dgWechatActiveLog.TabIndex = 71;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMonthActive);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblYearActive);
            this.panel1.Controls.Add(this.lblWeekActive);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtEndDate);
            this.panel1.Controls.Add(this.dtStartDate);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.cboType);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 100);
            this.panel1.TabIndex = 69;
            // 
            // lblMonthActive
            // 
            this.lblMonthActive.AutoSize = true;
            this.lblMonthActive.Location = new System.Drawing.Point(518, 16);
            this.lblMonthActive.Name = "lblMonthActive";
            this.lblMonthActive.Size = new System.Drawing.Size(65, 12);
            this.lblMonthActive.TabIndex = 89;
            this.lblMonthActive.TabStop = true;
            this.lblMonthActive.Text = "linkLabel4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(426, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 88;
            this.label2.Text = "本月活跃次数:";
            // 
            // lblYearActive
            // 
            this.lblYearActive.AutoSize = true;
            this.lblYearActive.Location = new System.Drawing.Point(702, 16);
            this.lblYearActive.Name = "lblYearActive";
            this.lblYearActive.Size = new System.Drawing.Size(65, 12);
            this.lblYearActive.TabIndex = 87;
            this.lblYearActive.TabStop = true;
            this.lblYearActive.Text = "linkLabel5";
            // 
            // lblWeekActive
            // 
            this.lblWeekActive.AutoSize = true;
            this.lblWeekActive.Location = new System.Drawing.Point(320, 15);
            this.lblWeekActive.Name = "lblWeekActive";
            this.lblWeekActive.Size = new System.Drawing.Size(65, 12);
            this.lblWeekActive.TabIndex = 86;
            this.lblWeekActive.TabStop = true;
            this.lblWeekActive.Text = "linkLabel6";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(624, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 85;
            this.label3.Text = "本年活跃次数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 84;
            this.label5.Text = "本周活跃次数:";
            // 
            // dtEndDate
            // 
            this.dtEndDate.Location = new System.Drawing.Point(235, 58);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(126, 21);
            this.dtEndDate.TabIndex = 83;
            // 
            // dtStartDate
            // 
            this.dtStartDate.Location = new System.Drawing.Point(91, 59);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(126, 21);
            this.dtStartDate.TabIndex = 82;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 81;
            this.label7.Text = "时间:";
            // 
            // btnExport
            // 
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExport.Location = new System.Drawing.Point(485, 53);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(58, 32);
            this.btnExport.TabIndex = 78;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(400, 54);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(65, 32);
            this.btnQuery.TabIndex = 77;
            this.btnQuery.Text = "查询详情";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click_1);
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "业绩查询",
            "佣金结算与支付查询",
            "报错处理",
            "服务监督",
            "渠道联系人查询",
            "通知公告与促销政策",
            "投诉协查",
            "在线学习"});
            this.cboType.Location = new System.Drawing.Point(67, 13);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(141, 20);
            this.cboType.TabIndex = 80;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 12);
            this.label14.TabIndex = 79;
            this.label14.Text = "类型:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(74, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 76;
            this.label10.Text = "从";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(218, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 75;
            this.label9.Text = "到";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grpUsedUser);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(998, 715);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "微信使用率";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // grpUsedUser
            // 
            this.grpUsedUser.Controls.Add(this.dgUsedUser);
            this.grpUsedUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUsedUser.Location = new System.Drawing.Point(3, 103);
            this.grpUsedUser.Name = "grpUsedUser";
            this.grpUsedUser.Size = new System.Drawing.Size(992, 609);
            this.grpUsedUser.TabIndex = 72;
            this.grpUsedUser.TabStop = false;
            this.grpUsedUser.Text = "用户数";
            // 
            // dgUsedUser
            // 
            this.dgUsedUser.AllowUserToAddRows = false;
            this.dgUsedUser.AllowUserToDeleteRows = false;
            this.dgUsedUser.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgUsedUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgUsedUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUsedUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUsedUser.Location = new System.Drawing.Point(3, 17);
            this.dgUsedUser.Name = "dgUsedUser";
            this.dgUsedUser.ReadOnly = true;
            this.dgUsedUser.RowHeadersWidth = 10;
            this.dgUsedUser.RowTemplate.Height = 23;
            this.dgUsedUser.Size = new System.Drawing.Size(986, 589);
            this.dgUsedUser.TabIndex = 71;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblUsedMonth);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lblUsedYear);
            this.panel2.Controls.Add(this.lblUsedWeek);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.dtUsedEndTime);
            this.panel2.Controls.Add(this.dtUsedStartTime);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.btnExportUsed);
            this.panel2.Controls.Add(this.btnQueryUsed);
            this.panel2.Controls.Add(this.cboTypeUsered);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 100);
            this.panel2.TabIndex = 71;
            // 
            // lblUsedMonth
            // 
            this.lblUsedMonth.AutoSize = true;
            this.lblUsedMonth.Location = new System.Drawing.Point(518, 16);
            this.lblUsedMonth.Name = "lblUsedMonth";
            this.lblUsedMonth.Size = new System.Drawing.Size(65, 12);
            this.lblUsedMonth.TabIndex = 89;
            this.lblUsedMonth.TabStop = true;
            this.lblUsedMonth.Text = "linkLabel4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(426, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 88;
            this.label6.Text = "本月使用用户:";
            // 
            // lblUsedYear
            // 
            this.lblUsedYear.AutoSize = true;
            this.lblUsedYear.Location = new System.Drawing.Point(702, 16);
            this.lblUsedYear.Name = "lblUsedYear";
            this.lblUsedYear.Size = new System.Drawing.Size(65, 12);
            this.lblUsedYear.TabIndex = 87;
            this.lblUsedYear.TabStop = true;
            this.lblUsedYear.Text = "linkLabel5";
            // 
            // lblUsedWeek
            // 
            this.lblUsedWeek.AutoSize = true;
            this.lblUsedWeek.Location = new System.Drawing.Point(320, 15);
            this.lblUsedWeek.Name = "lblUsedWeek";
            this.lblUsedWeek.Size = new System.Drawing.Size(65, 12);
            this.lblUsedWeek.TabIndex = 86;
            this.lblUsedWeek.TabStop = true;
            this.lblUsedWeek.Text = "linkLabel6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(624, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 12);
            this.label11.TabIndex = 85;
            this.label11.Text = "本年使用用户:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(228, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 12);
            this.label12.TabIndex = 84;
            this.label12.Text = "本周使用用户:";
            // 
            // dtUsedEndTime
            // 
            this.dtUsedEndTime.Location = new System.Drawing.Point(235, 58);
            this.dtUsedEndTime.Name = "dtUsedEndTime";
            this.dtUsedEndTime.Size = new System.Drawing.Size(126, 21);
            this.dtUsedEndTime.TabIndex = 83;
            // 
            // dtUsedStartTime
            // 
            this.dtUsedStartTime.Location = new System.Drawing.Point(91, 59);
            this.dtUsedStartTime.Name = "dtUsedStartTime";
            this.dtUsedStartTime.Size = new System.Drawing.Size(126, 21);
            this.dtUsedStartTime.TabIndex = 82;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 81;
            this.label13.Text = "时间:";
            // 
            // btnExportUsed
            // 
            this.btnExportUsed.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExportUsed.Location = new System.Drawing.Point(485, 53);
            this.btnExportUsed.Name = "btnExportUsed";
            this.btnExportUsed.Size = new System.Drawing.Size(58, 32);
            this.btnExportUsed.TabIndex = 78;
            this.btnExportUsed.Text = "导出";
            this.btnExportUsed.UseVisualStyleBackColor = true;
            this.btnExportUsed.Click += new System.EventHandler(this.btnExportUsed_Click);
            // 
            // btnQueryUsed
            // 
            this.btnQueryUsed.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQueryUsed.Location = new System.Drawing.Point(400, 54);
            this.btnQueryUsed.Name = "btnQueryUsed";
            this.btnQueryUsed.Size = new System.Drawing.Size(67, 32);
            this.btnQueryUsed.TabIndex = 77;
            this.btnQueryUsed.Text = "查询详情";
            this.btnQueryUsed.UseVisualStyleBackColor = true;
            this.btnQueryUsed.Click += new System.EventHandler(this.btnQueryUsed_Click);
            // 
            // cboTypeUsered
            // 
            this.cboTypeUsered.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeUsered.FormattingEnabled = true;
            this.cboTypeUsered.Items.AddRange(new object[] {
            "业绩查询",
            "佣金结算与支付查询",
            "报错处理",
            "服务监督",
            "渠道联系人查询",
            "通知公告与促销政策",
            "投诉协查",
            "在线学习"});
            this.cboTypeUsered.Location = new System.Drawing.Point(67, 13);
            this.cboTypeUsered.Name = "cboTypeUsered";
            this.cboTypeUsered.Size = new System.Drawing.Size(141, 20);
            this.cboTypeUsered.TabIndex = 80;
            this.cboTypeUsered.SelectedIndexChanged += new System.EventHandler(this.cboTypeUsered_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(27, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 12);
            this.label15.TabIndex = 79;
            this.label15.Text = "类型:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(74, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 76;
            this.label17.Text = "从";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(218, 63);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 12);
            this.label18.TabIndex = 75;
            this.label18.Text = "到";
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 741);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmReport";
            this.Text = "分析报告";
            this.Load += new System.EventHandler(this.frmPolicyPublish_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.grpActive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWechatActiveLog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.grpUsedUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUsedUser)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgUser;
        private System.Windows.Forms.LinkLabel lblUnAttentionUser;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.LinkLabel lblAttentionUser;
        private System.Windows.Forms.LinkLabel lblAllUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.LinkLabel lblUnSyncUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lblMonthActive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblYearActive;
        private System.Windows.Forms.LinkLabel lblWeekActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpActive;
        private System.Windows.Forms.DataGridView dgWechatActiveLog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox grpUsedUser;
        private System.Windows.Forms.DataGridView dgUsedUser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel lblUsedMonth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lblUsedYear;
        private System.Windows.Forms.LinkLabel lblUsedWeek;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtUsedEndTime;
        private System.Windows.Forms.DateTimePicker dtUsedStartTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnExportUsed;
        private System.Windows.Forms.Button btnQueryUsed;
        private System.Windows.Forms.ComboBox cboTypeUsered;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
    }
}