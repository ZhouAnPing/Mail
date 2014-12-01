using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.WechatForm
{
    public partial class frmWechatContactManagement : Form
    {
        WechatAction wechatAction = new WechatAction();
        private string secret = "JH40o2nk6C6Q3Ym9tjVzSl9WD3x3c5sPwP8HzE5sccNW9CjoYCEugEzI4XyHNEnj";
        public frmWechatContactManagement()
        {
            InitializeComponent();
        }

        private void frmWechatContactManagement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

           
            HttpResult result = wechatAction.getDepartmentListFromWechat(secret);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                DepartmentList departmentList = JsonConvert.DeserializeObject<DepartmentList>(result.Html);

                if (departmentList != null)
                {
                    DataTable tb = new DataTable();
                    DataColumn col1 = new DataColumn("id", typeof(int));
                    tb.Columns.Add(col1);

                    DataColumn col2 = new DataColumn("name", typeof(String));
                    tb.Columns.Add(col2);

                    DataColumn col3 = new DataColumn("parentid", typeof(int));
                    tb.Columns.Add(col3);
                    foreach (Department department in departmentList.department)
                    {
                        DataRow dr = tb.NewRow();
                        dr["id"] = department.id;
                        dr["name"] = department.name;
                        dr["parentid"] = department.parentid;
                        tb.Rows.Add(dr);
                    }
                    tvOrganization.Nodes.Clear();
                    //创建根节点
                    TreeNode rootNode = new TreeNode();
                    rootNode.Text = departmentList.department[0].name;
                    rootNode.Tag = departmentList.department[0].id;
                    //rootNode.ImageUrl = "~/Image/Root.gif";

                    tvOrganization.Nodes.Add(rootNode);

                    //创建其他节点
                    CreateChildNode(rootNode, tb);

                    tvOrganization.ExpandAll();

                }

            }
        }

        /// <summary>
        /// 为TreeView控件绑定子节点（递归）
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="dataTable"></param>
        private void CreateChildNode(TreeNode parentNode, DataTable dataTable)
        {
            DataRow[] rowList = dataTable.Select("parentid=" + parentNode.Tag);
            foreach (DataRow row in rowList)
            {   //创建新节点
                TreeNode node = new TreeNode();
                //设置节点的属性
                node.Text = row["name"].ToString();
                node.Tag = row["id"].ToString();

                parentNode.Nodes.Add(node);
                //递归调用，创建其他节点
                CreateChildNode(node, dataTable);
            }
        }

        private void tvOrganization_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (e.Node != null)
            {
                this.dgWechatMember.Rows.Clear();
                dgWechatMember.Columns.Clear();

                dgWechatMember.Columns.Add("姓名", "姓名");
                dgWechatMember.Columns.Add("账号", "账号");
                dgWechatMember.Columns.Add("微信号", "微信号");
                dgWechatMember.Columns.Add("手机", "手机");
                dgWechatMember.Columns.Add("邮箱", "邮箱");
                dgWechatMember.Columns.Add("状态", "状态");
               
                int department = int.Parse(e.Node.Tag.ToString());

                WechatUser wechatUser = wechatAction.getUserFromWechatByDepartment(department, secret);

                if (wechatUser != null && wechatUser.userlist.Count > 0)
                {
                    this.grpWechatList.Text = "微信用户列表(" + wechatUser.userlist.Count + ")";



                    for (int i = 0; i < wechatUser.userlist.Count; i++)
                    {
                        HttpResult result = wechatAction.getUserFromWechat(wechatUser.userlist[i].userid, secret);
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            //表示访问成功，具体的大家就参考HttpStatusCode类
                            try
                            {
                                WechatJsonUser wechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);

                                dgWechatMember.Rows.Add();
                                DataGridViewRow row = dgWechatMember.Rows[dgWechatMember.RowCount - 1];

                                row.Cells[0].Value = wechatJsonUser.name;
                                row.Cells[1].Value = wechatJsonUser.userid;

                                row.Cells[2].Value = wechatJsonUser.weixinid;
                                row.Cells[3].Value = wechatJsonUser.mobile;
                                row.Cells[4].Value = wechatJsonUser.email;

                                if (wechatJsonUser.status == "1")
                                {

                                    row.Cells[5].Value = "已关注";
                                }
                                if (wechatJsonUser.status == "2")
                                {

                                    row.Cells[5].Value = "已冻结";
                                }
                                if (wechatJsonUser.status == "4")
                                {

                                    row.Cells[5].Value = "未关注";
                                }


                            }
                            catch (Exception ex)
                            {
                                String exr = ex.Message;
                            }
                        }

                    }

                }
                dgWechatMember.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// (DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

                dgWechatMember.AutoResizeColumns();

            }
            this.Cursor = Cursors.Default;
        }

        private void tvOrganization_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            try
            {
                Point point = new Point(e.X, e.Y);
                TreeNode tn = this.tvOrganization.GetNodeAt(point);
                tvOrganization.SelectedNode = tn;
                if (e.Button == MouseButtons.Right)
                {
                    //显示上下文菜单
                    contextMenuStrip1.Show(tvOrganization.PointToScreen(point));
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string strCmd = ((System.Windows.Forms.ToolStripMenuItem)(e.ClickedItem)).Name.ToString();
            switch (strCmd)
            {
                case "menuRenameDepartment":
                    //进入编辑
                   frmAddDepartment frmModifyDepartment = new frmAddDepartment();
                   frmModifyDepartment.Text = "重命名";
                   frmModifyDepartment.department.id = int.Parse(this.tvOrganization.SelectedNode.Tag.ToString());
                   frmModifyDepartment.department.name = this.tvOrganization.SelectedNode.Text; 
                   DialogResult ModifyResult = frmModifyDepartment.ShowDialog();
                   if (ModifyResult == System.Windows.Forms.DialogResult.OK)
                   {
                       String msgJson = JsonConvert.SerializeObject(frmModifyDepartment.department, Formatting.Indented);
                       HttpResult httpResult = wechatAction.updateDepartmentToWechat(this.secret, msgJson);
                       ReturnMessage returnMessage = (ReturnMessage)JsonConvert.DeserializeObject(httpResult.Html, typeof(ReturnMessage));
                       if (returnMessage != null && returnMessage.errcode.Equals("0"))
                       {
                           tvOrganization.SelectedNode.Text = frmModifyDepartment.department.name;
                       }
                       else
                       {
                           MessageBox.Show("重命名不成功," + returnMessage.getErrorDescrition());

                       }
                   }
                    break;
                case "menuAddSubDepartment":
                    //进入编辑
                 //   this.tvOrganization.SelectedNode.Nodes.Add(this.tvOrganization.SelectedNode.Text + this.tvOrganization.SelectedNode.GetNodeCount(false));
                    frmAddDepartment frmAddDepartment = new frmAddDepartment();
                    frmAddDepartment.Text = "创建子部门";
                    frmAddDepartment.department.parentid = int.Parse(this.tvOrganization.SelectedNode.Tag.ToString());
                   DialogResult result =  frmAddDepartment.ShowDialog();
                   if (result == System.Windows.Forms.DialogResult.OK)
                   {
                       String msgJson = JsonConvert.SerializeObject(frmAddDepartment.department, Formatting.Indented);
                       HttpResult httpResult = wechatAction.addDepartmentToWechat(this.secret, msgJson);
                       AddDepartmentReturnMessage returnMessage = (AddDepartmentReturnMessage)JsonConvert.DeserializeObject(httpResult.Html, typeof(AddDepartmentReturnMessage));
                       if (returnMessage != null && returnMessage.errcode.Equals("0"))
                       {
                           //创建新节点
                           TreeNode node = new TreeNode();
                           //设置节点的属性
                           node.Text = frmAddDepartment.department.name;
                           node.Tag = returnMessage.id;

                           tvOrganization.SelectedNode.Nodes.Add(node);
                       }
                       else
                       {
                           MessageBox.Show("创建不成功," + returnMessage.getErrorDescrition());

                       }
                   }
                    
                    break;
                case "menuDeleteDeppartment":
                    //进入编辑
                   HttpResult deleteResult = wechatAction.deleteDepartmentFromWechat(int.Parse(this.tvOrganization.SelectedNode.Tag.ToString()),this.secret);
                   AddDepartmentReturnMessage deleteMessage = (AddDepartmentReturnMessage)JsonConvert.DeserializeObject(deleteResult.Html, typeof(AddDepartmentReturnMessage));
                   if (deleteMessage != null && deleteMessage.errcode.Equals("0"))
                       {
                           //创建新节点
                           tvOrganization.SelectedNode.Remove();
                       }
                       else
                       {
                           MessageBox.Show("删除不成功," + deleteMessage.getErrorDescrition());

                       }
                    break;
                //...case
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddWechatUser frmAddWechatUser = new frmAddWechatUser();

            if (frmAddWechatUser.wechatJsonUser.department == null)
            {
                frmAddWechatUser.wechatJsonUser.department = new List<int>();
            }
            if (this.tvOrganization.SelectedNode != null)
            {
                frmAddWechatUser.wechatJsonUser.department.Add(int.Parse(this.tvOrganization.SelectedNode.Tag.ToString()));
            }
            DialogResult result = frmAddWechatUser.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                dgWechatMember.Rows.Add();
                DataGridViewRow row = dgWechatMember.Rows[dgWechatMember.RowCount - 1];
                row.Cells[0].Value = frmAddWechatUser.wechatJsonUser.name;
                row.Cells[1].Value = frmAddWechatUser.wechatJsonUser.userid;
                row.Cells[2].Value = frmAddWechatUser.wechatJsonUser.weixinid;
                row.Cells[3].Value = frmAddWechatUser.wechatJsonUser.mobile;
                row.Cells[4].Value = frmAddWechatUser.wechatJsonUser.email;
                row.Cells[5].Value = "未关注";
            }
        }

       

        private void menuModifyUser_Click(object sender, EventArgs e)
        {
            String userId = null;
            if (this.dgWechatMember.SelectedRows == null)
            {
                MessageBox.Show("请先选择");
                return;
            }
            frmAddWechatUser frmAddWechatUser = new frmAddWechatUser();
            DialogResult result = frmAddWechatUser.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                dgWechatMember.Rows.Add();
                DataGridViewRow row = dgWechatMember.Rows[dgWechatMember.RowCount - 1];
                row.Cells[0].Value = frmAddWechatUser.wechatJsonUser.name;
                row.Cells[1].Value = frmAddWechatUser.wechatJsonUser.userid;
                row.Cells[2].Value = frmAddWechatUser.wechatJsonUser.weixinid;
                row.Cells[3].Value = frmAddWechatUser.wechatJsonUser.mobile;
                row.Cells[4].Value = frmAddWechatUser.wechatJsonUser.email;
                row.Cells[5].Value = "未关注";

            }
        }

        private void menuDeleteUser_Click(object sender, EventArgs e)
        {

        }
    }
}
