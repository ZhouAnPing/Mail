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


namespace ChinaUnion_Agent.MasterDataForm
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

                List<string> sheetNames=  execelfile.GetWorksheetNames().ToList();
               
                if (!sheetNames.Contains("代理商相关信息"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:代理商相关信息的sheet.");
                    return;
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

                


                //检查重复记录
                StringBuilder sbDuplicated = new StringBuilder();
                HashSet<String> agentFeeSet = new HashSet<string>();

                StringBuilder sb = new StringBuilder();               
              

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

              


                if (!String.IsNullOrEmpty(sbDuplicated.ToString()))
                {
                    this.btnImport.Enabled = false;
                    MessageBox.Show(sbDuplicated.ToString());
                }
                else
                {
                    this.btnImport.Enabled = true;
                }

                dgAgent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgAgent.AutoResizeColumns();

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
            saveFileDialog.FileName = "上海联通佣金告知单模板.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                File.Copy("./Template/上海联通佣金告知单_模板.xlsx", saveFileDialog.FileName);
            }
        }


    }
}
