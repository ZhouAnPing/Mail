
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentBonusDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentBonus entity)
        {


            string sql = "INSERT INTO agent_bonus (agentNo,agentName,scoreBonus,afterFeeBonus,starBonus,totalBonus) VALUE (@agentNo,@agentName,@scoreBonus,@afterFeeBonus,@starBonus,@totalBonus)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@scoreBonus", entity.scoreBonus);
                command.Parameters.AddWithValue("@afterFeeBonus", entity.afterFeeBonus);
                command.Parameters.AddWithValue("@starBonus", entity.starBonus);
                command.Parameters.AddWithValue("@totalBonus", entity.totalBonus);
                
                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String agentNo)
        {
            string sql = "DELETE FROM agent_bonus where agentNo=@agentNo ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
               
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
            string sql = "DELETE FROM agent_bonus";
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
        public AgentBonus Get(String agentNo)
        {
            string sql = "SELECT agentNo,agentName,scoreBonus,afterFeeBonus,starBonus,totalBonus FROM agent_bonus";

            sql = sql + " where agentNo = @agentNo";
             
           
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentBonus> list = new List<AgentBonus>();
                AgentBonus agentBonus = null;
                if (reader.Read())
                {
                    agentBonus = new AgentBonus();

                    agentBonus.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentBonus.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentBonus.scoreBonus = reader["scoreBonus"] == DBNull.Value ? null : reader["scoreBonus"].ToString();
                    agentBonus.afterFeeBonus = reader["afterFeeBonus"] == DBNull.Value ? null : reader["afterFeeBonus"].ToString();
                    agentBonus.starBonus = reader["starBonus"] == DBNull.Value ? null : reader["starBonus"].ToString();
                    agentBonus.totalBonus = reader["totalBonus"] == DBNull.Value ? null : reader["totalBonus"].ToString();
                    
                }
                return agentBonus;
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentBonus> GetListByKeyword(String keyword)
        {
            string sql = "SELECT agentNo,agentName,scoreBonus,afterFeeBonus,starBonus,totalBonus FROM agent_bonus";

             sql = sql+" where 1=1";
            if(!String.IsNullOrEmpty(keyword)){
                sql = sql + " and ((agentNo like \"%" + keyword +"%\")";               
                sql = sql + " or (agentName like \"%" + keyword + "%\"))";
            }
            sql = sql + " order by agentNo asc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentBonus> list = new List<AgentBonus>();
                AgentBonus agentBonus = null;
                while (reader.Read())
                {
                    agentBonus = new AgentBonus();

                    agentBonus.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentBonus.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentBonus.scoreBonus = reader["scoreBonus"] == DBNull.Value ? null : reader["scoreBonus"].ToString();
                    agentBonus.afterFeeBonus = reader["afterFeeBonus"] == DBNull.Value ? null : reader["afterFeeBonus"].ToString();
                    agentBonus.starBonus = reader["starBonus"] == DBNull.Value ? null : reader["starBonus"].ToString();
                    agentBonus.totalBonus = reader["totalBonus"] == DBNull.Value ? null : reader["totalBonus"].ToString();
                    list.Add(agentBonus);
                }
                return list;
            }
        }

    
    }
}

