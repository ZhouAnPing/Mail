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
using ChinaUnion_Agent.Util;


namespace ChinaUnion_Agent.PerformanceForm
{
    public partial class frmAgentMonthPerformanceImport : Form
    {
        public String performanceType = MyConstant.DIRECT;
        public frmAgentMonthPerformanceImport()
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

                this.txtAgentPermanceFile.Text = FileName;
                this.txtAgentPermanceFile.Enabled = false;

                List<string> sheetNames=  execelfile.GetWorksheetNames().ToList();
               
               

               

             

                //代理商月绩效
                List<Row> agentPerformance = execelfile.Worksheet(0).ToList();

                if (agentPerformance != null && agentPerformance.Count > 0)
                {
                    this.grpAgentFee.Text = "绩效明细(" + agentPerformance.Count + ")";
                    this.btnImport.Enabled = true;
                    dgAgentPerformance.Rows.Clear();
                    dgAgentPerformance.Columns.Clear();
                    if (agentPerformance[0].ColumnNames.Count() > 103)
                    {
                        MessageBox.Show("Excel格式不正确，明细表的sheet列的数量太多.");
                        return;
                    }
                    foreach (String coloumn in agentPerformance[0].ColumnNames)
                    {
                        this.dgAgentPerformance.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentPerformance.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentPerformance[i][0]))
                            continue;
                        dgAgentPerformance.Rows.Add();
                        DataGridViewRow row = dgAgentPerformance.Rows[i];
                        foreach (String coloumn in agentPerformance[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentPerformance[i][coloumn];
                        }
                       

                    }
                }

             

              
               
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            ImportLog importLog = new ChinaUnion_BO.ImportLog();
            if (performanceType.Equals(MyConstant.NoDIRECT))
            {
                importLog.type = "AgentMonthPermanceNoDirect";
            }
            else
            {
                importLog.type = "AgentMonthPermanceDirect";
            }
            importLog.import_month = this.dtMonth.Value.ToString("yyyy-MM");
            ImportLogDao importLogDao = new ChinaUnion_DataAccess.ImportLogDao();
            if (importLogDao.Get(importLog) != null)
            {
                if (MessageBox.Show("本月绩效已经导入，是否需要再次导入？", "数据导入", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
            }
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
            this.dtMonth.Value = DateTime.Now.AddMonths(-1);
        }

        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码
           

            worker.ReportProgress(1, "开始导入月度绩效...\r\n");

            //导入代理商佣金
            AgentMonthPerformanceDao agentMonthPerformanceDao = new AgentMonthPerformanceDao();
            for (int i = 0; i < dgAgentPerformance.RowCount; i++)
            {
                AgentMonthPerformance agentMonthPerformance = new AgentMonthPerformance();
                agentMonthPerformance.month = this.dtMonth.Value.ToString("yyyy-MM");
                agentMonthPerformance.branchNo = dgAgentPerformance[0, i].Value.ToString();
                agentMonthPerformance.branchName = dgAgentPerformance[1, i].Value.ToString();
                int index = 2;
                if (performanceType.Equals(MyConstant.NoDIRECT))
                {
                    agentMonthPerformance.agentNo = dgAgentPerformance[2, i].Value.ToString();
                    agentMonthPerformance.agentName = dgAgentPerformance[3, i].Value.ToString();
                    index = 4;
                }
                else
                {
                    agentMonthPerformance.agentNo = dgAgentPerformance[0, i].Value.ToString();
                    agentMonthPerformance.agentName = dgAgentPerformance[1, i].Value.ToString();
                    index = 2;
                }

                for (int j = index; j <= 101 && j < dgAgentPerformance.Columns.Count; j++)
                {

                    FieldInfo feeNameField = agentMonthPerformance.GetType().GetField("feeName" + (j - index+1));
                    FieldInfo feeField = agentMonthPerformance.GetType().GetField("fee" + (j - index + 1));
                   

                    String feeNameFieldValue = dgAgentPerformance.Columns[j].HeaderCell.Value.ToString();
                    String feeFieldValue = dgAgentPerformance[j, i].Value.ToString();
                    if (feeFieldValue.Trim().Equals("0") || String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        feeFieldValue = null;
                    }
                    feeNameField.SetValue(agentMonthPerformance, feeNameFieldValue);
                    feeField.SetValue(agentMonthPerformance, feeFieldValue);

                }

                agentMonthPerformanceDao.Delete(agentMonthPerformance);
                agentMonthPerformanceDao.Add(agentMonthPerformance);

            }

            worker.ReportProgress(2, "导入月度绩效完成...\r\n");


            ImportLog importLog = new ChinaUnion_BO.ImportLog();
            if (performanceType.Equals(MyConstant.NoDIRECT))
            {
                importLog.type = "AgentMonthPermanceNoDirect";
            }
            else
            {
                importLog.type = "AgentMonthPermanceDirect";
            }
            importLog.import_month = this.dtMonth.Value.ToString("yyyy-MM");
            ImportLogDao importLogDao = new ChinaUnion_DataAccess.ImportLogDao();
            importLogDao.Delete(importLog);
            importLogDao.Add(importLog);

        
           

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
            saveFileDialog.FileName = "ImportCommissio_Template.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportCommissio_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName,true);
            }
        }


    }
}
