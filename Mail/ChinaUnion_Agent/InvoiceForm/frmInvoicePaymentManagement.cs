using ChinaUnion_Agent.Util;
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

namespace ChinaUnion_Agent.InvoiceForm
{
    public partial class frmInvoicePaymentManagement : frmBase
    {
        public frmInvoicePaymentManagement()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            this.prepareGrid(this.txtAgentNo.Text.Trim(), this.txtAgentName.Text.Trim(),this.txtInvoiceNo.Text.Trim());
        }

        private void prepareGrid(string agentNo,string agentName, string invoiceNo)
        {
            this.Cursor = Cursors.WaitCursor;

            InvoicePaymentDao agentInvoicePaymentDao = new InvoicePaymentDao();

            IList<InvoicePayment> agentInvoiceList = new List<InvoicePayment>();

            agentInvoiceList = agentInvoicePaymentDao.GetList(agentNo, agentName, null, invoiceNo);
            

            if (agentInvoiceList != null && agentInvoiceList.Count > 0)
            {
                this.grpList.Text = "发票支付记录列表(" + agentInvoiceList.Count + ")";
                this.dgInvoicePayment.Rows.Clear();
                dgInvoicePayment.Columns.Clear();

                dgInvoicePayment.Columns.Add("月份", "月份");
                dgInvoicePayment.Columns.Add("收票日期", "收票日期");
                dgInvoicePayment.Columns.Add("处理时间", "处理时间");
                dgInvoicePayment.Columns.Add("代理商编号", "代理商编号");
                dgInvoicePayment.Columns.Add("代理商全称", "代理商全称");
                dgInvoicePayment.Columns.Add("内容", "内容");
                dgInvoicePayment.Columns.Add("金额", "金额");

                dgInvoicePayment.Columns.Add("发票类型", "发票类型");
                dgInvoicePayment.Columns.Add("发票号", "发票号");
                dgInvoicePayment.Columns.Add("票据状态", "票据状态");

                		
						

                for (int i = 0; i < agentInvoiceList.Count; i++)
                {
                    dgInvoicePayment.Rows.Add();
                    DataGridViewRow row = dgInvoicePayment.Rows[i];

                    row.Cells[0].Value = agentInvoiceList[i].month ;
                    row.Cells[1].Value = agentInvoiceList[i].receivedTime;
                    row.Cells[2].Value = agentInvoiceList[i].processTime;
                    row.Cells[3].Value = agentInvoiceList[i].agentNo;
                    row.Cells[4].Value = agentInvoiceList[i].agentName;
                    row.Cells[5].Value = agentInvoiceList[i].content;
                    row.Cells[6].Value = agentInvoiceList[i].invoiceFee;
                    row.Cells[7].Value = agentInvoiceList[i].invoiceType;
                    row.Cells[8].Value = agentInvoiceList[i].invoiceNo;
                    row.Cells[9].Value = agentInvoiceList[i].payStatus;
                   
                   
                   

                }
                dgInvoicePayment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgInvoicePayment.AutoResizeColumns();

            }


            this.Cursor = Cursors.Default;

        }

        private void frmAgentInvoicePaymentManagement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void menuModify_Click(object sender, EventArgs e)
        {
            if (this.dgInvoicePayment.SelectedRows == null)
            {
                MessageBox.Show("请先选择");
                return;
            }
            frmInvoicePaymentModification frmAgentInvoicePaymentModification = new frmInvoicePaymentModification();

            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.month  = this.dgInvoicePayment.CurrentRow.Cells[0].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.receivedTime = this.dgInvoicePayment.CurrentRow.Cells[1].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.processTime = this.dgInvoicePayment.CurrentRow.Cells[2].Value.ToString();

            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.agentNo = this.dgInvoicePayment.CurrentRow.Cells[3].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.agentName = this.dgInvoicePayment.CurrentRow.Cells[4].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.content  = this.dgInvoicePayment.CurrentRow.Cells[5].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.invoiceFee = this.dgInvoicePayment.CurrentRow.Cells[6].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.invoiceType = this.dgInvoicePayment.CurrentRow.Cells[7].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.invoiceNo = this.dgInvoicePayment.CurrentRow.Cells[8].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.payStatus = this.dgInvoicePayment.CurrentRow.Cells[9].Value.ToString();

            DialogResult result = frmAgentInvoicePaymentModification.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {

                DataGridViewRow row = dgInvoicePayment.CurrentRow;

                row.Cells[0].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.month;
                row.Cells[1].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.receivedTime;
                row.Cells[2].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.processTime;
                row.Cells[3].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.agentNo;
                row.Cells[4].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.agentName;
                row.Cells[5].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.content;
                row.Cells[6].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.invoiceFee;
                row.Cells[7].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.invoiceType;
                row.Cells[8].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.invoiceNo;
                row.Cells[9].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.payStatus;

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgInvoicePayment);
            this.Cursor = Cursors.Default;
        }
    }
}
