using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class AgentMonthPerformanceDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentMonthPerformance entity)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO  agent_month_performance (type,branchNo, branchName,agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("month) VALUE (@type,@branchNo, @branchName,@agentNo,@agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("@feeName").Append(i.ToString()).Append(",").Append("@fee").Append(i.ToString()).Append(",");
            }
            sb.Append("@month)");

            //string sql = "INSERT INTO agent_Fee (agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal) VALUE (@agentNo, @agentFeeSeq,@feeName1,@fee1,@feeName2,@fee2,@feeName3,@fee3,@feeName4,@fee4,@feeTotal)";
            string sql = sb.ToString();
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@type", entity.type);
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



                command.Parameters.AddWithValue("@month", entity.month);
                return command.ExecuteNonQuery();
            }
        }
       
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(AgentMonthPerformance entity)
        {
            string sql = "DELETE FROM agent_month_performance WHERE branchNo=@branchNo and month =@month";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@branchNo", entity.branchNo);
                command.Parameters.AddWithValue("@month", entity.month);
                return command.ExecuteNonQuery();
            }
        }
         /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentMonthPerformance GetByKey(String month, string branchNo,String type)
        {
            StringBuilder sb = new StringBuilder();
            if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
            {
                sb.Clear();
                sb.Append("SELECT type,branchNo, branchName,agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
                }

                sb.Append("month");

                sb.Append(" FROM agent_month_performance  where month=@month");
                sb.Append("  and branchNo= @branchNo ");
            }
            else
            {
                sb.Clear();
                sb.Append("SELECT type,branchNo, branchName,agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
                }

                sb.Append("month");

                sb.Append(" FROM agent_month_performance  where month=@month");
                sb.Append("  and agentNo= @branchNo ");
            }
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@branchNo", branchNo);
                command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
               
                AgentMonthPerformance agentMonthPerformance = null;
                if (reader.Read())
                {
                    agentMonthPerformance = new AgentMonthPerformance();
                    agentMonthPerformance.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();

                    agentMonthPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentMonthPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentMonthPerformance.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();
                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentMonthPerformance.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentMonthPerformance.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName"+i] == DBNull.Value ? null : reader["feeName"+i].ToString();
                        String feeFieldValue = reader["fee"+i] == DBNull.Value ? null : reader["fee"+i].ToString();
                        feeNameField.SetValue(agentMonthPerformance, feeNameFieldValue);
                        feeField.SetValue(agentMonthPerformance, feeFieldValue);                   

                    }                  


                  

                    
                }
                return agentMonthPerformance;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentMonthPerformance> GetAllList(String month, String type)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT type,branchNo, branchName,agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("month");

            sb.Append(" FROM agent_month_performance  where 1=1");

            if (!String.IsNullOrEmpty(month))
            {
                sb.Append(" and month = \"" + month + "\"");
            }
            if (!String.IsNullOrEmpty(type))
            {
                sb.Append(" and type = \"" + type + "\"");
            }
            
            
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                //command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentMonthPerformance> list = new List<AgentMonthPerformance>();
                AgentMonthPerformance agentMonthPerformance = null;
                while (reader.Read())
                {
                    agentMonthPerformance = new AgentMonthPerformance();
                    agentMonthPerformance.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();

                    agentMonthPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentMonthPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentMonthPerformance.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();

                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentMonthPerformance.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentMonthPerformance.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName" + i] == DBNull.Value ? null : reader["feeName" + i].ToString();
                        String feeFieldValue = reader["fee" + i] == DBNull.Value ? null : reader["fee" + i].ToString();
                        feeNameField.SetValue(agentMonthPerformance, feeNameFieldValue);
                        feeField.SetValue(agentMonthPerformance, feeFieldValue);

                    }                  


                  

                    list.Add(agentMonthPerformance);
                }
                return list;
            }


        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentMonthPerformance> GetList(String agentNo,String month,String type)
        {
            StringBuilder sb = new StringBuilder();
            if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
            {
                sb.Clear();
                sb.Append("SELECT type,branchNo, branchName,agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
                }

                sb.Append("month");

                sb.Append(" FROM agent_month_performance  where branchNo = @agentNo and month=@month");
  
            }
            else
            {
                sb.Clear();
                sb.Append("SELECT type,branchNo, branchName,agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
                }

                sb.Append("month");

                sb.Append(" FROM agent_month_performance  where agentNo = @agentNo and month=@month");
            }
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentMonthPerformance> list = new List<AgentMonthPerformance>();
                AgentMonthPerformance agentMonthPerformance = null;
                while (reader.Read())
                {
                    agentMonthPerformance = new AgentMonthPerformance();
                    agentMonthPerformance.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();

                    agentMonthPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentMonthPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentMonthPerformance.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();

                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentMonthPerformance.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentMonthPerformance.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName" + i] == DBNull.Value ? null : reader["feeName" + i].ToString();
                        String feeFieldValue = reader["fee" + i] == DBNull.Value ? null : reader["fee" + i].ToString();
                        feeNameField.SetValue(agentMonthPerformance, feeNameFieldValue);
                        feeField.SetValue(agentMonthPerformance, feeFieldValue);

                    }

                    list.Add(agentMonthPerformance);
                }
                return list;
            }
        }




        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentMonthPerformance GetSummary(String agentNo, String month,String type)
        {
            StringBuilder sb = new StringBuilder();
            if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
            {
                sb.Clear();
                sb.Append("SELECT branchNo,branchName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("sum(fee").Append(i.ToString()).Append(") fee").Append(i.ToString()).Append(" ,");
                }

                sb.Append("month");

                sb.Append(" FROM agent_month_performance group by branchNo,branchName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",");
                }
                sb.Append("month");
                sb.Append(" having branchNo = @branchNo and month=@month");
            }
            else
            {
                sb.Clear();
                sb.Append("SELECT agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("sum(fee").Append(i.ToString()).Append(") fee").Append(i.ToString()).Append(" ,");
                }

                sb.Append("month");

                sb.Append(" FROM agent_month_performance group by agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",");
                }
                sb.Append("month");
                sb.Append(" having agentNo = @agentNo and month=@month");
            }

            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
                {
                    command.Parameters.AddWithValue("@branchNo", agentNo);
                }
                else
                {
                    command.Parameters.AddWithValue("@agentNo", agentNo);
                }
                command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();

                AgentMonthPerformance agentMonthPerformance = null;
                if (reader.Read())
                {
                    agentMonthPerformance = new AgentMonthPerformance();

                    if (type.Contains("代理商"))
                    {
                        agentMonthPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                        agentMonthPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    }
                    if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
                    {
                        agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                        agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();

                    } 
                    agentMonthPerformance.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();
                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentMonthPerformance.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentMonthPerformance.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName" + i] == DBNull.Value ? null : reader["feeName" + i].ToString();
                        String feeFieldValue = reader["fee" + i] == DBNull.Value ? null : reader["fee" + i].ToString();
                        feeNameField.SetValue(agentMonthPerformance, feeNameFieldValue);
                        feeField.SetValue(agentMonthPerformance, feeFieldValue);

                    }
                }
                return agentMonthPerformance;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentMonthPerformance> GetAllListMonth(String agentNo,String type)
        {
            StringBuilder sb = new StringBuilder();
            if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
            {

                sb.Clear();
                sb.Append("SELECT distinct branchNo,branchName,month");

                sb.Append(" FROM agent_month_performance  where branchNo = @branchNo order by month desc");

            }
            else
            {
                sb.Clear();
                sb.Append("SELECT distinct agentNo,agentName,month");


                sb.Append(" FROM agent_month_performance  where agentNo = @agentNo order by month desc");
            }
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
                {
                    command.Parameters.AddWithValue("@branchNo", agentNo);
                    
                }
                else
                {
                    command.Parameters.AddWithValue("@agentNo", agentNo);
                }
               // command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentMonthPerformance> list = new List<AgentMonthPerformance>();
                AgentMonthPerformance agentMonthPerformance = null;
                while (reader.Read())
                {
                    agentMonthPerformance = new AgentMonthPerformance();

                    if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
                    {
                        agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                        agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    }
                    else
                    {
                        agentMonthPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                        agentMonthPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();

                    }                   
                    agentMonthPerformance.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();

                    

                    list.Add(agentMonthPerformance);
                }
                return list;
            }
        }
    }
}
