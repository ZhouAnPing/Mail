
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


            string sql = "INSERT INTO tb_policy (agentType,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime,validateEndTime, isValidate, isDelete, deleteTime,toAll) VALUE (@agentType,@subject,@content,@sender,@attachment,@attachmentName,@creatTime,@type, @validateStartTime,@validateEndTime, @isValidate, @isDelete, @deleteTime,@toAll)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentType", entity.agentType);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@sender", entity.sender);
                command.Parameters.AddWithValue("@attachment", entity.attachment);
                command.Parameters.AddWithValue("@attachmentName", entity.attachmentName);
                command.Parameters.AddWithValue("@creatTime", entity.creatTime);
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@validateStartTime", entity.validateStartTime);
                command.Parameters.AddWithValue("@validateEndTime", entity.validateEndTime);
                command.Parameters.AddWithValue("@isValidate", entity.isValidate);
                command.Parameters.AddWithValue("@isDelete", entity.isDelete);
                command.Parameters.AddWithValue("@deleteTime", entity.deleteTime);
                command.Parameters.AddWithValue("@toAll", entity.toAll);
               
               
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
            string sql = "UPDATE  tb_policy SET agentType=@agentType,subject=@subject,content=@content,sender=@sender,attachment=@attachment,attachmentName=@attachmentName,creatTime=@creatTime,";
            sql = sql + " type=@type,validateStartTime=@validateStartTime,validateEndTime=@validateEndTime,isValidate=@isValidate,isDelete=@isDelete,deleteTime=@deleteTime,toAll=@toAll where sequence=@sequence ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentType", entity.agentType);
                command.Parameters.AddWithValue("@sequence", entity.sequence);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@sender", entity.sender);
                command.Parameters.AddWithValue("@attachment", entity.attachment);
                command.Parameters.AddWithValue("@attachmentName", entity.attachmentName);
                command.Parameters.AddWithValue("@creatTime", entity.creatTime);
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@validateStartTime", entity.validateStartTime);
                 command.Parameters.AddWithValue("@validateEndTime", entity.validateEndTime);
                command.Parameters.AddWithValue("@isValidate", entity.isValidate);
                command.Parameters.AddWithValue("@isDelete", entity.isDelete);
                command.Parameters.AddWithValue("@deleteTime", entity.deleteTime);
                command.Parameters.AddWithValue("@toAll", entity.toAll);
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
        public Policy GetBySubject(String subject)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_policy where subject=@subject";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", subject);
                MySqlDataReader reader = command.ExecuteReader();

                Policy policy = null;
                if (reader.Read())
                {
                    policy = new Policy();
                    policy.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                    policy.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    policy.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                }
                return policy;
            }

        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public Policy Get(int primaryKey)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_policy where sequence=@sequence";
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
                    policy.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                    policy.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    policy.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

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
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_policy";
            sql = sql + " where 1=1 ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((subject like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }
            if (!String.IsNullOrEmpty(type))
            {
                sql = sql + " and type =\"" + type + "\"";
            }
            sql = sql + " order by creatTime desc LIMIT 300";
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
                    policy.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                     policy.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    policy.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

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
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime,validateEndTime, isValidate, isDelete, deleteTime,toAll from tb_policy";

            sql = sql + " where STR_TO_DATE( validateStartTime,'%Y-%m-%d') <= now() and STR_TO_DATE( validateEndTime,'%Y-%m-%d') >= now()  ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((subject like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }

            if (!String.IsNullOrEmpty(type))
            {
                sql = sql + " and type =\"" + type + "\"";
            }

            sql = sql + " order by creatTime desc LIMIT 300";
            
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

                    policy.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                     policy.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    policy.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                    list.Add(policy);
                }
                return list;
            }
        }



        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<String> GetAllAgentNoListBySeq(string sequence)
        {
            StringBuilder sb = new StringBuilder();


            //sb.Append("select distinct   agentNo from    agent_type ");
            //sb.Append("where    agentfeemonth = (select ");
            //sb.Append("max(agentfeemonth)       from ");
            //sb.Append("agent_type)       and agenttype in (SELECT  ");
            //sb.Append("receiver       FROM ");
            //sb.Append(" tb_policy_receiver      where ");
            //sb.Append(" type = '渠道类型' and  policy_sequence = @sequence  ");
            //sb.Append(" union  ");
            //sb.Append("     select  t.member        from  tb_user_define_group t   where ");
            //sb.Append(" t.groupName in (SELECT receiver ");
            //sb.Append("   FROM   tb_policy_receiver where ");
            //sb.Append("       type = '自定义组' and policy_sequence = @sequence) ");
            //sb.Append("    and t.type = '渠道类型')  ");
            //sb.Append("union  ");
            //sb.Append(" select distinct  t.member from   tb_user_define_group t ");
            //sb.Append("where   t.groupName in (SELECT   receiver ");
            //sb.Append("    FROM   tb_policy_receiver ");
            //sb.Append("  where    type = '自定义组' and policy_sequence = @sequence) ");
            //sb.Append(" and t.type = '代理商/渠道' ");


            sb.Append("  select distinct ");
            sb.Append("  agentNo ");
            sb.Append(" from ");
            sb.Append(" agent_type, ");
            sb.Append(" (SELECT  ");
            sb.Append("   receiver ");
            sb.Append("  FROM ");
            sb.Append("   tb_policy_receiver ");
            sb.Append("  where ");
            sb.Append("   type = '渠道类型' ");
            sb.Append("      and policy_sequence = @sequence union select  ");
            sb.Append("  t.member ");
            sb.Append("  from ");
            sb.Append("    tb_user_define_group t, (SELECT  ");
            sb.Append("   receiver ");
            sb.Append("  FROM ");
            sb.Append("      tb_policy_receiver ");
            sb.Append("  where ");
            sb.Append("     type = '自定义组' ");
            sb.Append("        and policy_sequence = @sequence) t2 ");
            sb.Append("  where ");
            sb.Append("     t.groupName = t2.receiver ");
            sb.Append("       and t.type = '渠道类型') t3 ");
            sb.Append(" where ");
            sb.Append("  agentfeemonth = (select  ");
            sb.Append("     max(agentfeemonth) ");
            sb.Append("   from ");
            sb.Append("    agent_type) ");
            sb.Append("  and agenttype = t3.receiver  ");
            sb.Append(" union select distinct ");
            sb.Append("   t.member ");
            sb.Append(" from ");
            sb.Append("  tb_user_define_group t ");
            sb.Append(" where ");
            sb.Append("  t.groupName in (SELECT  ");
            sb.Append("        receiver ");
            sb.Append("    FROM ");
            sb.Append("     tb_policy_receiver ");
            sb.Append("  where ");
            sb.Append("   type = '自定义组' ");
            sb.Append("    and policy_sequence = @sequence) ");
            sb.Append(" and t.type = '代理商/渠道' ");


            String sql = sb.ToString();

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", sequence);
                MySqlDataReader reader = command.ExecuteReader();
                IList<String> list = new List<String>();
                String agentNo = "";
                while (reader.Read())
                {


                    agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    if (String.IsNullOrEmpty(agentNo))
                        continue;
                    list.Add(agentNo);
                }
                return list;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<String> GetAllUserIdListBySeq(string sequence)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select    contactId from ");
            sb.Append(" agent_wechat_account t3, ");
            sb.Append("(select distinct  t1.agentNo ");
            sb.Append("from  agent_type t1, (SELECT  ");
            sb.Append(" receiver as agenttype ");
            sb.Append("FROM    tb_policy_receiver ");
            sb.Append(" where   type = '渠道类型' ");
            sb.Append("  and policy_sequence = @sequence union select  ");
            sb.Append(" t.member as agenttype ");
            sb.Append("from    tb_user_define_group t ");
            sb.Append(" where  t.groupName in (SELECT  receiver ");
            sb.Append(" FROM  tb_policy_receiver ");
            sb.Append("  where type = '自定义组' ");
            sb.Append("   and policy_sequence = @sequence) ");
            sb.Append(" and t.type = '渠道类型') t2 ");
            sb.Append(" where agentfeemonth = (select  ");
            sb.Append(" max(agentfeemonth) ");
            sb.Append(" from   agent_type) ");
            sb.Append(" and t1.agenttype = t2.agenttype union select distinct ");
            sb.Append(" t.member as agentNo ");
            sb.Append(" from  tb_user_define_group t ");
            sb.Append("where  t.groupName in (SELECT  receiver ");
            sb.Append("FROM  tb_policy_receiver ");
            sb.Append(" where   type = '自定义组' ");
            sb.Append(" and policy_sequence = @sequence) ");
            sb.Append("and t.type = '代理商/渠道') t4 ");
            sb.Append("where   (t3.agentNo = t4.agentNo) ");
            sb.Append(" or (t3.branchNo = t4.agentno) ");




            String sql = sb.ToString();

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", sequence);
                MySqlDataReader reader = command.ExecuteReader();
                IList<String> list = new List<String>();
                String contactId = "";
                while (reader.Read())
                {
                    contactId = reader["contactId"] == DBNull.Value ? null : reader["contactId"].ToString();
                    if (String.IsNullOrEmpty(contactId))
                        continue;
                    list.Add(contactId);
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
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_policy";
            sql = sql + " where 1=1 and type!='案例分享' ";
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
                    policy.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                    policy.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    policy.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                    list.Add(policy);
                }
                return list;
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Policy> GetAllList(string keyWord,String type)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_policy";
            sql = sql + " where 1=1 ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((subject like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }
            if (!String.IsNullOrEmpty(type))
            {
                sql = sql + " and type= \"" + type + "\"";
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
                    policy.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    policy.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    policy.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    policy.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    policy.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    policy.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    policy.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    policy.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    policy.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                     policy.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    policy.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    policy.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    policy.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    policy.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    policy.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                    list.Add(policy);
                }
                return list;
            }
        }
    }
}

