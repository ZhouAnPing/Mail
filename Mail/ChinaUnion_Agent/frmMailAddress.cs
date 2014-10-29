using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChinaUnion_Agent
{
    public partial class frmMailAddress : Form
    {
        public String email;
        public String subject;
        public frmMailAddress()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.email = this.txtEmail.Text;
            this.subject = this.txtSubject.Text;
            if (String.IsNullOrEmpty(this.email))
            {
                MessageBox.Show("请输入邮件地址");
                return;
            }
            if (String.IsNullOrEmpty(this.subject))
            {
                MessageBox.Show("请输入邮件主题");
                return;
            }
            if (!Regex.IsMatch(email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                MessageBox.Show("请输入有效的邮件地址");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
          

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmMailAddress_Load(object sender, EventArgs e)
        {
            this.txtSubject.Text = this.subject;
        }
    }
}
