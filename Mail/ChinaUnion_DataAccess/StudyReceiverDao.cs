using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class StudyReceiverDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(StudyReceiver entity)
        {
            string sql = "INSERT INTO tb_study_receiver (study_sequence,receiver,type) VALUE (@study_sequence,@receiver,@type)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_sequence", entity.studySequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
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
        public int Update(StudyReceiver entity)
        {
            string sql = "UPDATE  tb_study_receiver SET study_sequence=@study_sequence,receiver=@receiver where study_sequence=@study_sequence  and type=@type";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_sequence", entity.studySequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(String studySequence)
        {
            string sql = "DELETE FROM tb_study_receiver WHERE study_sequence=@study_sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_sequence", studySequence);

                int i = command.ExecuteNonQuery();
                mycn.Close();
                mycn.Dispose();
                return i;
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(StudyReceiver entity)
        {
            string sql = "DELETE FROM tb_study_receiver WHERE study_sequence=@study_sequence and receiver=@receiver and type=@type";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_sequence", entity.studySequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
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
        //public Agent Get(int primaryKey)
        //{
        //    string sql = "SELECT userid,userNickName FROM cimuser where userid=@userid";
        //    using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
        //    {
        //        mycn.Open();
        //        MySqlCommand command = new MySqlCommand(sql, mycn);
        //        command.Parameters.AddWithValue("@userid", primaryKey);
        //        MySqlDataReader reader = command.ExecuteReader();
        //        Agent userBase = null;
        //        if (reader.Read())
        //        {
        //            userBase = new Agent();
        //            userBase.UserId = Convert.ToInt32(reader["userid"]);
        //            userBase.UserNickName = reader["userNickName"] == DBNull.Value ? null : reader["userNickName"].ToString();
        //        }
        //        return userBase;
        //    }
        //}
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<StudyReceiver> GetList(String studySequence)
        {
            string sql = "SELECT study_sequence,receiver,type FROM tb_study_receiver where study_Sequence=@study_Sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@study_Sequence", studySequence);
                MySqlDataReader reader = command.ExecuteReader();
                IList<StudyReceiver> list = new List<StudyReceiver>();
                StudyReceiver studyReceiver = null;
                while (reader.Read())
                {
                    studyReceiver = new StudyReceiver();

                    studyReceiver.studySequence = reader["study_sequence"] == DBNull.Value ? null : reader["study_sequence"].ToString();
                    studyReceiver.receiver = reader["receiver"] == DBNull.Value ? null : reader["receiver"].ToString();
                    studyReceiver.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();

                    list.Add(studyReceiver);
                }
                mycn.Close();
                return list;
            }
        }


       
    }
}
