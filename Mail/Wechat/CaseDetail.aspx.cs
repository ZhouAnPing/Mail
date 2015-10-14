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
    public partial class CaseDetail : System.Web.UI.Page
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CaseDetail));
        protected void Page_Load(object sender, EventArgs e)
        {
            string sequence =  Request.QueryString["seq"];
            string userId =  Request.QueryString["userId"];
            logger.Info("sequence=" + sequence);
            logger.Info("userId=" + userId);

            
            if (!String.IsNullOrEmpty(sequence))
            {
                PolicyDao policyDao = new ChinaUnion_DataAccess.PolicyDao();
                Policy policy = policyDao.Get(Int32.Parse(sequence));
                if (policy != null)
                {
                    this.lblSubject.Text = policy.subject;
                   // this.lblSendTime.Text = policy.creatTime;
                    if (!String.IsNullOrEmpty(policy.content))
                    {
                        this.lblContent.Text = " <br>"+policy.content.Replace("\r\n", " <br>").Replace("\n", " <br>"); ;
                    }
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