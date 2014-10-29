using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using ChinaUnion_Agent.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TripolisDialogueAdapter;
using ChinaUnion_Agent.Wechat;

namespace ChinaUnion_Agent
{
    public partial class frmMailReSend : Form
    {
        private String subject;
        public String feeMonth;
        public ArrayList htReSendMail = new ArrayList();
        public frmMailReSend()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            frmMailSubject frmMailSubject = new ChinaUnion_Agent.frmMailSubject();
            frmMailSubject.subject = "重新发送:" + Settings.Default.MailSubject + "(" + this.dtFeeMonth.Value.ToString("yyyy-MM") + ")";
            DialogResult dialogResult = frmMailSubject.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.subject = frmMailSubject.subject;
                //异步执行开始
                worker.RunWorkerAsync();
                frmProgress frm = new frmProgress(this.worker);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
                frm.Close();
            }

        }

        BackgroundWorker worker;
        private void frmMailReSend_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);


            AgentFeeDao agentFeeDao = new AgentFeeDao();
            IList<AgentFee> agentFeeList = agentFeeDao.GetList(this.feeMonth);

            if (agentFeeList != null && agentFeeList.Count > 0)
            {
                dgAgentFee.Rows.Clear();
                dgAgentFee.Columns.Clear();

                this.dgAgentFee.Columns.Add("代理商编号", "代理商编号");
                this.dgAgentFee.Columns.Add("代理商名称", "代理商名称");
                this.dgAgentFee.Columns.Add("代理商类型", "代理商类型");
                this.dgAgentFee.Columns.Add("代理商类型说明", "代理商类型说明");
                this.dgAgentFee.Columns.Add("联系人邮件", "联系人邮件");
                this.dgAgentFee.Columns.Add("联系人名称", "联系人名称");
                this.dgAgentFee.Columns.Add("告知单编号", "告知单编号");



                for (int i = 0; i < agentFeeList.Count; i++)
                {
                    if (i == 0)
                    {
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName1) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName1))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName1, agentFeeList[i].feeName1);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName2) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName2))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName2, agentFeeList[i].feeName2);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName3) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName3))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName3, agentFeeList[i].feeName3);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName4) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName4))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName4, agentFeeList[i].feeName4);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName5) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName5))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName5, agentFeeList[i].feeName5);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName6) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName6))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName6, agentFeeList[i].feeName6);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName7) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName7))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName7, agentFeeList[i].feeName7);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName8) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName8))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName8, agentFeeList[i].feeName8);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName9) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName9))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName9, agentFeeList[i].feeName9);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName10) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName10))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName10, agentFeeList[i].feeName10);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName11) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName11))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName11, agentFeeList[i].feeName11);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName12) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName12))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName12, agentFeeList[i].feeName12);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName13) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName13))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName13, agentFeeList[i].feeName13);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName14) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName14))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName14, agentFeeList[i].feeName14);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName15) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName15))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName15, agentFeeList[i].feeName15);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName16) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName16))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName16, agentFeeList[i].feeName16);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName17) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName17))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName17, agentFeeList[i].feeName17);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName18) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName18))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName18, agentFeeList[i].feeName18);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName19) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName19))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName19, agentFeeList[i].feeName19);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName20) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName20))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName20, agentFeeList[i].feeName20);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName21) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName21))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName21, agentFeeList[i].feeName21);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName22) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName22))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName22, agentFeeList[i].feeName22);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName23) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName23))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName23, agentFeeList[i].feeName23);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName24) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName24))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName24, agentFeeList[i].feeName24);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName25) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName25))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName25, agentFeeList[i].feeName25);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName26) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName26))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName26, agentFeeList[i].feeName26);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName27) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName27))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName27, agentFeeList[i].feeName27);
                        }
                        if (!String.IsNullOrEmpty(agentFeeList[i].feeName28) && !String.IsNullOrWhiteSpace(agentFeeList[i].feeName28))
                        {
                            this.dgAgentFee.Columns.Add(agentFeeList[i].feeName28, agentFeeList[i].feeName28);
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

                    if (!htReSendMail.Contains(agentFeeList[i].agent.agentName))
                    {
                        continue;
                    }
                    dgAgentFee.Rows.Add();
                    DataGridViewRow row = dgAgentFee.Rows[dgAgentFee.RowCount-1];

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
            showHtml(0);
          

            this.Cursor = Cursors.Default;
        }
               /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码
            Cursor.Current = Cursors.WaitCursor;
            if (dgAgentFee.RowCount <= 0)
            {
                return;
            }

            try{
             MailData mailData = new MailData();


            StringBuilder sbAgent = new StringBuilder();

            worker.ReportProgress(1, "准备数据...\r\n");

            for (int i = 0; i < dgAgentFee.RowCount; i++)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("agent_no#").Append(dgAgentFee[0, i].Value.ToString()).Append(",");
                sb.Append("agent_name#").Append(dgAgentFee[1, i].Value.ToString()).Append(",");
                sb.Append("agent_type#").Append(dgAgentFee[2, i].Value.ToString()).Append(",");
                sb.Append("agent_type_comment#").Append(dgAgentFee[3, i].Value.ToString()).Append(",");
                sb.Append("email#").Append(dgAgentFee[4, i].Value.ToString());



                mailData.ContactJsonList.Add(sb.ToString());
            }

            String client = Settings.Default.TripolisClient;
            String userName = Settings.Default.TripoisUserName;
            String password = Settings.Default.TripolisPassword;


            mailData.fromAddress = Settings.Default.MailFromAddress;
            mailData.replyAddress = Settings.Default.MailReplyAddress;
            mailData.sender = Settings.Default.MailSender;
            mailData.subject = this.subject;
            ChinaUnionAdapter mailAdapter = new ChinaUnionAdapter(client, userName, password, null);
            String databaseId = Settings.Default.TripolisDBId;
            String workspaceId = Settings.Default.TripolisWorkspaceId;
            String emailId = Settings.Default.TripolisDirectEmailId;
            worker.ReportProgress(1, "发送邮件...\r\n");
            String message = mailAdapter.sendBatchMail(databaseId, workspaceId, emailId, mailData, this.dtFeeMonth.Value);
            if (message.Contains("OK:"))
            {
                String mailJobId = message.Substring(3);

                MailJob mailJob = new MailJob();
                mailJob.feeMonth = this.dtFeeMonth.Value.ToString("yyyy-MM");
                mailJob.mailJobId = mailJobId;
                mailJob.subject = mailData.subject;

                mailJob.sendTime = this.dtFeeMonth.Value.ToString("yyyy-MM-dd hh:mm:ss");


                MailJobDao mailJobDao = new MailJobDao();
                mailJobDao.Delete(mailJob);
                mailJobDao.Add(mailJob);

                WechatAction wechatAction = new WechatAction();
                wechatAction.sendMessageToWechat(this.dtFeeMonth.Value.ToString("yyyy-MM") + Settings.Default.Wechat_Message);
                MessageBox.Show("邮件重新发送成功");

            }
            else
            {
                MessageBox.Show(message);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }
        /// <summary>
        /// 事件: 异步执行完成后 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // MessageBox.Show("处理完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showHtml(int rowIndex)
        {
            StringBuilder sbAgent = new StringBuilder();

            int seq = 1;





            for (int j = 7; j < dgAgentFee.ColumnCount - 1; j++)
            {



                if (!String.IsNullOrEmpty(dgAgentFee[j, rowIndex].Value.ToString()))
                {
                    sbAgent.Append("<tr>");
                    sbAgent.Append("<tr>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append(seq.ToString());
                    sbAgent.Append("</td>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append(dgAgentFee.Columns[j].HeaderCell.Value.ToString());
                    sbAgent.Append("</td>");
                    sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
                    sbAgent.Append(dgAgentFee[j, rowIndex].Value.ToString());
                    sbAgent.Append("</td>");

                    sbAgent.Append("</tr>");
                    seq++;
                }




            }




            sbAgent.Append("<tr>");
            sbAgent.Append("<tr>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(seq.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(dgAgentFee.Columns[dgAgentFee.Columns.Count - 1].HeaderCell.Value.ToString());
            sbAgent.Append("</td>");
            sbAgent.Append("<td nowrap style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding:0px\" bgcolor=\"#ffffff\">");
            sbAgent.Append(dgAgentFee[dgAgentFee.Columns.Count - 1, rowIndex].Value.ToString());
            sbAgent.Append("</td>");

            sbAgent.Append("</tr>");






            StringBuilder sb1 = new StringBuilder();
            sb1.Append(System.IO.File.ReadAllText("./html/head.html", Encoding.UTF8));

            sb1.Append(sbAgent.ToString());
            sb1.Append(System.IO.File.ReadAllText("./html/footer.html", Encoding.UTF8));


            sb1.Replace("${contact.agent_fee_seq!}", dgAgentFee[6, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_no!}", dgAgentFee[0, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_name!}", dgAgentFee[1, rowIndex].Value.ToString());
            sb1.Replace("${contact.agent_type!}", dgAgentFee[2, rowIndex].Value.ToString());

            sb1.Replace("${contact.agent_type_comment!}", dgAgentFee[3, rowIndex].Value.ToString());

            sb1.Replace("${currentdate.date?string(\"yyyy年 M月 d日\")}", DateTime.Now.ToString("yyyy年MM月dd日"));

            webBrowser1.DocumentText = sb1.ToString();
            webBrowser1.Document.Encoding = "gb2312";

            webBrowser1.Show();
        }
        int currentRowIndex = 0;
        private void dgAgentFee_SelectionChanged(object sender, EventArgs e)
        {
            if (dgAgentFee.CurrentRow != null & dgAgentFee.CurrentRow.Index != currentRowIndex)
            {
                currentRowIndex = dgAgentFee.CurrentRow.Index;
                showHtml(currentRowIndex);
            }
        }

    }
}
