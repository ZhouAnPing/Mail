﻿using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.InvoiceForm
{
    public partial class frmInvoicePaymentImport : frmBase
    {
        public frmInvoicePaymentImport()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        BackgroundWorker worker; 
        private void frmPaymentImport_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        private void btnSelect_Click(object sender, EventArgs e)
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

                this.txtInvoicePayment.Text = FileName;
                this.txtInvoicePayment.Enabled = false;

                List<string> sheetNames = execelfile.GetWorksheetNames().ToList();

                //if (!sheetNames.Contains("支付记录"))
                //{
                //    MessageBox.Show("Excel格式不正确，必须含有名称:支付记录的sheet.");
                //    return;
                //}



                //代理商信息
                List<Row> invoicePayment = execelfile.Worksheet(0).ToList(); ;

                if (invoicePayment != null && invoicePayment.Count > 0)
                {

                    this.btnImport.Enabled = true;
                    dgInvoicePayment.Rows.Clear();
                    dgInvoicePayment.Columns.Clear();
                    foreach (String coloumn in invoicePayment[0].ColumnNames)
                    {
                        this.dgInvoicePayment.Columns.Add(coloumn, coloumn);
                    }
                    this.dgInvoicePayment.Columns.Add("result", "导入结果");
                    for (int i = 0; i < invoicePayment.Count; i++)
                    {
                        if (String.IsNullOrEmpty(invoicePayment[i][0].Value.ToString().Trim()))
                        {
                            continue;
                        }
                        dgInvoicePayment.Rows.Add();
                        DataGridViewRow row = dgInvoicePayment.Rows[i];
                        foreach (String coloumn in invoicePayment[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = invoicePayment[i][coloumn];
                        }

                    }
                }




                dgInvoicePayment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgInvoicePayment.AutoResizeColumns();

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
        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码


           


            worker.ReportProgress(3, "开始导入支付记录...\r\n");
            //导入代理商
            InvoicePaymentDao agentInvoicePaymentDao = new InvoicePaymentDao();
            AgentDao agentDao = new AgentDao();
            WechatAction wechatAction = new WechatAction();
            for (int i = 0; i < dgInvoicePayment.RowCount; i++)
            {
                InvoicePayment agentInvoicePayment = new InvoicePayment();
                agentInvoicePayment.month = dgInvoicePayment[0, i].Value.ToString();
                agentInvoicePayment.receivedTime = dgInvoicePayment[1, i].Value.ToString();
                agentInvoicePayment.processTime = dgInvoicePayment[2, i].Value.ToString();
                agentInvoicePayment.agentNo = dgInvoicePayment[3, i].Value.ToString();
                agentInvoicePayment.agentName = dgInvoicePayment[4, i].Value.ToString();
                agentInvoicePayment.content = dgInvoicePayment[5, i].Value.ToString();
                agentInvoicePayment.invoiceFee = dgInvoicePayment[6, i].Value.ToString();

                agentInvoicePayment.invoiceType = dgInvoicePayment[7, i].Value.ToString();
                agentInvoicePayment.invoiceNo = dgInvoicePayment[8, i].Value.ToString();
                agentInvoicePayment.payStatus = dgInvoicePayment[9, i].Value.ToString();

                agentInvoicePaymentDao.Delete(agentInvoicePayment);
                agentInvoicePaymentDao.Add(agentInvoicePayment);
                dgInvoicePayment["result", i].Value = "导入成功";


            }
            worker.ReportProgress(4, "导入支付记录完成...\r\n");
            
            wechatAction.sendTextMessageToWechat("@all", "结算支付信息已发布，请通过底部菜单查询结算支付详情", Settings.Default.Wechat_Secret, MyConstant.APP_Payment);



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

        private void btnDownload_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel格式|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "结算支付.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportInvoicePayment_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName, true);
            }
        }
    }
}