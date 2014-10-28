using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using TripolisDialogueAdapter.cn.tripolis.dialogue.contactDatabase;
using LinqToExcel;
using System.Threading;
using TripolisDialogueAdapter;
using TripolisDialogueAdapter.Action;
using System.Configuration;
using System.IO;

namespace JobMailTools
{
    public partial class frmTripolisMail : Form
    {
        private String client;
        private String userName;
        private String password;
        String email = "";
        //TripolisConfig tripolisConfig = new TripolisConfig();
        EmailParam emailParam = new EmailParam();
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(frmTripolisMail));

        TripolisDialogueAdapter.DialogueService dialogueService;

        public frmTripolisMail()
        {
            InitializeComponent();

            MenuStripRecipients.Items[0].Click += new System.EventHandler(this.ExportRecipientExcel_Click);
            this.MenuStripSent.Items[0].Click += new System.EventHandler(this.ExportSentExcel_Click);
            MenuStripOpen.Items[0].Click += new System.EventHandler(this.ExportOpenExcel_Click);
            MenuStripClick.Items[0].Click += new System.EventHandler(this.ExportClickExcel_Click);
            MenuStripBounced.Items[0].Click += new System.EventHandler(this.ExportBouncedExcel_Click);

            this.dgvRecipients.ContextMenuStrip = this.MenuStripRecipients;
            this.dgvSent.ContextMenuStrip = this.MenuStripSent;
            this.dgvOpened.ContextMenuStrip = this.MenuStripOpen;
            this.dgvClicked.ContextMenuStrip = this.MenuStripClick;
            this.dgvBounced.ContextMenuStrip = this.MenuStripBounced;


            initConfiguration();



            backgroundWorker1.RunWorkerAsync(this);
        }

        private void btnLoadContact_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            // openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Excel(*.xls)|*.xls|CSV(*.csv)|*.csv|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;

                var execelfile = new ExcelQueryFactory(FileName);
                List<Row> table1 = execelfile.Worksheet(0).ToList();

                if (table1 != null && table1.Count > 0)
                {
                    dgContact.Rows.Clear();
                    dgContact.Columns.Clear();
                    foreach (String coloumn in table1[0].ColumnNames)
                    {
                        this.dgContact.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < table1.Count; i++)
                    {
                        dgContact.Rows.Add();
                        DataGridViewRow row = dgContact.Rows[i];
                        foreach (String coloumn in table1[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = table1[i][coloumn];
                        }

                    }
                }

            }


            this.pnlContact.ResetText();
            this.pnlContact.Text = "联系人信息(*)【" + dgContact.Rows.Count + "】";
            dgContact.AutoSize = true;
            dgContact.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void btnLoadContent_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            // openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "网页(*.html)|*.html|文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;

                this.txtBody.Text = System.IO.File.ReadAllText(FileName, Encoding.Default);
                this.pnlBody.Text = "邮件正文(*)";// +"【" + FileName + "】";
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //toolStripProgressBar1.Value = 0;


            errorProvider1.Clear();

            if (String.IsNullOrWhiteSpace(this.txtSender.Text))
            {
                errorProvider1.SetError(txtSender, "没有发件人！");
                return;

            }
            //if (String.IsNullOrWhiteSpace(this.txtBatch.Text))
            //{
            //    errorProvider1.SetError(txtBatch, "没有批次号！");
            //    return;

            //}
            if (String.IsNullOrWhiteSpace(this.txtSubject.Text))
            {
                errorProvider1.SetError(txtSubject, "没有邮件主题！");
                return;
            }
            if (this.dgContact.Rows.Count <= 0)
            {
                errorProvider1.SetError(linkContact, "没有收件人！");
                return;
            }

            if (String.IsNullOrWhiteSpace(this.txtBody.Text))
            {
                errorProvider1.SetError(this.linkBody, "没有邮件正文！");
                return;

            }

            this.btnSend.Enabled = false;
            this.btnLoadContact.Enabled = false;
            this.btnLoadContent.Enabled = false;
            String sequence = DateTime.Now.ToString("yyyyMMddhhmmss");


