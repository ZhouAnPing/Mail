
using System; 
using System.Data; 
using System.Data.OleDb; 
using System.Collections;

namespace TripolisDialogueAdapter
{
    class SendLogDao
    {
        private OleDbConnection conn;
        private OleDbCommand comm;

        public SendLogDao()//用函数实现一个数据库联接 
        {
            conn = new OleDbConnection(@"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + @"./DB/DialogueMail.accdb'");
        }

        /// <summary> 
        /// 获取二级项目信息 
        /// </summary> 
        /// <param name="peid">一级项目ID</param> 
        /// <returns>二级项目信息</returns> 
        public DataTable SengLog_GetInfo()//从数据库中读取数据到da 再在内存中建立ds 用fill 来把da的数据填充到ds再返回ds 中的首行 
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [SendLog]", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0];

            }
            catch
            {
                return null;

            }
        }

        /// <summary> 
        /// 添加数据库记录 
        /// </summary> 
        /// <param name="info">信息数组</param> 
        public void SengLog_InsertInfo(string[] info)
        {
            try
            {
                comm = new OleDbCommand("insert into [SendLog] ([email],[jobId],[batchNo],[createDate]) values ('" + info[0] + "','" + info[1] + "','" + info[2] + "',Now())", conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show("添加失败"); 
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary> 
        /// 修改数据库记录 
        /// </summary> 
        /// <param name="info">信息数组</param> 
        /// <param name="id">ID</param> 
        public void SengLog_UpdateInfo(string[] info, string jobId)
        {
            try
            {
                comm = new OleDbCommand("update [SendLog] set [email]='" + info[0] + "',[jobId]='" + info[1] + "',[batchNo]='" + info[2] + "' where jobId=" + jobId, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                conn.Close();
            }
        }

        /// <summary> 
        /// 删除数据库记录 
        /// </summary> 
        /// <param name="info">信息数组</param>123 
        public void SengLog_DeleteInfoByID(string jobId)
        {
            try
            {
                comm = new OleDbCommand("delete from SendLog where [jobId]=" + jobId, conn);
                conn.Open();
                comm.ExecuteNonQuery();

            }
            catch { }
            finally
            {
                conn.Close();
            }
        }

    }
}