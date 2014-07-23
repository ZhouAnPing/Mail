using System;
using System.Collections;

namespace TripolistMailAdapter.Action
{        
    class ContactAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ContactAction));
        private cn.tripolis.dialogue.contact.ContactService contactService = null;
        private cn.tripolis.dialogue.contact.AuthInfo contactAuthInfo = null;
        private const String OK_RESULT = MailAdapter.OK_RESULT;
        private string userName;
        private string password;
        private string client;

         /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public ContactAction(String client, String userName, String password)
       {
           this.client = client;
           this.userName = userName;
           this.password = password;   
            contactService = new cn.tripolis.dialogue.contact.ContactService();
            contactAuthInfo = new cn.tripolis.dialogue.contact.AuthInfo
                { 
                    client = client,
                    username = userName,
                    password = password
                };
             contactService.authInfo = contactAuthInfo;
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contactDatabaseId">contact database id</param>      
        /// <param name="contactJsonList">contact list</param>
        /// <returns>contact id</returns>
        public String createContact(String contactDatabaseId, ArrayList contactJsonList)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createContact:createContactcontactDatabaseId=" + contactDatabaseId );
            }
            String result = OK_RESULT;
            ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password);
            Hashtable contactFieldTable = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);
            foreach (String str in contactJsonList)
            {
                int index = 0;
                if (!String.IsNullOrEmpty(str))
                {
                    String[] arrContact = str.Split(',');
                    cn.tripolis.dialogue.contact.CreateContactRequest request = new cn.tripolis.dialogue.contact.CreateContactRequest
                        {
                            contactDatabaseId = contactDatabaseId,
                            contactFields = new cn.tripolis.dialogue.contact.ContactFieldValue[arrContact.Length]
                        };

                    foreach (String strContact in arrContact)
                    {

                        String[] arryFiled = strContact.Split(new[] { "#" }, StringSplitOptions.None);
                        cn.tripolis.dialogue.contact.ContactFieldValue field = new cn.tripolis.dialogue.contact.ContactFieldValue
                            {
                                name = arryFiled[0].ToLower(),
                                value = arryFiled[1]
                            };

                        request.contactFields.SetValue(field, index++);
                        //add contact filed to table
                        if (contactFieldTable != null && !contactFieldTable.ContainsKey(field.name))
                        {
                            contactDatabaseFieldAction.addContactField(contactDatabaseId, field.name, field.value);

                        }
                    }

                    try
                    {
                        cn.tripolis.dialogue.contact.IDResponse response = contactService.create(request);
                        result = response.id;
                        if (logger.IsDebugEnabled)
                        {
                            logger.Debug("new contact id=" + result);
                        }
                    }
                    catch (System.Web.Services.Protocols.SoapException ex)
                    {
                        // if the error is not caused by exist id, continue the loop
                        if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                        {
                            result = ex.Detail.InnerXml;
                            if (logger.IsDebugEnabled)
                            {
                                logger.Debug("error happens in create contact, error is" + result);
                            }
                            continue;
                            // throw new Exception(ex.Detail.InnerXml);
                        }
                        result = Util.getExistId(ex.Detail);
                        if (logger.IsDebugEnabled)
                        {
                            logger.Debug("exist contact id=" + result);

                            cn.tripolis.dialogue.contact.UpdateContactRequest updateRequest = new cn.tripolis.dialogue.contact.UpdateContactRequest();
                            updateRequest.id = result;
                            updateRequest.contactFields = request.contactFields;
                            this.contactService.update(updateRequest);
                        }
                    }
                   
                   // addContactToGroup(result, contactGroupId);
                }
            }


            return result;
        }

        /// <summary>
        /// add contact to group
        /// </summary>
        /// <param name="contactId">contact Id</param>
        /// <param name="contactGroupId">contact group id</param>
        /// <returns></returns>
        private String addContactToGroup(String contactId, String contactGroupId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("addContactToGroup:contactId=" + contactId + ",contactGroupId=" + contactGroupId);
            }
            String result;
            cn.tripolis.dialogue.contact.AddToContactGroupRequest groupRequest = new cn.tripolis.dialogue.contact.AddToContactGroupRequest();

            try
            {
                cn.tripolis.dialogue.contact.ContactGroupSubscriptionRequestObject subOBject = new cn.tripolis.dialogue.contact.ContactGroupSubscriptionRequestObject();
                groupRequest.contactId = contactId;
                subOBject.contactGroupId = contactGroupId;//"MjYwMTMyNjAot_oDDCr0mA";
                subOBject.confirmed = true;
                groupRequest.contactGroupSubscriptions = new cn.tripolis.dialogue.contact.ContactGroupSubscriptionRequestObject[1];
                groupRequest.contactGroupSubscriptions.SetValue(subOBject, 0);

                cn.tripolis.dialogue.contact.IDResponse response = contactService.addToContactGroup(groupRequest);

                result = response.id;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in add contact to contact group, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);

            }
            return result;
        }

        private String getCommunicateHistory(String contactId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getCommunicateHistory:contactId=" + contactId);
            }
            String result;
            cn.tripolis.dialogue.contact.GetCommunicationHistoryRequest request = new cn.tripolis.dialogue.contact.GetCommunicationHistoryRequest();
            try
            {

                request.contactId = contactId;
                request.includeFullDetails = true;                
                cn.tripolis.dialogue.contact.CommunicationHistoryResponse response = contactService.getCommunicationHistory(request);

                result = response.message;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in add contact to contact group, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);

            }
            return result;
        }

    }
}
