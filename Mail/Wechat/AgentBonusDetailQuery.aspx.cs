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
    public partial class AgentBonusDetailQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentBonusDetailQuery));
        protected void Page_Load(object sender, EventArgs e)
        {
            string feeMonth = Request.QueryString["feeMonth"];
            string agentNo = Request.QueryString["agentNo"];
           
            logger.Info("month=" + Request.QueryString["month"]);
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

            WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
            wechatQueryLog.agentName = "";
            wechatQueryLog.subSystem = "红包查询";
            wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            wechatQueryLog.queryString = feeMonth;
            wechatQueryLog.wechatId = agentNo;
            WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
            wechatQueryLogDao.Add(wechatQueryLog);

            DataTable dt = new DataTable();
            DataRow row = null;
            dt.Columns.Add("name");
            dt.Columns.Add("value");

            AgentBonusDao agentBonusDao = new ChinaUnion_DataAccess.AgentBonusDao();
            AgentBonus agentBonus = new AgentBonus();

            agentBonus = agentBonusDao.GetByKey(feeMonth,agentNo );


            if (agentBonus != null)
            {
               
                    row = dt.NewRow();
                    row["name"] = "代理商编号";
                    row["value"] = agentBonus.agentNo;
                    dt.Rows.Add(row);

                    row = dt.NewRow();
                    row["name"] = "代理商名称";
                    row["value"] = agentBonus.agentName;
                    dt.Rows.Add(row);
                


                for (int j = 1; j <= 100; j++)
                {
                    FieldInfo feeNameField = agentBonus.GetType().GetField("feeName" + j);
                    FieldInfo feeField = agentBonus.GetType().GetField("fee" + j);
                    if (feeNameField != null && feeField != null)
                    {
                        String feeNameFieldValue = feeNameField.GetValue(agentBonus) == null ? null : feeNameField.GetValue(agentBonus).ToString();

                        String feeFieldValue = feeField.GetValue(agentBonus) == null ? null : feeField.GetValue(agentBonus).ToString();

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
            this.lblFeeMonth.Text = feeMonth + "红包详情";
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //隐藏列
            // e.Row.Cells[0].Attributes.Add("style", "display:none");   //隐藏数据列

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (!String.IsNullOrEmpty(e.Row.Cells[0].Text) && (e.Row.Cells[0].Text.Equals("红包总金额") ))
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
                    if (!String.IsNullOrEmpty(e.Row.Cells[0].Text) && (e.Row.Cells[0].Text.Equals("红包总金额") ))
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