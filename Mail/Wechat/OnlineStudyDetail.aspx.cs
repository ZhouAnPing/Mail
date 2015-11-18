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
    public partial class OnlineStudyDetail : System.Web.UI.Page
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(OnlineStudyDetail));
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
                StudyDao studyDao = new ChinaUnion_DataAccess.StudyDao();
                Study study = studyDao.Get(Int32.Parse(sequence));
                if (study != null)
                {
                    this.lblSubject.Text = study.subject;
                    this.lblSendTime.Text = study.creatTime;
                    if (!String.IsNullOrEmpty(study.content))
                    {
                        this.lblContent.Text = study.content.Replace("\r\n"," <br>").Replace("\n"," <br>");
                    }
                    this.lblValidateStartTime.Text = study.validateStartTime;
                    this.lblValidateEndTime.Text = study.validateEndTime;
                    this.lblAttachment.Text = study.attachmentName;
                    this.lblAttachment.NavigateUrl = "OnlineStudyAttachmentDetail.aspx?seq=" + study.sequence + "&userId=" + userId;

                    logger.Info("sequence=" + sequence);
                    logger.Info("userId=" + userId);
                    StudyReceiverLogDao studyReceiverLogDao = new StudyReceiverLogDao();
                    StudyReceiverLog studyReceiverLog = new StudyReceiverLog();
                    studyReceiverLog.studySequence = study.sequence;
                    studyReceiverLog.readtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    studyReceiverLog.userId = userId;
                    studyReceiverLogDao.Add(studyReceiverLog);


                    WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                    wechatQueryLog.agentName = "";
                    wechatQueryLog.module = Util.MyConstant.module_Study;
                    wechatQueryLog.subSystem = "在线学习";
                    wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    wechatQueryLog.queryString = study.type;
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
            }
        }
    }
}