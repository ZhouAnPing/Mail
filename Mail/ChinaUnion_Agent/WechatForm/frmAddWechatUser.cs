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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChinaUnion_Agent.WechatForm
{
    public partial class frmAddWechatUser : Form
    {
        private string secret = "JH40o2nk6C6Q3Ym9tjVzSl9WD3x3c5sPwP8HzE5sccNW9CjoYCEugEzI4XyHNEnj";
        public WechatJsonUser wechatJsonUser = new WechatJsonUser();
        WechatAction wechatAction = new WechatAction();
        IList<int> department = new List<int>();
       

        public frmAddWechatUser()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBox.Show("姓名不能为空");
                this.txtName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtUserId.Text.Trim()))
            {
                MessageBox.Show("账号不能为空");
                this.txtUserId.Focus();
                return;
            }

            if (String.IsNullOrEmpty(this.txtWeixinId.Text.Trim()) && String.IsNullOrEmpty(this.txtMobile.Text.Trim()) && String.IsNullOrEmpty(this.txtEmail.Text.Trim()))
            {
                MessageBox.Show("微信号，手机和邮箱不能同时为空");               
                return;
            }

            String EmailReg = @"^\w+([\.\-]\w+)*\@\w+([\.\-]\w+)*\.\w+$";
            if (!String.IsNullOrEmpty(txtEmail.Text.Trim())&&!Regex.IsMatch(txtEmail.Text.Trim(), EmailReg))
            {
                MessageBox.Show("请输入有效的邮箱地址");
                this.txtEmail.Focus();
                return;
            }

            String MobileReg = @"^[1]+[3,5,8]+\d{9}";
            if (!String.IsNullOrEmpty(txtMobile.Text.Trim()) && !Regex.IsMatch(this.txtMobile.Text.Trim(), MobileReg))
            {
                MessageBox.Show("请输入有效的手机号码");
                this.txtMobile.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            wechatJsonUser.name = this.txtName.Text.Trim();
           
            wechatJsonUser.weixinid = this.txtWeixinId.Text.Trim();
            wechatJsonUser.mobile = this.txtMobile.Text.Trim();
            wechatJsonUser.email = this.txtEmail.Text.Trim();
            wechatJsonUser.position = this.txtPosition.Text.Trim();
            IList<int> department = new List<int>();
            wechatJsonUser.department = this.getCheckedNode(this.tvOrganization.Nodes);
           


             String msgJson = JsonConvert.SerializeObject(wechatJsonUser, Formatting.Indented);
            HttpResult httpResult = null;
            if (this.txtUserId.Enabled)
            {
                wechatJsonUser.userid = this.txtUserId.Text.Trim();
                httpResult = wechatAction.addUserToWechat(this.secret, msgJson);
            }
            else
            {
                httpResult = wechatAction.updateUserToWechat(this.secret, msgJson);
            }
             ReturnMessage returnMessage = (ReturnMessage)JsonConvert.DeserializeObject(httpResult.Html, typeof(ReturnMessage));
             this.Cursor = Cursors.Default;
            if (returnMessage != null && returnMessage.errcode.Equals("0"))
             {
                 DialogResult = System.Windows.Forms.DialogResult.OK;
             }
             else
             {
                 MessageBox.Show("操作失败," + returnMessage.getErrorDescrition());
                 return;

             }
          
        }
        
        private IList<int> getCheckedNode(TreeNodeCollection nodes)
        {
              
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    department.Add(int.Parse(node.Tag.ToString()));
                }
                if (node.Nodes.Count > 0)
                {
                    getCheckedNode(node.Nodes);
                }
            }
            return department;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmAddWechatUser_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(wechatJsonUser.userid))
            {
                this.txtUserId.Enabled = true;
            }
            else
            {
                this.txtUserId.Enabled = false;
                this.txtUserId.Text = wechatJsonUser.userid;

                HttpResult userResult = wechatAction.getUserFromWechat(wechatJsonUser.userid, this.secret);
                if (userResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //表示访问成功，具体的大家就参考HttpStatusCode类
                    wechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(userResult.Html);

                    this.txtName.Text = wechatJsonUser.name;
                    this.txtEmail.Text = wechatJsonUser.email;
                    this.txtMobile.Text = wechatJsonUser.mobile;
                    this.txtPosition.Text = wechatJsonUser.position;
                    this.txtWeixinId.Text = wechatJsonUser.weixinid;
                }
            }
            

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
        private void CreateChildNode(TreeNode parentNode, DataTable dataTable)
        {
            DataRow[] rowList = dataTable.Select("parentid=" + parentNode.Tag);
            foreach (DataRow row in rowList)
            {   //创建新节点
                TreeNode node = new TreeNode();
                //设置节点的属性
                node.Text = row["name"].ToString();
                node.Tag = row["id"].ToString();

                if (wechatJsonUser.department != null && wechatJsonUser.department.Contains(int.Parse(node.Tag.ToString())))
                {
                    node.Checked = true;
                }

                parentNode.Nodes.Add(node);
                //递归调用，创建其他节点
                CreateChildNode(node, dataTable);
            }
        }
    }
}
