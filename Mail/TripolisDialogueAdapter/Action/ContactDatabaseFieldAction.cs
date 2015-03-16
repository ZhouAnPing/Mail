using System;
using System.Collections;

namespace TripolisDialogueAdapter.Action
{
     class ContactDatabaseFieldAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ContactDatabaseFieldAction));
        private cn.tripolis.dialogue.contactDatabaseField.ContactDatabaseFieldService contactDatabaseFieldService = null;
        private cn.tripolis.dialogue.contactDatabaseField.AuthInfo contactDatabaseFieldAuthInfo = null;
        public const String EMAIL = MailAdapter.EMAIL;

        /// <summary>
        /// Initial the webservice API.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        public ContactDatabaseFieldAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
        {
            contactDatabaseFieldService = new cn.tripolis.dialogue.contactDatabaseField.ContactDatabaseFieldService();
            contactDatabaseFieldAuthInfo = new cn.tripolis.dialogue.contactDatabaseField.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            contactDatabaseFieldService.authInfo = contactDatabaseFieldAuthInfo;
            contactDatabaseFieldService.Proxy = oWebProxy;
        }

        /// <summary>
        /// Add contact Fields to database
        /// </summary>
        /// <param name="databaseId">Database Id</param>
        /// <param name="fieldName">Filed name</param>
        /// <param name="defaultValue">defaul value</param>
        /// <returns>contact field id</returns>
        public String addContactField(String databaseId, String fieldName, String defaultValue)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("addContactField:databaseId=" + databaseId + ",FieldName=" + fieldName);
            }

            String result;

            cn.tripolis.dialogue.contactDatabaseField.CreateContactDatabaseFieldRequest request = new cn.tripolis.dialogue.contactDatabaseField.CreateContactDatabaseFieldRequest();
            try
            {
                request.contactDatabaseId = databaseId;
                request.label = fieldName;
                request.name = fieldName.ToLower();
                request.type = cn.tripolis.dialogue.contactDatabaseField.contactDatabaseFieldType.STRING;
                request.defaultValue = defaultValue;
                if (fieldName.Equals(MailAdapter.PID))
                {
                    request.key = true;
                }
                else
                {
                    request.key = false;
                }
                request.required = false;
                request.inOverview = true;
                request.kindOfField = cn.tripolis.dialogue.contactDatabaseField.kindOfField.GENERAL;

                cn.tripolis.dialogue.contactDatabaseField.IDResponse response = contactDatabaseFieldService.create(request);
                result = response.id;

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in get contact database fields, error is" + ex.Detail.InnerXml);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return result;

        }



        /// <summary>
        /// get contact fields from contact database
        /// </summary>
        /// <param name="databaseId">database Id</param>
        /// <returns>contact fields</returns>
        public Hashtable getContactDatabaseFields(String databaseId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getContactDatabaseFields:databaseId=" + databaseId);
            }

            Hashtable fieldNameHt = new Hashtable();

            cn.tripolis.dialogue.contactDatabaseField.SortableContactDatabaseIDRequest request = new cn.tripolis.dialogue.contactDatabaseField.SortableContactDatabaseIDRequest();
            try
            {
                request.contactDatabaseId = databaseId;
                request.paging = new cn.tripolis.dialogue.contactDatabaseField.PagingIn();
                request.paging.pageSize = 800;
                request.paging.pageNr = 1;
                
                cn.tripolis.dialogue.contactDatabaseField.ContactDatabaseFieldListResponse response = contactDatabaseFieldService.getByContactDatabaseId(request);

                foreach (cn.tripolis.dialogue.contactDatabaseField.ContactDatabaseField item in response.contactDatabaseFields)
                {
                   // logger.Debug(item.id + "-" + item.name);
                    fieldNameHt.Add(item.name, item.id);
                }


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in get contact database fields, error is" + ex.Detail.InnerXml);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return fieldNameHt;
        }

        /// <summary>
        /// get contact database email Id
        /// </summary>
        /// <param name="contactDatabaseId">database id</param>
        /// <returns>email id</returns>
        public String getDatabaseEmailId(String contactDatabaseId)
        {
            Hashtable ht = getContactDatabaseFields(contactDatabaseId);

            String toEmailFieldId; //Properties.Settings.Default.toEmailFieldId;// "MTExMzYxMTEfCGWCBaFKyA";
            if (ht[EMAIL] != null)
            {
                toEmailFieldId = ht[EMAIL].ToString();
            }
            else
            {
                throw new Exception("Email Id is not exist for database:" + contactDatabaseId);
            }
            return toEmailFieldId;
        }
    }
}
