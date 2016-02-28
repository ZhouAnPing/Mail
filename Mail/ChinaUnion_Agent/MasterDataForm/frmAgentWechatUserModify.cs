using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChinaUnion_Agent.MasterDataForm
{
    public partial class frmAgentWechatUserModify : Form
    {
        private string secret = Settings.Default.Wechat_Secret;
        public DataGridViewRow row = new DataGridViewRow();
        public WechatJsonUser wechatJsonUser = new WechatJsonUser();
        WechatAction wechatAction = new WechatAction();
        IList<int> department = new List<int>();
        public AgentWechatAccount retAgentWechatAccount = new AgentWechatAccount();

        public frmAgentWechatUserModify()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(this.txtAgentNo.Text.Trim()))
            {
                MessageBox.Show("编号不能为空");
                this.txtAgentNo.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtUserId.Text.Trim()))
            {
                MessageBox.Show("账号不能为空");
                this.txtAgentName.Focus();
                return;
            }

            if (String.IsNullOrEmpty(this.txtWeixinId.Text.Trim()) && String.IsNullOrEmpty(this.txtMobile.Text.Trim()) && String.IsNullOrEmpty(this.txtEmail.Text.Trim()))
            {
                MessageBox.Show("微信号，手机和邮箱不能同时为空");
                return;
            }

            String EmailReg = @"^\w+([\.\-]\w+)*\@\w+([\.\-]\w+)*\.\w+$";
            if (!String.IsNullOrEmpty(txtEmail.Text.Trim()) && !Regex.IsMatch(txtEmail.Text.Trim(), EmailReg))
            {
                MessageBox.Show("请输入有效的邮箱地址");
                this.txtEmail.Focus();
                return;
            }

            String MobileReg = @"^[1]+[3,5,8]+\d{9}";
            if (!String.IsNullOrEmpty(txtMobile.Text.Trim()) && !Regex.IsMatch(this.txtMobile.Text.Trim(), MobileReg))
            {
                MessageBox.Show("请输入有效的手机号码");
                this.txtMobile.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            AgentWechatAccount agentWechatAccount = new AgentWechatAccount();
            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            agentWechatAccount = agentWechatAccountDao.Get(this.txtUserId.Text);
            agentWechatAccount.contactEmail = this.txtEmail.Text;
            agentWechatAccount.contactId = this.txtUserId.Text;
            agentWechatAccount.contactName = this.txtUserName.Text;

            agentWechatAccount.contactTel = this.txtMobile.Text;
            agentWechatAccount.contactWechat = this.txtWeixinId.Text;
            agentWechatAccount.feeRight = "N";
            agentWechatAccount.policyRight = "N";
            agentWechatAccount.performanceRight = "N";
            agentWechatAccount.studyRight = "N";
            agentWechatAccount.complainRight = "N";
            agentWechatAccount.monitorRight = "N"; ;
            agentWechatAccount.errorRight = "N";
            agentWechatAccount.contactRight = "N";
            if (this.chkFee.Checked)
            {
                agentWechatAccount.feeRight = "Y";
            }
            if (this.chkPolicy.Checked)
            {
                agentWechatAccount.policyRight = "Y";
            }
            if (this.chkPerformance.Checked)
            {
                agentWechatAccount.performanceRight = "Y";
            }
            if (this.chkStudy.Checked)
            {
                agentWechatAccount.studyRight = "Y";
            }
            if (this.chkComplain.Checked)
            {
                agentWechatAccount.complainRight = "Y";
            }
            if (this.chkService.Checked)
            {
                agentWechatAccount.monitorRight = "Y";
            }
            if (this.chkError.Checked)
            {
                agentWechatAccount.errorRight = "Y";
            }
            if (this.chkContactUs.Checked)
            {
                agentWechatAccount.contactRight = "Y";
            }
            this.retAgentWechatAccount = agentWechatAccount;
            agentWechatAccountDao.Update(agentWechatAccount);
            this.Cursor = Cursors.Default;
            DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmAddWechatUser_Load(object sender, EventArgs e)
        {
           

            if ( row.Cells[12].Value.ToString().ToUpper().Equals("Y"))
            {
                this.chkFee.Checked = true;
               // feeRightUserIds.Add(userid);
            }
            if (row.Cells[13].Value.ToString().ToUpper().Equals("Y"))
            {
                this.chkPolicy.Checked = true;
            }
            if (row.Cells[14].Value.ToString().ToUpper().Equals("Y"))
            {
                this.chkPerformance.Checked = true;
            }
            if (row.Cells[15].Value.ToString().ToUpper().Equals("Y"))
            {
                this.chkStudy.Checked = true;
            }
            if (row.Cells[16].Value.ToString().ToUpper().Equals("Y"))
            {
                this.chkComplain.Checked = true;
            }
            if (row.Cells[17].Value.ToString().ToUpper().Equals("Y"))
            {
                this.chkService.Checked = true;
            }
            if (row.Cells[18].Value.ToString().ToUpper().Equals("Y"))
            {
                this.chkError.Checked = true;
            }
            if (row.Cells[19].Value.ToString().ToUpper().Equals("Y"))
            {
                this.chkContactUs.Checked = true;
            }

            
            this.txtUserId.Text = row.Cells[6].Value.ToString();

            this.txtUserName.Text = row.Cells[7].Value.ToString();
            if (string.IsNullOrEmpty(wechatJsonUser.name))
            {
                wechatJsonUser.name = wechatJsonUser.userid;
            }
            this.txtEmail.Text = row.Cells[8].Value.ToString();
            this.txtMobile.Text = row.Cells[9].Value.ToString();
            this.txtWeixinId.Text = row.Cells[10].Value.ToString();

            this.txtAgentName.Text = row.Cells[2].Value.ToString();
            this.txtAgentNo.Text = row.Cells[1].Value.ToString();
            if (String.IsNullOrEmpty(this.txtAgentName.Text))
            {
                this.txtAgentName.Text = row.Cells[4].Value.ToString();
                this.txtAgentNo.Text = row.Cells[3].Value.ToString();
            }
            //wechatJsonUser.position = row.Cells[4].Value.ToString();





        }
    }
       
}
