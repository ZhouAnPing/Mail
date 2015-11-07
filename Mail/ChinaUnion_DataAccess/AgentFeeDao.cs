using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChinaUnion_DataAccess
{
   public class AgentFeeDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentFee entity)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO agent_Fee (agentNo,agentName,agentFeeSeq,agentFeeMonth,");
            for(int i=1; i<=100;i++){
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("feeTotal,invoiceFee,preInvoiceFee) VALUE (@agentNo,@agentName, @agentFeeSeq,@agentFeeMonth,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("@feeName").Append(i.ToString()).Append(",").Append("@fee").Append(i.ToString()).Append(",");
            }
            sb.Append("@feeTotal,@invoiceFee,@preInvoiceFee)");

            //string sql = "INSERT INTO agent_Fee (agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal) VALUE (@agentNo, @agentFeeSeq,@feeName1,@fee1,@feeName2,@fee2,@feeName3,@fee3,@feeName4,@fee4,@feeTotal)";
            string sql = sb.ToString();
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
              
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@agentFeeSeq", entity.agentFeeSeq);

                command.Parameters.AddWithValue("@agentFeeMonth", entity.agentFeeMonth);

                for (int j = 1; j <= 100; j++)
                {
                    FieldInfo feeNameField = entity.GetType().GetField("feeName" + j);
                    FieldInfo feeField = entity.GetType().GetField("fee" + j);
                    String feeNameFieldValue = feeNameField.GetValue(entity) == null ? null : feeNameField.GetValue(entity).ToString();

                    String feeFieldValue = feeField.GetValue(entity) == null ? null : feeField.GetValue(entity).ToString();
                   

                    command.Parameters.AddWithValue("@feeName" + j, feeNameFieldValue);
                    command.Parameters.AddWithValue("@fee" + j, feeFieldValue);
                }
                

                command.Parameters.AddWithValue("@feeTotal", entity.feeTotal);
                command.Parameters.AddWithValue("@invoiceFee", entity.invoiceFee);
                command.Parameters.AddWithValue("@preInvoiceFee", entity.preInvoiceFee);
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
        public int Delete(AgentFee entity)
        {
            string sql = "DELETE FROM agent_Fee WHERE agentNo=@agentNo and agentFeeMonth =@agentFeeMonth";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentFeeMonth", entity.agentFeeMonth);
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
        public AgentFee GetByKey(String agentFeeMonth, string agentNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT t1.agentNo,t1.agentName, t1.agentFeeSeq,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("t1.feeName").Append(i.ToString()).Append(",").Append("t1.fee").Append(i.ToString()).Append(",");
            }

            sb.Append("feeTotal,invoiceFee,preInvoiceFee, (select group_concat(distinct t3.agentType separator ';')  from agent_type t3 where t1.agentNo = t3.agentNo and t3.agentFeeMonth=@agentFeeMonth) agentType,");
            sb.Append("(select group_concat(distinct t4.agentTypeComment separator '<br>') from agent_type_comment t4 , agent_type t5 where t1.agentNo = t5.agentNo and  t4.agentType = t5.agentType and t4.agentFeeMonth=t5.agentFeeMonth and t4.agentFeeMonth=@agentFeeMonth) agentTypeComment");
           // sb.Append("t2.agentName,t2.contactEmail,t2.contactName,t2.contactTel");

            sb.Append(" FROM agent_Fee t1 where agentFeeMonth=@agentFeeMonth");

           // sb.Append("  and t1.agentNo= t2.agentNo ");
            sb.Append("  and t1.agentNo= @agentNo ");
           // sb.Append("  and t2.status!='Y'");
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentFeeMonth", agentFeeMonth);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                MySqlDataReader reader = command.ExecuteReader();
               
                AgentFee agentFee = null;
                if (reader.Read())
                {
                    agentFee = new AgentFee();
                    agentFee.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentFee.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentFee.agentFeeSeq = reader["agentFeeSeq"] == DBNull.Value ? null : reader["agentFeeSeq"].ToString();
                    agentFee.agentFeeMonth = agentFeeMonth;
                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentFee.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentFee.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName"+i] == DBNull.Value ? null : reader["feeName"+i].ToString();
                        String feeFieldValue = reader["fee"+i] == DBNull.Value ? null : reader["fee"+i].ToString();
                        feeNameField.SetValue(agentFee, feeNameFieldValue);
                        feeField.SetValue(agentFee, feeFieldValue);                   

                    }                  


                    agentFee.feeTotal = reader["feeTotal"] == DBNull.Value ? null : reader["feeTotal"].ToString();
                    agentFee.invoiceFee = reader["invoiceFee"] == DBNull.Value ? null : reader["invoiceFee"].ToString();
                    agentFee.preInvoiceFee = reader["preInvoiceFee"] == DBNull.Value ? null : reader["preInvoiceFee"].ToString();

                    Agent agent = new Agent();
                    agent.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                  //  agent.contactEmail = reader["contactEmail"] == DBNull.Value ? null : reader["contactEmail"].ToString();
                    //agent.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                  //  agent.contactTel = reader["contactTel"] == DBNull.Value ? null : reader["contactTel"].ToString();
                    agent.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();
                    agent.agentTypeComment = reader["agentTypeComment"] == DBNull.Value ? null : reader["agentTypeComment"].ToString();

                    agentFee.agent = agent;

                    
                }
                mycn.Close();
                return agentFee;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentFee> GetList(String agentFeeMonth)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT t1.agentNo,t1.agentName, t1.agentFeeSeq,");
            for (int i = 1; i <= 100; i++)
            {
                sb.Append("t1.feeName").Append(i.ToString()).Append(",").Append("t1.fee").Append(i.ToString()).Append(",");
            }

            sb.Append("feeTotal,invoiceFee,preInvoiceFee, (select group_concat(distinct t3.agentType separator ';')  from agent_type t3 where t1.agentNo = t3.agentNo and t3.agentFeeMonth=@agentFeeMonth) agentType,");
            sb.Append("(select group_concat(distinct t4.agentTypeComment separator '<br>') from agent_type_comment t4 , agent_type t5 where t1.agentNo = t5.agentNo and  t4.agentType = t5.agentType and t4.agentFeeMonth=t5.agentFeeMonth and t4.agentFeeMonth=@agentFeeMonth) agentTypeComment");
           // sb.Append("t2.agentName,t2.contactEmail,t2.contactName,t2.contactTel");

            sb.Append(" FROM agent_Fee t1  where agentFeeMonth=@agentFeeMonth");

           // sb.Append("  and t1.agentNo= t2.agentNo ");
          //  sb.Append("  and t2.status!='Y'");
            string sql = sb.ToString();// "SELECT agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal FROM agent_Fee";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentFeeMonth", agentFeeMonth);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentFee> list = new List<AgentFee>();
                AgentFee agentFee = null;
                while (reader.Read())
                {
                    agentFee = new AgentFee();
                    agentFee.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentFee.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentFee.agentFeeSeq = reader["agentFeeSeq"] == DBNull.Value ? null : reader["agentFeeSeq"].ToString();

                    for (int i = 1; i <= 100; i++)
                    {
                        FieldInfo feeNameField = agentFee.GetType().GetField("feeName" + i);
                        FieldInfo feeField = agentFee.GetType().GetField("fee" + i);
                        String feeNameFieldValue = reader["feeName" + i] == DBNull.Value ? null : reader["feeName" + i].ToString();
                        String feeFieldValue = reader["fee" + i] == DBNull.Value ? null : reader["fee" + i].ToString();
                        feeNameField.SetValue(agentFee, feeNameFieldValue);
                        feeField.SetValue(agentFee, feeFieldValue);

                    }                  


                    agentFee.feeTotal = reader["feeTotal"] == DBNull.Value ? null : reader["feeTotal"].ToString();
                    agentFee.invoiceFee = reader["invoiceFee"] == DBNull.Value ? null : reader["invoiceFee"].ToString();
                    agentFee.preInvoiceFee = reader["preInvoiceFee"] == DBNull.Value ? null : reader["preInvoiceFee"].ToString();

                    Agent agent = new Agent();
                    agent.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    //agent.contactEmail = reader["contactEmail"] == DBNull.Value ? null : reader["contactEmail"].ToString();
                   // agent.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    //agent.contactTel = reader["contactTel"] == DBNull.Value ? null : reader["contactTel"].ToString();
                    agent.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();
                    agent.agentTypeComment = reader["agentTypeComment"] == DBNull.Value ? null : reader["agentTypeComment"].ToString();

                    agentFee.agent = agent;

                    list.Add(agentFee);
                }
                mycn.Close();
                return list;
            }
        }
    }
}
