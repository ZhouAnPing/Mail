using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class UserGroupDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentType entity)
        {
            string sql = "INSERT INTO agent_type (agentNo,agentType,agentFeeMonth) VALUE (@agentNo,@agentType,@agentFeeMonth)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentType", entity.agentType);
                command.Parameters.AddWithValue("@agentFeeMonth", entity.agentFeeMonth);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(AgentType entity)
        {
            string sql = "UPDATE  agent_type SET agentNo=@agentNo,agentType=@agentType where agentNo=@agentNo  and agentFeeMonth=@agentFeeMonth";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentType", entity.agentType);
                command.Parameters.AddWithValue("@agentFeeMonth", entity.agentFeeMonth);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(AgentType entity)
        {
            string sql = "DELETE FROM agent_type WHERE agentNo=@agentNo and agentType=@agentType and agentFeeMonth=@agentFeeMonth";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentType", entity.agentType);
                command.Parameters.AddWithValue("@agentFeeMonth", entity.agentFeeMonth);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        //public Agent Get(int primaryKey)
        //{
        //    string sql = "SELECT userid,userNickName FROM cimuser where userid=@userid";
        //    using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
        //    {
        //        mycn.Open();
        //        MySqlCommand command = new MySqlCommand(sql, mycn);
        //        command.Parameters.AddWithValue("@userid", primaryKey);
        //        MySqlDataReader reader = command.ExecuteReader();
        //        Agent userBase = null;
        //        if (reader.Read())
        //        {
        //            userBase = new Agent();
        //            userBase.UserId = Convert.ToInt32(reader["userid"]);
        //            userBase.UserNickName = reader["userNickName"] == DBNull.Value ? null : reader["userNickName"].ToString();
        //        }
        //        return userBase;
        //    }
        //}
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentType> GetList(String agentFeeMonth)
        {
            string sql = "SELECT agentNo,agentType,agentFeeMonth FROM agent_type where agentFeeMonth=@agentFeeMonth";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentFeeMonth", agentFeeMonth);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentType> list = new List<AgentType>();
                AgentType agent_Type = null;
                while (reader.Read())
                {
                    agent_Type = new AgentType();

                    agent_Type.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agent_Type.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();
                    agent_Type.agentFeeMonth = reader["agentFeeMonth"] == DBNull.Value ? null : reader["agentFeeMonth"].ToString();

                    list.Add(agent_Type);
                }
                return list;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentType> GetDistinctType()
        {
            string sql = "SELECT distinct agentType FROM agent_type";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
               
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentType> list = new List<AgentType>();
                AgentType agent_Type = null;
                while (reader.Read())
                {
                    agent_Type = new AgentType();

                    //agent_Type.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agent_Type.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();
                  //  agent_Type.agentFeeMonth = reader["agentFeeMonth"] == DBNull.Value ? null : reader["agentFeeMonth"].ToString();

                    list.Add(agent_Type);
                }
                return list;
            }
        }
    }
}
