namespace ChinaUnion_Agent.WechatForm
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
            this.grpWechatList = new System.Windows.Forms.GroupBox();
            this.dgWechat = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.grpWechatList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWechat)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpWechatList
            // 
            this.grpWechatList.Controls.Add(this.dgWechat);
            this.grpWechatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWechatList.Location = new System.Drawing.Point(0, 55);
            this.grpWechatList.Name = "grpWechatList";
            this.grpWechatList.Size = new System.Drawing.Size(1016, 686);
            this.grpWechatList.TabIndex = 7;
            this.grpWechatList.TabStop = false;
            this.grpWechatList.Text = "微信用户列表";
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
            this.dgWechat.Size = new System.Drawing.Size(1010, 666);
            this.dgWechat.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.textBox1);
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
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(451, 16);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 31);
            this.btnExport.TabIndex = 17;
            this.btnExport.Text = "数据导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFind.Location = new System.Drawing.Point(354, 16);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(55, 31);
            this.btnFind.TabIndex = 11;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(82, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(248, 21);
            this.textBox1.TabIndex = 16;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(13, 21);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(59, 12);
            this.lblType.TabIndex = 15;
            this.lblType.Text = "渠道名称:";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(588, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.Location = new System.Drawing.Point(471, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(55, 31);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            // 
            // frmWechatManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.grpWechatList);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmWechatManagement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "微信管理";
            this.Load += new System.EventHandler(this.frmWechatManagement_Load);
            this.grpWechatList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWechat)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpWechatList;
        private System.Windows.Forms.DataGridView dgWechat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnExport;
    }
}