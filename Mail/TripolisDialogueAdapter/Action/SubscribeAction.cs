using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripolistMailAdapter;
using TripolisDialogueAdapter.cn.tripolis.dialogue.subscription;

namespace TripolisDialogueAdapter.Action
{
    class SubscribeAction
    {
         private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SubscribeAction));
         private cn.tripolis.dialogue.subscription.SubscriptionService subscriptionService = null;
        private cn.tripolis.dialogue.subscription.AuthInfo subscriptionAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public SubscribeAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
        {
            subscriptionService = new SubscriptionService();
            subscriptionAuthInfo = new cn.tripolis.dialogue.subscription.AuthInfo
                 {
                     client = client,
                     username = userName,
                     password = password
                 };
            subscriptionService.authInfo = subscriptionAuthInfo;
            subscriptionService.Proxy = oWebProxy;
        }



        /// <summary>
        /// subscribe single email
        /// </summary>
        /// <param name="contactGroupId">contact group id</param>
        /// <param name="emailId">email id</param>
        /// <returns>publish status</returns>
        public String subscribeSingleEmail(String contactDatabaseId, String workspaceId,String emailAddress,String emailId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("subscribeSingleEmail:contactDatabaseId=" + contactDatabaseId + ",emailId=" + emailId + ",emailAddress=" + emailAddress);
            }
            String result = "";

            subscribeContactRequest request = new subscribeContactRequest();
            try
            {
                request.contactDatabase = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.contactDatabase.id = contactDatabaseId;
                request.workspace = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.workspace.id = workspaceId;

                request.contactFields = new cn.tripolis.dialogue.subscription.ContactFieldValue[1];
                ContactFieldValue contactFieldValue = new ContactFieldValue();
                contactFieldValue.name = "email";
                contactFieldValue.value = emailAddress;
                request.contactFields.SetValue(contactFieldValue, 0);

                request.directEmail = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.directEmail.id = emailId;

                request.ip = "127.0.0.1";

                
                cn.tripolis.dialogue.subscription.IDResponse response = subscriptionService.subscribeContact(request);
                String contactId = response.id;
                result = contactId;

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
