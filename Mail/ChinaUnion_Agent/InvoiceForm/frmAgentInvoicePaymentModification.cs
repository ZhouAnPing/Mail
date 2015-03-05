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
    public partial class frmAgentInvoicePaymentModification :Form
    {
        public AgentInvoicePayment inputAgentInvoicePayment = new AgentInvoicePayment();
        public AgentInvoicePayment outputAgentInvoicePayment = new AgentInvoicePayment();
        public frmAgentInvoicePaymentModification()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAgentInvoicePaymentModification_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;

            this.txtAgentName.Text = inputAgentInvoicePayment.agentName;
            this.txtAgentNo.Text = inputAgentInvoicePayment.agentNo;
            this.txtPayStatus.Text = inputAgentInvoicePayment.payStatus;
            this.txtProcessTime.Text = inputAgentInvoicePayment.processTime;

            this.txtInvoiceFee.Text = inputAgentInvoicePayment.invoiceFee;
            this.txtSummary.Text = inputAgentInvoicePayment.summary;
            this.txtPayFee.Text = inputAgentInvoicePayment.payFee;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            outputAgentInvoicePayment.agentName = this.txtAgentName.Text;
            outputAgentInvoicePayment.agentNo = this.txtAgentNo.Text;
            outputAgentInvoicePayment.payStatus = this.txtPayStatus.Text;
            outputAgentInvoicePayment.processTime = this.txtProcessTime.Text;
            
            outputAgentInvoicePayment.invoiceFee = this.txtInvoiceFee.Text;
            outputAgentInvoicePayment.summary = this.txtSummary.Text;
            outputAgentInvoicePayment.payFee = this.txtPayFee.Text;
           

            AgentInvoicePaymentDao agentInvoicePaymenDao = new AgentInvoicePaymentDao();
             int i =  agentInvoicePaymenDao.Update(outputAgentInvoicePayment);
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
