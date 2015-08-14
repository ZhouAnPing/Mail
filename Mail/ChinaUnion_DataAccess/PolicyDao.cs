
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class PolicyDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(Policy entity)
        {


            string sql = "INSERT INTO tb_policy (subject,content,sender,attachment,attachmentName,creatTime,type, validateTime, isValidate, isDelete, deleteTime) VALUE (@subject,@content,@sender,@attachment,@attachmentName,@creatTime,@type, @validateTime, @isValidate, @isDelete, @deleteTime)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@sender", entity.sender);
                command.Parameters.AddWithValue("@attachment", entity.attachment);
                command.Parameters.AddWithValue("@attachmentName", entity.attachmentName);
                command.Parameters.AddWithValue("@creatTime", entity.creatTime);
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@validateTime", entity.validateTime);
                command.Parameters.AddWithValue("@isValidate", entity.isValidate);
                command.Parameters.AddWithValue("@isDelete", entity.isDelete);
                command.Parameters.AddWithValue("@deleteTime", entity.deleteTime);
               
               
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(Policy entity)
        {
            string sql = "UPDATE  tb_policy SET subject=@subject,content=@content,sender=@sender,attachment=@attachment,attachmentName=@attachmentName,creatTime=@creatTime,";
            sql = sql + " type=@type,validateTime=@validateTime,isValidate=@isValidate,isDelete=@isDelete,deleteTime=@deleteTime where sequence=@sequence ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", entity.sequence);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@sender", entity.sender);
                command.Parameters.AddWithValue("@attachment", entity.attachment);
                command.Parameters.AddWithValue("@attachmentName", entity.attachmentName);
                command.Parameters.AddWithValue("@creatTime", entity.creatTime);
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@validateTime", entity.validateTime);
                command.Parameters.AddWithValue("@isValidate", entity.isValidate);
                command.Parameters.AddWithValue("@isDelete", entity.isDelete);
                command.Parameters.AddWithValue("@deleteTime", entity.deleteTime);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(int primaryKey)
        {
            string sql = "DELETE FROM tb_policy WHERE sequence=@sequence ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", primaryKey);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public Policy Get(int primaryKey)
        {
            string sql = "SELECT sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateTime, isValidate, isDelete, deleteTime from tb_policy where sequence=@sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", primaryKey);
                MySqlDataReader reader = command.ExecuteReader();

                Policy policy = null;
                if (reader.Read())
                {
                    policy = new Policy();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateTime = reader["validateTime"] == DBNull.Value ? null : reader["validateTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();

                }
                return policy;
            }
               
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Policy> GetList(string keyWord, String type)
        {
            string sql = "SELECT sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateTime, isValidate, isDelete, deleteTime from tb_policy";
            sql = sql + " where 1=1 ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((subject like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }
            if (!String.IsNullOrEmpty(type))
            {
                sql = sql + " and type =\"" + type + "\"";
            }
            sql = sql + " order by creatTime desc LIMIT 30";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<Policy> list = new List<Policy>();
                Policy policy = null;
                while (reader.Read())
                {
                    policy = new Policy();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateTime = reader["validateTime"] == DBNull.Value ? null : reader["validateTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    list.Add(policy);
                }
                return list;
            }
        }


         /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Policy> GetAllValidatedList(string keyWord, String type)
        {
            string sql = "SELECT sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateTime, isValidate, isDelete, deleteTime from tb_policy";

            sql = sql + " where STR_TO_DATE( validateTime,'%Y-%m-%d') >= now()  ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((subject like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }

            if (!String.IsNullOrEmpty(type))
            {
                sql = sql + " and type =\"" + type + "\"";
            }

            sql = sql + " order by creatTime desc LIMIT 30";
            
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<Policy> list = new List<Policy>();
                Policy policy = null;
                while (reader.Read())
                {
                    policy = new Policy();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateTime = reader["validateTime"] == DBNull.Value ? null : reader["validateTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    list.Add(policy);
                }
                return list;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Policy> GetAllList(string keyWord)
        {
            string sql = "SELECT sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateTime, isValidate, isDelete, deleteTime from tb_policy";
            sql = sql + " where 1=1 ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((subject like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }
            sql = sql + "  order by creatTime desc ";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<Policy> list = new List<Policy>();
                Policy policy = null;
                while (reader.Read())
                {
                    policy = new Policy();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateTime = reader["validateTime"] == DBNull.Value ? null : reader["validateTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    list.Add(policy);
                }
                return list;
            }
        }
    }
}

