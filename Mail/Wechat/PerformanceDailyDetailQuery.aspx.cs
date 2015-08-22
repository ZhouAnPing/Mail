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
    public partial class PerformanceDailyDetailQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PerformanceDailyDetailQuery));
        protected void Page_Load(object sender, EventArgs e)
        {
            string feeDate = Request.QueryString["date"];
            string branchNo = Request.QueryString["branchNo"];
            string branchName = Request.QueryString["branchName"];
            logger.Info("feeDate=" + Request.QueryString["date"]);
            logger.Info("branchNo=" + Request.QueryString["branchNo"]);
            logger.Info("branchName=" + Request.QueryString["branchName"]);
            try
            {
                Request.ContentEncoding = Encoding.UTF8;
               // feeMonth = QueryStringEncryption.Decode(feeMonth, QueryStringEncryption.key);
               // agentNo = QueryStringEncryption.Decode(agentNo, QueryStringEncryption.key);
                logger.Info("feeMonth=" + feeDate);
                logger.Info("branchNo=" + branchNo);
            }
            catch (Exception)
            {
                // return;
            }

            DataTable dt = new DataTable();
            DataRow row = null;
            dt.Columns.Add("name");
            dt.Columns.Add("value");

            AgentDailyPerformanceDao agentPerformanceDao = new ChinaUnion_DataAccess.AgentDailyPerformanceDao();
            AgentDailyPerformance agentPerformance = new AgentDailyPerformance();
            if (!branchName.Equals("总计"))
            {
                agentPerformance = agentPerformanceDao.GetByKey(feeDate, branchNo);
            }
            else
            {
                agentPerformance = agentPerformanceDao.GetSummary(branchNo,feeDate);
            }

            if (agentPerformance != null)
            {
                if (!branchName.Equals("总计"))
                {
                    row = dt.NewRow();
                    row["name"] = "门店编号";
                    row["value"] = agentPerformance.branchNo;
                    dt.Rows.Add(row);

                    row = dt.NewRow();
                    row["name"] = "门店名称";
                    row["value"] = agentPerformance.branchName;
                    dt.Rows.Add(row);
                }
                else
                {
                    row = dt.NewRow();
                    row["name"] = "代理商编号";
                    row["value"] = agentPerformance.agentNo;
                    dt.Rows.Add(row);

                    row = dt.NewRow();
                    row["name"] = "代理商名称";
                    row["value"] = agentPerformance.agentName;
                    dt.Rows.Add(row);
                }


                for (int j = 1; j <= 100; j++)
                {
                    FieldInfo feeNameField = agentPerformance.GetType().GetField("feeName" + j);
                    FieldInfo feeField = agentPerformance.GetType().GetField("fee" + j);
                    if (feeNameField != null && feeField != null)
                    {
                        String feeNameFieldValue = feeNameField.GetValue(agentPerformance) == null ? null : feeNameField.GetValue(agentPerformance).ToString();

                        String feeFieldValue = feeField.GetValue(agentPerformance) == null ? null : feeField.GetValue(agentPerformance).ToString();

                        if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                        {
                            row = dt.NewRow();
                            row["name"] = feeNameFieldValue;
                            row["value"] = feeFieldValue;
                            dt.Rows.Add(row);

                        }
                    }


                }
            }
            this.lblFeeMonth.Text = feeDate + "绩效详情";
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //隐藏列
            // e.Row.Cells[0].Attributes.Add("style", "display:none");   //隐藏数据列

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (!String.IsNullOrEmpty(e.Row.Cells[0].Text) && (e.Row.Cells[0].Text.Equals("后付费发展数") || e.Row.Cells[0].Text.Equals("预付费发展数")))
                {
                    e.Row.Cells[0].Attributes.Add("style", "color: #000066; font-weight: bold;");
                    e.Row.Cells[1].Attributes.Add("style", "color: #000066; font-weight: bold;");
                   // e.Row.Cells[2].Attributes.Add("style", "color: #000066; font-weight: bold;");
                }
                else
                {
                    //e.Row.Cells[0].Attributes.Add("style", "display:none");
                  //  e.Row.Cells[1].Attributes.Add("style", "display:none");
                   // e.Row.Cells[2].Attributes.Add("style", "display:none;");
                }

                if (e.Row.RowIndex > 1)
                {
                    if (!String.IsNullOrEmpty(e.Row.Cells[0].Text) && (e.Row.Cells[0].Text.Equals("后付费发展数") || e.Row.Cells[0].Text.Equals("预付费发展数")))
                    {
                        //e.Row.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[1].Text;
                        e.Row.Cells[0].Attributes.Add("style", "color: #000066; font-weight: bold;text-align:left;");
                        e.Row.Cells[1].Attributes.Add("style", "color: #000066; font-weight: bold;text-align:right;");
                       // e.Row.Cells[2].Attributes.Add("style", "color: #000066; font-weight: bold;text-align:right;");
                    }
                    else
                    {
                        e.Row.Cells[0].Attributes.Add("style", "color: #000066; font-weight: normal;text-align:right;");
                        e.Row.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[1].Text;
                    }
                }
                


            }


        }

       
    }
}