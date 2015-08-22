
using BSE.Windows.Forms;
using ChinaUnion_Agent.InvoiceForm;
using ChinaUnion_Agent.PolicyForm;
using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.UserManagement;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_Agent.WechatForm;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        public DataTable menuTable = new DataTable();
        public ChinaUnion_BO.User loginUser = new ChinaUnion_BO.User();
        public bool isNewVersion = false;
        public frmMain()
        {
            InitializeComponent();
        }



        public void menuItemExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出系统？", "退出", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void CommissionImport_Click(object sender, EventArgs e)
        {
            frmAgentFeeImport frmAgentFeeImport = new frmAgentFeeImport();
            CheckChildOpenState(this, frmAgentFeeImport);
        }

        public void CommissionPublish_Click(object sender, EventArgs e)
        {
            frmAgentQuery frmAgentQuery = new frmAgentQuery();
            CheckChildOpenState(this, frmAgentQuery);

        }

        public void CommissionOpenReport_Click(object sender, EventArgs e)
        {
            
            frmMailReport frmMailReport = new frmMailReport();
            CheckChildOpenState(this, frmMailReport);
        }

        public void ChannelImport_Click(object sender, EventArgs e)
        {
            frmAgentImport frmAgentImport = new frmAgentImport();
            CheckChildOpenState(this, frmAgentImport);
        }

        public void ChannelWechatSync_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            frmAgentWechatManagement frmAgentWechatManagement = new frmAgentWechatManagement();
            CheckChildOpenState(this, frmAgentWechatManagement);
            this.Cursor = Cursors.Default;
        }

        public void ChannelWechatQuery_Click(object sender, EventArgs e)
        {
            frmWechatManagement frmWechatManagementTmp = new frmWechatManagement();
            frmWechatManagementTmp.wechatType = "Agent";
            CheckChildOpenState(this, frmWechatManagementTmp);

        }

        public void ChannelWechatBroadcast_Click(object sender, EventArgs e)
        {
           
             frmBroadcast frmBroadcast = new frmBroadcast();
             frmBroadcast.Wechat_secretId = Settings.Default.Wechat_Secret;
             frmBroadcast.Wechar_AppId = Settings.Default.Wechat_Agent_AppId;

             frmBroadcast.ShowInTaskbar = false;

             frmBroadcast.ShowDialog();
            // frmBroadcast.WindowState = FormWindowState.Maximized;

        }



        public void ErrorCodeImport_Click(object sender, EventArgs e)
        {
            frmErrorCodeImport frmErrorCodeImport = new frmErrorCodeImport();
            CheckChildOpenState(this, frmErrorCodeImport);
        }

        public void ErrorCodeQuery_Click(object sender, EventArgs e)
        {
            frmErrorCodeQuery frmErrorCodeQuery = new frmErrorCodeQuery();
            CheckChildOpenState(this, frmErrorCodeQuery);
        }

        public void ErrorWechatImport_Click(object sender, EventArgs e)
        {
            frmErrorCodeWechat frmErrorCodeWechat = new frmErrorCodeWechat();
            CheckChildOpenState(this, frmErrorCodeWechat);
        }

        public void ErrorWechatQuery_Click(object sender, EventArgs e)
        {
            frmWechatManagement frmWechatManagementTmp = new frmWechatManagement();
            frmWechatManagementTmp.wechatType = "ErrorCode";
            CheckChildOpenState(this, frmWechatManagementTmp);
        }

        public void ErrorWehatBroadcast_Click(object sender, EventArgs e)
        {
            frmBroadcast frmBroadcast = new frmBroadcast();
            frmBroadcast.Wechat_secretId = Settings.Default.Wechat_Secret;
            frmBroadcast.Wechar_AppId = Settings.Default.Wechar_Error_AppId;

            frmBroadcast.ShowInTaskbar = false;

            frmBroadcast.ShowDialog();

        }

       

       

        private void menuItemLog_Click(object sender, EventArgs e)
        {

            frmLogMemo frmLogMemo = new frmLogMemo();

            CheckChildOpenState(this, frmLogMemo);

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

        #region

        public void CreateToolbar(DataTable menuTable)
        {
            this.ToolBarPannelList.XPanderPanels.Clear();           
          
            //检查判断DataSet数据是否完整
            if (menuTable != null && menuTable.Rows.Count > 0)
            {
                LoadToolbarItem( menuTable);
            }

        }

        /// <summary>
        /// 递归创建MenuStrip菜单(模块列表)
        /// </summary>
     
        /// <param name="MenuDT">菜单数据</param>
        private void LoadToolbarItem(DataTable MenuDT)
        {
            DataView dvList = new DataView(MenuDT);            
            dvList.RowFilter = "FATHER_ID='" + "0" + "'";

            foreach (DataRowView dv in dvList)
            {
                if (dv["MENU_NAME"].ToString().Equals("Exit"))
                {
                    continue;
                }
                XPanderPanel xPanderPanel = new XPanderPanel();
                             
                xPanderPanel.Dock = DockStyle.Fill;
                ToolStrip toolStrip = new ToolStrip();
                toolStrip.Dock = DockStyle.Fill;
                toolStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                toolStrip.ImageList = this.imageList1;
                toolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
                
                xPanderPanel.Tag = dv["MENU_NAME"].ToString();
                xPanderPanel.Text = dv["MENU_TEXT"].ToString();
                xPanderPanel.CaptionHeight = 30;
                xPanderPanel.CaptionFont = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
             

                ToolBarPannelList.XPanderPanels.Add(xPanderPanel);
                xPanderPanel.Controls.Add(toolStrip);

                xPanderPanel.BackgroundImage = Properties.Resources.Desktop;
                xPanderPanel.BackgroundImageLayout = ImageLayout.Stretch;
                toolStrip.BackgroundImage = Properties.Resources.Desktop;
                toolStrip.BackgroundImageLayout = ImageLayout.Stretch;

                DataView dvSubList = new DataView(MenuDT);
                dvSubList.RowFilter = "FATHER_ID='" + dv["ID"].ToString() + "'";
              
                foreach (DataRowView subDv in dvSubList)
                {
                   

                    ToolStripButton toolStripButton = new System.Windows.Forms.ToolStripButton();
                    toolStripButton.Tag = subDv["MENU_NAME"].ToString();
                    toolStripButton.Text = subDv["MENU_TEXT"].ToString();
                  
                   // toolStripButton.Size = new System.Drawing.Size(138, 64);
                   // toolStripButton.ImageIndex = i;
                    toolStripButton.ImageKey = subDv["IMAGE_KEY"].ToString();
                   
                    toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    toolStripButton.TextImageRelation = TextImageRelation.ImageAboveText;                    
                    toolStripButton.Click += new EventHandler(subItem_Click);
                    toolStrip.Items.Add(toolStripButton);

                    ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
                    toolStrip.Items.Add(toolStripSeparator);
                }

            }

        }

        public void CreateMenu(DataTable menuTable)
        {
            menuMain.Items.Clear();
            menuMain.ImageList = this.imageList1;
          
            //检查判断DataSet数据是否完整
           if (menuTable != null && menuTable.Rows.Count > 0)
            {
                //加载MenuStrip菜单
                ToolStripMenuItem topMenu = new ToolStripMenuItem();
                LoadSubMenu(ref topMenu, "0", menuTable);
            }

        }
        /// <summary>
        /// 递归创建MenuStrip菜单(模块列表)
        /// </summary>
        /// <param name="topMenu">父菜单项</param>
        /// <param name="FATHER_ID">父菜单的ID</param>
        /// <param name="MenuDT">菜单数据</param>
        private void LoadSubMenu(ref ToolStripMenuItem topMenu, String inFatherId, DataTable MenuDT)
        {
            DataView dvList = new DataView(MenuDT);
            //过滤出当前父菜单下在所有子菜单数据(仅为下一层的)
            dvList.RowFilter = "FATHER_ID='" + inFatherId + "'";
            ToolStripMenuItem subMenu;
            foreach (DataRowView dv in dvList)
            {
                //创建子菜单项
                subMenu = new ToolStripMenuItem();
                subMenu.Tag = dv["MENU_NAME"].ToString();
                subMenu.Text = dv["MENU_TEXT"].ToString();
                subMenu.ImageKey = dv["IMAGE_KEY"].ToString();
                //判断是否为顶级菜单
                if (inFatherId == "0")
                {
                    menuMain.Items.Add(subMenu);
                    if(subMenu.Tag.Equals("Exit")){
                    subMenu.Click += new EventHandler(subItem_Click);
                    }
                }
                else
                {
                   // subMenu.Tag = dv["EVENT_NAME"].ToString();
                   
                    //给菜单项加事件。
                    subMenu.Click += new EventHandler(subItem_Click);
                    topMenu.DropDownItems.Add(subMenu);
                }
                //递归调用
                LoadSubMenu(ref subMenu, dv["ID"].ToString(), MenuDT);
            }
        }

        /**/
        /// <summary>
        /// 菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void subItem_Click(object sender, EventArgs e)
        {
            try
            {

                string acName = ((ToolStripItem)sender).Tag.ToString();
                switch (acName)
                {
                    case "Exit":

                        if (MessageBox.Show("是否退出代理商系统？", "退出", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Application.Exit();
                        }
                        break;



                    case "CommissionImport":

                        frmAgentFeeImport frmAgentFeeImport = new frmAgentFeeImport();
                        CheckChildOpenState(this, frmAgentFeeImport);
                        break;


                    case "CommissionPublish":

                        frmAgentQuery frmAgentQuery = new frmAgentQuery();
                        CheckChildOpenState(this, frmAgentQuery);
                        break;


                    case "CommissionOpenReport":


                        frmMailReport frmMailReport = new frmMailReport();
                        CheckChildOpenState(this, frmMailReport);
                        break;


                



                    case "ChannelWechatBroadcast":


                        frmBroadcast frmBroadcast = new frmBroadcast();
                        frmBroadcast.Wechat_secretId = Settings.Default.Wechat_Secret;
                        frmBroadcast.Wechar_AppId = Settings.Default.Wechat_Agent_AppId;

                        frmBroadcast.ShowInTaskbar = false;

                        frmBroadcast.ShowDialog();
                        // frmBroadcast.WindowState = FormWindowState.Maximized;
                        break;




                    case "ErrorCodeImport":

                        frmErrorCodeImport frmErrorCodeImport = new frmErrorCodeImport();
                        CheckChildOpenState(this, frmErrorCodeImport);
                        break;


                    case "ErrorCodeQuery":

                        frmErrorCodeQuery frmErrorCodeQuery = new frmErrorCodeQuery();
                        CheckChildOpenState(this, frmErrorCodeQuery);
                        break;


                    case "ErrorWechatImport":

                        frmErrorCodeWechat frmErrorCodeWechat = new frmErrorCodeWechat();
                        CheckChildOpenState(this, frmErrorCodeWechat);
                        break;


                    case "ErrorWechatQuery":

                        frmWechatManagement frmErrorWechatManagementTmp = new frmWechatManagement();
                        frmErrorWechatManagementTmp.wechatType = "ErrorCode";
                        CheckChildOpenState(this, frmErrorWechatManagementTmp);
                        break;


                    case "ErrorWehatBroadcast":

                        frmBroadcast frmErrorWechatBroadcast = new frmBroadcast();
                        frmErrorWechatBroadcast.Wechat_secretId = Settings.Default.Wechat_Secret;
                        frmErrorWechatBroadcast.Wechar_AppId = Settings.Default.Wechar_Error_AppId;

                        frmErrorWechatBroadcast.ShowInTaskbar = false;

                        frmErrorWechatBroadcast.ShowDialog();
                        break;
                    case "PolicyPublish":

                        frmPolicyPublish frmPolicyPublish = new frmPolicyPublish();
                        frmPolicyPublish.loginUser = this.loginUser;
                        CheckChildOpenState(this, frmPolicyPublish);
                        break;
                 

                    case "UserMantain":

                        frmUserManagement frmUserManagement = new frmUserManagement();

                        CheckChildOpenState(this, frmUserManagement);
                        break;
                    case "ChangePassword":

                        frmUserModification frmUserModification = new frmUserModification();
                        frmUserModification.loginUser = this.loginUser;
                        CheckChildOpenState(this, frmUserModification);
                        break;
                    case "WechatMemberManagement":
                        frmWechatContactManagement frmWechatContactManagement = new frmWechatContactManagement();
                      
                        CheckChildOpenState(this, frmWechatContactManagement);
                        break;
                    case "AgentQuery":

                        MasterDataForm.frmAgentQuery frmAgentManagement = new MasterDataForm.frmAgentQuery();
                        CheckChildOpenState(this, frmAgentManagement);
                        break;
                    case "AgentImport":

                        MasterDataForm.frmAgentImport frmAgentImport = new MasterDataForm.frmAgentImport();
                        CheckChildOpenState(this, frmAgentImport);
                        break;
                    case "WechatSync":

                        MasterDataForm.frmWechatSync frmWechatSync = new MasterDataForm.frmWechatSync();
                        CheckChildOpenState(this, frmWechatSync);
                        break;
                    case "WechatQuery":

                        MasterDataForm.frmWechatQuery frmWechatQuery = new MasterDataForm.frmWechatQuery();
                        frmWechatQuery.wechatType = "Agent";
                        CheckChildOpenState(this, frmWechatQuery);
                        break;
                    case "WechatImport":

                        MasterDataForm.frmAgentWechatAccountImport frmAgentWechatAccountImport = new MasterDataForm.frmAgentWechatAccountImport();
                        CheckChildOpenState(this, frmAgentWechatAccountImport);
                        break;

                    case "InvoicePaymentImport":

                        InvoiceForm.frmInvoicePaymentImport frmInvoicePaymentImport = new frmInvoicePaymentImport();

                        CheckChildOpenState(this, frmInvoicePaymentImport);
                        break;

                    case "InvoicePaymentManagement":

                        InvoiceForm.frmInvoicePaymentManagement frmInvoicePaymentManagement = new frmInvoicePaymentManagement();

                        CheckChildOpenState(this, frmInvoicePaymentManagement);
                        break;

                    case "MonthPermanceImport":

                        PerformanceForm.frmAgentMonthPerformanceImport frmAgentMonthPerformanceImport = new PerformanceForm.frmAgentMonthPerformanceImport();
                        frmAgentMonthPerformanceImport.performanceType = MyConstant.NoDIRECT;
                        frmAgentMonthPerformanceImport.Text = "月绩效导入-非直供渠道";
                        CheckChildOpenState(this, frmAgentMonthPerformanceImport);
                        break;
                    case "MonthPermanceQuery":

                        PerformanceForm.frmAgentMonthPerformanceQuery frmAgentMonthPerformanceQuery = new PerformanceForm.frmAgentMonthPerformanceQuery();
                        frmAgentMonthPerformanceQuery.performanceType = MyConstant.NoDIRECT;
                        frmAgentMonthPerformanceQuery.Text = "月绩效查询-非直供渠道";
                        CheckChildOpenState(this, frmAgentMonthPerformanceQuery);
                        break;

                    case "MonthPermanceDirectImport":

                        PerformanceForm.frmAgentMonthPerformanceImport frmAgentMonthPerformanceDirectImport = new PerformanceForm.frmAgentMonthPerformanceImport();
                        frmAgentMonthPerformanceDirectImport.performanceType = MyConstant.DIRECT;
                        frmAgentMonthPerformanceDirectImport.Text = "月绩效导入-直供渠道";
                        CheckChildOpenState(this, frmAgentMonthPerformanceDirectImport);
                        break;
                    case "MonthPermanceDirectQuery":

                        PerformanceForm.frmAgentMonthPerformanceQuery frmAgentMonthPerformanceDirectQuery = new PerformanceForm.frmAgentMonthPerformanceQuery();
                        frmAgentMonthPerformanceDirectQuery.performanceType = MyConstant.DIRECT;
                        frmAgentMonthPerformanceDirectQuery.Text = "月绩效查询-直供渠道";
                        CheckChildOpenState(this, frmAgentMonthPerformanceDirectQuery);
                        break;


                    case "DailyPermanceImport":

                        PerformanceForm.frmAgentDailyPerformanceImport frmAgentDailyPerformanceImport = new PerformanceForm.frmAgentDailyPerformanceImport();
                        frmAgentDailyPerformanceImport.performanceType = MyConstant.NoDIRECT;
                        frmAgentDailyPerformanceImport.Text = "日绩效导入-非直供渠道";
                        CheckChildOpenState(this, frmAgentDailyPerformanceImport);
                        break;
                    case "DailyPermanceQuery":

                        PerformanceForm.frmAgentDailyPerformanceQuery frmAgentDailyPerformanceQuery = new PerformanceForm.frmAgentDailyPerformanceQuery();
                        frmAgentDailyPerformanceQuery.performanceType = MyConstant.NoDIRECT;
                        frmAgentDailyPerformanceQuery.Text = "日绩效查询-非直供渠道";
                        CheckChildOpenState(this, frmAgentDailyPerformanceQuery);
                        break;

                    case "DailyPermanceDirectImport":

                        PerformanceForm.frmAgentDailyPerformanceImport frmAgentDailyPerformanceDirectImport = new PerformanceForm.frmAgentDailyPerformanceImport();
                        frmAgentDailyPerformanceDirectImport.performanceType = MyConstant.DIRECT;
                        frmAgentDailyPerformanceDirectImport.Text = "日绩效导入-直供渠道";
                        CheckChildOpenState(this, frmAgentDailyPerformanceDirectImport);
                        break;
                    case "DailyPermanceDirectQuery":

                        PerformanceForm.frmAgentDailyPerformanceQuery frmAgentDailyPerformanceDirectQuery = new PerformanceForm.frmAgentDailyPerformanceQuery();
                        frmAgentDailyPerformanceDirectQuery.performanceType = MyConstant.DIRECT;
                        frmAgentDailyPerformanceDirectQuery.Text = "日绩效查询-直供渠道";
                        CheckChildOpenState(this, frmAgentDailyPerformanceDirectQuery);
                        break;
                    case "ContactImport":

                        ContactUs.frmAgentContactImport frmAgentContactImport = new ContactUs.frmAgentContactImport();
                        CheckChildOpenState(this, frmAgentContactImport);
                        break;
                    case "ContactQuery":
                        ContactUs.frmAgentContactQuery frmAgentContactQuery = new ContactUs.frmAgentContactQuery();
                         CheckChildOpenState(this, frmAgentContactQuery);
                        break;


                    case "BonusImport":
                        ScoreGrade.frmAgentBonusImport frmAgentBonusImport = new ScoreGrade.frmAgentBonusImport();
                        CheckChildOpenState(this, frmAgentBonusImport);
                        break;
                    case "BonusQuery":
                        ScoreGrade.frmAgentBonusQuery frmAgentBonusQuery = new ScoreGrade.frmAgentBonusQuery();
                        CheckChildOpenState(this, frmAgentBonusQuery);
                        break;

                    case "ScoreStarImport":
                        ScoreGrade.frmAgentScoreStarImport frmAgentStarImport = new ScoreGrade.frmAgentScoreStarImport();
                        CheckChildOpenState(this, frmAgentStarImport);
                        break;
                    case "ScoreStarQuery":
                        ScoreGrade.frmAgentScoreStarQuery frmAgentStarQuery = new ScoreGrade.frmAgentScoreStarQuery();
                        CheckChildOpenState(this, frmAgentStarQuery);
                        break;

                    case "SuggestionReply":

                        SuggestionForm.frmSuggestionQuery frmSuggestionQuery = new SuggestionForm.frmSuggestionQuery();
                        CheckChildOpenState(this, frmSuggestionQuery);
                        break;

                    case "AnyasisReport":

                        Analysis.frmAnalysisReport frmAnalysisReport = new Analysis.frmAnalysisReport();
                        CheckChildOpenState(this, frmAnalysisReport);
                        break;
                }

            }
            catch (Exception)
            {
            }
        }
       
#endregion


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

            if (isNewVersion)
            {
                this.imageList1.Images.Add("agentImport", Image.FromFile(Application.StartupPath + @"\Images\agentImport.png"));
                this.imageList1.Images.Add("broadcast", Image.FromFile(Application.StartupPath + @"\Images\broadcast.png"));
                this.imageList1.Images.Add("publish", Image.FromFile(Application.StartupPath + @"\Images\publish.png"));
                this.imageList1.Images.Add("query", Image.FromFile(Application.StartupPath + @"\Images\query.png"));
                this.imageList1.Images.Add("import", Image.FromFile(Application.StartupPath + @"\Images\import.png"));
                this.imageList1.Images.Add("wechatSync", Image.FromFile(Application.StartupPath + @"\Images\wechatSync.png"));
                this.imageList1.Images.Add("wechatQuery", Image.FromFile(Application.StartupPath + @"\Images\wechatQuery.png"));
                this.imageList1.Images.Add("exit", Image.FromFile(Application.StartupPath + @"\Images\exit.png"));
                CreateMenu(this.menuTable);
                CreateToolbar(this.menuTable);
                this.ToolBarPannelList.XPanderPanels[0].Expand = true;
            }
            this.Text =this.Text+ Application.ProductVersion;
          
        }

        private void btnInvoiceImport_Click(object sender, EventArgs e)
        {
            frmInvoiceImport frmInvoiceImport = new frmInvoiceImport();
            CheckChildOpenState(this, frmInvoiceImport);
        }

        private void btnPaymentImport_Click(object sender, EventArgs e)
        {
            frmPaymentImport frmPaymentImport = new frmPaymentImport();
            CheckChildOpenState(this, frmPaymentImport);
        }

        private void btnInvoiceManagement_Click(object sender, EventArgs e)
        {
            frmAgentInvoiceManagement frmAgentInvoiceManagement = new frmAgentInvoiceManagement();
            CheckChildOpenState(this, frmAgentInvoiceManagement);
        }

        private void btnPaymentManagement_Click(object sender, EventArgs e)
        {
            frmAgentInvoicePaymentManagement frmAgentInvoicePaymentManagement = new frmAgentInvoicePaymentManagement();
            CheckChildOpenState(this, frmAgentInvoicePaymentManagement);
        }

        private void btnInvoiceWechatPublish_Click(object sender, EventArgs e)
        {
            frmBroadcast frmBroadcast = new frmBroadcast();
            frmBroadcast.Wechat_secretId = Settings.Default.Wechat_Secret;
            frmBroadcast.Wechar_AppId = Settings.Default.Wechar_Invoice_AppId;

            frmBroadcast.ShowInTaskbar = false;

            frmBroadcast.ShowDialog();
        }

       

        
    }
}
