using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.Util
{
    class ExportData
    {
        public static void exportGridData(DataGridView dgv)
        {
            if (dgv.RowCount <= 0)
            {
                MessageBox.Show("表格没有数据！");
                return;
            }
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv格式|*.csv";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "ExportData.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {


                try
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.GetEncoding("gb2312")); //写入流 

                    for (int i = 0; i < dgv.Columns.Count; ++i)
                    {
                        sw.Write(dgv.Columns[i].HeaderText);
                        sw.Write(",");
                    }
                    sw.WriteLine();// sw.Write("\r\n");

                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        for (int j = 0; j < dgv.Columns.Count; ++j)
                        {
                            sw.Write(dgv[j, i].Value == null ? null : dgv[j, i].Value.ToString().Replace(",", " ").Replace("<br>", " ").Replace("\r", " ").Replace("\n", " "));
                            sw.Write(",");
                        }
                        sw.WriteLine();// ("\r\n");
                    }
                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("成功导出[" + dgv.RowCount.ToString() + "]行数据！");
                }
                catch
                {
                    MessageBox.Show("导出失败！");
                }
            }
        }
    }
}
