
using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_Agent.WechatForm;
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

        private void menuItemAgentFeeImport_Click(object sender, EventArgs e)
        {
            frmAgentFeeImport frmAgentFeeImport = new frmAgentFeeImport();
            CheckChildOpenState(this, frmAgentFeeImport);
        }      

        private void menuItemAgentFeeQuery_Click(object sender, EventArgs e)
        {
            frmAgentQuery frmAgentQuery = new frmAgentQuery();
            CheckChildOpenState(this, frmAgentQuery);

        }

        private void menuItemReportQuery_Click(object sender, EventArgs e)
        {
            
            frmMailReport frmMailReport = new frmMailReport();
            CheckChildOpenState(this, frmMailReport);
        }


        private void toolbarWechat_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmWechatManagement frmWechatManagement = new frmWechatManagement();
            CheckChildOpenState(this, frmWechatManagement);
            this.Cursor = Cursors.Default;
        }

        private void toolbarBroadcast_Click(object sender, EventArgs e)
        {
           
             frmBroadcast frmBroadcast = new frmBroadcast();
             frmBroadcast.Wechat_secretId = Settings.Default.Wechat_Secret;
             frmBroadcast.Wechar_AppId = Settings.Default.Wechat_Agent_AppId;

             frmBroadcast.ShowInTaskbar = false;

             frmBroadcast.ShowDialog();
            // frmBroadcast.WindowState = FormWindowState.Maximized;

        }

        /// <summary>  
        /// 名称：CheckChildOpenState  
        /// 功能：用子窗体的Name进行判断是否已实例化，如果存在则将他激活  
        /// </summary>  
        /// <param name="MdiForm">容器窗体</param>  
        /// <param name="ChildForm">子窗体</param>  
        public static void CheckChildOpenState(Form MdiForm, Form ChildForm)
        {
            foreach (Form tempChildForm in MdiForm.MdiChildren)
            {
                tempChildForm.Close();
                //if (tempChildForm.Name == ChildForm.Name.ToString())
                //{                  
                //    tempChildForm.WindowState = FormWindowState.Maximized;                   
                //    tempChildForm.Activate();
                //    return;
                //}
                //else
                //{
                //  //  tempChildForm.Close();
                //}
            }
            ChildForm.MdiParent = MdiForm;
            ChildForm.ShowInTaskbar = false;
            ChildForm.BackgroundImage = Properties.Resources.Desktop;
            ChildForm.BackgroundImageLayout = ImageLayout.Stretch;
            ChildForm.ShowIcon = false;
            foreach (Control c in ChildForm.Controls)            {
                               
                    c.BackgroundImage = Properties.Resources.Desktop;
                    c.BackgroundImageLayout = ImageLayout.Stretch;

                    foreach (Control c1 in c.Controls)
                    {
                        c1.BackgroundImage = Properties.Resources.Desktop;
                        c1.BackgroundImageLayout = ImageLayout.Stretch;
                    }
               
            }
         
            ChildForm.Show();
            ChildForm.WindowState = FormWindowState.Maximized;
        }
        private void menuItemAgentImport_Click(object sender, EventArgs e)
        {
            frmAgentImport frmAgentImport = new frmAgentImport();
            CheckChildOpenState(this, frmAgentImport);
        }
        private void toolbarErrorCodeImport_Click(object sender, EventArgs e)
        {
            frmErrorCodeImport frmErrorCodeImport = new frmErrorCodeImport();
            CheckChildOpenState(this, frmErrorCodeImport);
        }

        private void toolbarErrorCodeQuery_Click(object sender, EventArgs e)
        {
            frmErrorCodeQuery frmErrorCodeQuery = new frmErrorCodeQuery();
            CheckChildOpenState(this, frmErrorCodeQuery);

        }

        private void toolbarErrorCodeWechatImport_Click(object sender, EventArgs e)
        {
            frmErrorCodeWechat frmErrorCodeWechat = new frmErrorCodeWechat();
            CheckChildOpenState(this, frmErrorCodeWechat);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            this.BackgroundImage = Properties.Resources.Desktop;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            foreach (Control c in this.Controls)
            {
                c.BackgroundImage = Properties.Resources.Desktop;
                c.BackgroundImageLayout = ImageLayout.Stretch;
                foreach (Control c1 in c.Controls)
                {
                    c1.BackgroundImage = Properties.Resources.Desktop;
                    c1.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
               

         
        }

        private void toolbarErrorBroadcast_Click(object sender, EventArgs e)
        {
            frmBroadcast frmBroadcast = new frmBroadcast();
            frmBroadcast.Wechat_secretId = Settings.Default.Wechat_Secret;
            frmBroadcast.Wechar_AppId = Settings.Default.Wechar_Error_AppId;

            frmBroadcast.ShowInTaskbar = false;

            frmBroadcast.ShowDialog();

        }

      
       

        
    }
}
