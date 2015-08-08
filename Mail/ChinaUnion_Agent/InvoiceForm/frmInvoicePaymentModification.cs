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
    public partial class frmInvoicePaymentModification :Form
    {
        public InvoicePayment inputAgentInvoicePayment = new InvoicePayment();
        public InvoicePayment outputAgentInvoicePayment = new InvoicePayment();
        public frmInvoicePaymentModification()
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
            this.txtMonth.Text = inputAgentInvoicePayment.month ;
            this.txtReceiveDate.Text = inputAgentInvoicePayment.receivedTime;
            this.txtProcessTime.Text = inputAgentInvoicePayment.processTime;
            this.txtAgentName.Text = inputAgentInvoicePayment.agentName;
            this.txtAgentNo.Text = inputAgentInvoicePayment.agentNo;
            this.txtContent.Text = inputAgentInvoicePayment.content;
            this.txtInvoiceFee.Text = inputAgentInvoicePayment.invoiceFee;
            this.txtInvoiceNo.Text = inputAgentInvoicePayment.invoiceNo;
            this.txtInvoiceType.Text = inputAgentInvoicePayment.invoiceType;
            this.txtPayStatus.Text = inputAgentInvoicePayment.payStatus;
            

            
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            outputAgentInvoicePayment.month = this.txtMonth.Text.Trim();
            outputAgentInvoicePayment.receivedTime = this.txtReceiveDate.Text.Trim();
            outputAgentInvoicePayment.processTime = this.txtProcessTime.Text.Trim();
            outputAgentInvoicePayment.agentName = this.txtAgentName.Text.Trim();
            outputAgentInvoicePayment.agentNo = this.txtAgentNo.Text.Trim();
            outputAgentInvoicePayment.content = this.txtContent.Text.Trim();

            outputAgentInvoicePayment.invoiceFee = this.txtInvoiceFee.Text.Trim();
            outputAgentInvoicePayment.invoiceType = this.txtInvoiceType.Text.Trim();
            outputAgentInvoicePayment.invoiceNo = this.txtInvoiceNo.Text.Trim();

            outputAgentInvoicePayment.payStatus = this.txtPayStatus.Text.Trim();





            InvoicePaymentDao agentInvoicePaymenDao = new InvoicePaymentDao();
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
