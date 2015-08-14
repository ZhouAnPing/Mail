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

namespace ChinaUnion_Agent.ContactUs
{
    public partial class frmAgentContactQuery : Form
    {

        public frmAgentContactQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //代理商信息            
         //   Queryworker.ReportProgress(1, "代理商信息...\r\n");
            AgentContactDao agentContactDao = new AgentContactDao();
            //agentDao.Get("P001");
            IList<AgentContact> agentContactList = agentContactDao.GetListByKeyword(this.txtKeyword.Text.Trim());
            dgAgentContact.Rows.Clear();
            dgAgentContact.Columns.Clear();

            if (agentContactList != null && agentContactList.Count > 0)
            {              

                dgAgentContact.Columns.Add("代理商编号", "代理商编号");
                dgAgentContact.Columns.Add("代理商名称", "代理商名称");
                dgAgentContact.Columns.Add("渠道编码", "渠道编码");
                dgAgentContact.Columns.Add("渠道名称", "渠道名称");
                dgAgentContact.Columns.Add("所属区县", "所属区县");
                dgAgentContact.Columns.Add("所属网格", "所属网格");
                dgAgentContact.Columns.Add("联系人", "联系人");
                dgAgentContact.Columns.Add("电话", "电话");
               

                for (int i = 0; i < agentContactList.Count; i++)
                {
                    dgAgentContact.Rows.Add();
                    DataGridViewRow row = dgAgentContact.Rows[i];

                    row.Cells[0].Value = agentContactList[i].agentNo;
                    row.Cells[1].Value = agentContactList[i].agentName;
                    row.Cells[2].Value = agentContactList[i].branchNo;
                    row.Cells[3].Value = agentContactList[i].contactName;
                    row.Cells[4].Value = agentContactList[i].area;
                    row.Cells[5].Value = agentContactList[i].zone;
                    row.Cells[6].Value = agentContactList[i].contactName;
                    row.Cells[7].Value = agentContactList[i].contactTel;


                }
            }
          

            this.dgAgentContact.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgentContact.AutoResizeColumns();

         
            
           
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
