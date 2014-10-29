using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChinaUnion_Agent
{
    public partial class frmErrorCode : Form
    {
        public frmErrorCode()
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

                dgErrorCode.Columns.Add("问题类型", "问题类型");
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
                    row.Cells[1].Value = "业务受理";
                    row.Cells[2].Value = "./TestError.png";
                    row.Cells[3].Value = "对不起，你所在的渠道没有活动权限";
                    row.Cells[4].Value = "联通客服";
                    row.Cells[5].Value = "建议通过微信企业号联系";


                }

                this.dgErrorCode.AutoResizeColumns();
                this.dgErrorCode.AutoResizeRows();
            }
        }
    }
}
