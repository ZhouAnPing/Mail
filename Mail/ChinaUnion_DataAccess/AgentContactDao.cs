
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentContactDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentContact entity)
        {


            string sql = "INSERT INTO agent_Contact (agentNo,agentName,branchNo,branchName,area,zone,contactTel,contactName,contactEmail) VALUE (@agentNo,@agentName,@branchNo,@branchName,@area,@zone,@contactTel,@contactName,@contactEmail)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@branchNo", entity.branchNo);
                command.Parameters.AddWithValue("@branchName", entity.branchName);
                command.Parameters.AddWithValue("@area", entity.area);
                command.Parameters.AddWithValue("@zone", entity.zone);
                command.Parameters.AddWithValue("@contactTel", entity.contactTel);
                command.Parameters.AddWithValue("@contactName", entity.contactName);
                command.Parameters.AddWithValue("@contactEmail", entity.contactEmail);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String agentNo, String branchNo, String contactName)
        {
            string sql = "DELETE FROM agent_contact where agentNo=@agentNo and branchNo=@branchNo and contactName=@contactName";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", agentNo);
                command.Parameters.AddWithValue("@branchNo", branchNo);
                command.Parameters.AddWithValue("@contactName", contactName);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int DeleteAll()
        {
            string sql = "DELETE FROM agent_contact";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }
        
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentContact> GetListByKeyword(String keyword)
        {
            string sql = "SELECT agentNo,agentName,branchNo,branchName,area,zone,contactTel,contactName,contactEmail FROM agent_contact";

             sql = sql+" where 1=1";
            if(!String.IsNullOrEmpty(keyword)){
                sql = sql + " and ((agentNo like \"%" + keyword +"%\")";
                sql = sql + " or (agentName like \"%" + keyword + "%\")";
                sql = sql + " or (branchNo like \"%" + keyword + "%\")";
                sql = sql + " or (branchName like \"%" + keyword + "%\"))";
            }
            sql = sql + " order by agentNo asc,branchNo asc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentContact> list = new List<AgentContact>();
                AgentContact agentContact = null;
                while (reader.Read())
                {
                    agentContact = new AgentContact();
                    
                    agentContact.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentContact.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentContact.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentContact.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentContact.area = reader["area"] == DBNull.Value ? null : reader["area"].ToString();
                    agentContact.zone = reader["zone"] == DBNull.Value ? null : reader["zone"].ToString();
                    agentContact.contactTel = reader["contactTel"] == DBNull.Value ? null : reader["contactTel"].ToString();
                    agentContact.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    agentContact.contactEmail = reader["contactEmail"] == DBNull.Value ? null : reader["contactEmail"].ToString();

                    list.Add(agentContact);
                }
                mycn.Close();
                return list;
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentContact> GetListByNo(String StringKey)
        {
            string sql = "SELECT agentNo,agentName,branchNo,branchName,area,zone,contactTel,contactName,contactEmail FROM agent_contact";

            sql = sql + " where 1=1";
            if (!String.IsNullOrEmpty(StringKey))
            {
                sql = sql + " and ((agentNo = \"" + StringKey + "\")";
                sql = sql + " or (contactName = \"" + StringKey + "\")";   
                sql = sql + " or (branchNo = \"" + StringKey + "\"))";                
            }
            sql = sql + " order by agentNo asc,branchNo asc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentContact> list = new List<AgentContact>();
                AgentContact agentContact = null;
                while (reader.Read())
                {
                    agentContact = new AgentContact();

                    agentContact.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentContact.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentContact.branchNo = reader["branchNo"] == DBNull.Value ? null : reader["branchNo"].ToString();
                    agentContact.branchName = reader["branchName"] == DBNull.Value ? null : reader["branchName"].ToString();
                    agentContact.area = reader["area"] == DBNull.Value ? null : reader["area"].ToString();
                    agentContact.zone = reader["zone"] == DBNull.Value ? null : reader["zone"].ToString();
                    agentContact.contactTel = reader["contactTel"] == DBNull.Value ? null : reader["contactTel"].ToString();
                    agentContact.contactName = reader["contactName"] == DBNull.Value ? null : reader["contactName"].ToString();
                    agentContact.contactEmail = reader["contactEmail"] == DBNull.Value ? null : reader["contactEmail"].ToString();

                    list.Add(agentContact);
                }
                mycn.Close();
                return list;
            }
        }
    }
}

