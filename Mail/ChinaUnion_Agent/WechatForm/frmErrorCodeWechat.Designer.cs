namespace ChinaUnion_Agent.WechatForm
{
    partial class frmErrorCodeWechat
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgWechat = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtErrorCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSync = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWechat)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgWechat);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 55);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1016, 686);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " 错误代码微信用户列表";
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
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.txtErrorCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSync);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 55);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Location = new System.Drawing.Point(473, 18);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(55, 28);
            this.btnSelect.TabIndex = 10;
            this.btnSelect.Text = "...";
            this.btnSelect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtErrorCode
            // 
            this.txtErrorCode.Location = new System.Drawing.Point(110, 24);
            this.txtErrorCode.Name = "txtErrorCode";
            this.txtErrorCode.Size = new System.Drawing.Size(360, 21);
            this.txtErrorCode.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "微信账号文件地址：";
            // 
            // btnSync
            // 
            this.btnSync.Enabled = false;
            this.btnSync.Location = new System.Drawing.Point(598, 24);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(138, 23);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "账号同步到微信";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // frmErrorCodeWechat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmErrorCodeWechat";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信账号导入";
            this.Load += new System.EventHandler(this.frmErrorCodeWechat_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWechat)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgWechat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtErrorCode;
        private System.Windows.Forms.Label label2;
    }
}