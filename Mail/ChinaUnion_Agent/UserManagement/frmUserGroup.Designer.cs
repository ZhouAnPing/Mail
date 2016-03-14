namespace ChinaUnion_Agent.UserManagement
{
    partial class frmUserGroup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgGroup = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtAgent = new System.Windows.Forms.TextBox();
            this.lstAssignAgent = new System.Windows.Forms.ListBox();
            this.lstAllAgent = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lstAssignType = new System.Windows.Forms.ListBox();
            this.lstAllType = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRightAll = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeftAll = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lstUser = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstAgentType = new System.Windows.Forms.CheckedListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroup)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.txtKeyword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 60);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // btnImport
            // 
            this.btnImport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnImport.Location = new System.Drawing.Point(508, 20);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(72, 31);
            this.btnImport.TabIndex = 21;
            this.btnImport.Text = "导入组";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(74, 26);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(206, 21);
            this.txtKeyword.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "组名:";
            // 
            // btnNew
            // 
            this.btnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNew.Location = new System.Drawing.Point(409, 20);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(72, 31);
            this.btnNew.TabIndex = 17;
            this.btnNew.Text = "新建组";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(607, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 31);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(310, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(72, 31);
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgGroup);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 681);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "组列表";
            // 
            // dgGroup
            // 
            this.dgGroup.AllowUserToAddRows = false;
            this.dgGroup.AllowUserToDeleteRows = false;
            this.dgGroup.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgGroup.Location = new System.Drawing.Point(3, 17);
            this.dgGroup.Name = "dgGroup";
            this.dgGroup.ReadOnly = true;
            this.dgGroup.RowHeadersWidth = 10;
            this.dgGroup.RowTemplate.Height = 23;
            this.dgGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgGroup.Size = new System.Drawing.Size(314, 661);
            this.dgGroup.TabIndex = 7;
            this.dgGroup.SelectionChanged += new System.EventHandler(this.dgGroup_SelectionChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.txtDescription);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtGroupName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(320, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(696, 681);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExport.Location = new System.Drawing.Point(76, 456);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 31);
            this.btnExport.TabIndex = 43;
            this.btnExport.Text = "导出代理商";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtAgent);
            this.groupBox7.Controls.Add(this.lstAssignAgent);
            this.groupBox7.Controls.Add(this.lstAllAgent);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.button1);
            this.groupBox7.Controls.Add(this.button2);
            this.groupBox7.Controls.Add(this.button3);
            this.groupBox7.Controls.Add(this.button4);
            this.groupBox7.Location = new System.Drawing.Point(51, 264);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(573, 186);
            this.groupBox7.TabIndex = 42;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "代理商";
            // 
            // txtAgent
            // 
            this.txtAgent.Location = new System.Drawing.Point(66, 17);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Size = new System.Drawing.Size(159, 21);
            this.txtAgent.TabIndex = 30;
            this.txtAgent.TextChanged += new System.EventHandler(this.txtAgent_TextChanged);
            // 
            // lstAssignAgent
            // 
            this.lstAssignAgent.FormattingEnabled = true;
            this.lstAssignAgent.ItemHeight = 12;
            this.lstAssignAgent.Location = new System.Drawing.Point(350, 40);
            this.lstAssignAgent.Name = "lstAssignAgent";
            this.lstAssignAgent.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstAssignAgent.Size = new System.Drawing.Size(216, 136);
            this.lstAssignAgent.TabIndex = 29;
            this.lstAssignAgent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAssignAgent_MouseDoubleClick);
            // 
            // lstAllAgent
            // 
            this.lstAllAgent.FormattingEnabled = true;
            this.lstAllAgent.ItemHeight = 12;
            this.lstAllAgent.Location = new System.Drawing.Point(8, 40);
            this.lstAllAgent.Name = "lstAllAgent";
            this.lstAllAgent.Size = new System.Drawing.Size(217, 136);
            this.lstAllAgent.TabIndex = 28;
            this.lstAllAgent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAllAgent_MouseDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "所有：";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(252, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 19);
            this.button1.TabIndex = 25;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(252, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 19);
            this.button2.TabIndex = 24;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(252, 153);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 19);
            this.button3.TabIndex = 23;
            this.button3.Text = "<<";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button4.Location = new System.Drawing.Point(252, 118);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 19);
            this.button4.TabIndex = 22;
            this.button4.Text = "<";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtType);
            this.groupBox6.Controls.Add(this.lstAssignType);
            this.groupBox6.Controls.Add(this.lstAllType);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.btnRightAll);
            this.groupBox6.Controls.Add(this.btnRight);
            this.groupBox6.Controls.Add(this.btnLeftAll);
            this.groupBox6.Controls.Add(this.btnLeft);
            this.groupBox6.Location = new System.Drawing.Point(49, 78);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(573, 186);
            this.groupBox6.TabIndex = 41;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "渠道类型";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(66, 17);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(159, 21);
            this.txtType.TabIndex = 30;
            this.txtType.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // lstAssignType
            // 
            this.lstAssignType.FormattingEnabled = true;
            this.lstAssignType.ItemHeight = 12;
            this.lstAssignType.Location = new System.Drawing.Point(350, 40);
            this.lstAssignType.Name = "lstAssignType";
            this.lstAssignType.Size = new System.Drawing.Size(216, 136);
            this.lstAssignType.TabIndex = 29;
            this.lstAssignType.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAssignType_MouseDoubleClick);
            // 
            // lstAllType
            // 
            this.lstAllType.FormattingEnabled = true;
            this.lstAllType.ItemHeight = 12;
            this.lstAllType.Location = new System.Drawing.Point(8, 40);
            this.lstAllType.Name = "lstAllType";
            this.lstAllType.Size = new System.Drawing.Size(217, 136);
            this.lstAllType.TabIndex = 28;
            this.lstAllType.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAllType_MouseDoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "所有：";
            // 
            // btnRightAll
            // 
            this.btnRightAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRightAll.Location = new System.Drawing.Point(252, 83);
            this.btnRightAll.Name = "btnRightAll";
            this.btnRightAll.Size = new System.Drawing.Size(75, 19);
            this.btnRightAll.TabIndex = 25;
            this.btnRightAll.Text = ">>";
            this.btnRightAll.UseVisualStyleBackColor = true;
            this.btnRightAll.Click += new System.EventHandler(this.btnRightAll_Click);
            // 
            // btnRight
            // 
            this.btnRight.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRight.Location = new System.Drawing.Point(252, 48);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 19);
            this.btnRight.TabIndex = 24;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeftAll
            // 
            this.btnLeftAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLeftAll.Location = new System.Drawing.Point(252, 153);
            this.btnLeftAll.Name = "btnLeftAll";
            this.btnLeftAll.Size = new System.Drawing.Size(75, 19);
            this.btnLeftAll.TabIndex = 23;
            this.btnLeftAll.Text = "<<";
            this.btnLeftAll.UseVisualStyleBackColor = true;
            this.btnLeftAll.Click += new System.EventHandler(this.btnLeftAll_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLeft.Location = new System.Drawing.Point(252, 118);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(75, 19);
            this.btnLeft.TabIndex = 22;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lstUser);
            this.groupBox5.Location = new System.Drawing.Point(59, 237);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(191, 146);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "代理商或者渠道";
            // 
            // lstUser
            // 
            this.lstUser.CheckOnClick = true;
            this.lstUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUser.FormattingEnabled = true;
            this.lstUser.Items.AddRange(new object[] {
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
            this.lstUser.Location = new System.Drawing.Point(3, 17);
            this.lstUser.Name = "lstUser";
            this.lstUser.Size = new System.Drawing.Size(185, 126);
            this.lstUser.TabIndex = 37;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lstAgentType);
            this.groupBox4.Location = new System.Drawing.Point(59, 78);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(191, 156);
            this.groupBox4.TabIndex = 39;
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
            this.lstAgentType.Size = new System.Drawing.Size(185, 136);
            this.lstAgentType.TabIndex = 37;
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.Location = new System.Drawing.Point(284, 456);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 31);
            this.btnDelete.TabIndex = 26;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(59, 50);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(421, 21);
            this.txtDescription.TabIndex = 22;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(180, 456);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 31);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "描述:";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(59, 17);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(206, 21);
            this.txtGroupName.TabIndex = 1;
            this.txtGroupName.TextChanged += new System.EventHandler(this.txtGroupName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "组名:";
            // 
            // frmUserGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmUserGroup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "接收群组管理";
            this.Load += new System.EventHandler(this.frmGroupManagement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgGroup)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgGroup;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.CheckedListBox lstAgentType;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox lstUser;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRightAll;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeftAll;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.ListBox lstAssignType;
        private System.Windows.Forms.ListBox lstAllType;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtAgent;
        private System.Windows.Forms.ListBox lstAssignAgent;
        private System.Windows.Forms.ListBox lstAllAgent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;

    }
}