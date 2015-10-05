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
            System.Drawing.Image result = null;
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
                result = System.Drawing.Image.FromStream(fs);

                fs.Close();
            }
            catch (Exception)
            {
            }

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

              // String path= Path.GetDirectoryName(FileName);

                var execelfile = new ExcelQueryFactory(FileName);
                this.txtErrorCode.Text = FileName;
                this.txtErrorCode.Enabled = false;


                List<string> sheetNames = execelfile.GetWorksheetNames().ToList();
                if (!sheetNames.Contains("ESS"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:ESS的sheet.");
                    return;
                }
                if (!sheetNames.Contains("CBSS"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:CBSS的sheet.");
                    return;
                }

                dgErrorCode.Rows.Clear();
                dgErrorCode.Columns.Clear();

                this.dgErrorCode.Columns.Add("序号", "序号");
                this.dgErrorCode.Columns.Add("子系统", "子系统");
                this.dgErrorCode.Columns.Add("报错关键字", "报错关键字");
                DataGridViewImageColumn column = new DataGridViewImageColumn();
                column.HeaderText = "错误图片";
                column.Name = "Image";
               
                this.dgErrorCode.Columns.Add(column);
                this.dgErrorCode.Columns.Add("报错描述", "报错描述");
                this.dgErrorCode.Columns.Add("原因及处理方法", "原因及处理方法");
                this.dgErrorCode.Columns.Add("联系方式", "联系方式");
               
               

                List<Row> ESSList = execelfile.Worksheet("ESS").ToList();
                //int count = 0;
                if (ESSList != null && ESSList.Count > 0)
                {
                     //count = ESSList.Count;
                    this.btnImport.Enabled = true;   

                    for (int i = 0; i < ESSList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(ESSList[i][0]) || String.IsNullOrWhiteSpace(ESSList[i][0]))
                        {
                            break;
                        }
                        dgErrorCode.Rows.Add();
                        DataGridViewRow row = dgErrorCode.Rows[dgErrorCode.RowCount-1];

                        row.Cells["序号"].Value = dgErrorCode.RowCount ;
                        row.Cells["子系统"].Value = "ESS";
                        row.Cells["报错关键字"].Value = ESSList[i][1].ToString().Trim(); ;
                        row.Cells["Image"].Value = Path.GetDirectoryName(FileName)+"\\ESS\\" + ESSList[i][0].ToString().Trim() + ".jpg";
                        row.Cells["报错描述"].Value = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(ESSList[i][2].ToString().Trim(), " ");
                        row.Cells["原因及处理方法"].Value = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(ESSList[i][3].ToString().Trim(), " ");
                        row.Cells["联系方式"].Value = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(ESSList[i][4].ToString().Trim(), " ");                       

                    }
                }

               
                List<Row>  CBSSList = execelfile.Worksheet("CBSS").ToList();

                if (CBSSList != null && CBSSList.Count > 0)
                {                  
                  

                    for (int i = 0; i < CBSSList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(CBSSList[i][0]) || String.IsNullOrWhiteSpace(CBSSList[i][0]))
                        {
                            break;
                        }
                        dgErrorCode.Rows.Add();
                        DataGridViewRow row = dgErrorCode.Rows[dgErrorCode.RowCount - 1];

                        row.Cells["序号"].Value = dgErrorCode.RowCount;
                        row.Cells["子系统"].Value = "CBSS";
                        row.Cells["报错关键字"].Value = CBSSList[i][1].ToString().Trim();
                        row.Cells["Image"].Value = Path.GetDirectoryName(FileName) + "\\CBSS\\" + CBSSList[i][0].ToString().Trim() + ".jpg";
                        row.Cells["报错描述"].Value =  new System.Text.RegularExpressions.Regex("[\\s]+").Replace(CBSSList[i][2].ToString().Trim(), " ");
                        row.Cells["原因及处理方法"].Value =  new System.Text.RegularExpressions.Regex("[\\s]+").Replace(CBSSList[i][3].ToString().Trim(), " ");
                        row.Cells["联系方式"].Value = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(CBSSList[i][4].ToString().Trim(), " ");

                    }
                }


                List<Row> otherList = execelfile.Worksheet("沃易销").ToList();

                if (otherList != null && otherList.Count > 0)
                {


                    for (int i = 0; i < otherList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(otherList[i][0]) || String.IsNullOrWhiteSpace(otherList[i][0]))
                        {
                            break;
                        }
                        dgErrorCode.Rows.Add();
                        DataGridViewRow row = dgErrorCode.Rows[dgErrorCode.RowCount - 1];

                        row.Cells["序号"].Value = dgErrorCode.RowCount;
                        row.Cells["子系统"].Value = "沃易销";
                        row.Cells["报错关键字"].Value = otherList[i][1].ToString().Trim();
                        row.Cells["Image"].Value = Path.GetDirectoryName(FileName) + "\\沃易销\\" + otherList[i][0].ToString().Trim() + ".jpg";
                        row.Cells["报错描述"].Value = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(otherList[i][2].ToString().Trim(), " ");
                        row.Cells["原因及处理方法"].Value = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(otherList[i][3].ToString().Trim(), " ");
                        row.Cells["联系方式"].Value = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(otherList[i][4].ToString().Trim(), " ");

                    }
                }



                this.dgErrorCode.AutoResizeColumns();
                this.dgErrorCode.AutoResizeRows();

                //StringBuilder sb = new StringBuilder();
                //HashSet<String> ErrorCodeSet = new HashSet<string>();
                //foreach (DataGridViewRow v in this.dgErrorCode.Rows)
                //{
                //    if (v.Cells[2].Value != null)
                //    {

                //        foreach (DataGridViewRow v2 in dgErrorCode.Rows)
                //        {
                //            if (v.Index == v2.Index)
                //            {
                //                continue;
                //            }
                //            if (v2.Cells[2].Value != null)
                //            {
                //                if (v.Cells[2].Value.ToString().Equals(v2.Cells[2].Value.ToString()))
                //                {
                //                    if (!ErrorCodeSet.Contains<String>(v2.Cells[2].Value.ToString()))
                //                    {
                //                        ErrorCodeSet.Add(v2.Cells[2].Value.ToString() );
                //                        sb.AppendFormat("{0}", v.Cells[2].Value.ToString()).AppendLine();
                //                    }
                //                    break;
                //                }
                //            }
                //        }
                //    }

                //}
                //if (!String.IsNullOrEmpty(sb.ToString()))
                //{
                //    this.btnImport.Enabled = false;
                //    MessageBox.Show("以下记录关键字重复:\n"+sb.ToString());
                //}
                //else
                //{
                //    this.btnImport.Enabled = true;
                //}
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

                agentErrorCode.keyword = dgErrorCode[2, i].Value.ToString();
                agentErrorCode.errorDesc = dgErrorCode[4, i].Value.ToString();
               

                byte[] b = new byte[0];
                String fullpath = dgErrorCode[3, i].Value.ToString();
                if (File.Exists(fullpath))
                {
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
                }
               
                agentErrorCode.solution = dgErrorCode[5, i].Value.ToString();
                agentErrorCode.contactName = dgErrorCode[6, i].Value.ToString();
               // agentErrorCode.comment = dgErrorCode[5, i].Value.ToString();
                agentErrorCode.module = dgErrorCode[1, i].Value.ToString();
                if (agentErrorCodeDao.GetByKey(agentErrorCode.keyword) != null)
                {
                    agentErrorCodeDao.Update(agentErrorCode);
                }
                else
                {
                    //agentErrorCodeDao.Delete(agentErrorCode.keyword);
                    agentErrorCodeDao.Add(agentErrorCode);
                }
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

        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel格式|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "ImportError_Template.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = Application.StartupPath + @"\Template\ImportError_Template.xlsx";
                File.Copy(filePath, saveFileDialog.FileName,true);
                String path =Path.GetDirectoryName(saveFileDialog.FileName);
                if (!Directory.Exists(path + "/ESS"))
                {
                    Directory.CreateDirectory(path + "/ESS");
                }
                if (!Directory.Exists(path + "/CBSS"))
                {
                    Directory.CreateDirectory(path + "/CBSS");
                }
            }
        }
    }
}
