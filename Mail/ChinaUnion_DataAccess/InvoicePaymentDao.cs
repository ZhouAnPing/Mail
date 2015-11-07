using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChinaUnion_BO;
using MySql.Data.MySqlClient;

namespace ChinaUnion_DataAccess
{
    public class InvoicePaymentDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(InvoicePayment entity)
        {


            string sql = "INSERT INTO tb_invoice_payment (agentNo,agentName,month,receivedTime,processTime,content,invoiceFee,invoiceType,invoiceNo,payStatus) VALUE (@agentNo,@agentName,@month,@receivedTime,@processTime,@content,@invoiceFee,@invoiceType,@invoiceNo,@payStatus)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@month", entity.month);
                command.Parameters.AddWithValue("@receivedTime", entity.receivedTime);
                command.Parameters.AddWithValue("@processTime", entity.processTime);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@invoiceFee", entity.invoiceFee);
                command.Parameters.AddWithValue("@invoiceType", entity.invoiceType);
                command.Parameters.AddWithValue("@invoiceNo", entity.invoiceNo);
                command.Parameters.AddWithValue("@payStatus", entity.payStatus);

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
        public int Update(InvoicePayment entity)
        {
            string sql = "UPDATE  tb_invoice_payment SET agentNo=@agentNo,agentName=@agentName,month=@month,";
            sql = sql + "receivedTime=@receivedTime,processTime=@processTime,content=@content,invoiceFee=@invoiceFee";
            sql = sql + "invoiceType=@invoiceType,invoiceNo=@invoiceNo,payStatus=@payStatus";
            sql = sql + "  where  agentNo=@agentNo and month=@month and invoiceNo=@invoiceNo";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@month", entity.month);
                command.Parameters.AddWithValue("@receivedTime", entity.receivedTime);
                command.Parameters.AddWithValue("@processTime", entity.processTime);
                command.Parameters.AddWithValue("@content", entity.content);
                command.Parameters.AddWithValue("@invoiceFee", entity.invoiceFee);
                command.Parameters.AddWithValue("@invoiceType", entity.invoiceType);
                command.Parameters.AddWithValue("@invoiceNo", entity.invoiceNo);
                command.Parameters.AddWithValue("@payStatus", entity.payStatus);
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
        public int Delete(InvoicePayment entity)
        {
            string sql = "DELETE FROM tb_invoice_payment where agentNo=@agentNo and month=@month and invoiceNo=@invoiceNo ";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);

                command.Parameters.AddWithValue("@month", entity.month);
                command.Parameters.AddWithValue("@invoiceNo", entity.invoiceNo);
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
        public InvoicePayment GetByKey(InvoicePayment entity)
        {
            string sql = "SELECT agentNo,agentName,month,receivedTime,processTime,content,invoiceFee,invoiceType,invoiceNo,payStatus FROM tb_invoice_payment where agentNo=@agentNo and month=@month and invoiceNo=@invoiceNo ";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);

                command.Parameters.AddWithValue("@month", entity.month);
                command.Parameters.AddWithValue("@invoiceNo", entity.invoiceNo);

                MySqlDataReader reader = command.ExecuteReader();
                InvoicePayment invoicePayment = null;
                if (reader.Read())
                {
                    invoicePayment = new InvoicePayment();
                    invoicePayment.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();
                    invoicePayment.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    invoicePayment.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    invoicePayment.receivedTime = reader["receivedTime"] == DBNull.Value ? null : reader["receivedTime"].ToString();
                   
                    invoicePayment.processTime = reader["processTime"] == DBNull.Value ? null : reader["processTime"].ToString();
                    invoicePayment.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();

                    invoicePayment.invoiceFee = reader["invoiceFee"] == DBNull.Value ? null : reader["invoiceFee"].ToString();

                    invoicePayment.invoiceType = reader["invoiceType"] == DBNull.Value ? null : reader["invoiceType"].ToString();
                    invoicePayment.invoiceNo = reader["invoiceNo"] == DBNull.Value ? null : reader["invoiceNo"].ToString();
                   
                    invoicePayment.payStatus = reader["payStatus"] == DBNull.Value ? null : reader["payStatus"].ToString();
                    
                }
                mycn.Close();
                return invoicePayment;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<InvoicePayment> GetList(String agentNo, String agentName, String processMonth, String invoiceNo)
        {
            string sql = "SELECT agentNo,agentName,month,receivedTime,processTime,content,invoiceFee,invoiceType,invoiceNo,payStatus FROM tb_invoice_payment  where 1=1 ";
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
                sql = sql + "  and (month = '" + processMonth + "')";
            }
            if (!String.IsNullOrEmpty(invoiceNo))
            {
                sql = sql + "  and (invoiceNo like \"%" + invoiceNo + "%\")";
            }
            sql = sql + " order by processtime asc";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);

                MySqlDataReader reader = command.ExecuteReader();
                IList<InvoicePayment> list = new List<InvoicePayment>();
                InvoicePayment invoicePayment = null;
                while (reader.Read())
                {
                    invoicePayment = new InvoicePayment();
                    invoicePayment.month = reader["month"] == DBNull.Value ? null : reader["month"].ToString();
                    invoicePayment.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    invoicePayment.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    invoicePayment.receivedTime = reader["receivedTime"] == DBNull.Value ? null : reader["receivedTime"].ToString();

                    invoicePayment.processTime = reader["processTime"] == DBNull.Value ? null : reader["processTime"].ToString();
                    invoicePayment.content = reader["content"] == DBNull.Value ? null : reader["content"].ToString();

                    invoicePayment.invoiceFee = reader["invoiceFee"] == DBNull.Value ? null : reader["invoiceFee"].ToString();

                    invoicePayment.invoiceType = reader["invoiceType"] == DBNull.Value ? null : reader["invoiceType"].ToString();
                    invoicePayment.invoiceNo = reader["invoiceNo"] == DBNull.Value ? null : reader["invoiceNo"].ToString();

                    invoicePayment.payStatus = reader["payStatus"] == DBNull.Value ? null : reader["payStatus"].ToString();
                    list.Add(invoicePayment);
                }
                mycn.Close();
                return list;
            }
        }
    }
}