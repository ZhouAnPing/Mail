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
    public partial class MyComplainQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MyExamQuery));


        protected void Page_Load(object sender, EventArgs e)
        {

            //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fMyExamQuery.aspx%3fagentId%3d10&response_type=code&scope=snsapi_base&state=join#wechat_redirect
           
           // http%3a%2f%2f112.64.17.80%2fwechat%2fMyExamQuery.aspx%3fagentId%3d10
            // String myUrl = "http://112.64.17.80/wechat/MyComplainQuery.aspx?agentId=10";
           //  myUrl = this.Server.UrlEncode(myUrl);
           // bindDataToGrid("");
            logger.Info(this.Request.Url.AbsoluteUri);
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];            
            string agentId = Request.QueryString["agentId"];
           
            logger.Info("agentId=" + Request.QueryString["agentId"]);
            logger.Info("code=" + Request.QueryString["code"]);
            logger.Info("state=" + Request.QueryString["state"]);
            
             WechatUtil wechatUtil = new Util.WechatUtil();
             HttpResult result = wechatUtil.getUserInfoFromWechat(code, agentId, MyConstant.ScretId);
            logger.Info("result=" + result.Html);
            if (result != null && result.Html != null && result.Html.Contains("UserId"))
            {
                WechatUserId returnMessage = (WechatUserId)JsonConvert.DeserializeObject(result.Html, typeof(WechatUserId));

                logger.Info("returnMessage userId=" + returnMessage.UserId);

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
                   

                    //this.lblUserId.Text = agentWechatAccount.contactId;

                    this.Session.Add("userId", agentWechatAccount.contactId);
                   
                    bindDataToGrid("");
                }
            }
            else
            {
               
                string userId = Request.QueryString["userId"];
                if (!String.IsNullOrEmpty(userId))
                {
                    this.Session.Add("userId", userId);
                    bindDataToGrid("");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            bindDataToGrid(this.txtCondition.Text.Trim());
        }
        void bindDataToGrid(String condition)
        {
            String userId = "";
           
            if (Session["userId"] != null)
            {
                userId = this.Session["userId"].ToString(); ;
            }
            else
            {
                logger.Info("userId=null" );
            }
           

            logger.Info("userId=" + userId);

            AgentComplainDao agentComplainDao = new AgentComplainDao();
            IList<AgentComplain> complainList = agentComplainDao.GetListByWechatUserId(userId);

           
            
           // this.lblAgentNo.Text = agentNo;
            this.lblUserId.Text = userId;
            // int index = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("seq");
            dt.Columns.Add("userId");
            dt.Columns.Add("processCode");
            dt.Columns.Add("content");
            
            dt.Columns.Add("replyTime");
           

            DataRow row = null;
            if (complainList != null && complainList.Count > 0)
            {
                foreach (AgentComplain agentComplain in complainList)
                {
                    
                    row = dt.NewRow();
                    row["seq"] = agentComplain.sequence;
                    row["userId"] = userId;
                    row["processCode"] = agentComplain.processCode;
                    if (!String.IsNullOrEmpty(agentComplain.content) && agentComplain.content.Length > 20)
                    {
                        row["content"] = agentComplain.content.Substring(0,20)+"...";
                    }
                    else
                    {
                        row["content"] = agentComplain.content;
                    }
                    row["replyTime"] = agentComplain.replyTime;
                    dt.Rows.Add(row);
                  
                }
            }
            else
            {
                this.lblMessag.Text = "未找到"  + "记录!";
            }
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindDataToGrid(this.txtCondition.Text.Trim());
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //隐藏列
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Attributes.Add("style", "display:none");   //隐藏数据列
                e.Row.Cells[1].Attributes.Add("style", "display:none");   //隐藏数据列
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
                      //  e.Row.Cells[2].Attributes.Add("style", "color: #000066; font-weight: bold;");
                    }
                    else
                    {
                        e.Row.Cells[0].Attributes.Add("style", "display:none");
                        e.Row.Cells[1].Attributes.Add("style", "display:none");
                      //  e.Row.Cells[2].Attributes.Add("style", "display:none;");
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