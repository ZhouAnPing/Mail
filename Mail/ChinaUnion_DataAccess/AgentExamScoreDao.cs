
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class AgentExamScoreDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentExamScore entity)
        {


            string sql = "INSERT INTO tb_agent_exam_score (userId,exam_sequence,question_sequence,answer)";
            sql = sql + " VALUE (@userId,@exam_sequence,@question_sequence,@answer)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@userId", entity.userId);
                command.Parameters.AddWithValue("@exam_sequence", entity.exam_sequence);
                command.Parameters.AddWithValue("@question_sequence", entity.question_sequence);
                command.Parameters.AddWithValue("@answer", entity.answer);
                
               
                return command.ExecuteNonQuery();
            }
        }
       
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String examSeq, String userId, String question_sequence)
        {
            string sql = "DELETE FROM tb_agent_exam_score WHERE exam_sequence=@examSeq and userId=@userId and question_sequence=@question_sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@examSeq", examSeq);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@question_sequence", question_sequence);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public Exam GetByName(String subject)
        {
            string sql = "SELECT sequence,subject,sender,creatTime,type,validateStartTime,validateEndTime,isValidate,toAll,agentType,duration FROM tb_exam WHERE subject=@subject";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", subject);
                MySqlDataReader reader = command.ExecuteReader();

                Exam exam = null;
                if (reader.Read())
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

                }
                return exam;
            }

        }

        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public Exam Get(String primaryKey)
        {
            string sql = "SELECT sequence,subject,sender,creatTime,type,validateStartTime,validateEndTime,isValidate,toAll,agentType,duration FROM tb_exam WHERE sequence=@sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@sequence", primaryKey);
                MySqlDataReader reader = command.ExecuteReader();

                Exam exam = null;
                if (reader.Read())
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

                }
                return exam;
            }
               
        }

        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<ExamQuestion> GetUserExamQuestion(String exam_sequence, String userId)
        {
            string sql = "SELECT t1.sequence,t1.exam_sequence,t1.questionType,t1.answer as standardAnswer,t2.answer as userAnswer,question,option1,option2,option3,option4,option5,option6,option7,option8 FROM tb_exam_question t1 left join tb_agent_exam_score t2 ";
            sql = sql + " on t1.sequence = t2.question_sequence and t1.exam_sequence=t2.exam_sequence and t1.exam_sequence=@exam_sequence and t2.userId = @userId";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_sequence", exam_sequence);
                command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = command.ExecuteReader();
                IList<ExamQuestion> list = new List<ExamQuestion>();
                ExamQuestion examQuestion = null;
                while (reader.Read())
                {
                    examQuestion = new ExamQuestion();
                    examQuestion.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    examQuestion.exam_sequence = reader["exam_sequence"] == DBNull.Value ? null : reader["exam_sequence"].ToString();
                    examQuestion.questionType = reader["questionType"] == DBNull.Value ? null : reader["questionType"].ToString();
                    examQuestion.answer = reader["userAnswer"] == DBNull.Value ? null : reader["userAnswer"].ToString();
                    examQuestion.standardAnswer = reader["standardAnswer"] == DBNull.Value ? null : reader["standardAnswer"].ToString();
                    examQuestion.question = reader["question"] == DBNull.Value ? null : reader["question"].ToString();
                    examQuestion.option1 = reader["option1"] == DBNull.Value ? null : reader["option1"].ToString();
                    examQuestion.option2 = reader["option2"] == DBNull.Value ? null : reader["option2"].ToString();
                    examQuestion.option3 = reader["option3"] == DBNull.Value ? null : reader["option3"].ToString();
                    examQuestion.option4 = reader["option4"] == DBNull.Value ? null : reader["option4"].ToString();
                    examQuestion.option5 = reader["option5"] == DBNull.Value ? null : reader["option5"].ToString();
                    examQuestion.option6 = reader["option6"] == DBNull.Value ? null : reader["option6"].ToString();
                    examQuestion.option7 = reader["option7"] == DBNull.Value ? null : reader["option7"].ToString();
                    examQuestion.option8 = reader["option8"] == DBNull.Value ? null : reader["option8"].ToString();
                    list.Add(examQuestion);
                }
                return list;
            }
        }


        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<Exam> GetList(String keyword)
        {
            string sql = "SELECT sequence, subject,sender,creatTime,type,validateStartTime,validateEndTime,isValidate,toAll,agentType,duration FROM tb_exam WHERE 1=1";
            if (!String.IsNullOrEmpty(keyword))
            {
                sql = sql + " subject like '%" + keyword+"%'";
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
                    list.Add(exam);
                }
                return list;
            }
        }
    }
}