            emailParam = new EmailParam();
            emailParam.sender = this.txtSender.Text.Trim();
            emailParam.emailSubject = this.txtSubject.Text.Trim();
            emailParam.emailBody = this.txtBody.Text.Trim();
            emailParam.batchNo = this.txtSubject.Text.Trim() + "-" + sequence;
            TripolisConfig tripolisConfig = new TripolisConfig();
            // String directEmailId = JobMailTools.Properties.Settings.Default.DirectEmailId;
            tripolisConfig.contactDatabaseId = JobMailTools.Properties.Settings.Default.ContactDatabaseId;
            tripolisConfig.workspaceId = JobMailTools.Properties.Settings.Default.WorkspaceId;
            tripolisConfig.emailTypeId = JobMailTools.Properties.Settings.Default.EmailTypeId;
            tripolisConfig.directEmailId = JobMailTools.Properties.Settings.Default.DirectEmailId;
            tripolisConfig.EmailFileName = JobMailTools.Properties.Settings.Default.EmailFiledName;
            emailParam.tripolisConfig = tripolisConfig;
            emailParam.dataGrid = this.dgContact;

            this.toolbarUIBehavior.Value = 0;
            this.toolbarUIBehavior.Minimum = 0;
            this.toolbarUIBehavior.Maximum = 2 * dgContact.RowCount + 3;
            this.toolbarUIBehavior.Step = 1;           

            backgroundWorker2.RunWorkerAsync(this);



