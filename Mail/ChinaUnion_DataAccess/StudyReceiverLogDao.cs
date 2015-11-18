using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class StudyReceiverLogDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(StudyReceiverLog entity)
        {
            string sql = "INSERT INTO tb_study_read_log (study_sequence,userId,readtime) VALUE (@study_sequence,@userId,@readtime)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_sequence", entity.studySequence);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@readtime", entity.readtime);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(StudyReceiverLog entity)
        {
            string sql = "UPDATE  tb_study_read_log SET study_sequence=@study_sequence,userId=@userId where study_sequence=@study_sequence  and readtime=@readtime";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_sequence", entity.studySequence);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@readtime", entity.readtime);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(StudyReceiverLog entity)
        {
            string sql = "DELETE FROM tb_study_read_log WHERE study_sequence=@study_sequence and userId=@userId and readtime=@readtime";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_sequence", entity.studySequence);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@readtime", entity.readtime);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                return i;
            }
        }
        
       
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<StudyReceiverLog> GetList(String studySequence)
        {
            string sql = "SELECT study_sequence,userId,readtime FROM tb_study_read_log where study_Sequence=@study_Sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_Sequence", studySequence);
                MySqlDataReader reader = command.ExecuteReader();
                IList<StudyReceiverLog> list = new List<StudyReceiverLog>();
                StudyReceiverLog studyReceiver = null;
                while (reader.Read())
                {
                    studyReceiver = new StudyReceiverLog();

                    studyReceiver.studySequence = reader["study_sequence"] == DBNull.Value ? null : reader["study_sequence"].ToString();
                    studyReceiver.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    studyReceiver.readtime = reader["readtime"] == DBNull.Value ? null : reader["readtime"].ToString();

                    list.Add(studyReceiver);
                }
                mycn.Close();
                return list;
            }
        }



        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<StudyReceiverLog> GetList(String subject, String userId, String readTime)
        {
            string sql = "SELECT t1.study_sequence,t1.userId,t1.readtime,t2.subject,t2.content,t3.type,t3.agentNo,t3.agentName,t3.branchNo,t3.branchName,t3.regionName,t3.contactId,t3.contactName,t3.contactEmail,t3.contactTel,t3.contactWechat FROM tb_study_read_log t1,tb_study t2,agent_wechat_account t3 ";
            sql = sql + " where  t1.study_sequence = t2.sequence and t1.userId = t3.contactId ";

            if (!String.IsNullOrEmpty(subject))
            {
                sql = sql + " and ((t2.subject like \"%" + subject + "%\")";

                sql = sql + " or (t2.content like \"%" + subject + "%\"))";
            }
            if (!String.IsNullOrEmpty(userId))
            {
                sql = sql + " and ((t3.agentNo like \"%" + userId + "%\")";
                sql = sql + " or (t3.agentName like \"%" + userId + "%\")";
                sql = sql + " or (t3.contactId like \"%" + userId + "%\")";
                sql = sql + " or (t3.contactName like \"%" + userId + "%\")";
                sql = sql + " or (t3.contactWechat like \"%" + userId + "%\")";
                sql = sql + " or (t3.branchNo like \"%" + userId + "%\")";
                sql = sql + " or (t3.branchName like \"%" + userId + "%\"))";
            }
            if (!String.IsNullOrEmpty(readTime))
            {
                sql = sql + " and left(readtime,10)<=\""+ readTime + "\"";

              
            }

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
               
                MySqlDataReader reader = command.ExecuteReader();
                IList<StudyReceiverLog> list = new List<StudyReceiverLog>();
                StudyReceiverLog studyReceiver = null;
                while (reader.Read())
                {
                    studyReceiver = new StudyReceiverLog();

                    studyReceiver.studySequence = reader["study_sequence"] == DBNull.Value ? null : reader["study_sequence"].ToString();
                    studyReceiver.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    studyReceiver.readtime = reader["readtime"] == DBNull.Value ? null : reader["readtime"].ToString();

                    Study study = new Study();
                    study.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    study.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();
                    studyReceiver.study = study;

                    AgentWechatAccount agentContact = new AgentWechatAccount();
                    agentContact.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                  //  agentContact.status = reader["status"] == DBNull.Value ? null : reader["status"].ToString();

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

                    studyReceiver.agentContact = agentContact;
                    list.Add(studyReceiver);
                }

                mycn.Close();
                return list;
            }
        }


    }
}
