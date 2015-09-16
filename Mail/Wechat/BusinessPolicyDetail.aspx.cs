using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wechat.BO;
using Wechat.Util;

namespace Wechat
{
    public partial class BusinessPolicyDetail : System.Web.UI.Page
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BusinessPolicyDetail));
        protected void Page_Load(object sender, EventArgs e)
        {
            string sequence =  Request.QueryString["seq"];
            string userId =  Request.QueryString["userId"];
            logger.Info("sequence=" + sequence);
            logger.Info("userId=" + userId);

            if (String.IsNullOrEmpty(sequence))
            {
                string code = Request.QueryString["code"];
                sequence = Request.QueryString["state"];
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
                    userId = returnMessage.UserId;
                }
                
            }
            if (!String.IsNullOrEmpty(sequence))
            {
                PolicyDao policyDao = new ChinaUnion_DataAccess.PolicyDao();
                Policy policy = policyDao.Get(Int32.Parse(sequence));
                if (policy != null)
                {
                    this.lblSubject.Text = policy.subject;
                    this.lblSendTime.Text = policy.creatTime;
                    this.lblContent.Text = policy.content;
                    this.lblValidateStartTime.Text = policy.validateStartTime;
                    this.lblValidateEndTime.Text = policy.validateEndTime;
                    this.lblAttachment.Text = policy.attachmentName;
                    this.lblAttachment.NavigateUrl = "BusinessPolicyAttachmentDetail.aspx?seq=" + policy.sequence + "&userId=" + userId;

                    logger.Info("sequence=" + sequence);
                    logger.Info("userId=" + userId);
                    PolicyReceiverLogDao policyReceiverLogDao = new PolicyReceiverLogDao();
                    PolicyReceiverLog policyReceiverLog = new PolicyReceiverLog();
                    policyReceiverLog.policySequence = policy.sequence;
                    policyReceiverLog.readtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    policyReceiverLog.userId = userId;
                    policyReceiverLogDao.Add(policyReceiverLog);
                }
            }
        }
    }
}