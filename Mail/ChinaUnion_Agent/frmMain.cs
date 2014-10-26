
using ChinaUnion_Agent.Wechat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHC.OrderWater.Commons;

namespace ChinaUnion_Agent
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出代理商系统？", "退出", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void menuItemAgentImport_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
            frmAgentImport frmAgentImport = new frmAgentImport();
            frmAgentImport.MdiParent = this;
          
            frmAgentImport.Show();
            frmAgentImport.WindowState = FormWindowState.Maximized;


        }

      

        private void menuItemAgentFeeQuery_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            frmAgentQuery frmAgentQuery = new frmAgentQuery();
            frmAgentQuery.MdiParent = this;
           
            frmAgentQuery.Show();
            frmAgentQuery.WindowState = FormWindowState.Maximized;

        }

        private void menuItemReportQuery_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
            frmMailReport frmMailReport = new frmMailReport();
            frmMailReport.MdiParent = this;

            frmMailReport.Show();
            frmMailReport.WindowState = FormWindowState.Maximized;

        }


        private void toolbarWechat_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
            frmWechatManagement frmWechatManagement = new frmWechatManagement();
            frmWechatManagement.MdiParent = this;

            frmWechatManagement.Show();
            frmWechatManagement.WindowState = FormWindowState.Maximized;
        }
       

        
    }
}
