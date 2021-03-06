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
using ChinaUnion_Agent.Wechat;
using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;


namespace ChinaUnion_Agent.ScoreGrade
{
    public partial class frmAgentBonusImport : Form
    {
        public frmAgentBonusImport()
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
                List<Row> agentBonus = execelfile.Worksheet(0).ToList(); ;

                if (agentBonus != null && agentBonus.Count > 0)
                {
                   
                    this.btnImport.Enabled = true;
                    dgAgentBonus.Rows.Clear();
                    dgAgentBonus.Columns.Clear();
                    foreach (String coloumn in agentBonus[0].ColumnNames)
                    {
                        this.dgAgentBonus.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentBonus.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentBonus[i][0]))
                        {
                            continue;
                        }
                        dgAgentBonus.Rows.Add();
                        DataGridViewRow row = dgAgentBonus.Rows[i];
                        foreach (String coloumn in agentBonus[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentBonus[i][coloumn];
                        }

                    }
                }

                dgAgentBonus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgAgentBonus.AutoResizeColumns();

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
            this.dtFeeMonth.Value = DateTime.Now.AddMonths(-1);
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


            worker.ReportProgress(1, "开始导入红包...\r\n");



            AgentBonusDao agentBonusDao = new AgentBonusDao();
            for (int i = 0; i < dgAgentBonus.RowCount; i++)
            {
                AgentBonus agentBonus = new AgentBonus();
                agentBonus.month = this.dtFeeMonth.Value.ToString("yyyyMM");



                agentBonus.agentNo = dgAgentBonus[0, i].Value.ToString();
                agentBonus.agentName = dgAgentBonus[1, i].Value.ToString();

                int index = 2;
                for (int j = index; j <= 101 && j < dgAgentBonus.Columns.Count; j++)
                {

                    FieldInfo feeNameField = agentBonus.GetType().GetField("feeName" + (j - index + 1));
                    FieldInfo feeField = agentBonus.GetType().GetField("fee" + (j - index + 1));

                    String feeNameFieldValue = dgAgentBonus.Columns[j].HeaderCell.Value.ToString();
                    String feeFieldValue = dgAgentBonus[j, i].Value.ToString();
                    if (feeFieldValue.Trim().Equals("0") || String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        feeFieldValue = null;
                    }
                    feeNameField.SetValue(agentBonus, feeNameFieldValue);
                    feeField.SetValue(agentBonus, feeFieldValue);

                }

                agentBonusDao.Delete(agentBonus);
                agentBonusDao.Add(agentBonus);

            }
            worker.ReportProgress(4, "导入红包完成...\r\n");

            WechatAction wechatAction = new WechatAction();
            wechatAction.sendTextMessageToWechat("@all", this.dtFeeMonth.Value.ToString("yyyy年MM月") + "红包已发布，请通过底部菜单查询红包详情", Settings.Default.Wechat_Secret, MyConstant.APP_Payment);


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
            saveFileDialog.FileName = "红包模板.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                File.Copy("./Template/ImportBonus_Template.xlsx", saveFileDialog.FileName);
            }
        }


    }
}
