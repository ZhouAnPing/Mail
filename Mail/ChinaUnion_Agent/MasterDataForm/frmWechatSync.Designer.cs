namespace ChinaUnion_Agent.MasterDataForm
{
    partial class frmWechatSync
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
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.grpAgentList = new System.Windows.Forms.GroupBox();
            this.dgAgentWechatAccount = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.grpAgentList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentWechatAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.txtKeyword);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSync);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(578, 16);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 31);
            this.btnExport.TabIndex = 20;
            this.btnExport.Text = "数据导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFind.Location = new System.Drawing.Point(350, 16);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(55, 31);
            this.btnFind.TabIndex = 17;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(81, 19);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(248, 21);
            this.txtKeyword.TabIndex = 19;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 22);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(47, 12);
            this.lblType.TabIndex = 18;
            this.lblType.Text = "关键字:";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(690, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 31);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSync
            // 
            this.btnSync.AutoSize = true;
            this.btnSync.Location = new System.Drawing.Point(442, 16);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(99, 31);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "账号同步到微信";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // grpAgentList
            // 
            this.grpAgentList.Controls.Add(this.dgAgentWechatAccount);
            this.grpAgentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAgentList.Location = new System.Drawing.Point(0, 56);
            this.grpAgentList.Name = "grpAgentList";
            this.grpAgentList.Size = new System.Drawing.Size(1016, 685);
            this.grpAgentList.TabIndex = 7;
            this.grpAgentList.TabStop = false;
            this.grpAgentList.Text = "代理商列表";
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
            this.dgAgentWechatAccount.Size = new System.Drawing.Size(1010, 665);
            this.dgAgentWechatAccount.TabIndex = 7;
            // 
            // frmWechatSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.grpAgentList);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmWechatSync";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代理商微信账号同步";
            this.Load += new System.EventHandler(this.frmAgentWechatManagement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpAgentList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgentWechatAccount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpAgentList;
        private System.Windows.Forms.DataGridView dgAgentWechatAccount;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnExport;
    }
}