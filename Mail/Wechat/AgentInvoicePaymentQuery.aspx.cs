using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wechat.Util;

namespace Wechat
{
    public partial class AgentInvoicePaymentQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentInvoicePaymentQuery));
        protected void Page_Load(object sender, EventArgs e)
        {
            string feeMonth = Request.QueryString["feeMonth"];
            string agentNo = Request.QueryString["agentNo"];
            logger.Info("feeMonth=" + Request.QueryString["feeMonth"]);
            logger.Info("agentNo=" + Request.QueryString["agentNo"]);
            try
            {
                Request.ContentEncoding = Encoding.UTF8;
                feeMonth = QueryStringEncryption.Decode(feeMonth, QueryStringEncryption.key);
                agentNo = QueryStringEncryption.Decode(agentNo, QueryStringEncryption.key);
                logger.Info("feeMonth=" + feeMonth);
                logger.Info("agentNo=" + agentNo);
            }
            catch (Exception)
            {
               // return;
            }

            DataTable dt = new DataTable();
            DataRow row = null;
            dt.Columns.Add("result");
            //dt.Columns.Add("processTime");
            //dt.Columns.Add("invoiceFee");
            //dt.Columns.Add("payFee");
            //dt.Columns.Add("summary");
            //dt.Columns.Add("payStatus");

           // agentNo = "";// "DL224049";
           // feeMonth = "201412";
            AgentInvoicePaymentDao agentInvoicePaymentDao = new AgentInvoicePaymentDao();

            IList<AgentInvoicePayment> agentInvoicePaymentList = new List<AgentInvoicePayment>();

            agentInvoicePaymentList = agentInvoicePaymentDao.GetList(agentNo, null, feeMonth);

            foreach (AgentInvoicePayment agentInvoicePayment in agentInvoicePaymentList)
            {
                row = dt.NewRow();
                row["result"] = "<b>处理时间</b>：" + agentInvoicePayment.processTime + "<br/><b>发票金额</b>：" + agentInvoicePayment.invoiceFee + "<br/><b>付款金额</b>：" + agentInvoicePayment.payFee + "<br/><b>摘要</b>：" + agentInvoicePayment.summary + "<br/><b>付款状态</b>：" + agentInvoicePayment.payStatus;
                //row["processTime"] = agentInvoicePayment.processTime;
                //row["invoiceFee"] = agentInvoicePayment.invoiceFee;
                //row["payFee"] = agentInvoicePayment.payFee;
                //row["summary"] = agentInvoicePayment.summary;
                //row["payStatus"] = agentInvoicePayment.payStatus;
                dt.Rows.Add(row);
            }

            //this.lblFeeMonth.Text = feeMonth + "支付查询结果";
            
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
            
            

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCellCollection cells = e.Row.Cells;
                foreach (TableCell cell in cells)
                {
                    cell.Text = Server.HtmlDecode(cell.Text); //注意：此处所有的列所有的html代码都会按照html格式输出，如果只需要其中的哪一列的数据需要转换，此处需要小的修改即可。 
                } 

                if (String.IsNullOrEmpty(e.Row.Cells[0].Text) || e.Row.Cells[0].Text.Equals("&nbsp;"))
                {
                    if (!String.IsNullOrEmpty(e.Row.Cells[1].Text) || e.Row.Cells[1].Text.Equals("&nbsp;"))
                    {
                        e.Row.Cells[0].Attributes.Add("style", "color: #000066; font-weight: bold;");
                        e.Row.Cells[1].Attributes.Add("style", "color: #000066; font-weight: bold;");
                        e.Row.Cells[2].Attributes.Add("style", "color: #000066; font-weight: bold;");
                    }
                    else
                    {
                        e.Row.Cells[0].Attributes.Add("style", "display:none");
                        e.Row.Cells[1].Attributes.Add("style", "display:none");
                        e.Row.Cells[2].Attributes.Add("style", "display:none;");
                    }
                }
                
                //隐藏列

                //   e.Row.Cells[6].Attributes.Add("style", "display:none");   //隐藏数据列

            }
        }
    }
}