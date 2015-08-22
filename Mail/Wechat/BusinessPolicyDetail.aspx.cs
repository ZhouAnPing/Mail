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
        protected void Page_Load(object sender, EventArgs e)
        {
            string sequence = Request.QueryString["seq"];

            PolicyDao policyDao = new ChinaUnion_DataAccess.PolicyDao();
            Policy policy = policyDao.Get(Int32.Parse(sequence));
            if (policy != null)
            {
                this.lblSubject.Text = policy.subject;
                this.lblSendTime.Text = policy.creatTime;
                this.lblContent.Text = policy.content;
                this.lblAttachment.Text = policy.attachmentName;
                this.lblAttachment.NavigateUrl = "BusinessPolicyAttachmentDetail.aspx?seq=" + policy.sequence;
            }
        }
    }
}