            //backgroundWorker2
            errorProvider1.Clear();
            //this.toolStripStatusLabel1.Text = "";
            Cursor.Current = Cursors.Default;

        }

        private void tabMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (tabMail.SelectedIndex == 1)
            {
                initConfiguration();
                DataTable dt = dialogueService.Report_GetInfo("", 5);
                //this.cboBatch.Items.Clear();
                cboBatch.DataSource = dt;
                cboBatch.ValueMember = "batchNo";
                cboBatch.DisplayMember = "batchNo";
                //foreach (ContactDatabase db in contactDatabases)
                //{
                //    ComboBoxItem cbxitem = new ComboBoxItem();
                //    cbxitem.Text = db.label;
                //    cbxitem.Value = db.id;
                //    this.cbxContact.Items.Add(cbxitem);
                //}

                refreshReportGrid();

            }
            else if (tabMail.SelectedIndex == 2)
            {
                refreshConfiguration();
            }
            Cursor.Current = Cursors.Default;
        }



        private void btnQuery_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //refreshAccessDB();
            refreshReportGrid();
            Cursor.Current = Cursors.Default;
        }

        //refresh UI
        #region
        private void initConfiguration()
        {
            client = JobMailTools.Properties.Settings.Default.client;
            userName = JobMailTools.Properties.Settings.Default.username;
            password = JobMailTools.Properties.Settings.Default.password;

            System.Net.WebProxy oWebProxy = null;

            //判断是否启用代理服务器            
            string strDomain;
            strDomain = JobMailTools.Properties.Settings.Default.proxyDomain;
            //域访问名
            string strUserName;
            strUserName = JobMailTools.Properties.Settings.Default.proxyUserName;
            //域访问密码
            string strPassWord;
            strPassWord = JobMailTools.Properties.Settings.Default.proxyPassword;
            //代理地址
            string strHost;
            strHost = JobMailTools.Properties.Settings.Default.proxyHost;
            //代理端口
            int strPort = 0; ;
            Int32.TryParse(JobMailTools.Properties.Settings.Default.proxyPort, out strPort);
            // int strPort = Convert.ToInt32(JobMailTools.Properties.Settings.Default.proxyPort);
            //设置代理
            if (!string.IsNullOrEmpty(strHost) && strPort > 0)
            {
                oWebProxy = new System.Net.WebProxy(strHost, strPort);
                //本地访问不需要通过代理
                oWebProxy.BypassProxyOnLocal = true;

                // 获取或设置提交给代理服务器进行身份验证的凭据
                if (!string.IsNullOrEmpty(strDomain))
                    oWebProxy.Credentials = new System.Net.NetworkCredential(strUserName, strPassWord, strDomain);

            }
            dialogueService = new DialogueService(client, userName, password, oWebProxy);
        }
        private void refreshReportGrid()
        {
            DataTable dt;
            String batchNo = this.cboBatch.Text.Trim();
            initConfiguration();
            dt = dialogueService.Report_GetInfo(batchNo, 0);
            this.dgvRecipients.DataSource = dt;

            dt = dialogueService.Report_GetInfo(batchNo, 1);
            this.dgvSent.DataSource = dt;


            dt = dialogueService.Report_GetInfo(batchNo, 2);
            this.dgvOpened.DataSource = dt;


            dt = dialogueService.Report_GetInfo(batchNo, 3);
            this.dgvClicked.DataSource = dt;

            dt = dialogueService.Report_GetInfo(batchNo, 4);
            this.dgvBounced.DataSource = dt;

            int RecipientsCnt = dgvRecipients.RowCount;

            int SentCnt = dgvSent.RowCount;
            int BounceCnt = dgvBounced.RowCount;
            int NotSentCnt = RecipientsCnt - SentCnt - BounceCnt;

            int OpenCnt = dgvOpened.RowCount;
            int NotOpenCnt = SentCnt - OpenCnt;

            int ClickCnt = dgvClicked.RowCount;
            int NotClickCnt = OpenCnt - ClickCnt;



            this.pnlRecipients.Text = "收件人(Recipients)【" + RecipientsCnt + "】";
            this.lblRecipients.Text = RecipientsCnt + "/100%";

            this.pnlSent.Text = "已发送(Sent)【" + SentCnt + "】";
            this.lblDelivered.Text = SentCnt + "/" + (((Double)SentCnt) / ((Double)RecipientsCnt)).ToString("p2");

            this.pnlBounced.Text = "退回(Bounced)【" + dgvBounced.RowCount + "】";
            this.lblBounced.Text = BounceCnt + "/" + (((Double)BounceCnt) / ((Double)RecipientsCnt)).ToString("p2");
            this.lblNotSent.Text = NotSentCnt + "/" + (((Double)NotSentCnt) / ((Double)RecipientsCnt)).ToString("p2");


            this.pnlOpen.Text = "已打开(Opened)【" + OpenCnt + "】";
            if (SentCnt > 0)
            {
                this.lblOpened.Text = OpenCnt + "/" + (((Double)OpenCnt) / ((Double)SentCnt)).ToString("p2");
                this.lblNotOpened.Text = NotOpenCnt + "/" + (NotOpenCnt / ((Double)SentCnt)).ToString("p2");
            }

            this.pnlClick.Text = "已点击(Clicked)【" + dgvClicked.RowCount + "】";
            if (OpenCnt > 0)
            {
                this.lblClicked.Text = ClickCnt + "/" + (((Double)ClickCnt) / ((Double)OpenCnt)).ToString("p2");
                this.lblNotClicked.Text = NotClickCnt + "/" + (NotClickCnt / ((Double)OpenCnt)).ToString("p2");
            }



            this.dgvRecipients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOpened.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvClicked.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBounced.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }
        private void refreshAccessDB()
        {
            initConfiguration();

            DateTime startTime = DateTime.Now.AddDays(-1);

            DateTime endTime = DateTime.Now;

            String result = dialogueService.SyncFeedbackInfo(JobMailTools.Properties.Settings.Default.ContactDatabaseId, startTime, endTime);

        }
        private void refreshConfiguration()
        {
            this.txtClient.Text = JobMailTools.Properties.Settings.Default.client;
            this.txtUserName.Text = JobMailTools.Properties.Settings.Default.username;
            this.txtPassword.Text = JobMailTools.Properties.Settings.Default.password;

            this.txtDBId.Text = JobMailTools.Properties.Settings.Default.ContactDatabaseId;
            this.txtWorkSpaceId.Text = JobMailTools.Properties.Settings.Default.WorkspaceId;
            this.txtEmailTypeId.Text = JobMailTools.Properties.Settings.Default.EmailTypeId;
            this.txtEmailId.Text = JobMailTools.Properties.Settings.Default.DirectEmailId;
            this.txtEmail.Text = JobMailTools.Properties.Settings.Default.EmailFiledName;
            this.txtReportRefreshInterval.Text = JobMailTools.Properties.Settings.Default.ReportRefreshInterval.ToString();
            this.txtMaxMailPerBatch.Text = JobMailTools.Properties.Settings.Default.PerBatchSize.ToString();
            this.txtBatchInterval.Text = JobMailTools.Properties.Settings.Default.PerBatchWaitInterval.ToString();
            this.txtMailInterval.Text = JobMailTools.Properties.Settings.Default.PerMailWaitInterval.ToString();

            this.txtHost.Text = JobMailTools.Properties.Settings.Default.proxyHost.ToString();

            this.txtPort.Text = JobMailTools.Properties.Settings.Default.proxyPort.ToString();

            this.txtDomain.Text = JobMailTools.Properties.Settings.Default.proxyDomain.ToString();

            this.txtProxyName.Text = JobMailTools.Properties.Settings.Default.proxyUserName.ToString();

            this.txtProxyPassword.Text = JobMailTools.Properties.Settings.Default.proxyPassword.ToString();
            //this.textBox1.Text =System.Net.WebProxy.GetDefaultProxy().s
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int temp = 0;
            errorProvider1.Clear();
            if (!Int32.TryParse(this.txtReportRefreshInterval.Text.Trim(), out temp))
            {
                this.errorProvider1.SetError(txtReportRefreshInterval, "请输入数字");
                return;
            }
            if (!Int32.TryParse(this.txtMaxMailPerBatch.Text.Trim(), out temp))
            {
                this.errorProvider1.SetError(txtMaxMailPerBatch, "请输入数字");
                return;
            }
            if (!Int32.TryParse(this.txtBatchInterval.Text.Trim(), out temp))
            {
                this.errorProvider1.SetError(txtBatchInterval, "请输入数字");
                return;
            }
            if (!Int32.TryParse(this.txtMailInterval.Text.Trim(), out temp))
            {
                this.errorProvider1.SetError(txtMailInterval, "请输入数字");
                return;
            }


            JobMailTools.Properties.Settings.Default.client = this.txtClient.Text.Trim();
            JobMailTools.Properties.Settings.Default.username = this.txtUserName.Text.Trim();
            JobMailTools.Properties.Settings.Default.password = this.txtPassword.Text.Trim(); ;

            JobMailTools.Properties.Settings.Default.ContactDatabaseId = this.txtDBId.Text.Trim();
            JobMailTools.Properties.Settings.Default.WorkspaceId = this.txtWorkSpaceId.Text.Trim();
            JobMailTools.Properties.Settings.Default.EmailTypeId = this.txtEmailTypeId.Text.Trim(); ;
            JobMailTools.Properties.Settings.Default.DirectEmailId = this.txtEmailId.Text.Trim(); ;
            JobMailTools.Properties.Settings.Default.EmailFiledName = this.txtEmail.Text.Trim();
            int tempReportRefreshInterval = JobMailTools.Properties.Settings.Default.ReportRefreshInterval;

            JobMailTools.Properties.Settings.Default.ReportRefreshInterval = Int32.Parse(this.txtReportRefreshInterval.Text.Trim());
            JobMailTools.Properties.Settings.Default.PerBatchSize = Int32.Parse(this.txtMaxMailPerBatch.Text.Trim());
            JobMailTools.Properties.Settings.Default.PerBatchWaitInterval = Int32.Parse(this.txtBatchInterval.Text.Trim());
            JobMailTools.Properties.Settings.Default.PerMailWaitInterval = Int32.Parse(this.txtMailInterval.Text.Trim());

            JobMailTools.Properties.Settings.Default.proxyHost = this.txtHost.Text.Trim();
            JobMailTools.Properties.Settings.Default.proxyPort = this.txtPort.Text.Trim();
            JobMailTools.Properties.Settings.Default.proxyDomain = this.txtDomain.Text.Trim(); ;
            JobMailTools.Properties.Settings.Default.proxyUserName = this.txtProxyName.Text.Trim(); ;
            JobMailTools.Properties.Settings.Default.proxyPassword = this.txtProxyPassword.Text.Trim();
            if (tempReportRefreshInterval != JobMailTools.Properties.Settings.Default.ReportRefreshInterval)
            {
                if (backgroundWorker1.IsBusy)
                {
                    MessageBox.Show("你调整了报表刷新间隔，需要重新启动应用程序才能生效");
                }
                else
                {
                    backgroundWorker1.CancelAsync();
                    backgroundWorker1.RunWorkerAsync(this);
                }
            }

            JobMailTools.Properties.Settings.Default.Save();

            errorProvider1.Clear();


            Cursor.Current = Cursors.Default;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            refreshConfiguration();
        }

        private void linkRecipients_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pnlRecipients.Expand = true;
            this.pnlSent.Expand = false;
            this.pnlOpen.Expand = false;
            this.pnlClick.Expand = false;
            this.pnlBounced.Expand = false;
        }

        private void linkDelivered_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pnlRecipients.Expand = false;
            this.pnlSent.Expand = true;
            this.pnlOpen.Expand = false;
            this.pnlClick.Expand = false;
            this.pnlBounced.Expand = false;
        }

        private void linkOpened_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pnlRecipients.Expand = false;
            this.pnlSent.Expand = false;
            this.pnlOpen.Expand = true;
            this.pnlClick.Expand = false;
            this.pnlBounced.Expand = false;
        }

        private void linkClicked_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pnlRecipients.Expand = false;
            this.pnlSent.Expand = false;
            this.pnlOpen.Expand = false;
            this.pnlClick.Expand = true;
            this.pnlBounced.Expand = false;
        }

        private void linkBounced_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pnlRecipients.Expand = false;
            this.pnlSent.Expand = false;
            this.pnlOpen.Expand = false;
            this.pnlClick.Expand = false;
            this.pnlBounced.Expand = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker bw = (BackgroundWorker)sender;
            frmTripolisMail win = (frmTripolisMail)e.Argument;
                       
            while (true)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                bw.ReportProgress(1, "后台开始更新邮件反馈信息...");

                refreshAccessDB();

                bw.ReportProgress(100, "后台结束更新邮件反馈信息.");

                Thread.Sleep(JobMailTools.Properties.Settings.Default.ReportRefreshInterval * 60 * 1000);

            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            string message = e.UserState.ToString();
            this.toolBarRefreshReport.Text = string.Format("{0}", message);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("抓取反馈错误,请确认网络正常连接或者正确设置了代理服务器");
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("抓取反馈中断");
            }
            else
            {
                MessageBox.Show("抓取反馈完成");
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            frmTripolisMail win = (frmTripolisMail)e.Argument;

            bw.ReportProgress(0, "准备上传邮件内容");
            initConfiguration();
            dialogueService.updateDirectEmail(emailParam.tripolisConfig.directEmailId,
                emailParam.emailSubject, emailParam.sender, emailParam.emailBody);
            bw.ReportProgress(1, "邮件内容完毕");

            int perBatchSize = JobMailTools.Properties.Settings.Default.PerBatchSize;
            int rowNo = 0;
            String contactStr = "";
            foreach (DataGridViewRow row in emailParam.dataGrid.Rows)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                rowNo++;
                //DataGridViewRow dataRow = dgContact.Rows[row];
                contactStr = "";
                for (int i = 0; i < emailParam.dataGrid.Columns.Count; i++)
                {
                    DataGridViewColumn col = emailParam.dataGrid.Columns[i];
                    contactStr = contactStr + col.Name + "#" + row.Cells[col.Name].Value;
                    if (i < emailParam.dataGrid.Columns.Count - 1)
                    {
                        contactStr = contactStr + ";";
                    }
                    if (col.Name.ToLower().Contains("email"))
                    {
                        email = row.Cells[col.Name].Value.ToString();
                    }

                }

                bw.ReportProgress(row.Index + 2, "准备发送邮件给" + email);
                initConfiguration();
                dialogueService.sendSingleEmail(emailParam.tripolisConfig, contactStr,
                    emailParam.batchNo, emailParam.sender, emailParam.emailSubject, emailParam.emailBody);
                bw.ReportProgress(row.Index + 2, "成功发送邮件给" + email);
                if (rowNo == emailParam.dataGrid.RowCount)
                {
                    ;
                }
                else
                    if (rowNo % perBatchSize > 0)
                    {
                        Thread.Sleep(JobMailTools.Properties.Settings.Default.PerMailWaitInterval * 1000);
                    }
                    else
                    {
                        Thread.Sleep(JobMailTools.Properties.Settings.Default.PerBatchWaitInterval * 1000);

                    }
            }
            bw.ReportProgress(1, "邮件:" + emailParam.emailSubject + "发送完成");
            backgroundWorker2.CancelAsync();
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            string message = e.UserState.ToString();
            this.toolbarUIBehavior.PerformStep();
            this.lblMessage.Text = string.Format("{0}", message);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("发送邮件错误,请确认网络正常连接或者正确设置了代理服务器");
                this.btnSend.Enabled = true;
                this.btnLoadContact.Enabled = true;
                this.btnLoadContent.Enabled = true;
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("邮件发送中断");
                this.btnSend.Enabled = true;
                this.btnLoadContact.Enabled = true;
                this.btnLoadContent.Enabled = true;
            }
            else
            {                
                MessageBox.Show("邮件发送完成");
                this.btnSend.Enabled = true;
                this.btnLoadContact.Enabled = true;
                this.btnLoadContent.Enabled = true;
                this.toolbarUIBehavior.Value = 0;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.btnSend.Enabled = true;
            this.btnLoadContact.Enabled = true;
            this.btnLoadContent.Enabled = true;
            this.txtSender.Text = "";
            this.txtSubject.Text = "";
            this.txtBody.Text = "";
            this.dgContact.Columns.Clear();
            this.dgContact.Rows.Clear();
            this.pnlContact.ResetText();
            this.pnlBody.ResetText();
            this.btnNew.Enabled = false;
        }

        private void btnQueryReal_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            refreshAccessDB();
            refreshReportGrid();
            Cursor.Current = Cursors.Default;
        }
        private void ExportRecipientExcel_Click(object sender, EventArgs e)
        {
            ExportCSV(dgvRecipients);
        }
        private void ExportSentExcel_Click(object sender, EventArgs e)
        {
            ExportCSV(dgvSent);
        }
        private void ExportOpenExcel_Click(object sender, EventArgs e)
        {
            ExportCSV(dgvOpened);
        }
        private void ExportClickExcel_Click(object sender, EventArgs e)
        {
            ExportCSV(dgvClicked);
        }
        private void ExportBouncedExcel_Click(object sender, EventArgs e)
        {
            ExportCSV(dgvBounced);
        }


        private void ExportCSV(DataGridView dg)
        {
            if (dg == null)
                return;

            if (dg.Rows.Count == 0)
            {
                MessageBox.Show("No data available!", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.FileName = null;
                saveFileDialog.Title = "Save path of the file to be exported";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stream myStream = saveFileDialog.OpenFile();
                    StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.UTF8);
                    string strLine = "";
                    try
                    {
                        //Write in the headers of the columns.
                        for (int i = 0; i < dg.ColumnCount; i++)
                        {
                            if (i > 0)
                                strLine += ",";
                            strLine += dg.Columns[i].HeaderText;
                        }
                        strLine.Remove(strLine.Length - 1);
                        sw.WriteLine(strLine);
                        strLine = "";
                        //Write in the content of the columns.
                        for (int j = 0; j < dg.Rows.Count; j++)
                        {
                            strLine = "";
                            for (int k = 0; k < dg.Columns.Count; k++)
                            {
                                if (k > 0)
                                    strLine += ",";
                                if (dg.Rows[j].Cells[k].Value == null)
                                    strLine += "";
                                else
                                {
                                    string m = dg.Rows[j].Cells[k].Value.ToString().Trim();
                                    strLine += m.Replace(",", "，");
                                }
                            }
                            strLine.Remove(strLine.Length - 1);
                            sw.WriteLine(strLine);
                            //Update the Progess Bar.
                            //toolStripProgressBar1.Value = 100 * (j + 1) / dataGridView1.Rows.Count;
                        }
                        sw.Close();
                        myStream.Close();
                        MessageBox.Show("Data has been exported to：" + saveFileDialog.FileName.ToString(), "Exporting Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // toolStripProgressBar1.Value = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Exporting Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnUploadContact_Click(object sender, EventArgs e)
        {
           

            String contactDatabaseId="MjU1OTI1NTm6UFiCZQIokg";
            String groupId = "NzQ5MDc3NDn2o5Y7f*cVSA";
            String fileName = "exportContactsInDatabase.csv";
            String reportReceiverAddress = "zapjx@hotmail.com";
            String ftpAccountId = "NTM0NTM0NTPaLKEuENgtAA";
            DateTime scheduleAt = DateTime.Now;

            dialogueService.importContactFromFTP(contactDatabaseId, groupId, fileName, reportReceiverAddress, ftpAccountId, scheduleAt);
        }
              

    }
}
