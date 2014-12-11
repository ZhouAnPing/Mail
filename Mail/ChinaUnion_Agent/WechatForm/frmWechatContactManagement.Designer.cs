namespace ChinaUnion_Agent.WechatForm
{
    partial class frmWechatContactManagement
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvOrganization = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtPolicy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpWechatList = new System.Windows.Forms.GroupBox();
            this.dgWechatMember = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAddSubDepartment = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteDeppartment = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRenameDepartment = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuModifyUser = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddUser = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpWechatList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWechatMember)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvOrganization);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 741);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "组织架构";
            // 
            // tvOrganization
            // 
            this.tvOrganization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOrganization.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvOrganization.ItemHeight = 20;
            this.tvOrganization.Location = new System.Drawing.Point(3, 17);
            this.tvOrganization.Name = "tvOrganization";
            this.tvOrganization.ShowNodeToolTips = true;
            this.tvOrganization.Size = new System.Drawing.Size(260, 721);
            this.tvOrganization.TabIndex = 2;
            this.tvOrganization.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvOrganization_AfterSelect);
            this.tvOrganization.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvOrganization_NodeMouseClick);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(266, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 741);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtPolicy);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(269, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(747, 69);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(529, 22);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 34);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(315, 22);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(74, 34);
            this.btnQuery.TabIndex = 32;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(422, 22);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 34);
            this.btnAdd.TabIndex = 36;
            this.btnAdd.Text = "添加成员";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtPolicy
            // 
            this.txtPolicy.Location = new System.Drawing.Point(65, 30);
            this.txtPolicy.Name = "txtPolicy";
            this.txtPolicy.Size = new System.Drawing.Size(235, 21);
            this.txtPolicy.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(18, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "账号：";
            // 
            // grpWechatList
            // 
            this.grpWechatList.Controls.Add(this.dgWechatMember);
            this.grpWechatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWechatList.Location = new System.Drawing.Point(269, 69);
            this.grpWechatList.Name = "grpWechatList";
            this.grpWechatList.Size = new System.Drawing.Size(747, 672);
            this.grpWechatList.TabIndex = 11;
            this.grpWechatList.TabStop = false;
            this.grpWechatList.Text = "通讯录成员";
            // 
            // dgWechatMember
            // 
            this.dgWechatMember.AllowUserToAddRows = false;
            this.dgWechatMember.AllowUserToDeleteRows = false;
            this.dgWechatMember.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgWechatMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWechatMember.ContextMenuStrip = this.contextMenuStrip2;
            this.dgWechatMember.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgWechatMember.Location = new System.Drawing.Point(3, 17);
            this.dgWechatMember.MultiSelect = false;
            this.dgWechatMember.Name = "dgWechatMember";
            this.dgWechatMember.ReadOnly = true;
            this.dgWechatMember.RowHeadersWidth = 10;
            this.dgWechatMember.RowTemplate.Height = 23;
            this.dgWechatMember.Size = new System.Drawing.Size(741, 652);
            this.dgWechatMember.TabIndex = 11;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddSubDepartment,
            this.menuDeleteDeppartment,
            this.menuRenameDepartment});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 70);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // menuAddSubDepartment
            // 
            this.menuAddSubDepartment.Name = "menuAddSubDepartment";
            this.menuAddSubDepartment.Size = new System.Drawing.Size(130, 22);
            this.menuAddSubDepartment.Text = "添加子部门";
            // 
            // menuDeleteDeppartment
            // 
            this.menuDeleteDeppartment.Name = "menuDeleteDeppartment";
            this.menuDeleteDeppartment.Size = new System.Drawing.Size(130, 22);
            this.menuDeleteDeppartment.Text = "删除";
            // 
            // menuRenameDepartment
            // 
            this.menuRenameDepartment.Name = "menuRenameDepartment";
            this.menuRenameDepartment.Size = new System.Drawing.Size(130, 22);
            this.menuRenameDepartment.Text = "重命名";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuModifyUser,
            this.menuDeleteUser,
            this.menuAddUser});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(119, 70);
            // 
            // menuModifyUser
            // 
            this.menuModifyUser.Name = "menuModifyUser";
            this.menuModifyUser.Size = new System.Drawing.Size(118, 22);
            this.menuModifyUser.Text = "修改成员";
            this.menuModifyUser.Click += new System.EventHandler(this.menuModifyUser_Click);
            // 
            // menuDeleteUser
            // 
            this.menuDeleteUser.Name = "menuDeleteUser";
            this.menuDeleteUser.Size = new System.Drawing.Size(118, 22);
            this.menuDeleteUser.Text = "删除成员";
            this.menuDeleteUser.Click += new System.EventHandler(this.menuDeleteUser_Click);
            // 
            // menuAddUser
            // 
            this.menuAddUser.Name = "menuAddUser";
            this.menuAddUser.Size = new System.Drawing.Size(118, 22);
            this.menuAddUser.Text = "添加成员";
            this.menuAddUser.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmWechatContactManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.grpWechatList);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmWechatContactManagement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "微信通讯录管理";
            this.Load += new System.EventHandler(this.frmWechatContactManagement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpWechatList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWechatMember)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView tvOrganization;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtPolicy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpWechatList;
        private System.Windows.Forms.DataGridView dgWechatMember;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuAddSubDepartment;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteDeppartment;
        private System.Windows.Forms.ToolStripMenuItem menuRenameDepartment;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem menuModifyUser;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteUser;
        private System.Windows.Forms.ToolStripMenuItem menuAddUser;
    }
}