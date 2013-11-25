using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace TripolisDialogueAdapter.DAO
{
    class ReportDao
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ReportDao));
        private OleDbConnection conn;
      //  private OleDbCommand comm;

        public ReportDao()//用函数实现一个数据库联接 
        {
            conn = new OleDbConnection(@"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + @"./DB/DialogueMail.accdb'");
        }
        /// <summary> 
        /// 获取二级项目信息 
        /// </summary> 
        /// <param name="peid">一级项目ID</param> 
        /// <returns>二级项目信息</returns> 
        public DataTable Report_GetInfo(String batchNo, int type)//从数据库中读取数据到da 再在内存中建立ds 用fill 来把da的数据填充到ds再返回ds 中的首行 
        {
            try
            {
                String sql = "";
                switch (type)
                {
                    case 0://sent
                        sql = "SELECT SendLog.batchNo, SendLog.email FROM  SendLog where 1=1 ";

                        break;
                    case 1://sent
                        sql = "SELECT Feedback.jobId, Feedback.email FROM Feedback INNER JOIN SendLog "
                + "ON (Ucase(Feedback.email) = Ucase(SendLog.email)) "
                + " where 1=1 AND (Ucase(Feedback.jobId) = Ucase(SendLog.jobId))"
                      + " and Feedback.bouncedate is  null ";
                        break;
                    case 2://opened
                        sql = "SELECT Feedback.jobId, Feedback.email, Feedback.opentime," +
                "Feedback.ipAddress, Feedback.browse, Feedback.os, Feedback.rendered FROM Feedback INNER JOIN SendLog "
                         + "ON (Ucase(Feedback.email) = Ucase(SendLog.email)) where Feedback.opentime is not null AND (Ucase(Feedback.jobId) = Ucase(SendLog.jobId))";
                        break;
                    case 3://clicked
                        sql = "SELECT Feedback.jobId, Feedback.email, " +
                "Feedback.ipAddress, Feedback.browse, Feedback.clicked,"
                + "Feedback.linkid FROM Feedback INNER JOIN SendLog "
                            + "ON (Ucase(Feedback.email) = Ucase(SendLog.email)) where Feedback.clicked is not null  AND (Ucase(Feedback.jobId) = Ucase(SendLog.jobId))";
                        break;
                    case 4://bounced
                        sql = "SELECT Feedback.jobId, Feedback.email, Feedback.bouncedate, Feedback.bouncecode, Feedback.bounceDecription,"
                + " Feedback.hardbounce FROM Feedback INNER JOIN SendLog "
                           + "ON (Ucase(Feedback.email) = Ucase(SendLog.email)) where Feedback.bouncedate is not null  AND (Ucase(Feedback.jobId) = Ucase(SendLog.jobId))";
                        break;
                    case 5://get All the batch
                        sql = "select distinct batchNo from sendlog where 1=1 order by batchNo";
                        break;
                }
                //String sql = "SELECT Feedback.jobId, Feedback.email, Feedback.opentime," +
                //    "Feedback.ipAddress, Feedback.browse, Feedback.os, Feedback.rendered, Feedback.clicked,"
                //    + "Feedback.linkid, Feedback.bouncedate, Feedback.bouncecode, Feedback.bounceDecription,"
                //    + " Feedback.hardbounce FROM Feedback INNER JOIN SendLog "
                //    + "ON (Feedback.email = SendLog.email) AND (Feedback.jobId = SendLog.jobId)";

                if (!String.IsNullOrWhiteSpace(batchNo))
                {
                    sql = sql + " and Ucase(batchNo) like '%" + batchNo.ToUpper() + "%'";
                }
                //if(type==0){
                //    sql = sql + " order by createdate desc";
                //}
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0];

            }
            catch(Exception ex)
            {
                logger.Info(ex.Message);
                return null;

            }
        }
    }
}
