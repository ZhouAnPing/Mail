using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class AgentTypeCommentDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentTypeComment entity)
        {


            string sql = "INSERT INTO agent_type_comment (agentTypeComment,agentType,agentFeeMonth) VALUE (@agentTypeComment,@agentType,@agentFeeMonth)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentType", entity.agentType);
                command.Parameters.AddWithValue("@agentTypeComment", entity.agentTypeComment);
                command.Parameters.AddWithValue("@agentFeeMonth", entity.agentFeeMonth);
                

                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(AgentTypeComment entity)
        {
            string sql = "UPDATE  agent_type_comment SET agentTypeComment=@agentTypeComment,agentType=@agentType, agentFeeMonth=@agentFeeMonth where agentType=@agentType and agentFeeMonth=@agentFeeMonth ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentTypeComment", entity.agentTypeComment);
                command.Parameters.AddWithValue("@agentType", entity.agentType);
                command.Parameters.AddWithValue("@agentFeeMonth", entity.agentFeeMonth);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(AgentTypeComment entity)
        {
            string sql = "DELETE FROM agent_type_comment WHERE agentType=@agentType and agentFeeMonth=@agentFeeMonth";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
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
        public IList<AgentTypeComment> GetList(String agentFeeMonth)
        {
            string sql = "SELECT agentTypeComment,agentType,agentFeeMonth FROM agent_type_comment where agentFeeMonth=@agentFeeMonth";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentFeeMonth", agentFeeMonth);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentTypeComment> list = new List<AgentTypeComment>();
                AgentTypeComment agentTypeComment = null;
                while (reader.Read())
                {
                    agentTypeComment = new AgentTypeComment();

                    agentTypeComment.agentTypeComment = reader["agentTypeComment"] == DBNull.Value ? null : reader["agentTypeComment"].ToString();
                    agentTypeComment.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();
                    agentTypeComment.agentFeeMonth = reader["agentFeeMonth"] == DBNull.Value ? null : reader["agentFeeMonth"].ToString();

                    list.Add(agentTypeComment);
                }
                return list;
            }
        }
    }
}
