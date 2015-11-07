using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class AgentComplainReplyDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentComplainReply entity)
        {
            string sql = "INSERT INTO tb_complain_reply (sequence,complain_seq,createTime,replyComment) VALUE (@sequence,@complain_seq,@createTime,@replyComment)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", entity.sequence);
                command.Parameters.AddWithValue("@complain_seq", entity.complainSequence);
                command.Parameters.AddWithValue("@createTime", entity.replyTime);
                command.Parameters.AddWithValue("@replyComment", entity.replyContent);
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
        public int Delete(String complain_seq)
        {
            string sql = "DELETE FROM tb_complain_reply WHERE complain_seq=@complain_seq";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@complain_seq", complain_seq);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }

        
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentComplainReply> GetList(String complain_seq)
        {
            string sql = "SELECT sequence,complain_seq,createTime,replyComment FROM tb_complain_reply where complain_seq=@complain_seq";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@complain_seq", complain_seq);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentComplainReply> list = new List<AgentComplainReply>();
                AgentComplainReply agentComplainReply = null;
                while (reader.Read())
                {
                    agentComplainReply = new AgentComplainReply();

                    agentComplainReply.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    agentComplainReply.complainSequence = reader["complain_seq"] == DBNull.Value ? null : reader["complain_seq"].ToString();
                    agentComplainReply.replyTime = reader["createTime"] == DBNull.Value ? null : reader["createTime"].ToString();
                    agentComplainReply.replyContent = reader["replyComment"] == DBNull.Value ? null : reader["replyComment"].ToString();

                    list.Add(agentComplainReply);
                }
                mycn.Close();
                return list;
            }
        }


       
    }
}
