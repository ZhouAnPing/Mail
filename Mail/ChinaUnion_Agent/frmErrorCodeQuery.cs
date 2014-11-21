using ChinaUnion_Agent.Util;
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
    public partial class frmErrorCodeQuery : Form
    {
        public frmErrorCodeQuery()
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
            
           
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void btnQuery_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            this.dgErrorCode.Rows.Clear();
                dgErrorCode.Columns.Clear();

                dgErrorCode.Columns.Add("序号", "序号");
                dgErrorCode.Columns.Add("子系统", "子系统");
                dgErrorCode.Columns.Add("报错关键字", "报错关键字");
                dgErrorCode.Columns.Add("报错描述", "报错描述");
                DataGridViewImageColumn column = new DataGridViewImageColumn();
                column.HeaderText = "出错截图信息";
                column.Name = "Image";
                //column.Image =System.Drawing.Image.FromFile( "./TestError.png");
                dgErrorCode.Columns.Add(column);
                //dgErrorCode.Columns.Add("出错截图信息", "出错截图信息");
                dgErrorCode.Columns.Add("原因及处理方法", "原因及处理方法");
                dgErrorCode.Columns.Add("联系方式", "联系方式");
               // dgErrorCode.Columns.Add("备注", "备注");

            AgentErrorCodeDao agentErrorCodeDao = new AgentErrorCodeDao();
            IList<AgentErrorCode> ErrorCodeList = agentErrorCodeDao.GetList(this.txtErrorCode.Text.Trim());


            if (ErrorCodeList != null && ErrorCodeList.Count > 0)
            {
                

                for (int i = 0; i < ErrorCodeList.Count; i++)
                {
                    dgErrorCode.Rows.Add();
                    DataGridViewRow row = dgErrorCode.Rows[i];
                    row.Cells[0].Value = (i + 1).ToString();
                    row.Cells[1].Value = ErrorCodeList[i].module;
                    row.Cells[2].Value = ErrorCodeList[i].keyword;
                    row.Cells[3].Value = ErrorCodeList[i].errorDesc;
                    row.Cells[4].Value = ErrorCodeList[i].errorImg;
                    row.Cells[5].Value = ErrorCodeList[i].solution;
                    row.Cells[6].Value = ErrorCodeList[i].contactName;
                   // row.Cells[6].Value = ErrorCodeList[i].comment;


                }

                this.dgErrorCode.AutoResizeColumns();
                this.dgErrorCode.AutoResizeRows();
                //dgErrorCode.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            this.Cursor = Cursors.Default;
            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ExportData.exportGridData(this.dgErrorCode);
            this.Cursor = Cursors.Default;
        }
    }
}
