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
                    agentComplianSuggestion.agentReadtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    agentComplianSuggestionDao.updateReadTime(agentComplianSuggestion);
                }



                this.lblReplyTime.Text = agentComplianSuggestion.replyTime;
                this.lblReplyContent.Text = agentComplianSuggestion.replyContent;
            }
            else
            {
                this.lblType.Text = "记录不存在";
            }
            
        }
    }
}