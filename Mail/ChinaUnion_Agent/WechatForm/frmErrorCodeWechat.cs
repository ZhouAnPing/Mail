using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using LinqToExcel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TripolisDialogueAdapter;

namespace ChinaUnion_Agent.WechatForm
{
    public partial class frmErrorCodeWechat : Form
    {
        public frmErrorCodeWechat()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            // openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|Excel 2000-2003(*.xls)|*.xls|CSV(*.csv)|*.csv|所有文件(*.*)|*.*";

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string FileName = openFileDialog.FileName;

                // String path= Path.GetDirectoryName(FileName);

                var execelfile = new ExcelQueryFactory(FileName);
                this.txtErrorCode.Text = FileName;
                this.txtErrorCode.Enabled = false;

                List<string> sheetNames = execelfile.GetWorksheetNames().ToList();
                if (!sheetNames.Contains("微信用户"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:微信用户的sheet.");
                    return;
                }

                this.dgWechat.Rows.Clear();
                dgWechat.Columns.Clear();
                dgWechat.Columns.Add("渠道名称", "渠道名称");
                dgWechat.Columns.Add("门店联系人", "门店联系人");
                dgWechat.Columns.Add("微信号", "微信号");
                dgWechat.Columns.Add("手机号", "手机号");
                dgWechat.Columns.Add("邮箱", "邮箱");
                dgWechat.Columns.Add("账号禁用", "账号禁用");
                dgWechat.Columns.Add("微信导入备注", "微信导入备注");
               

               



                List<Row> WechatList = execelfile.Worksheet("微信用户").ToList();
                //int count = 0;
                if (WechatList != null && WechatList.Count > 0)
                {
                    //count = ESSList.Count;
                    this.btnSync.Enabled = true;

                    this.grpWechatList.Text = " 错误代码微信用户列表(" + WechatList.Count + ")";

                    for (int i = 0; i < WechatList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(WechatList[i][0]) || String.IsNullOrWhiteSpace(WechatList[i][0]))
                        {
                            break;
                        }
                        this.dgWechat.Rows.Add();
                        DataGridViewRow row = dgWechat.Rows[dgWechat.RowCount - 1];
                        row.Cells["渠道名称"].Value = WechatList[i][0].ToString().Trim();
                        row.Cells["门店联系人"].Value = WechatList[i][1].ToString().Trim();
                        row.Cells["微信号"].Value = WechatList[i][2].ToString().Trim();
                        row.Cells["手机号"].Value = WechatList[i][3].ToString().Trim();  
                        row.Cells["邮箱"].Value = WechatList[i][4].ToString().Trim();
                        row.Cells["账号禁用"].Value = WechatList[i][5].ToString().Trim();
                        row.Cells["微信导入备注"].Value = "";                
                       

                    }
                }
            }


            //检查重复记录
            StringBuilder sbDuplicated = new StringBuilder();
            HashSet<String> agentFeeSet = new HashSet<string>();

            StringBuilder sb = new StringBuilder();


            //
            sb = new StringBuilder();
            HashSet<String> weChatSet = new HashSet<string>();
            foreach (DataGridViewRow v in this.dgWechat.Rows)
            {
                if (!String.IsNullOrEmpty(v.Cells[0].Value.ToString()))
                {
                    foreach (DataGridViewRow v2 in dgWechat.Rows)
                    {
                        if (v.Index == v2.Index)
                        {
                            continue;
                        }
                        if (!String.IsNullOrEmpty(v2.Cells[0].Value.ToString()))
                        {

                            if (v.Cells[0].Value.ToString().Equals(v2.Cells[0].Value.ToString()))
                            {
                                v2.Cells[0].Style.BackColor = Color.Red;
                                if (!weChatSet.Contains<String>(v2.Cells[0].Value.ToString()))
                                {
                                    weChatSet.Add(v2.Cells[0].Value.ToString() );
                                    sb.AppendFormat("渠道名称:{0}", v.Cells[0].Value.ToString()).AppendLine();
                                }
                                break;
                            }
                        }
                    }
                }
            }
            if (!String.IsNullOrEmpty(sb.ToString()))
            {
                sbDuplicated.AppendLine("\n渠道名称存在以下重复记录:");
                sbDuplicated.AppendLine(sb.ToString());
                sbDuplicated.AppendLine("\n请修复后重新导入");
            }




            if (!String.IsNullOrEmpty(sbDuplicated.ToString()))
            {
                this.btnSync.Enabled = false;
                MessageBox.Show(sbDuplicated.ToString());
            }
            else
            {
                this.btnSync.Enabled = true;
            }


            dgWechat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgWechat.AutoResizeColumns();

