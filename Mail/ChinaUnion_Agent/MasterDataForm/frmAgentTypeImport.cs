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
    public partial class frmAgentTypeImport : Form
    {
        public frmAgentTypeImport()
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

                this.txtAgent.Text = FileName;
                this.txtAgent.Enabled = false;

                List<string> sheetNames=  execelfile.GetWorksheetNames().ToList();
               
                if (!sheetNames.Contains("代理商渠道类型"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:代理商渠道类型的sheet.");
                    return;
                }
                if (!sheetNames.Contains("说明格式"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:说明格式的sheet.");
                    return;
                }

                ArrayList AgentNoList = new ArrayList();
                StringBuilder NotExistAgentNo = new StringBuilder();

             

                //代理商渠道类型
                List<Row> agentType = execelfile.Worksheet("代理商渠道类型").ToList(); ;

                if (agentType != null && agentType.Count > 0)
                {
                    this.btnImport.Enabled = true;
                    dgAgentType.Rows.Clear();
                    dgAgentType.Columns.Clear();
                    foreach (String coloumn in agentType[0].ColumnNames)
                    {
                        this.dgAgentType.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentType.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentType[i][0]))
                            continue;
                        dgAgentType.Rows.Add();
                        DataGridViewRow row = dgAgentType.Rows[i];
                        foreach (String coloumn in agentType[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentType[i][coloumn];
                        }
                        if (!AgentNoList.Contains(agentType[i][0].ToString()))
                        {
                            NotExistAgentNo.AppendFormat("代理商渠道类型中代理商编号:{0}不存在", agentType[i][0].ToString()).AppendLine();
                        }

                    }
                }

              

                //代理商渠道类型说明
                List<Row> agentTypeComment = execelfile.Worksheet("说明格式").ToList(); ;

                if (agentTypeComment != null && agentTypeComment.Count > 0)
                {
                    this.btnImport.Enabled = true;
                    dgAgentTypeComment.Rows.Clear();
                    dgAgentTypeComment.Columns.Clear();
                    foreach (String coloumn in agentTypeComment[0].ColumnNames)
                    {
                        this.dgAgentTypeComment.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentTypeComment.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentTypeComment[i][0]))
                            continue;
                        dgAgentTypeComment.Rows.Add();
                        DataGridViewRow row = dgAgentTypeComment.Rows[i];
                        foreach (String coloumn in agentTypeComment[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentTypeComment[i][coloumn];
                        }

                    }
                }


                this.dgAgentType.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                this.dgAgentType.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgAgentType.AutoResizeColumns();
                this.dgAgentTypeComment.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                this.dgAgentTypeComment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgAgentTypeComment.AutoResizeColumns();
              
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
        private void frmAgentFeeImport_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            this.dtFeeMonth.Value = DateTime.Now.AddMonths(-1);
        }

        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码
           

          
            worker.ReportProgress(5, "开始导入代理商类型...\r\n");

            //导入代理商类型
            AgentTypeDao agentTypeDao = new AgentTypeDao();
            for (int i = 0; i < dgAgentType.RowCount; i++)
            {
                AgentType agentType = new AgentType();
                agentType.agentNo = dgAgentType[0, i].Value.ToString();
                agentType.agentType = dgAgentType[1, i].Value.ToString();
                agentType.agentFeeMonth = this.dtFeeMonth.Value.ToString("yyyy-MM");
                agentTypeDao.Delete(agentType);
                agentTypeDao.Add(agentType);
            }

          
            worker.ReportProgress(6, "导入代理商类型完成...\r\n");
            worker.ReportProgress(7, "开始导入代理商类型说明...\r\n");


            //导入代理商类型说明
            AgentTypeCommentDao agentTypeCommentDao = new AgentTypeCommentDao();
            for (int i = 0; i < dgAgentTypeComment.RowCount; i++)
            {
                AgentTypeComment agentTypeComment = new AgentTypeComment();

                agentTypeComment.agentType = dgAgentTypeComment[0, i].Value.ToString();
                agentTypeComment.agentTypeComment = dgAgentTypeComment[1, i].Value.ToString();
                agentTypeComment.agentFeeMonth = this.dtFeeMonth.Value.ToString("yyyy-MM");
                agentTypeCommentDao.Delete(agentTypeComment);
                agentTypeCommentDao.Add(agentTypeComment);
            }

            ImportLog importLog = new ChinaUnion_BO.ImportLog();
            importLog.type="Agent";
            importLog.import_month = this.dtFeeMonth.Value.ToString("yyyy-MM");
            ImportLogDao importLogDao = new ChinaUnion_DataAccess.ImportLogDao();
            importLogDao.Delete(importLog);
            importLogDao.Add(importLog);
            worker.ReportProgress(8, "导入代理商类型完成...\r\n");
           

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
            saveFileDialog.FileName = "代理商渠道类型.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportAgentType_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName,true);
            }
        }


    }
}
