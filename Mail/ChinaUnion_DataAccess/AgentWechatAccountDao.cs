
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentWechatAccountDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentWechatAccount entity)
        {
            string sql = "INSERT INTO agent_wechat_account (agentNo,agentName,branchNo,branchName,regionName,contactId,contactName,contactEmail,contactTel,contactWechat,feeRight,policyRight,performanceRight,studyRight,complainRight,monitorRight,errorRight,contactRight,type)";
            sql = sql + " VALUE (@agentNo,@agentName,@branchNo,@branchName,@regionName,@contactId,@contactName,@contactEmail,@contactTel,@contactWechat,@feeRight,@policyRight,@performanceRight,@studyRight,@complainRight,@monitorRight,@errorRight,@contactRight,@type)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@branchNo", entity.branchNo);
                command.Parameters.AddWithValue("@branchName", entity.branchName);
                command.Parameters.AddWithValue("@regionName", entity.regionName);
                command.Parameters.AddWithValue("@contactId", entity.contactId);
                command.Parameters.AddWithValue("@contactName", entity.contactName);
                command.Parameters.AddWithValue("@contactEmail", entity.contactEmail);
                command.Parameters.AddWithValue("@contactTel", entity.contactTel);
                command.Parameters.AddWithValue("@contactWechat", entity.contactWechat);

                command.Parameters.AddWithValue("@feeRight", entity.feeRight);
                command.Parameters.AddWithValue("@policyRight", entity.policyRight);
                command.Parameters.AddWithValue("@performanceRight", entity.performanceRight);
                command.Parameters.AddWithValue("@studyRight", entity.studyRight);
                command.Parameters.AddWithValue("@complainRight", entity.complainRight);
                command.Parameters.AddWithValue("@monitorRight", entity.monitorRight);
                command.Parameters.AddWithValue("@errorRight", entity.errorRight);
                command.Parameters.AddWithValue("@contactRight", entity.contactRight);

                command.Parameters.AddWithValue("@type", entity.type);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String contactId)
        {


            string sql = "DELETE FROM agent_wechat_account where contactId=@contactId";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@contactId", contactId);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String agentNo, String branchNo)
        {
            if (String.IsNullOrEmpty(branchNo))
                branchNo = "";
            string sql = "DELETE FROM agent_wechat_account where agentNo=@agentNo and branchNo=@branchNo";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                command.Parameters.AddWithValue("@branchNo", branchNo);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int DeleteAll()
        {
            string sql = "DELETE FROM agent_wechat_account";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                
                return command.ExecuteNonQuery();
            }
        }
        
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentWechatAccount> GetListByKeyword(String keyword)
        {
            string sql = "SELECT type,agentNo,agentName,branchNo,branchName,regionName,contactId,contactName,contactEmail,contactTel,contactWechat,feeRight,policyRight,performanceRight,studyRight,complainRight,monitorRight,errorRight,contactRight FROM agent_wechat_account";

             sql = sql+" where 1=1";
            if(!String.IsNullOrEmpty(keyword)){
                sql = sql + " and ((agentNo like \"%" + keyword +"%\")";
                sql = sql + " or (agentName like \"%" + keyword + "%\")";
                sql = sql + " or (contactId like \"%" + keyword + "%\")";
                sql = sql + " or (contactName like \"%" + keyword + "%\")";
                sql = sql + " or (contactWechat like \"%" + keyword + "%\")";
                sql = sql + " or (branchNo like \"%" + keyword + "%\")";              
                sql = sql + " or (branchName like \"%" + keyword + "%\"))";
            }
            sql = sql + " order by agentNo asc,branchNo asc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentWechatAccount> list = new List<AgentWechatAccount>();
                AgentWechatAccount agentContact = null;
                while (reader.Read())
                {
                    agentContact = new AgentWechatAccount();
                    agentContact.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();

                    agentContact.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentContact.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentContact.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentContact.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentContact.regionName = reader["regionName"] == DBNull.Value ? null : reader["regionName"].ToString();
                    agentContact.contactId = reader["contactId"] == DBNull.Value ? null : reader["contactId"].ToString();

                    agentContact.contactEmail = reader["contactEmail"] == DBNull.Value ? null : reader["contactEmail"].ToString();
                    agentContact.contactTel = reader["contactTel"] == DBNull.Value ? null : reader["contactTel"].ToString();
                    agentContact.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    agentContact.contactWechat = reader["contactWechat"] == DBNull.Value ? null : reader["contactWechat"].ToString();
                    agentContact.feeRight = reader["feeRight"] == DBNull.Value ? null : reader["feeRight"].ToString();
                    agentContact.policyRight = reader["policyRight"] == DBNull.Value ? null : reader["policyRight"].ToString();
                    agentContact.performanceRight = reader["performanceRight"] == DBNull.Value ? null : reader["performanceRight"].ToString();
                    agentContact.studyRight = reader["studyRight"] == DBNull.Value ? null : reader["studyRight"].ToString();
                    agentContact.complainRight = reader["complainRight"] == DBNull.Value ? null : reader["complainRight"].ToString();
                    agentContact.monitorRight = reader["monitorRight"] == DBNull.Value ? null : reader["monitorRight"].ToString();
                    agentContact.errorRight = reader["errorRight"] == DBNull.Value ? null : reader["errorRight"].ToString();
                    agentContact.contactRight = reader["contactRight"] == DBNull.Value ? null : reader["contactRight"].ToString();

                    list.Add(agentContact);
                }
                return list;
            }
        }
    }
}

