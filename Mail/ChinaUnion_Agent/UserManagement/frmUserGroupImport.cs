using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.UserManagement
{
    public partial class frmUserGroupImport : frmBase
    {
        public frmUserGroupImport()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
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

                List<string> sheetNames = execelfile.GetWorksheetNames().ToList();




                //代理商信息
                List<Row> agentList = execelfile.Worksheet(0).ToList(); ;

                if (agentList != null && agentList.Count > 0)
                {

                    this.btnImport.Enabled = true;
                    dgAgent.Rows.Clear();
                    dgAgent.Columns.Clear();
                    foreach (String coloumn in agentList[0].ColumnNames)
                    {
                        this.dgAgent.Columns.Add(coloumn, coloumn);
                    }

                   // this.dgAgent.Columns.Add("result", "导入结果");

                    for (int i = 0; i < agentList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(agentList[i][0].Value.ToString().Trim()))
                        {
                            continue;
                        }
                        dgAgent.Rows.Add();
                        DataGridViewRow row = dgAgent.Rows[i];
                        foreach (String coloumn in agentList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = agentList[i][coloumn];
                        }

                    }
                }




                dgAgent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgAgent.AutoResizeColumns();

                Cursor.Current = Cursors.Default;
            }
        }
        BackgroundWorker worker; 
        private void frmInvoiceImport_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtGroupName.Text.Trim()))
            {
                MessageBox.Show("请输入组名");
                txtGroupName.Focus();
                return;
            }

            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();
            this.btnImport.Enabled = false;
        }
        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码

            worker.ReportProgress(3, "开始导入信息...\r\n");
            //导入代理商
            GroupDao groupDao = new GroupDao();
            Group group = new Group();
            group.groupName = this.txtGroupName.Text.Trim();
            group.description = group.groupName;
            groupDao.Delete(group);
            groupDao.Add(group);


            UserDefinedGroupDao userDefinedGroupDao = new UserDefinedGroupDao();
            for (int i = 0; i < dgAgent.RowCount; i++)
            {
                UserDefinedGroup userDefinedGroup = new UserDefinedGroup();
                userDefinedGroup.groupName = group.groupName;
                userDefinedGroup.member = dgAgent[0, i].Value.ToString();
                userDefinedGroup.type = "代理商/渠道";
                userDefinedGroupDao.Add(userDefinedGroup);
                worker.ReportProgress(4, "导入" + userDefinedGroup.member + "信息完成...\r\n");
               


            }
            //dgInvoice.AutoResizeColumns();
            
            worker.ReportProgress(5, "导入信息完成...\r\n");



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

        private void btnDownload_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel格式|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "ImportUserDefinedGroup_Template.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportUserDefinedGroup_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName, true);
            }
        }
    }
}
