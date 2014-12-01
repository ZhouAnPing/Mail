
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class PolicyDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(Policy entity)
        {


            string sql = "INSERT INTO tb_policy (subject,content,sender,sendtime) VALUE (@subject,@content,@sender,@sendtime)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@sender", entity.sender);
                command.Parameters.AddWithValue("@sendtime", entity.sendTime);
               
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(Policy entity)
        {
            string sql = "UPDATE  tb_policy SET subject=@subject,content=@content,sender=@sender,sendtime=@sendtime where subject=@subject ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@sender", entity.sender);
                command.Parameters.AddWithValue("@sendtime", entity.sendTime);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String primaryKey)
        {
            string sql = "DELETE FROM tb_policy WHERE subject=@subject";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", primaryKey);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public Policy Get(String primaryKey)
        {
            string sql = "SELECT subject,content,sender,sendtime from tb_policy where subject=@subject";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", primaryKey);
                MySqlDataReader reader = command.ExecuteReader();

                Policy policy = null;
                if (reader.Read())
                {
                    policy = new Policy();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.sendTime = reader["sendtime"] == DBNull.Value ? null : reader["sendtime"].ToString();
                  

                }
                return policy;
            }
               
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Policy> GetList()
        {
            string sql = "SELECT subject,content,sender,sendtime from tb_policy";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<Policy> list = new List<Policy>();
                Policy policy = null;
                while (reader.Read())
                {
                    policy = new Policy();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.sendTime = reader["sendtime"] == DBNull.Value ? null : reader["sendtime"].ToString();
                    list.Add(policy);
                }
                return list;
            }
        }
    }
}

