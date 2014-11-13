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
using TripolisDialogueAdapter.BO;
using TripolisDialogueAdapter.cn.tripolis.dialogue.reporting;

namespace ChinaUnion_Agent
{
    public partial class frmMailReport : Form
    {
        public frmMailReport()
        {
            InitializeComponent();
        }

        private void frmMailReport_Load(object sender, EventArgs e)
        {
           
            this.WindowState = FormWindowState.Maximized;
            this.cboFeeBatch.Items.Clear();
            //构造数据源（或从数据库中查询）
            DataTable ADt = new DataTable();
            DataColumn ADC1 = new DataColumn("ValueMember", typeof(string));
            DataColumn ADC2 = new DataColumn("DisplayMember", typeof(string));
            ADt.Columns.Add(ADC1);
            ADt.Columns.Add(ADC2);

            MailJobDao mailJobDao = new MailJobDao();

            IList<MailJob> mailJobList = mailJobDao.GetList("");

            foreach (MailJob mailJob in mailJobList)
            {
               // this.cboFeeBatch.Items.Add(mailJob.feeMonth + "|" + mailJob.subject + "|" + mailJob.sendTime);
                DataRow ADR = ADt.NewRow();
                ADR[0] = mailJob.mailJobId;
                ADR[1] = mailJob.feeMonth + "|" + mailJob.subject + "|" + mailJob.sendTime;
                ADt.Rows.Add(ADR);
            }            
            
            
            //进行绑定
            cboFeeBatch.DisplayMember = "DisplayMember";//控件显示的列名
            cboFeeBatch.ValueMember = "ValueMember";//控件值的列名
            cboFeeBatch.DataSource = ADt;

            if (String.IsNullOrEmpty(this.cboFeeBatch.Text))
            {
                this.btnQuery.Enabled = false;
            }


        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                String client = Settings.Default.TripolisClient;
                String userName = Settings.Default.TripoisUserName;
                String password = Settings.Default.TripolisPassword;



                ChinaUnionAdapter mailAdapter = new ChinaUnionAdapter(client, userName, password, null);

                String feeMonth = cboFeeBatch.Text.Substring(0, 7) + "-01";
                DateTime startTime = DateTime.Parse(feeMonth);
                DateTime endTime = startTime.AddMonths(1);

                ReportData reportData = mailAdapter.getRerportByJobId(this.cboFeeBatch.SelectedValue.ToString(), startTime, endTime);






                this.dgvSent.Rows.Clear();
                dgvSent.Columns.Clear();

                dgvSent.Columns.Add("邮件地址", "邮件地址");
                dgvSent.Columns.Add("代理商编号", "代理商编号");
                dgvSent.Columns.Add("代理商名称", "代理商名称");
                dgvSent.Columns.Add("联系人", "联系人");
                //已发送
                if (reportData != null && reportData.sent != null && reportData.sent.Length > 0)
                {


                    for (int i = 0; i < reportData.sent.Length; i++)
                    {
                        dgvSent.Rows.Add();
                        DataGridViewRow row = dgvSent.Rows[i];

                        for (int j = 0; j < dgvSent.ColumnCount; j++)
                        {
                            if (reportData.sent[i].contactFields != null)
                            {
                                row.Cells[j].Value = reportData.sent[i].contactFields[j].value;
                            }
                        }

                    }

                }
                this.tabSent.Text = "已发送(Sent)【" + dgvSent.RowCount + "】";



                this.dgvBounced.Rows.Clear();
                dgvBounced.Columns.Clear();
                dgvBounced.Columns.Add("邮件地址", "邮件地址");
                dgvBounced.Columns.Add("代理商编号", "代理商编号");
                dgvBounced.Columns.Add("代理商名称", "代理商名称");
                dgvBounced.Columns.Add("联系人", "联系人");
                dgvBounced.Columns.Add("退回时间", "退回时间");
                dgvBounced.Columns.Add("退回原因", "退回原因");

                //已退回
                if (reportData != null && reportData.bounced != null && reportData.bounced.Length > 0)
                {
                    ArrayList emailList = new ArrayList();

                    for (int i = 0; i < reportData.bounced.Length; i++)
                    {

                        if (reportData.bounced[i].contactFields != null && emailList.Contains(reportData.bounced[i].contactFields[0].value))
                        {
                            continue;
                        }
                        dgvBounced.Rows.Add();
                        DataGridViewRow row = dgvBounced.Rows[dgvBounced.RowCount - 1];
                        emailList.Add(reportData.bounced[i].contactFields[0].value);

                        for (int j = 0; j < dgvBounced.ColumnCount - 2; j++)
                        {
                            if (reportData.bounced[i].contactFields != null)
                            {
                                row.Cells[j].Value = reportData.bounced[i].contactFields[j].value;
                            }
                        }
                        row.Cells[4].Value = reportData.bounced[i].bouncedAt.ToString("yyyy-MM-dd hh:mm:ss");
                        row.Cells[5].Value = reportData.bounced[i].bounceCode + "-" + reportData.bounced[i].bounceCategoryDescription + "-" + reportData.bounced[i].bounceReason;
                    }

                    for (int i = 0; i < reportData.skipped.Length; i++)
                    {
                        dgvBounced.Rows.Add();
                        DataGridViewRow row = dgvBounced.Rows[dgvBounced.RowCount - 1];

                        for (int j = 0; j < dgvBounced.ColumnCount - 1; j++)
                        {
                            if (reportData.skipped[i].contactFields != null)
                            {
                                row.Cells[j].Value = reportData.skipped[i].contactFields[j].value;
                            }
                        }
                        row.Cells[4].Value = reportData.skipped[i].skippedAt.ToString("yyyy-MM-dd hh:mm:ss");
                    }

                }
                this.tabBounced.Text = "已退回(Bounced)【" + dgvBounced.RowCount + "】";

                ////已跳过
                //if (reportData != null && reportData.skipped != null && reportData.skipped.Length > 0)
                //{

                //    this.dgvSkipped.Rows.Clear();
                //    dgvSkipped.Columns.Clear();

                //    dgvSkipped.Columns.Add("邮件地址", "邮件地址");
                //    dgvSkipped.Columns.Add("代理商编号", "代理商编号");
                //    dgvSkipped.Columns.Add("代理商名称", "代理商名称");
                //    dgvSkipped.Columns.Add("联系人", "联系人");
                //    dgvSkipped.Columns.Add("跳过时间", "跳过时间");

                //    for (int i = 0; i < reportData.skipped.Length; i++)
                //    {
                //        dgvSkipped.Rows.Add();
                //        DataGridViewRow row = dgvSkipped.Rows[i];

                //        for (int j = 0; j < dgvSkipped.ColumnCount - 1; j++)
                //        {
                //            if (reportData.skipped[i].contactFields != null)
                //            {
                //                row.Cells[j].Value = reportData.skipped[i].contactFields[j].value;
                //            }
                //        }
                //        row.Cells[4].Value = reportData.skipped[i].skippedAt.ToString("yyyy-MM-dd hh:mm:ss");
                //    }

                //}
                //this.tabSkipped.Text = "已跳过(Skipped)【" + dgvSkipped.RowCount + "】";



                this.dgvOpened.Rows.Clear();
                dgvOpened.Columns.Clear();

                dgvOpened.Columns.Add("邮件地址", "邮件地址");
                dgvOpened.Columns.Add("代理商编号", "代理商编号");
                dgvOpened.Columns.Add("代理商名称", "代理商名称");
                dgvOpened.Columns.Add("联系人", "联系人");
                dgvOpened.Columns.Add("退回时间", "退回时间");
                dgvOpened.Columns.Add("打开IP地址", "打开IP地址");
                //已opened
                if (reportData != null && reportData.opened != null && reportData.opened.Length > 0)
                {
                    ArrayList emailList = new ArrayList();

                    for (int i = 0; i < reportData.opened.Length; i++)
                    {

                        if (reportData.opened[i].contact.contactFields != null && emailList.Contains(reportData.opened[i].contact.contactFields[0].value))
                        {
                            continue;
                        }
                        dgvOpened.Rows.Add();
                        DataGridViewRow row = dgvOpened.Rows[dgvOpened.RowCount - 1];
                        emailList.Add(reportData.opened[i].contact.contactFields[0].value);


                        for (int j = 0; j < dgvOpened.ColumnCount - 2; j++)
                        {
                            if (reportData.opened[i].contact.contactFields != null)
                            {
                                row.Cells[j].Value = reportData.opened[i].contact.contactFields[j].value;
                            }
                        }
                        row.Cells[4].Value = reportData.opened[i].openedAt.ToString("yyyy-MM-dd hh:mm:ss");
                        row.Cells[5].Value = reportData.opened[i].ip;
                    }

                }
                this.tabOpened.Text = "已打开(Opened)【" + dgvOpened.RowCount + "】";


                int RecipientsCnt = reportData.emailSummary.job.requestedNumberOfSend;


                int NotSentCnt = reportData.emailSummary.job.numberOfSkipped;

                int BounceCnt = reportData.emailSummary.softBounces + reportData.emailSummary.hardBounces;

                int SentCnt = RecipientsCnt - NotSentCnt - BounceCnt;

                int OpenCnt = reportData.emailSummary.uniqueOpens;
                int NotOpenCnt = SentCnt - OpenCnt;




                this.lblRecipients.Text = RecipientsCnt + "/100%";


                this.lblDelivered.Text = SentCnt + "/" + (((Double)SentCnt) / ((Double)RecipientsCnt)).ToString("p2");


                this.lblBounced.Text = BounceCnt + "/" + (((Double)BounceCnt) / ((Double)RecipientsCnt)).ToString("p2");
                this.lblNotSent.Text = NotSentCnt + "/" + (((Double)NotSentCnt) / ((Double)RecipientsCnt)).ToString("p2");


                if (dgvBounced.RowCount > 0)
                {
                    this.btnReSend.Enabled = true;
                }
                else
                {
                    this.btnReSend.Enabled = false;
                }


                this.dgvSent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.dgvOpened.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                // this.dgvSkipped.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.dgvBounced.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReSend_Click(object sender, EventArgs e)
        {
            frmMailReSend frmMailReSend = new frmMailReSend();
            frmMailReSend.ShowIcon = false;
            frmMailReSend.ShowInTaskbar = false;
            frmMailReSend.feeMonth = cboFeeBatch.Text.Substring(0, 7);

            for (int i = 0; i < this.dgvBounced.RowCount; i++)
            {
                frmMailReSend.htReSendMail.Add(dgvBounced[2,i].Value.ToString());
            }           
            frmMailReSend.ShowDialog();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
