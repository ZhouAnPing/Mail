using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wechat.BO;
using Wechat.Util;

namespace Wechat
{
    public partial class OnlineComplainSuggestionQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(OnlineComplainSuggestionQuery));


        protected void Page_Load(object sender, EventArgs e)
        {
            logger.Info(this.Request.Url.AbsoluteUri);

           
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];
            
            logger.Info("code=" + Request.QueryString["code"]);
            logger.Info("state=" + Request.QueryString["state"]);

            if (!String.IsNullOrEmpty(code))
            {
                WechatUtil wechatUtil = new Util.WechatUtil();
                HttpResult result = wechatUtil.getUserInfoFromWechat(code, "9", "stkc3kfO0sCrIJTFCHzOhmEsRWAnVGHqrzBIy4le6mV6EcSkNbpY0Tt49Uci2Buu");
                logger.Info("result=" + result.Html);
                if (result.Html.Contains("UserId"))
                {
                    WechatUserId returnMessage = (WechatUserId)JsonConvert.DeserializeObject(result.Html, typeof(WechatUserId));
                    lblAgentNo.Text = returnMessage.UserId;
                    String type = "投诉";
                    if (!String.IsNullOrEmpty(state) && state.Equals("complain"))
                    {
                        type = "投诉";
                    }

                    if (!String.IsNullOrEmpty(state) && state.Equals("suggestion"))
                    {
                        type = "建议";
                    }

                    bindDataToGrid("", type, lblAgentNo.Text);
                }
            }
            else
            {
                string type = Request.QueryString["type"];
                string agentNo = Request.QueryString["agentNo"];
                if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(agentNo))
                {
                    bindDataToGrid("", type, agentNo);
                }
            }

           
           

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            bindDataToGrid(this.txtCondition.Text.Trim(), this.lblType.Text, this.lblAgentNo.Text.Trim());
        }
        void bindDataToGrid(String subject, String type, String agentNo)
        {
            logger.Info("bindDataToGrid=");
            logger.Info("subject=" + subject);
            logger.Info("type=" + type);
            logger.Info("agentNo=" + agentNo);
            AgentComplianSuggestionDao agentComplianSuggestionDao = new ChinaUnion_DataAccess.AgentComplianSuggestionDao();
           
            IList<AgentComplianSuggestion> agentComplianSuggestionList = null;

            agentComplianSuggestionList = agentComplianSuggestionDao.GetListByKeyword("", type, agentNo);
          
            this.lblType.Text = type;
            this.lblAgentNo.Text = agentNo;
            // int index = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("seq");
            dt.Columns.Add("createTime");
            dt.Columns.Add("subject");
          //  dt.Columns.Add("content");
            dt.Columns.Add("isReply");

            DataRow row = null;
            if (agentComplianSuggestionList != null)
            {
                foreach (AgentComplianSuggestion agentComplianSuggestion in agentComplianSuggestionList)
                {
                    row = dt.NewRow();
                    row["seq"] = agentComplianSuggestion.sequence;
                    row["createTime"] = agentComplianSuggestion.createTime;
                    row["subject"] = agentComplianSuggestion.subject;
                    if (String.IsNullOrEmpty(agentComplianSuggestion.replyContent))
                    {
                        row["isReply"] = "尚未回复";
                    }
                    else
                    {
                        row["isReply"] = "已回复";
                    }
                   
                    dt.Rows.Add(row);
                }
            }
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindDataToGrid(this.txtCondition.Text.Trim(), this.lblType.Text, this.lblAgentNo.Text.Trim());
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //隐藏列
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Attributes.Add("style", "display:none");   //隐藏数据列
               // e.Row.Cells[3].Attributes.Add("style", "display:none");   //隐藏数据列
            }
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
                    e.Row.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[1].Text;
                }


            }

        }


    }
}