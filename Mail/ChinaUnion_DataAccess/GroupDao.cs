using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class GroupDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(Group entity)
        {
            string sql = "INSERT INTO tb_group (groupName,description) VALUE (@groupName,@description)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@groupName", entity.groupName);
                command.Parameters.AddWithValue("@description", entity.description);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(Group entity)
        {
            string sql = "UPDATE  tb_group SET groupName=@groupName,description=@description where groupName=@groupName ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@groupName", entity.groupName);
                command.Parameters.AddWithValue("@description", entity.description);

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
        public int Delete(Group entity)
        {
            string sql = "DELETE FROM tb_group WHERE groupName=@groupName ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@groupName", entity.groupName);
                command.Parameters.AddWithValue("@description", entity.description);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public Group Get(String primaryKey)
        {
            string sql = "SELECT groupName,description FROM tb_group where groupName=@groupName";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@groupName", primaryKey);
                MySqlDataReader reader = command.ExecuteReader();
                Group group = null;
                if (reader.Read())
                {
                    group = new Group();
                    group.groupName = reader["groupName"] == DBNull.Value ? null : reader["groupName"].ToString();

                    group.description = reader["description"] == DBNull.Value ? null : reader["description"].ToString();
                }
                mycn.Close();
                return group;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Group> GetAll(String groupName )
        {
            string sql = "SELECT distinct groupName,description FROM tb_group where 1=1";
            if (!String.IsNullOrEmpty(groupName))
            {
                sql = sql + " and groupName = \"%" + groupName + "%\"";
            }
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                MySqlDataReader reader = command.ExecuteReader();
                IList<Group> list = new List<Group>();
                Group group = null;
                while (reader.Read())
                {
                    group = new Group();

                    group.groupName = reader["groupName"] == DBNull.Value ? null : reader["groupName"].ToString();
                    group.description = reader["description"] == DBNull.Value ? null : reader["description"].ToString();
                    list.Add(group);
                }
                mycn.Close();
                return list;
            }
        }
    }
}