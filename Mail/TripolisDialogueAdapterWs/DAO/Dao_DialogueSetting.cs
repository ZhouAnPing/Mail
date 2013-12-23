using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using TripolisDialogueAdapterWs.BO;

namespace TripolisDialogueAdapterWs.DAO
{
    public class DialogueSettingDao
    {
        public const string mysqlConnection = "Host=115.29.229.134;Port=3307;Database=triplolis_ws;User Id=tripolis;password=Test123;Charset=utf8;";

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        public int Add(DialogueSettingBO entity)
        {
            string sql = "INSERT INTO cimuser (userid,userNickName) VALUE (@userid,@userNickName)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userid", entity.client);
                command.Parameters.AddWithValue("@userNickName", entity.username);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(DialogueSettingBO entity)
        {
            string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                // command.Parameters.AddWithValue("@userid", entity.UserId);
                // command.Parameters.AddWithValue("@userNickName", entity.UserNickName);
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
            string sql = "DELETE FROM cimuser WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                // command.Parameters.AddWithValue("@userid", primaryKey);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public DialogueSettingBO Get(string primaryKey)
        {
            string sql = "SELECT client,username,password,database_id,workspace_id,emailtype_id,email_id,ftp_account_id,prefix,sms_account,sms_password,sms_pid FROM t_dialoguesetting where APIKey=@APIKey";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@APIKey", primaryKey);
                MySqlDataReader reader = command.ExecuteReader();
                DialogueSettingBO DB_DialogueSetting = null;
                if (reader.Read())
                {
                    DB_DialogueSetting = new DialogueSettingBO();
                    DB_DialogueSetting.client = reader["client"] == DBNull.Value ? "" : reader["client"].ToString();
                    DB_DialogueSetting.username = reader["username"] == DBNull.Value ? "" : reader["username"].ToString();
                    DB_DialogueSetting.password = reader["password"] == DBNull.Value ? "" : reader["password"].ToString();
                    DB_DialogueSetting.contactDatabaseId = reader["database_id"] == DBNull.Value ? "" : reader["database_id"].ToString();
                    DB_DialogueSetting.workspaceId = reader["workspace_id"] == DBNull.Value ? "" : reader["workspace_id"].ToString();
                    DB_DialogueSetting.emailTypeId = reader["emailtype_id"] == DBNull.Value ? "" : reader["emailtype_id"].ToString();
                    DB_DialogueSetting.directEmailId = reader["email_id"] == DBNull.Value ? "" : reader["email_id"].ToString();
                    DB_DialogueSetting.ftpAccountId = reader["ftp_account_id"] == DBNull.Value ? "" : reader["ftp_account_id"].ToString();
                    DB_DialogueSetting.sms_account = reader["sms_account"] == DBNull.Value ? "" : reader["sms_account"].ToString();
                    DB_DialogueSetting.sms_password = reader["sms_password"] == DBNull.Value ? "" : reader["sms_password"].ToString();
                    DB_DialogueSetting.sms_pid = reader["sms_pid"] == DBNull.Value ? "" : reader["sms_pid"].ToString();
                    DB_DialogueSetting.prefix = reader["prefix"] == DBNull.Value ? "" : reader["prefix"].ToString();

                }
                return DB_DialogueSetting;
            }

        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <returns></returns>
        public IList<DialogueSettingBO> GetList()
        {
            string sql = "SELECT userid,userNickName FROM cimuser limit 1,10";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<DialogueSettingBO> list = new List<DialogueSettingBO>();
                DialogueSettingBO DB_DialogueSetting = null;
                while (reader.Read())
                {
                    DB_DialogueSetting = new DialogueSettingBO();
                    // DB_DialogueSetting.UserId = Convert.ToInt32(reader["userid"]);
                    // DB_DialogueSetting.UserNickName = reader["userNickName"] == DBNull.Value ? null : reader["userNickName"].ToString();
                    list.Add(DB_DialogueSetting);
                }
                return list;
            }
        }


    }
}