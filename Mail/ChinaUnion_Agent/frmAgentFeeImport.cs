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
using System.Reflection;


namespace ChinaUnion_Agent
{
    public partial class frmAgentFeeImport : Form
    {
        public frmAgentFeeImport()
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

                List<string> sheetNames=  execelfile.GetWorksheetNames().ToList();
                if (!sheetNames.Contains("明细表"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:明细表的sheet.");
                    return;
                }
                if (!sheetNames.Contains("代理商相关信息"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:代理商相关信息的sheet.");
                    return;
                }
                if (!sheetNames.Contains("代理商渠道类型"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:代理商渠道类型的sheet.");
                    return;
                }
                if (!sheetNames.Contains("说明格式"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:说明格式的sheet.");
                    return;
                }

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

            ImportLog importLog = new ChinaUnion_BO.ImportLog();
            importLog.type = "Agent";
            importLog.import_month = this.dtFeeMonth.Value.ToString("yyyy-MM");
            ImportLogDao importLogDao = new ChinaUnion_DataAccess.ImportLogDao();
            if (importLogDao.Get(importLog) != null)
            {
                if (MessageBox.Show("本月佣金已经导入，是否需要再次导入？", "数据导入", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
            }
            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();
            this.btnImport.Enabled = false;
           

        }

        BackgroundWorker worker; 
        private void frmAgentFeeImport_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            this.dtFeeMonth.Value = DateTime.Now.AddMonths(-1);
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
                agentFee.agentFeeSeq = agentFee.agentNo + this.dtFeeMonth.Value.ToString("yyyyMM") + String.Format("{0:D5}", i+1);
                agentFee.feeTotal = dgAgentFee[dgAgentFee.Columns.Count - 1, i].Value.ToString();

                for (int j = 2; j <= 100 && j < dgAgentFee.Columns.Count-1; j++)
                {

                    FieldInfo feeNameField = agentFee.GetType().GetField("feeName" + j);
                    FieldInfo feeField = agentFee.GetType().GetField("fee" + j);

                    String feeNameFieldValue = dgAgentFee.Columns[j].HeaderCell.Value.ToString();
                    String feeFieldValue = dgAgentFee[j, i].Value.ToString();
                    if (feeFieldValue.Trim().Equals("0") || String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        feeFieldValue = null;
                    }
                    feeNameField.SetValue(agentFee, feeNameFieldValue);
                    feeField.SetValue(agentFee, feeFieldValue);

                }

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
                agent.status = dgAgent[6, i].Value.ToString();
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
                agentType.agentFeeMonth = this.dtFeeMonth.Value.ToString("yyyy-MM");
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
                agentTypeComment.agentFeeMonth = this.dtFeeMonth.Value.ToString("yyyy-MM");
                agentTypeCommentDao.Delete(agentTypeComment);
                agentTypeCommentDao.Add(agentTypeComment);
            }

            ImportLog importLog = new ChinaUnion_BO.ImportLog();
            importLog.type="Agent";
            importLog.import_month = this.dtFeeMonth.Value.ToString("yyyy-MM");
            ImportLogDao importLogDao = new ChinaUnion_DataAccess.ImportLogDao();
            importLogDao.Delete(importLog);
            importLogDao.Add(importLog);
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

        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel格式|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "ImportCommissio_Template.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportCommissio_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName,true);
            }
        }


    }
}
