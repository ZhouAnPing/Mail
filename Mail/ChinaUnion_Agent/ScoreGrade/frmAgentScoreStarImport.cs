﻿using ChinaUnion_BO;
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
    public partial class frmAgentScoreStarImport : Form
    {
        public frmAgentScoreStarImport()
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
               
                
                //星级信息
                List<Row> agentStar = execelfile.Worksheet(0).ToList(); ;

                if (agentStar != null && agentStar.Count > 0)
                {
                   
                    this.btnImport.Enabled = true;
                    dgAgentStar.Rows.Clear();
                    dgAgentStar.Columns.Clear();
                    foreach (String coloumn in agentStar[0].ColumnNames)
                    {
                        this.dgAgentStar.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentStar.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentStar[i][0]))
                        {
                            continue;
                        }
                        dgAgentStar.Rows.Add();
                        DataGridViewRow row = dgAgentStar.Rows[i];
                        foreach (String coloumn in agentStar[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentStar[i][coloumn];
                        }

                    }
                }

                //星级信息
                List<Row> agentScore = execelfile.Worksheet(1).ToList(); ;

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

                dgAgentStar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgAgentStar.AutoResizeColumns();

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


            worker.ReportProgress(1, "开始导入星级...\r\n");
            
            //导入星级
            AgentStarDao agentStarDao = new AgentStarDao();
            for (int i = 0; i < dgAgentStar.RowCount; i++)
            {
                AgentStar agentStar = new AgentStar();
                agentStar.dateTime = dgAgentStar[0, i].Value.ToString();
                agentStar.agentNo = dgAgentStar[1, i].Value.ToString();
                agentStar.agentName = dgAgentStar[2, i].Value.ToString();
                agentStar.branchNo = dgAgentStar[3, i].Value.ToString();
                agentStar.branchName = dgAgentStar[4, i].Value.ToString();                
                agentStar.star = dgAgentStar[5, i].Value.ToString();

                agentStarDao.Delete(agentStar.agentNo.Trim(), agentStar.branchNo.Trim(), agentStar.dateTime.Trim());
                agentStarDao.Add(agentStar);

            }
            worker.ReportProgress(2, "导入星级完成...\r\n");


            worker.ReportProgress(3, "开始导入积分...\r\n");

            //导入星级
            AgentScoreDao agentScoreDao = new AgentScoreDao();
            for (int i = 0; i < dgAgentStar.RowCount; i++)
            {
                AgentScore agentScore = new AgentScore();
                agentScore.dateTime = dgAgentStar[0, i].Value.ToString();
                agentScore.agentNo = dgAgentStar[1, i].Value.ToString();
                agentScore.agentName = dgAgentStar[2, i].Value.ToString();
                agentScore.branchNo = dgAgentStar[3, i].Value.ToString();
                agentScore.branchName = dgAgentStar[4, i].Value.ToString();
                agentScore.score = dgAgentStar[5, i].Value.ToString();

                agentScoreDao.Delete(agentScore.agentNo.Trim(), agentScore.branchNo.Trim(), agentScore.dateTime.Trim());
                agentScoreDao.Add(agentScore);

            }
            worker.ReportProgress(4, "导入积分完成...\r\n");
           
           

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
