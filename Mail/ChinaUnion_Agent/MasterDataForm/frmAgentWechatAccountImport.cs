using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using LinqToExcel;
using System.Threading;

using System.Configuration;
using System.IO;
using System.Reflection;


namespace ChinaUnion_Agent.MasterDataForm
{
    public partial class frmAgentWechatAccountImport : Form
    {
        public frmAgentWechatAccountImport()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            // openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|Excel 2000-2003(*.xls)|*.xls|CSV(*.csv)|*.csv|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string FileName = openFileDialog.FileName;

                var execelfile = new ExcelQueryFactory(FileName);

                this.txtContact.Text = FileName;
                this.txtContact.Enabled = false;

                List<string> sheetNames=  execelfile.GetWorksheetNames().ToList();
               
                
                //代理商信息
                List<Row> agentWechatList = execelfile.Worksheet(0).ToList(); ;

                if (agentWechatList != null && agentWechatList.Count > 0)
                {
                   
                    this.btnImport.Enabled = true;
                    dgAgentWechatAccount.Rows.Clear();
                    dgAgentWechatAccount.Columns.Clear();
                    foreach (String coloumn in agentWechatList[0].ColumnNames)
                    {
                        this.dgAgentWechatAccount.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentWechatList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentWechatList[i][0]))
                            continue;
                        dgAgentWechatAccount.Rows.Add();
                        DataGridViewRow row = dgAgentWechatAccount.Rows[i];
                        foreach (String coloumn in agentWechatList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentWechatList[i][coloumn];
                        }

                    }
                }

                dgAgentWechatAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgAgentWechatAccount.AutoResizeColumns();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

           
            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();
            this.btnImport.Enabled = false;
           

        }

        BackgroundWorker worker; 
        private void frmAgentImport_Load(object sender, EventArgs e)
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


            worker.ReportProgress(1, "开始导入联系人信息...\r\n");

         
            
            //导入代理商
            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            for (int i = 0; i < dgAgentWechatAccount.RowCount; i++)
            {
                AgentWechatAccount agentWechatAccount = new AgentWechatAccount();
                int index = 0;
                agentWechatAccount.type = "代理商联系人";
                if (dgAgentWechatAccount.Columns[0].HeaderText.Equals("区县"))
                {
                    agentWechatAccount.regionName = dgAgentWechatAccount[index++, i].Value.ToString();
                    agentWechatAccount.type = "直供渠道联系人";
                }
                if (dgAgentWechatAccount.Columns[2].HeaderText.Equals("渠道编码"))
                {
                    agentWechatAccount.type = "非直供渠道联系";
                }
                agentWechatAccount.agentNo = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.agentName = dgAgentWechatAccount[index++, i].Value.ToString();
                if (dgAgentWechatAccount.Columns[2].HeaderText.Equals("渠道编码") || dgAgentWechatAccount.Columns[0].HeaderText.Equals("区县"))
                {
                    agentWechatAccount.branchNo = dgAgentWechatAccount[index++, i].Value.ToString();
                    agentWechatAccount.branchName = dgAgentWechatAccount[index++, i].Value.ToString();
                }
                agentWechatAccount.contactEmail = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.contactId = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.contactName = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.contactTel = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.contactWechat = dgAgentWechatAccount[index++, i].Value.ToString();

                agentWechatAccount.feeRight = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.policyRight = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.performanceRight = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.studyRight = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.complainRight = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.monitorRight = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.errorRight = dgAgentWechatAccount[index++, i].Value.ToString();
                agentWechatAccount.contactRight = dgAgentWechatAccount[index++, i].Value.ToString();


                if (String.IsNullOrEmpty(agentWechatAccount.branchNo))
                    agentWechatAccount.branchNo = "";

                agentWechatAccountDao.Delete(agentWechatAccount.contactId);
                agentWechatAccountDao.Add(agentWechatAccount);

            }
            worker.ReportProgress(4, "导入联系人信息完成...\r\n");
           
           

            //MessageBox.Show("数据上传完毕");

        }


        /// <summary>
        /// 事件: 异步执行完成后 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("数据上传完毕。", "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            saveFileDialog.FileName = "代理商联系信息表.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                File.Copy("./Template/ImportWechat_V1_Template.xlsx", saveFileDialog.FileName);
            }
        }


    }
}
