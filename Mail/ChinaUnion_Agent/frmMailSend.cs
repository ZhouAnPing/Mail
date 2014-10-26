using ChinaUnion_Agent.BO;
using ChinaUnion_Agent.Dao;
using ChinaUnion_Agent.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TripolisDialogueAdapter;

namespace ChinaUnion_Agent
{
    public partial class frmMailSend : Form
    {

        public DateTime schduleAt = DateTime.Now;
        public DataGridView dgvTemp;
        public frmMailSend()
        {
            InitializeComponent();
        }

        BackgroundWorker worker;



        private void frmMailSend_Load(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);



            dgAgentFee.Rows.Clear();
            dgAgentFee.Columns.Clear();
            foreach (DataGridViewColumn column in dgvTemp.Columns)
            {
                dgAgentFee.Columns.Add(column.Name, column.Name);
            }

            for (int i = 0; i < dgvTemp.Rows.Count; i++)
            {

                dgAgentFee.Rows.Add();
                DataGridViewRow row = dgAgentFee.Rows[i];
                for (int j = 0; j < dgvTemp.Columns.Count; j++)
                {
                    row.Cells[j].Value = dgvTemp.Rows[i].Cells[j].Value;
                }

            }



            showHtml(0);
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
            try
            {
                String client = Settings.Default.TripolisClient;
                String userName = Settings.Default.TripoisUserName;
                String password = Settings.Default.TripolisPassword;

                ChinaUnionAdapter mailAdapter = new ChinaUnionAdapter(client, userName, password, null);
                MailData mailData = new MailData();
                mailData.fromAddress = Settings.Default.MailFromAddress;
                mailData.replyAddress = Settings.Default.MailReplyAddress;
                mailData.sender = Settings.Default.MailSender;
                mailData.subject = Settings.Default.MailSubject + "(" + this.dateTimePicker1.Value.ToString("yyyy-MM") + ")";

                String databaseId = Settings.Default.TripolisDBId;
                String workspaceId = Settings.Default.TripolisWorkspaceId;
                String emailId = Settings.Default.TripolisDirectEmailId;
                worker.ReportProgress(1, "准备数据...\r\n");

                for (int i = 0; i < dgAgentFee.RowCount; i++)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("agent_no#").Append(dgAgentFee[0, i].Value.ToString()).Append(",");
                    sb.Append("agent_name#").Append(dgAgentFee[1, i].Value.ToString()).Append(",");
                    sb.Append("agent_type#").Append(dgAgentFee[2, i].Value.ToString()).Append(",");
                    sb.Append("agent_type_comment#").Append(dgAgentFee[3, i].Value.ToString()).Append(",");
                    sb.Append("email#").Append(dgAgentFee[4, i].Value.ToString()).Append(",");
                    sb.Append("contact_name#").Append(dgAgentFee[5, i].Value.ToString()).Append(",");
                    sb.Append("agent_fee_seq#").Append(dgAgentFee[6, i].Value.ToString()).Append(",");

                    for (int j = 7; j < dgAgentFee.ColumnCount - 1; j++)
                    {
                        int index = j - 6;
                        sb.Append("fee_name").Append(index.ToString()).Append("#").Append(dgAgentFee.Columns[j].HeaderCell.Value.ToString()).Append(",");
                        sb.Append("fee").Append(index.ToString()).Append("#").Append(dgAgentFee[j, i].Value.ToString()).Append(",");

                    }



                    sb.Append("fee_total#").Append(dgAgentFee[dgAgentFee.Columns.Count - 1, i].Value.ToString());

                    mailData.ContactJsonList.Add(sb.ToString());

                    Cursor.Current = Cursors.Default;
                }

                worker.ReportProgress(1, "发送邮件...\r\n");
                String message = mailAdapter.sendBatchMail(databaseId, workspaceId, emailId, mailData, dateTimePicker1.Value);
                if (message.Contains("OK:"))
                {
                    String mailJobId = message.Substring(3);

                    MailJob mailJob = new MailJob();
                    mailJob.feeMonth = this.dateTimePicker1.Value.ToString("yyyy-MM");
                    mailJob.mailJobId = mailJobId;
                    mailJob.subject = mailData.subject;
                    if (dateTimePicker1.Value != null)
                    {
                        mailJob.sendTime = dateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm:ss");
                    }

                    MailJobDao mailJobDao = new MailJobDao();
                    mailJobDao.Delete(mailJob);
                    mailJobDao.Add(mailJob);

                    MessageBox.Show("邮件发送成功");

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

            //this.Close();
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
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();

        }

