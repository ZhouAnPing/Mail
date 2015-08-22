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


namespace ChinaUnion_Agent.ContactUs
{
    public partial class frmAgentContactImport : Form
    {
        public frmAgentContactImport()
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
                List<Row> agentContact = execelfile.Worksheet(0).ToList(); ;

                if (agentContact != null && agentContact.Count > 0)
                {
                   
                    this.btnImport.Enabled = true;
                    dgAgentContact.Rows.Clear();
                    dgAgentContact.Columns.Clear();
                    foreach (String coloumn in agentContact[0].ColumnNames)
                    {
                        this.dgAgentContact.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentContact.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentContact[i][0]))
                            continue;
                        dgAgentContact.Rows.Add();
                        DataGridViewRow row = dgAgentContact.Rows[i];
                        foreach (String coloumn in agentContact[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentContact[i][coloumn];
                        }

                    }
                }

                dgAgentContact.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgAgentContact.AutoResizeColumns();

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


            worker.ReportProgress(1, "开始导入代理商联系人...\r\n");

         
            
            //导入代理商
            AgentContactDao agentContactDao = new AgentContactDao();
            for (int i = 0; i < dgAgentContact.RowCount; i++)
            {
                AgentContact agentContact = new AgentContact();
                agentContact.agentNo = dgAgentContact[0, i].Value.ToString();
                agentContact.agentName = dgAgentContact[1, i].Value.ToString();
                agentContact.branchNo = dgAgentContact[2, i].Value.ToString();
                agentContact.branchName = dgAgentContact[3, i].Value.ToString();
                agentContact.area = dgAgentContact[4, i].Value.ToString();
                agentContact.zone = dgAgentContact[5, i].Value.ToString();
                agentContact.contactName = dgAgentContact[6, i].Value.ToString();
                agentContact.contactTel = dgAgentContact[7, i].Value.ToString();
                agentContact.contactEmail = dgAgentContact[8, i].Value.ToString();
                
                agentContactDao.Delete(agentContact.agentNo.Trim(), agentContact.branchNo.Trim());
                agentContactDao.Add(agentContact);

            }
            worker.ReportProgress(4, "导入代理商联系人完成...\r\n");
           
           

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
            saveFileDialog.FileName = "联系我们.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                File.Copy("./Template/ImportContactUs_Template.xlsx", saveFileDialog.FileName);
            }
        }


    }
}
