using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class ImportLogDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(ImportLog entity)
        {


            string sql = "INSERT INTO import_log (type,import_month) VALUE (@type,@import_month)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
               
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@import_month", entity.import_month);
               
                return command.ExecuteNonQuery();
            }
        }
        
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(ImportLog entity)
        {
            string sql = "DELETE FROM import_log WHERE  import_month=@import_month and type=@type";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
               
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@import_month", entity.import_month);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public ImportLog Get(ImportLog entity)
        {
            string sql = "SELECT type,import_month FROM import_log where  import_month=@import_month and type=@type";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
               
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@import_month", entity.import_month);
                MySqlDataReader reader = command.ExecuteReader();
                ImportLog importLog = null;
                if (reader.Read())
                {
                    importLog = new ImportLog();

                  
                    importLog.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    importLog.import_month = reader["import_month"] == DBNull.Value ? null : reader["import_month"].ToString();
                }
                return importLog;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<ImportLog> GetList(ImportLog entity)
        {
            string sql = "SELECT type,import_month FROM import_log where import_month=@import_month and type=@type";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<ImportLog> list = new List<ImportLog>();
                ImportLog importLog = null;
                while (reader.Read())
                {
                    importLog = new ImportLog();

                   
                    importLog.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    importLog.import_month = reader["import_month"] == DBNull.Value ? null : reader["import_month"].ToString();

                    list.Add(importLog);
                }
                return list;
            }
        }
    }
}
