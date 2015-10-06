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


            string sql = "INSERT INTO wechat_query_log (subSystem,wechatId,agentName,queryTime,queryString,module) VALUE (@subSystem,@wechatId,@agentName,@queryTime,@queryString,@module)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subSystem", entity.subSystem);
                command.Parameters.AddWithValue("@wechatId", entity.wechatId);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@queryTime", entity.queryTime);
                command.Parameters.AddWithValue("@queryString", entity.queryString);
                command.Parameters.AddWithValue("@module", entity.module);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<WechatQueryLog> GetList(String module, String queryString, String wechatId, String queryStartTime, String queryEndTime)
        {
            string sql = "SELECT distinct module,wechatId,queryTime,queryString, contactName FROM wechat_query_log left join agent_wechat_account on  wechat_query_log.wechatId=agent_wechat_account.contactId  where 1=1   ";
            if (!String.IsNullOrEmpty(module))
            {
                sql = sql + " and  (module=\"" + module + "\")";
            }
            if (!String.IsNullOrEmpty(queryString))
            {
                sql = sql + " and  (queryString like \"%" + queryString + "%\")";
            }
            if (!String.IsNullOrEmpty(wechatId))
            {
                sql = sql + " and  (wechatId like \"%" + wechatId + "%\")";
            }
            if (!String.IsNullOrEmpty(queryStartTime))
            {
                sql = sql + " and  queryTime>='" + queryStartTime + "'";
            }
            if (!String.IsNullOrEmpty(queryEndTime))
            {
                sql = sql + " and  queryTime<='" + queryEndTime + "'";
            }
            sql = sql + " order by queryTime desc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                MySqlDataReader reader = command.ExecuteReader();
                IList<WechatQueryLog> list = new List<WechatQueryLog>();
                WechatQueryLog wechatQueryLog = null;
                while (reader.Read())
                {
                    wechatQueryLog = new WechatQueryLog();
                    wechatQueryLog.module = reader["module"] == DBNull.Value ? null : reader["module"].ToString();

                   // wechatQueryLog.subSystem = reader["subSystem"] == DBNull.Value ? null : reader["subSystem"].ToString();
                    wechatQueryLog.wechatId = reader["wechatId"] == DBNull.Value ? null : reader["wechatId"].ToString();
                    wechatQueryLog.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    wechatQueryLog.queryTime = reader["queryTime"] == DBNull.Value ? null : reader["queryTime"].ToString();
                    wechatQueryLog.queryString = reader["queryString"] == DBNull.Value ? null : reader["queryString"].ToString();
                   
                    list.Add(wechatQueryLog);
                }
                return list;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<WechatQueryLog> GetUsedSummary(String module, String queryStartTime, String queryEndTime)
        {
            string sql = "SELECT distinct module,wechatId, contactName FROM wechat_query_log left join agent_wechat_account on  wechat_query_log.wechatId=agent_wechat_account.contactId  where 1=1  ";
            if (!String.IsNullOrEmpty(module))
            {
                sql = sql + " and  (module=\"" + module + "\")";
            }
           
            if (!String.IsNullOrEmpty(queryStartTime))
            {
                sql = sql + " and  queryTime>='" + queryStartTime + "'";
            }
            if (!String.IsNullOrEmpty(queryEndTime))
            {
                sql = sql + " and  queryTime<='" + queryEndTime + "'";
            }
            sql = sql + " and queryString<>'成员进入应用' order by queryTime desc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                MySqlDataReader reader = command.ExecuteReader();
                IList<WechatQueryLog> list = new List<WechatQueryLog>();
                WechatQueryLog wechatQueryLog = null;
                while (reader.Read())
                {
                    wechatQueryLog = new WechatQueryLog();
                    wechatQueryLog.module = reader["module"] == DBNull.Value ? null : reader["module"].ToString();
                    wechatQueryLog.wechatId = reader["wechatId"] == DBNull.Value ? null : reader["wechatId"].ToString();
                    wechatQueryLog.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    

                    list.Add(wechatQueryLog);
                }
                return list;
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<WechatQueryLog> GetUsedDetail(String module, String queryStartTime, String queryEndTime)
        {
            string sql = "SELECT distinct module,wechatId, contactName,queryTime,queryString FROM wechat_query_log left join agent_wechat_account on  wechat_query_log.wechatId=agent_wechat_account.contactId  where 1=1  ";
            if (!String.IsNullOrEmpty(module))
            {
                sql = sql + " and  (module=\"" + module + "\")";
            }

            if (!String.IsNullOrEmpty(queryStartTime))
            {
                sql = sql + " and  queryTime>='" + queryStartTime + "'";
            }
            if (!String.IsNullOrEmpty(queryEndTime))
            {
                sql = sql + " and  queryTime<='" + queryEndTime + "'";
            }
            sql = sql + " and queryString<>'成员进入应用' order by queryTime desc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                MySqlDataReader reader = command.ExecuteReader();
                IList<WechatQueryLog> list = new List<WechatQueryLog>();
                WechatQueryLog wechatQueryLog = null;
                while (reader.Read())
                {
                    wechatQueryLog = new WechatQueryLog();
                    wechatQueryLog.module = reader["module"] == DBNull.Value ? null : reader["module"].ToString();
                    wechatQueryLog.wechatId = reader["wechatId"] == DBNull.Value ? null : reader["wechatId"].ToString();
                    wechatQueryLog.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    wechatQueryLog.queryTime = reader["queryTime"] == DBNull.Value ? null : reader["queryTime"].ToString();
                    wechatQueryLog.queryString = reader["queryString"] == DBNull.Value ? null : reader["queryString"].ToString();

                    list.Add(wechatQueryLog);
                }
                return list;
            }
        }
    }
}
