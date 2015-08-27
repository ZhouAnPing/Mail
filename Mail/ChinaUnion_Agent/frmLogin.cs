using ChinaUnion_Agent.UserManagement;
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

namespace ChinaUnion_Agent
{
    public partial class frmLogin : Form
    {
        public DataTable menuTable = new DataTable();
        public User loginUser = new User();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtUserName.Text))
            {
                MessageBox.Show("请输入用户名");
                txtUserName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtPassword.Text))
            {
                MessageBox.Show("请输入密码");
                txtPassword.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            UserDao userDao = new UserDao();
            this.loginUser = userDao.Get(this.txtUserName.Text);
            if (loginUser == null || (loginUser != null && !loginUser.password.Equals(this.txtPassword.Text)))
            {
                MessageBox.Show("用户名或者密码不正确，请重新输入.");
                txtUserName.Focus();
                this.Cursor = Cursors.Default;
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Hide();
            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
          //  frm.Height = 100;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();
            this.Cursor = Cursors.Default;

            this.Close();
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Application.Exit();
        }
        BackgroundWorker worker;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

        }
         /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(2, "系统正在加载数据，请稍后...\r\n");
            MyMenuItemDao menuDao = new MyMenuItemDao();
            menuTable = menuDao.GetListByUserId(this.txtUserName.Text.Trim());//.GetListDT();            
            worker.ReportProgress(2, "系统加载数据完成，正在启动...\r\n");
        }
        /// <summary>
        /// 事件: 异步执行完成后 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // MessageBox.Show("处理完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnModifyPassword_Click(object sender, EventArgs e)
        {
          
            if (String.IsNullOrEmpty(this.txtUserName.Text))
            {
                MessageBox.Show("请输入用户名");
                txtUserName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtPassword.Text))
            {
                MessageBox.Show("请输入密码");
                txtPassword.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            UserDao userDao = new UserDao();
            this.loginUser = userDao.Get(this.txtUserName.Text);
            if (loginUser == null || (loginUser != null && !loginUser.password.Equals(this.txtPassword.Text)))
            {
                MessageBox.Show("用户名或者密码不正确，请重新输入.");
                txtUserName.Focus();
                return;
            }
            this.Cursor = Cursors.Default;
            frmUserModification frmUserModification = new frmUserModification();
            frmUserModification.ShowDialog();
        }
    }
}
