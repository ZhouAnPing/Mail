
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text;
using MySql.Data.MySqlClient;
using ChinaUnion_BO;
using ChinaUnion_DataAccess.Properties;
namespace ChinaUnion_DataAccess
{
    public class ExamDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(Exam entity)
        {


            string sql = "INSERT INTO tb_exam (subject,sender,creatTime,type,validateStartTime,validateEndTime,isValidate,toAll,agentType,duration)";
            sql = sql + " VALUE (@subject,@sender,@creatTime,@type,@validateStartTime,@validateEndTime,@isValidate,@toAll,@agentType,@duration)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@subject", entity.subject);
                command.Parameters.AddWithValue("@sender", entity.sender);
                command.Parameters.AddWithValue("@creatTime", entity.creatTime);
                command.Parameters.AddWithValue("@type", entity.type);
                command.Parameters.AddWithValue("@validateStartTime", entity.validateStartTime);
                command.Parameters.AddWithValue("@validateEndTime", entity.validateEndTime);
                command.Parameters.AddWithValue("@isValidate", entity.isValidate);
                command.Parameters.AddWithValue("@toAll", entity.toAll);
                command.Parameters.AddWithValue("@agentType", entity.agentType);
                command.Parameters.AddWithValue("@duration", entity.duration);
               
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
            string sql = "DELETE FROM tb_exam WHERE sequence=@sequence";
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
        public IList<Exam> GetList(String keyword,String type)
        {
            string sql = "SELECT sequence, subject,sender,creatTime,type,validateStartTime,validateEndTime,isValidate,toAll,agentType,duration FROM tb_exam WHERE 1=1";
            if (!String.IsNullOrEmpty(keyword))
            {
                sql = sql + " and subject like '%" + keyword+"%'";
            }
            if (!String.IsNullOrEmpty(type))
            {
                sql = sql + " and type = '" + type + "'";
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

