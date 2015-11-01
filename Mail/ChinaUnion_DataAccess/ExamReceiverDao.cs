using ChinaUnion_BO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_DataAccess
{
    public class ExamReceiverDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(ExamReceiver entity)
        {
            string sql = "INSERT INTO tb_exam_receiver (exam_sequence,receiver,type) VALUE (@exam_sequence,@receiver,@type)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_sequence", entity.examSequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(ExamReceiver entity)
        {
            string sql = "UPDATE  tb_exam_receiver SET exam_sequence=@exam_sequence,receiver=@receiver where exam_sequence=@exam_sequence  and type=@type";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_sequence", entity.examSequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(String examSequence)
        {
            string sql = "DELETE FROM tb_exam_receiver WHERE exam_sequence=@exam_sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_sequence", examSequence);
               
                return command.ExecuteNonQuery();
            }
        }

        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Delete(ExamReceiver entity)
        {
            string sql = "DELETE FROM tb_exam_receiver WHERE exam_sequence=@exam_sequence and receiver=@receiver and type=@type";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_sequence", entity.examSequence);
                command.Parameters.AddWithValue("@receiver", entity.receiver);
                command.Parameters.AddWithValue("@type", entity.type);
                return command.ExecuteNonQuery();
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
        public IList<ExamReceiver> GetList(String examSequence)
        {
            string sql = "SELECT exam_sequence,receiver,type FROM tb_exam_receiver where exam_Sequence=@exam_Sequence";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@exam_Sequence", examSequence);
                MySqlDataReader reader = command.ExecuteReader();
                IList<ExamReceiver> list = new List<ExamReceiver>();
                ExamReceiver examReceiver = null;
                while (reader.Read())
                {
                    examReceiver = new ExamReceiver();

                    examReceiver.examSequence = reader["exam_sequence"] == DBNull.Value ? null : reader["exam_sequence"].ToString();
                    examReceiver.receiver = reader["receiver"] == DBNull.Value ? null : reader["receiver"].ToString();
                    examReceiver.type = reader["type"] == DBNull.Value ? null : reader["type"].ToString();

                    list.Add(examReceiver);
                }
                return list;
            }
        }
        public IList<String> GetAllAgentNoListBySeq(string sequence)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  select distinct ");
            sb.Append("  agentNo ");
            sb.Append(" from ");
            sb.Append(" agent_type, ");
            sb.Append(" (SELECT  ");
            sb.Append("   receiver ");
            sb.Append("  FROM ");
            sb.Append("   tb_exam_receiver ");
            sb.Append("  where ");
            sb.Append("   type = '渠道类型' ");
            sb.Append("      and exam_sequence = @sequence union select  ");
            sb.Append("  t.member ");
            sb.Append("  from ");
            sb.Append("    tb_user_define_group t, (SELECT  ");
            sb.Append("   receiver ");
            sb.Append("  FROM ");
            sb.Append("      tb_exam_receiver ");
            sb.Append("  where ");
            sb.Append("     type = '自定义组' ");
            sb.Append("        and exam_sequence = @sequence) t2 ");
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
            sb.Append("     tb_exam_receiver ");
            sb.Append("  where ");
            sb.Append("   type = '自定义组' ");
            sb.Append("    and exam_sequence = @sequence) ");
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

       
    }
}
