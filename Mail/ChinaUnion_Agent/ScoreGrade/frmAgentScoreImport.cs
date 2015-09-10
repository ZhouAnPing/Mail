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


namespace ChinaUnion_Agent.ScoreGrade
{
    public partial class frmAgentScoreImport : Form
    {
        public frmAgentScoreImport()
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
               
               

                //积分信息
                List<Row> agentScore = execelfile.Worksheet(0).ToList(); ;

                if (agentScore != null && agentScore.Count > 0)
                {

                    this.btnImport.Enabled = true;
                    dgAgentScore.Rows.Clear();
                    dgAgentScore.Columns.Clear();
                    foreach (String coloumn in agentScore[0].ColumnNames)
                    {
                        this.dgAgentScore.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentScore.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentScore[i][0]))
                        {
                            continue;
                        }
                        dgAgentScore.Rows.Add();
                        DataGridViewRow row = dgAgentScore.Rows[i];
                        foreach (String coloumn in agentScore[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentScore[i][coloumn];
                        }

                    }
                }


                dgAgentScore.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgAgentScore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgAgentScore.AutoResizeColumns();

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




            worker.ReportProgress(3, "开始导入积分...\r\n");

            //导入积分
            AgentScoreDao agentScoreDao = new AgentScoreDao();
            for (int i = 0; i < dgAgentScore.RowCount; i++)
            {
                AgentScore agentScore = new AgentScore();
                agentScore.dateTime = dgAgentScore[0, i].Value.ToString();
                agentScore.agentNo = dgAgentScore[1, i].Value.ToString();
                agentScore.agentName = dgAgentScore[2, i].Value.ToString();
                agentScore.branchNo = dgAgentScore[3, i].Value.ToString();
                agentScore.branchName = dgAgentScore[4, i].Value.ToString();
                agentScore.score = dgAgentScore[5, i].Value.ToString();
                agentScore.standardScore = dgAgentScore[6, i].Value.ToString();

                agentScoreDao.Delete(agentScore.agentNo.Trim(), agentScore.branchNo.Trim(), agentScore.dateTime.Trim());
                agentScoreDao.Add(agentScore);
                if (!String.IsNullOrEmpty(agentScore.agentNo))
                {
                    worker.ReportProgress(4, "正在导入代理商:" + agentScore.agentNo + "积分...\r\n");
                }
                else
                {
                    worker.ReportProgress(4, "正在导入渠道:" + agentScore.branchNo + "积分...\r\n");
                }

            }
            worker.ReportProgress(5, "导入积分完成...\r\n");
           
           

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
            saveFileDialog.FileName = "积分模板.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                File.Copy("./Template/ImportScore_Template.xlsx", saveFileDialog.FileName);
            }
        }


    }
}
