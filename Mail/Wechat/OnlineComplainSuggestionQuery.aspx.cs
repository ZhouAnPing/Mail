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
                if (result != null && result.Html != null && result.Html.Contains("UserId"))
                {
                    WechatUserId returnMessage = (WechatUserId)JsonConvert.DeserializeObject(result.Html, typeof(WechatUserId));

                    AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
                    AgentWechatAccount agentWechatAccount = agentWechatAccountDao.Get(returnMessage.UserId);

                    if (agentWechatAccount != null && !String.IsNullOrEmpty(agentWechatAccount.status) && !agentWechatAccount.status.Equals("Y"))
                    {
                        this.lblType.Text = "你的账号已被停用，请联系联通工作人员!";
                        //  sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        // sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "对不起，你的账号已被停用，请联系联通工作人员!\n\n");

                    }
                    else if (agentWechatAccount == null)
                    {
                        this.lblType.Text = "用户不存在，请联系联通工作人员!";
                        //sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        // sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "用户不存在，请联系联通工作人员!\n\n");
                    }
                    else
                    {
                        String agentNo = agentWechatAccount.branchNo;
                        if (String.IsNullOrEmpty(agentNo))
                        {
                            agentNo = agentWechatAccount.agentNo;
                        }
                        String type = "吐槽";
                        if (!String.IsNullOrEmpty(state) && state.Equals("complain"))
                        {
                            type = "吐槽";
                            this.lblTitle.Text = "吐槽历史";
                        }

                        if (!String.IsNullOrEmpty(state) && state.Equals("appraise"))
                        {
                            type = "点赞";
                            this.lblTitle.Text = "点赞历史";
                        }

                        if (!String.IsNullOrEmpty(state) && state.Equals("suggestion"))
                        {
                            type = "建议";
                            this.lblTitle.Text = "建议历史";
                        }

                        bindDataToGrid("", type, agentNo, returnMessage.UserId);
                    }
                }
            }
            else
            {
                string type = Request.QueryString["type"];
                string agentNo = Request.QueryString["agentNo"];
                string userId = Request.QueryString["userId"];
                if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(agentNo))
                {
                    bindDataToGrid("", type, agentNo, userId);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            bindDataToGrid(this.txtCondition.Text.Trim(), this.lblType.Text, this.lblAgentNo.Text.Trim(),this.lblUserId.Text.Trim());
        }
        void bindDataToGrid(String subject, String type, String agentNo,String userId)
        {
            logger.Info("bindDataToGrid=");
            logger.Info("subject=" + subject);
            logger.Info("type=" + type);
            logger.Info("agentNo=" + agentNo);
            AgentComplianSuggestionDao agentComplianSuggestionDao = new ChinaUnion_DataAccess.AgentComplianSuggestionDao();
           
            IList<AgentComplianSuggestion> agentComplianSuggestionList = null;

            agentComplianSuggestionList = agentComplianSuggestionDao.GetListByKeyword("", type, agentNo,userId);
          
            this.lblType.Text = type;
            this.lblAgentNo.Text = agentNo;
            this.lblUserId.Text = userId;
            // int index = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("seq");
            dt.Columns.Add("createTime");
            dt.Columns.Add("subject");
            //  dt.Columns.Add("content");
            dt.Columns.Add("isReply");

            DataRow row = null;
            if (agentComplianSuggestionList != null && agentComplianSuggestionList.Count>0)
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
            else
            {
                this.lblMessag.Text = "未找到" + type+"记录!";
            }
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindDataToGrid(this.txtCondition.Text.Trim(), this.lblType.Text, this.lblAgentNo.Text.Trim(),lblUserId.Text.Trim());
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