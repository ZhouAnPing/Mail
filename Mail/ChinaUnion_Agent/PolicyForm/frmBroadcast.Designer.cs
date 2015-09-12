namespace ChinaUnion_Agent.PolicyForm
{
    partial class frmBroadcast
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
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstModule = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lstGroup = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtContent);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(777, 343);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "公告内容";
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(3, 17);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(771, 323);
            this.txtContent.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(431, 381);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(104, 41);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送公告";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(431, 448);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 41);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "离开";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstModule
            // 
            this.lstModule.FormattingEnabled = true;
            this.lstModule.Items.AddRange(new object[] {
            "佣金结算与支付查询",
            "报错处理",
            "通知公告与促销政策",
            "业绩查询",
            "服务监督",
            "投诉协查",
            "在线学习",
            "企业小助手"});
            this.lstModule.Location = new System.Drawing.Point(12, 366);
            this.lstModule.Name = "lstModule";
            this.lstModule.Size = new System.Drawing.Size(144, 148);
            this.lstModule.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "接收通知模块:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lstGroup);
            this.groupBox5.Location = new System.Drawing.Point(171, 358);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(217, 156);
            this.groupBox5.TabIndex = 43;
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
            this.lstGroup.Size = new System.Drawing.Size(211, 136);
            this.lstGroup.TabIndex = 37;
            // 
            // frmBroadcast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(777, 532);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstModule);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBroadcast";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信公告发布";
            this.Load += new System.EventHandler(this.frmBroadcast_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckedListBox lstModule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox lstGroup;
    }
}