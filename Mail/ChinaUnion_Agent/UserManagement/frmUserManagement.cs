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
    public partial class frmUserManagement : Form
    {
        private UserDao userDao = new UserDao();
        private UserRightDao userRightDao = new UserRightDao();
        private MyMenuItemDao myMenuItemDao = new MyMenuItemDao();
        
        public frmUserManagement()
        {
            InitializeComponent();
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            initControl();
        }

        private void prepareGrid(String userId)
        {
            this.Cursor = Cursors.WaitCursor;
            

            IList<User> userList = userDao.GetList(userId);

            this.dgUser.Rows.Clear();
            dgUser.Columns.Clear();

            dgUser.Columns.Add("用户名", "用户名");
            dgUser.Columns.Add("姓名", "姓名");

            if (userList != null && userList.Count > 0)
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    dgUser.Rows.Add();
                    DataGridViewRow row = dgUser.Rows[dgUser.RowCount - 1];
                    row.Cells[0].Value = userList[i].userId;
                    row.Cells[1].Value = userList[i].name;
                }
            }
            dgUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgUser.AutoResizeColumns();
            this.Cursor = Cursors.Default;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;

            prepareGrid(this.txtUserId.Text.Trim());

            this.Cursor = Cursors.Default;
        }

        private void initControl()
        {
            this.Cursor = Cursors.WaitCursor;
            this.btnSave.Enabled = false;
            this.btnDelete.Enabled = false;
            this.txtNewUserId.Enabled = true;
            //this.btnClear.Enabled = true;
            this.txtNewUserId.Clear();
            this.txtPassword.Clear();
            this.txtName.Clear();
            dgAllRight.Columns.Clear();
            this.dgAllRight.Columns.Add("序号", "序号");
            dgAllRight.Columns.Add("权限", "权限");
            dgAllRight.Columns[0].Visible = false;
            this.dgAssignRight.Columns.Clear();
            this.dgAssignRight.Columns.Add("序号", "序号");
            dgAssignRight.Columns.Add("权限", "权限");
            dgAssignRight.Columns[0].Visible = false;

            IList<MyMenuItem> menuList = myMenuItemDao.GetList();           
            foreach (MyMenuItem item in menuList)
            {
                if (item.Parent_Id.Equals("0") && !item.Id.Equals("0000"))
                {
                    dgAllRight.Rows.Add();
                    DataGridViewRow row = dgAllRight.Rows[dgAllRight.RowCount - 1];
                    row.Cells[0].Value = item.Id;
                    row.Cells[1].Value = item.Menu_Text;
                }
            }
            dgAllRight.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
          
            dgAllRight.AutoResizeColumns();
            this.Cursor = Cursors.Default;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            initControl();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNewUserId_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtNewUserId.Text.Trim()))
            {
                this.btnDelete.Enabled = false;
                this.btnSave.Enabled = false;
            }
            else
            {
                this.btnDelete.Enabled = true;
                this.btnSave.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.initControl();
        }

        int currentRowIndex = 0;
        private void dgUser_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (dgUser.CurrentRow != null)
            {
                if (this.dgUser[0, currentRowIndex].Value != null)
                {
                    this.initControl();
                    currentRowIndex = dgUser.CurrentRow.Index;
                    User user = userDao.Get(this.dgUser[0, currentRowIndex].Value.ToString());
                    if (user != null)
                    {
                        this.txtName.Text = user.name;
                        this.txtNewUserId.Text = user.userId;
                        this.txtPassword.Text = user.password;
                        this.txtNewUserId.Enabled = false;
                       // this.btnClear.Enabled = false;

                        this.dgAssignRight.Columns.Clear();
                        this.dgAssignRight.Columns.Add("序号", "序号");
                        dgAssignRight.Columns.Add("权限", "权限");
                        dgAssignRight.Columns[0].Visible = false;
                        foreach (UserRight item in user.userRightList)
                        {
                            dgAssignRight.Rows.Add();
                            DataGridViewRow row = dgAssignRight.Rows[dgAssignRight.RowCount - 1];
                            row.Cells[0].Value = item.menuId;
                            row.Cells[1].Value = item.menuText;

                            foreach (DataGridViewRow row1 in dgAllRight.Rows)
                            {
                                if(row1.Cells[0].Value.Equals(row.Cells[0].Value)){
                                    dgAllRight.Rows.Remove(row1);
                                    break;
                                }
                            }
                        }
                        dgAssignRight.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgAssignRight.AutoResizeColumns();
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtNewUserId.Text.Trim()))
            {
                MessageBox.Show("请输入用户名！");
                this.txtNewUserId.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBox.Show("请输入姓名！");
                this.txtName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtPassword.Text.Trim()))
            {
                MessageBox.Show("请输入密码！");
                this.txtPassword.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            User user = new User();
            user.userId = this.txtNewUserId.Text.Trim();
            user.name = this.txtName.Text.Trim();
            user.password = this.txtPassword.Text.Trim();
            userRightDao.DeleteByUserId(user.userId);
            userDao.Delete(user.userId);
            userDao.Add(user);
            foreach (DataGridViewRow row in this.dgAssignRight.Rows)
            {
                UserRight userRight = new UserRight();
                userRight.userId = user.userId;
                userRight.menuId = row.Cells[0].Value.ToString();
                userRightDao.Add(userRight);
            }
            this.prepareGrid("");
            MessageBox.Show("操作完成");

            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgUser.CurrentRow == null)
            {
                MessageBox.Show("请从用户列表中选择选择一行进行删除");
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            String userId = dgUser.CurrentRow.Cells[0].Value.ToString();
            userRightDao.DeleteByUserId(userId);
            userDao.Delete(userId);
            this.prepareGrid("");
           
            this.Cursor = Cursors.Default;
            MessageBox.Show("操作完成");
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (dgAllRight.CurrentRow == null)
            {
                MessageBox.Show("请从左边权限列表中选择选择一行");
                return;
            }
            //dgAssignRight.Rows.Add(this.dgAllRight.CurrentRow.Clone());
           

            dgAssignRight.Rows.Add();
            DataGridViewRow row = dgAssignRight.Rows[dgAssignRight.RowCount - 1];
            row.Cells[0].Value = this.dgAllRight.CurrentRow.Cells[0].Value;
            row.Cells[1].Value = this.dgAllRight.CurrentRow.Cells[1].Value;

            this.dgAllRight.Rows.Remove(this.dgAllRight.CurrentRow);
            dgAllRight.Refresh();
            dgAssignRight.Refresh();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (dgAssignRight.CurrentRow == null)
            {
                MessageBox.Show("请从右边权限列表中选择选择一行");
                return;
            }
            //dgAssignRight.Rows.Add(this.dgAllRight.CurrentRow.Clone());
          

            dgAllRight.Rows.Add();
            DataGridViewRow row = dgAllRight.Rows[dgAllRight.RowCount - 1];
            row.Cells[0].Value = this.dgAssignRight.CurrentRow.Cells[0].Value;
            row.Cells[1].Value = this.dgAssignRight.CurrentRow.Cells[1].Value;

            this.dgAssignRight.Rows.Remove(this.dgAssignRight.CurrentRow);
            dgAllRight.Refresh();
            dgAssignRight.Refresh();
        }

        private void btnRightAll_Click(object sender, EventArgs e)
        {

            //this.dgAllRight.Rows.Remove(this.dgAllRight.CurrentRow);
            foreach (DataGridViewRow allRightRow in dgAllRight.Rows)
            {
                dgAssignRight.Rows.Add();
                DataGridViewRow row = dgAssignRight.Rows[dgAssignRight.RowCount - 1];
                row.Cells[0].Value = allRightRow.Cells[0].Value;
                row.Cells[1].Value = allRightRow.Cells[1].Value;
            }
            dgAllRight.Rows.Clear();
            dgAllRight.Refresh();
            dgAssignRight.Refresh();

        }

        private void btnLeftAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow AssignRightRow in dgAssignRight.Rows)
            {
                dgAllRight.Rows.Add();
                DataGridViewRow row = dgAllRight.Rows[dgAllRight.RowCount - 1];
                row.Cells[0].Value = AssignRightRow.Cells[0].Value;
                row.Cells[1].Value = AssignRightRow.Cells[1].Value;
            }
            dgAssignRight.Rows.Clear();
            dgAllRight.Refresh();
            dgAssignRight.Refresh();

        }
    }
}
