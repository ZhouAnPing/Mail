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
    public partial class AgentInvoiceQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentInvoiceQuery));
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
            //dt.Columns.Add("invoiceContent");
            //dt.Columns.Add("invoiceFee");
            //dt.Columns.Add("invoiceType");
            //dt.Columns.Add("invoiceNo");
            //dt.Columns.Add("comment");

            agentNo = "";// "DL224049";
            feeMonth = "201501";
            AgentInvoiceDao agentInvoiceDao = new AgentInvoiceDao();

            IList<AgentInvoice> agentInvoiceList = new List<AgentInvoice>();

            agentInvoiceList = agentInvoiceDao.GetList(agentNo, null, feeMonth);

            foreach (AgentInvoice agentInvoice in agentInvoiceList)
            {
                row = dt.NewRow();
                row["result"] = "<b>收票日期</b>:" + agentInvoice.invoiceDate + "<br/><b>内容</b>:" + agentInvoice.invoiceContent + "<br/><b>金额</b>:" + agentInvoice.invoiceFee + "<br/><b>发票类型</b>:" + agentInvoice.invoiceType + "<br/><b>发票号</b>:" + agentInvoice.invoiceNo + "<br/><b>备注</b>:" + agentInvoice.comment;
                //row["invoiceContent"] = agentInvoice.invoiceContent;
                //row["invoiceFee"] = agentInvoice.invoiceFee;
                //row["invoiceType"] = agentInvoice.invoiceType;
                //row["invoiceNo"] = agentInvoice.invoiceNo;
                //row["comment"] = agentInvoice.comment;
                dt.Rows.Add(row);
            }

            //this.lblFeeMonth.Text = feeMonth + "发票查询结果";
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