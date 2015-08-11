using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class AgentDailyPerformanceDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentDailyPerformance entity)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO agent_daily_performance (branchNo, branchName,agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("date) VALUE (@branchNo, @branchName,@agentNo,@agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("@feeName").Append(i.ToString()).Append(",").Append("@fee").Append(i.ToString()).Append(",");
            }
            sb.Append("@date)");

            //string sql = "INSERT INTO agent_Fee (agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal) VALUE (@agentNo, @agentFeeSeq,@feeName1,@fee1,@feeName2,@fee2,@feeName3,@fee3,@feeName4,@fee4,@feeTotal)";
            string sql = sb.ToString();
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                command.Parameters.AddWithValue("@branchNo", entity.branchNo);
                command.Parameters.AddWithValue("@branchName", entity.branchName);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);

                for (int j = 1; j <= 100; j++)
                {
                    FieldInfo feeNameField = entity.GetType().GetField("feeName" + j);
                    FieldInfo feeField = entity.GetType().GetField("fee" + j);
                    String feeNameFieldValue = feeNameField.GetValue(entity) == null ? null : feeNameField.GetValue(entity).ToString();

                    String feeFieldValue = feeField.GetValue(entity) == null ? null : feeField.GetValue(entity).ToString();


                    command.Parameters.AddWithValue("@feeName" + j, feeNameFieldValue);
                    command.Parameters.AddWithValue("@fee" + j, feeFieldValue);
                }



                command.Parameters.AddWithValue("@date", entity.date);
                return command.ExecuteNonQuery();
            }
        }
       
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(AgentDailyPerformance entity)
        {
            string sql = "DELETE FROM agent_daily_performance WHERE branchNo=@branchNo and date =@date";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@branchNo", entity.branchNo);
                command.Parameters.AddWithValue("@date", entity.date);
                return command.ExecuteNonQuery();
            }
        }
         /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentDailyPerformance GetByKey(String date, string branchNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT branchNo, branchName,agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("date");

            sb.Append(" FROM agent_daily_performance  where date=@date");
            sb.Append("  and branchNo= @branchNo ");
           
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@branchNo", branchNo);
                command.Parameters.AddWithValue("@date", date);
                MySqlDataReader reader = command.ExecuteReader();
               
                AgentDailyPerformance agentDailyPerformance = null;
                if (reader.Read())
                {
                    agentDailyPerformance = new AgentDailyPerformance();

                    agentDailyPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentDailyPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentDailyPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentDailyPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentDailyPerformance.date = reader["date"] == DBNull.Value ? null : reader["date"].ToString();
                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentDailyPerformance.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentDailyPerformance.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName"+i] == DBNull.Value ? null : reader["feeName"+i].ToString();
                        String feeFieldValue = reader["fee"+i] == DBNull.Value ? null : reader["fee"+i].ToString();
                        feeNameField.SetValue(agentDailyPerformance, feeNameFieldValue);
                        feeField.SetValue(agentDailyPerformance, feeFieldValue);                   

                    }                  


                  

                    
                }
                return agentDailyPerformance;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentDailyPerformance> GetList(String date)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT branchNo, branchName,agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("date");

            sb.Append(" FROM agent_daily_performance  where date=@date");
            
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@date", date);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentDailyPerformance> list = new List<AgentDailyPerformance>();
                AgentDailyPerformance agentDailyPerformance = null;
                while (reader.Read())
                {
                    agentDailyPerformance = new AgentDailyPerformance();

                    agentDailyPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentDailyPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentDailyPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentDailyPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentDailyPerformance.date = reader["date"] == DBNull.Value ? null : reader["date"].ToString();

                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentDailyPerformance.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentDailyPerformance.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName" + i] == DBNull.Value ? null : reader["feeName" + i].ToString();
                        String feeFieldValue = reader["fee" + i] == DBNull.Value ? null : reader["fee" + i].ToString();
                        feeNameField.SetValue(agentDailyPerformance, feeNameFieldValue);
                        feeField.SetValue(agentDailyPerformance, feeFieldValue);

                    }                  


                  

                    list.Add(agentDailyPerformance);
                }
                return list;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentDailyPerformance> GetList(String agentNo, String date)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT branchNo, branchName,agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("date");

            sb.Append(" FROM agent_daily_performance  where agentNo = @agentNo and date=@date");

            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                command.Parameters.AddWithValue("@date", date);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentDailyPerformance> list = new List<AgentDailyPerformance>();
                AgentDailyPerformance agentPerformance = null;
                while (reader.Read())
                {
                    agentPerformance = new AgentDailyPerformance();

                    agentPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentPerformance.date = reader["date"] == DBNull.Value ? null : reader["date"].ToString();

                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentPerformance.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentPerformance.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName" + i] == DBNull.Value ? null : reader["feeName" + i].ToString();
                        String feeFieldValue = reader["fee" + i] == DBNull.Value ? null : reader["fee" + i].ToString();
                        feeNameField.SetValue(agentPerformance, feeNameFieldValue);
                        feeField.SetValue(agentPerformance, feeFieldValue);

                    }

                    list.Add(agentPerformance);
                }
                return list;
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentDailyPerformance GetSummary(String agentNo, String date)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("sum(fee").Append(i.ToString()).Append(") fee").Append(i.ToString()).Append(" ,");
            }

            sb.Append("date");

            sb.Append(" FROM agent_daily_performance group by agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",");
            }
            sb.Append("date");
            sb.Append(" having agentNo = @agentNo and date=@date");

            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                command.Parameters.AddWithValue("@date", date);
                MySqlDataReader reader = command.ExecuteReader();

                AgentDailyPerformance agentDailyPerformance = null;
                if (reader.Read())
                {
                    agentDailyPerformance = new AgentDailyPerformance();

                    agentDailyPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentDailyPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    //agentDailyPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    //agentDailyPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentDailyPerformance.date = reader["date"] == DBNull.Value ? null : reader["date"].ToString();
                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentDailyPerformance.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentDailyPerformance.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName" + i] == DBNull.Value ? null : reader["feeName" + i].ToString();
                        String feeFieldValue = reader["fee" + i] == DBNull.Value ? null : reader["fee" + i].ToString();
                        feeNameField.SetValue(agentDailyPerformance, feeNameFieldValue);
                        feeField.SetValue(agentDailyPerformance, feeFieldValue);

                    }
                }
                return agentDailyPerformance;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentDailyPerformance> GetAllListDate(String agentNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT distinct agentNo,agentName,date");


            sb.Append(" FROM agent_daily_performance  where agentNo = @agentNo order by date desc");

            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                // command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentDailyPerformance> list = new List<AgentDailyPerformance>();
                AgentDailyPerformance agentDailyPerformance = null;
                while (reader.Read())
                {
                    agentDailyPerformance = new AgentDailyPerformance();

                    agentDailyPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentDailyPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();

                    agentDailyPerformance.date = reader["date"] == DBNull.Value ? null : reader["date"].ToString();



                    list.Add(agentDailyPerformance);
                }
                return list;
            }
        }
    }
}
