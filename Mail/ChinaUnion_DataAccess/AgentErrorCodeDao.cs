using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChinaUnion_BO;
using MySql.Data.MySqlClient;

namespace ChinaUnion_DataAccess
{
   public class AgentErrorCodeDao
    {
        public const string mysqlConnection = "User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentErrorCode entity)
        {


            string sql = "INSERT INTO agent_error_code (keyword,errorDesc,errorImg,solution,contactName,comment) VALUE (@keyword,@errorDesc,@errorImg,@solution,@contactName,@comment)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@keyword", entity.keyword);
                command.Parameters.AddWithValue("@errorDesc", entity.errorDesc);
                command.Parameters.AddWithValue("@errorImg", entity.errorImg);
                command.Parameters.AddWithValue("@solution", entity.solution);
                command.Parameters.AddWithValue("@contactName", entity.contactName);
                command.Parameters.AddWithValue("@comment", entity.comment);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(AgentErrorCode entity)
        {
            string sql = "UPDATE  agent_error_code SET keyword=@keyword,errorDesc=@errorDesc,errorImg=@errorImg,solution=@solution,contactName=@contactName,comment=@comment where  keyword=@keyword";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@keyword", entity.keyword);
                command.Parameters.AddWithValue("@errorDesc", entity.errorDesc);
                command.Parameters.AddWithValue("@errorImg", entity.errorImg);
                command.Parameters.AddWithValue("@solution", entity.solution);
                command.Parameters.AddWithValue("@contactName", entity.contactName);
                command.Parameters.AddWithValue("@comment", entity.comment);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String keyword)
        {
            string sql = "DELETE FROM agent_error_code WHERE keyword=@keyword";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@keyword", keyword);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public AgentErrorCode Get(int keyword)
        {
            string sql = "SELECT seq,keyword,errorDesc,errorImg,solution,contactName,comment FROM agent_error_code where keyword=@keyword";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@keyword", keyword);
                MySqlDataReader reader = command.ExecuteReader();
                AgentErrorCode agentErrorCode = null;
                if (reader.Read())
                {
                    agentErrorCode = new AgentErrorCode();
                    agentErrorCode.seq = reader["seq"] == DBNull.Value ? 0 : int.Parse(reader["seq"].ToString());
                    agentErrorCode.keyword = reader["keyword"] == DBNull.Value ? null : reader["keyword"].ToString();
                    agentErrorCode.errorDesc = reader["errorDesc"] == DBNull.Value ? null : reader["errorDesc"].ToString();
                    agentErrorCode.errorImg = reader["errorImg"] == DBNull.Value ? null : (byte[])reader["errorImg"];
                    agentErrorCode.solution = reader["solution"] == DBNull.Value ? null : reader["solution"].ToString();
                    agentErrorCode.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    agentErrorCode.comment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString();
                }
                return agentErrorCode;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentErrorCode> GetList(String qeuryString)
        {
            string sql = "SELECT seq,keyword,errorDesc,errorImg,solution,contactName,comment FROM agent_error_code ";

            if(!String.IsNullOrEmpty(qeuryString)){
                sql = sql + " where (keyword like \"%" + qeuryString + "%\" or errorDesc  like \"%" + qeuryString + "%\")";
            }
         
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentErrorCode> list = new List<AgentErrorCode>();
                AgentErrorCode AgentErrorCode = null;
                while (reader.Read())
                {
                    AgentErrorCode = new AgentErrorCode();
                    AgentErrorCode.seq = reader["seq"] == DBNull.Value ? 0 :int.Parse(reader["seq"].ToString());
                    AgentErrorCode.keyword = reader["keyword"] == DBNull.Value ? null : reader["keyword"].ToString();
                    AgentErrorCode.errorDesc = reader["errorDesc"] == DBNull.Value ? null : reader["errorDesc"].ToString();
                    AgentErrorCode.errorImg = reader["errorImg"] == DBNull.Value ? null : (byte[])reader["errorImg"];
                    AgentErrorCode.solution = reader["solution"] == DBNull.Value ? null : reader["solution"].ToString();
                    AgentErrorCode.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    AgentErrorCode.comment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString();
                    list.Add(AgentErrorCode);
                }
                return list;
            }
        }
    }
}
