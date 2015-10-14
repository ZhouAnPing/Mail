using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.Analysis
{
    public partial class frmReport : frmBase
    {
        WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
        WechatAction wechatAction = new WechatAction();
        
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmPolicyPublish_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.initControl();
        }

        private void initControl()
        {
            this.Cursor = Cursors.WaitCursor;

            this.cboType.SelectedIndex = 0;
            this.cboTypeUsered.SelectedIndex = 0;
            this.dtStartDate.Value = DateTime.Now.AddMonths(-1);
            this.dtUsedStartTime.Value = DateTime.Now.AddMonths(-1);

            ArrayList attentionUseList = new ArrayList();
            ArrayList unAttentionUseList = new ArrayList();

            WechatUser fromWechatUser1 = wechatAction.getAllUserFromWechatByStatus(1, Settings.Default.Wechat_Secret, 1);

            if (fromWechatUser1 != null && fromWechatUser1.userlist != null)
            {
                this.lblAttentionUser.Text = fromWechatUser1.userlist.Count.ToString();

                attentionUseList.Clear();
                foreach (Wechat.User user in fromWechatUser1.userlist)
                {
                    attentionUseList.Add(user.userid);
                }
            }
            WechatUser fromWechatUser2 = wechatAction.getAllUserFromWechatByStatus(1, Settings.Default.Wechat_Secret, 4);
            if (fromWechatUser2 != null && fromWechatUser2.userlist != null)
            {
                this.lblUnAttentionUser.Text = fromWechatUser2.userlist.Count.ToString();

                unAttentionUseList.Clear();
                foreach (Wechat.User user in fromWechatUser2.userlist)
                {
                    unAttentionUseList.Add(user.userid);
                }
            }
            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            IList<AgentWechatAccount> agentWechatAccountList = agentWechatAccountDao.GetListByKeyword("", "");
            if (agentWechatAccountList != null)
            {
                this.lblAllUser.Text = agentWechatAccountList.Count.ToString();
            }

            this.lblUnSyncUser.Text = (int.Parse(this.lblAllUser.Text) - int.Parse(this.lblAttentionUser.Text) - int.Parse(this.lblUnAttentionUser.Text)).ToString();


            dgUser.Columns.Clear();
            dgUser.Columns.Add("联系人编号", "联系人编号");
            dgUser.Columns.Add("联系人姓名", "联系人姓名");
            dgUser.Columns.Add("联系人邮箱", "联系人邮箱");
            dgUser.Columns.Add("联系人电话", "联系人电话");
            dgUser.Columns.Add("联系人微信", "联系人微信");
            dgUser.Columns.Add("关注状态", "关注状态");
            for (int i = 0; i < agentWechatAccountList.Count; i++)
            {
                dgUser.Rows.Add();
                DataGridViewRow row = dgUser.Rows[i];
                int index = 0;
                row.Cells[index++].Value = agentWechatAccountList[i].contactId;
                row.Cells[index++].Value = agentWechatAccountList[i].contactName;
                row.Cells[index++].Value = agentWechatAccountList[i].contactEmail;
                row.Cells[index++].Value = agentWechatAccountList[i].contactTel;
                row.Cells[index++].Value = agentWechatAccountList[i].contactWechat;
                row.Cells[index].Value = "未同步";
                row.Cells[index].Style.BackColor = Color.White;
                if (attentionUseList.Contains(agentWechatAccountList[i].contactId))
                {
                    row.Cells[index].Value = "已关注";
                    row.Cells[index].Style.BackColor = Color.Blue;
                }
                if (unAttentionUseList.Contains(agentWechatAccountList[i].contactId))
                {
                    row.Cells[index].Value = "未关注";
                    row.Cells[index].Style.BackColor = Color.Red;
                }
            }
            dgUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgUser.AutoResizeColumns();

            this.Cursor = Cursors.Default;
        }


        private void btnExportUser_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgUser);
            this.Cursor = Cursors.Default;
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);

            String queryEndTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            //获取本周第一天  
            String queryStartTime = DateTime.Now.AddDays(dayspan).ToString("yyyy-MM-dd");

            IList<WechatQueryLog> list = wechatQueryLogDao.GetList(this.cboType.Text, "成员进入应用", "", queryStartTime, queryEndTime);

            if (list != null)
            {
                this.lblWeekActive.Text = list.Count.ToString();
            }
            //本月第一天  
            queryStartTime = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");
            list = wechatQueryLogDao.GetList(this.cboType.Text, "成员进入应用", "", queryStartTime, queryEndTime);
            if (list != null)
            {
                this.lblMonthActive.Text = list.Count.ToString();
            }


            //本年第一天  
            queryStartTime = dt.AddMonths(-dt.Month + 1).AddDays(-dt.Day + 1).ToString("yyyy-MM-dd");
            list = wechatQueryLogDao.GetList(this.cboType.Text, "成员进入应用", "", queryStartTime, queryEndTime);
            if (list != null)
            {
                this.lblYearActive.Text = list.Count.ToString();
            }
        }

        private void btnQuery_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            String queryEndTime = this.dtEndDate.Value.AddDays(1).ToString("yyyy-MM-dd");

            //获取本周第一天  
            String queryStartTime = this.dtStartDate.Value.ToString("yyyy-MM-dd");
            IList<WechatQueryLog> list = wechatQueryLogDao.GetList(this.cboType.Text, "成员进入应用", "", queryStartTime, queryEndTime);


            this.dgWechatActiveLog.Columns.Clear();
            dgWechatActiveLog.Columns.Add("用户编号", "用户编号");
           // dgWechatActiveLog.Columns.Add("用户名称", "用户名称");
            dgWechatActiveLog.Columns.Add("进入应用时间", "进入应用时间");
            for (int i = 0; i < list.Count; i++)
            {
                dgWechatActiveLog.Rows.Add();
                DataGridViewRow row = dgWechatActiveLog.Rows[i];
                int index = 0;
                row.Cells[index++].Value = list[i].wechatId;
               // row.Cells[index++].Value = list[i].contactName;
                row.Cells[index++].Value = list[i].queryTime;
            }

            this.grpActive.Text = "活跃次数详情:"+list.Count;
            dgWechatActiveLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgWechatActiveLog.AutoResizeColumns();
            this.Cursor = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgWechatActiveLog);
            this.Cursor = Cursors.Default;
        }

        private void btnExportUsed_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgUsedUser);
            this.Cursor = Cursors.Default;
        }

        private void cboTypeUsered_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            int weeknow = Convert.ToInt32(DateTime.Now.DayOfWeek);
            int dayspan = (-1) * weeknow + 1;
            DateTime dt2 = dt.AddMonths(1);

            String queryEndTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            //获取本周第一天  
            String queryStartTime = DateTime.Now.AddDays(dayspan).ToString("yyyy-MM-dd");

            IList<WechatQueryLog> list = wechatQueryLogDao.GetUsedSummary(this.cboTypeUsered.Text, queryStartTime, queryEndTime);

            if (list != null)
            {
                this.lblUsedWeek.Text = list.Count.ToString();
            }
            //本月第一天  
            queryStartTime = dt.AddDays(-(dt.Day) + 1).ToString("yyyy-MM-dd");
            list = wechatQueryLogDao.GetUsedSummary(this.cboTypeUsered.Text, queryStartTime, queryEndTime);
            if (list != null)
            {
                this.lblUsedMonth.Text = list.Count.ToString();
            }


            //本年第一天  
            queryStartTime = dt.AddMonths(-dt.Month + 1).AddDays(-dt.Day + 1).ToString("yyyy-MM-dd");
            list = wechatQueryLogDao.GetUsedSummary(this.cboTypeUsered.Text, queryStartTime, queryEndTime);
            if (list != null)
            {
                this.lblUsedYear.Text = list.Count.ToString();
            }
        }

        private void btnQueryUsed_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            String queryEndTime = this.dtUsedEndTime.Value.AddDays(1).ToString("yyyy-MM-dd");

            //获取本周第一天  
            String queryStartTime = this.dtUsedStartTime.Value.ToString("yyyy-MM-dd");
            IList<WechatQueryLog> list = wechatQueryLogDao.GetUsedDetail(this.cboTypeUsered.Text, queryStartTime, queryEndTime);


            this.dgUsedUser.Columns.Clear();
            dgUsedUser.Columns.Add("用户编号", "用户编号");
            //dgUsedUser.Columns.Add("用户名称", "用户名称");
            dgUsedUser.Columns.Add("查询时间", "查询时间");
            dgUsedUser.Columns.Add("查询关键字", "查询关键字");
            for (int i = 0; i < list.Count; i++)
            {
                dgUsedUser.Rows.Add();
                DataGridViewRow row = dgUsedUser.Rows[i];
                int index = 0;
                row.Cells[index++].Value = list[i].wechatId;
               // row.Cells[index++].Value = list[i].contactName;
                row.Cells[index++].Value = list[i].queryTime;
                row.Cells[index++].Value = list[i].queryString;
            }

            this.grpUsedUser.Text = "微信使用率详情:" + list.Count;
            dgUsedUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgUsedUser.AutoResizeColumns();
            this.Cursor = Cursors.Default;
        }

    }
}
