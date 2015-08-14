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
using System.Reflection;

namespace Wechat
{
    public partial class PerformanceDailySummaryQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PerformanceDailySummaryQuery));

        protected void Page_Load(object sender, EventArgs e)
        {
            string feeDate = Request.QueryString["feeDate"];
            string agentNo = Request.QueryString["agentNo"];
            logger.Info("feeDate=" + Request.QueryString["feeDate"]);
            logger.Info("agentNo=" + Request.QueryString["agentNo"]);
            try
            {
                Request.ContentEncoding = Encoding.UTF8;
               feeDate = QueryStringEncryption.Decode(feeDate, QueryStringEncryption.key);
                agentNo = QueryStringEncryption.Decode(agentNo, QueryStringEncryption.key);
                logger.Info("feeDate=" + feeDate);
                logger.Info("agentNo=" + agentNo);
            }
            catch (Exception)
            {
                // return;
            }


            DataTable dt = new DataTable();
            DataRow row = null;
            dt.Columns.Add("date");
            dt.Columns.Add("branchNo");
            dt.Columns.Add("branchName");
            dt.Columns.Add("fee1");
            dt.Columns.Add("fee2");

            dt.Columns.Add("summary");

             //agentNo = "P001";
           // feeDate = "2015-07-08";// DateTime.Now.AddMonths(-1).ToString("yyyy-MM");

            AgentDailyPerformanceDao agentPerformanceDao = new ChinaUnion_DataAccess.AgentDailyPerformanceDao();


            IList<AgentDailyPerformance> agentPerformanceList = agentPerformanceDao.GetList(agentNo, feeDate);

            foreach (AgentDailyPerformance agentPerformance in agentPerformanceList)
            {
                row = dt.NewRow();
                row["date"] = agentPerformance.date;
                row["branchNo"] = agentPerformance.branchNo;
                row["branchName"] = agentPerformance.branchName;
                row["fee1"] = "0";
                row["fee2"] = "0";
                for (int j = 1; j <= 100; j++)
                {
                    FieldInfo feeNameField = agentPerformance.GetType().GetField("feeName" + j);
                    FieldInfo feeField = agentPerformance.GetType().GetField("fee" + j);
                    if (feeNameField != null && feeField != null)
                    {
                        String feeNameFieldValue = feeNameField.GetValue(agentPerformance) == null ? null : feeNameField.GetValue(agentPerformance).ToString();

                        String feeFieldValue = feeField.GetValue(agentPerformance) == null ? null : feeField.GetValue(agentPerformance).ToString();


                        if (!String.IsNullOrEmpty(feeFieldValue) && feeNameFieldValue.Equals("后付费发展数"))
                        {
                            row["fee1"] = feeFieldValue;
                        }
                        if (!String.IsNullOrEmpty(feeFieldValue) && feeNameFieldValue.Equals("预付费发展数"))
                        {
                            row["fee2"] = feeFieldValue;
                        }
                    }
                }



                row["summary"] = Int32.Parse(row["fee1"].ToString()) + Int32.Parse(row["fee2"].ToString());
                dt.Rows.Add(row);
            }

            AgentDailyPerformance agentPerformanceSummary = agentPerformanceDao.GetSummary(agentNo, feeDate);
            row = dt.NewRow();
            row["date"] = agentPerformanceSummary.date;
            row["branchNo"] = agentPerformanceSummary.agentNo;
            row["branchName"] = "总计";
            row["fee1"] = "0";
            row["fee2"] = "0";
            for (int j = 1; j <= 100; j++)
            {
                FieldInfo feeNameField = agentPerformanceSummary.GetType().GetField("feeName" + j);
                FieldInfo feeField = agentPerformanceSummary.GetType().GetField("fee" + j);
                if (feeNameField != null && feeField != null)
                {
                    String feeNameFieldValue = feeNameField.GetValue(agentPerformanceSummary) == null ? null : feeNameField.GetValue(agentPerformanceSummary).ToString();

                    String feeFieldValue = feeField.GetValue(agentPerformanceSummary) == null ? null : feeField.GetValue(agentPerformanceSummary).ToString();


                    if (!String.IsNullOrEmpty(feeFieldValue) && feeNameFieldValue.Equals("后付费发展数"))
                    {
                        row["fee1"] = feeFieldValue;
                    }
                    if (!String.IsNullOrEmpty(feeFieldValue) && feeNameFieldValue.Equals("预付费发展数"))
                    {
                        row["fee2"] = feeFieldValue;
                    }
                }
            }



            row["summary"] = Int32.Parse(row["fee1"].ToString()) + Int32.Parse(row["fee2"].ToString());
            dt.Rows.Add(row);
            this.lblFeeMonth.Text = feeDate + "绩效统计";
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();

        }

      

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //隐藏列
            e.Row.Cells[0].Attributes.Add("style", "display:none");   //隐藏数据列
            e.Row.Cells[1].Attributes.Add("style", "display:none");   //隐藏数据列

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