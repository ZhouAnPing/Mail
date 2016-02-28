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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TripolisDialogueAdapter;

namespace ChinaUnion_Agent.MasterDataForm
{
    public partial class frmWechatQuery : Form
    {
        public string wechatType = "ErrorCode";
        AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
        BackgroundWorker worker; 
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
            if (String.IsNullOrEmpty(this.txtKeyword.Text.Trim()))
            {
                this.errorProvider1.SetError(txtKeyword, "请输入用户编号，代理商或者渠道等相关信息!");
                this.Cursor = Cursors.Default;
                return;
            }
          
            this.prepareGrid(this.txtKeyword.Text.Trim(),this.cboType.Text,this.checkUnsync.Checked);
            this.Cursor = Cursors.Default;
        }
        private void sendEmail(String emailId)
        {
            String client = Settings.Default.TripolisClient;
            String userName = Settings.Default.TripoisUserName;
            String password = Settings.Default.TripolisPassword;
            ChinaUnionAdapter mailAdapter = new ChinaUnionAdapter(client, userName, password, null);
            MailData mailData = new MailData();
            mailData.fromAddress = Settings.Default.MailFromAddress;
            mailData.replyAddress = Settings.Default.MailReplyAddress;
            mailData.sender = Settings.Default.MailSender;
            mailData.subject = "扫一扫，加入上海联通合作伙伴微信平台";

            String databaseId = Settings.Default.TripolisDBId;
            String workspaceId = Settings.Default.TripolisWorkspaceId;
            String emailTypeId = Settings.Default.TripolisEmailTypeId;

            String FilePath = Application.StartupPath + @"\BarcodeNotification.html";
            String content = System.IO.File.ReadAllText(FilePath, Encoding.UTF8);

            String message = mailAdapter.sendSingleEmail(databaseId, workspaceId, emailTypeId, Settings.Default.TripolisEmailId, mailData.sender, mailData.fromAddress, emailId, "Test", mailData.subject, content);


        }
        private void prepareGrid(string keyword, String type,bool isUnsync)
        {
            this.Cursor = Cursors.WaitCursor;
            if (type.Equals("全部"))
            {
                type = "";
            }
            WechatAction  wechatAction = new WechatAction();
            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();

            IList<AgentWechatAccount> agentWechatAccountList = new List<AgentWechatAccount>();
            agentWechatAccountList = agentWechatAccountDao.GetListByKeyword(keyword,type);
            this.grpWechatList.Text = "";
            dgAgentWechatAccount.Rows.Clear();

            if (agentWechatAccountList != null && agentWechatAccountList.Count > 0)
            {
                this.grpWechatList.Text = "微信用户列表(" + agentWechatAccountList.Count + ")";
                dgAgentWechatAccount.Rows.Clear();
                dgAgentWechatAccount.Columns.Clear();

                dgAgentWechatAccount.Columns.Add("类型", "类型");

                dgAgentWechatAccount.Columns.Add("代理商编号", "代理商编号");
                dgAgentWechatAccount.Columns.Add("代理商名称", "代理商名称");                
                dgAgentWechatAccount.Columns.Add("渠道编码", "渠道编码");
                dgAgentWechatAccount.Columns.Add("渠道名称", "渠道名称");
                dgAgentWechatAccount.Columns.Add("区县", "区县");
               
                dgAgentWechatAccount.Columns["渠道编码"].Visible = false;
                dgAgentWechatAccount.Columns["渠道名称"].Visible = false;
                dgAgentWechatAccount.Columns["区县"].Visible = false;
                if (!type.Equals("代理商联系人"))
                {
                    if (String.IsNullOrEmpty(type) || type.Equals("直供渠道联系人"))
                    {
                        dgAgentWechatAccount.Columns["区县"].Visible = true;

                    }
                    dgAgentWechatAccount.Columns["渠道编码"].Visible = true;
                    dgAgentWechatAccount.Columns["渠道名称"].Visible = true;

                }
                dgAgentWechatAccount.Columns.Add("联系人编号", "联系人编号");
                dgAgentWechatAccount.Columns.Add("联系人姓名", "联系人姓名");
                dgAgentWechatAccount.Columns.Add("联系人邮箱", "联系人邮箱");
                dgAgentWechatAccount.Columns.Add("联系人电话", "联系人电话");
                dgAgentWechatAccount.Columns.Add("联系人微信", "联系人微信");
                //dgAgent.Columns.Add("账号禁用", "账号禁用");
                dgAgentWechatAccount.Columns.Add("是否关注", "是否关注");
                dgAgentWechatAccount.Columns.Add("佣金结算与支付查询", "佣金结算与支付查询");
                dgAgentWechatAccount.Columns.Add("业务政策", "业务政策");
                dgAgentWechatAccount.Columns.Add("业绩查询", "业绩查询");
                dgAgentWechatAccount.Columns.Add("在线学习", "在线学习");
                dgAgentWechatAccount.Columns.Add("投诉协查", "投诉协查");
                dgAgentWechatAccount.Columns.Add("服务监督", "服务监督");
                dgAgentWechatAccount.Columns.Add("报错处理", "报错处理");
                dgAgentWechatAccount.Columns.Add("企业小助手", "企业小助手");

                for (int i = 0; i < agentWechatAccountList.Count; i++)
                {
                    Color curColor = Color.Blue;
                    String rowVlaue = "未同步";
                    HttpResult result = wechatAction.getUserFromWechat(agentWechatAccountList[i].contactId, Settings.Default.Wechat_Secret);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //表示访问成功，具体的大家就参考HttpStatusCode类
                        try
                        {
                            WechatJsonUser wechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);


                            if (wechatJsonUser.status == "1")
                            {
                                curColor = Color.White;

                                rowVlaue = "已关注";
                            }
                            if (wechatJsonUser.status == "2")
                            {
                                curColor = Color.Yellow;
                                rowVlaue = "已冻结";
                            }
                            if (wechatJsonUser.status == "4")
                            {
                                curColor = Color.Red;
                                rowVlaue = "未关注";
                            }
                        }
                        catch (Exception ex)
                        {
                            //row.Cells[index].Value = "";
                            String exr = ex.Message;
                        }

                    }
                    if (!rowVlaue.Equals("未同步") && isUnsync)
                    {
                        continue;
                    }
                    dgAgentWechatAccount.Rows.Add();
                    DataGridViewRow row = dgAgentWechatAccount.Rows[dgAgentWechatAccount.RowCount-1];
                    int index = 0;
                    row.Cells[index++].Value = agentWechatAccountList[i].type;
                    row.Cells[index++].Value = agentWechatAccountList[i].agentNo;
                    row.Cells[index++].Value = agentWechatAccountList[i].agentName;



                    row.Cells[index++].Value = agentWechatAccountList[i].branchNo;
                    row.Cells[index++].Value = agentWechatAccountList[i].branchName;
                    row.Cells[index++].Value = agentWechatAccountList[i].regionName;

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
                    row.Cells[index].Value = rowVlaue;
                    row.Cells[index].Style.BackColor = curColor;


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
                dgAgentWechatAccount.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgAgentWechatAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgAgentWechatAccount.AutoResizeColumns();

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
                this.dgAgentWechatAccount.Rows.Clear();
                dgAgentWechatAccount.Columns.Clear();
                if (wechatType.Equals("ErrorCode"))
                {
                    dgAgentWechatAccount.Columns.Add("渠道名称", "渠道名称");
                    dgAgentWechatAccount.Columns.Add("门店联系人", "门店联系人");
                }
                if (wechatType.Equals("Agent"))
                {
                    dgAgentWechatAccount.Columns.Add("代理商编号", "代理商编号");
                    dgAgentWechatAccount.Columns.Add("代理商名称", "代理商名称");
                    
                }
                dgAgentWechatAccount.Columns.Add("微信号", "微信号");
                dgAgentWechatAccount.Columns.Add("手机", "手机");
                dgAgentWechatAccount.Columns.Add("邮箱", "邮箱");
                dgAgentWechatAccount.Columns.Add("是否关注", "是否关注"); 


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
                                dgAgentWechatAccount.Rows.Add();
                                DataGridViewRow row = dgAgentWechatAccount.Rows[dgAgentWechatAccount.RowCount - 1];
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
            dgAgentWechatAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// (DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            dgAgentWechatAccount.AutoResizeColumns();

        }

