namespace ChinaUnion_Agent.MasterDataForm
{
    partial class frmWechatQuery
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
            this.grpWechatList = new System.Windows.Forms.GroupBox();
            this.dgAgentWechatAccount = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.checkUnsync = new System.Windows.Forms.CheckBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.grpWechatList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentWechatAccount)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpWechatList
            // 
            this.grpWechatList.Controls.Add(this.dgAgentWechatAccount);
            this.grpWechatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWechatList.Location = new System.Drawing.Point(0, 55);
            this.grpWechatList.Name = "grpWechatList";
            this.grpWechatList.Size = new System.Drawing.Size(1016, 686);
            this.grpWechatList.TabIndex = 7;
            this.grpWechatList.TabStop = false;
            this.grpWechatList.Text = "微信用户列表";
            // 
            // dgAgentWechatAccount
            // 
            this.dgAgentWechatAccount.AllowUserToAddRows = false;
            this.dgAgentWechatAccount.AllowUserToDeleteRows = false;
            this.dgAgentWechatAccount.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgAgentWechatAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAgentWechatAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAgentWechatAccount.Location = new System.Drawing.Point(3, 17);
            this.dgAgentWechatAccount.Name = "dgAgentWechatAccount";
            this.dgAgentWechatAccount.ReadOnly = true;
            this.dgAgentWechatAccount.RowHeadersWidth = 10;
            this.dgAgentWechatAccount.RowTemplate.Height = 23;
            this.dgAgentWechatAccount.Size = new System.Drawing.Size(1010, 666);
            this.dgAgentWechatAccount.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSync);
            this.groupBox1.Controls.Add(this.checkUnsync);
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.txtKeyword);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 55);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "全部",
            "代理商联系人",
            "直供渠道联系人",
            "非直供渠道联系人"});
            this.cboType.Location = new System.Drawing.Point(338, 16);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(171, 20);
            this.cboType.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(271, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "联系人类型:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(775, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(73, 31);
            this.btnExport.TabIndex = 17;
            this.btnExport.Text = "数据导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFind.Location = new System.Drawing.Point(601, 12);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(44, 31);
            this.btnFind.TabIndex = 11;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(82, 18);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(165, 21);
            this.txtKeyword.TabIndex = 16;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(13, 21);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(47, 12);
            this.lblType.TabIndex = 15;
            this.lblType.Text = "关键字:";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(862, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(44, 31);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.Location = new System.Drawing.Point(659, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(44, 31);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // checkUnsync
            // 
            this.checkUnsync.AutoSize = true;
            this.checkUnsync.Location = new System.Drawing.Point(517, 18);
            this.checkUnsync.Name = "checkUnsync";
            this.checkUnsync.Size = new System.Drawing.Size(60, 16);
            this.checkUnsync.TabIndex = 25;
            this.checkUnsync.Text = "未同步";
            this.checkUnsync.UseVisualStyleBackColor = true;
            // 
            // btnSync
            // 
            this.btnSync.AutoSize = true;
            this.btnSync.Location = new System.Drawing.Point(717, 12);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(44, 31);
            this.btnSync.TabIndex = 26;
            this.btnSync.Text = "同步";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // frmWechatQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.grpWechatList);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmWechatQuery";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "微信管理";
            this.Load += new System.EventHandler(this.frmWechatManagement_Load);
            this.grpWechatList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentWechatAccount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpWechatList;
        private System.Windows.Forms.DataGridView dgAgentWechatAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox checkUnsync;
        private System.Windows.Forms.Button btnSync;
    }
}