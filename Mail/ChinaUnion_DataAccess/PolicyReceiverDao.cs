using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class PolicyReceiverDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(PolicyReceiver entity)
        {
            string sql = "INSERT INTO tb_policy_receiver (policy_sequence,receiver,type) VALUE (@policy_sequence,@receiver,@type)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_sequence", entity.policySequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(PolicyReceiver entity)
        {
            string sql = "UPDATE  tb_policy_receiver SET policy_sequence=@policy_sequence,receiver=@receiver where policy_sequence=@policy_sequence  and type=@type";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_sequence", entity.policySequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(String policySequence)
        {
            string sql = "DELETE FROM tb_policy_receiver WHERE policy_sequence=@policy_sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_sequence", policySequence);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(PolicyReceiver entity)
        {
            string sql = "DELETE FROM tb_policy_receiver WHERE policy_sequence=@policy_sequence and receiver=@receiver and type=@type";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_sequence", entity.policySequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
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
        public IList<PolicyReceiver> GetList(String policySequence)
        {
            string sql = "SELECT policy_sequence,receiver,type FROM tb_policy_receiver where policy_Sequence=@policy_Sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@policy_Sequence", policySequence);
                MySqlDataReader reader = command.ExecuteReader();
                IList<PolicyReceiver> list = new List<PolicyReceiver>();
                PolicyReceiver policyReceiver = null;
                while (reader.Read())
                {
                    policyReceiver = new PolicyReceiver();

                    policyReceiver.policySequence = reader["policy_sequence"] == DBNull.Value ? null : reader["policy_sequence"].ToString();
                    policyReceiver.receiver = reader["receiver"] == DBNull.Value ? null : reader["receiver"].ToString();
                    policyReceiver.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();

                    list.Add(policyReceiver);
                }
                mycn.Close();
                return list;
            }
        }


       
    }
}