        /// <summary>
        /// 事件: 异步执行完成后 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("微信同步完毕。", "微信同步", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void frmWechatManagement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted); 
           
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgAgentWechatAccount);
            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgAgentWechatAccount.CurrentRow==null)
            {
                MessageBox.Show("请选择一行记录"); 
                return;
            }

            String userId= this.dgAgentWechatAccount.CurrentRow.Cells[6].Value.ToString();
            if (!String.IsNullOrEmpty(userId))
            {
                WechatAction wechatAction = new Wechat.WechatAction();
                HttpResult result = wechatAction.deleteUserFromWechat(userId, Settings.Default.Wechat_Secret);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
                    agentWechatAccountDao.Delete(userId);

                    this.prepareGrid(this.txtKeyword.Text.Trim(), this.cboType.Text.Trim(),this.checkUnsync.Checked);
                }
            }
        }

        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码

            worker.ReportProgress(1, "开始同步微信账号...\r\n");

            WechatAction wechatAction = new WechatAction();

            List<String> feeRightUserIdsAll = new List<string>();
            List<String> policyRightUserIdsAll = new List<string>();
            List<String> performanceRightUserIdsAll = new List<string>();
            List<String> studyRightUserIdsAll = new List<string>();
            List<String> complainRightUserIdsAll = new List<string>();
            List<String> monitorRightUserIdsAll = new List<string>();
            List<String> errorRightUserIdsAll = new List<string>();
            List<String> contactRightUserIdsAll = new List<string>();

            List<String> feeRightUserIds = new List<string>();
            List<String> policyRightUserIds = new List<string>();
            List<String> performanceRightUserIds = new List<string>();
            List<String> studyRightUserIds = new List<string>();
            List<String> complainRightUserIds = new List<string>();
            List<String> monitorRightUserIds = new List<string>();
            List<String> errorRightUserIds = new List<string>();
            List<String> contactRightUserIds = new List<string>();

            for (int i = 0; i < dgAgentWechatAccount.RowCount; i++)
            {
                String userid = this.dgAgentWechatAccount[6, i].Value.ToString();
                feeRightUserIdsAll.Add(userid);
                policyRightUserIdsAll.Add(userid);
                performanceRightUserIdsAll.Add(userid);
                studyRightUserIdsAll.Add(userid);
                complainRightUserIdsAll.Add(userid);
                monitorRightUserIdsAll.Add(userid);
                errorRightUserIdsAll.Add(userid);
                contactRightUserIdsAll.Add(userid);



                if (this.dgAgentWechatAccount[12, i].Value.ToString().ToUpper().Equals("Y"))
                {
                    feeRightUserIds.Add(userid);
                }
                if (this.dgAgentWechatAccount[13, i].Value.ToString().ToUpper().Equals("Y"))
                {
                    policyRightUserIds.Add(userid);
                }
                if (this.dgAgentWechatAccount[14, i].Value.ToString().ToUpper().Equals("Y"))
                {
                    performanceRightUserIds.Add(userid);
                }
                if (this.dgAgentWechatAccount[15, i].Value.ToString().ToUpper().Equals("Y"))
                {
                    studyRightUserIds.Add(userid);
                }
                if (this.dgAgentWechatAccount[16, i].Value.ToString().ToUpper().Equals("Y"))
                {
                    complainRightUserIds.Add(userid);
                }
                if (this.dgAgentWechatAccount[17, i].Value.ToString().ToUpper().Equals("Y"))
                {
                    monitorRightUserIds.Add(userid);
                }
                if (this.dgAgentWechatAccount[18, i].Value.ToString().ToUpper().Equals("Y"))
                {
                    errorRightUserIds.Add(userid);
                }
                if (this.dgAgentWechatAccount[19, i].Value.ToString().ToUpper().Equals("Y"))
                {
                    contactRightUserIds.Add(userid);
                }

                WechatJsonUser wechatJsonUser = new WechatJsonUser();
                wechatJsonUser.userid = this.dgAgentWechatAccount[6, i].Value.ToString();

                wechatJsonUser.name = this.dgAgentWechatAccount[7, i].Value.ToString();
                if (string.IsNullOrEmpty(wechatJsonUser.name))
                {
                    wechatJsonUser.name = wechatJsonUser.userid;
                }
                wechatJsonUser.email = this.dgAgentWechatAccount[8, i].Value.ToString();
                wechatJsonUser.mobile = this.dgAgentWechatAccount[9, i].Value.ToString();
                wechatJsonUser.weixinid = this.dgAgentWechatAccount[10, i].Value.ToString();
                wechatJsonUser.position = this.dgAgentWechatAccount[4, i].Value.ToString();
                if (String.IsNullOrEmpty(wechatJsonUser.position))
                {
                    wechatJsonUser.position = this.dgAgentWechatAccount[1, i].Value.ToString();
                }
                wechatJsonUser.department = new List<int>();
                wechatJsonUser.department.Add(1);
                worker.ReportProgress(2, "总共" + dgAgentWechatAccount.RowCount + "条记录，正在处理第" + (i + 1) + "条，同步微信账号" + wechatJsonUser.userid + "\r\n");


                //Check the Wechat rule
                #region

                String mobile = wechatJsonUser.mobile;
                String weixinid = wechatJsonUser.weixinid;

                // ^[1]+[3,5,8]+\d{9}
                if (Regex.IsMatch(weixinid, @"^[1]+[3,5,8]+\d{9}"))
                {
                    mobile = weixinid;
                    weixinid = "";
                }
                else
                {
                    if (Regex.IsMatch(weixinid, @"^\d+$"))
                    {
                        weixinid = "QQ" + weixinid;
                        mobile = "";
                    }

                }



                #endregion


                var userData = new
                {
                    userid = wechatJsonUser.userid,
                    name = wechatJsonUser.name,
                    department = wechatJsonUser.department,
                    mobile = mobile,//wechatJsonUser.mobile,
                    email = "",// wechatJsonUser.email,
                    position = wechatJsonUser.position,
                    weixinid = weixinid// wechatJsonUser.weixinid
                };

                string InsertUserJson = JsonConvert.SerializeObject(userData, Formatting.Indented);

                HttpResult result = wechatAction.getUserFromWechat(wechatJsonUser.userid, Settings.Default.Wechat_Secret);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    //表示访问成功，具体的大家就参考HttpStatusCode类
                    WechatJsonUser wechatJsonUserFromWechat = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);
                    if (!String.IsNullOrEmpty(wechatJsonUserFromWechat.userid))
                    {
                        string updateUserJson = JsonConvert.SerializeObject(userData, Formatting.Indented);

                        // if (wechatJsonUser.department.Count == 0)
                        // {
                        //  result = wechatAction.deleteUserFromWechat(wechatJsonUser.userid, Settings.Default.Wechat_Secret);
                        // }
                        // else
                        // {
                        result = wechatAction.updateUserToWechat(Settings.Default.Wechat_Secret, updateUserJson);
                        if (!String.IsNullOrEmpty(wechatJsonUser.email))
                        {
                            //this.sendEmail(this.dgAgent[2, i].Value.ToString());
                        }
                        // }
                    }
                    else
                    {
                        result = wechatAction.addUserToWechat(Settings.Default.Wechat_Secret, InsertUserJson);
                        ReturnMessage returnMessage1 = (ReturnMessage)JsonConvert.DeserializeObject(result.Html, typeof(ReturnMessage));
                        if (returnMessage1 != null && returnMessage1.errcode.Equals("0"))
                        {
                            if (!String.IsNullOrEmpty(wechatJsonUser.email))
                            {
                                this.sendEmail(wechatJsonUser.email);
                            }
                        }
                        else
                        {
                            AgentWechatAccount agentWechatAccount = new ChinaUnion_BO.AgentWechatAccount();
                            agentWechatAccount.contactId = wechatJsonUser.userid;

                            agentWechatAccount.wechatImportStatus = returnMessage1.getErrorDescrition();
                            agentWechatAccountDao.UpdateWechatImportStatus(agentWechatAccount);
                        }
                    }



                    ReturnMessage returnMessage = (ReturnMessage)JsonConvert.DeserializeObject(result.Html, typeof(ReturnMessage));
                    if (returnMessage != null && returnMessage.errcode != "0")
                    {


                        this.dgAgentWechatAccount[11, i].Value = returnMessage.getErrorDescrition();
                    }
                    else
                    {
                        var userInviteData = new
                        {
                            userid = wechatJsonUser.userid
                        };

                        string inviteUserJson = JsonConvert.SerializeObject(userInviteData, Formatting.Indented);

                        wechatAction.inviteUserToWechat(Settings.Default.Wechat_Secret, inviteUserJson);
                        this.dgAgentWechatAccount[11, i].Value = "同步成功";
                    }
                }



            }

            deleteTagUser(feeRightUserIdsAll, 2);
            addTagUser(feeRightUserIds, 2);

            deleteTagUser(policyRightUserIdsAll, 3);
            addTagUser(policyRightUserIds, 3);

            deleteTagUser(performanceRightUserIdsAll, 4);
            addTagUser(performanceRightUserIds, 4);

            deleteTagUser(studyRightUserIdsAll, 5);
            addTagUser(studyRightUserIds, 5);

            deleteTagUser(complainRightUserIdsAll, 6);
            addTagUser(complainRightUserIds, 6);

            deleteTagUser(monitorRightUserIdsAll, 7);
            addTagUser(monitorRightUserIds, 7);

            deleteTagUser(errorRightUserIdsAll, 8);
            addTagUser(errorRightUserIds, 8);

            deleteTagUser(contactRightUserIdsAll, 9);
            addTagUser(contactRightUserIds, 9);





            worker.ReportProgress(2, "同步微信账号完毕...\r\n");


        }

        private void deleteTagUser(List<String> RightUserIds, int tagId)
        {
            WechatAction wechatAction = new WechatAction();
            List<String> rightUserIdsBuffer = new List<string>();
            for (int i = 1; i <= RightUserIds.Count; i++)
            {
                rightUserIdsBuffer.Add(RightUserIds[i - 1]);
                if (i % 300 == 0 || i == RightUserIds.Count)
                {
                    var userTagData = new
                    {
                        tagid = tagId,
                        userlist = rightUserIdsBuffer
                    };

                    string updateTagJson = JsonConvert.SerializeObject(userTagData, Formatting.Indented);

                    wechatAction.deleteTagUsers(Settings.Default.Wechat_Secret, updateTagJson);

                    rightUserIdsBuffer.Clear();
                }

            }
        }
        private void addTagUser(List<String> RightUserIds, int tagId)
        {
            WechatAction wechatAction = new WechatAction();
            List<String> rightUserIdsBuffer = new List<string>();
            for (int i = 1; i <= RightUserIds.Count; i++)
            {
                rightUserIdsBuffer.Add(RightUserIds[i - 1]);
                if (i % 300 == 0 || i == RightUserIds.Count)
                {
                    var userTagData = new
                    {
                        tagid = tagId,
                        userlist = rightUserIdsBuffer
                    };

                    string updateTagJson = JsonConvert.SerializeObject(userTagData, Formatting.Indented);
                    wechatAction.addTagUsers(Settings.Default.Wechat_Secret, updateTagJson);

                    rightUserIdsBuffer.Clear();
                }

            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();


            this.Cursor = Cursors.Default;
        }


       



        private void menuModifyUser_Click(object sender, EventArgs e)
        {
            //String userId = null;
            if (this.dgAgentWechatAccount.SelectedRows == null)
            {
                MessageBox.Show("请先选择");
                return;
            }
            frmAgentWechatUserModify frmAgentWechatUserModify = new frmAgentWechatUserModify();
            frmAgentWechatUserModify.row = this.dgAgentWechatAccount.CurrentRow;
         
            //frmAgentWechatUserModify.wechatJsonUser.userid = this.dgAgentWechatAccount.CurrentRow.Cells[6].Value.ToString();
            DialogResult result = frmAgentWechatUserModify.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                int index = 6;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.contactId;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.contactName;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.contactEmail;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.contactTel;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.contactWechat;

                //if (!String.IsNullOrEmpty(frmAgentWechatUserModify.retAgentWechatAccount.status) && frmAgentWechatUserModify.retAgentWechatAccount.status.ToUpper().Equals("Y"))
                //{
                //    row.Cells[5].Value = "账号已经停用";
                //}
                //else
                //{
                //    row.Cells[5].Value = "";
                //}
               


                index++;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.feeRight;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.policyRight;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.performanceRight;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.studyRight;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.complainRight;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.monitorRight;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.errorRight;
                this.dgAgentWechatAccount.CurrentRow.Cells[index++].Value = frmAgentWechatUserModify.retAgentWechatAccount.contactRight;

                this.btnSync_Click(sender, e);
              //  this.btnSync.Invoke(this.btnSync_Click(sender,e));
            }
        }

       
    }
}
