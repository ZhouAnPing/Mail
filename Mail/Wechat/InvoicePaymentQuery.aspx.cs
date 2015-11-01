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
    public partial class InvoicePaymentQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(InvoicePaymentQuery));
        protected void Page_Load(object sender, EventArgs e)
        {
            string feeMonth = Request.QueryString["feeMonth"];
            string agentNo = Request.QueryString["agentNo"];
            string invoiceNo = Request.QueryString["invoiceNo"];

            logger.Info("feeMonth=" + Request.QueryString["feeMonth"]);
            logger.Info("agentNo=" + Request.QueryString["agentNo"]);
            logger.Info("invoiceNo=" + Request.QueryString["invoiceNo"]);
            try
            {
                Request.ContentEncoding = Encoding.UTF8;
                feeMonth = QueryStringEncryption.Decode(feeMonth, QueryStringEncryption.key);
                agentNo = QueryStringEncryption.Decode(agentNo, QueryStringEncryption.key);
                // invoiceNo = QueryStringEncryption.Decode(invoiceNo, QueryStringEncryption.key);
                logger.Info("feeMonth=" + feeMonth);
                logger.Info("agentNo=" + agentNo);
                logger.Info("invoiceNo=" + invoiceNo);
            }
            catch (Exception)
            {
               // return;
            }

            WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
            wechatQueryLog.agentName = "";
            wechatQueryLog.module = Util.MyConstant.module_Commission;
            wechatQueryLog.subSystem = "支付结算查询";
            wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            wechatQueryLog.queryString = feeMonth;
            wechatQueryLog.wechatId = agentNo;
            WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
            try
            {
                wechatQueryLogDao.Add(wechatQueryLog);
            }
            catch
            {
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
           // feeMonth = "201501";
            InvoicePaymentDao agentInvoicePaymentDao = new InvoicePaymentDao();

            IList<InvoicePayment> agentInvoicePaymentList = new List<InvoicePayment>();

            agentInvoicePaymentList = agentInvoicePaymentDao.GetList(agentNo, null, feeMonth,null);
            if (agentInvoicePaymentList != null && agentInvoicePaymentList.Count > 0)
            {
                foreach (InvoicePayment agentInvoicePayment in agentInvoicePaymentList)
                {
                    row = dt.NewRow();
                    row["result"] = "<b>收票日期</b>：" + agentInvoicePayment.receivedTime
                         + "<br/><b>处理时间</b>：" + agentInvoicePayment.processTime
                        + "<br/><b>内容</b>：" + agentInvoicePayment.content
                        + "<br/><b>金额</b>：" + agentInvoicePayment.invoiceFee
                        + "<br/><b>发票类型</b>：" + agentInvoicePayment.invoiceType
                        + "<br/><b>发票号</b>：" + "***" + agentInvoicePayment.invoiceNo.Substring(3)
                        + "<br/><b>付款状态</b>：" + agentInvoicePayment.payStatus;
                    //row["processTime"] = agentInvoicePayment.processTime;
                    //row["invoiceFee"] = agentInvoicePayment.invoiceFee;
                    //row["payFee"] = agentInvoicePayment.payFee;
                    //row["summary"] = agentInvoicePayment.summary;
                    //row["payStatus"] = agentInvoicePayment.payStatus;
                    dt.Rows.Add(row);
                }
            }
            else
            {
                row = dt.NewRow();
                row["result"] = feeMonth + "无发票受理记录或尚未完成，请耐心等候。";
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