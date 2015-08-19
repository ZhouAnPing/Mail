
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentStarDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentStar entity)
        {


            string sql = "INSERT INTO agent_star (agentNo,agentName,branchNo,branchName,star,dateTime) VALUE (@agentNo,@agentName,@branchNo,@branchName,@star,@dateTime)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@branchNo", entity.branchNo);
                command.Parameters.AddWithValue("@branchName", entity.branchName);
                command.Parameters.AddWithValue("@star", entity.star);
                command.Parameters.AddWithValue("@dateTime", entity.dateTime);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String agentNo, String branchNo, String dateTime)
        {
            string sql = "DELETE FROM agent_star where agentNo=@agentNo and branchNo=@branchNo and dateTime=@dateTime ";
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
            string sql = "DELETE FROM agent_star";
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
        public IList<AgentStar> GetLatestByKeyword(String keyword,String dateTime)
        {
            string sql = "SELECT agentNo,agentName,branchNo,branchName,star,dateTime FROM agent_star";
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
                IList<AgentStar> list = new List<AgentStar>();
                AgentStar agentStar = null;
                while (reader.Read())
                {
                    agentStar = new AgentStar();

                    agentStar.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentStar.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentStar.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentStar.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentStar.star = reader["star"] == DBNull.Value ? null : reader["star"].ToString();
                    agentStar.dateTime = reader["dateTime"] == DBNull.Value ? null : reader["dateTime"].ToString();
                    list.Add(agentStar);
                }
                return list;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentStar> GetListByKeyword(String keyword)
        {
            string sql = "SELECT agentNo,agentName,branchNo,branchName,star,dateTime FROM agent_star";

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
                IList<AgentStar> list = new List<AgentStar>();
                AgentStar agentStar = null;
                while (reader.Read())
                {
                    agentStar = new AgentStar();

                    agentStar.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentStar.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentStar.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentStar.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentStar.star = reader["star"] == DBNull.Value ? null : reader["star"].ToString();
                    agentStar.dateTime = reader["dateTime"] == DBNull.Value ? null : reader["dateTime"].ToString();
                    list.Add(agentStar);
                }
                return list;
            }
        }


    }
}

