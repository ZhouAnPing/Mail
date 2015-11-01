
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentExamDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentExam entity)
        {


            string sql = "INSERT INTO tb_agent_exam (userId,exam_sequence,duration,status,scoreSummary)";
            sql = sql + " VALUE (@userId,@exam_sequence,@duration,@status,@scoreSummary)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@exam_sequence", entity.exam_sequence);
                command.Parameters.AddWithValue("@duration", entity.duration);
                command.Parameters.AddWithValue("@status", entity.status);
                command.Parameters.AddWithValue("@scoreSummary", entity.scoreSummary);
                
               
                return command.ExecuteNonQuery();
            }
        }
       
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String examSeq, String userId)
        {
            string sql = "DELETE FROM tb_agent_exam WHERE exam_sequence=@examSeq and userId=@userId ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@examSeq", examSeq);
                command.Parameters.AddWithValue("@userId", userId);
               
                return command.ExecuteNonQuery();
            }
        }


        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int update(String examSeq, String userId,String status)
        {
            string sql = "update tb_agent_exam set status=@status WHERE exam_sequence=@examSeq and userId=@userId ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@examSeq", examSeq);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@status", status);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public AgentExam Get(String examSeq, String userId)
        {
            string sql = "SELECT exam_sequence,userId,status,scoreSummary,duration from tb_agent_exam WHERE exam_sequence=@examSeq and userId=@userId";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@examSeq", examSeq);
                command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = command.ExecuteReader();

                AgentExam exam = null;
                if (reader.Read())
                {
                    exam = new AgentExam();
                    exam.exam_sequence = reader["exam_sequence"] == DBNull.Value ? null : reader["exam_sequence"].ToString();
                    exam.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    exam.status = reader["status"] == DBNull.Value ? null : reader["status"].ToString();
                    exam.scoreSummary = reader["scoreSummary"] == DBNull.Value ? null : reader["scoreSummary"].ToString();
                    exam.duration = reader["duration"] == DBNull.Value ? null : reader["duration"].ToString();
                    

                }
                return exam;
            }

        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Exam> GetList(String keyword, String userId,string messageType)
        {
            string sql = "SELECT sequence, subject,sender,creatTime,type,validateStartTime,validateEndTime,isValidate,toAll,agentType,t1.duration, status FROM tb_exam t1 left join tb_agent_exam t2 on sequence = exam_sequence ";
                sql = sql+ " and userId='" + userId+"'";
            sql= sql+ " WHERE 1=1";
            if (!String.IsNullOrEmpty(keyword))
            {
                sql = sql + " and subject like '%" + keyword + "%'";
            }
            if (!String.IsNullOrEmpty(messageType))
            {
                sql = sql + " and type = '" + messageType + "'";
            }
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                MySqlDataReader reader = command.ExecuteReader();
                IList<Exam> list = new List<Exam>();
                Exam exam = null;
                while (reader.Read())
                {
                    exam = new Exam();
                    exam.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    exam.subject = reader["subject"] == DBNull.Value ? null : reader["subject"].ToString();
                    exam.creatTime = reader["creatTime"] == DBNull.Value ? null : reader["creatTime"].ToString();
                    exam.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();
                    exam.validateStartTime = reader["validateStartTime"] == DBNull.Value ? null : reader["validateStartTime"].ToString();
                    exam.validateEndTime = reader["validateEndTime"] == DBNull.Value ? null : reader["validateEndTime"].ToString();
                    exam.isValidate = reader["isValidate"] == DBNull.Value ? null : reader["isValidate"].ToString();
                    exam.toAll = reader["toAll"] == DBNull.Value ? null : reader["toAll"].ToString();
                    exam.agentType = reader["agentType"] == DBNull.Value ? null : reader["agentType"].ToString();
                    exam.duration = reader["duration"] == DBNull.Value ? null : reader["duration"].ToString();

                    exam.status = reader["status"] == DBNull.Value ? null : reader["status"].ToString();
                    list.Add(exam);
                }
                return list;
            }
        }


        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public IList<AgentExam> GetList(String examSeq)
        {
            string sql = "SELECT exam_sequence,userId,status,scoreSummary,duration from tb_agent_exam WHERE exam_sequence=@examSeq ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@examSeq", examSeq);
                //command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentExam> list = new List<AgentExam>();
                AgentExam exam = null;
                while (reader.Read())
                {
                    exam = new AgentExam();
                    exam.exam_sequence = reader["exam_sequence"] == DBNull.Value ? null : reader["exam_sequence"].ToString();
                    exam.userId = reader["userId"] == DBNull.Value ? null : reader["userId"].ToString();
                    exam.status = reader["status"] == DBNull.Value ? null : reader["status"].ToString();
                    exam.scoreSummary = reader["scoreSummary"] == DBNull.Value ? null : reader["scoreSummary"].ToString();
                    exam.duration = reader["duration"] == DBNull.Value ? null : reader["duration"].ToString();

                    list.Add(exam);
                }
                return list;
            }

        }

      
    }
}

