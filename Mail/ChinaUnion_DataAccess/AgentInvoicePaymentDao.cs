using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChinaUnion_BO;
using MySql.Data.MySqlClient;

namespace ChinaUnion_DataAccess
{
    public class AgentInvoicePaymentDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentInvoicePayment entity)
        {


            string sql = "INSERT INTO agent_invoice_payment (agentNo,agentName,processTime,invoiceFee,payFee,summary,payStatus) VALUE (@agentNo,@agentName,@processTime,@invoiceFee,@payFee,@summary,@payStatus)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@processTime", entity.processTime);
                command.Parameters.AddWithValue("@invoiceFee", entity.invoiceFee);
                command.Parameters.AddWithValue("@payFee", entity.payFee);
                command.Parameters.AddWithValue("@summary", entity.summary);
                command.Parameters.AddWithValue("@payStatus", entity.payStatus);
               
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 修改数据 
        /// </summary> 
        /// <param name="entity"></param> 
        /// <returns></returns> 
        public int Update(AgentInvoicePayment entity)
        {
            string sql = "UPDATE  agent_invoice_payment SET agentNo=@agentNo,agentName=@agentName,processTime=@processTime,";
            sql = sql + "invoiceFee=@invoiceFee,payFee=@payFee,summary=@summary,payStatus=@payStatus";
            sql = sql + "  where  agentNo=@agentNo and processTime=@processTime ";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@processTime", entity.processTime);
                command.Parameters.AddWithValue("@invoiceFee", entity.invoiceFee);
                command.Parameters.AddWithValue("@payFee", entity.payFee);
                command.Parameters.AddWithValue("@summary", entity.summary);
                command.Parameters.AddWithValue("@payStatus", entity.payStatus);
                return command.ExecuteNonQuery();
            }
        }


        /// <summary> 
        /// 删除数据 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public int Delete(AgentInvoicePayment entity)
        {
            string sql = "DELETE FROM agent_invoice_payment where agentNo=@agentNo and processTime=@processTime ";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
               
                command.Parameters.AddWithValue("@processTime", entity.processTime);
                return command.ExecuteNonQuery();
            }
        }
        /// <summary> 
        /// 根据主键查询 
        /// </summary> 
        /// <param name="primaryKey"></param> 
        /// <returns></returns> 
        public AgentInvoicePayment GetByKey(AgentInvoicePayment entity)
        {
            string sql = "SELECT agentNo,agentName,processTime,invoiceFee,payFee,summary,payStatus FROM agent_invoice_payment where  agentNo=@agentNo and processTime=@processTime ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);

                command.Parameters.AddWithValue("@processTime", entity.processTime);
                MySqlDataReader reader = command.ExecuteReader();
                AgentInvoicePayment agentInvoicePayment = null;
                if (reader.Read())
                {
                    agentInvoicePayment = new AgentInvoicePayment();
                    agentInvoicePayment.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentInvoicePayment.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentInvoicePayment.processTime = reader["processTime"] == DBNull.Value ? null : reader["processTime"].ToString();
                    agentInvoicePayment.invoiceFee = reader["invoiceFee"] == DBNull.Value ? null : reader["invoiceFee"].ToString();
                    agentInvoicePayment.payFee = reader["payFee"] == DBNull.Value ? null : reader["payFee"].ToString();
                    agentInvoicePayment.summary = reader["summary"] == DBNull.Value ? null : reader["summary"].ToString();
                    agentInvoicePayment.payStatus = reader["payStatus"] == DBNull.Value ? null : reader["payStatus"].ToString();
                    
                }
                return agentInvoicePayment;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentInvoicePayment> GetList(String agentNo,String agentName,String processMonth)
        {
            string sql = "SELECT agentNo,agentName,processTime,invoiceFee,payFee,summary,payStatus FROM agent_invoice_payment  where 1=1 ";
            if (!String.IsNullOrEmpty(agentNo))
            {
                sql = sql + "  and (agentNo like \"%" + agentNo + "%\")";
            }
            if (!String.IsNullOrEmpty(agentName))
            {
                sql = sql + "  and (agentName like \"%" + agentName + "%\")";
            }
            if (!String.IsNullOrEmpty(processMonth))
            {
                sql = sql + "  and ( date_format(str_to_date(processtime, '%Y-%m-%d'),'%Y%m') = '" + processMonth + "')";
            }
            sql = sql + " order by processtime asc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentInvoicePayment> list = new List<AgentInvoicePayment>();
                AgentInvoicePayment agentInvoicePayment = null;
                while (reader.Read())
                {
                    agentInvoicePayment = new AgentInvoicePayment();
                    agentInvoicePayment.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentInvoicePayment.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentInvoicePayment.processTime = reader["processTime"] == DBNull.Value ? null : reader["processTime"].ToString();
                    agentInvoicePayment.invoiceFee = reader["invoiceFee"] == DBNull.Value ? null : reader["invoiceFee"].ToString();
                    agentInvoicePayment.payFee = reader["payFee"] == DBNull.Value ? null : reader["payFee"].ToString();
                    agentInvoicePayment.summary = reader["summary"] == DBNull.Value ? null : reader["summary"].ToString();
                    agentInvoicePayment.payStatus = reader["payStatus"] == DBNull.Value ? null : reader["payStatus"].ToString();
                    list.Add(agentInvoicePayment);
                }
                return list;
            }
        }
    }
}