using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using TripolisDialogueAdapter;

namespace ChinaUnion_Agent.Wechat
{
    public partial class frmAgentWechatManagement : Form
    {
        public frmAgentWechatManagement()
        {
            InitializeComponent();
        }

        BackgroundWorker worker; 
        private void frmAgentWechatManagement_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.WindowState = FormWindowState.Maximized;   
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            //代理商信息            
            //   Queryworker.ReportProgress(1, "代理商信息...\r\n");
            AgentDao agentDao = new AgentDao();
            IList<Agent> agentList = agentDao.GetList();

            if (agentList != null && agentList.Count > 0)
            {
                dgAgent.Rows.Clear();
                dgAgent.Columns.Clear();

                dgAgent.Columns.Add("代理商编号", "代理商编号");
                dgAgent.Columns.Add("代理商名称", "代理商名称");
                dgAgent.Columns.Add("联系人邮箱", "联系人邮箱");
                // dgAgent.Columns.Add("联系人姓名", "联系人姓名");
                dgAgent.Columns.Add("联系人电话", "联系人电话");
                dgAgent.Columns.Add("联系人微信账号", "联系人微信账号");
                dgAgent.Columns.Add("代理商状态", "代理商状态");


                for (int i = 0; i < agentList.Count; i++)
                {
                    dgAgent.Rows.Add();
                    DataGridViewRow row = dgAgent.Rows[i];

                    row.Cells[0].Value = agentList[i].agentNo;
                    row.Cells[1].Value = agentList[i].agentName;
                    row.Cells[2].Value = agentList[i].contactEmail;
                    //   row.Cells[3].Value = agentList[i].contactName;
                    row.Cells[3].Value = agentList[i].contactTel;
                    row.Cells[4].Value = agentList[i].contactWechatAccount;
                    if (!String.IsNullOrEmpty(agentList[i].status) && agentList[i].status.ToUpper().Equals("Y"))
                    {
                        row.Cells[5].Value = "账号已经停用";
                    }
                    else
                    {
                        row.Cells[5].Value = "";
                    }

                }
                dgAgent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
             
                dgAgent.AutoResizeColumns();
                this.grpWechat.Dock = DockStyle.None;                
               // this.ResizeRedraw = true;
            }


            this.Cursor = Cursors.Default;
        }
        private void prepareGrid()
        {
           
            WechatAction wechatAction = new WechatAction();
            WechatUser wechatUser = wechatAction.getUserFromWechatByDepartment(Settings.Default.Wechat_Agent_Department, Settings.Default.Wechat_Secret);
            this.grpWechat.Dock = DockStyle.Right;
          
            if (wechatUser != null && wechatUser.userlist.Count > 0)
            {
                this.dgWechat.Rows.Clear();
                dgWechat.Columns.Clear();

                dgWechat.Columns.Add("代理商编号", "代理商编号");
                dgWechat.Columns.Add("代理商名称", "代理商名称");

                //  dgAgent.Columns.Add("联系人邮箱", "联系人邮箱");               
                //  dgAgent.Columns.Add("联系人电话", "联系人电话");
                dgWechat.Columns.Add("联系人微信账号", "联系人微信账号");


                for (int i = 0; i < wechatUser.userlist.Count; i++)
                {
                    dgWechat.Rows.Add();
                    DataGridViewRow row = dgWechat.Rows[i];

                    row.Cells[0].Value = wechatUser.userlist[i].userid;
                    row.Cells[1].Value = wechatUser.userlist[i].name;

                    HttpResult result = wechatAction.getUserFromWechat(wechatUser.userlist[i].userid, Settings.Default.Wechat_Secret);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //表示访问成功，具体的大家就参考HttpStatusCode类
                        try
                        {
                            WechatJsonUser wechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);
                            row.Cells[2].Value = wechatJsonUser.weixinid;
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

            for (int i = 0; i < dgAgent.RowCount; i++)
            {
                WechatJsonUser wechatJsonUser = new WechatJsonUser();
                wechatJsonUser.userid = this.dgAgent[0, i].Value.ToString();
                wechatJsonUser.name = this.dgAgent[1, i].Value.ToString();
                wechatJsonUser.email = this.dgAgent[2, i].Value.ToString();
                wechatJsonUser.mobile = this.dgAgent[3, i].Value.ToString();
                wechatJsonUser.weixinid = this.dgAgent[4, i].Value.ToString();
                wechatJsonUser.department = new List<int>();
                wechatJsonUser.department.Add(Settings.Default.Wechat_Agent_Department);
                worker.ReportProgress(2, "同步微信账号" + wechatJsonUser.userid + "\r\n");
                var userData = new
                {
                    userid = wechatJsonUser.userid,
                    name = wechatJsonUser.name,
                    department = wechatJsonUser.department,
                    position = wechatJsonUser.name,
                    mobile = wechatJsonUser.mobile,
                    email = wechatJsonUser.email,
                    weixinid = wechatJsonUser.weixinid
                };

                string InsertUserJson = JsonConvert.SerializeObject(userData, Formatting.Indented);

                HttpResult result = wechatAction.getUserFromWechat(wechatJsonUser.userid, Settings.Default.Wechat_Secret);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //表示访问成功，具体的大家就参考HttpStatusCode类
                    wechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);
                    if (!String.IsNullOrEmpty(wechatJsonUser.userid))
                    {
                        if (this.dgAgent[5, i].Value.ToString().Equals("账号已经停用"))
                        {                          
                            wechatJsonUser.department.Remove(Settings.Default.Wechat_Agent_Department);                           
                        }
                        else
                        {
                            wechatJsonUser.department.Add(Settings.Default.Wechat_Agent_Department);                           
                        }
                        var updateUserData = new
                        {
                            userid = this.dgAgent[0, i].Value.ToString(),
                            name = this.dgAgent[1, i].Value.ToString(),
                            email = this.dgAgent[2, i].Value.ToString(),
                            mobile = this.dgAgent[3, i].Value.ToString(),
                            weixinid = this.dgAgent[4, i].Value.ToString(),
                            department = wechatJsonUser.department
                        };

                        string updateUserJson = JsonConvert.SerializeObject(updateUserData, Formatting.Indented);

                        if (wechatJsonUser.department.Count == 0)
                        {
                            result = wechatAction.deleteUserFromWechat(wechatJsonUser.userid, Settings.Default.Wechat_Secret);
                        }
                        else
                        {
                            result = wechatAction.updateUserToWechat(Settings.Default.Wechat_Secret, updateUserJson);
                            if (!String.IsNullOrEmpty(this.dgAgent[2, i].Value.ToString()))
                            {
                              this.sendEmail(this.dgAgent[2, i].Value.ToString());
                            }
                        }
                    }
                    else
                    {
                        result = wechatAction.addUserToWechat(Settings.Default.Wechat_Secret, InsertUserJson);
                        if (!String.IsNullOrEmpty(this.dgAgent[2, i].Value.ToString()))
                        {
                          this.sendEmail(this.dgAgent[2, i].Value.ToString());
                        }
                    }
                }

            }



            worker.ReportProgress(2, "同步微信账号完毕...\r\n");


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

            String message = mailAdapter.sendSingleEmail(databaseId, workspaceId, emailTypeId, mailData.sender, mailData.fromAddress, emailId, "Test", mailData.subject, content);


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
        private void btnSync_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();
           

            this.prepareGrid();
            this.Cursor = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
       
    }
}
