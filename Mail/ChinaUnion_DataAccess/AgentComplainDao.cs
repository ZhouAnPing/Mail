
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentComplainDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public string Add(AgentComplain entity)
        {


            string sql = "INSERT INTO tb_complain (agentNo,userName,processCode,joinTime,joinMenu,content,processBranchCode,processBranchName,replyTime, comment,createTime) VALUE (@agentNo,@userName,@processCode,@joinTime,@joinMenu,@content,@processBranchCode,@processBranchName,@replyTime, @comment,@createTime)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@userName", entity.userName);
                command.Parameters.AddWithValue("@processCode", entity.processCode);
                command.Parameters.AddWithValue("@joinTime", entity.joinTime);
                command.Parameters.AddWithValue("@joinMenu", entity.joinMenu);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@processBranchCode", entity.processBranchCode);
                command.Parameters.AddWithValue("@processBranchName", entity.processBranchName);
                command.Parameters.AddWithValue("@replyTime", entity.replyTime);
                command.Parameters.AddWithValue("@comment", entity.comment);
                command.Parameters.AddWithValue("@createTime", entity.createTime);



                 command.ExecuteNonQuery();

                // If has last inserted id, add a parameter to hold it.
                 if (command.LastInsertedId != null) command.Parameters.Add(
                            new MySqlParameter("newId", command.LastInsertedId));

                // Return the id of the new record. Convert from Int64 to Int32 (int).
                 String str = Convert.ToString(command.Parameters["@newId"].Value);

                 mycn.Close();
                 return str;
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(AgentComplain entity)
        {
            string sql = "UPDATE  tb_complain SET agentNo=@agentNo,userName=@userName,processCode=@processCode,joinTime=@joinTime,joinMenu=@joinMenu,content=@content,processBranchCode=@processBranchCode,processBranchName=@processBranchName,";
            sql = sql + " replyTime=@replyTime,comment=@comment,createTime=@createTime where sequence=@sequence ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@userName", entity.userName);
                command.Parameters.AddWithValue("@sequence", entity.sequence);
                command.Parameters.AddWithValue("@processCode", entity.processCode);
                command.Parameters.AddWithValue("@joinTime", entity.joinTime);
                command.Parameters.AddWithValue("@joinMenu", entity.joinMenu);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@processBranchCode", entity.processBranchCode);
                command.Parameters.AddWithValue("@processBranchName", entity.processBranchName);
                command.Parameters.AddWithValue("@replyTime", entity.replyTime);
                command.Parameters.AddWithValue("@comment", entity.comment);
                command.Parameters.AddWithValue("@createTime", entity.createTime);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(int primaryKey)
        {
            string sql = "DELETE FROM tb_complain WHERE sequence=@sequence ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", primaryKey);
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
        public AgentComplain GetByProcessCode(String processCode)
        {
            string sql = "SELECT agentNo,userName,sequence,processCode,joinTime,joinMenu,content,processBranchCode,processBranchName,replyTime, comment, createTime from tb_complain where processCode=@processCode";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@processCode", processCode);
                MySqlDataReader reader = command.ExecuteReader();

                AgentComplain agentComplain = null;
                if (reader.Read())
                {
                    agentComplain = new AgentComplain();
                    agentComplain.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentComplain.userName = reader["userName"] == DBNull.Value ? null : reader["userName"].ToString();
                    agentComplain.processCode = reader["processCode"] == DBNull.Value ? null : reader["processCode"].ToString();
                    agentComplain.joinTime = reader["joinTime"] == DBNull.Value ? null : reader["joinTime"].ToString();
                    agentComplain.joinMenu = reader["joinMenu"] == DBNull.Value ? null : reader["joinMenu"].ToString();
                    agentComplain.processBranchCode = reader["processBranchCode"] == DBNull.Value ? null : reader["processBranchCode"].ToString();
                    agentComplain.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    agentComplain.processBranchName = reader["processBranchName"] == DBNull.Value ? null : reader["processBranchName"].ToString();
                    agentComplain.replyTime = reader["replyTime"] == DBNull.Value ? null : reader["replyTime"].ToString();
                    agentComplain.comment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString();
                    agentComplain.createTime = reader["createTime"] == DBNull.Value ? null : reader["createTime"].ToString();

                    agentComplain.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();


                }
                mycn.Close();
                return agentComplain;
            }

        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public AgentComplain Get(int primaryKey)
        {
            string sql = "SELECT agentNo,userName,sequence,processCode,joinTime,joinMenu,content,processBranchCode,processBranchName,replyTime, comment, createTime from tb_complain where sequence=@sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", primaryKey);
                MySqlDataReader reader = command.ExecuteReader();

                AgentComplain agentComplain = null;
                if (reader.Read())
                {
                    agentComplain = new AgentComplain();
                    agentComplain.userName = reader["userName"] == DBNull.Value ? null : reader["userName"].ToString();
                    agentComplain.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentComplain.processCode = reader["processCode"] == DBNull.Value ? null : reader["processCode"].ToString();
                    agentComplain.joinTime = reader["joinTime"] == DBNull.Value ? null : reader["joinTime"].ToString();
                    agentComplain.joinMenu = reader["joinMenu"] == DBNull.Value ? null : reader["joinMenu"].ToString();
                    agentComplain.processBranchCode = reader["processBranchCode"] == DBNull.Value ? null : reader["processBranchCode"].ToString();
                    agentComplain.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    agentComplain.processBranchName = reader["processBranchName"] == DBNull.Value ? null : reader["processBranchName"].ToString();
                    agentComplain.replyTime = reader["replyTime"] == DBNull.Value ? null : reader["replyTime"].ToString();
                    agentComplain.comment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString();
                    agentComplain.createTime = reader["createTime"] == DBNull.Value ? null : reader["createTime"].ToString();

                    agentComplain.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();


                }
                mycn.Close();
                return agentComplain;
            }

        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentComplain> GetList(string keyWord)
        {
            string sql = "SELECT agentNo,userName,sequence,processCode,joinTime,joinMenu,content,processBranchCode,processBranchName,replyTime, comment, createTime from tb_complain";
            sql = sql + " where 1=1 ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((processCode like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }

            sql = sql + " order by createTime asc,replyTime asc LIMIT 300";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentComplain> list = new List<AgentComplain>();
                AgentComplain agentComplain = null;
                while (reader.Read())
                {
                    agentComplain = new AgentComplain();
                    agentComplain.userName = reader["userName"] == DBNull.Value ? null : reader["userName"].ToString();
                    agentComplain.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentComplain.processCode = reader["processCode"] == DBNull.Value ? null : reader["processCode"].ToString();
                    agentComplain.joinTime = reader["joinTime"] == DBNull.Value ? null : reader["joinTime"].ToString();
                    agentComplain.joinMenu = reader["joinMenu"] == DBNull.Value ? null : reader["joinMenu"].ToString();
                    agentComplain.processBranchCode = reader["processBranchCode"] == DBNull.Value ? null : reader["processBranchCode"].ToString();
                    agentComplain.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    agentComplain.processBranchName = reader["processBranchName"] == DBNull.Value ? null : reader["processBranchName"].ToString();
                    agentComplain.replyTime = reader["replyTime"] == DBNull.Value ? null : reader["replyTime"].ToString();
                    agentComplain.comment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString();
                    agentComplain.createTime = reader["createTime"] == DBNull.Value ? null : reader["createTime"].ToString();

                    agentComplain.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();

                    list.Add(agentComplain);
                }
                mycn.Close();
                return list;
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentComplain> GetListByWechatUserId(string userId)
        {
            string sql = "SELECT distinct t1.agentNo,userName,t1.sequence,processCode,joinTime,joinMenu,content,processBranchCode,processBranchName,replyTime, t1.comment, createTime from tb_complain t1";
            sql = sql + " , agent_wechat_account t2 where 1=1 and t1.agentNo = t2.agentNo or t1.agentNo=t2.branchNo";
            sql = sql + " and t2.contactId=@userId";
            sql = sql + " order by createTime asc,replyTime asc LIMIT 300";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentComplain> list = new List<AgentComplain>();
                AgentComplain agentComplain = null;
                while (reader.Read())
                {
                    agentComplain = new AgentComplain();
                    agentComplain.userName = reader["userName"] == DBNull.Value ? null : reader["userName"].ToString();
                    agentComplain.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentComplain.processCode = reader["processCode"] == DBNull.Value ? null : reader["processCode"].ToString();
                    agentComplain.joinTime = reader["joinTime"] == DBNull.Value ? null : reader["joinTime"].ToString();
                    agentComplain.joinMenu = reader["joinMenu"] == DBNull.Value ? null : reader["joinMenu"].ToString();
                    agentComplain.processBranchCode = reader["processBranchCode"] == DBNull.Value ? null : reader["processBranchCode"].ToString();
                    agentComplain.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    agentComplain.processBranchName = reader["processBranchName"] == DBNull.Value ? null : reader["processBranchName"].ToString();
                    agentComplain.replyTime = reader["replyTime"] == DBNull.Value ? null : reader["replyTime"].ToString();
                    agentComplain.comment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString();
                    agentComplain.createTime = reader["createTime"] == DBNull.Value ? null : reader["createTime"].ToString();

                    agentComplain.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();

                    list.Add(agentComplain);
                }
                mycn.Close();
                return list;
            }
        }

    }
}
