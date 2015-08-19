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
    public partial class frmAgentBonusQuery : Form
    {

        public frmAgentBonusQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
           

            //代理商信息            
         //   Queryworker.ReportProgress(1, "代理商信息...\r\n");
            AgentBonusDao agentBonusDao = new AgentBonusDao();
            //agentDao.Get("P001");
            IList<AgentBonus> agentBonusList = agentBonusDao.GetListByKeyword(this.txtKeyword.Text.Trim());
            dgAgentBonus.Rows.Clear();
            dgAgentBonus.Columns.Clear();

            if (agentBonusList != null && agentBonusList.Count > 0)
            {              

                
                dgAgentBonus.Columns.Add("渠道编码", "渠道编码");
                dgAgentBonus.Columns.Add("渠道名称", "渠道名称");
                dgAgentBonus.Columns.Add("渠道积分奖励", "渠道积分奖励");
                dgAgentBonus.Columns.Add("后付费奖励", "后付费奖励");
                dgAgentBonus.Columns.Add("渠道星级补贴", "渠道星级补贴");
                dgAgentBonus.Columns.Add("红包总金额", "红包总金额");
               

                for (int i = 0; i < agentBonusList.Count; i++)
                {
                    dgAgentBonus.Rows.Add();
                    DataGridViewRow row = dgAgentBonus.Rows[i];

                    row.Cells[0].Value = agentBonusList[i].agentNo;
                    row.Cells[1].Value = agentBonusList[i].agentName;
                    row.Cells[2].Value = agentBonusList[i].scoreBonus;
                    row.Cells[3].Value = agentBonusList[i].afterFeeBonus;
                    row.Cells[4].Value = agentBonusList[i].starBonus;
                    row.Cells[5].Value = agentBonusList[i].totalBonus;
                  


                }
            }


            dgAgentBonus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgAgentBonus.AutoResizeColumns();

         
            
           
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
