using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
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
            this.prepareGrid(this.txtKeyword.Text.Trim());
            this.Cursor = Cursors.Default;
        }

        private void prepareGrid(string keyword)
        {
            this.Cursor = Cursors.WaitCursor;

            WechatAction  wechatAction = new WechatAction();
            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();

            IList<AgentWechatAccount> agentWechatAccountList = new List<AgentWechatAccount>();
            agentWechatAccountList = agentWechatAccountDao.GetListByKeyword(keyword);
            this.grpWechatList.Text = "";
            dgWechat.Rows.Clear();
            if (agentWechatAccountList != null && agentWechatAccountList.Count > 0)
            {
                this.grpWechatList.Text = "微信用户列表(" + agentWechatAccountList.Count + ")";
                dgWechat.Rows.Clear();
                dgWechat.Columns.Clear();

                dgWechat.Columns.Add("类型", "类型");

                dgWechat.Columns.Add("代理商编号", "代理商编号");
                dgWechat.Columns.Add("代理商名称", "代理商名称");
                dgWechat.Columns.Add("区县", "区县");
                dgWechat.Columns.Add("渠道编码", "渠道编码");
                dgWechat.Columns.Add("渠道名称", "渠道名称");
                dgWechat.Columns.Add("联系人编号", "联系人编号");
                dgWechat.Columns.Add("联系人姓名", "联系人姓名");
                dgWechat.Columns.Add("联系人邮箱", "联系人邮箱");
                dgWechat.Columns.Add("联系人电话", "联系人电话");
                dgWechat.Columns.Add("联系人微信", "联系人微信");
                //dgAgent.Columns.Add("账号禁用", "账号禁用");
                dgWechat.Columns.Add("是否关注", "是否关注");
                dgWechat.Columns.Add("佣金结算与支付查询", "佣金结算与支付查询");
                dgWechat.Columns.Add("业务政策", "业务政策");
                dgWechat.Columns.Add("业绩查询", "业绩查询");
                dgWechat.Columns.Add("在线学习", "在线学习");
                dgWechat.Columns.Add("投诉协查", "投诉协查");
                dgWechat.Columns.Add("服务监督", "服务监督");
                dgWechat.Columns.Add("报错处理", "报错处理");
                dgWechat.Columns.Add("企业小助手", "企业小助手");

                for (int i = 0; i < agentWechatAccountList.Count; i++)
                {
                    dgWechat.Rows.Add();
                    DataGridViewRow row = dgWechat.Rows[i];
                    int index = 0;
                    row.Cells[index++].Value = agentWechatAccountList[i].type;
                    row.Cells[index++].Value = agentWechatAccountList[i].agentNo;
                    row.Cells[index++].Value = agentWechatAccountList[i].agentName;
                    row.Cells[index++].Value = agentWechatAccountList[i].regionName;
                    row.Cells[index++].Value = agentWechatAccountList[i].branchNo;
                    row.Cells[index++].Value = agentWechatAccountList[i].branchName;
                    row.Cells[index++].Value = agentWechatAccountList[i].contactId;
                    row.Cells[index++].Value = agentWechatAccountList[i].contactName;
                    row.Cells[index++].Value = agentWechatAccountList[i].contactEmail;
                    row.Cells[index++].Value = agentWechatAccountList[i].contactTel;
                    row.Cells[index++].Value = agentWechatAccountList[i].contactWechat;

                    //if (!String.IsNullOrEmpty(agentWechatAccountList[i].status) && agentWechatAccountList[i].status.ToUpper().Equals("Y"))
                    //{
                    //    row.Cells[5].Value = "账号已经停用";
                    //}
                    //else
                    //{
                    //    row.Cells[5].Value = "";
                    //}
                    row.Cells[index].Value = "未同步";
                    row.Cells[index].Style.BackColor = Color.Blue;
                    HttpResult result = wechatAction.getUserFromWechat(agentWechatAccountList[i].contactId, Settings.Default.Wechat_Secret);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //表示访问成功，具体的大家就参考HttpStatusCode类
                        try
                        {
                            WechatJsonUser wechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);


                            if (wechatJsonUser.status == "1")
                            {
                                row.Cells[index].Style.BackColor = Color.White;

                                row.Cells[index].Value = "已关注";
                            }
                            if (wechatJsonUser.status == "2")
                            {
                                row.Cells[index].Style.BackColor = Color.Yellow;
                                row.Cells[index].Value = "已冻结";
                            }
                            if (wechatJsonUser.status == "4")
                            {
                                row.Cells[index].Style.BackColor = Color.Red;
                                row.Cells[index].Value = "未关注";
                            }
                        }
                        catch (Exception ex)
                        {
                            //row.Cells[index].Value = "";
                            String exr = ex.Message;
                        }
                        index++;
                        row.Cells[index++].Value = agentWechatAccountList[i].feeRight;
                        row.Cells[index++].Value = agentWechatAccountList[i].policyRight;
                        row.Cells[index++].Value = agentWechatAccountList[i].performanceRight;
                        row.Cells[index++].Value = agentWechatAccountList[i].studyRight;
                        row.Cells[index++].Value = agentWechatAccountList[i].complainRight;
                        row.Cells[index++].Value = agentWechatAccountList[i].monitorRight;
                        row.Cells[index++].Value = agentWechatAccountList[i].errorRight;
                        row.Cells[index++].Value = agentWechatAccountList[i].contactRight;

                    }
                }
                dgWechat.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgWechat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgWechat.AutoResizeColumns();

            }


            this.Cursor = Cursors.Default;

        }

        private void prepareGrid_V1(String condition)
        {

            WechatAction wechatAction = new WechatAction();
            int department = 1;//根部门
            
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
           
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgWechat);
            this.Cursor = Cursors.Default;
        }
    }
}
