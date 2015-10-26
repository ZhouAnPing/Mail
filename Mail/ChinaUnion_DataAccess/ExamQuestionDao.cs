
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class ExamQuestionDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(ExamQuestion entity)
        {


            string sql = "INSERT INTO tb_exam_question (exam_sequence,questionType,answer,question,option1,option2,option3,option4,option5,option6,option7,option8)";
            sql = sql + " VALUE (@exam_sequence,@questionType,@answer,@question,@option1,@option2,@option3,@option4,@option5,@option6,@option7,@option8)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_sequence", entity.exam_sequence);
                command.Parameters.AddWithValue("@questionType", entity.questionType);
                command.Parameters.AddWithValue("@answer", entity.answer);
                command.Parameters.AddWithValue("@question", entity.question);
                command.Parameters.AddWithValue("@option1", entity.option1);
                command.Parameters.AddWithValue("@option2", entity.option2);
                command.Parameters.AddWithValue("@option3", entity.option3);
                command.Parameters.AddWithValue("@option4", entity.option4);
                command.Parameters.AddWithValue("@option5", entity.option5);
                command.Parameters.AddWithValue("@option6", entity.option6);
                command.Parameters.AddWithValue("@option7", entity.option7);
                command.Parameters.AddWithValue("@option8", entity.option8);
               
                return command.ExecuteNonQuery();
            }
        }
       
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(String primaryKey)
        {
            string sql = "DELETE FROM tb_exam_question WHERE exam_sequence=@exam_sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_sequence", primaryKey);
                return command.ExecuteNonQuery();
            }
        }
       
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<ExamQuestion> GetList(String exam_sequence)
        {
            string sql = "SELECT sequence,exam_sequence,questionType,answer,question,option1,option2,option3,option4,option5,option6,option7,option8 FROM tb_exam_question WHERE exam_sequence=@exam_sequence";
            
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_sequence", exam_sequence);
                MySqlDataReader reader = command.ExecuteReader();
                IList<ExamQuestion> list = new List<ExamQuestion>();
                ExamQuestion examQuestion = null;
                while (reader.Read())
                {
                    examQuestion = new ExamQuestion();
                    examQuestion.sequence = reader["sequence"] == DBNull.Value ? null : reader["sequence"].ToString();
                    examQuestion.exam_sequence = reader["exam_sequence"] == DBNull.Value ? null : reader["exam_sequence"].ToString();
                    examQuestion.questionType = reader["questionType"] == DBNull.Value ? null : reader["questionType"].ToString();
                    examQuestion.answer = reader["answer"] == DBNull.Value ? null : reader["answer"].ToString();
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
    }
}

