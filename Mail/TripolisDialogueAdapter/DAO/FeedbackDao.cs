
using System; 
using System.Data; 
using System.Data.OleDb; 
using System.Collections;

namespace TripolisDialogueAdapter
{
    class FeedbackDao
    {
        private OleDbConnection conn;
        private OleDbCommand comm;

        public FeedbackDao()//用函数实现一个数据库联接 
        {
            conn = new OleDbConnection(@"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + @"./DB/DialogueMail.accdb'");
        }

        /// <summary> 
        /// 获取二级项目信息 
        /// </summary> 
        /// <param name="peid">一级项目ID</param> 
        /// <returns>二级项目信息</returns> 
        public DataTable Feedback_GetInfo()//从数据库中读取数据到da 再在内存中建立ds 用fill 来把da的数据填充到ds再返回ds 中的首行 
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Feedback", conn);
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
        public bool Feedback_IsRecordExist(string jobId)
        {
            bool isExisted = false;
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [Feedback] where [jobId]='" + jobId+"'", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0] != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    isExisted = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show("添加失败"); 
            }
            finally
            {
                conn.Close();
            }
            return isExisted;
        }
        /// <summary> 
        /// 添加数据库记录 
        /// </summary> 
        /// <param name="feedbackBO">信息数组</param> 
        public void Feedback_updateInfoForSent(TripolisDialogueAdapter.DAO.FeedbackBO feedbackBO)
        {
            try
            {
                if (!Feedback_IsRecordExist(feedbackBO.jobId))
                {
                    comm = new OleDbCommand("insert into [Feedback] ([jobId],[email],[updateDate]) values ('"
                        + feedbackBO.jobId + "','" + feedbackBO.email + "',Now())", conn);
                }
                else
                {
                    comm = new OleDbCommand("update [Feedback] set [email]='" + feedbackBO.email + "',[updatedate]=Now() where jobId='" 
                        + feedbackBO.jobId + "'", conn);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
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
        /// 添加数据库记录 
        /// </summary> 
        /// <param name="feedbackBO">信息数组</param> 
        public void Feedback_updateInfoForOpened(TripolisDialogueAdapter.DAO.FeedbackBO feedbackBO)
        {
            try
            {
                if (!Feedback_IsRecordExist(feedbackBO.jobId))
                {
                    comm = new OleDbCommand("insert into [Feedback] ([jobId],[email],[opentime],[ipAddress],[browse],[os],[rendered],[updateDate]) values ('" 
                        + feedbackBO.jobId  + "','" + feedbackBO.email + "','" + feedbackBO.opentime + "','" 
                        + feedbackBO.ipAddress + "','" + feedbackBO.browse + "','" + feedbackBO.os + "','"
                        + feedbackBO.rendered + "',Now())", conn);
                }
                else
                {
                    comm = new OleDbCommand("update [Feedback] set  [bouncedate]=null,[bouncecode]=null,[bounceDecription]=null,[hardbounce]=null,[email]='" 
                        + feedbackBO.email + "',[opentime]='" 
                        + feedbackBO.opentime + "',[ipAddress]='" + feedbackBO.ipAddress + "',[browse]='" + feedbackBO.browse + "',[os]='"
                        + feedbackBO.os + "',[rendered]='" + feedbackBO.rendered + "',[updatedate]=Now() where jobId='" + feedbackBO.jobId + "'", conn);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
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
        /// 添加数据库记录 
        /// </summary> 
        /// <param name="feedbackBO">信息数组</param> 
        public void Feedback_updateInfoForClicked(TripolisDialogueAdapter.DAO.FeedbackBO feedbackBO)
        {
            try
            {
                if (!Feedback_IsRecordExist(feedbackBO.jobId))
                {
                    comm = new OleDbCommand("insert into [Feedback] ([jobId],[email],[linkid],[ipAddress],[browse],[clicked],[updateDate]) values ('" 
                        + feedbackBO.jobId + "','" + feedbackBO.email + "','" + feedbackBO.linkid + "','"
                        + feedbackBO.ipAddress + "','" + feedbackBO.browse + "','" + feedbackBO.clicked + "',Now())", conn);
                }
                else
                {
                    comm = new OleDbCommand("update [Feedback] set  [bouncedate]=null,[bouncecode]=null,[bounceDecription]=null,[hardbounce]=null,[email]='" 
                        + feedbackBO.email + "',[linkid]='" + feedbackBO.linkid 
                        + "',[ipAddress]='" + feedbackBO.ipAddress + "',[browse]='" + feedbackBO.browse
                        + "',[clicked]='" + feedbackBO.clicked + "',[updatedate]=Now() where jobId='" + feedbackBO.jobId + "'", conn);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
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
        /// 添加数据库记录 
        /// </summary> 
        /// <param name="feedbackBO">信息数组</param> 
        public void Feedback_updateInfoForBounced(TripolisDialogueAdapter.DAO.FeedbackBO feedbackBO)
        {
            try
            {
                if (!Feedback_IsRecordExist(feedbackBO.jobId))
                {
                    comm = new OleDbCommand("insert into [Feedback] ([jobId],[email],[bouncedate],[bouncecode],[bounceDecription],[hardbounce],[updateDate]) values ('"
                        + feedbackBO.jobId + "','" + feedbackBO.email + "','" + feedbackBO.bouncedate + "','" +
                        feedbackBO.bouncecode + "','" + feedbackBO.bounceDecription + "','" + feedbackBO.hardbounce + "',Now())", conn);
                }
                else
                {
                    comm = new OleDbCommand("update [Feedback] set [email]='" + feedbackBO.email
                        + "',[bouncedate]='" + feedbackBO.bouncedate + "',[bouncecode]='" + feedbackBO.bouncecode
                        + "',[bounceDecription]='" + feedbackBO.bounceDecription + "',[hardbounce]='" + feedbackBO.hardbounce
                        + "',[updatedate]=Now() where jobId='" + feedbackBO.jobId + "'", conn);
                }
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
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
        /// 删除数据库记录 
        /// </summary> 
        /// <param name="info">信息数组</param>123 
        public void Feedback_DeleteInfoByID(string jobId)
        {
            try
            {
                comm = new OleDbCommand("delete from Feedback where [jobId]='" + jobId + "'", conn);
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