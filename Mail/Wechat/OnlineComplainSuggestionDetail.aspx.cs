using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wechat
{
    public partial class OnlineComplainSuggestionDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sequence = Request.QueryString["seq"];
            string from = Request.QueryString["from"];
            if (!String.IsNullOrEmpty(from))
            {
                this.btnBack.Visible = false;
            }

            AgentComplianSuggestionDao agentComplianSuggestionDao = new ChinaUnion_DataAccess.AgentComplianSuggestionDao();
            AgentComplianSuggestion agentComplianSuggestion = agentComplianSuggestionDao.Get(Int32.Parse(sequence));
            if (agentComplianSuggestion != null)
            {
                this.lblType.Text = agentComplianSuggestion.type + "详情";
                this.lblSubject.Text = agentComplianSuggestion.subject;
                this.lblContent.Text = agentComplianSuggestion.content;
                this.lblCreateTime.Text = agentComplianSuggestion.createTime;

                if (String.IsNullOrEmpty(agentComplianSuggestion.replyContent))
                {
                    this.lblIsReply.Text = "尚未回复";
                    this.lblIsReply.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.lblIsReply.Text = "已回复";
                    this.lblIsReply.ForeColor = System.Drawing.Color.Blue;
                    agentComplianSuggestion.agentReadtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    agentComplianSuggestionDao.updateReadTime(agentComplianSuggestion);
                }



                this.lblReplyTime.Text = agentComplianSuggestion.replyTime;
                this.lblReplyContent.Text = agentComplianSuggestion.replyContent;


                WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                wechatQueryLog.agentName = "";
                wechatQueryLog.module = Util.MyConstant.module_Service;
                wechatQueryLog.subSystem = "服务监督";
                wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                wechatQueryLog.queryString = agentComplianSuggestion.type;
                wechatQueryLog.wechatId = agentComplianSuggestion.userId;
                WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
                try
                {
                    wechatQueryLogDao.Add(wechatQueryLog);
                }
                catch
                {
                }
            }
            else
            {
                this.lblType.Text = "记录不存在";
            }
            
        }
    }
}