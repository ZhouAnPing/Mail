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
    public partial class frmAgentTypeQuery : Form
    {
       
        public frmAgentTypeQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
           
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



            dgAgentType.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgAgentType.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgentType.AutoResizeColumns();
            dgAgentTypeComment.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
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
