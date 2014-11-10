namespace ChinaUnion_Agent.Wechat
{
    partial class frmWechatManagement
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.grpWechat = new System.Windows.Forms.GroupBox();
            this.dgWechat = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgAgent = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.grpWechat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWechat)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAgent)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSync);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSync
            // 
            this.btnSync.AutoSize = true;
            this.btnSync.Location = new System.Drawing.Point(198, 20);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(138, 23);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "账号同步到微信";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // grpWechat
            // 
            this.grpWechat.Controls.Add(this.dgWechat);
            this.grpWechat.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpWechat.Location = new System.Drawing.Point(719, 56);
            this.grpWechat.Name = "grpWechat";
            this.grpWechat.Size = new System.Drawing.Size(297, 685);
            this.grpWechat.TabIndex = 6;
            this.grpWechat.TabStop = false;
            this.grpWechat.Text = "企业微信用户列表";
            // 
            // dgWechat
            // 
            this.dgWechat.AllowUserToAddRows = false;
            this.dgWechat.AllowUserToDeleteRows = false;
            this.dgWechat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgWechat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWechat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgWechat.Location = new System.Drawing.Point(3, 17);
            this.dgWechat.Name = "dgWechat";
            this.dgWechat.ReadOnly = true;
            this.dgWechat.RowHeadersWidth = 10;
            this.dgWechat.RowTemplate.Height = 23;
            this.dgWechat.Size = new System.Drawing.Size(291, 665);
            this.dgWechat.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgAgent);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(719, 685);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "代理商列表";
            // 
            // dgAgent
            // 
            this.dgAgent.AllowUserToAddRows = false;
            this.dgAgent.AllowUserToDeleteRows = false;
            this.dgAgent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgAgent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAgent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAgent.Location = new System.Drawing.Point(3, 17);
            this.dgAgent.Name = "dgAgent";
            this.dgAgent.ReadOnly = true;
            this.dgAgent.RowHeadersWidth = 10;
            this.dgAgent.RowTemplate.Height = 23;
            this.dgAgent.Size = new System.Drawing.Size(713, 665);
            this.dgAgent.TabIndex = 7;
            // 
            // frmWechatManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpWechat);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmWechatManagement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信管理";
            this.Load += new System.EventHandler(this.frmWechatManagement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpWechat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWechat)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAgent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpWechat;
        private System.Windows.Forms.DataGridView dgWechat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgAgent;
    }
}