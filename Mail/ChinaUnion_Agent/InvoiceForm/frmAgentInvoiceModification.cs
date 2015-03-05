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
    public partial class frmAgentInvoiceModification :Form
    {
        public AgentInvoice inputAgentInvoice = new AgentInvoice();
        public AgentInvoice outputAgentInvoice = new AgentInvoice();
        public frmAgentInvoiceModification()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAgentInvoiceModification_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;

            this.txtAgentName.Text = inputAgentInvoice.agentName;
            this.txtAgentNo.Text = inputAgentInvoice.agentNo;
            this.txtComment.Text = inputAgentInvoice.comment;
            this.txtContent.Text = inputAgentInvoice.invoiceContent;
            this.txtDate.Text = inputAgentInvoice.invoiceDate;
            this.txtFee.Text = inputAgentInvoice.invoiceFee;
            this.txtInvoiceNo.Text = inputAgentInvoice.invoiceNo;
            this.txtInvoiceType.Text = inputAgentInvoice.invoiceType;
            this.txtMonth.Text = this.inputAgentInvoice.invoiceMonth;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            outputAgentInvoice.agentName = this.txtAgentName.Text;
            outputAgentInvoice.agentNo = this.txtAgentNo.Text;
            outputAgentInvoice.comment = this.txtComment.Text;
            outputAgentInvoice.invoiceContent = this.txtContent.Text;
            outputAgentInvoice.invoiceDate = this.txtDate.Text;
            outputAgentInvoice.invoiceFee = this.txtFee.Text;
            outputAgentInvoice.invoiceNo = this.txtInvoiceNo.Text;
            outputAgentInvoice.invoiceType = this.txtInvoiceType.Text;
            outputAgentInvoice.invoiceMonth = this.txtMonth.Text;

            AgentInvoiceDao agentInvoiceDao = new AgentInvoiceDao();
             int i =  agentInvoiceDao.Update(outputAgentInvoice);
             if (i >= 0)
             {
                 MessageBox.Show("修改完成");

                 this.DialogResult = System.Windows.Forms.DialogResult.OK;
             }
             else
             {
                 MessageBox.Show("修改失败");
             }
        }
    }
}
