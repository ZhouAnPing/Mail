using System;

namespace TripolistMailAdapter.Action
{
    class PublishingAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PublishingAction));
        private cn.tripolis.dialogue.publishing.PublishingService publishingService = null;
        private cn.tripolis.dialogue.publishing.AuthInfo publishingAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public PublishingAction(String client, String userName, String password)
       {           
            publishingService = new cn.tripolis.dialogue.publishing.PublishingService();
            publishingAuthInfo = new cn.tripolis.dialogue.publishing.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            publishingService.authInfo = publishingAuthInfo;
        }



        /// <summary>
        /// publishing email
        /// </summary>
        /// <param name="contactGroupId">contact group id</param>
        /// <param name="emailId">email id</param>
        /// <returns>publish status</returns>
        public String publishingEmail(String contactGroupId, String emailId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("publishingEmail:contactGroupId=" + contactGroupId + ",emailId=" + emailId);
            }
            String result;

            cn.tripolis.dialogue.publishing.PublishEmailRequest request = new cn.tripolis.dialogue.publishing.PublishEmailRequest();
            try
            {
                request.contactGroupId = contactGroupId;
                request.directEmailId = emailId;
                request.mailsPerHour = 50000;             
                
                // request.
                cn.tripolis.dialogue.publishing.IDResponse response = publishingService.publishEmail(request);

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

            cn.tripolis.dialogue.publishing.PublishTransactionalEmailRequest request = new cn.tripolis.dialogue.publishing.PublishTransactionalEmailRequest();
            try
            {
                request.contactId = contactId;
                request.directEmailId = emailId;              


                // request.
                cn.tripolis.dialogue.publishing.IDResponse response = publishingService.publishTransactionalEmail(request);

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
    }
}
