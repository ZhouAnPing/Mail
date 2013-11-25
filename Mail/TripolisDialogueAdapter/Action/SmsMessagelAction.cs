using System;
using TripolisDialogueAdapter.cn.tripolis.dialogue.smsMessage;
using TripolistMailAdapter;


namespace TripolisDialogueAdapter.Action
{
    class SmsMessageAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SmsMessageAction));
        private SmsMessageService smsMessageService = null;
        private AuthInfo smsMessageAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public SmsMessageAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
        {
            smsMessageService = new SmsMessageService();
            smsMessageAuthInfo = new AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            smsMessageService.authInfo = smsMessageAuthInfo;
            smsMessageService.Proxy = oWebProxy;

        }



        /// <summary>
        /// Get sms id by sms type id and sms name.
        /// </summary>
        /// <param name="smsTypeId">sms type id</param>
        /// <param name="smsName">sms name</param>
        /// <returns>sms id</returns>
        public String getSmsMessageId(String smsTypeId, String smsName)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getSmsMessageId:smsTypeId=" + smsTypeId + ",smsName=" + smsName);
            }

            String result = "";

            SmsTypeIDRequest request = new SmsTypeIDRequest();
            try
            {
                request.smsTypeId = smsTypeId;
                SmsMessageListResponse response = smsMessageService.getBySmsMessageTypeId(request);

                foreach (SmsMessage item in response.smsMessages)
                {
                    if (item.name.Equals(smsName))
                    {
                        return item.id;
                    }
                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in getting sms type id, error is" + result);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return result;
        }

        /// <summary>
        /// create direct sms
        /// </summary>
        /// <param name="smsTypeId">direct sms type id</param>
        /// <param name="smsLabel">sms label</param>
        /// <param name="smsName">sms name</param>
        /// <param name="subject">subject</param>
        /// <param name="description">decription</param>
        /// <param name="fromName">sender name</param>
        /// <param name="fromAddress">from address</param>
        /// <param name="htmlSource">html source</param>
        /// <returns>direct sms id</returns>
        public String createsmsMessage(String smsTypeId, String smsLabel, String smsName, String description, String fromName, String message, String alternativeMessage)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createsmsMessage:smsTypeId=" + smsTypeId + ",smsLabel=" + smsLabel + ",smsName=" + smsName +  ",description=" + description + ",fromName=" + fromName + ",message=" + message);
            }
            String result;
            String smsId = getSmsMessageId(smsTypeId, smsName);
            //Check if the sms type is already exist.
            if (!smsId.Equals(""))
            {
                return smsId;
            }

            CreateSmsMessageRequest request = new CreateSmsMessageRequest();
            try
            {
                request.smsTypeId = smsTypeId;
                request.label = smsLabel;
                request.name = smsName;
               
                request.description = description;
                request.originator = fromName;
                request.message = message;
                request.alternativeMessage = alternativeMessage;
              
                
                //request.
                IDResponse response = smsMessageService.create(request);

                result = response.id;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in creating direct sms, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);

            }
            return result;
        }

    }
}
