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

namespace ChinaUnion_Agent.StudyForm
{
    public partial class frmStudyReadLog : Form
    {

        public String performanceType = MyConstant.DIRECT;

        public frmStudyReadLog()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;


            // Queryworker.ReportProgress(4, "代理商佣金...\r\n");
            //代理商佣金
            StudyReceiverLogDao studyReceiverLogDao = new StudyReceiverLogDao();
            StudyDao studyDao = new StudyDao();
            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            IList<StudyReceiverLog> studyReceiverLogList = studyReceiverLogDao.GetList(this.txtSubjectKeyword.Text.Trim(), this.txtUserKeyword.Text.Trim(), this.dtDay.Value.ToString("yyyy-MM-dd"));
            dgStudyReadLog.Rows.Clear();
            dgStudyReadLog.Columns.Clear();
            if (studyReceiverLogList != null && studyReceiverLogList.Count > 0)
            {
                this.grpAgentFee.Text = "阅读日志(" + studyReceiverLogList.Count + ")";
                dgStudyReadLog.Columns.Add("渠道类型", "渠道类型");
                dgStudyReadLog.Columns.Add("渠道编码", "渠道编码");
                dgStudyReadLog.Columns.Add("渠道名称", "渠道名称");
                dgStudyReadLog.Columns.Add("代理商编号", "代理商编号");
                dgStudyReadLog.Columns.Add("代理商名称", "代理商名称");
                dgStudyReadLog.Columns.Add("用户账号", "用户账号");
                dgStudyReadLog.Columns.Add("用户名", "用户名");
                dgStudyReadLog.Columns.Add("用户微信", "用户微信");
                dgStudyReadLog.Columns.Add("阅读时间", "阅读时间");
                dgStudyReadLog.Columns.Add("主题", "主题");
                dgStudyReadLog.Columns.Add("内容", "内容");
               

                for (int i = 0; i < studyReceiverLogList.Count; i++)
                {

                   // Study study = studyDao.Get(Int32.Parse(studyReceiverLogList[i].studySequence));
                    //AgentWechatAccount agentWechatAccount = agentWechatAccountDao.Get(studyReceiverLogList[i].userId);
                    dgStudyReadLog.Rows.Add();
                    DataGridViewRow row = dgStudyReadLog.Rows[i];
                    if (studyReceiverLogList[i].agentContact != null)
                    {
                        row.Cells[0].Value = studyReceiverLogList[i].agentContact.type;
                        row.Cells[1].Value = studyReceiverLogList[i].agentContact.branchNo;
                        row.Cells[2].Value = studyReceiverLogList[i].agentContact.branchName;
                        row.Cells[3].Value = studyReceiverLogList[i].agentContact.agentNo;
                        row.Cells[4].Value = studyReceiverLogList[i].agentContact.agentName;
                        row.Cells[5].Value = studyReceiverLogList[i].agentContact.contactId;
                        row.Cells[6].Value = studyReceiverLogList[i].agentContact.contactName;
                        row.Cells[7].Value = studyReceiverLogList[i].agentContact.contactWechat;
                    }
                    row.Cells[8].Value = studyReceiverLogList[i].readtime;
                    if (studyReceiverLogList[i].study != null)
                    {
                        row.Cells[9].Value = studyReceiverLogList[i].study.subject;
                        row.Cells[10].Value = studyReceiverLogList[i].study.content;
                    }
                   



                }
            }
            dgStudyReadLog.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgStudyReadLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgStudyReadLog.AutoResizeColumns();


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
            ExportData.exportGridData(this.dgStudyReadLog);
            this.Cursor = Cursors.Default;
        }
     

        
    }
}
