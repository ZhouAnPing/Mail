using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class PolicyReceiverLogDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(PolicyReceiverLog entity)
        {
            string sql = "INSERT INTO tb_policy_read_log (policy_sequence,userId,readtime) VALUE (@policy_sequence,@userId,@readtime)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_sequence", entity.policySequence);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@readtime", entity.readtime);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(PolicyReceiverLog entity)
        {
            string sql = "UPDATE  tb_policy_read_log SET policy_sequence=@policy_sequence,userId=@userId where policy_sequence=@policy_sequence  and readtime=@readtime";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_sequence", entity.policySequence);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@readtime", entity.readtime);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(PolicyReceiverLog entity)
        {
            string sql = "DELETE FROM tb_policy_read_log WHERE policy_sequence=@policy_sequence and userId=@userId and readtime=@readtime";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_sequence", entity.policySequence);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@readtime", entity.readtime);
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
        public IList<PolicyReceiverLog> GetList(String policySequence)
        {
            string sql = "SELECT policy_sequence,userId,readtime FROM tb_policy_read_log where policy_Sequence=@policy_Sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_Sequence", policySequence);
                MySqlDataReader reader = command.ExecuteReader();
                IList<PolicyReceiverLog> list = new List<PolicyReceiverLog>();
                PolicyReceiverLog policyReceiver = null;
                while (reader.Read())
                {
                    policyReceiver = new PolicyReceiverLog();

                    policyReceiver.policySequence = reader["policy_sequence"] == DBNull.Value ? null : reader["policy_sequence"].ToString();
                    policyReceiver.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    policyReceiver.readtime = reader["readtime"] == DBNull.Value ? null : reader["readtime"].ToString();

                    list.Add(policyReceiver);
                }
                return list;
            }
        }



    }
}
