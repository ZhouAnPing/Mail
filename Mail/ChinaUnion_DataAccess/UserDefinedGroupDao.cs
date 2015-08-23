using ChinaUnion_BO;
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
                return command.ExecuteNonQuery();
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
                return command.ExecuteNonQuery();
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
                return list;
            }
        }


        
    }
}
