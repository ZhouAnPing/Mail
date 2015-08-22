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

namespace ChinaUnion_Agent.PerformanceForm
{
    public partial class frmAgentMonthPerformanceQuery : Form
    {

        public String performanceType = MyConstant.DIRECT;
       
        public frmAgentMonthPerformanceQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;


            AgentMonthPerformanceDao agentMonthPerformanceDao = new AgentMonthPerformanceDao();
            IList<AgentMonthPerformance> agentMonthPerformanceList = agentMonthPerformanceDao.GetAllList(this.dtFeeMonth.Value.ToString("yyyy-MM"), this.cboType.Text);
            dgAgentPerformance.Rows.Clear();
            dgAgentPerformance.Columns.Clear();
            if (agentMonthPerformanceList != null && agentMonthPerformanceList.Count > 0)
            {
                this.grpAgentFee.Text = "月度绩效信息(" + agentMonthPerformanceList.Count + ")";
                dgAgentPerformance.Columns.Add("渠道类型", "渠道类型");
                dgAgentPerformance.Columns.Add("渠道编码", "渠道编码");
                dgAgentPerformance.Columns.Add("渠道名称", "渠道名称");

                dgAgentPerformance.Columns.Add("代理商编号", "代理商编号");
                dgAgentPerformance.Columns.Add("代理商名称", "代理商名称");

                if (this.cboType.Text.Equals("直供渠道"))
                {
                    dgAgentPerformance.Columns[3].Visible = false;
                    dgAgentPerformance.Columns[4].Visible = false;
                }

                for (int i = 0; i < agentMonthPerformanceList.Count; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 1; j <= 100; j++)
                        {
                            FieldInfo feeNameField = agentMonthPerformanceList[i].GetType().GetField("feeName" + j);
                            // FieldInfo feeField = agentFeeList[i].GetType().GetField("fee" + j);

                            String feeNameFieldValue = feeNameField.GetValue(agentMonthPerformanceList[i]) == null ? null : feeNameField.GetValue(agentMonthPerformanceList[i]).ToString();
                            // String feeFieldValue = feeField.GetValue(agentFeeList[i]) == null ? null : feeField.GetValue(agentFeeList[i]).ToString(); ;

                            if (!String.IsNullOrEmpty(feeNameFieldValue) && !String.IsNullOrWhiteSpace(feeNameFieldValue))
                            {
                                dgAgentPerformance.Columns.Add(feeNameFieldValue, feeNameFieldValue);
                            }
                        }




                    }


                    dgAgentPerformance.Rows.Add();
                    DataGridViewRow row = dgAgentPerformance.Rows[i];
                    row.Cells[0].Value = agentMonthPerformanceList[i].type;
                    row.Cells[1].Value = agentMonthPerformanceList[i].branchNo;
                    row.Cells[2].Value = agentMonthPerformanceList[i].branchName;


                    row.Cells[3].Value = agentMonthPerformanceList[i].agentNo;
                    row.Cells[4].Value = agentMonthPerformanceList[i].agentName;
                    int feeColIndex = 4;
                    int fixColCount = feeColIndex + 1;


                    for (int j = 1; j <= 100; j++)
                    {
                        // FieldInfo feeNameField = agentFeeList[i].GetType().GetField("feeName" + j);
                        FieldInfo feeField = agentMonthPerformanceList[i].GetType().GetField("fee" + j);

                        //  String feeNameFieldValue = feeNameField.GetValue(agentFeeList[i]) == null ? null : feeNameField.GetValue(agentFeeList[i]).ToString();
                        String feeFieldValue = feeField.GetValue(agentMonthPerformanceList[i]) == null ? null : feeField.GetValue(agentMonthPerformanceList[i]).ToString(); ;

                        if (dgAgentPerformance.Columns.Count >= fixColCount + j)
                        {
                            row.Cells[feeColIndex + j].Value = feeFieldValue;
                        }
                    }



                }
            }
            dgAgentPerformance.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgAgentPerformance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgentPerformance.AutoResizeColumns();
           

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

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgAgentPerformance);
            this.Cursor = Cursors.Default;
        }
     

        
    }
}
