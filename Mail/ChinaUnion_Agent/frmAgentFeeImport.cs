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
using ChinaUnion_Agent.Wechat;
using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;


namespace ChinaUnion_Agent
{
    public partial class frmAgentFeeImport : Form
    {
        public frmAgentFeeImport()
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
                if (!sheetNames.Contains("明细表"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:明细表的sheet.");
                    return;
                }
               

            

                //代理商佣金明细
                List<Row> agentFee = execelfile.Worksheet("明细表").ToList();

                if (agentFee != null && agentFee.Count > 0)
                {
                    this.grpAgentFee.Text = "月度佣金明细信息(" + agentFee.Count + ")";
                    this.btnImport.Enabled = true;
                    dgAgentFee.Rows.Clear();
                    dgAgentFee.Columns.Clear();
                    if (agentFee[0].ColumnNames.Count() > 103)
                    {
                        MessageBox.Show("Excel格式不正确，明细表的sheet列的数量太多.");
                        return;
                    }
                    foreach (String coloumn in agentFee[0].ColumnNames)
                    {
                        this.dgAgentFee.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentFee.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentFee[i][0]))
                            continue;
                        dgAgentFee.Rows.Add();
                        DataGridViewRow row = dgAgentFee.Rows[i];
                        foreach (String coloumn in agentFee[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentFee[i][coloumn];
                        }
                      

                    }
                }

               
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            ImportLog importLog = new ChinaUnion_BO.ImportLog();
            importLog.type = "Agent";
            importLog.import_month = this.dtFeeMonth.Value.ToString("yyyy-MM");
            ImportLogDao importLogDao = new ChinaUnion_DataAccess.ImportLogDao();
            if (importLogDao.Get(importLog) != null)
            {
                if (MessageBox.Show("本月佣金已经导入，是否需要再次导入？", "数据导入", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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
           

            worker.ReportProgress(1, "开始导入代理商佣金...\r\n");

            //导入代理商佣金
            AgentFeeDao agentFeeDao = new AgentFeeDao();
            for (int i = 0; i < dgAgentFee.RowCount; i++)
            {
                AgentFee agentFee = new AgentFee();
                agentFee.agentFeeMonth = this.dtFeeMonth.Value.ToString("yyyy-MM");
                agentFee.agentNo = dgAgentFee[0, i].Value.ToString();
                agentFee.agentName= dgAgentFee[1, i].Value.ToString();
                agentFee.agentFeeSeq = agentFee.agentNo + this.dtFeeMonth.Value.ToString("yyyyMM") + String.Format("{0:D5}", i+1);
                agentFee.feeTotal = dgAgentFee[dgAgentFee.Columns.Count - 3, i].Value.ToString();
                agentFee.invoiceFee = dgAgentFee[dgAgentFee.Columns.Count - 2, i].Value.ToString();
                agentFee.preInvoiceFee = dgAgentFee[dgAgentFee.Columns.Count - 1, i].Value.ToString();

                for (int j = 2; j <= 101 && j < dgAgentFee.Columns.Count-3; j++)
                {

                    FieldInfo feeNameField = agentFee.GetType().GetField("feeName" + (j-1));
                    FieldInfo feeField = agentFee.GetType().GetField("fee" + (j-1));

                    String feeNameFieldValue = dgAgentFee.Columns[j].HeaderCell.Value.ToString();
                    String feeFieldValue = dgAgentFee[j, i].Value.ToString();
                    if (feeFieldValue.Trim().Equals("0") || String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        feeFieldValue = null;
                    }
                    feeNameField.SetValue(agentFee, feeNameFieldValue);
                    feeField.SetValue(agentFee, feeFieldValue);

                }

                agentFeeDao.Delete(agentFee);
                agentFeeDao.Add(agentFee);

            }
            WechatAction wechatAction = new WechatAction();
            wechatAction.sendTextMessageToWechat("@all", this.dtFeeMonth.Value.ToString("yyyy年MM月") + "佣金已发布，请通过底部菜单查询佣金详情", Settings.Default.Wechat_Secret, MyConstant.APP_Payment);

            worker.ReportProgress(2, "导入代理商佣金完成...\r\n");
           
           

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
