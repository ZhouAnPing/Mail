namespace ChinaUnion_Agent.MasterDataForm
{
    partial class frmAgentWechatUserModify
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAgentNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAgentName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWeixinId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkFee = new System.Windows.Forms.CheckBox();
            this.chkStudy = new System.Windows.Forms.CheckBox();
            this.chkContactUs = new System.Windows.Forms.CheckBox();
            this.chkError = new System.Windows.Forms.CheckBox();
            this.chkService = new System.Windows.Forms.CheckBox();
            this.chkComplain = new System.Windows.Forms.CheckBox();
            this.chkPolicy = new System.Windows.Forms.CheckBox();
            this.chkPerformance = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(162, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "(成员唯一标识，不可更改，不支持中文)";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(284, 490);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(177, 489);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtAgentNo
            // 
            this.txtAgentNo.Enabled = false;
            this.txtAgentNo.Location = new System.Drawing.Point(134, 12);
            this.txtAgentNo.Name = "txtAgentNo";
            this.txtAgentNo.Size = new System.Drawing.Size(279, 21);
            this.txtAgentNo.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "编号：";
            // 
            // txtAgentName
            // 
            this.txtAgentName.Enabled = false;
            this.txtAgentName.Location = new System.Drawing.Point(134, 47);
            this.txtAgentName.Name = "txtAgentName";
            this.txtAgentName.Size = new System.Drawing.Size(279, 21);
            this.txtAgentName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "联系人编号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMobile);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtWeixinId);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(32, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 184);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "身份验证信息(以下三种信息不能同时为空)";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(67, 131);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(328, 21);
            this.txtEmail.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "邮箱：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(64, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(269, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "(若未匹配到员工的微信，则通过邮箱来验证身份)";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(67, 77);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(328, 21);
            this.txtMobile.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "手机：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(64, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(317, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "(若该手机对应的微信与员工扫描的微信匹配，则允许关注)";
            // 
            // txtWeixinId
            // 
            this.txtWeixinId.Location = new System.Drawing.Point(67, 26);
            this.txtWeixinId.Name = "txtWeixinId";
            this.txtWeixinId.Size = new System.Drawing.Size(328, 21);
            this.txtWeixinId.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "微信号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(64, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(221, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "(若与员工扫描的微信匹配，则允许关注)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(108, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(108, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 16;
            this.label12.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(108, 123);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 21;
            this.label14.Text = "*";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(134, 117);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(279, 21);
            this.txtUserName.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(39, 120);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 20;
            this.label15.Text = "联系人姓名：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(108, 51);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 12);
            this.label16.TabIndex = 24;
            this.label16.Text = "*";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(134, 82);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(279, 21);
            this.txtUserId.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(39, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 23;
            this.label17.Text = "名称：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkPerformance);
            this.groupBox2.Controls.Add(this.chkPolicy);
            this.groupBox2.Controls.Add(this.chkComplain);
            this.groupBox2.Controls.Add(this.chkService);
            this.groupBox2.Controls.Add(this.chkError);
            this.groupBox2.Controls.Add(this.chkContactUs);
            this.groupBox2.Controls.Add(this.chkStudy);
            this.groupBox2.Controls.Add(this.chkFee);
            this.groupBox2.Location = new System.Drawing.Point(32, 346);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 115);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "权限";
            // 
            // chkFee
            // 
            this.chkFee.AutoSize = true;
            this.chkFee.Location = new System.Drawing.Point(15, 21);
            this.chkFee.Name = "chkFee";
            this.chkFee.Size = new System.Drawing.Size(132, 16);
            this.chkFee.TabIndex = 0;
            this.chkFee.Text = "佣金结算与支付查询";
            this.chkFee.UseVisualStyleBackColor = true;
            // 
            // chkStudy
            // 
            this.chkStudy.AutoSize = true;
            this.chkStudy.Location = new System.Drawing.Point(15, 93);
            this.chkStudy.Name = "chkStudy";
            this.chkStudy.Size = new System.Drawing.Size(72, 16);
            this.chkStudy.TabIndex = 1;
            this.chkStudy.Text = "在线学习";
            this.chkStudy.UseVisualStyleBackColor = true;
            // 
            // chkContactUs
            // 
            this.chkContactUs.AutoSize = true;
            this.chkContactUs.Location = new System.Drawing.Point(207, 93);
            this.chkContactUs.Name = "chkContactUs";
            this.chkContactUs.Size = new System.Drawing.Size(72, 16);
            this.chkContactUs.TabIndex = 2;
            this.chkContactUs.Text = "联系我们";
            this.chkContactUs.UseVisualStyleBackColor = true;
            // 
            // chkError
            // 
            this.chkError.AutoSize = true;
            this.chkError.Location = new System.Drawing.Point(207, 69);
            this.chkError.Name = "chkError";
            this.chkError.Size = new System.Drawing.Size(72, 16);
            this.chkError.TabIndex = 3;
            this.chkError.Text = "报错处理";
            this.chkError.UseVisualStyleBackColor = true;
            // 
            // chkService
            // 
            this.chkService.AutoSize = true;
            this.chkService.Location = new System.Drawing.Point(207, 45);
            this.chkService.Name = "chkService";
            this.chkService.Size = new System.Drawing.Size(72, 16);
            this.chkService.TabIndex = 4;
            this.chkService.Text = "服务监督";
            this.chkService.UseVisualStyleBackColor = true;
            // 
            // chkComplain
            // 
            this.chkComplain.AutoSize = true;
            this.chkComplain.Location = new System.Drawing.Point(207, 21);
            this.chkComplain.Name = "chkComplain";
            this.chkComplain.Size = new System.Drawing.Size(72, 16);
            this.chkComplain.TabIndex = 5;
            this.chkComplain.Text = "投诉协查";
            this.chkComplain.UseVisualStyleBackColor = true;
            // 
            // chkPolicy
            // 
            this.chkPolicy.AutoSize = true;
            this.chkPolicy.Location = new System.Drawing.Point(15, 45);
            this.chkPolicy.Name = "chkPolicy";
            this.chkPolicy.Size = new System.Drawing.Size(72, 16);
            this.chkPolicy.TabIndex = 6;
            this.chkPolicy.Text = "业务政策";
            this.chkPolicy.UseVisualStyleBackColor = true;
            // 
            // chkPerformance
            // 
            this.chkPerformance.AutoSize = true;
            this.chkPerformance.Location = new System.Drawing.Point(15, 69);
            this.chkPerformance.Name = "chkPerformance";
            this.chkPerformance.Size = new System.Drawing.Size(72, 16);
            this.chkPerformance.TabIndex = 7;
            this.chkPerformance.Text = "业绩查询";
            this.chkPerformance.UseVisualStyleBackColor = true;
            // 
            // frmAgentWechatUserModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 545);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtAgentName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAgentNo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAgentWechatUserModify";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "成员维护";
            this.Load += new System.EventHandler(this.frmAddWechatUser_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtAgentNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAgentName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWeixinId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkFee;
        private System.Windows.Forms.CheckBox chkPerformance;
        private System.Windows.Forms.CheckBox chkPolicy;
        private System.Windows.Forms.CheckBox chkComplain;
        private System.Windows.Forms.CheckBox chkService;
        private System.Windows.Forms.CheckBox chkError;
        private System.Windows.Forms.CheckBox chkContactUs;
        private System.Windows.Forms.CheckBox chkStudy;
    }
}