using ChinaUnion_Agent.Util;
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
using System.Windows.Forms;
using TripolisDialogueAdapter;

namespace ChinaUnion_Agent.PolicyForm
{
    public partial class frmPolicyReadLog : Form
    {

        public String performanceType = MyConstant.DIRECT;

        public frmPolicyReadLog()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;


            // Queryworker.ReportProgress(4, "代理商佣金...\r\n");
            //代理商佣金
            PolicyReceiverLogDao policyReceiverLogDao = new PolicyReceiverLogDao();
            PolicyDao policyDao = new PolicyDao();
            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            IList<PolicyReceiverLog> policyReceiverLogList = policyReceiverLogDao.GetList(this.txtSubjectKeyword.Text.Trim(), this.txtUserKeyword.Text.Trim(), this.dtDay.Value.ToString("yyyy-MM-dd"));
            dgPolicyReadLog.Rows.Clear();
            dgPolicyReadLog.Columns.Clear();
            if (policyReceiverLogList != null && policyReceiverLogList.Count > 0)
            {
                this.grpAgentFee.Text = "阅读日志(" + policyReceiverLogList.Count + ")";
                dgPolicyReadLog.Columns.Add("渠道类型", "渠道类型");
                dgPolicyReadLog.Columns.Add("渠道编码", "渠道编码");
                dgPolicyReadLog.Columns.Add("渠道名称", "渠道名称");
                dgPolicyReadLog.Columns.Add("代理商编号", "代理商编号");
                dgPolicyReadLog.Columns.Add("代理商名称", "代理商名称");
                dgPolicyReadLog.Columns.Add("用户账号", "用户账号");
                dgPolicyReadLog.Columns.Add("用户名", "用户名");
                dgPolicyReadLog.Columns.Add("用户微信", "用户微信");
                dgPolicyReadLog.Columns.Add("阅读时间", "阅读时间");
                dgPolicyReadLog.Columns.Add("主题", "主题");
                dgPolicyReadLog.Columns.Add("内容", "内容");
               

                for (int i = 0; i < policyReceiverLogList.Count; i++)
                {

                   // Policy policy = policyDao.Get(Int32.Parse(policyReceiverLogList[i].policySequence));
                    //AgentWechatAccount agentWechatAccount = agentWechatAccountDao.Get(policyReceiverLogList[i].userId);
                    dgPolicyReadLog.Rows.Add();
                    DataGridViewRow row = dgPolicyReadLog.Rows[i];
                    if (policyReceiverLogList[i].agentContact != null)
                    {
                        row.Cells[0].Value = policyReceiverLogList[i].agentContact.type;
                        row.Cells[1].Value = policyReceiverLogList[i].agentContact.branchNo;
                        row.Cells[2].Value = policyReceiverLogList[i].agentContact.branchName;
                        row.Cells[3].Value = policyReceiverLogList[i].agentContact.agentNo;
                        row.Cells[4].Value = policyReceiverLogList[i].agentContact.agentName;
                        row.Cells[5].Value = policyReceiverLogList[i].agentContact.contactId;
                        row.Cells[6].Value = policyReceiverLogList[i].agentContact.contactName;
                        row.Cells[7].Value = policyReceiverLogList[i].agentContact.contactWechat;
                    }
                    row.Cells[8].Value = policyReceiverLogList[i].readtime;
                    if (policyReceiverLogList[i].policy != null)
                    {
                        row.Cells[9].Value = policyReceiverLogList[i].policy.subject;
                        row.Cells[10].Value = policyReceiverLogList[i].policy.content;
                    }
                   



                }
            }
            dgPolicyReadLog.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgPolicyReadLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPolicyReadLog.AutoResizeColumns();


            this.Cursor = Cursors.Default;


        }

       
     
      
        private void frmAgentQuery_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.dtDay.Value = DateTime.Now.AddDays(-1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgPolicyReadLog);
            this.Cursor = Cursors.Default;
        }
     

        
    }
}
