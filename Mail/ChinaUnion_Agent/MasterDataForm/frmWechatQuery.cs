using ChinaUnion_Agent.Properties;
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

namespace ChinaUnion_Agent.MasterDataForm
{
    public partial class frmWechatQuery : Form
    {
        public string wechatType = "ErrorCode";
        public frmWechatQuery()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.prepareGrid(this.textBox1.Text.Trim());
            this.Cursor = Cursors.Default;
        }

        private void prepareGrid(String condition)
        {

            WechatAction wechatAction = new WechatAction();
            int department = Settings.Default.Wechat_Error_Department;
            if (wechatType.Equals("ErrorCode"))
            {
                department = Settings.Default.Wechat_Error_Department;
            }
            if (wechatType.Equals("Agent"))
            {
                department = Settings.Default.Wechat_Agent_Department;
            }
            WechatUser wechatUser = wechatAction.getUserFromWechatByDepartment(department, Settings.Default.Wechat_Secret);

            if (wechatUser != null && wechatUser.userlist !=null&& wechatUser.userlist.Count > 0)
            {
                this.grpWechatList.Text = "微信用户列表(" + wechatUser.userlist.Count + ")";
                this.dgWechat.Rows.Clear();
                dgWechat.Columns.Clear();
                if (wechatType.Equals("ErrorCode"))
                {
                    dgWechat.Columns.Add("渠道名称", "渠道名称");
                    dgWechat.Columns.Add("门店联系人", "门店联系人");
                }
                if (wechatType.Equals("Agent"))
                {
                    dgWechat.Columns.Add("代理商编号", "代理商编号");
                    dgWechat.Columns.Add("代理商名称", "代理商名称");
                    
                }
                dgWechat.Columns.Add("微信号", "微信号");
                dgWechat.Columns.Add("手机", "手机");
                dgWechat.Columns.Add("邮箱", "邮箱");
                dgWechat.Columns.Add("是否关注", "是否关注"); 


                for (int i = 0; i < wechatUser.userlist.Count; i++)
                {    
                    HttpResult result = wechatAction.getUserFromWechat(wechatUser.userlist[i].userid, Settings.Default.Wechat_Secret);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //表示访问成功，具体的大家就参考HttpStatusCode类
                        try
                        {
                            WechatJsonUser wechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);
                            bool isExist = false;

                            if (wechatType.Equals("ErrorCode"))
                            {
                                if (wechatJsonUser.position.Contains(condition) || String.IsNullOrEmpty(condition))
                                {
                                    isExist = true;
                                }
                            }
                            if (wechatType.Equals("Agent"))
                            {
                                if (wechatJsonUser.name.Contains(condition) || String.IsNullOrEmpty(condition))
                                {
                                    isExist = true;
                                }

                            }

                            if (isExist)
                            {
                                dgWechat.Rows.Add();
                                DataGridViewRow row = dgWechat.Rows[dgWechat.RowCount - 1];
                                if (wechatType.Equals("ErrorCode"))
                                {
                                    row.Cells[0].Value = wechatJsonUser.position;
                                    row.Cells[1].Value = wechatJsonUser.name;
                                }
                                if (wechatType.Equals("Agent"))
                                {
                                    row.Cells[0].Value = wechatJsonUser.userid;
                                    row.Cells[1].Value = wechatJsonUser.name;
                                }
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

                        }
                        catch (Exception ex)
                        {
                            String exr = ex.Message;
                        }
                    }

                }


            }
            dgWechat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// (DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            dgWechat.AutoResizeColumns();

        }

        private void frmWechatManagement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;   
            if (wechatType.Equals("ErrorCode"))
            {
                this.lblType.Text = "渠道名称:";
            }
            if (wechatType.Equals("Agent"))
            {
                this.lblType.Text = "代理商名称:";

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgWechat);
            this.Cursor = Cursors.Default;
        }
    }
}
