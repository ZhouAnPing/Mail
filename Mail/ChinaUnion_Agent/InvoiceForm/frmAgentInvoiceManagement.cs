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
    }
}
