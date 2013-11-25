using System;
using TripolisDialogueAdapter.cn.tripolis.dialogue.directEmail;
using TripolistMailAdapter;


namespace TripolisDialogueAdapter.Action
{
     class DirectEmailAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DirectEmailAction));
        private DirectEmailService directEmailService = null;
        private AuthInfo directEmailAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public DirectEmailAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
        {
            directEmailService = new DirectEmailService();
            directEmailAuthInfo = new AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            directEmailService.authInfo = directEmailAuthInfo;
            directEmailService.Proxy = oWebProxy;
        }



        /// <summary>
        /// Get email id by email type id and email name.
        /// </summary>
        /// <param name="emailTypeId">email type id</param>
        /// <param name="emailName">email name</param>
        /// <returns>email id</returns>
        public String getDirectEmailId(String emailTypeId, String emailName)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getEmailId:emailTypeId=" + emailTypeId + ",emailName=" + emailName);
            }

            String result = "";

            DirectEmailTypeIDRequest request = new DirectEmailTypeIDRequest();
            try
            {
                request.directEmailTypeId = emailTypeId;
                DirectEmailListResponse response = directEmailService.getByDirectEmailTypeId(request);

                foreach (DirectEmailForListing item in response.directEmails)
                {
                    if (item.name.Equals(emailName))
                    {
                        return item.id;
                    }
                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in getting email type id, error is" + result);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return result;
        }

        /// <summary>
        /// create direct email
        /// </summary>
        /// <param name="directEmailId">direct email type id</param>
        /// <param name="emailLabel">email label</param>
        /// <param name="emailName">email name</param>
        /// <param name="subject">subject</param>
        /// <param name="description">decription</param>
        /// <param name="fromName">sender name</param>
        /// <param name="fromAddress">from address</param>
        /// <param name="htmlSource">html source</param>
        /// <returns>direct email id</returns>
        public String createDirectEmail(String directEmailTypeId, String emailLabel, String emailName, String subject, String description, String fromName, String fromAddress, String htmlSource)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createDirectEmail:directEmailTypeId=" + directEmailTypeId + ",emailLabel=" + emailLabel + ",emailName=" + emailName + ",subject=" + subject + ",description=" + description + ",fromName=" + fromName + ",fromAddress=" + fromAddress);
            }
            String result;
            String emailId = getDirectEmailId(directEmailTypeId, emailName);
            //Check if the email type is already exist.
            if (!emailId.Equals(""))
            {
                this.updateDirectEmail(emailId, subject, fromName, htmlSource);
                return emailId;
            }

            CreateDirectEmailRequest request = new CreateDirectEmailRequest();
            try
            {
                request.directEmailTypeId = directEmailTypeId;
                request.label = emailLabel;
                request.name = emailName;
                request.subject = subject;
                request.description = description;
                request.fromName = fromName;
                request.fromAddress = fromAddress;
                request.htmlSource = htmlSource;
                request.textSource = htmlSource;
                
                //request.
                IDResponse response = directEmailService.create(request);

                result = response.id;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in creating direct email, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);

            }
            return result;
        }


        public String updateDirectEmail(String directEmailId,  String subject,  String fromName, String htmlSource)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createDirectEmail:directEmailId=" + directEmailId +  ",subject=" + subject  + ",fromName=" + fromName);
            }
            String result;
           


            UpdateDirectEmailRequest request = new UpdateDirectEmailRequest();
            try
            {
                request.id = directEmailId;                
                request.subject = subject;               
                request.fromName = fromName;               
                request.htmlSource = htmlSource;
                request.textSource = htmlSource;
                //request.
                IDResponse response = directEmailService.update(request);

                result = response.id;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in creating direct email, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);

            }
            return result;
        }

    }
}
