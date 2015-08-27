
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentScoreDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentScore entity)
        {


            string sql = "INSERT INTO agent_score (agentNo,agentName,branchNo,branchName,score,standardScore,dateTime) VALUE (@agentNo,@agentName,@branchNo,@branchName,@score,@standardScore,@dateTime)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@branchNo", entity.branchNo);
                command.Parameters.AddWithValue("@branchName", entity.branchName);
                command.Parameters.AddWithValue("@score", entity.score);
                command.Parameters.AddWithValue("@standardScore", entity.standardScore);
                
                command.Parameters.AddWithValue("@dateTime", entity.dateTime);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String agentNo,String branchNo, String dateTime)
        {
            string sql = "DELETE FROM agent_score where agentNo=@agentNo and branchNo=@branchNo and dateTime=@dateTime ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                command.Parameters.AddWithValue("@branchNo", branchNo);
                command.Parameters.AddWithValue("@dateTime", dateTime);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int DeleteAll()
        {
            string sql = "DELETE FROM agent_score";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentScore> GetLatestByKeyword(String keyword, String dateTime)
        {
            string sql = "SELECT agentNo,agentName,branchNo,branchName,score,standardScore,dateTime FROM agent_score";
            sql = sql + " where 1=1";
            if (!String.IsNullOrEmpty(dateTime))
            {
                sql = sql + " and dateTime = \"" + dateTime + "\"";
            }
            if (!String.IsNullOrEmpty(keyword))
            {
                sql = sql + " and ((agentNo = \"" + keyword + "\")";

                sql = sql + " or (branchNo = \"" + keyword + "\"))";
            }
            sql = sql + " order by agentNo asc,dateTime asc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentScore> list = new List<AgentScore>();
                AgentScore agentScore = null;
                while (reader.Read())
                {
                    agentScore = new AgentScore();

                    agentScore.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentScore.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentScore.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentScore.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentScore.score = reader["score"] == DBNull.Value ? null : reader["score"].ToString();
                    agentScore.standardScore = reader["standardScore"] == DBNull.Value ? null : reader["standardScore"].ToString();

                    agentScore.dateTime = reader["dateTime"] == DBNull.Value ? null : reader["dateTime"].ToString();
                    list.Add(agentScore);
                }
                return list;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentScore> GetListByKeyword(String keyword)
        {
            string sql = "SELECT agentNo,agentName,branchNo,branchName,score,standardScore,dateTime FROM agent_score";

            sql = sql + " where 1=1";
            if (!String.IsNullOrEmpty(keyword))
            {
                sql = sql + " and ((agentNo like \"%" + keyword + "%\")";
                sql = sql + " or (agentName like \"%" + keyword + "%\")";
                sql = sql + " or (branchNo like \"%" + keyword + "%\")";
                sql = sql + " or (agentName like \"%" + keyword + "%\"))";
            }
            sql = sql + " order by dateTime asc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentScore> list = new List<AgentScore>();
                AgentScore agentScore = null;
                while (reader.Read())
                {
                    agentScore = new AgentScore();

                    agentScore.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentScore.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentScore.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentScore.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentScore.score = reader["score"] == DBNull.Value ? null : reader["score"].ToString();
                    agentScore.standardScore = reader["standardScore"] == DBNull.Value ? null : reader["standardScore"].ToString();

                    agentScore.dateTime = reader["dateTime"] == DBNull.Value ? null : reader["dateTime"].ToString();
                    list.Add(agentScore);
                }
                return list;
            }
        }


    }
}

