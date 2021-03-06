﻿using ChinaUnion_BO;
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
            sb.Append("INSERT INTO agent_daily_performance (type,branchNo, branchName,agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("date) VALUE (@type,@branchNo, @branchName,@agentNo,@agentName,");
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



                command.Parameters.AddWithValue("@date", entity.date);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
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
                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }
         /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentDailyPerformance GetByKey(String date, string branchNo,String type)
        {
            StringBuilder sb = new StringBuilder();
            if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
            {
                sb.Clear();
                sb.Append("SELECT type, branchNo, branchName,agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
                }

                sb.Append("date");

                sb.Append(" FROM agent_daily_performance  where date=@date");
                sb.Append("  and branchNo= @branchNo ");
            }
            else
            {
                sb.Clear();
                sb.Append("SELECT type, branchNo, branchName,agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
                }

                sb.Append("date");

                sb.Append(" FROM agent_daily_performance  where date=@date");
                sb.Append("  and agentNo= @branchNo ");
            }
           
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
                    agentDailyPerformance.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                   
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
                mycn.Close();
                return agentDailyPerformance;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentDailyPerformance> GetAllList(String date, String type)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT type,branchNo, branchName,agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("date");

            sb.Append(" FROM agent_daily_performance  where 1=1");

            if (!String.IsNullOrEmpty(date))
            {
                sb.Append(" and date = \"" + date + "\"");
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
               // command.Parameters.AddWithValue("@date", date);
               // command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentDailyPerformance> list = new List<AgentDailyPerformance>();
                AgentDailyPerformance agentDailyPerformance = null;
                while (reader.Read())
                {
                    agentDailyPerformance = new AgentDailyPerformance();
                    agentDailyPerformance.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
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
                mycn.Close();
                return list;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentDailyPerformance> GetList(String agentNo, String date,String type)
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

                sb.Append("date");

                sb.Append(" FROM agent_daily_performance  where branchNo = @agentNo and date=@date");
            }
            else
            {
                sb.Clear();
                sb.Append("SELECT type,branchNo, branchName,agentNo,agentName,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
                }

                sb.Append("date");

                sb.Append(" FROM agent_daily_performance  where agentNo = @agentNo and date=@date");
            }
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
                    agentPerformance.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
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
                mycn.Close();
                return list;
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentDailyPerformance GetSummary(String agentNo, String date,String type)
        {
            StringBuilder sb = new StringBuilder();
            if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
            {
                sb.Clear();
                sb.Append("SELECT branchNo,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("sum(fee").Append(i.ToString()).Append(") fee").Append(i.ToString()).Append(" ,");
                }

                sb.Append("branchName");

                sb.Append(" FROM agent_daily_performance ");
                sb.Append(" where   branchNo = @branchNo and date=@date group by branchNo,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",");
                }
                sb.Append("branchName");
               
            }else{
                sb.Clear();
                sb.Append("SELECT agentNo,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",").Append("sum(fee").Append(i.ToString()).Append(") fee").Append(i.ToString()).Append(" ,");
                }

                 sb.Append(" agentName");

                sb.Append(" FROM agent_daily_performance ");
                sb.Append(" where   agentNo = @agentNo and date=@date group by agentNo,");
                for (int i = 1; i <= 100; i++)
                {
                    sb.Append("feeName").Append(i.ToString()).Append(",");
                }
                sb.Append("agentName");
               // sb.Append(" having agentNo = @agentNo and date=@date");
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
                command.Parameters.AddWithValue("@date", date);
                MySqlDataReader reader = command.ExecuteReader();

                AgentDailyPerformance agentDailyPerformance = null;
                if (reader.Read())
                {
                    agentDailyPerformance = new AgentDailyPerformance();
                    if (type.Contains("代理商"))
                    {
                        agentDailyPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                        agentDailyPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    }
                    if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
                    {
                        agentDailyPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                        agentDailyPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();

                    }
                    agentDailyPerformance.date = date;// DBNull.Value ? null : reader["date"].ToString();
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
                mycn.Close();
                return agentDailyPerformance;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentDailyPerformance> GetAllListDate(String agentNo, String type, String date)
        {
            StringBuilder sb = new StringBuilder();
            if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
            {
               
                sb.Clear();
                sb.Append("SELECT distinct branchNo,branchName,date");

                sb.Append(" FROM agent_daily_performance  where branchNo = @branchNo ");
               
            }
            else
            {
                sb.Clear();
               
                sb.Append("SELECT distinct agentNo,agentName,date");


                sb.Append(" FROM agent_daily_performance  where agentNo = @agentNo ");
            
            }
            if (!string.IsNullOrEmpty(date))
            {
                sb.Append(" and date>=@date");
            }

            sb.Append(" order by date desc");

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
                command.Parameters.AddWithValue("@date", date);
                // command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentDailyPerformance> list = new List<AgentDailyPerformance>();
                AgentDailyPerformance agentDailyPerformance = null;
                while (reader.Read())
                {
                    agentDailyPerformance = new AgentDailyPerformance();
                    if (type.Contains("直供渠道") || type.Contains("非直供渠道"))
                    {
                        agentDailyPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                        agentDailyPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    }
                    else
                    {
                        agentDailyPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                        agentDailyPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
        
                    }
                    agentDailyPerformance.date = reader["date"] == DBNull.Value ? null : reader["date"].ToString();



                    list.Add(agentDailyPerformance);
                }
                mycn.Close();
                return list;
            }
        }
    }
}
