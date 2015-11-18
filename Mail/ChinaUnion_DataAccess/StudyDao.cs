
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class StudyDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(Study entity)
        {


            string sql = "INSERT INTO tb_study (agentType,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime,validateEndTime, isValidate, isDelete, deleteTime,toAll) VALUE (@agentType,@subject,@content,@sender,@attachment,@attachmentName,@creatTime,@type, @validateStartTime,@validateEndTime, @isValidate, @isDelete, @deleteTime,@toAll)";
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
        public int Update(Study entity)
        {
            string sql = "UPDATE  tb_study SET agentType=@agentType,subject=@subject,content=@content,sender=@sender,attachment=@attachment,attachmentName=@attachmentName,creatTime=@creatTime,";
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
        public int Delete(int primaryKey)
        {
            string sql = "DELETE FROM tb_study WHERE sequence=@sequence ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", primaryKey);
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
        public Study GetBySubject(String subject)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_study where subject=@subject";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", subject);
                MySqlDataReader reader = command.ExecuteReader();

                Study study = null;
                if (reader.Read())
                {
                    study = new Study();
                    study.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    study.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    study.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    study.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    study.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    study.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    study.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    study.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    study.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                    study.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    study.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    study.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    study.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    study.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    study.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                }
                mycn.Close();
                return study;
            }

        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public Study Get(int primaryKey)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_study where sequence=@sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", primaryKey);
                MySqlDataReader reader = command.ExecuteReader();

                Study study = null;
                if (reader.Read())
                {
                    study = new Study();
                    study.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    study.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    study.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    study.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    study.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    study.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    study.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    study.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    study.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                    study.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    study.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    study.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    study.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    study.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    study.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                }
                mycn.Close();
                return study;
            }
               
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Study> GetList(string keyWord, String type)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_study";
            sql = sql + " where 1=1 ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((subject like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }
            if (!String.IsNullOrEmpty(type))
            {
                sql = sql + " and type =\"" + type + "\"";
            }
            sql = sql + " order by validateEndTime asc,validateStartTime asc LIMIT 300";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<Study> list = new List<Study>();
                Study study = null;
                while (reader.Read())
                {
                    study = new Study();
                    study.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    study.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    study.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    study.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    study.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    study.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    study.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    study.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    study.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                     study.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    study.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    study.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    study.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    study.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    study.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                    list.Add(study);
                }
                mycn.Close();
                return list;
            }
        }


         /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Study> GetAllValidatedList(string keyWord, String type)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime,validateEndTime, isValidate, isDelete, deleteTime,toAll from tb_study";

            sql = sql + " where STR_TO_DATE( validateStartTime,'%Y-%m-%d') <= now() and STR_TO_DATE( validateEndTime,'%Y-%m-%d') >= now()  ";
            if (!String.IsNullOrEmpty(keyWord))
            {
                sql = sql + " and ((subject like \"%" + keyWord + "%\") or (content like \"%" + keyWord + "%\"))";
            }

            if (!String.IsNullOrEmpty(type))
            {
                sql = sql + " and type =\"" + type + "\"";
            }

            sql = sql + " order by validateEndTime asc,validateStartTime asc LIMIT 300";
            
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<Study> list = new List<Study>();
                Study study = null;
                while (reader.Read())
                {
                    study = new Study();

                    study.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    study.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    study.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    study.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    study.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    study.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    study.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    study.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    study.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                     study.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    study.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    study.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    study.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    study.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    study.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                    list.Add(study);
                }
                mycn.Close();
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
            //sb.Append(" tb_study_receiver      where ");
            //sb.Append(" type = '渠道类型' and  study_sequence = @sequence  ");
            //sb.Append(" union  ");
            //sb.Append("     select  t.member        from  tb_user_define_group t   where ");
            //sb.Append(" t.groupName in (SELECT receiver ");
            //sb.Append("   FROM   tb_study_receiver where ");
            //sb.Append("       type = '自定义组' and study_sequence = @sequence) ");
            //sb.Append("    and t.type = '渠道类型')  ");
            //sb.Append("union  ");
            //sb.Append(" select distinct  t.member from   tb_user_define_group t ");
            //sb.Append("where   t.groupName in (SELECT   receiver ");
            //sb.Append("    FROM   tb_study_receiver ");
            //sb.Append("  where    type = '自定义组' and study_sequence = @sequence) ");
            //sb.Append(" and t.type = '代理商/渠道' ");


            sb.Append("  select distinct ");
            sb.Append("  agentNo ");
            sb.Append(" from ");
            sb.Append(" agent_type, ");
            sb.Append(" (SELECT  ");
            sb.Append("   receiver ");
            sb.Append("  FROM ");
            sb.Append("   tb_study_receiver ");
            sb.Append("  where ");
            sb.Append("   type = '渠道类型' ");
            sb.Append("      and study_sequence = @sequence union select  ");
            sb.Append("  t.member ");
            sb.Append("  from ");
            sb.Append("    tb_user_define_group t, (SELECT  ");
            sb.Append("   receiver ");
            sb.Append("  FROM ");
            sb.Append("      tb_study_receiver ");
            sb.Append("  where ");
            sb.Append("     type = '自定义组' ");
            sb.Append("        and study_sequence = @sequence) t2 ");
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
            sb.Append("     tb_study_receiver ");
            sb.Append("  where ");
            sb.Append("   type = '自定义组' ");
            sb.Append("    and study_sequence = @sequence) ");
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
                mycn.Close();
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
            sb.Append(" select contactId from agent_wechat_account tt1,");
            sb.Append("  (select distinct ");
            sb.Append("  agentNo ");
            sb.Append(" from ");
            sb.Append(" agent_type, ");
            sb.Append(" (SELECT  ");
            sb.Append("   receiver ");
            sb.Append("  FROM ");
            sb.Append("   tb_study_receiver ");
            sb.Append("  where ");
            sb.Append("   type = '渠道类型' ");
            sb.Append("      and study_sequence = @sequence union select  ");
            sb.Append("  t.member ");
            sb.Append("  from ");
            sb.Append("    tb_user_define_group t, (SELECT  ");
            sb.Append("   receiver ");
            sb.Append("  FROM ");
            sb.Append("      tb_study_receiver ");
            sb.Append("  where ");
            sb.Append("     type = '自定义组' ");
            sb.Append("        and study_sequence = @sequence) t2 ");
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
            sb.Append("     tb_study_receiver ");
            sb.Append("  where ");
            sb.Append("   type = '自定义组' ");
            sb.Append("    and study_sequence = @sequence) ");
            sb.Append(" and t.type = '代理商/渠道' ) tt2");

            sb.Append(" where   ((tt1.agentNo = tt2.agentNo) ");
            sb.Append(" or ( tt1.branchNo = tt2.agentno)) ");




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
                mycn.Close();
                return list;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Study> GetAllList(string keyWord)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_study";
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
                IList<Study> list = new List<Study>();
                Study study = null;
                while (reader.Read())
                {
                    study = new Study();
                    study.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    study.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    study.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    study.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    study.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    study.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    study.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    study.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    study.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                    study.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    study.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    study.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    study.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    study.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    study.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                    list.Add(study);
                }
                mycn.Close();
                return list;
            }
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Study> GetAllList(string keyWord,String type)
        {
            string sql = "SELECT agentType,sequence,subject,content,sender,attachment,attachmentName,creatTime,type, validateStartTime, validateEndTime,isValidate, isDelete, deleteTime,toAll from tb_study";
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
                IList<Study> list = new List<Study>();
                Study study = null;
                while (reader.Read())
                {
                    study = new Study();
                    study.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();

                    study.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    study.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    study.sender = reader["sender"] == DBNull.Value ? null : reader["sender"].ToString();
                    study.attachmentName = reader["attachmentName"] == DBNull.Value ? null : reader["attachmentName"].ToString();
                    study.attachment = reader["attachment"] == DBNull.Value ? null : (byte[])reader["attachment"];
                    study.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    study.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    study.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                     study.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    study.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    study.isDelete = reader["isDelete"] == DBNull.Value ? null : reader["isDelete"].ToString();
                    study.deleteTime = reader["deleteTime"] == DBNull.Value ? null : reader["deleteTime"].ToString();
                    study.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    study.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();

                    list.Add(study);
                }
                mycn.Close();
                return list;
            }
        }
    }
}

