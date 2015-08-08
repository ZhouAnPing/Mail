using ChinaUnion_Agent.Properties;
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
    public partial class frmInvoiceImport : frmBase
    {
        public frmInvoiceImport()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

                this.txtInvoice.Text = FileName;
                this.txtInvoice.Enabled = false;

                List<string> sheetNames = execelfile.GetWorksheetNames().ToList();

                if (!sheetNames.Contains("发票登记"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:发票登记的sheet.");
                    return;
                }



                //代理商信息
                List<Row> invoice = execelfile.Worksheet("发票登记").ToList(); ;

                if (invoice != null && invoice.Count > 0)
                {

                    this.btnImport.Enabled = true;
                    dgInvoice.Rows.Clear();
                    dgInvoice.Columns.Clear();
                    foreach (String coloumn in invoice[0].ColumnNames)
                    {
                        this.dgInvoice.Columns.Add(coloumn, coloumn);
                    }

                    this.dgInvoice.Columns.Add("result", "导入结果");

                    for (int i = 0; i < invoice.Count; i++)
                    {
                        if (String.IsNullOrEmpty(invoice[i][0].Value.ToString().Trim()))
                        {
                            continue;
                        }
                        dgInvoice.Rows.Add();
                        DataGridViewRow row = dgInvoice.Rows[i];
                        foreach (String coloumn in invoice[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = invoice[i][coloumn];
                        }

                    }
                }




                dgInvoice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgInvoice.AutoResizeColumns();

                Cursor.Current = Cursors.Default;
            }
        }
        BackgroundWorker worker; 
        private void frmInvoiceImport_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
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

            worker.ReportProgress(3, "开始导入发票信息...\r\n");
            //导入代理商
            WechatAction wechatAction = new WechatAction();
            AgentInvoiceDao agentInvoiceDao = new AgentInvoiceDao();
            AgentDao agentDao = new AgentDao();
            for (int i = 0; i < dgInvoice.RowCount; i++)
            {
                AgentInvoice agentInvoice = new AgentInvoice();
                agentInvoice.invoiceMonth = dgInvoice[0, i].Value.ToString();
                agentInvoice.invoiceDate = DateTime.Parse( dgInvoice[1, i].Value.ToString()).ToString("yyyy-MM-dd");
                agentInvoice.agentNo = dgInvoice[2, i].Value.ToString();
                agentInvoice.agentName = dgInvoice[3, i].Value.ToString();
                agentInvoice.invoiceContent = dgInvoice[4, i].Value.ToString();               
                agentInvoice.invoiceFee = dgInvoice[5, i].Value.ToString();
                agentInvoice.invoiceType = dgInvoice[6, i].Value.ToString();
                agentInvoice.invoiceNo = dgInvoice[7, i].Value.ToString();
                agentInvoice.comment = dgInvoice[8, i].Value.ToString();

                Agent agent = agentDao.Get(agentInvoice.agentNo);
                if (agent != null && !String.IsNullOrEmpty(agent.agentName))
                {
                    agentInvoiceDao.Delete(agentInvoice);
                    agentInvoiceDao.Add(agentInvoice);
                    dgInvoice["result", i].Value = "导入成功";

                   String message = String.Format(Settings.Default.Invoice_Wechat_Message, agentInvoice.invoiceDate, agentInvoice.invoiceContent, agentInvoice.invoiceFee, agentInvoice.invoiceType, agentInvoice.invoiceNo,agentInvoice.comment);
                   wechatAction.sendTextMessageToWechat(agentInvoice.agentNo, message, Settings.Default.Wechat_Secret, Settings.Default.Wechar_Invoice_AppId);

                }
                else
                {
                    dgInvoice["result", i].Value = "导入失败，代理商编号：" + agentInvoice.agentNo+"不存在,请先导入代理商.";
                }

            }
            //dgInvoice.AutoResizeColumns();
            
            worker.ReportProgress(4, "导入发票信息完成...\r\n");



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
            saveFileDialog.FileName = "ImportInvoice_Template.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportInvoice_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName, true);
            }
        }
    }
}
