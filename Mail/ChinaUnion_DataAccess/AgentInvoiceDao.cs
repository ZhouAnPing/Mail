using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChinaUnion_BO;
using MySql.Data.MySqlClient;

namespace ChinaUnion_DataAccess
{
    public class AgentInvoiceDao
    {
        public const string mysqlConnection = DBConstant.mysqlConnection;//"User Id=root;Host=115.29.229.134;Database=chinaunion;password=c513324665;charset=utf8";
        /// <summary> 
        /// 添加数据 
        /// </summary> 
        /// <returns></returns> 
        public int Add(AgentInvoice entity)
        {


            string sql = "INSERT INTO agent_invoice (agentNo,agentName,invoiceMonth,invoiceNo,invoiceDate,invoiceContent,invoiceType,invoiceFee,comment) VALUE (@agentNo,@agentName,@invoiceMonth,@invoiceNo,@invoiceDate,@invoiceContent,@invoiceType,@invoiceFee,@comment)";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@invoiceMonth", entity.invoiceMonth);
                command.Parameters.AddWithValue("@invoiceNo", entity.invoiceNo);
                command.Parameters.AddWithValue("@invoiceDate", entity.invoiceDate);
                command.Parameters.AddWithValue("@invoiceContent", entity.invoiceContent);
                command.Parameters.AddWithValue("@invoiceType", entity.invoiceType);
                command.Parameters.AddWithValue("@invoiceFee", entity.invoiceFee);
                command.Parameters.AddWithValue("@comment", entity.comment);
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
        public int Update(AgentInvoice entity)
        {
            string sql = "UPDATE  agent_invoice SET agentNo=@agentNo,agentName=@agentName,invoiceMonth=@invoiceMonth,";
            sql = sql + "invoiceNo=@invoiceNo,invoiceDate=@invoiceDate,invoiceContent=@invoiceContent,invoiceType=@invoiceType,";
            sql = sql + "invoiceFee=@invoiceFee ,comment=@comment where  agentNo=@agentNo and invoiceMonth=@invoiceMonth and invoiceNo=@invoiceNo";

            //string sql = "UPDATE cimuser SET userNickName=@userNickName WHERE userid=@userid";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);
                command.Parameters.AddWithValue("@agentName", entity.agentName);
                command.Parameters.AddWithValue("@invoiceMonth", entity.invoiceMonth);
                command.Parameters.AddWithValue("@invoiceNo", entity.invoiceNo);
                command.Parameters.AddWithValue("@invoiceDate", entity.invoiceDate);
                command.Parameters.AddWithValue("@invoiceContent", entity.invoiceContent);
                command.Parameters.AddWithValue("@invoiceType", entity.invoiceType);
                command.Parameters.AddWithValue("@invoiceFee", entity.invoiceFee);
                command.Parameters.AddWithValue("@comment", entity.comment);
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
        public int Delete(AgentInvoice entity)
        {
            string sql = "DELETE FROM agent_invoice where  agentNo=@agentNo and invoiceMonth=@invoiceMonth and invoiceNo=@invoiceNo";

            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);

                command.Parameters.AddWithValue("@invoiceMonth", entity.invoiceMonth);
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
        public AgentInvoice GetByKey(AgentInvoice entity)
        {
            string sql = "SELECT agentNo,agentName,invoiceMonth,invoiceNo,invoiceDate,invoiceContent,invoiceType,invoiceFee,comment FROM agent_invoice where  agentNo=@agentNo and invoiceMonth=@invoiceMonth and invoiceNo=@invoiceNo";
            using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                command.Parameters.AddWithValue("@agentNo", entity.agentNo);

                command.Parameters.AddWithValue("@invoiceMonth", entity.invoiceMonth);
                command.Parameters.AddWithValue("@invoiceNo", entity.invoiceNo);
                MySqlDataReader reader = command.ExecuteReader();
                AgentInvoice agentInvoice = null;
                if (reader.Read())
                {
                    agentInvoice = new AgentInvoice();
                    agentInvoice.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentInvoice.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentInvoice.invoiceMonth = reader["invoiceMonth"] == DBNull.Value ? null : reader["invoiceMonth"].ToString();
                    agentInvoice.invoiceNo = reader["invoiceNo"] == DBNull.Value ? null : reader["invoiceNo"].ToString();
                    agentInvoice.invoiceDate = reader["invoiceDate"] == DBNull.Value ? null : reader["invoiceDate"].ToString();
                    agentInvoice.invoiceContent = reader["invoiceContent"] == DBNull.Value ? null : reader["invoiceContent"].ToString();
                    agentInvoice.invoiceType = reader["invoiceType"] == DBNull.Value ? null : reader["invoiceType"].ToString();
                    agentInvoice.invoiceFee = reader["invoiceFee"] == DBNull.Value ? null : reader["invoiceFee"].ToString();
                    agentInvoice.comment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString();
                }
                mycn.Close();
                return agentInvoice;
            }
        }
        /// <summary> 
        /// 查询集合 
        /// </summary> 
        /// <returns></returns> 
        public IList<AgentInvoice> GetList(String agentNo, String agentName, String invoiceMonth)
        {
            string sql = "SELECT agentNo,agentName,invoiceMonth,invoiceNo,invoiceDate,invoiceContent,invoiceType,invoiceFee,comment FROM agent_invoice where 1=1 ";
            if (!String.IsNullOrEmpty(agentNo))
            {
                sql = sql + " and  (agentNo like \"%" + agentNo + "%\")";
            }
            if (!String.IsNullOrEmpty(agentName))
            {
                sql = sql + " and  (agentName like \"%" + agentName + "%\")";
            }
            if (!String.IsNullOrEmpty(invoiceMonth))
            {
                sql = sql + " and  invoiceMonth='" + invoiceMonth + "'";
            }
            sql = sql + " order by invoiceDate asc";
           using (MySqlConnection mycn = new MySqlConnection(mysqlConnection))
            {
                mycn.Open();
                MySqlCommand command = new MySqlCommand(sql, mycn);
                
                MySqlDataReader reader = command.ExecuteReader();
                IList<AgentInvoice> list = new List<AgentInvoice>();
                AgentInvoice agentInvoice = null;
                while (reader.Read()){
                    agentInvoice = new AgentInvoice();
                    agentInvoice.agentNo = reader["agentNo"] == DBNull.Value ? null : reader["agentNo"].ToString();
                    agentInvoice.agentName = reader["agentName"] == DBNull.Value ? null : reader["agentName"].ToString();
                    agentInvoice.invoiceMonth = reader["invoiceMonth"] == DBNull.Value ? null : reader["invoiceMonth"].ToString();
                    agentInvoice.invoiceNo = reader["invoiceNo"] == DBNull.Value ? null : reader["invoiceNo"].ToString();
                    agentInvoice.invoiceDate = reader["invoiceDate"] == DBNull.Value ? null : reader["invoiceDate"].ToString();
                    agentInvoice.invoiceContent = reader["invoiceContent"] == DBNull.Value ? null : reader["invoiceContent"].ToString();
                    agentInvoice.invoiceType = reader["invoiceType"] == DBNull.Value ? null : reader["invoiceType"].ToString();
                    agentInvoice.invoiceFee = reader["invoiceFee"] == DBNull.Value ? null : reader["invoiceFee"].ToString();
                    agentInvoice.comment = reader["comment"] == DBNull.Value ? null : reader["comment"].ToString();
                    list.Add(agentInvoice);
                }
                mycn.Close();
                 return list;
            }
        }
    }
}