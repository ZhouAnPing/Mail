using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.UserManagement
{
    public partial class frmUserModification : Form
    {
        public ChinaUnion_BO.User loginUser = new ChinaUnion_BO.User();
        public frmUserModification()
        {
            InitializeComponent();
        }

        private void frmUserModification_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.txtUserName.Text = loginUser.userId;
            this.txtName.Text = loginUser.name;
            this.txtPassword.Text = loginUser.password;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (String.IsNullOrEmpty(this.txtNewPassword1.Text))
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            if (!this.txtNewPassword1.Text.Equals(this.txtNewPassword2.Text))
            {
                MessageBox.Show("二次输入的密码不一致，请重新输入");
                return;
            }

            UserDao userDao = new UserDao();
            User user = new User();
            user.userId = this.txtUserName.Text;
            user.name = this.txtName.Text;
            user.password = this.txtNewPassword1.Text;
            userDao.Update(user);
            this.Cursor = Cursors.Default;
            MessageBox.Show("密码修改完成,下次请用新密码登录.");

        }
    }
}
