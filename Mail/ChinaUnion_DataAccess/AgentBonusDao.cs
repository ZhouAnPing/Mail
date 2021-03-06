﻿using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO agent_bonus (agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("month) VALUE (@agentNo,@agentName,");
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
                int i= command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
       
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(AgentBonus entity)
        {
            string sql = "DELETE FROM agent_bonus WHERE agentNo=@agentNo and month =@month";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@month", entity.month);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
         /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentBonus GetByKey(String month, string agentNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("month");

            sb.Append(" FROM agent_bonus  where month=@month");
            sb.Append("  and agentNo= @agentNo ");
           
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
               
                AgentBonus agentBonus = null;
                if (reader.Read())
                {
                    agentBonus = new AgentBonus();

                    agentBonus.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentBonus.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                  //  agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                   // agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentBonus.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();
                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentBonus.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentBonus.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName"+i] == DBNull.Value ? null : reader["feeName"+i].ToString();
                        String feeFieldValue = reader["fee"+i] == DBNull.Value ? null : reader["fee"+i].ToString();
                        feeNameField.SetValue(agentBonus, feeNameFieldValue);
                        feeField.SetValue(agentBonus, feeFieldValue);                   

                    }   
                }

                mycn.Close();
                return agentBonus;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentBonus> GetList(String agentNo, String month)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("month");

            sb.Append(" FROM agent_bonus  where agentNo=@agentNo and month=@month");
            
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentBonus> list = new List<AgentBonus>();
                AgentBonus agentBonus = null;
                while (reader.Read())
                {
                    agentBonus = new AgentBonus();

                    agentBonus.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentBonus.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    //agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    //agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentBonus.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();

                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentBonus.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentBonus.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName" + i] == DBNull.Value ? null : reader["feeName" + i].ToString();
                        String feeFieldValue = reader["fee" + i] == DBNull.Value ? null : reader["fee" + i].ToString();
                        feeNameField.SetValue(agentBonus, feeNameFieldValue);
                        feeField.SetValue(agentBonus, feeFieldValue);

                    }                  


                  

                    list.Add(agentBonus);
                }

                mycn.Close();
                return list;
            }


        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentBonus> GetListByKeyword(String keyword,String month)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("month");

            sb.Append(" FROM agent_bonus  where 1=1");

            if (!String.IsNullOrEmpty(month))
            {
                sb.Append(" and month = \"" + month + "\"");
            }
            if (!String.IsNullOrEmpty(keyword))
            {
                sb.Append(" and agentNo like \"%" + keyword + "%\"");

               
            }

            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                //command.Parameters.AddWithValue("@agentNo", agentNo);
                //command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentBonus> list = new List<AgentBonus>();
                AgentBonus agentMonthPerformance = null;
                while (reader.Read())
                {
                    agentMonthPerformance = new AgentBonus();

                    agentMonthPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentMonthPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    //agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    //agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
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

                mycn.Close();
                return list;
            }
        }




        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentBonus GetSummary(String agentNo, String month)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("sum(fee").Append(i.ToString()).Append(") fee").Append(i.ToString()).Append(" ,");
            }

            sb.Append("month");

            sb.Append(" FROM agent_bonus group by agentNo,agentName,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("feeName").Append(i.ToString()).Append(",");
            }
            sb.Append("month");
            sb.Append(" having agentNo = @agentNo and month=@month");

            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();

                AgentBonus agentMonthPerformance = null;
                if (reader.Read())
                {
                    agentMonthPerformance = new AgentBonus();

                    agentMonthPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                   agentMonthPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    //agentMonthPerformance.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    //agentMonthPerformance.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
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
                mycn.Close();
                return agentMonthPerformance;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentBonus> GetAllListMonth(String agentNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT distinct agentNo,agentName,month");
            

            sb.Append(" FROM agent_bonus  where agentNo = @agentNo order by month desc");

            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
               // command.Parameters.AddWithValue("@month", month);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentBonus> list = new List<AgentBonus>();
                AgentBonus agentMonthPerformance = null;
                while (reader.Read())
                {
                    agentMonthPerformance = new AgentBonus();

                    agentMonthPerformance.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentMonthPerformance.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                   
                    agentMonthPerformance.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();

                    

                    list.Add(agentMonthPerformance);
                }
                mycn.Close();
                return list;
            }
        }
    }
}
