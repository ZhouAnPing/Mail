using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
   public class AgentFeeDao
    {
       public const string mysqlConnection = "User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentFee entity)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO agent_Fee (agentNo, agentFeeSeq,agentFeeMonth,");
            for(int i=1; i<50;i++){
                sb.Append("feeName").Append(i.ToString()).Append(",").Append("fee").Append(i.ToString()).Append(",");
            }

            sb.Append("feeTotal) VALUE (@agentNo, @agentFeeSeq,@agentFeeMonth,");
            for (int i = 1; i < 50; i++)
            {
                sb.Append("@feeName").Append(i.ToString()).Append(",").Append("@fee").Append(i.ToString()).Append(",");
            }
            sb.Append("@feeTotal)");

            //string sql = "INSERT INTO agent_Fee (agentNo, agentFeeSeq,feeName1,fee1,feeName2,fee2,feeName3,fee3,feeName4,fee4,feeTotal) VALUE (@agentNo, @agentFeeSeq,@feeName1,@fee1,@feeName2,@fee2,@feeName3,@fee3,@feeName4,@fee4,@feeTotal)";
            string sql = sb.ToString();
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
              
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentFeeSeq", entity.agentFeeSeq);

                command.Parameters.AddWithValue("@agentFeeMonth", entity.agentFeeMonth);

                command.Parameters.AddWithValue("@feeName1", entity.feeName1);
                command.Parameters.AddWithValue("@fee1", entity.fee1);
                command.Parameters.AddWithValue("@feeName2", entity.feeName2);
                command.Parameters.AddWithValue("@fee2", entity.fee2);
                command.Parameters.AddWithValue("@feeName3", entity.feeName3);
                command.Parameters.AddWithValue("@fee3", entity.fee3);
                command.Parameters.AddWithValue("@feeName4", entity.feeName4);
                command.Parameters.AddWithValue("@fee4", entity.fee4);
                command.Parameters.AddWithValue("@feeName5", entity.feeName5);
                command.Parameters.AddWithValue("@fee5", entity.fee5);
                command.Parameters.AddWithValue("@feeName6", entity.feeName6);
                command.Parameters.AddWithValue("@fee6", entity.fee6);
                command.Parameters.AddWithValue("@feeName7", entity.feeName7);
                command.Parameters.AddWithValue("@fee7", entity.fee7);
                command.Parameters.AddWithValue("@feeName8", entity.feeName8);
                command.Parameters.AddWithValue("@fee8", entity.fee8);
                command.Parameters.AddWithValue("@feeName9", entity.feeName9);
                command.Parameters.AddWithValue("@fee9", entity.fee9);
                command.Parameters.AddWithValue("@feeName10", entity.feeName10);
                command.Parameters.AddWithValue("@fee10", entity.fee10);
                command.Parameters.AddWithValue("@feeName11", entity.feeName11);
                command.Parameters.AddWithValue("@fee11", entity.fee11);
                command.Parameters.AddWithValue("@feeName12", entity.feeName12);
                command.Parameters.AddWithValue("@fee12", entity.fee12);
                command.Parameters.AddWithValue("@feeName13", entity.feeName13);
                command.Parameters.AddWithValue("@fee13", entity.fee13);
                command.Parameters.AddWithValue("@feeName14", entity.feeName14);
                command.Parameters.AddWithValue("@fee14", entity.fee14);
                command.Parameters.AddWithValue("@feeName15", entity.feeName15);
                command.Parameters.AddWithValue("@fee15", entity.fee15);
                command.Parameters.AddWithValue("@feeName16", entity.feeName16);
                command.Parameters.AddWithValue("@fee16", entity.fee16);
                command.Parameters.AddWithValue("@feeName17", entity.feeName17);
                command.Parameters.AddWithValue("@fee17", entity.fee17);
                command.Parameters.AddWithValue("@feeName18", entity.feeName18);
                command.Parameters.AddWithValue("@fee18", entity.fee18);
                command.Parameters.AddWithValue("@feeName19", entity.feeName19);
                command.Parameters.AddWithValue("@fee19", entity.fee19);
                command.Parameters.AddWithValue("@feeName20", entity.feeName20);
                command.Parameters.AddWithValue("@fee20", entity.fee20);
                command.Parameters.AddWithValue("@feeName21", entity.feeName21);
                command.Parameters.AddWithValue("@fee21", entity.fee21);
                command.Parameters.AddWithValue("@feeName22", entity.feeName22);
                command.Parameters.AddWithValue("@fee22", entity.fee22);
                command.Parameters.AddWithValue("@feeName23", entity.feeName23);
                command.Parameters.AddWithValue("@fee23", entity.fee23);
                command.Parameters.AddWithValue("@feeName24", entity.feeName24);
                command.Parameters.AddWithValue("@fee24", entity.fee24);
                command.Parameters.AddWithValue("@feeName25", entity.feeName25);
                command.Parameters.AddWithValue("@fee25", entity.fee25);
                command.Parameters.AddWithValue("@feeName26", entity.feeName26);
                command.Parameters.AddWithValue("@fee26", entity.fee26);
                command.Parameters.AddWithValue("@feeName27", entity.feeName27);
                command.Parameters.AddWithValue("@fee27", entity.fee27);
                command.Parameters.AddWithValue("@feeName28", entity.feeName28);
                command.Parameters.AddWithValue("@fee28", entity.fee28);
                command.Parameters.AddWithValue("@feeName29", entity.feeName29);
                command.Parameters.AddWithValue("@fee29", entity.fee29);
                command.Parameters.AddWithValue("@feeName30", entity.feeName30);
                command.Parameters.AddWithValue("@fee30", entity.fee30);
                command.Parameters.AddWithValue("@feeName31", entity.feeName31);
                command.Parameters.AddWithValue("@fee31", entity.fee31);
                command.Parameters.AddWithValue("@feeName32", entity.feeName32);
                command.Parameters.AddWithValue("@fee32", entity.fee32);
                command.Parameters.AddWithValue("@feeName33", entity.feeName33);
                command.Parameters.AddWithValue("@fee33", entity.fee33);
                command.Parameters.AddWithValue("@feeName34", entity.feeName34);
                command.Parameters.AddWithValue("@fee34", entity.fee34);
                command.Parameters.AddWithValue("@feeName35", entity.feeName35);
                command.Parameters.AddWithValue("@fee35", entity.fee35);
                command.Parameters.AddWithValue("@feeName36", entity.feeName36);
                command.Parameters.AddWithValue("@fee36", entity.fee36);
                command.Parameters.AddWithValue("@feeName37", entity.feeName37);
                command.Parameters.AddWithValue("@fee37", entity.fee37);
                command.Parameters.AddWithValue("@feeName38", entity.feeName38);
                command.Parameters.AddWithValue("@fee38", entity.fee38);
                command.Parameters.AddWithValue("@feeName39", entity.feeName39);
                command.Parameters.AddWithValue("@fee39", entity.fee39);
                command.Parameters.AddWithValue("@feeName40", entity.feeName40);
                command.Parameters.AddWithValue("@fee40", entity.fee40);
                command.Parameters.AddWithValue("@feeName41", entity.feeName41);
                command.Parameters.AddWithValue("@fee41", entity.fee41);
                command.Parameters.AddWithValue("@feeName42", entity.feeName42);
                command.Parameters.AddWithValue("@fee42", entity.fee42);
                command.Parameters.AddWithValue("@feeName43", entity.feeName43);
                command.Parameters.AddWithValue("@fee43", entity.fee43);
                command.Parameters.AddWithValue("@feeName44", entity.feeName44);
                command.Parameters.AddWithValue("@fee44", entity.fee44);
                command.Parameters.AddWithValue("@feeName45", entity.feeName45);
                command.Parameters.AddWithValue("@fee45", entity.fee45);
                command.Parameters.AddWithValue("@feeName46", entity.feeName46);
                command.Parameters.AddWithValue("@fee46", entity.fee46);
                command.Parameters.AddWithValue("@feeName47", entity.feeName47);
                command.Parameters.AddWithValue("@fee47", entity.fee47);
                command.Parameters.AddWithValue("@feeName48", entity.feeName48);
                command.Parameters.AddWithValue("@fee48", entity.fee48);
                command.Parameters.AddWithValue("@feeName49", entity.feeName49);
                command.Parameters.AddWithValue("@fee49", entity.fee49);
                command.Parameters.AddWithValue("@feeName50", entity.feeName50);
                command.Parameters.AddWithValue("@fee50", entity.fee50);

                command.Parameters.AddWithValue("@feeTotal", entity.feeTotal);
                return command.ExecuteNonQuery();
            }
        }
       
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(AgentFee entity)
        {
            string sql = "DELETE FROM agent_Fee WHERE agentNo=@agentNo and agentFeeSeq =@agentFeeSeq";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentFeeSeq", entity.agentFeeSeq);
                return command.ExecuteNonQuery();
            }
        }
         /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public AgentFee GetByKey(String agentFeeMonth, string agentNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT t1.agentNo, t1.agentFeeSeq,");
            for (int i = 1; i <= 50; i++)
            {
                sb.Append("t1.feeName").Append(i.ToString()).Append(",").Append("t1.fee").Append(i.ToString()).Append(",");
            }

            sb.Append("feeTotal, (select group_concat(distinct t3.agentType separator ';')  from agent_type t3 where t2.agentNo = t3.agentNo) agentType,");
            sb.Append("(select group_concat(t4.agentTypeComment separator '<br>') from agent_type_comment t4 , agent_type t5 where t2.agentNo = t5.agentNo and  t4.agentType = t5.agentType) agentTypeComment,");
            sb.Append("t2.agentName,t2.contactEmail,t2.contactName,t2.contactTel");

            sb.Append(" FROM agent_Fee t1 , agent t2 where agentFeeMonth=@agentFeeMonth");

            sb.Append("  and t1.agentNo= t2.agentNo ");
            sb.Append("  and t1.agentNo= @agentNo ");
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

                    agentFee.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentFee.agentFeeSeq = reader["agentFeeSeq"] == DBNull.Value ? null : reader["agentFeeSeq"].ToString();
                    agentFee.agentFeeMonth = agentFeeMonth;
                    agentFee.feeName1 = reader["feeName1"] == DBNull.Value ? null : reader["feeName1"].ToString();
                    agentFee.fee1 = reader["fee1"] == DBNull.Value ? null : reader["fee1"].ToString();
                    agentFee.feeName2 = reader["feeName2"] == DBNull.Value ? null : reader["feeName2"].ToString();
                    agentFee.fee2 = reader["fee2"] == DBNull.Value ? null : reader["fee2"].ToString();
                    agentFee.feeName3 = reader["feeName3"] == DBNull.Value ? null : reader["feeName3"].ToString();
                    agentFee.fee3 = reader["fee3"] == DBNull.Value ? null : reader["fee3"].ToString();
                    agentFee.feeName4 = reader["feeName4"] == DBNull.Value ? null : reader["feeName4"].ToString();
                    agentFee.fee4 = reader["fee4"] == DBNull.Value ? null : reader["fee4"].ToString();
                    agentFee.feeName5 = reader["feeName5"] == DBNull.Value ? null : reader["feeName5"].ToString();
                    agentFee.fee5 = reader["fee5"] == DBNull.Value ? null : reader["fee5"].ToString();
                    agentFee.feeName6 = reader["feeName6"] == DBNull.Value ? null : reader["feeName6"].ToString();
                    agentFee.fee6 = reader["fee6"] == DBNull.Value ? null : reader["fee6"].ToString();
                    agentFee.feeName7 = reader["feeName7"] == DBNull.Value ? null : reader["feeName7"].ToString();
                    agentFee.fee7 = reader["fee7"] == DBNull.Value ? null : reader["fee7"].ToString();
                    agentFee.feeName8 = reader["feeName8"] == DBNull.Value ? null : reader["feeName8"].ToString();
                    agentFee.fee8 = reader["fee8"] == DBNull.Value ? null : reader["fee8"].ToString();
                    agentFee.feeName9 = reader["feeName9"] == DBNull.Value ? null : reader["feeName9"].ToString();
                    agentFee.fee9 = reader["fee9"] == DBNull.Value ? null : reader["fee9"].ToString();
                    agentFee.feeName10 = reader["feeName10"] == DBNull.Value ? null : reader["feeName10"].ToString();
                    agentFee.fee10 = reader["fee10"] == DBNull.Value ? null : reader["fee10"].ToString();
                    agentFee.feeName11 = reader["feeName11"] == DBNull.Value ? null : reader["feeName11"].ToString();
                    agentFee.fee11 = reader["fee11"] == DBNull.Value ? null : reader["fee11"].ToString();
                    agentFee.feeName12 = reader["feeName12"] == DBNull.Value ? null : reader["feeName12"].ToString();
                    agentFee.fee12 = reader["fee12"] == DBNull.Value ? null : reader["fee12"].ToString();
                    agentFee.feeName13 = reader["feeName13"] == DBNull.Value ? null : reader["feeName13"].ToString();
                    agentFee.fee13 = reader["fee13"] == DBNull.Value ? null : reader["fee13"].ToString();
                    agentFee.feeName14 = reader["feeName14"] == DBNull.Value ? null : reader["feeName14"].ToString();
                    agentFee.fee14 = reader["fee14"] == DBNull.Value ? null : reader["fee14"].ToString();
                    agentFee.feeName15 = reader["feeName15"] == DBNull.Value ? null : reader["feeName15"].ToString();
                    agentFee.fee15 = reader["fee15"] == DBNull.Value ? null : reader["fee15"].ToString();
                    agentFee.feeName16 = reader["feeName16"] == DBNull.Value ? null : reader["feeName16"].ToString();
                    agentFee.fee16 = reader["fee16"] == DBNull.Value ? null : reader["fee16"].ToString();
                    agentFee.feeName17 = reader["feeName17"] == DBNull.Value ? null : reader["feeName17"].ToString();
                    agentFee.fee17 = reader["fee17"] == DBNull.Value ? null : reader["fee17"].ToString();
                    agentFee.feeName18 = reader["feeName18"] == DBNull.Value ? null : reader["feeName18"].ToString();
                    agentFee.fee18 = reader["fee18"] == DBNull.Value ? null : reader["fee18"].ToString();
                    agentFee.feeName19 = reader["feeName19"] == DBNull.Value ? null : reader["feeName19"].ToString();
                    agentFee.fee19 = reader["fee19"] == DBNull.Value ? null : reader["fee19"].ToString();
                    agentFee.feeName20 = reader["feeName20"] == DBNull.Value ? null : reader["feeName20"].ToString();
                    agentFee.fee20 = reader["fee20"] == DBNull.Value ? null : reader["fee20"].ToString();
                    agentFee.feeName21 = reader["feeName21"] == DBNull.Value ? null : reader["feeName21"].ToString();
                    agentFee.fee21 = reader["fee21"] == DBNull.Value ? null : reader["fee21"].ToString();
                    agentFee.feeName22 = reader["feeName22"] == DBNull.Value ? null : reader["feeName22"].ToString();
                    agentFee.fee22 = reader["fee22"] == DBNull.Value ? null : reader["fee22"].ToString();
                    agentFee.feeName23 = reader["feeName23"] == DBNull.Value ? null : reader["feeName23"].ToString();
                    agentFee.fee23 = reader["fee23"] == DBNull.Value ? null : reader["fee23"].ToString();
                    agentFee.feeName24 = reader["feeName24"] == DBNull.Value ? null : reader["feeName24"].ToString();
                    agentFee.fee24 = reader["fee24"] == DBNull.Value ? null : reader["fee24"].ToString();
                    agentFee.feeName25 = reader["feeName25"] == DBNull.Value ? null : reader["feeName25"].ToString();
                    agentFee.fee25 = reader["fee25"] == DBNull.Value ? null : reader["fee25"].ToString();
                    agentFee.feeName26 = reader["feeName26"] == DBNull.Value ? null : reader["feeName26"].ToString();
                    agentFee.fee26 = reader["fee26"] == DBNull.Value ? null : reader["fee26"].ToString();
                    agentFee.feeName27 = reader["feeName27"] == DBNull.Value ? null : reader["feeName27"].ToString();
                    agentFee.fee27 = reader["fee27"] == DBNull.Value ? null : reader["fee27"].ToString();
                    agentFee.feeName28 = reader["feeName28"] == DBNull.Value ? null : reader["feeName28"].ToString();
                    agentFee.fee28 = reader["fee28"] == DBNull.Value ? null : reader["fee28"].ToString();
                    agentFee.feeName29 = reader["feeName29"] == DBNull.Value ? null : reader["feeName29"].ToString();
                    agentFee.fee29 = reader["fee29"] == DBNull.Value ? null : reader["fee29"].ToString();
                    agentFee.feeName30 = reader["feeName30"] == DBNull.Value ? null : reader["feeName30"].ToString();
                    agentFee.fee30 = reader["fee30"] == DBNull.Value ? null : reader["fee30"].ToString();
                    agentFee.feeName31 = reader["feeName31"] == DBNull.Value ? null : reader["feeName31"].ToString();
                    agentFee.fee31 = reader["fee31"] == DBNull.Value ? null : reader["fee31"].ToString();
                    agentFee.feeName32 = reader["feeName32"] == DBNull.Value ? null : reader["feeName32"].ToString();
                    agentFee.fee32 = reader["fee32"] == DBNull.Value ? null : reader["fee32"].ToString();
                    agentFee.feeName33 = reader["feeName33"] == DBNull.Value ? null : reader["feeName33"].ToString();
                    agentFee.fee33 = reader["fee33"] == DBNull.Value ? null : reader["fee33"].ToString();
                    agentFee.feeName34 = reader["feeName34"] == DBNull.Value ? null : reader["feeName34"].ToString();
                    agentFee.fee34 = reader["fee34"] == DBNull.Value ? null : reader["fee34"].ToString();
                    agentFee.feeName35 = reader["feeName35"] == DBNull.Value ? null : reader["feeName35"].ToString();
                    agentFee.fee35 = reader["fee35"] == DBNull.Value ? null : reader["fee35"].ToString();
                    agentFee.feeName36 = reader["feeName36"] == DBNull.Value ? null : reader["feeName36"].ToString();
                    agentFee.fee36 = reader["fee36"] == DBNull.Value ? null : reader["fee36"].ToString();
                    agentFee.feeName37 = reader["feeName37"] == DBNull.Value ? null : reader["feeName37"].ToString();
                    agentFee.fee37 = reader["fee37"] == DBNull.Value ? null : reader["fee37"].ToString();
                    agentFee.feeName38 = reader["feeName38"] == DBNull.Value ? null : reader["feeName38"].ToString();
                    agentFee.fee38 = reader["fee38"] == DBNull.Value ? null : reader["fee38"].ToString();
                    agentFee.feeName39 = reader["feeName39"] == DBNull.Value ? null : reader["feeName39"].ToString();
                    agentFee.fee39 = reader["fee39"] == DBNull.Value ? null : reader["fee39"].ToString();
                    agentFee.feeName40 = reader["feeName40"] == DBNull.Value ? null : reader["feeName40"].ToString();
                    agentFee.fee40 = reader["fee40"] == DBNull.Value ? null : reader["fee40"].ToString();
                    agentFee.feeName41 = reader["feeName41"] == DBNull.Value ? null : reader["feeName41"].ToString();
                    agentFee.fee41 = reader["fee41"] == DBNull.Value ? null : reader["fee41"].ToString();
                    agentFee.feeName42 = reader["feeName42"] == DBNull.Value ? null : reader["feeName42"].ToString();
                    agentFee.fee42 = reader["fee42"] == DBNull.Value ? null : reader["fee42"].ToString();
                    agentFee.feeName43 = reader["feeName43"] == DBNull.Value ? null : reader["feeName43"].ToString();
                    agentFee.fee43 = reader["fee43"] == DBNull.Value ? null : reader["fee43"].ToString();
                    agentFee.feeName44 = reader["feeName44"] == DBNull.Value ? null : reader["feeName44"].ToString();
                    agentFee.fee44 = reader["fee44"] == DBNull.Value ? null : reader["fee44"].ToString();
                    agentFee.feeName45 = reader["feeName45"] == DBNull.Value ? null : reader["feeName45"].ToString();
                    agentFee.fee45 = reader["fee45"] == DBNull.Value ? null : reader["fee45"].ToString();
                    agentFee.feeName46 = reader["feeName46"] == DBNull.Value ? null : reader["feeName46"].ToString();
                    agentFee.fee46 = reader["fee46"] == DBNull.Value ? null : reader["fee46"].ToString();
                    agentFee.feeName47 = reader["feeName47"] == DBNull.Value ? null : reader["feeName47"].ToString();
                    agentFee.fee47 = reader["fee47"] == DBNull.Value ? null : reader["fee47"].ToString();
                    agentFee.feeName48 = reader["feeName48"] == DBNull.Value ? null : reader["feeName48"].ToString();
                    agentFee.fee48 = reader["fee48"] == DBNull.Value ? null : reader["fee48"].ToString();
                    agentFee.feeName49 = reader["feeName49"] == DBNull.Value ? null : reader["feeName49"].ToString();
                    agentFee.fee49 = reader["fee49"] == DBNull.Value ? null : reader["fee49"].ToString();
                    agentFee.feeName50 = reader["feeName50"] == DBNull.Value ? null : reader["feeName50"].ToString();
                    agentFee.fee50 = reader["fee50"] == DBNull.Value ? null : reader["fee50"].ToString();


                    agentFee.feeTotal = reader["feeTotal"] == DBNull.Value ? null : reader["feeTotal"].ToString();

                    Agent agent = new Agent();
                    agent.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agent.contactEmail = reader["contactEmail"] == DBNull.Value ? null : reader["contactEmail"].ToString();
                    agent.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    agent.contactTel = reader["contactTel"] == DBNull.Value ? null : reader["contactTel"].ToString();
                    agent.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();
                    agent.agentTypeComment = reader["agentTypeComment"] == DBNull.Value ? null : reader["agentTypeComment"].ToString();

                    agentFee.agent = agent;

                    
                }
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
            sb.Append("SELECT t1.agentNo, t1.agentFeeSeq,");
            for (int i = 1; i <= 50; i++)
            {
                sb.Append("t1.feeName").Append(i.ToString()).Append(",").Append("t1.fee").Append(i.ToString()).Append(",");
            }

            sb.Append("feeTotal, (select group_concat(distinct t3.agentType separator ';')  from agent_type t3 where t2.agentNo = t3.agentNo) agentType,");
            sb.Append("(select group_concat(t4.agentTypeComment separator '<br>') from agent_type_comment t4 , agent_type t5 where t2.agentNo = t5.agentNo and  t4.agentType = t5.agentType) agentTypeComment,");
            sb.Append("t2.agentName,t2.contactEmail,t2.contactName,t2.contactTel");

            sb.Append(" FROM agent_Fee t1 , agent t2 where agentFeeMonth=@agentFeeMonth");

            sb.Append("  and t1.agentNo= t2.agentNo ");
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
                    
                    agentFee.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentFee.agentFeeSeq = reader["agentFeeSeq"] == DBNull.Value ? null : reader["agentFeeSeq"].ToString();

                    agentFee.feeName1 = reader["feeName1"] == DBNull.Value ? null : reader["feeName1"].ToString();
                    agentFee.fee1 = reader["fee1"] == DBNull.Value ? null : reader["fee1"].ToString();
                    agentFee.feeName2 = reader["feeName2"] == DBNull.Value ? null : reader["feeName2"].ToString();
                    agentFee.fee2 = reader["fee2"] == DBNull.Value ? null : reader["fee2"].ToString();
                    agentFee.feeName3 = reader["feeName3"] == DBNull.Value ? null : reader["feeName3"].ToString();
                    agentFee.fee3 = reader["fee3"] == DBNull.Value ? null : reader["fee3"].ToString();
                    agentFee.feeName4 = reader["feeName4"] == DBNull.Value ? null : reader["feeName4"].ToString();
                    agentFee.fee4 = reader["fee4"] == DBNull.Value ? null : reader["fee4"].ToString();
                    agentFee.feeName5 = reader["feeName5"] == DBNull.Value ? null : reader["feeName5"].ToString();
                    agentFee.fee5 = reader["fee5"] == DBNull.Value ? null : reader["fee5"].ToString();
                    agentFee.feeName6 = reader["feeName6"] == DBNull.Value ? null : reader["feeName6"].ToString();
                    agentFee.fee6 = reader["fee6"] == DBNull.Value ? null : reader["fee6"].ToString();
                    agentFee.feeName7 = reader["feeName7"] == DBNull.Value ? null : reader["feeName7"].ToString();
                    agentFee.fee7 = reader["fee7"] == DBNull.Value ? null : reader["fee7"].ToString();
                    agentFee.feeName8 = reader["feeName8"] == DBNull.Value ? null : reader["feeName8"].ToString();
                    agentFee.fee8 = reader["fee8"] == DBNull.Value ? null : reader["fee8"].ToString();
                    agentFee.feeName9 = reader["feeName9"] == DBNull.Value ? null : reader["feeName9"].ToString();
                    agentFee.fee9 = reader["fee9"] == DBNull.Value ? null : reader["fee9"].ToString();
                    agentFee.feeName10 = reader["feeName10"] == DBNull.Value ? null : reader["feeName10"].ToString();
                    agentFee.fee10 = reader["fee10"] == DBNull.Value ? null : reader["fee10"].ToString();
                    agentFee.feeName11 = reader["feeName11"] == DBNull.Value ? null : reader["feeName11"].ToString();
                    agentFee.fee11 = reader["fee11"] == DBNull.Value ? null : reader["fee11"].ToString();
                    agentFee.feeName12 = reader["feeName12"] == DBNull.Value ? null : reader["feeName12"].ToString();
                    agentFee.fee12 = reader["fee12"] == DBNull.Value ? null : reader["fee12"].ToString();
                    agentFee.feeName13 = reader["feeName13"] == DBNull.Value ? null : reader["feeName13"].ToString();
                    agentFee.fee13 = reader["fee13"] == DBNull.Value ? null : reader["fee13"].ToString();
                    agentFee.feeName14 = reader["feeName14"] == DBNull.Value ? null : reader["feeName14"].ToString();
                    agentFee.fee14 = reader["fee14"] == DBNull.Value ? null : reader["fee14"].ToString();
                    agentFee.feeName15 = reader["feeName15"] == DBNull.Value ? null : reader["feeName15"].ToString();
                    agentFee.fee15 = reader["fee15"] == DBNull.Value ? null : reader["fee15"].ToString();
                    agentFee.feeName16 = reader["feeName16"] == DBNull.Value ? null : reader["feeName16"].ToString();
                    agentFee.fee16 = reader["fee16"] == DBNull.Value ? null : reader["fee16"].ToString();
                    agentFee.feeName17 = reader["feeName17"] == DBNull.Value ? null : reader["feeName17"].ToString();
                    agentFee.fee17 = reader["fee17"] == DBNull.Value ? null : reader["fee17"].ToString();
                    agentFee.feeName18 = reader["feeName18"] == DBNull.Value ? null : reader["feeName18"].ToString();
                    agentFee.fee18 = reader["fee18"] == DBNull.Value ? null : reader["fee18"].ToString();
                    agentFee.feeName19 = reader["feeName19"] == DBNull.Value ? null : reader["feeName19"].ToString();
                    agentFee.fee19 = reader["fee19"] == DBNull.Value ? null : reader["fee19"].ToString();
                    agentFee.feeName20 = reader["feeName20"] == DBNull.Value ? null : reader["feeName20"].ToString();
                    agentFee.fee20 = reader["fee20"] == DBNull.Value ? null : reader["fee20"].ToString();
                    agentFee.feeName21 = reader["feeName21"] == DBNull.Value ? null : reader["feeName21"].ToString();
                    agentFee.fee21 = reader["fee21"] == DBNull.Value ? null : reader["fee21"].ToString();
                    agentFee.feeName22 = reader["feeName22"] == DBNull.Value ? null : reader["feeName22"].ToString();
                    agentFee.fee22 = reader["fee22"] == DBNull.Value ? null : reader["fee22"].ToString();
                    agentFee.feeName23 = reader["feeName23"] == DBNull.Value ? null : reader["feeName23"].ToString();
                    agentFee.fee23 = reader["fee23"] == DBNull.Value ? null : reader["fee23"].ToString();
                    agentFee.feeName24 = reader["feeName24"] == DBNull.Value ? null : reader["feeName24"].ToString();
                    agentFee.fee24 = reader["fee24"] == DBNull.Value ? null : reader["fee24"].ToString();
                    agentFee.feeName25 = reader["feeName25"] == DBNull.Value ? null : reader["feeName25"].ToString();
                    agentFee.fee25 = reader["fee25"] == DBNull.Value ? null : reader["fee25"].ToString();
                    agentFee.feeName26 = reader["feeName26"] == DBNull.Value ? null : reader["feeName26"].ToString();
                    agentFee.fee26 = reader["fee26"] == DBNull.Value ? null : reader["fee26"].ToString();
                    agentFee.feeName27 = reader["feeName27"] == DBNull.Value ? null : reader["feeName27"].ToString();
                    agentFee.fee27 = reader["fee27"] == DBNull.Value ? null : reader["fee27"].ToString();
                    agentFee.feeName28 = reader["feeName28"] == DBNull.Value ? null : reader["feeName28"].ToString();
                    agentFee.fee28 = reader["fee28"] == DBNull.Value ? null : reader["fee28"].ToString();
                    agentFee.feeName29 = reader["feeName29"] == DBNull.Value ? null : reader["feeName29"].ToString();
                    agentFee.fee29 = reader["fee29"] == DBNull.Value ? null : reader["fee29"].ToString();
                    agentFee.feeName30 = reader["feeName30"] == DBNull.Value ? null : reader["feeName30"].ToString();
                    agentFee.fee30 = reader["fee30"] == DBNull.Value ? null : reader["fee30"].ToString();
                    agentFee.feeName31 = reader["feeName31"] == DBNull.Value ? null : reader["feeName31"].ToString();
                    agentFee.fee31 = reader["fee31"] == DBNull.Value ? null : reader["fee31"].ToString();
                    agentFee.feeName32 = reader["feeName32"] == DBNull.Value ? null : reader["feeName32"].ToString();
                    agentFee.fee32 = reader["fee32"] == DBNull.Value ? null : reader["fee32"].ToString();
                    agentFee.feeName33 = reader["feeName33"] == DBNull.Value ? null : reader["feeName33"].ToString();
                    agentFee.fee33 = reader["fee33"] == DBNull.Value ? null : reader["fee33"].ToString();
                    agentFee.feeName34 = reader["feeName34"] == DBNull.Value ? null : reader["feeName34"].ToString();
                    agentFee.fee34 = reader["fee34"] == DBNull.Value ? null : reader["fee34"].ToString();
                    agentFee.feeName35 = reader["feeName35"] == DBNull.Value ? null : reader["feeName35"].ToString();
                    agentFee.fee35 = reader["fee35"] == DBNull.Value ? null : reader["fee35"].ToString();
                    agentFee.feeName36 = reader["feeName36"] == DBNull.Value ? null : reader["feeName36"].ToString();
                    agentFee.fee36 = reader["fee36"] == DBNull.Value ? null : reader["fee36"].ToString();
                    agentFee.feeName37 = reader["feeName37"] == DBNull.Value ? null : reader["feeName37"].ToString();
                    agentFee.fee37 = reader["fee37"] == DBNull.Value ? null : reader["fee37"].ToString();
                    agentFee.feeName38 = reader["feeName38"] == DBNull.Value ? null : reader["feeName38"].ToString();
                    agentFee.fee38 = reader["fee38"] == DBNull.Value ? null : reader["fee38"].ToString();
                    agentFee.feeName39 = reader["feeName39"] == DBNull.Value ? null : reader["feeName39"].ToString();
                    agentFee.fee39 = reader["fee39"] == DBNull.Value ? null : reader["fee39"].ToString();
                    agentFee.feeName40 = reader["feeName40"] == DBNull.Value ? null : reader["feeName40"].ToString();
                    agentFee.fee40 = reader["fee40"] == DBNull.Value ? null : reader["fee40"].ToString();
                    agentFee.feeName41 = reader["feeName41"] == DBNull.Value ? null : reader["feeName41"].ToString();
                    agentFee.fee41 = reader["fee41"] == DBNull.Value ? null : reader["fee41"].ToString();
                    agentFee.feeName42 = reader["feeName42"] == DBNull.Value ? null : reader["feeName42"].ToString();
                    agentFee.fee42 = reader["fee42"] == DBNull.Value ? null : reader["fee42"].ToString();
                    agentFee.feeName43 = reader["feeName43"] == DBNull.Value ? null : reader["feeName43"].ToString();
                    agentFee.fee43 = reader["fee43"] == DBNull.Value ? null : reader["fee43"].ToString();
                    agentFee.feeName44 = reader["feeName44"] == DBNull.Value ? null : reader["feeName44"].ToString();
                    agentFee.fee44 = reader["fee44"] == DBNull.Value ? null : reader["fee44"].ToString();
                    agentFee.feeName45 = reader["feeName45"] == DBNull.Value ? null : reader["feeName45"].ToString();
                    agentFee.fee45 = reader["fee45"] == DBNull.Value ? null : reader["fee45"].ToString();
                    agentFee.feeName46 = reader["feeName46"] == DBNull.Value ? null : reader["feeName46"].ToString();
                    agentFee.fee46 = reader["fee46"] == DBNull.Value ? null : reader["fee46"].ToString();
                    agentFee.feeName47 = reader["feeName47"] == DBNull.Value ? null : reader["feeName47"].ToString();
                    agentFee.fee47 = reader["fee47"] == DBNull.Value ? null : reader["fee47"].ToString();
                    agentFee.feeName48 = reader["feeName48"] == DBNull.Value ? null : reader["feeName48"].ToString();
                    agentFee.fee48 = reader["fee48"] == DBNull.Value ? null : reader["fee48"].ToString();
                    agentFee.feeName49 = reader["feeName49"] == DBNull.Value ? null : reader["feeName49"].ToString();
                    agentFee.fee49 = reader["fee49"] == DBNull.Value ? null : reader["fee49"].ToString();
                    agentFee.feeName50 = reader["feeName50"] == DBNull.Value ? null : reader["feeName50"].ToString();
                    agentFee.fee50 = reader["fee50"] == DBNull.Value ? null : reader["fee50"].ToString();


                    agentFee.feeTotal = reader["feeTotal"] == DBNull.Value ? null : reader["feeTotal"].ToString();

                    Agent agent = new Agent();
                    agent.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agent.contactEmail = reader["contactEmail"] == DBNull.Value ? null : reader["contactEmail"].ToString();
                    agent.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    agent.contactTel = reader["contactTel"] == DBNull.Value ? null : reader["contactTel"].ToString();
                    agent.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();
                    agent.agentTypeComment = reader["agentTypeComment"] == DBNull.Value ? null : reader["agentTypeComment"].ToString();

                    agentFee.agent = agent;

                    list.Add(agentFee);
                }
                return list;
            }
        }
    }
}
