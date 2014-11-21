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
using System.Text.RegularExpressions;

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
          
            this.WindowState = FormWindowState.Maximized;   
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            //代理商信息            
            //   Queryworker.ReportProgress(1, "代理商信息...\r\n");
         
        }
        private void prepareGrid(string agentNo)
        {
            this.Cursor = Cursors.WaitCursor;

            AgentDao agentDao = new AgentDao();
            Agent agent = null;
            IList<Agent> agentList = new List<Agent>();
            if (!string.IsNullOrEmpty(agentNo))
            {
                agent = agentDao.Get(agentNo);
                if (agent != null)
                {
                    agentList.Add(agent);
                }
            }
            else
            {
                agentList = agentDao.GetList();
            }

            if (agentList != null && agentList.Count > 0)
            {
                this.grpAgentList.Text = "代理商列表(" + agentList.Count+")";
                dgAgent.Rows.Clear();
                dgAgent.Columns.Clear();

                dgAgent.Columns.Add("代理商编号", "代理商编号");
                dgAgent.Columns.Add("代理商名称", "代理商名称");
                dgAgent.Columns.Add("联系人邮箱", "联系人邮箱");               
                dgAgent.Columns.Add("联系人电话", "联系人电话");
                dgAgent.Columns.Add("联系人微信", "联系人微信");
                dgAgent.Columns.Add("账号禁用", "账号禁用");
                dgAgent.Columns.Add("微信同步备注", "微信同步备注");


                for (int i = 0; i < agentList.Count; i++)
                {
                    dgAgent.Rows.Add();
                    DataGridViewRow row = dgAgent.Rows[i];

                    row.Cells[0].Value = agentList[i].agentNo;
                    row.Cells[1].Value = agentList[i].agentName;
                    row.Cells[2].Value = agentList[i].contactEmail;                  
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
                    row.Cells[6].Value = "";

                }
                dgAgent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgAgent.AutoResizeColumns();

            }


            this.Cursor = Cursors.Default;
          
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

            for (int i = 0; i < dgAgent.RowCount ; i++)
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

                //Check the Wechat rule
                #region
                String email = "";
                String mobile = this.dgAgent[3, i].Value.ToString();
                String weixinid = this.dgAgent[4, i].Value.ToString();
              

               
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
                        else
                        {
                            mobile = "";
                        } 
                    }
                

               // this.dgAgent[2, i].Value = email;
               // this.dgAgent[3, i].Value = mobile;
               // this.dgAgent[4, i].Value = weixinid;

             

                   
                #endregion


                var userData = new
                {
                    userid = wechatJsonUser.userid,
                    name = wechatJsonUser.name,
                    department = wechatJsonUser.department,                    
                    mobile = mobile,
                    email = email,
                    weixinid = weixinid
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
                            email = email,
                            mobile = mobile,
                            weixinid = weixinid,
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
                              //this.sendEmail(this.dgAgent[2, i].Value.ToString());
                            }
                        }
                    }
                    else
                    {
                        result = wechatAction.addUserToWechat(Settings.Default.Wechat_Secret, InsertUserJson);
                        ReturnMessage returnMessage1 = (ReturnMessage)JsonConvert.DeserializeObject(result.Html, typeof(ReturnMessage));
                        if (returnMessage1 != null && returnMessage1.errcode .Equals( "0") && !String.IsNullOrEmpty(this.dgAgent[2, i].Value.ToString()))
                        {
                          this.sendEmail(this.dgAgent[2, i].Value.ToString());
                        }
                    }
                    ReturnMessage returnMessage = (ReturnMessage)JsonConvert.DeserializeObject(result.Html, typeof(ReturnMessage));
                    if (returnMessage != null && returnMessage.errcode != "0")
                    {
                        this.dgAgent[6, i].Value = returnMessage.getErrorDescrition();
                    }
                    else
                    {
                        this.dgAgent[6, i].Value = "同步成功";
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

            String message = mailAdapter.sendSingleEmail(databaseId, workspaceId, emailTypeId, Settings.Default.TripolisEmailId, mailData.sender, mailData.fromAddress, emailId, "Test", mailData.subject, content);


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

          
            this.Cursor = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            this.prepareGrid(this.txtAgentNo.Text.Trim());

        }

     
       
    }
}
