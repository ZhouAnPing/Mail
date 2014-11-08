
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(Agent entity)
        {


            string sql = "INSERT INTO agent (agentNo,agentName,contactEmail,contactName,contactTel,contactWechatAccount) VALUE (@agentNo,@agentName,@contactEmail,@contactName,@contactTel,@contactWechatAccount)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@contactEmail", entity.contactEmail);
                command.Parameters.AddWithValue("@contactName", entity.contactName);
                command.Parameters.AddWithValue("@contactTel", entity.contactTel);
                command.Parameters.AddWithValue("@contactWechatAccount", entity.contactWechatAccount);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(Agent entity)
        {
            string sql = "UPDATE  agent SET agentNo=@agentNo  ,agentName=@agentName,contactEmail=@contactEmail,contactName=@contactName,contactTel=@contactTel,contactWechatAccount=@contactWechatAccount where agentNo=@agentNo ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@contactEmail", entity.contactEmail);
                command.Parameters.AddWithValue("@contactName", entity.contactName);
                command.Parameters.AddWithValue("@contactTel", entity.contactTel);
                command.Parameters.AddWithValue("@contactWechatAccount", entity.contactWechatAccount);
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
            string sql = "DELETE FROM agent WHERE agentNo=@agentNo";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", primaryKey);
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
        public IList<Agent> GetList()
        {
            string sql = "SELECT agentNo,agentName,contactEmail,contactName,contactTel,contactWechatAccount FROM agent";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<Agent> list = new List<Agent>();
                Agent agent = null;
                while (reader.Read())
                {
                    agent = new Agent();
                    
                    agent.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agent.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agent.contactEmail = reader["contactEmail"] == DBNull.Value ? null : reader["contactEmail"].ToString();
                    agent.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    agent.contactTel = reader["contactTel"] == DBNull.Value ? null : reader["contactTel"].ToString();
                    agent.contactWechatAccount = reader["contactWechatAccount"] == DBNull.Value ? null : reader["contactWechatAccount"].ToString();
                    list.Add(agent);
                }
                return list;
            }
        }
    }
}