            this.dgWechat.Columns[6].MinimumWidth = 300;

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
        BackgroundWorker worker; 
        private void frmErrorCodeWechat_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;   
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }
        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码
            WechatAction wechatAction = new Wechat.WechatAction();

            worker.ReportProgress(1, "开始同步微信账号...\r\n");


            bool isNewUser = false;
            bool isDisableUser = false;


            for (int i = 0; i < this.dgWechat.RowCount; i++)
            {
                isNewUser = false;

                //Check the Wechat rule
                #region
                String email = this.dgWechat[4, i].Value.ToString();
                String mobile = this.dgWechat[3, i].Value.ToString();
                String weixinid = this.dgWechat[2, i].Value.ToString();
                
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
                      //  mobile = "";
                    }
                   
                }



                #endregion


                WechatJsonUser toWechatJsonUser = new WechatJsonUser();

                String userId = this.dgWechat[2, i].Value.ToString().Trim();
                if (String.IsNullOrEmpty(this.dgWechat[2, i].Value.ToString().Trim()))
                {
                    if (String.IsNullOrEmpty(this.dgWechat[3, i].Value.ToString().Trim()))
                    {
                        userId = this.dgWechat[4, i].Value.ToString();
                    }
                    else
                    {
                        userId = this.dgWechat[3, i].Value.ToString();
                    }
                }
                else
                {
                    userId = this.dgWechat[2, i].Value.ToString();
                }

               

                toWechatJsonUser.userid = userId;
                worker.ReportProgress(2, "同步微信账号" + this.dgWechat[1, i].Value.ToString() + "\r\n");
                HttpResult result = wechatAction.getUserFromWechat(toWechatJsonUser.userid, Settings.Default.Wechat_Secret);


                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //表示访问成功，具体的大家就参考HttpStatusCode类
                    toWechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);
                    if (String.IsNullOrEmpty(toWechatJsonUser.userid))
                    {
                        isNewUser = true;
                    }

                    if (!String.IsNullOrEmpty(this.dgWechat[5, i].Value.ToString()) && this.dgWechat[5, i].Value.ToString().ToUpper()=="Y")
                    {
                        isDisableUser = true;
                    }

                  
                    toWechatJsonUser.position = this.dgWechat[0, i].Value.ToString();
                    toWechatJsonUser.name = this.dgWechat[1, i].Value.ToString();
                    toWechatJsonUser.weixinid = weixinid;
                    toWechatJsonUser.mobile = mobile;
                    toWechatJsonUser.email = email;
                    toWechatJsonUser.userid = userId;
                   
                   
                   

                    if (toWechatJsonUser.department == null)
                    {
                        toWechatJsonUser.department = new List<int>();
                    }

                    WechatUser fromWechatUser = wechatAction.getUserFromWechatByDepartment(Settings.Default.Wechat_Error_Department, Settings.Default.Wechat_Secret);
                    if (fromWechatUser.userlist.Count <= 1000)
                    {
                        toWechatJsonUser.department.Add(Settings.Default.Wechat_Error_Sub_Department_1);
                    }
                    else
                    {
                        if (fromWechatUser.userlist.Count > 1000 && fromWechatUser.userlist.Count <= 2000)
                        {
                           toWechatJsonUser.department.Add(Settings.Default.Wechat_Error_Sub_Department_2);
                        }
                        else
                        {
                            if (fromWechatUser.userlist.Count > 2000 && fromWechatUser.userlist.Count <= 3000)
                            {
                                toWechatJsonUser.department.Add(Settings.Default.Wechat_Error_Sub_Department_3);
                            }
                            else
                            {
                               toWechatJsonUser.department.Add(Settings.Default.Wechat_Error_Department);
                            }
                        }
                    }
                    var userData = new
                    {
                        userid = toWechatJsonUser.userid,
                        name = toWechatJsonUser.name,
                        department = toWechatJsonUser.department,
                        position = toWechatJsonUser.position,
                        mobile = mobile,
                        email = email,
                        weixinid = weixinid
                    };

                    string userJson = JsonConvert.SerializeObject(userData, Formatting.Indented);

                    if (!isNewUser)
                    {
                        result = wechatAction.updateUserToWechat(Settings.Default.Wechat_Secret, userJson);
                        
                        if (!String.IsNullOrEmpty(toWechatJsonUser.email))
                        {
                           // this.sendEmail(toWechatJsonUser.email);
                        }
                    }
                    else
                    {
                       
                        result = wechatAction.addUserToWechat(Settings.Default.Wechat_Secret, userJson);
                        ReturnMessage returnMessage1 = (ReturnMessage)JsonConvert.DeserializeObject(result.Html, typeof(ReturnMessage));
                        
                        if (returnMessage1 != null && returnMessage1.errcode.Equals( "0")&&!String.IsNullOrEmpty(toWechatJsonUser.email))
                        {
                           this.sendEmail(toWechatJsonUser.email);
                        }
                    }
                    if (isDisableUser)
                    {
                        result = wechatAction.deleteUserFromWechat(toWechatJsonUser.userid, Settings.Default.Wechat_Secret);
                    }
                   
                   ReturnMessage returnMessage= (ReturnMessage) JsonConvert.DeserializeObject(result.Html,typeof(ReturnMessage));
                   if (returnMessage != null && returnMessage.errcode != "0")
                   {
                       this.dgWechat[6, i].Value = returnMessage.getErrorDescrition(); ;
                   }
                   else
                   {
                       this.dgWechat[6, i].Value = "导入成功";
                   }
                }

            }
           // dgWechat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

           
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

            String message = mailAdapter.sendSingleEmail(databaseId, workspaceId, emailTypeId, Settings.Default.TripolisEmailId,mailData.sender, mailData.fromAddress, emailId, "Test", mailData.subject, content);


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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel格式|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "ImportWechat_Template.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportWechat_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName,true);

               
               
              
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
