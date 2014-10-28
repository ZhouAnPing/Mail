using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
   public class MailJobDao
    {
       public const string mysqlConnection = "User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(MailJob entity)
        {


            string sql = "INSERT INTO mail_job (mailJobId,feeMonth, subject, sendTime) VALUE (@mailJobId,@feeMonth,@subject, @sendTime)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@mailJobId", entity.mailJobId);
                command.Parameters.AddWithValue("@feeMonth", entity.feeMonth);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@sendTime", entity.sendTime);

                return command.ExecuteNonQuery();
            }
        }
        
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(MailJob entity)
        {
            string sql = "DELETE FROM mail_job WHERE mailJobId=@mailJobId";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@mailJobId", entity.mailJobId);               
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        //public Agent Get(int primaryKey)
        //{
        //    string sql = "SELECT userid,userNickName FROM cimuser where userid=@userid";
        //    using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
        //    {
        //        mycn.Open();
        //        MySqlCommand command = new MySqlCommand(sql, mycn);
        //        command.Parameters.AddWithValue("@userid", primaryKey);
        //        MySqlDataReader reader = command.ExecuteReader();
        //        Agent userBase = null;
        //        if (reader.Read())
        //        {
        //            userBase = new Agent();
        //            userBase.UserId = Convert.ToInt32(reader["userid"]);
        //            userBase.UserNickName = reader["userNickName"] == DBNull.Value ? null : reader["userNickName"].ToString();
        //        }
        //        return userBase;
        //    }
        //}
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<MailJob> GetList(String feeMonth)
        {
            string sql = "SELECT mailJobId,feeMonth, subject, sendTime FROM mail_job ";
            if (!String.IsNullOrEmpty(feeMonth))
            {
                sql = sql + " where feeMonth=@feeMonth";
            }
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                if (!String.IsNullOrEmpty(feeMonth))
                {
                    command.Parameters.AddWithValue("@feeMonth", feeMonth);
                }
                MySqlDataReader reader = command.ExecuteReader();
                IList<MailJob> list = new List<MailJob>();
                MailJob mailJob = null;
                while (reader.Read())
                {
                    mailJob = new MailJob();

                    mailJob.mailJobId = reader["mailJobId"] == DBNull.Value ? null : reader["mailJobId"].ToString();
                    mailJob.feeMonth = reader["feeMonth"] == DBNull.Value ? null : reader["feeMonth"].ToString();
                    mailJob.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    mailJob.sendTime = reader["sendTime"] == DBNull.Value ? null : reader["sendTime"].ToString();

                    list.Add(mailJob);
                }
                return list;
            }
        }
    }
}
