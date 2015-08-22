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
            AgentBonusDao agentBonusDao = new AgentBonusDao();
            IList<AgentBonus> agentBonusList = agentBonusDao.GetListByKeyword(this.txtKeyword.Text.Trim(),dtFeeMonth.Value.ToString("yyyyMM"));
            dgAgentBonus.Rows.Clear();
            dgAgentBonus.Columns.Clear();
            if (agentBonusList != null && agentBonusList.Count > 0)
            {
                this.groupBox1.Text = "红包信息(" + agentBonusList.Count + ")";
               
                    dgAgentBonus.Columns.Add("代理商编号", "代理商编号");
                    dgAgentBonus.Columns.Add("代理商名称", "代理商名称");

               
                for (int i = 0; i < agentBonusList.Count; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 1; j <= 100; j++)
                        {
                            FieldInfo feeNameField = agentBonusList[i].GetType().GetField("feeName" + j);
                            // FieldInfo feeField = agentFeeList[i].GetType().GetField("fee" + j);

                            String feeNameFieldValue = feeNameField.GetValue(agentBonusList[i]) == null ? null : feeNameField.GetValue(agentBonusList[i]).ToString();
                            // String feeFieldValue = feeField.GetValue(agentFeeList[i]) == null ? null : feeField.GetValue(agentFeeList[i]).ToString(); ;

                            if (!String.IsNullOrEmpty(feeNameFieldValue) && !String.IsNullOrWhiteSpace(feeNameFieldValue))
                            {
                                dgAgentBonus.Columns.Add(feeNameFieldValue, feeNameFieldValue);
                            }
                        }
                    }


                    dgAgentBonus.Rows.Add();
                    DataGridViewRow row = dgAgentBonus.Rows[i];

                    row.Cells[0].Value = agentBonusList[i].agentNo;
                    row.Cells[1].Value = agentBonusList[i].agentName;
                    int feeColIndex = 1;
                    int fixColCount = feeColIndex + 1;
                    


                    for (int j = 1; j <= 100; j++)
                    {
                        // FieldInfo feeNameField = agentFeeList[i].GetType().GetField("feeName" + j);
                        FieldInfo feeField = agentBonusList[i].GetType().GetField("fee" + j);

                        //  String feeNameFieldValue = feeNameField.GetValue(agentFeeList[i]) == null ? null : feeNameField.GetValue(agentFeeList[i]).ToString();
                        String feeFieldValue = feeField.GetValue(agentBonusList[i]) == null ? null : feeField.GetValue(agentBonusList[i]).ToString(); ;

                        if (dgAgentBonus.Columns.Count >= fixColCount + j)
                        {
                            row.Cells[feeColIndex + j].Value = feeFieldValue;
                        }
                    }



                }
            }
            dgAgentBonus.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgAgentBonus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgentBonus.AutoResizeColumns();

         
            
           
            this.Cursor = Cursors.Default;     
           

        }

       

     
      
        private void frmAgentQuery_Load(object sender, EventArgs e)
        {
            this.dtFeeMonth.Value = DateTime.Now.AddMonths(-1);
            this.WindowState = FormWindowState.Maximized;
            this.txtKeyword.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
     

        
    }
}
