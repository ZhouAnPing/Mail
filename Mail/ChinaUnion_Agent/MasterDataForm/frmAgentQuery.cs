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

namespace ChinaUnion_Agent.MasterDataForm
{
    public partial class frmAgentQuery : Form
    {
       
        public frmAgentQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //代理商信息            
         //   Queryworker.ReportProgress(1, "代理商信息...\r\n");
            AgentDao agentDao = new AgentDao();
            //agentDao.Get("P001");
            IList<Agent> agentList = agentDao.GetList();
            dgAgent.Rows.Clear();
            dgAgent.Columns.Clear();

            if (agentList != null && agentList.Count > 0)
            {
                

                dgAgent.Columns.Add("代理商编号", "代理商编号");
                dgAgent.Columns.Add("代理商名称", "代理商名称");
                dgAgent.Columns.Add("联系人邮箱", "联系人邮箱");
                dgAgent.Columns.Add("联系人姓名", "联系人姓名");
                dgAgent.Columns.Add("联系人电话", "联系人电话");
                dgAgent.Columns.Add("联系人微信账号", "联系人微信账号");

                dgAgent.Columns.Add("是否禁用", "是否禁用");

                for (int i = 0; i < agentList.Count; i++)
                {
                    dgAgent.Rows.Add();
                    DataGridViewRow row = dgAgent.Rows[i];

                    row.Cells[0].Value = agentList[i].agentNo;
                    row.Cells[1].Value = agentList[i].agentName;
                    row.Cells[2].Value = agentList[i].contactEmail;
                    row.Cells[3].Value = agentList[i].contactName;
                    row.Cells[4].Value = agentList[i].contactTel;
                    row.Cells[5].Value = agentList[i].contactWechatAccount;
                    if (!String.IsNullOrEmpty(agentList[i].status) && agentList[i].status.ToUpper().Equals("Y"))
                    {
                        row.Cells[6].Value = "账号已经停用";
                    }
                    else
                    {
                        row.Cells[6].Value = "";
                    }
                    


                }
            }
           // Queryworker.ReportProgress(2, "代理商渠道类型...\r\n");
            //代理商渠道类型
            AgentTypeDao agentTypeDao = new AgentTypeDao();
            IList<AgentType> agentTypeList = agentTypeDao.GetList(dtFeeMonth.Value.ToString("yyyy-MM"));
            dgAgentType.Rows.Clear();
            dgAgentType.Columns.Clear();
            if (agentTypeList != null && agentTypeList.Count > 0)
            {
               
                dgAgentType.Columns.Add("代理商编号", "代理商编号");
                dgAgentType.Columns.Add("渠道类型", "渠道类型");


                for (int i = 0; i < agentTypeList.Count; i++)
                {
                    dgAgentType.Rows.Add();
                    DataGridViewRow row = dgAgentType.Rows[i];
                    row.Cells[0].Value = agentTypeList[i].agentNo;
                    row.Cells[1].Value = agentTypeList[i].agentType;

                }
            }
          //  Queryworker.ReportProgress(3, "代理商渠道类型说明...\r\n");
            //代理商渠道类型说明
            AgentTypeCommentDao agentTypeCommentDao = new AgentTypeCommentDao();
            IList<AgentTypeComment> agentTypeCommentList = agentTypeCommentDao.GetList(dtFeeMonth.Value.ToString("yyyy-MM"));
            dgAgentTypeComment.Rows.Clear();
            dgAgentTypeComment.Columns.Clear();
            if (agentTypeCommentList != null && agentTypeCommentList.Count > 0)
            {
               

                dgAgentTypeComment.Columns.Add("渠道类型", "渠道类型");
                dgAgentTypeComment.Columns.Add("佣金说明", "佣金说明");




                for (int i = 0; i < agentTypeCommentList.Count; i++)
                {
                    dgAgentTypeComment.Rows.Add();
                    DataGridViewRow row = dgAgentTypeComment.Rows[i];
                    row.Cells[0].Value = agentTypeCommentList[i].agentType;
                    row.Cells[1].Value = agentTypeCommentList[i].agentTypeComment;

                }
            }


            this.dgAgent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgent.AutoResizeColumns();

            this.dgAgentType.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgentType.AutoResizeColumns();
            this.dgAgentTypeComment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgentTypeComment.AutoResizeColumns();
            
           
            this.Cursor = Cursors.Default;     
           

        }

       

     
      
        private void frmAgentQuery_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.dtFeeMonth.Value = DateTime.Now.AddMonths(-1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
     

        
    }
}
