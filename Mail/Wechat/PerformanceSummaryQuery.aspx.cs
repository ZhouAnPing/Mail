using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
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
    public partial class PerformanceSummaryQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PerformanceSummaryQuery));

        protected void Page_Load(object sender, EventArgs e)
        {
            string feeMonth = Request.QueryString["feeMonth"];
            string agentNo = Request.QueryString["agentNo"];
            logger.Info("feeMonth=" + Request.QueryString["feeMonth"]);
            logger.Info("agentNo=" + Request.QueryString["agentNo"]);
            try
            {
                Request.ContentEncoding = Encoding.UTF8;
                //feeMonth = QueryStringEncryption.Decode(feeMonth, QueryStringEncryption.key);
               // agentNo = QueryStringEncryption.Decode(agentNo, QueryStringEncryption.key);
                logger.Info("feeMonth=" + feeMonth);
                logger.Info("agentNo=" + agentNo);
            }
            catch (Exception)
            {
                // return;
            }

            DataTable dt = new DataTable();
            DataRow row = null;
            dt.Columns.Add("branchNo");
            dt.Columns.Add("branchName");
            dt.Columns.Add("fee1");
            dt.Columns.Add("fee2");
            dt.Columns.Add("fee3");
            dt.Columns.Add("summary");


            feeMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
            agentNo = "P001";
            AgentMonthPerformanceDao agentMonthPerformanceDao = new ChinaUnion_DataAccess.AgentMonthPerformanceDao();          
            IList<AgentMonthPerformance> agentMonthPerformanceList = agentMonthPerformanceDao.GetList(agentNo, feeMonth);
           
            foreach (AgentMonthPerformance agentMonthPerformance in agentMonthPerformanceList)
            {
                row = dt.NewRow();
                row["branchNo"] = agentMonthPerformance.branchNo;
                row["branchName"] = agentMonthPerformance.branchName;
                row["fee1"] = agentMonthPerformance.fee1;
                row["fee2"] = agentMonthPerformance.fee2;
                row["fee3"] = agentMonthPerformance.fee3;
                row["summary"] = Decimal.Parse(agentMonthPerformance.fee1) + Decimal.Parse(agentMonthPerformance.fee2) + Decimal.Parse(agentMonthPerformance.fee3);
                dt.Rows.Add(row);
            }

            AgentMonthPerformance agentMonthPerformanceSummary = agentMonthPerformanceDao.GetSummary(agentNo, feeMonth);
            row = dt.NewRow();
            row["branchNo"] = agentMonthPerformanceSummary.agentNo;
            row["branchName"] = "总计";
            row["fee1"] = agentMonthPerformanceSummary.fee1;
            row["fee2"] = agentMonthPerformanceSummary.fee2;
            row["fee3"] = agentMonthPerformanceSummary.fee3;
            row["summary"] = Decimal.Parse(agentMonthPerformanceSummary.fee1) + Decimal.Parse(agentMonthPerformanceSummary.fee2) + Decimal.Parse(agentMonthPerformanceSummary.fee3);
            dt.Rows.Add(row);

            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //隐藏列
            e.Row.Cells[0].Attributes.Add("style", "display:none");   //隐藏数据列

            if (e.Row.RowType == DataControlRowType.DataRow)
            {



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

                if (!String.IsNullOrEmpty(e.Row.Cells[0].Text) && !e.Row.Cells[0].Text.Equals("&nbsp;") && !e.Row.Cells[1].Text.Equals("总计"))
                {
                    //e.Row.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[1].Text;
                }
              

            }


        }
      
    }
}