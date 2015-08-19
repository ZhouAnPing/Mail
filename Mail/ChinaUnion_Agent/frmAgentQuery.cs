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

namespace ChinaUnion_Agent
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

           // Queryworker.ReportProgress(4, "代理商佣金...\r\n");
            //代理商佣金
            AgentFeeDao agentFeeDao = new AgentFeeDao();
            IList<AgentFee> agentFeeList = agentFeeDao.GetList(dtFeeMonth.Value.ToString("yyyy-MM"));
            dgAgentFee.Rows.Clear();
            dgAgentFee.Columns.Clear();
            if (agentFeeList != null && agentFeeList.Count > 0)
            {
                this.grpAgentFee.Text = "月度佣金明细信息(" + agentFeeList.Count + ")";
                dgAgentFee.Columns.Add("代理商编号", "代理商编号");
                dgAgentFee.Columns.Add("代理商名称", "代理商名称");
                dgAgentFee.Columns.Add("代理商类型", "代理商类型");
                dgAgentFee.Columns.Add("代理商类型说明", "代理商类型说明");
                dgAgentFee.Columns.Add("联系人邮件", "联系人邮件");
                dgAgentFee.Columns.Add("联系人名称", "联系人名称");
                dgAgentFee.Columns.Add("告知单编号", "告知单编号");

                for (int i = 0; i < agentFeeList.Count; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 1; j <= 100; j++)
                        {
                            FieldInfo feeNameField = agentFeeList[i].GetType().GetField("feeName" + j);
                           // FieldInfo feeField = agentFeeList[i].GetType().GetField("fee" + j);

                            String feeNameFieldValue = feeNameField.GetValue(agentFeeList[i]) == null ? null : feeNameField.GetValue(agentFeeList[i]).ToString();
                           // String feeFieldValue = feeField.GetValue(agentFeeList[i]) == null ? null : feeField.GetValue(agentFeeList[i]).ToString(); ;

                            if (!String.IsNullOrEmpty(feeNameFieldValue) && !String.IsNullOrWhiteSpace(feeNameFieldValue))
                            {
                                dgAgentFee.Columns.Add(feeNameFieldValue, feeNameFieldValue);
                            }
                        }
                       

                        this.dgAgentFee.Columns.Add("总计", "总计");
                        this.dgAgentFee.Columns.Add("开票金额", "开票金额");
                        this.dgAgentFee.Columns.Add("过网开票金额", "过网开票金额");

                    }


                    dgAgentFee.Rows.Add();
                    DataGridViewRow row = dgAgentFee.Rows[i];

                    row.Cells[0].Value = agentFeeList[i].agentNo;
                    row.Cells[1].Value = agentFeeList[i].agent.agentName;
                    row.Cells[2].Value = agentFeeList[i].agent.agentType;
                    row.Cells[3].Value = agentFeeList[i].agent.agentTypeComment;
                    row.Cells[4].Value = agentFeeList[i].agent.contactEmail;
                    row.Cells[5].Value = agentFeeList[i].agent.contactName;
                    row.Cells[6].Value = agentFeeList[i].agentFeeSeq;

                    int feeColIndex = 6;
                    int fixColCount = 7;

                    for (int j = 1; j <= 100 ; j++)
                    {
                       // FieldInfo feeNameField = agentFeeList[i].GetType().GetField("feeName" + j);
                        FieldInfo feeField = agentFeeList[i].GetType().GetField("fee" + j);

                      //  String feeNameFieldValue = feeNameField.GetValue(agentFeeList[i]) == null ? null : feeNameField.GetValue(agentFeeList[i]).ToString();
                        String feeFieldValue = feeField.GetValue(agentFeeList[i]) == null ? null : feeField.GetValue(agentFeeList[i]).ToString(); ;

                        if (dgAgentFee.Columns.Count >= fixColCount + j)
                        {
                            row.Cells[feeColIndex + j].Value = feeFieldValue;
                        }
                    }
                  

                    row.Cells[dgAgentFee.Columns.Count - 3].Value = agentFeeList[i].feeTotal;
                    row.Cells[dgAgentFee.Columns.Count - 2].Value = agentFeeList[i].invoiceFee;
                    row.Cells[dgAgentFee.Columns.Count - 1].Value = agentFeeList[i].preInvoiceFee;

                }
            }
            this.dgAgentType.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgentType.AutoResizeColumns();
            this.dgAgentTypeComment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgentTypeComment.AutoResizeColumns();
            this.dgAgent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgAgent.AutoResizeColumns();
            if (dgAgentFee.RowCount > 0)
            {
                this.btnMail.Visible = true;
            }
            else
            {
                this.btnMail.Visible = false;
            }

            this.Cursor = Cursors.Default;     
           

        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            if (dgAgentFee.RowCount <= 0)
            {
                return;
            }


            //StringBuilder sb = new StringBuilder();           
            //for (int i = 1; i <= 100; i++)
            //{
            //    sb.AppendLine("<#if contact.fee"+i.ToString()+"?has_content>");
            //    sb.AppendLine("<tr>");
            //    sb.AppendLine("<td nowrap=\"nowrap\" style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding: 0px;\" bgcolor=\"#ffffff\">");

            //    sb.AppendLine("${contact.fee_seq"+i.ToString()+"!}");
            //    sb.AppendLine("</td>");

            //    sb.AppendLine("<td nowrap=\"nowrap\" style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding: 0px;\" bgcolor=\"#ffffff\">");

            //    sb.AppendLine("${contact.fee_name" + i.ToString() + "!}");
            //    sb.AppendLine("</td>");

            //    sb.AppendLine("<td nowrap=\"nowrap\" style=\"font-size: 13px; color: black; font-weight: normal; text-align: right; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding: 0px;\" bgcolor=\"#ffffff\">");

            //    sb.AppendLine("${contact.fee" + i.ToString() + "!}");
            //    sb.AppendLine("</td>");
            //    sb.AppendLine("</tr>");
            //    sb.AppendLine("</#if>");

            //}

            //String s = sb.ToString();

            frmMailSend frmMailSend = new ChinaUnion_Agent.frmMailSend();
            frmMailSend.ShowIcon = false;
            frmMailSend.feeMonth = dtFeeMonth.Value.ToString("yyyy-MM");
            frmMailSend.dgvTemp = this.dgAgentFee;
            frmMailSend.ShowDialog();

           

            Cursor.Current = Cursors.Default;
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
            ExportData.exportGridData(this.dgAgentFee);
            this.Cursor = Cursors.Default;
        }
     

        
    }
}
