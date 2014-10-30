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
using System.Collections;
using LinqToExcel;
using System.Threading;

using System.Configuration;
using System.IO;


namespace ChinaUnion_Agent
{
    public partial class frmAgentImport : Form
    {
        public frmAgentImport()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            // openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|Excel 2000-2003(*.xls)|*.xls|CSV(*.csv)|*.csv|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string FileName = openFileDialog.FileName;

                var execelfile = new ExcelQueryFactory(FileName);

                this.txtAgent.Text = FileName;
                this.txtAgent.Enabled = false;

                //代理商佣金明细
                List<Row> agentFee = execelfile.Worksheet("明细表").ToList(); ;

                if (agentFee != null && agentFee.Count > 0)
                {
                    this.btnImport.Enabled = true;
                    dgAgentFee.Rows.Clear();
                    dgAgentFee.Columns.Clear();
                    foreach (String coloumn in agentFee[0].ColumnNames)
                    {
                        this.dgAgentFee.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentFee.Count; i++)
                    {
                        dgAgentFee.Rows.Add();
                        DataGridViewRow row = dgAgentFee.Rows[i];
                        foreach (String coloumn in agentFee[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentFee[i][coloumn];
                        }

                    }
                }

                //代理商信息
                List<Row> agent = execelfile.Worksheet("代理商相关信息").ToList(); ;

                if (agent != null && agent.Count > 0)
                {
                    this.btnImport.Enabled = true;
                    dgAgent.Rows.Clear();
                    dgAgent.Columns.Clear();
                    foreach (String coloumn in agent[0].ColumnNames)
                    {
                        this.dgAgent.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agent.Count; i++)
                    {
                        dgAgent.Rows.Add();
                        DataGridViewRow row = dgAgent.Rows[i];
                        foreach (String coloumn in agent[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agent[i][coloumn];
                        }

                    }
                }

                //代理商渠道类型
                List<Row> agentType = execelfile.Worksheet("代理商渠道类型").ToList(); ;

                if (agentType != null && agentType.Count > 0)
                {
                    this.btnImport.Enabled = true;
                    dgAgentType.Rows.Clear();
                    dgAgentType.Columns.Clear();
                    foreach (String coloumn in agentType[0].ColumnNames)
                    {
                        this.dgAgentType.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentType.Count; i++)
                    {
                        dgAgentType.Rows.Add();
                        DataGridViewRow row = dgAgentType.Rows[i];
                        foreach (String coloumn in agentType[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentType[i][coloumn];
                        }

                    }
                }


                //代理商渠道类型说明
                List<Row> agentTypeComment = execelfile.Worksheet("说明格式").ToList(); ;

                if (agentTypeComment != null && agentTypeComment.Count > 0)
                {
                    this.btnImport.Enabled = true;
                    dgAgentTypeComment.Rows.Clear();
                    dgAgentTypeComment.Columns.Clear();
                    foreach (String coloumn in agentTypeComment[0].ColumnNames)
                    {
                        this.dgAgentTypeComment.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < agentTypeComment.Count; i++)
                    {
                        dgAgentTypeComment.Rows.Add();
                        DataGridViewRow row = dgAgentTypeComment.Rows[i];
                        foreach (String coloumn in agentTypeComment[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentTypeComment[i][coloumn];
                        }

                    }
                }


                //检查重复记录
                StringBuilder sbDuplicated = new StringBuilder();
                HashSet<String> agentFeeSet = new HashSet<string>();

                StringBuilder sb = new StringBuilder();               
                foreach (DataGridViewRow v in this.dgAgentFee.Rows)
                {
                   
                    if (!String.IsNullOrEmpty(v.Cells[0].Value.ToString()))
                    {
                        
                        foreach (DataGridViewRow v2 in dgAgentFee.Rows)
                        {
                            if (v.Index == v2.Index)
                            {
                                continue;
                            }

                            if (!String.IsNullOrEmpty(v2.Cells[0].Value.ToString()) )
                            {
                                
                                if (v.Cells[0].Value.ToString().Equals(v2.Cells[0].Value.ToString()))
                                {
                                    if (!agentFeeSet.Contains<String>(v.Cells[0].Value.ToString()))
                                    {
                                        agentFeeSet.Add(v.Cells[0].Value.ToString());
                                        sb.AppendFormat("代理商:{0}", v.Cells[0].Value.ToString()).AppendLine();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    sbDuplicated.AppendLine("代理商佣金存在以下重复记录:");
                    sbDuplicated.AppendLine(sb.ToString());
                }

                //
                sb = new StringBuilder();
                HashSet<String> agentSet = new HashSet<string>();
                foreach (DataGridViewRow v in this.dgAgent.Rows)
                {
                    if (!String.IsNullOrEmpty(v.Cells[0].Value.ToString()) && !String.IsNullOrEmpty(v.Cells[1].Value.ToString()))
                    {
                        

                        foreach (DataGridViewRow v2 in dgAgent.Rows)
                        {
                            if (v.Index == v2.Index)
                            {
                                continue;
                            }
                            if (!String.IsNullOrEmpty(v2.Cells[0].Value.ToString()) && !String.IsNullOrEmpty(v2.Cells[1].Value.ToString()))
                            {

                                if (v.Cells[0].Value.ToString().Equals(v2.Cells[0].Value.ToString()) && v.Cells[1].Value.ToString().Equals(v2.Cells[1].Value.ToString()))
                                {
                                    if (!agentSet.Contains<String>(v2.Cells[0].Value.ToString() + v2.Cells[0].Value.ToString()))
                                    {
                                        agentSet.Add(v2.Cells[0].Value.ToString() + v2.Cells[0].Value.ToString());
                                        sb.AppendFormat("代理商:{0}-{1}", v.Cells[0].Value.ToString(), v.Cells[1].Value.ToString()).AppendLine();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    sbDuplicated.AppendLine("\n代理商存在以下重复记录:");
                    sbDuplicated.AppendLine(sb.ToString());
                }

                //
                sb = new StringBuilder();
                HashSet<String> agentFeeTypeSet = new HashSet<string>();
                foreach (DataGridViewRow v in this.dgAgentType.Rows)
                {
                    if (!String.IsNullOrEmpty(v.Cells[0].Value.ToString()) && !String.IsNullOrEmpty(v.Cells[1].Value.ToString()))
                    {

                       
                        foreach (DataGridViewRow v2 in this.dgAgentType.Rows)
                        {
                            if (v.Index == v2.Index)
                            {
                                continue;
                            }
                            if (!String.IsNullOrEmpty(v2.Cells[0].Value.ToString()) && !String.IsNullOrEmpty(v2.Cells[1].Value.ToString()))
                            {

                                if (v.Cells[0].Value.ToString().Equals(v2.Cells[0].Value.ToString()) && v.Cells[1].Value.ToString().Equals(v2.Cells[1].Value.ToString()))
                                {
                                    if (!agentFeeTypeSet.Contains<String>(v2.Cells[0].Value.ToString() + v2.Cells[0].Value.ToString()))
                                    {
                                        agentFeeTypeSet.Add(v2.Cells[0].Value.ToString() + v2.Cells[0].Value.ToString());
                                        sb.AppendFormat("渠道类型:{0}-{1}", v.Cells[0].Value.ToString(), v.Cells[1].Value.ToString()).AppendLine();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    sbDuplicated.AppendLine("\n代理商渠道类型存在以下重复记录:");
                    sbDuplicated.AppendLine(sb.ToString());
                }


                //
                sb = new StringBuilder();
                HashSet<String> agentFeeTypeCommentSet = new HashSet<string>();
                foreach (DataGridViewRow v in this.dgAgentTypeComment.Rows)
                {
                    if (!String.IsNullOrEmpty(v.Cells[0].Value.ToString()) && !String.IsNullOrEmpty(v.Cells[1].Value.ToString()))
                    {
                        
                        foreach (DataGridViewRow v2 in dgAgentTypeComment.Rows)
                        {
                            if (v.Index == v2.Index)
                            {
                                continue;
                            }
                            if (!String.IsNullOrEmpty(v2.Cells[0].Value.ToString()) && !String.IsNullOrEmpty(v2.Cells[1].Value.ToString()))
                            {
                                if (agentFeeTypeCommentSet.Contains<String>(v2.Cells[0].Value.ToString() + v2.Cells[1].Value.ToString()))
                                {
                                    continue;
                                }
                                if (v.Cells[0].Value.ToString().Equals(v2.Cells[0].Value.ToString()) && v.Cells[1].Value.ToString().Equals(v2.Cells[1].Value.ToString()))
                                {
                                    if (!agentFeeTypeCommentSet.Contains<String>(v2.Cells[0].Value.ToString() + v2.Cells[0].Value.ToString()))
                                    {
                                        agentFeeTypeCommentSet.Add(v2.Cells[0].Value.ToString() + v2.Cells[0].Value.ToString());

                                        sb.AppendFormat("渠道类型说明:{0}-{1}", v.Cells[0].Value.ToString(), v.Cells[1].Value.ToString()).AppendLine();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    sbDuplicated.AppendLine("\n代理商渠道类型说明存在以下重复记录:");
                    sbDuplicated.AppendLine(sb.ToString());
                }


                if (!String.IsNullOrEmpty(sbDuplicated.ToString()))
                {
                    this.btnImport.Enabled = false;
                    MessageBox.Show(sbDuplicated.ToString());
                }
                else
                {
                    this.btnImport.Enabled = true;
                }



                Cursor.Current = Cursors.Default;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();
            this.btnImport.Enabled = false;
           

        }

        BackgroundWorker worker; 
        private void frmAgentImport_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码
           

            worker.ReportProgress(1, "开始导入代理商佣金...\r\n");

            //导入代理商佣金
            AgentFeeDao agentFeeDao = new AgentFeeDao();
            for (int i = 0; i < dgAgentFee.RowCount; i++)
            {
                AgentFee agentFee = new AgentFee();
                agentFee.agentFeeMonth = this.dtFeeMonth.Value.ToString("yyyy-MM");
                agentFee.agentNo = dgAgentFee[0, i].Value.ToString();
                agentFee.agentFeeSeq = dgAgentFee[1, i].Value.ToString();
                if (dgAgentFee.Columns.Count > 3)
                {
                    agentFee.feeName1 = dgAgentFee.Columns[2].HeaderCell.Value.ToString();
                    agentFee.fee1 = dgAgentFee[2, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 4)
                {
                    agentFee.feeName2 = dgAgentFee.Columns[3].HeaderCell.Value.ToString();
                    agentFee.fee2 = dgAgentFee[3, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 5)
                {
                    agentFee.feeName3 = dgAgentFee.Columns[4].HeaderCell.Value.ToString();
                    agentFee.fee3 = dgAgentFee[4, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 6)
                {
                    agentFee.feeName4 = dgAgentFee.Columns[5].HeaderCell.Value.ToString();
                    agentFee.fee4 = dgAgentFee[5, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 7)
                {
                    agentFee.feeName5 = dgAgentFee.Columns[6].HeaderCell.Value.ToString();
                    agentFee.fee5 = dgAgentFee[6, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 8)
                {
                    agentFee.feeName6 = dgAgentFee.Columns[7].HeaderCell.Value.ToString();
                    agentFee.fee6 = dgAgentFee[7, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 9)
                {
                    agentFee.feeName7 = dgAgentFee.Columns[8].HeaderCell.Value.ToString();
                    agentFee.fee7 = dgAgentFee[8, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 10)
                {
                    agentFee.feeName8 = dgAgentFee.Columns[9].HeaderCell.Value.ToString();
                    agentFee.fee8 = dgAgentFee[9, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 11)
                {
                    agentFee.feeName9 = dgAgentFee.Columns[10].HeaderCell.Value.ToString();
                    agentFee.fee9 = dgAgentFee[10, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 12)
                {
                    agentFee.feeName10 = dgAgentFee.Columns[11].HeaderCell.Value.ToString();
                    agentFee.fee10 = dgAgentFee[11, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 13)
                {
                    agentFee.feeName11 = dgAgentFee.Columns[12].HeaderCell.Value.ToString();
                    agentFee.fee11 = dgAgentFee[12, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 14)
                {
                    agentFee.feeName12 = dgAgentFee.Columns[13].HeaderCell.Value.ToString();
                    agentFee.fee12 = dgAgentFee[13, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 15)
                {
                    agentFee.feeName13 = dgAgentFee.Columns[14].HeaderCell.Value.ToString();
                    agentFee.fee13 = dgAgentFee[14, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 16)
                {
                    agentFee.feeName14 = dgAgentFee.Columns[15].HeaderCell.Value.ToString();
                    agentFee.fee14 = dgAgentFee[15, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 17)
                {
                    agentFee.feeName15 = dgAgentFee.Columns[16].HeaderCell.Value.ToString();
                    agentFee.fee15 = dgAgentFee[16, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 18)
                {
                    agentFee.feeName16 = dgAgentFee.Columns[17].HeaderCell.Value.ToString();
                    agentFee.fee16 = dgAgentFee[17, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 19)
                {
                    agentFee.feeName17 = dgAgentFee.Columns[18].HeaderCell.Value.ToString();
                    agentFee.fee17 = dgAgentFee[18, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 20)
                {
                    agentFee.feeName18 = dgAgentFee.Columns[19].HeaderCell.Value.ToString();
                    agentFee.fee18 = dgAgentFee[19, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 21)
                {
                    agentFee.feeName19 = dgAgentFee.Columns[20].HeaderCell.Value.ToString();
                    agentFee.fee19 = dgAgentFee[20, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 22)
                {
                    agentFee.feeName20 = dgAgentFee.Columns[21].HeaderCell.Value.ToString();
                    agentFee.fee20 = dgAgentFee[21, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 23)
                {
                    agentFee.feeName21 = dgAgentFee.Columns[22].HeaderCell.Value.ToString();
                    agentFee.fee21 = dgAgentFee[22, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 24)
                {
                    agentFee.feeName22 = dgAgentFee.Columns[23].HeaderCell.Value.ToString();
                    agentFee.fee22 = dgAgentFee[23, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 25)
                {
                    agentFee.feeName23 = dgAgentFee.Columns[24].HeaderCell.Value.ToString();
                    agentFee.fee23 = dgAgentFee[24, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 26)
                {
                    agentFee.feeName24 = dgAgentFee.Columns[25].HeaderCell.Value.ToString();
                    agentFee.fee24 = dgAgentFee[25, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 27)
                {
                    agentFee.feeName25 = dgAgentFee.Columns[26].HeaderCell.Value.ToString();
                    agentFee.fee25 = dgAgentFee[26, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 28)
                {
                    agentFee.feeName26 = dgAgentFee.Columns[27].HeaderCell.Value.ToString();
                    agentFee.fee26 = dgAgentFee[27, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 29)
                {
                    agentFee.feeName27 = dgAgentFee.Columns[28].HeaderCell.Value.ToString();
                    agentFee.fee27 = dgAgentFee[28, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 30)
                {
                    agentFee.feeName28 = dgAgentFee.Columns[29].HeaderCell.Value.ToString();
                    agentFee.fee28 = dgAgentFee[29, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 31)
                {
                    agentFee.feeName29 = dgAgentFee.Columns[30].HeaderCell.Value.ToString();
                    agentFee.fee29 = dgAgentFee[30, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 32)
                {
                    agentFee.feeName30 = dgAgentFee.Columns[31].HeaderCell.Value.ToString();
                    agentFee.fee30 = dgAgentFee[31, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 33)
                {
                    agentFee.feeName31 = dgAgentFee.Columns[32].HeaderCell.Value.ToString();
                    agentFee.fee31 = dgAgentFee[32, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 34)
                {
                    agentFee.feeName32 = dgAgentFee.Columns[33].HeaderCell.Value.ToString();
                    agentFee.fee32 = dgAgentFee[33, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 35)
                {
                    agentFee.feeName33 = dgAgentFee.Columns[34].HeaderCell.Value.ToString();
                    agentFee.fee33 = dgAgentFee[34, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 36)
                {
                    agentFee.feeName34 = dgAgentFee.Columns[35].HeaderCell.Value.ToString();
                    agentFee.fee34 = dgAgentFee[35, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 37)
                {
                    agentFee.feeName35 = dgAgentFee.Columns[36].HeaderCell.Value.ToString();
                    agentFee.fee35 = dgAgentFee[36, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 38)
                {
                    agentFee.feeName36 = dgAgentFee.Columns[37].HeaderCell.Value.ToString();
                    agentFee.fee36 = dgAgentFee[37, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 39)
                {
                    agentFee.feeName37 = dgAgentFee.Columns[38].HeaderCell.Value.ToString();
                    agentFee.fee37 = dgAgentFee[38, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 40)
                {
                    agentFee.feeName38 = dgAgentFee.Columns[39].HeaderCell.Value.ToString();
                    agentFee.fee38 = dgAgentFee[39, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 41)
                {
                    agentFee.feeName39 = dgAgentFee.Columns[40].HeaderCell.Value.ToString();
                    agentFee.fee39 = dgAgentFee[40, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 42)
                {
                    agentFee.feeName40 = dgAgentFee.Columns[41].HeaderCell.Value.ToString();
                    agentFee.fee40 = dgAgentFee[41, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 43)
                {
                    agentFee.feeName41 = dgAgentFee.Columns[42].HeaderCell.Value.ToString();
                    agentFee.fee41 = dgAgentFee[42, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 44)
                {
                    agentFee.feeName42 = dgAgentFee.Columns[43].HeaderCell.Value.ToString();
                    agentFee.fee42 = dgAgentFee[43, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 45)
                {
                    agentFee.feeName43 = dgAgentFee.Columns[44].HeaderCell.Value.ToString();
                    agentFee.fee43 = dgAgentFee[44, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 46)
                {
                    agentFee.feeName44 = dgAgentFee.Columns[45].HeaderCell.Value.ToString();
                    agentFee.fee44 = dgAgentFee[45, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 47)
                {
                    agentFee.feeName45 = dgAgentFee.Columns[46].HeaderCell.Value.ToString();
                    agentFee.fee45 = dgAgentFee[46, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 48)
                {
                    agentFee.feeName46 = dgAgentFee.Columns[47].HeaderCell.Value.ToString();
                    agentFee.fee46 = dgAgentFee[47, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 49)
                {
                    agentFee.feeName47 = dgAgentFee.Columns[48].HeaderCell.Value.ToString();
                    agentFee.fee47 = dgAgentFee[48, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 50)
                {
                    agentFee.feeName48 = dgAgentFee.Columns[49].HeaderCell.Value.ToString();
                    agentFee.fee48 = dgAgentFee[49, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 51)
                {
                    agentFee.feeName49 = dgAgentFee.Columns[50].HeaderCell.Value.ToString();
                    agentFee.fee49 = dgAgentFee[50, i].Value.ToString();
                }
                if (dgAgentFee.Columns.Count > 52)
                {
                    agentFee.feeName50 = dgAgentFee.Columns[51].HeaderCell.Value.ToString();
                    agentFee.fee50 = dgAgentFee[51, i].Value.ToString();
                }

                agentFee.feeTotal = dgAgentFee[dgAgentFee.Columns.Count - 1, i].Value.ToString();

                agentFeeDao.Delete(agentFee);
                agentFeeDao.Add(agentFee);

            }

            worker.ReportProgress(2, "导入代理商佣金完成...\r\n");
            worker.ReportProgress(3, "开始导入代理商...\r\n");
            //导入代理商
            AgentDao agentDao = new AgentDao();
            for (int i = 0; i < dgAgent.RowCount; i++)
            {
                Agent agent = new Agent();
                agent.agentNo = dgAgent[0, i].Value.ToString();
                agent.agentName = dgAgent[1, i].Value.ToString();
                agent.contactEmail = dgAgent[2, i].Value.ToString();
                agent.contactName = dgAgent[3, i].Value.ToString();
                agent.contactTel = dgAgent[4, i].Value.ToString();
                agent.contactWechatAccount = dgAgent[5, i].Value.ToString();
                agentDao.Delete(agent.agentNo);
                agentDao.Add(agent);

            }
            worker.ReportProgress(4, "导入代理商完成...\r\n");
            worker.ReportProgress(5, "开始导入代理商类型...\r\n");

            //导入代理商类型
            AgentTypeDao agentTypeDao = new AgentTypeDao();
            for (int i = 0; i < dgAgentType.RowCount; i++)
            {
                AgentType agentType = new AgentType();
                agentType.agentNo = dgAgentType[0, i].Value.ToString();
                agentType.agentType = dgAgentType[1, i].Value.ToString();

                agentTypeDao.Delete(agentType);
                agentTypeDao.Add(agentType);
            }

          
            worker.ReportProgress(6, "导入代理商类型完成...\r\n");
            worker.ReportProgress(7, "开始导入代理商类型说明...\r\n");


            //导入代理商类型说明
            AgentTypeCommentDao agentTypeCommentDao = new AgentTypeCommentDao();
            for (int i = 0; i < dgAgentTypeComment.RowCount; i++)
            {
                AgentTypeComment agentTypeComment = new AgentTypeComment();

                agentTypeComment.agentType = dgAgentTypeComment[0, i].Value.ToString();
                agentTypeComment.agentTypeComment = dgAgentTypeComment[1, i].Value.ToString();

                agentTypeCommentDao.Delete(agentTypeComment.agentType);
                agentTypeCommentDao.Add(agentTypeComment);
            }

            worker.ReportProgress(8, "导入代理商类型完成...\r\n");
           

            //MessageBox.Show("数据上传完毕");

        }


        /// <summary>
        /// 事件: 异步执行完成后 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("数据上传完毕。", "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
