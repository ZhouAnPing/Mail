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
                List<Row> agentWechatList = execelfile.Worksheet("代理商联系信息表").ToList(); ;

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

                //代理商信息
                List<Row> agentWechatNoDirectList = execelfile.Worksheet("非直供渠道联系信息表").ToList(); ;

                if (agentWechatNoDirectList != null && agentWechatNoDirectList.Count > 0)
                {

                    this.btnImport.Enabled = true;
                    dgAgentWechatAccountNoDirect.Rows.Clear();
                    dgAgentWechatAccountNoDirect.Columns.Clear();
                    foreach (String coloumn in agentWechatNoDirectList[0].ColumnNames)
                    {
                        this.dgAgentWechatAccountNoDirect.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentWechatNoDirectList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentWechatNoDirectList[i][0]))
                            continue;
                        dgAgentWechatAccountNoDirect.Rows.Add();
                        DataGridViewRow row = dgAgentWechatAccountNoDirect.Rows[i];
                        foreach (String coloumn in agentWechatNoDirectList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentWechatNoDirectList[i][coloumn];
                        }

                    }
                }

                //代理商信息
                List<Row> agentWechatDirectList = execelfile.Worksheet("直供渠道联系信息表").ToList(); ;

                if (agentWechatDirectList != null && agentWechatDirectList.Count > 0)
                {

                    this.btnImport.Enabled = true;
                    dgAgentWechatAccountDirect.Rows.Clear();
                    dgAgentWechatAccountDirect.Columns.Clear();
                    foreach (String coloumn in agentWechatDirectList[0].ColumnNames)
                    {
                        this.dgAgentWechatAccountDirect.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentWechatDirectList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentWechatDirectList[i][0]))
                            continue;
                        dgAgentWechatAccountDirect.Rows.Add();
                        DataGridViewRow row = dgAgentWechatAccountDirect.Rows[i];
                        foreach (String coloumn in agentWechatDirectList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentWechatDirectList[i][coloumn];
                        }

                    }
                }
                dgAgentWechatAccount.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgAgentWechatAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgAgentWechatAccount.AutoResizeColumns();

                dgAgentWechatAccountDirect.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgAgentWechatAccountDirect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgAgentWechatAccountDirect.AutoResizeColumns();

                dgAgentWechatAccountNoDirect.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgAgentWechatAccountNoDirect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgAgentWechatAccountNoDirect.AutoResizeColumns();

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

         
            
            //导入联系人
            this.saveGridData(this.dgAgentWechatAccount,"代理商联系人");
            this.saveGridData(this.dgAgentWechatAccountDirect,"直供渠道联系人");
            this.saveGridData(this.dgAgentWechatAccountNoDirect, "非直供渠道联系人");

            worker.ReportProgress(4, "导入联系人信息完成...\r\n");
           
           

            //MessageBox.Show("数据上传完毕");

        }

        private void saveGridData(DataGridView dg, String type)
        {
            ArrayList AgentNoList = new ArrayList();
            ArrayList WechatNoList = new ArrayList();

            for (int i = 0; i < dgAgentWechatAccount.RowCount; i++)
            {
                AgentNoList.Add(dgAgentWechatAccount[4, i].Value.ToString());
                if (!String.IsNullOrEmpty(dgAgentWechatAccount[7, i].Value.ToString()))
                {
                    WechatNoList.Add(dgAgentWechatAccount[7, i].Value.ToString());
                }
            }
            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            for (int i = 0; i < dg.RowCount; i++)
            {
                AgentWechatAccount agentWechatAccount = new AgentWechatAccount();
                int index = 0;
                agentWechatAccount.type = type;
                if (dg.Columns[0].HeaderText.Equals("区县"))
                {
                    agentWechatAccount.regionName = dg[index++, i].Value.ToString();
                    agentWechatAccount.branchNo = dg[index++, i].Value.ToString();
                    agentWechatAccount.branchName = dg[index++, i].Value.ToString();
                    //agentWechatAccount.type = "直供渠道联系人";
                }
                else if (dg.Columns[0].HeaderText.Equals("代理商编号"))
                {
                    agentWechatAccount.agentNo = dg[index++, i].Value.ToString();
                    agentWechatAccount.agentName = dg[index++, i].Value.ToString();
                    if (dg.Columns[2].HeaderText.Equals("渠道编码"))
                    {
                        agentWechatAccount.branchNo = dg[index++, i].Value.ToString();
                        agentWechatAccount.branchName = dg[index++, i].Value.ToString();

                    }
                }


                agentWechatAccount.contactEmail = dg[index++, i].Value.ToString();
                agentWechatAccount.contactId = dg[index++, i].Value.ToString();
                agentWechatAccount.contactName = dg[index++, i].Value.ToString();
                agentWechatAccount.contactTel = dg[index++, i].Value.ToString();
                agentWechatAccount.contactWechat = dg[index++, i].Value.ToString();

                agentWechatAccount.feeRight = dg[index++, i].Value.ToString();
                agentWechatAccount.policyRight = dg[index++, i].Value.ToString();
                agentWechatAccount.performanceRight = dg[index++, i].Value.ToString();
                agentWechatAccount.studyRight = dg[index++, i].Value.ToString();
                agentWechatAccount.complainRight = dg[index++, i].Value.ToString();
                agentWechatAccount.monitorRight = dg[index++, i].Value.ToString();
                agentWechatAccount.errorRight = dg[index++, i].Value.ToString();
                agentWechatAccount.contactRight = dg[index++, i].Value.ToString();


                if (String.IsNullOrEmpty(agentWechatAccount.branchNo))
                    agentWechatAccount.branchNo = "";
                if (AgentNoList.Contains(agentWechatAccount.contactId) && !type.Equals("代理商联系人"))
                {
                    continue;
                }
                if (WechatNoList.Contains(agentWechatAccount.contactWechat) && !type.Equals("代理商联系人"))
                {
                    continue;
                }

                agentWechatAccountDao.Delete(agentWechatAccount.contactId);
                agentWechatAccountDao.Add(agentWechatAccount);



            }
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
