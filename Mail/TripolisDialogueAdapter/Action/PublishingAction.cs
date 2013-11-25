using System;
using TripolistMailAdapter;
using TripolisDialogueAdapter.cn.tripolis.dialogue.publish;

namespace TripolisDialogueAdapter.Action
{
    class PublishingAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PublishingAction));
        private cn.tripolis.dialogue.publish.PublishingService publishingService = null;
        private cn.tripolis.dialogue.publish.AuthInfo publishingAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public PublishingAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
       {           
            publishingService = new cn.tripolis.dialogue.publish.PublishingService();
            publishingAuthInfo = new cn.tripolis.dialogue.publish.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            publishingService.authInfo = publishingAuthInfo;
            publishingService.Proxy = oWebProxy;
        }



        /// <summary>
        /// publishing email
        /// </summary>
        /// <param name="contactGroupId">contact group id</param>
        /// <param name="emailId">email id</param>
        /// <returns>publish status</returns>
        public String publishingEmail(String contactGroupId, String emailId,DateTime scheduleAt)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("publishingEmail:contactGroupId=" + contactGroupId + ",emailId=" + emailId);
            }
            String result;

            cn.tripolis.dialogue.publish.PublishEmailRequest request = new cn.tripolis.dialogue.publish.PublishEmailRequest();
            try
            {
                request.contactGroupId = contactGroupId;
                request.directEmailId = emailId;
                request.mailsPerHour = 50000;
                request.scheduleAtSpecified = true;
                request.scheduleAt = scheduleAt;
                
                // request.
                cn.tripolis.dialogue.publish.IDResponse response = publishingService.publishEmail(request);

                result = response.id;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in publishing direct email, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);
            }
            return result;
        }

        public String publishTransactionalEmail(String contactId, String emailId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("publishTransactionalEmail:contactId=" + contactId + ",emailId=" + emailId);
            }
            String result;

            cn.tripolis.dialogue.publish.PublishTransactionalEmailRequest request = new cn.tripolis.dialogue.publish.PublishTransactionalEmailRequest();
            try
            {
                request.contactId = contactId;
                request.directEmailId = emailId;              


                // request.
                cn.tripolis.dialogue.publish.IDResponse response = publishingService.publishTransactionalEmail(request);

                result = response.id;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in publishing direct email, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);
            }
            return result;
        }

        public Job getPublishStatus(String publishId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getPublishStatus:publishId=" + publishId);
            }
            Job result;

            cn.tripolis.dialogue.publish.IDRequest request = new cn.tripolis.dialogue.publish.IDRequest();
            try
            {
                request.id = publishId;


                // request.
                cn.tripolis.dialogue.publish.JobResponse response = publishingService.getById(request);

                result = response.job;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    //result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in publishing direct email, error is" + ex.Detail.InnerXml);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = null;
            }
            return result;
        }
    }
}
