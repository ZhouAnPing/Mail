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
    public partial class OnlineStudyQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(OnlineStudyQuery));

       
        protected void Page_Load(object sender, EventArgs e)
        {
            logger.Info(this.Request.Url.AbsoluteUri);

            //bindDataToGrid("", "政策", "validate", "DL204053", "P001");
            //http%3a%2f%2f112.64.17.80%2fwechat%2fOnlineStudyQuery.aspx%3fsearch_scope%3dvalidate%26messageType%3dnotice%26agentId%3d6
            //http%3a%2f%2f112.64.17.80%2fwechat%2fOnlineStudyQuery.aspx%3fsearch_scope%3dvalidate%26messageType%3dnotice
            //String myUrl = "http://112.64.17.80/wechat/OnlineStudyQuery.aspx?search_scope=validate&messageType=notice&agentId=6";
            // myUrl = this.Server.UrlEncode(myUrl);
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];
            string search_scope = Request.QueryString["search_scope"];
            string agentId = Request.QueryString["agentId"];
            logger.Info("agentId=" + Request.QueryString["agentId"]);
            logger.Info("code=" + Request.QueryString["code"]);
            logger.Info("state=" + Request.QueryString["state"]);
            logger.Info("search_scope=" + Request.QueryString["search_scope"]);
             WechatUtil wechatUtil = new Util.WechatUtil();
             HttpResult result = wechatUtil.getUserInfoFromWechat(code, agentId, MyConstant.ScretId);
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


                   // search_scope = "all";
                    String type = "在线学习";
                    


                    WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                    wechatQueryLog.agentName = "";
                    wechatQueryLog.module = Util.MyConstant.module_Study;
                    wechatQueryLog.subSystem = "在线学习";
                    wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    wechatQueryLog.queryString = type;
                    wechatQueryLog.wechatId = agentWechatAccount.contactId;
                    WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
                    try
                    {
                        wechatQueryLogDao.Add(wechatQueryLog);
                    }
                    catch
                    {
                    }

                    bindDataToGrid("", type, search_scope, agentNo, agentWechatAccount.contactId);
                }
            }
          

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            bindDataToGrid(this.txtCondition.Text.Trim(), this.lblType.Text, this.lblScope.Text.Trim(),this.lblAgentNo.Text.Trim(),this.lblUserId.Text.Trim());
        }
        void bindDataToGrid(String subject, String type, String search_scope, String agentNo,String userId)
        {
            logger.Info("bindDataToGrid=");
            logger.Info("subject=" + subject);
            logger.Info("type=" + type);
            logger.Info("search_scope=" + search_scope);
            logger.Info("agentNo=" + agentNo);
            logger.Info("userId=" + userId);
            StudyDao studyDao = new ChinaUnion_DataAccess.StudyDao();

            IList<Study> studyList = null;

            if (!String.IsNullOrEmpty(search_scope) && search_scope.Equals("validate"))
            {
                studyList = studyDao.GetAllValidatedList(subject, type);
                logger.Info("validate=");
            }


            if (!String.IsNullOrEmpty(search_scope) && search_scope.Equals("all"))
            {
                studyList = studyDao.GetList(subject, type);
                logger.Info("all=");
            }
            this.lblType.Text = type;
            this.lblScope.Text = search_scope;
            this.lblAgentNo.Text = agentNo;
            this.lblUserId.Text = userId;
            // int index = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("seq");
            dt.Columns.Add("userId");
            dt.Columns.Add("subject");
            dt.Columns.Add("content");
            dt.Columns.Add("attachment");
            dt.Columns.Add("validateStartTime");
            dt.Columns.Add("validateEndTime");

            DataRow row = null;
            if (studyList != null && studyList.Count>0)
            {
                foreach (Study study in studyList)
                {
                    if (!study.toAll.Equals("Y"))
                    {
                        AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
                        AgentWechatAccount agentWechatAccount = agentWechatAccountDao.Get(userId);

                        if (!String.IsNullOrEmpty(study.agentType) )
                        {
                              IList<String> list = study.agentType.Split(';').ToList<String>();
                              if (!list.Contains(agentWechatAccount.type))
                              {
                                  continue;
                              }
                        }
                        if (String.IsNullOrEmpty(study.agentType))
                        {
                            IList<String> agentNoList = studyDao.GetAllAgentNoListBySeq(study.sequence);
                            if (!agentNoList.Contains(agentNo))
                            {
                                logger.Info("userId=" + userId + " 没有权限范围" + study.sequence);
                                continue;
                            }
                        }
                    }
                    row = dt.NewRow();
                    row["seq"] = study.sequence;
                    row["userId"] = userId;
                    row["subject"] = study.subject;
                    if (study.content.Length > 10)
                    {
                        row["content"] = study.content.Substring(0, 10) + "......";
                    }
                    else
                    {
                        row["content"] = study.content;
                    }
                    if (!String.IsNullOrEmpty(study.attachmentName))
                    {
                        row["attachment"] = "附件";
                    }
                    row["validateStartTime"] = study.validateStartTime;
                    row["validateEndTime"] = study.validateEndTime;
                    dt.Rows.Add(row);
                }
            }
            else
            {
                this.lblMessag.Text = "未找到" + type + "记录!";
            }
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindDataToGrid(this.txtCondition.Text.Trim(), this.lblType.Text, this.lblScope.Text.Trim(), this.lblAgentNo.Text.Trim(), this.lblUserId.Text.Trim());
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //隐藏列
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Attributes.Add("style", "display:none");   //隐藏数据列
                e.Row.Cells[1].Attributes.Add("style", "display:none");   //隐藏数据列
                e.Row.Cells[3].Attributes.Add("style", "display:none");   //隐藏数据列
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