﻿using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class UserDefinedGroupDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(UserDefinedGroup entity)
        {
            string sql = "INSERT INTO tb_user_define_group (groupName,member,type) VALUE (@groupName,@member,@type)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@groupName", entity.groupName);
                command.Parameters.AddWithValue("@member", entity.member);
                command.Parameters.AddWithValue("@type", entity.type);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(UserDefinedGroup entity)
        {
            string sql = "UPDATE  tb_user_define_group SET groupName=@groupName,member=@member where groupName=@groupName and member=@member  and type=@type";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@groupName", entity.groupName);
                command.Parameters.AddWithValue("@member", entity.member);
                command.Parameters.AddWithValue("@type", entity.type);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(String groupName)
        {
            string sql = "DELETE FROM tb_user_define_group WHERE groupName=@groupName ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@groupName", groupName);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
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
        public IList<UserDefinedGroup> GetList(String groupName)
        {
            string sql = "SELECT groupName,member,type FROM tb_user_define_group where groupName=@groupName";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@groupName", groupName);
                MySqlDataReader reader = command.ExecuteReader();
                IList<UserDefinedGroup> list = new List<UserDefinedGroup>();
                UserDefinedGroup userDefinedGroup = null;
                while (reader.Read())
                {
                    userDefinedGroup = new UserDefinedGroup();

                    userDefinedGroup.groupName = reader["groupName"] == DBNull.Value ? null : reader["groupName"].ToString();
                    userDefinedGroup.member = reader["member"] == DBNull.Value ? null : reader["member"].ToString();
                    userDefinedGroup.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();

                    list.Add(userDefinedGroup);
                }
                mycn.Close();
                return list;
            }
        }



        public IList<String> GetReceiverList(IList<String> groupNames)
        {
            String parameter = "";
            foreach (String groupName in groupNames)
            {
                parameter += "'" + groupName + "',";
            }
            parameter += "'1=1'";

            string sql = " select contactId from agent_wechat_account a1,";

           sql += " (select distinct  agentNo  from    agent_type ";
            sql += " where    agentfeemonth = (select   max(agentfeemonth)";
            sql += " from   agent_type) and agenttype in (select ";
            sql += " t.member from  tb_user_define_group t";
            sql += " where  t.groupName in ("+parameter+")  and t.type = '渠道类型') ";
            sql += " union select distinct  t.member from tb_user_define_group t";
            sql += " where   t.groupName in (" + parameter + ") and t.type = '代理商/渠道') a2";
            sql += " where ( a1.agentNo = a2.agentNo) or (a1.branchNo = a2.agentno)";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
             
              
                MySqlDataReader reader = command.ExecuteReader();
                IList<String> list = new List<String>();

                while (reader.Read())
                {
                    String contactId = reader["contactId"] == DBNull.Value ? null : reader["contactId"].ToString();

                    list.Add(contactId);
                }
                mycn.Close();
                return list;
            }
        }


        
    }
}