        private void showHtml(int rowIndex)
        {
            StringBuilder sbAgent = new StringBuilder();

            int seq = 1;





            for (int j = 7; j < dgAgentFee.ColumnCount - 1; j++)
            {



                if (!String.IsNullOrEmpty(dgAgentFee[j, rowIndex].Value.ToString()))
                {
                    sbAgent.Append("<tr>");
                    sbAgent.Append("<tr>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append(seq.ToString());
                    sbAgent.Append("</td>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append(dgAgentFee.Columns[j].HeaderCell.Value.ToString());
                    sbAgent.Append("</td>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append(dgAgentFee[j, rowIndex].Value.ToString());
                    sbAgent.Append("</td>");

                    sbAgent.Append("</tr>");
                    seq++;
                }




            }




            sbAgent.Append("<tr>");
            sbAgent.Append("<tr>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(seq.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(dgAgentFee.Columns[dgAgentFee.Columns.Count - 1].HeaderCell.Value.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(dgAgentFee[dgAgentFee.Columns.Count - 1, rowIndex].Value.ToString());
            sbAgent.Append("</td>");

            sbAgent.Append("</tr>");






            StringBuilder sb1 = new StringBuilder();
            sb1.Append(System.IO.File.ReadAllText("./html/head.html", Encoding.UTF8));

            sb1.Append(sbAgent.ToString());
            sb1.Append(System.IO.File.ReadAllText("./html/footer.html", Encoding.UTF8));


            sb1.Replace("${contact.agent_fee_seq!}", dgAgentFee[6, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_no!}", dgAgentFee[0, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_name!}", dgAgentFee[1, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_type!}", dgAgentFee[2, rowIndex].Value.ToString());

            sb1.Replace("${contact.agent_type_comment!}", dgAgentFee[3, rowIndex].Value.ToString());

            sb1.Replace("${currentdate.date?string(\"yyyy年 M月 d日\")}", DateTime.Now.ToString("yyyy年MM月dd日"));

            webBrowser1.DocumentText = sb1.ToString();
            webBrowser1.Document.Encoding = "gb2312";

            webBrowser1.Show();
        }

        int currentRowIndex = 0;
        private void dgAgentFee_SelectionChanged(object sender, EventArgs e)
        {
            if (dgAgentFee.CurrentRow != null & dgAgentFee.CurrentRow.Index != currentRowIndex)
            {
                currentRowIndex = dgAgentFee.CurrentRow.Index;
                showHtml(currentRowIndex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSendTestMail_Click(object sender, EventArgs e)
        {
            try
            {
                frmMailAddress frmMailAddress = new frmMailAddress();
                frmMailAddress.ShowDialog();
                String email = frmMailAddress.email;
                if (!String.IsNullOrEmpty(email) && !String.IsNullOrWhiteSpace(email))
                {

                    Cursor.Current = Cursors.WaitCursor;

                    String client = Settings.Default.TripolisClient;
                    String userName = Settings.Default.TripoisUserName;
                    String password = Settings.Default.TripolisPassword;

                    ChinaUnionAdapter mailAdapter = new ChinaUnionAdapter(client, userName, password, null);
                    MailData mailData = new MailData();
                    mailData.fromAddress = Settings.Default.MailFromAddress;
                    mailData.replyAddress = Settings.Default.MailReplyAddress;
                    mailData.sender = Settings.Default.MailSender;
                    mailData.subject = "测试:" + Settings.Default.MailSubject + "(" + this.dateTimePicker1.Value.ToString("yyyy-MM") + ")";

                    String databaseId = Settings.Default.TripolisDBId;
                    String workspaceId = Settings.Default.TripolisWorkspaceId;
                    String emailTypeId = Settings.Default.TripolisEmailTypeId;

                    String message = mailAdapter.sendSingleEmail(databaseId, workspaceId, emailTypeId, mailData.sender, mailData.fromAddress, email, mailData.subject, this.webBrowser1.DocumentText);

                    if (message.Contains("OK:"))
                    {

                        MessageBox.Show("邮件发送完成");

                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                    Cursor.Current = Cursors.Default;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
