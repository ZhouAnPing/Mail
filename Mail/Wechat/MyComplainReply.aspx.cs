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
    public partial class MyComplainReply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sequence = Request.QueryString["seq"];
            string userId = Request.QueryString["userId"];
            string from = Request.QueryString["from"];
            if (!String.IsNullOrEmpty(from))
            {
                //this.btnBack.Visible = false;
            }
           

            this.lblSeq.Text = sequence;
            this.lblWechatUser.Text = userId;
            AgentComplainDao agentComplainDao = new ChinaUnion_DataAccess.AgentComplainDao();
            AgentComplain agentComplain = agentComplainDao.Get(int.Parse(sequence));

            AgentComplainReplyDao agentComplainReplyDao = new AgentComplainReplyDao();
            IList<AgentComplainReply> agentComplainReplyList = agentComplainReplyDao.GetList(sequence);
            if (agentComplain != null)
            {
                this.lblUserId.Text = agentComplain.userName;
                this.lblProcessCode.Text = agentComplain.processCode;
                this.lblJoinTime.Text = agentComplain.joinTime;
                this.lblJoinMenu.Text = agentComplain.joinMenu;
                this.lblComplainContent.Text = agentComplain.content;
                this.lblBranchCode.Text = agentComplain.processBranchCode;
                this.lblBranchName.Text = agentComplain.processBranchName;
                this.lblReplyTime.Text = agentComplain.replyTime;
                this.lblComment.Text = agentComplain.comment;
                this.lblReplyHis.Text = "<br>";
                foreach (AgentComplainReply agentComplainReply in agentComplainReplyList)
                {
                    this.lblReplyHis.Text += "【"+agentComplainReply.replyTime + "】" + agentComplainReply.replyContent + "<br>";
                }

                WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                wechatQueryLog.agentName = "";
                wechatQueryLog.module = Util.MyConstant.module_Complain;
                wechatQueryLog.subSystem = "投诉协查";
                wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                wechatQueryLog.queryString = "";
                wechatQueryLog.wechatId = userId;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AgentComplainReplyDao agentComplainReplyDao = new AgentComplainReplyDao();
            AgentComplainReply agentComplainReply = new AgentComplainReply();
            if (!String.IsNullOrEmpty(this.txtReplyContent.Text))
            {
                agentComplainReply.replyContent = this.txtReplyContent.Text;
                agentComplainReply.complainSequence = this.lblSeq.Text;
                agentComplainReply.replyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                agentComplainReplyDao.Add(agentComplainReply);

                this.Server.Transfer("./MyComplainQuery.aspx?userId=" + this.lblWechatUser.Text);
            }
            
        }

      
    }
}