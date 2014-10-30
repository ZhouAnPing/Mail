using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChinaUnion_Agent
{
    public partial class frmErrorCodeImport : Form
    {
        public frmErrorCodeImport()
        {
            InitializeComponent();
        }
        private void dgErrorCode_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgErrorCode.Columns[e.ColumnIndex].Name.Equals("Image"))
            {
                // Ensure that the value is a string.
                String stringValue = e.Value as string;
                if (stringValue == null) return;

                // Set the cell ToolTip to the text value.
                DataGridViewCell cell = dgErrorCode[e.ColumnIndex, e.RowIndex];
                cell.ToolTipText = stringValue;

                string path = e.Value.ToString();
                e.Value = GetImage(path);
            }
        }
        public System.Drawing.Image GetImage(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);

            fs.Close();

            return result;

        } 
        private void frmErrorCode_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            
           
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

                this.dgErrorCode.Rows.Clear();
                dgErrorCode.Columns.Clear();

                dgErrorCode.Columns.Add("序号", "序号");
                dgErrorCode.Columns.Add("关键字", "关键字");
                dgErrorCode.Columns.Add("问题描述", "问题描述");
                DataGridViewImageColumn column = new DataGridViewImageColumn();
                column.HeaderText = "出错截图信息";
                column.Name = "Image";
                //column.Image =System.Drawing.Image.FromFile( "./TestError.png");
                dgErrorCode.Columns.Add(column);
                //dgErrorCode.Columns.Add("出错截图信息", "出错截图信息");
                dgErrorCode.Columns.Add("处理方法", "处理方法");
                dgErrorCode.Columns.Add("联系人员", "联系人员");
                dgErrorCode.Columns.Add("备注", "备注");

                for (int i = 0; i < 100; i++)
                {
                    dgErrorCode.Rows.Add();
                    DataGridViewRow row = dgErrorCode.Rows[i];
                    row.Cells[0].Value = (i + 1).ToString();
                    row.Cells[1].Value = "关键字";
                    row.Cells[2].Value = "系统出错";
                    row.Cells[3].Value = "./TestError.png";
                    row.Cells[4].Value = "对不起，你所在的渠道没有活动权限";
                    row.Cells[5].Value = "联通客服";
                    row.Cells[6].Value = "建议通过微信企业号联系";


                }


                this.dgErrorCode.AutoResizeColumns();
                this.dgErrorCode.AutoResizeRows();

                StringBuilder sb = new StringBuilder();
                HashSet<String> ErrorCodeSet = new HashSet<string>();
                foreach (DataGridViewRow v in this.dgErrorCode.Rows)
                {
                    if (v.Cells[1].Value != null)
                    {

                        foreach (DataGridViewRow v2 in dgErrorCode.Rows)
                        {
                            if (v.Index == v2.Index)
                            {
                                continue;
                            }
                            if (v2.Cells[1].Value != null)
                            {
                                if (v.Cells[1].Value.ToString().Equals(v2.Cells[1].Value.ToString()))
                                {
                                    if (!ErrorCodeSet.Contains<String>(v2.Cells[1].Value.ToString()))
                                    {
                                        ErrorCodeSet.Add(v2.Cells[1].Value.ToString() );
                                        sb.AppendFormat("{0}", v.Cells[1].Value.ToString()).AppendLine();
                                    }
                                    break;
                                }
                            }
                        }
                    }

                }
                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    this.btnImport.Enabled = false;
                    MessageBox.Show("以下记录关键字重复:\n"+sb.ToString());
                }
                else
                {
                    this.btnImport.Enabled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        BackgroundWorker worker; 
         /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码


            worker.ReportProgress(1, "开始导入错误代码...\r\n");


            //导入代理商类型说明
            AgentErrorCodeDao agentErrorCodeDao = new AgentErrorCodeDao();

            for (int i = 0; i < this.dgErrorCode.RowCount; i++)
            {
                AgentErrorCode agentErrorCode = new AgentErrorCode();

                agentErrorCode.keyword = dgErrorCode[1, i].Value.ToString();
                agentErrorCode.errorDesc = dgErrorCode[2, i].Value.ToString();
               

                byte[] b = new byte[0];
                String fullpath = dgErrorCode[3, i].Value.ToString();

                FileStream fs = new FileStream(fullpath, FileMode.Open);
                byte[] imagebytes = new byte[fs.Length];
                BinaryReader br = new BinaryReader(fs);
                imagebytes = br.ReadBytes(Convert.ToInt32(fs.Length));
                fs.Close();
                br.Close();

                if (imagebytes.Length > 0)
                {
                    agentErrorCode.errorImg = imagebytes;                    
                }
               
               
                agentErrorCode.solution = dgErrorCode[4, i].Value.ToString();
                agentErrorCode.contactName = dgErrorCode[5, i].Value.ToString();
                agentErrorCode.comment = dgErrorCode[5, i].Value.ToString();

                agentErrorCodeDao.Delete(agentErrorCode.keyword);
                agentErrorCodeDao.Add(agentErrorCode);
            }

            worker.ReportProgress(2, "导入错误代码完成...\r\n");


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
    }
}
