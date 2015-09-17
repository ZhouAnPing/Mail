using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class WechatQueryLogDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(WechatQueryLog entity)
        {


            string sql = "INSERT INTO wechat_query_log (subSystem,wechatId,agentName,queryTime,queryString) VALUE (@subSystem,@wechatId,@agentName,@queryTime,@queryString)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subSystem", entity.subSystem);
                command.Parameters.AddWithValue("@wechatId", entity.wechatId);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@queryTime", entity.queryTime);
                command.Parameters.AddWithValue("@queryString", entity.queryString);

                return command.ExecuteNonQuery();
            }
        }
    }
}
