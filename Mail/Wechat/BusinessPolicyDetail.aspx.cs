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
    public partial class BusinessPolicyDetail : System.Web.UI.Page
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BusinessPolicyDetail));
        protected void Page_Load(object sender, EventArgs e)
        {
            string sequence =  Request.QueryString["seq"];
            string userId =  Request.QueryString["userId"];
            logger.Info("sequence=" + sequence);
            logger.Info("userId=" + userId);
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
                this.lblAttachment.NavigateUrl = "BusinessPolicyAttachmentDetail.aspx?seq=" + policy.sequence+"&userId="+userId;

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