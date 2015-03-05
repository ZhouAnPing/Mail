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
    public partial class frmAgentInvoicePaymentManagement : frmBase
    {
        public frmAgentInvoicePaymentManagement()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            this.prepareGrid(this.txtAgentNo.Text.Trim(), this.txtAgentName.Text.Trim());
        }

        private void prepareGrid(string agentNo,string agentName)
        {
            this.Cursor = Cursors.WaitCursor;

            AgentInvoicePaymentDao agentInvoicePaymentDao = new AgentInvoicePaymentDao();
            
            IList<AgentInvoicePayment> agentInvoiceList = new List<AgentInvoicePayment>();

            agentInvoiceList = agentInvoicePaymentDao.GetList(agentNo, agentName,null);
            

            if (agentInvoiceList != null && agentInvoiceList.Count > 0)
            {
                this.grpList.Text = "支付记录列表(" + agentInvoiceList.Count + ")";
                this.dgInvoicePayment.Rows.Clear();
                dgInvoicePayment.Columns.Clear();

               
                dgInvoicePayment.Columns.Add("代理商编号", "代理商编号");
                dgInvoicePayment.Columns.Add("代理商全称", "代理商全称");
                dgInvoicePayment.Columns.Add("处理时间", "处理时间");
                dgInvoicePayment.Columns.Add("发票金额", "发票金额");
                dgInvoicePayment.Columns.Add("付款金额", "付款金额");
                dgInvoicePayment.Columns.Add("摘要", "摘要");
                dgInvoicePayment.Columns.Add("票据状态", "票据状态");

                		
						

                for (int i = 0; i < agentInvoiceList.Count; i++)
                {
                    dgInvoicePayment.Rows.Add();
                    DataGridViewRow row = dgInvoicePayment.Rows[i];

                    row.Cells[0].Value = agentInvoiceList[i].agentNo;
                    row.Cells[1].Value = agentInvoiceList[i].agentName;
                    row.Cells[2].Value = agentInvoiceList[i].processTime;
                    row.Cells[3].Value = agentInvoiceList[i].invoiceFee;
                    row.Cells[4].Value = agentInvoiceList[i].payFee;
                    row.Cells[5].Value = agentInvoiceList[i].summary;
                    row.Cells[6].Value = agentInvoiceList[i].payStatus;
                   
                   

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
            frmAgentInvoicePaymentModification frmAgentInvoicePaymentModification = new frmAgentInvoicePaymentModification();


            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.agentNo = this.dgInvoicePayment.CurrentRow.Cells[0].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.agentName = this.dgInvoicePayment.CurrentRow.Cells[1].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.processTime = this.dgInvoicePayment.CurrentRow.Cells[2].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.invoiceFee = this.dgInvoicePayment.CurrentRow.Cells[3].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.payFee = this.dgInvoicePayment.CurrentRow.Cells[4].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.summary = this.dgInvoicePayment.CurrentRow.Cells[5].Value.ToString();
            frmAgentInvoicePaymentModification.inputAgentInvoicePayment.payStatus = this.dgInvoicePayment.CurrentRow.Cells[6].Value.ToString();

            DialogResult result = frmAgentInvoicePaymentModification.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {

                DataGridViewRow row = dgInvoicePayment.CurrentRow;

                row.Cells[0].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.agentNo;
                row.Cells[1].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.agentName;
                row.Cells[2].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.processTime;
                row.Cells[3].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.invoiceFee;
                row.Cells[4].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.payFee;
                row.Cells[5].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.summary;
                row.Cells[6].Value = frmAgentInvoicePaymentModification.outputAgentInvoicePayment.payStatus;

            }
        }
    }
}
