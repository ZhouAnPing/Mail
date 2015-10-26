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
    public partial class MyExamQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MyExamQuery));


        protected void Page_Load(object sender, EventArgs e)
        {

            //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fMyExamQuery.aspx%3fstatus%3dnew%26messageType%3dexam%26agentId%3d11&response_type=code&scope=snsapi_base&state=new#wechat_redirect
            //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fMyExamQuery.aspx%3fstatus%3dfinish%26messageType%3dexam%26agentId%3d11&response_type=code&scope=snsapi_base&state=join#wechat_redirect
            //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fMyExamQuery.aspx%3fstatus%3dnew%26messageType%3dsurvey%26agentId%3d11&response_type=code&scope=snsapi_base&state=new#wechat_redirect
            //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fMyExamQuery.aspx%3fstatus%3dfinish%26messageType%3dsurvey%26agentId%3d11&response_type=code&scope=snsapi_base&state=join#wechat_redirect
            //String myUrl = "http://112.64.17.80/wechat/MyExamQuery.aspx?status=new&messageType=exam&agentId=11";
            // myUrl = this.Server.UrlEncode(myUrl);

            logger.Info(this.Request.Url.AbsoluteUri);
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];
            string messageType = Request.QueryString["messageType"];
            string agentId = Request.QueryString["agentId"];
            string status = Request.QueryString["status"];
            logger.Info("agentId=" + Request.QueryString["agentId"]);
            logger.Info("code=" + Request.QueryString["code"]);
            logger.Info("state=" + Request.QueryString["state"]);
            logger.Info("messageType=" + Request.QueryString["messageType"]);
            logger.Info("status=" + Request.QueryString["status"]);
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
                    this.Session.Add("messageType", messageType);
                    this.Session.Add("status", status);
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
            String status = "";
            String messageType = "";
            if (Session["userId"] != null)
            {
                userId = this.Session["userId"].ToString(); ;
            }
            else
            {
                logger.Info("userId=null" );
            }
            if (Session["status"] != null)
            {
                status = this.Session["status"].ToString(); ;
            }
            if (Session["messageType"] != null)
            {
                messageType = this.Session["messageType"].ToString(); ;
            }

            logger.Info("userId=" + userId);
            AgentExamDao examDao = new ChinaUnion_DataAccess.AgentExamDao();
            IList<Exam> examList = examDao.GetList(condition, userId);

           
            
           // this.lblAgentNo.Text = agentNo;
            this.lblUserId.Text = userId;
            // int index = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("seq");
            dt.Columns.Add("userId");
            dt.Columns.Add("subject");
            dt.Columns.Add("status");
            
            dt.Columns.Add("validateStartTime");
            dt.Columns.Add("validateEndTime");

            DataRow row = null;
            if (examList != null && examList.Count > 0)
            {
                foreach (Exam exam in examList)
                {
                    
                    row = dt.NewRow();
                    row["seq"] = exam.sequence;
                    row["userId"] = userId;
                    row["subject"] = exam.subject;
                    row["status"] = exam.status;
                    if (String.IsNullOrEmpty(exam.status))
                    {
                        row["status"] = "未开始";
                    }
                    row["validateStartTime"] = exam.validateStartTime;
                    row["validateEndTime"] = exam.validateEndTime;
                   // dt.Rows.Add(row);
                    
                    if (status.Equals("new") )
                    {
                        dt.Rows.Add(row);
                    }
                    if (status.Equals("finish") && !String.IsNullOrEmpty(exam.status))
                    {
                        dt.Rows.Add(row);
                    }
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