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

namespace ChinaUnion_Agent.ScoreGrade
{
    public partial class frmAgentScoreStarQuery : Form
    {

        public frmAgentScoreStarQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //代理商信息            
         //   Queryworker.ReportProgress(1, "代理商信息...\r\n");
            AgentStarDao agentStarDao = new AgentStarDao();
            //agentDao.Get("P001");
            IList<AgentStar> agentStarList = agentStarDao.GetListByKeyword(this.txtKeyword.Text.Trim());
            dgAgentStar.Rows.Clear();
            dgAgentStar.Columns.Clear();

            if (agentStarList != null && agentStarList.Count > 0)
            {
                dgAgentStar.Columns.Add("时间", "时间");
                dgAgentStar.Columns.Add("代理商编号", "代理商编号");
                dgAgentStar.Columns.Add("代理商名称", "代理商名称");
                dgAgentStar.Columns.Add("渠道编码", "渠道编码");
                dgAgentStar.Columns.Add("渠道名称", "渠道名称");
                dgAgentStar.Columns.Add("星级", "星级");

               
               

                for (int i = 0; i < agentStarList.Count; i++)
                {
                    dgAgentStar.Rows.Add();
                    DataGridViewRow row = dgAgentStar.Rows[i];
                    row.Cells[0].Value = agentStarList[i].dateTime;
                    row.Cells[1].Value = agentStarList[i].agentNo;
                    row.Cells[2].Value = agentStarList[i].agentName;
                    row.Cells[3].Value = agentStarList[i].branchNo;                  
                    row.Cells[4].Value = agentStarList[i].branchName;
                    row.Cells[5].Value = agentStarList[i].star;
                }
            }


            this.dgAgentStar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgAgentStar.AutoResizeColumns();


            AgentScoreDao agentScoreDao = new AgentScoreDao();
            //agentDao.Get("P001");
            IList<AgentScore> agentScoreList = agentScoreDao.GetListByKeyword(this.txtKeyword.Text.Trim());
            dgAgentScore.Rows.Clear();
            dgAgentScore.Columns.Clear();

            if (agentScoreList != null && agentScoreList.Count > 0)
            {
                dgAgentScore.Columns.Add("时间", "时间");
                dgAgentScore.Columns.Add("代理商编号", "代理商编号");
                dgAgentScore.Columns.Add("代理商名称", "代理商名称");
                dgAgentScore.Columns.Add("渠道编码", "渠道编码");
                dgAgentScore.Columns.Add("渠道名称", "渠道名称");
                dgAgentScore.Columns.Add("积分", "积分");
                dgAgentScore.Columns.Add("本月积分", "本月积分");



                for (int i = 0; i < agentScoreList.Count; i++)
                {
                    dgAgentScore.Rows.Add();
                    DataGridViewRow row = dgAgentScore.Rows[i];
                    row.Cells[0].Value = agentScoreList[i].dateTime;
                    row.Cells[1].Value = agentScoreList[i].agentNo;
                    row.Cells[2].Value = agentScoreList[i].agentName;
                    row.Cells[3].Value = agentScoreList[i].branchNo;
                    row.Cells[4].Value = agentScoreList[i].branchName;
                    row.Cells[5].Value = agentScoreList[i].score;
                    row.Cells[6].Value = agentScoreList[i].standardScore;
                }
            }
            this.dgAgentScore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgAgentScore.AutoResizeColumns();
            
           
            this.Cursor = Cursors.Default;     
           

        }

       

     
      
        private void frmAgentQuery_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.txtKeyword.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
     

        
    }
}
