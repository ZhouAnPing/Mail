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
using ChinaUnion_Agent.Wechat;
using ChinaUnion_Agent.Properties;


namespace ChinaUnion_Agent.PerformanceForm
{
    public partial class frmAgentDailyPerformanceImport : Form
    {
        public String performanceType = MyConstant.DIRECT;
        public frmAgentDailyPerformanceImport()
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
               
               

               

             

                //非直供
                List<Row> agentPerformanceNoDirectList = execelfile.Worksheet("非直供渠道").ToList();

                if (agentPerformanceNoDirectList != null && agentPerformanceNoDirectList.Count > 0)
                {
                    this.grpNoDirectAgentFee.Text = "绩效明细(" + agentPerformanceNoDirectList.Count + ")";
                    this.btnImport.Enabled = true;
                    dgAgentPerformanceNoDirect.Rows.Clear();
                    dgAgentPerformanceNoDirect.Columns.Clear();
                    if (agentPerformanceNoDirectList[0].ColumnNames.Count() > 103)
                    {
                        MessageBox.Show("Excel格式不正确，明细表的sheet列的数量太多.");
                        return;
                    }
                    foreach (String coloumn in agentPerformanceNoDirectList[0].ColumnNames)
                    {
                        this.dgAgentPerformanceNoDirect.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentPerformanceNoDirectList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentPerformanceNoDirectList[i][0]))
                            continue;
                        dgAgentPerformanceNoDirect.Rows.Add();
                        DataGridViewRow row = dgAgentPerformanceNoDirect.Rows[i];
                        foreach (String coloumn in agentPerformanceNoDirectList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentPerformanceNoDirectList[i][coloumn];
                        }
                       

                    }
                }



                //直供
                List<Row> agentPerformanceDirectList = execelfile.Worksheet("直供渠道").ToList();

                if (agentPerformanceDirectList != null && agentPerformanceDirectList.Count > 0)
                {
                    this.groupBox1.Text = "绩效明细(" + agentPerformanceDirectList.Count + ")";
                    this.btnImport.Enabled = true;
                    dgAgentPerformanceDirect.Rows.Clear();
                    dgAgentPerformanceDirect.Columns.Clear();
                    if (agentPerformanceDirectList[0].ColumnNames.Count() > 103)
                    {
                        MessageBox.Show("Excel格式不正确，明细表的sheet列的数量太多.");
                        return;
                    }
                    foreach (String coloumn in agentPerformanceDirectList[0].ColumnNames)
                    {
                        this.dgAgentPerformanceDirect.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentPerformanceDirectList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentPerformanceDirectList[i][0]))
                            continue;
                        dgAgentPerformanceDirect.Rows.Add();
                        DataGridViewRow row = dgAgentPerformanceDirect.Rows[i];
                        foreach (String coloumn in agentPerformanceDirectList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentPerformanceDirectList[i][coloumn];
                        }


                    }
                }

                dgAgentPerformanceDirect.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgAgentPerformanceDirect.AutoResizeColumns();

                dgAgentPerformanceNoDirect.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgAgentPerformanceNoDirect.AutoResizeColumns();
              
               
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
            this.dtDay.Value = DateTime.Now.AddDays(-1);
        }

        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码


            worker.ReportProgress(1, "开始导入日绩效...\r\n");

            //导入非直供
            AgentDailyPerformanceDao agentDailyPerformanceDao = new AgentDailyPerformanceDao();
            for (int i = 0; i < dgAgentPerformanceNoDirect.RowCount; i++)
            {
                AgentDailyPerformance agentDailyPerformance = new AgentDailyPerformance();
                agentDailyPerformance.date = this.dtDay.Value.ToString("yyyy-MM-dd");
                agentDailyPerformance.branchNo = dgAgentPerformanceNoDirect[0, i].Value.ToString();
                agentDailyPerformance.branchName = dgAgentPerformanceNoDirect[1, i].Value.ToString();
                int index = 4;

                agentDailyPerformance.agentNo = dgAgentPerformanceNoDirect[2, i].Value.ToString();
                agentDailyPerformance.agentName = dgAgentPerformanceNoDirect[3, i].Value.ToString();
                agentDailyPerformance.type = "非直供渠道";

                for (int j = index; j <= 101 && j < dgAgentPerformanceNoDirect.Columns.Count; j++)
                {

                    FieldInfo feeNameField = agentDailyPerformance.GetType().GetField("feeName" + (j - index + 1));
                    FieldInfo feeField = agentDailyPerformance.GetType().GetField("fee" + (j - index + 1));

                    String feeNameFieldValue = dgAgentPerformanceNoDirect.Columns[j].HeaderCell.Value.ToString();
                    String feeFieldValue = dgAgentPerformanceNoDirect[j, i].Value.ToString();
                    if (feeFieldValue.Trim().Equals("0") || String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        feeFieldValue = null;
                    }
                    feeNameField.SetValue(agentDailyPerformance, feeNameFieldValue);
                    feeField.SetValue(agentDailyPerformance, feeFieldValue);

                }

                agentDailyPerformanceDao.Delete(agentDailyPerformance);
                agentDailyPerformanceDao.Add(agentDailyPerformance);

            }


            //导入直供
            //AgentDailyPerformanceDao agentDailyPerformanceDao = new AgentDailyPerformanceDao();
            for (int i = 0; i < dgAgentPerformanceDirect.RowCount; i++)
            {
                AgentDailyPerformance agentDailyPerformance = new AgentDailyPerformance();
                agentDailyPerformance.type = "直供渠道";
                agentDailyPerformance.date = this.dtDay.Value.ToString("yyyy-MM-dd");
                agentDailyPerformance.branchNo = dgAgentPerformanceDirect[0, i].Value.ToString();
                agentDailyPerformance.branchName = dgAgentPerformanceDirect[1, i].Value.ToString();
                int index = 2;

               // agentDailyPerformance.agentNo = dgAgentPerformanceDirect[0, i].Value.ToString();
                //agentDailyPerformance.agentName = dgAgentPerformanceDirect[1, i].Value.ToString();



                for (int j = index; j <= 101 && j < dgAgentPerformanceDirect.Columns.Count; j++)
                {

                    FieldInfo feeNameField = agentDailyPerformance.GetType().GetField("feeName" + (j - index + 1));
                    FieldInfo feeField = agentDailyPerformance.GetType().GetField("fee" + (j - index + 1));

                    String feeNameFieldValue = dgAgentPerformanceDirect.Columns[j].HeaderCell.Value.ToString();
                    String feeFieldValue = dgAgentPerformanceDirect[j, i].Value.ToString();
                    if (feeFieldValue.Trim().Equals("0") || String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        feeFieldValue = null;
                    }
                    feeNameField.SetValue(agentDailyPerformance, feeNameFieldValue);
                    feeField.SetValue(agentDailyPerformance, feeFieldValue);

                }

                agentDailyPerformanceDao.Delete(agentDailyPerformance);
                agentDailyPerformanceDao.Add(agentDailyPerformance);

            }

            worker.ReportProgress(2, "导入日绩效完成...\r\n");

            WechatAction wechatAction = new WechatAction();
            wechatAction.sendTextMessageToWechat("@all", this.dtDay.Value.ToString("yyyy年MM月dd日") + "业绩已发布，请通过底部菜单查询当日业绩详情", Settings.Default.Wechat_Secret, MyConstant.APP_Performace);


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
            saveFileDialog.FileName = "销售业绩统计导入模板.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportPerformance_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName,true);
            }
        }


    }
}
