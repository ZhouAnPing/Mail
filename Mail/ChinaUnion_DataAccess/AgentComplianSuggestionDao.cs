
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentComplianSuggestionDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentComplianSuggestion entity)
        {


            string sql = "INSERT INTO agent_suggestion_feedback (createTime,agentNo,userId,type,subject,content) VALUE (@createTime,@agentNo,@userId,@type,@subject,@content)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@createTime", entity.createTime);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@content", entity.content);
               
                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(int sequence)
        {
            string sql = "DELETE FROM agent_suggestion_feedback where sequence=@sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", sequence);
              
                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int update(AgentComplianSuggestion entity)
        {


            string sql = "update agent_suggestion_feedback set ownerDepartment=@ownerDepartment, ownerReplyContent=@ownerReplyContent, checkStatus=@checkStatus, replyTime=@replyTime, replyContent=@replyContent where sequence=@sequence ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", entity.sequence);
                command.Parameters.AddWithValue("@ownerDepartment", entity.ownerDepartment);
                command.Parameters.AddWithValue("@ownerReplyContent", entity.ownerReplyContent);
                command.Parameters.AddWithValue("@checkStatus", entity.checkStatus);
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@replyTime", entity.replyTime);
                command.Parameters.AddWithValue("@replyContent", entity.replyContent);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int updateReadTime(AgentComplianSuggestion entity)
        {


            string sql = "update agent_suggestion_feedback set agentReadtime=@agentReadtime where sequence=@sequence ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", entity.sequence);
                command.Parameters.AddWithValue("@agentReadtime", entity.agentReadtime);
               

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 查询 
        /// </summary> 
        /// <returns></returns> 
        public AgentComplianSuggestion Get(int primaryKey)
        {
            string sql = "SELECT sequence, createTime,agentNo,userId,type,subject,content,ownerDepartment";
            sql = sql + ",ownerReplyContent,checkStatus,replyTime,replyContent,agentReadtime";
            sql = sql + " from agent_suggestion_feedback where sequence=@sequence";
           
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", primaryKey);
                MySqlDataReader reader = command.ExecuteReader();

                AgentComplianSuggestion  agentComplianSuggestion = new AgentComplianSuggestion();
               
                if (reader.Read())
                {
                    

                    agentComplianSuggestion.sequence = reader["sequence"] == DBNull.Value ? 0 : Int32.Parse(reader["sequence"].ToString());
                    agentComplianSuggestion.createTime = reader["createTime"] == DBNull.Value ? null : reader["createTime"].ToString();
                    agentComplianSuggestion.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentComplianSuggestion.agentNo = reader["userId"] == DBNull.Value ? null : reader["agentNo"].ToString();

                    agentComplianSuggestion.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    agentComplianSuggestion.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    agentComplianSuggestion.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    agentComplianSuggestion.ownerDepartment = reader["ownerDepartment"] == DBNull.Value ? null : reader["ownerDepartment"].ToString();
                    agentComplianSuggestion.ownerReplyContent = reader["ownerReplyContent"] == DBNull.Value ? null : reader["ownerReplyContent"].ToString();
                    agentComplianSuggestion.checkStatus = reader["checkStatus"] == DBNull.Value ? null : reader["checkStatus"].ToString();
                    agentComplianSuggestion.replyTime = reader["replyTime"] == DBNull.Value ? null : reader["replyTime"].ToString();
                    agentComplianSuggestion.replyContent = reader["replyContent"] == DBNull.Value ? null : reader["replyContent"].ToString();
                    agentComplianSuggestion.agentReadtime = reader["agentReadtime"] == DBNull.Value ? null : reader["agentReadtime"].ToString();

                   
                }
                return agentComplianSuggestion;
            }
        }
        
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentComplianSuggestion> GetListByKeyword(String keyword,String type, String agentNo,String userId)
        {
            string sql = "SELECT sequence, createTime,agentNo,userId,type,subject,content,ownerDepartment";
            sql = sql + ",ownerReplyContent,checkStatus,replyTime,replyContent,agentReadtime";
             sql = sql+" from agent_suggestion_feedback where 1=1";
             if (!String.IsNullOrEmpty(type))
             {
                 sql = sql + " and type =\"" + type + "\"";
             }
             if (!String.IsNullOrEmpty(agentNo))
             {
                 sql = sql + " and agentNo =\"" + agentNo + "\"";
             }
             if (!String.IsNullOrEmpty(userId))
             {
                 sql = sql + " and userId =\"" + userId + "\"";
             }
            if(!String.IsNullOrEmpty(keyword)){
                sql = sql + " and ((subject like \"%" + keyword +"%\")";
                sql = sql + " or (ownerReplyContent like \"%" + keyword + "%\")";
                sql = sql + " or (replyContent like \"%" + keyword + "%\")";
                sql = sql + " or (content like \"%" + keyword + "%\"))";
            }
            sql = sql + " order by createTime desc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentComplianSuggestion> list = new List<AgentComplianSuggestion>();
                AgentComplianSuggestion agentComplianSuggestion = null;
                while (reader.Read())
                {
                    agentComplianSuggestion = new AgentComplianSuggestion();
                    agentComplianSuggestion.sequence = reader["sequence"] == DBNull.Value ? 0 : Int32.Parse(reader["sequence"].ToString());
                    agentComplianSuggestion.createTime = reader["createTime"] == DBNull.Value ? null : reader["createTime"].ToString();
                    agentComplianSuggestion.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentComplianSuggestion.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    agentComplianSuggestion.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    agentComplianSuggestion.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    agentComplianSuggestion.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    agentComplianSuggestion.ownerDepartment = reader["ownerDepartment"] == DBNull.Value ? null : reader["ownerDepartment"].ToString();
                    agentComplianSuggestion.ownerReplyContent = reader["ownerReplyContent"] == DBNull.Value ? null : reader["ownerReplyContent"].ToString();
                    agentComplianSuggestion.checkStatus = reader["checkStatus"] == DBNull.Value ? null : reader["checkStatus"].ToString();
                    agentComplianSuggestion.replyTime = reader["replyTime"] == DBNull.Value ? null : reader["replyTime"].ToString();
                    agentComplianSuggestion.replyContent = reader["replyContent"] == DBNull.Value ? null : reader["replyContent"].ToString();
                    agentComplianSuggestion.agentReadtime = reader["agentReadtime"] == DBNull.Value ? null : reader["agentReadtime"].ToString();

                    list.Add(agentComplianSuggestion);
                }
                return list;
            }
        }

      
    }
}

