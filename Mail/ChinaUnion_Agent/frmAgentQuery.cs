using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            IList<Agent> agentList = agentDao.GetList();

            if (agentList != null && agentList.Count > 0)
            {
                dgAgent.Rows.Clear();
                dgAgent.Columns.Clear();

                dgAgent.Columns.Add("代理商编号", "代理商编号");
                dgAgent.Columns.Add("代理商名称", "代理商名称");
                dgAgent.Columns.Add("联系人邮箱", "联系人邮箱");
                dgAgent.Columns.Add("联系人姓名", "联系人姓名");
                dgAgent.Columns.Add("联系人电话", "联系人电话");
                dgAgent.Columns.Add("联系人微信账号", "联系人微信账号");


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


                }
            }
           // Queryworker.ReportProgress(2, "代理商渠道类型...\r\n");
            //代理商渠道类型
            AgentTypeDao agentTypeDao = new AgentTypeDao();
            IList<AgentType> agentTypeList = agentTypeDao.GetList();

            if (agentTypeList != null && agentTypeList.Count > 0)
            {
                dgAgentType.Rows.Clear();
                dgAgentType.Columns.Clear();
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
            IList<AgentTypeComment> agentTypeCommentList = agentTypeCommentDao.GetList();

            if (agentTypeCommentList != null && agentTypeCommentList.Count > 0)
            {
                dgAgentTypeComment.Rows.Clear();
                dgAgentTypeComment.Columns.Clear();

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


            if (agentFeeList != null && agentFeeList.Count > 0)
            {
                dgAgentFee.Rows.Clear();
                dgAgentFee.Columns.Clear();

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
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName1) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName1))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName1, agentFeeList[i].feeName1);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName2) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName2))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName2, agentFeeList[i].feeName2);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName3) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName3))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName3, agentFeeList[i].feeName3);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName4) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName4))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName4, agentFeeList[i].feeName4);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName5) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName5))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName5, agentFeeList[i].feeName5);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName6) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName6))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName6, agentFeeList[i].feeName6);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName7) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName7))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName7, agentFeeList[i].feeName7);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName8) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName8))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName8, agentFeeList[i].feeName8);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName9) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName9))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName9, agentFeeList[i].feeName9);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName10) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName10))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName10, agentFeeList[i].feeName10);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName11) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName11))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName11, agentFeeList[i].feeName11);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName12) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName12))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName12, agentFeeList[i].feeName12);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName13) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName13))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName13, agentFeeList[i].feeName13);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName14) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName14))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName14, agentFeeList[i].feeName14);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName15) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName15))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName15, agentFeeList[i].feeName15);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName16) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName16))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName16, agentFeeList[i].feeName16);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName17) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName17))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName17, agentFeeList[i].feeName17);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName18) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName18))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName18, agentFeeList[i].feeName18);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName19) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName19))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName19, agentFeeList[i].feeName19);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName20) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName20))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName20, agentFeeList[i].feeName20);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName21) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName21))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName21, agentFeeList[i].feeName21);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName22) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName22))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName22, agentFeeList[i].feeName22);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName23) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName23))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName23, agentFeeList[i].feeName23);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName24) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName24))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName24, agentFeeList[i].feeName24);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName25) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName25))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName25, agentFeeList[i].feeName25);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName26) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName26))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName26, agentFeeList[i].feeName26);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName27) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName27))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName27, agentFeeList[i].feeName27);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName28) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName28))
                        {
                            dgAgentFee.Columns.Add(agentFeeList[i].feeName28, agentFeeList[i].feeName28);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName29) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName29))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName29, agentFeeList[i].feeName29);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName30) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName30))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName30, agentFeeList[i].feeName30);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName31) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName31))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName31, agentFeeList[i].feeName31);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName32) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName32))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName32, agentFeeList[i].feeName32);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName33) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName33))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName33, agentFeeList[i].feeName33);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName34) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName34))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName34, agentFeeList[i].feeName34);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName35) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName35))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName35, agentFeeList[i].feeName35);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName36) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName36))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName36, agentFeeList[i].feeName36);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName37) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName37))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName37, agentFeeList[i].feeName37);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName38) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName38))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName38, agentFeeList[i].feeName38);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName39) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName39))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName39, agentFeeList[i].feeName39);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName40) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName40))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName40, agentFeeList[i].feeName40);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName41) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName41))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName41, agentFeeList[i].feeName41);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName42) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName42))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName42, agentFeeList[i].feeName42);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName43) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName43))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName43, agentFeeList[i].feeName43);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName44) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName44))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName44, agentFeeList[i].feeName44);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName45) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName45))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName45, agentFeeList[i].feeName45);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName46) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName46))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName46, agentFeeList[i].feeName46);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName47) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName47))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName47, agentFeeList[i].feeName47);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName48) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName48))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName48, agentFeeList[i].feeName48);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName49) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName49))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName49, agentFeeList[i].feeName49);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName50) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName50))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName50, agentFeeList[i].feeName50);
                        }

                        this.dgAgentFee.Columns.Add("总计", "总计");

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

                    if (dgAgentFee.Columns.Count >= fixColCount + 1) { row.Cells[feeColIndex + 1].Value = agentFeeList[i].fee1; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 2) { row.Cells[feeColIndex + 2].Value = agentFeeList[i].fee2; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 3) { row.Cells[feeColIndex + 3].Value = agentFeeList[i].fee3; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 4) { row.Cells[feeColIndex + 4].Value = agentFeeList[i].fee4; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 5) { row.Cells[feeColIndex + 5].Value = agentFeeList[i].fee5; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 6) { row.Cells[feeColIndex + 6].Value = agentFeeList[i].fee6; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 7) { row.Cells[feeColIndex + 7].Value = agentFeeList[i].fee7; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 8) { row.Cells[feeColIndex + 8].Value = agentFeeList[i].fee8; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 9) { row.Cells[feeColIndex + 9].Value = agentFeeList[i].fee9; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 10) { row.Cells[feeColIndex + 10].Value = agentFeeList[i].fee10; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 11) { row.Cells[feeColIndex + 11].Value = agentFeeList[i].fee11; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 12) { row.Cells[feeColIndex + 12].Value = agentFeeList[i].fee12; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 13) { row.Cells[feeColIndex + 13].Value = agentFeeList[i].fee13; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 14) { row.Cells[feeColIndex + 14].Value = agentFeeList[i].fee14; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 15) { row.Cells[feeColIndex + 15].Value = agentFeeList[i].fee15; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 16) { row.Cells[feeColIndex + 16].Value = agentFeeList[i].fee16; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 17) { row.Cells[feeColIndex + 17].Value = agentFeeList[i].fee17; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 18) { row.Cells[feeColIndex + 18].Value = agentFeeList[i].fee18; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 19) { row.Cells[feeColIndex + 19].Value = agentFeeList[i].fee19; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 20) { row.Cells[feeColIndex + 20].Value = agentFeeList[i].fee20; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 21) { row.Cells[feeColIndex + 21].Value = agentFeeList[i].fee21; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 22) { row.Cells[feeColIndex + 22].Value = agentFeeList[i].fee22; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 23) { row.Cells[feeColIndex + 23].Value = agentFeeList[i].fee23; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 24) { row.Cells[feeColIndex + 24].Value = agentFeeList[i].fee24; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 25) { row.Cells[feeColIndex + 25].Value = agentFeeList[i].fee25; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 26) { row.Cells[feeColIndex + 26].Value = agentFeeList[i].fee26; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 27) { row.Cells[feeColIndex + 27].Value = agentFeeList[i].fee27; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 28) { row.Cells[feeColIndex + 28].Value = agentFeeList[i].fee28; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 29) { row.Cells[feeColIndex + 29].Value = agentFeeList[i].fee29; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 30) { row.Cells[feeColIndex + 30].Value = agentFeeList[i].fee30; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 31) { row.Cells[feeColIndex + 31].Value = agentFeeList[i].fee31; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 32) { row.Cells[feeColIndex + 32].Value = agentFeeList[i].fee32; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 33) { row.Cells[feeColIndex + 33].Value = agentFeeList[i].fee33; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 34) { row.Cells[feeColIndex + 34].Value = agentFeeList[i].fee34; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 35) { row.Cells[feeColIndex + 35].Value = agentFeeList[i].fee35; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 36) { row.Cells[feeColIndex + 36].Value = agentFeeList[i].fee36; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 37) { row.Cells[feeColIndex + 37].Value = agentFeeList[i].fee37; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 38) { row.Cells[feeColIndex + 38].Value = agentFeeList[i].fee38; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 39) { row.Cells[feeColIndex + 39].Value = agentFeeList[i].fee39; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 40) { row.Cells[feeColIndex + 40].Value = agentFeeList[i].fee40; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 41) { row.Cells[feeColIndex + 41].Value = agentFeeList[i].fee41; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 42) { row.Cells[feeColIndex + 42].Value = agentFeeList[i].fee42; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 43) { row.Cells[feeColIndex + 43].Value = agentFeeList[i].fee43; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 44) { row.Cells[feeColIndex + 44].Value = agentFeeList[i].fee44; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 45) { row.Cells[feeColIndex + 45].Value = agentFeeList[i].fee45; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 46) { row.Cells[feeColIndex + 46].Value = agentFeeList[i].fee46; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 47) { row.Cells[feeColIndex + 47].Value = agentFeeList[i].fee47; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 48) { row.Cells[feeColIndex + 48].Value = agentFeeList[i].fee48; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 49) { row.Cells[feeColIndex + 49].Value = agentFeeList[i].fee49; }
                    if (dgAgentFee.Columns.Count >= fixColCount + 50) { row.Cells[feeColIndex + 50].Value = agentFeeList[i].fee50; }

                    row.Cells[dgAgentFee.Columns.Count - 1].Value = agentFeeList[i].feeTotal;

                }
            }
            if (dgAgentFee.RowCount > 0)
            {
                this.btnMail.Visible = true;
            }

            this.Cursor = Cursors.Default;     
           

        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            if (dgAgentFee.RowCount <= 0)
            {
                return;
            }
         


            //StringBuilder sb1 = new StringBuilder();

            //for (int i = 16; i <= 50; i++)
            //{
            //    sb1.Append("<#if contact.fee").Append(i.ToString()).Append("?has_content>");
            //    sb1.Append("<#assign seq=seq+1 />");
            //    sb1.Append("<tr>");
            //    sb1.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            //    sb1.Append("${seq!}");
            //    sb1.Append("</td>");
            //    sb1.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            //    sb1.Append("${contact.fee_name").Append(i.ToString()).Append("!}");
            //    sb1.Append("</td>");
            //    sb1.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            //    sb1.Append("${contact.fee").Append(i.ToString()).Append("!}");
            //    sb1.Append("</td>");

            //    sb1.Append("</tr>");
            //    sb1.Append("</#if>");
            //}

           



       




            frmMailSend frmMailSend = new ChinaUnion_Agent.frmMailSend();
            frmMailSend.ShowIcon = false;
            
            frmMailSend.dgvTemp = this.dgAgentFee;
            frmMailSend.ShowDialog();

           

            Cursor.Current = Cursors.Default;
        }

     
      
        private void frmAgentQuery_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;    
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     

        
    }
}
