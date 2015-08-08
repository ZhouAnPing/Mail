using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using ChinaUnion_Agent.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TripolisDialogueAdapter;
using ChinaUnion_Agent.Wechat;
using System.Reflection;

namespace ChinaUnion_Agent
{
    public partial class frmMailReSend : Form
    {
        private String subject;
        public String feeMonth;
        public ArrayList htReSendMail = new ArrayList();
        public frmMailReSend()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            frmMailSubject frmMailSubject = new ChinaUnion_Agent.frmMailSubject();
            frmMailSubject.subject = "重新发送:" + Settings.Default.MailSubject + "(" + feeMonth + ")";
            DialogResult dialogResult = frmMailSubject.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.subject = frmMailSubject.subject;
                //异步执行开始
                worker.RunWorkerAsync();
                frmProgress frm = new frmProgress(this.worker);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
                frm.Close();
            }

        }

        BackgroundWorker worker;
        private void frmMailReSend_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);


            AgentFeeDao agentFeeDao = new AgentFeeDao();
            IList<AgentFee> agentFeeList = agentFeeDao.GetList(this.feeMonth);

            if (agentFeeList != null && agentFeeList.Count > 0)
            {
                dgAgentFee.Rows.Clear();
                dgAgentFee.Columns.Clear();

                this.dgAgentFee.Columns.Add("代理商编号", "代理商编号");
                this.dgAgentFee.Columns.Add("代理商名称", "代理商名称");
                this.dgAgentFee.Columns.Add("代理商类型", "代理商类型");
                this.dgAgentFee.Columns.Add("代理商类型说明", "代理商类型说明");
                this.dgAgentFee.Columns.Add("联系人邮件", "联系人邮件");
                this.dgAgentFee.Columns.Add("联系人名称", "联系人名称");
                this.dgAgentFee.Columns.Add("告知单编号", "告知单编号");



                for (int i = 0; i < agentFeeList.Count; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 1; j <= 100; j++)
                        {
                            FieldInfo feeNameField = agentFeeList[i].GetType().GetField("feeName" + j);
                            // FieldInfo feeField = agentFeeList[i].GetType().GetField("fee" + j);

                            String feeNameFieldValue = feeNameField.GetValue(agentFeeList[i]) == null ? null : feeNameField.GetValue(agentFeeList[i]).ToString();
                            // String feeFieldValue = feeField.GetValue(agentFeeList[i]) == null ? null : feeField.GetValue(agentFeeList[i]).ToString(); ;

                            if (!String.IsNullOrEmpty(feeNameFieldValue) && !String.IsNullOrWhiteSpace(feeNameFieldValue))
                            {
                                dgAgentFee.Columns.Add(feeNameFieldValue, feeNameFieldValue);
                            }
                        }


                        this.dgAgentFee.Columns.Add("总计", "总计");
                        this.dgAgentFee.Columns.Add("开票金额", "开票金额");
                        this.dgAgentFee.Columns.Add("过往开票金额", "过往开票金额");

                    }

                    if (!htReSendMail.Contains(agentFeeList[i].agent.agentName))
                    {
                        continue;
                    }
                    dgAgentFee.Rows.Add();
                    DataGridViewRow row = dgAgentFee.Rows[dgAgentFee.RowCount-1];

                    row.Cells[0].Value = agentFeeList[i].agentNo;
                    row.Cells[1].Value = agentFeeList[i].agent.agentName;
                    row.Cells[2].Value = agentFeeList[i].agent.agentType;
                    row.Cells[3].Value = agentFeeList[i].agent.agentTypeComment.Replace(",","、").Replace("#","%");
                    row.Cells[4].Value = agentFeeList[i].agent.contactEmail;
                    row.Cells[5].Value = agentFeeList[i].agent.contactName;
                    row.Cells[6].Value = agentFeeList[i].agentFeeSeq;




                    int feeColIndex = 6;
                    int fixColCount = 7;

                    for (int j = 1; j <= 100; j++)
                    {
                        // FieldInfo feeNameField = agentFeeList[i].GetType().GetField("feeName" + j);
                        FieldInfo feeField = agentFeeList[i].GetType().GetField("fee" + j);

                        //  String feeNameFieldValue = feeNameField.GetValue(agentFeeList[i]) == null ? null : feeNameField.GetValue(agentFeeList[i]).ToString();
                        String feeFieldValue = feeField.GetValue(agentFeeList[i]) == null ? null : feeField.GetValue(agentFeeList[i]).ToString(); ;

                        if (dgAgentFee.Columns.Count >= fixColCount + j)
                        {
                            row.Cells[feeColIndex + j].Value = feeFieldValue;
                        }
                    }

                    row.Cells[dgAgentFee.Columns.Count - 3].Value = agentFeeList[i].feeTotal;
                    row.Cells[dgAgentFee.Columns.Count - 2].Value = agentFeeList[i].invoiceFee;
                    row.Cells[dgAgentFee.Columns.Count - 1].Value = agentFeeList[i].preInvoiceFee;

                }
            }
            showHtml(0);
          

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
            Cursor.Current = Cursors.WaitCursor;
            if (dgAgentFee.RowCount <= 0)
            {
                return;
            }

            try{
             MailData mailData = new MailData();


            StringBuilder sbAgent = new StringBuilder();

            worker.ReportProgress(1, "准备数据...\r\n");
            WechatAction wechatAction = new WechatAction();
            for (int i = 0; i < dgAgentFee.RowCount; i++)
            {
                StringBuilder sb = new StringBuilder();
                String url = String.Format(Settings.Default.Wechat_Message, dgAgentFee[0, i].Value.ToString(), feeMonth);
                wechatAction.sendTextMessageToWechat(dgAgentFee[0, i].Value.ToString(), feeMonth + url, Settings.Default.Wechat_Secret, Settings.Default.Wechat_Agent_AppId);

                sb.Append("agent_no#").Append(dgAgentFee[0, i].Value == null ? "" : dgAgentFee[0, i].Value.ToString()).Append(",");
                sb.Append("agent_name#").Append(dgAgentFee[1, i].Value == null ? "" : dgAgentFee[1, i].Value.ToString().Replace(",", "、").Replace("#", "%")).Append(",");
                sb.Append("agent_type#").Append(dgAgentFee[2, i].Value == null ? "" : dgAgentFee[2, i].Value.ToString().Replace(",", "、").Replace("#", "%")).Append(",");
                sb.Append("agent_type_comment#").Append(dgAgentFee[3, i].Value == null ? "" : dgAgentFee[3, i].Value.ToString().Replace(",", "、").Replace("#", "%")).Append(",");
                sb.Append("email#").Append(dgAgentFee[4, i].Value == null ? "" : dgAgentFee[4, i].Value.ToString());

                mailData.ContactJsonList.Add(sb.ToString());
            }

            String client = Settings.Default.TripolisClient;
            String userName = Settings.Default.TripoisUserName;
            String password = Settings.Default.TripolisPassword;


            mailData.fromAddress = Settings.Default.MailFromAddress;
            mailData.replyAddress = Settings.Default.MailReplyAddress;
            mailData.sender = Settings.Default.MailSender;
            mailData.subject = this.subject;
            ChinaUnionAdapter mailAdapter = new ChinaUnionAdapter(client, userName, password, null);
            String databaseId = Settings.Default.TripolisDBId;
            String workspaceId = Settings.Default.TripolisWorkspaceId;
            String emailId = Settings.Default.TripolisDirectEmailId;
            worker.ReportProgress(1, "发送邮件...\r\n");
            String message = mailAdapter.sendBatchMail(databaseId, workspaceId, emailId, mailData, this.dtFeeMonth.Value);
            if (message.Contains("OK:"))
            {
                String mailJobId = message.Substring(3);

                MailJob mailJob = new MailJob();
                mailJob.feeMonth = feeMonth;
                mailJob.mailJobId = mailJobId;
                mailJob.subject = mailData.subject;

                mailJob.sendTime = this.dtFeeMonth.Value.ToString("yyyy-MM-dd hh:mm:ss");


                MailJobDao mailJobDao = new MailJobDao();
                mailJobDao.Delete(mailJob);
                mailJobDao.Add(mailJob);

             //  WechatAction wechatAction = new WechatAction();
              //  wechatAction.sendMessageToWechat("@all",this.dtFeeMonth.Value.ToString("yyyy-MM") + Settings.Default.Wechat_Message);
                MessageBox.Show("邮件重新发送成功");

            }
            else
            {
                MessageBox.Show(message);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor.Current = Cursors.Default;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showHtml(int rowIndex)
        {
            StringBuilder sbAgent = new StringBuilder();

            //    int seq = 1;

            HashSet<String> category = new HashSet<string>();
            Dictionary<String, Dictionary<String, String>> CategoryMap = new Dictionary<string, Dictionary<String, String>>();
            //按结账科目分类
            for (int j = 7; j < dgAgentFee.ColumnCount - 3; j++)
            {
                if (dgAgentFee[j, rowIndex].Value != null && !String.IsNullOrEmpty(dgAgentFee[j, rowIndex].Value.ToString()) && !dgAgentFee[j, rowIndex].Value.ToString().Equals("0"))
                {
                    String headText = dgAgentFee.Columns[j].HeaderText;
                    int locationIndex = headText.IndexOf("-");
                    int endIndex = headText.IndexOf("（");
                    if (locationIndex == -1)
                    {
                        locationIndex = headText.IndexOf("-");
                    }
                    if (endIndex == -1)
                    {
                        endIndex = headText.IndexOf("(");
                    }
                    String key = dgAgentFee.Columns[j].HeaderText.Substring(locationIndex + 1);
                    if (endIndex != -1)
                    {
                        key = dgAgentFee.Columns[j].HeaderText.Substring(locationIndex + 1, endIndex - locationIndex - 1);
                    }
                    String value = dgAgentFee[j, rowIndex].Value.ToString();
                    if (!CategoryMap.ContainsKey(key))
                    {
                        Dictionary<String, String> valueMap = new Dictionary<string, string>();
                        valueMap.Add(headText, value);
                        CategoryMap.Add(key, valueMap);
                    }
                    else
                    {
                        Dictionary<String, String> valueMap = CategoryMap[key];
                        valueMap.Add(headText, value);
                    }
                }
            }


            int index = 1;
            foreach (String itemKey in CategoryMap.Keys)
            {
                Dictionary<String, String> valueMap = CategoryMap[itemKey];
                float subTotal = 0;
                foreach (String value in valueMap.Values)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        subTotal = subTotal + float.Parse(value);
                    }
                }

                sbAgent.Append("<tr><td colspan=3/></tr>");
                sbAgent.Append("<tr>");
                sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                sbAgent.Append("");
                sbAgent.Append("</td>");
                sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                sbAgent.Append(itemKey);
                sbAgent.Append("</td>");
                sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: right; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                sbAgent.Append(subTotal);
                sbAgent.Append("</td>");

                sbAgent.Append("</tr>");

                foreach (String subKey in valueMap.Keys)
                {
                    // sbAgent.Append("<tr>");
                    sbAgent.Append("<tr>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append(index.ToString());
                    sbAgent.Append("</td>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append("&nbsp;&nbsp;&nbsp;&nbsp;").Append(subKey);
                    sbAgent.Append("</td>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: right; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append(valueMap[subKey]);
                    sbAgent.Append("</td>");

                    sbAgent.Append("</tr>");

                    index++;
                }

            }

            //for (int j = 7; j < dgAgentFee.ColumnCount - 1; j++)
            //{

            //    if (dgAgentFee[j, rowIndex].Value!=null&&!String.IsNullOrEmpty(dgAgentFee[j, rowIndex].Value.ToString()) && !dgAgentFee[j, rowIndex].Value.ToString().Equals("0"))
            //    { 
            //        sbAgent.Append("<tr>");
            //        sbAgent.Append("<tr>");
            //        sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            //        sbAgent.Append(seq.ToString());
            //        sbAgent.Append("</td>");
            //        sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            //        sbAgent.Append(dgAgentFee.Columns[j].HeaderCell.Value.ToString());
            //        sbAgent.Append("</td>");
            //        sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            //        sbAgent.Append(dgAgentFee[j, rowIndex].Value.ToString());
            //        sbAgent.Append("</td>");

            //        sbAgent.Append("</tr>");
            //        seq++;
            //    }
            //}




            //sbAgent.Append("<tr>");
            sbAgent.Append("<tr><td colspan=3/></tr>");

            sbAgent.Append("<tr>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(index.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(dgAgentFee.Columns[dgAgentFee.Columns.Count - 3].HeaderCell.Value.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: right; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(dgAgentFee[dgAgentFee.Columns.Count - 3, rowIndex].Value.ToString());
            sbAgent.Append("</td>");

            sbAgent.Append("</tr>");

            index++;

            sbAgent.Append("<tr><td colspan=3/></tr>");

            sbAgent.Append("<tr>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(index.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(dgAgentFee.Columns[dgAgentFee.Columns.Count - 2].HeaderCell.Value.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: right; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            if (dgAgentFee[dgAgentFee.Columns.Count - 2, rowIndex].Value != null)
            {
                sbAgent.Append(dgAgentFee[dgAgentFee.Columns.Count - 2, rowIndex].Value.ToString());
            }
            sbAgent.Append("</td>");

            sbAgent.Append("</tr>");


            index++;

            sbAgent.Append("<tr><td colspan=3/></tr>");

            sbAgent.Append("<tr>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(index.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(dgAgentFee.Columns[dgAgentFee.Columns.Count - 1].HeaderCell.Value.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: bold; text-align: right; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            if (dgAgentFee[dgAgentFee.Columns.Count - 1, rowIndex].Value != null)
            {
                sbAgent.Append(dgAgentFee[dgAgentFee.Columns.Count - 1, rowIndex].Value.ToString());
            }
            sbAgent.Append("</td>");

            sbAgent.Append("</tr>");






            StringBuilder sb1 = new StringBuilder();
            String HeadFilePath = Application.StartupPath + @"\html\head.html";
            sb1.Append(System.IO.File.ReadAllText(HeadFilePath, Encoding.UTF8));

            sb1.Append(sbAgent.ToString());
            String FooterFilePath = Application.StartupPath + @"\html\footer.html";
            sb1.Append(System.IO.File.ReadAllText(FooterFilePath, Encoding.UTF8));


            sb1.Replace("${contact.agent_fee_seq!}", dgAgentFee[6, rowIndex].Value == null ? "" : dgAgentFee[6, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_no!}", dgAgentFee[0, rowIndex].Value == null ? "" : dgAgentFee[0, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_name!}", dgAgentFee[1, rowIndex].Value == null ? "" : dgAgentFee[1, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_type!}", dgAgentFee[2, rowIndex].Value == null ? "" : dgAgentFee[2, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_fee_month!}", this.feeMonth);
            sb1.Replace("${contact.agent_type_comment!}", dgAgentFee[3, rowIndex].Value == null ? "" : dgAgentFee[3, rowIndex].Value.ToString());

            sb1.Replace("${currentdate.date?string(\"yyyy年 M月 d日\")}", DateTime.Now.ToString("yyyy年MM月dd日"));

            webBrowser1.DocumentText = sb1.ToString();
            webBrowser1.Document.Encoding = "gb2312";

            webBrowser1.Show();
        }


        int currentRowIndex = 0;
        private void dgAgentFee_SelectionChanged(object sender, EventArgs e)
        {
            if (dgAgentFee.CurrentRow != null && dgAgentFee.CurrentRow.Index != currentRowIndex)
            {
                currentRowIndex = dgAgentFee.CurrentRow.Index;
                showHtml(currentRowIndex);
            }
        }

    }
}
