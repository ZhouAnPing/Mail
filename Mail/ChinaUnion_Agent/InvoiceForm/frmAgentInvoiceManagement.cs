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
    public partial class frmAgentInvoiceManagement : frmBase
    {
        public frmAgentInvoiceManagement()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            this.prepareGrid(this.txtAgentNo.Text.Trim(), this.txtAgentName.Text.Trim(), this.txtMonth.Text.Trim());
        }

        private void prepareGrid(string agentNo,string agentName, string invoiceMonth)
        {
            this.Cursor = Cursors.WaitCursor;

            AgentInvoiceDao agentInvoiceDao = new AgentInvoiceDao();
           
            IList<AgentInvoice> agentInvoiceList = new List<AgentInvoice>();

            agentInvoiceList = agentInvoiceDao.GetList(agentNo,agentName,invoiceMonth);
            

            if (agentInvoiceList != null && agentInvoiceList.Count > 0)
            {
                this.grpList.Text = "发票记录列表(" + agentInvoiceList.Count + ")";
                this.dgInvoice.Rows.Clear();
                dgInvoice.Columns.Clear();

                dgInvoice.Columns.Add("月份", "月份");
                dgInvoice.Columns.Add("收票日期", "收票日期");
                dgInvoice.Columns.Add("代理商编号", "代理商编号");
                dgInvoice.Columns.Add("代理商全称", "代理商全称");
                dgInvoice.Columns.Add("内容", "内容");
                dgInvoice.Columns.Add("金额", "金额");
                dgInvoice.Columns.Add("发票类型", "发票类型");
                dgInvoice.Columns.Add("发票号", "发票号");
                dgInvoice.Columns.Add("备注", "备注");

                								

                for (int i = 0; i < agentInvoiceList.Count; i++)
                {
                    dgInvoice.Rows.Add();
                    DataGridViewRow row = dgInvoice.Rows[i];

                    row.Cells[0].Value = agentInvoiceList[i].invoiceMonth;
                    row.Cells[1].Value = agentInvoiceList[i].invoiceDate;
                    row.Cells[2].Value = agentInvoiceList[i].agentNo;
                    row.Cells[3].Value = agentInvoiceList[i].agentName;
                    row.Cells[4].Value = agentInvoiceList[i].invoiceContent;
                    row.Cells[5].Value = agentInvoiceList[i].invoiceType;
                    row.Cells[6].Value = agentInvoiceList[i].invoiceFee;
                    row.Cells[7].Value = agentInvoiceList[i].invoiceNo;
                    row.Cells[8].Value = agentInvoiceList[i].comment;
                   

                }
                dgInvoice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgInvoice.AutoResizeColumns();

            }


            this.Cursor = Cursors.Default;

        }

        private void frmAgentInvoiceManagement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void menuModify_Click(object sender, EventArgs e)
        {
            if (this.dgInvoice.SelectedRows == null)
            {
                MessageBox.Show("请先选择");
                return;
            }
            frmAgentInvoiceModification frmAgentInvoiceModification = new frmAgentInvoiceModification();

            frmAgentInvoiceModification.inputAgentInvoice.invoiceMonth = this.dgInvoice.CurrentRow.Cells[0].Value.ToString();
            frmAgentInvoiceModification.inputAgentInvoice.invoiceDate = this.dgInvoice.CurrentRow.Cells[1].Value.ToString();
            frmAgentInvoiceModification.inputAgentInvoice.agentNo = this.dgInvoice.CurrentRow.Cells[2].Value.ToString();
            frmAgentInvoiceModification.inputAgentInvoice.agentName = this.dgInvoice.CurrentRow.Cells[3].Value.ToString();
            frmAgentInvoiceModification.inputAgentInvoice.invoiceContent = this.dgInvoice.CurrentRow.Cells[4].Value.ToString();
            frmAgentInvoiceModification.inputAgentInvoice.invoiceType = this.dgInvoice.CurrentRow.Cells[5].Value.ToString();
            frmAgentInvoiceModification.inputAgentInvoice.invoiceFee = this.dgInvoice.CurrentRow.Cells[6].Value.ToString();
            frmAgentInvoiceModification.inputAgentInvoice.invoiceNo = this.dgInvoice.CurrentRow.Cells[7].Value.ToString();
            frmAgentInvoiceModification.inputAgentInvoice.comment = this.dgInvoice.CurrentRow.Cells[8].Value.ToString();

            DialogResult result = frmAgentInvoiceModification.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {

                DataGridViewRow row = dgInvoice.CurrentRow;

                row.Cells[0].Value =frmAgentInvoiceModification. outputAgentInvoice.invoiceMonth;
                row.Cells[1].Value =frmAgentInvoiceModification. outputAgentInvoice.invoiceDate;
                row.Cells[2].Value = frmAgentInvoiceModification. outputAgentInvoice.agentNo;
                row.Cells[3].Value = frmAgentInvoiceModification. outputAgentInvoice.agentName;
                row.Cells[4].Value = frmAgentInvoiceModification. outputAgentInvoice.invoiceContent;
                row.Cells[5].Value = frmAgentInvoiceModification. outputAgentInvoice.invoiceType;
                row.Cells[6].Value = frmAgentInvoiceModification. outputAgentInvoice.invoiceFee;
                row.Cells[7].Value = frmAgentInvoiceModification. outputAgentInvoice.invoiceNo;
                row.Cells[8].Value = frmAgentInvoiceModification. outputAgentInvoice.comment;

            }
        }
    }
}
