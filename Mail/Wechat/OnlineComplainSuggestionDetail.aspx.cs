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
            this.lblSubject.Text = agentComplianSuggestion.subject;
            this.lblContent.Text = agentComplianSuggestion.content;
            this.lblCreateTime.Text = agentComplianSuggestion.createTime;
            this.lblDepartment.Text = agentComplianSuggestion.ownerDepartment;

            this.lblCheckStatus.Text = agentComplianSuggestion.checkStatus;
            this.lblDepartmentReply.Text = agentComplianSuggestion.ownerReplyContent;
            this.lblReplyTime.Text = agentComplianSuggestion.replyTime;
            this.lblReplyContent.Text = agentComplianSuggestion.replyContent;

          
        }
    }
}