using System;
using TripolistMailAdapter.cn.tripolis.dialogue.contactDatabase;

namespace TripolistMailAdapter.Action
{
    class ContactDatabaseAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ContactDatabaseAction));
        private  ContactDatabaseService contactDatabaseService = null;
        private  AuthInfo contactDatabaseAuthInfo = null;

        /// <summary>
        /// Initial the webservice API.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        public ContactDatabaseAction(String client, String userName, String password)
        {
            contactDatabaseService = new ContactDatabaseService();
            contactDatabaseAuthInfo = new AuthInfo {client = client, username = userName, password = password};
            contactDatabaseService.authInfo = contactDatabaseAuthInfo;
        }

        /// <summary>
        /// Get all og the databases
        /// </summary>
        /// <returns></returns>
        public ContactDatabase[] getAllContactDatabase()
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getAllContactDatabase");
            }


            ServiceRequest request = new ServiceRequest();
            try
            {
                ContactDatabaseListResponse response = contactDatabaseService.getAll(request);
                return response.contactDatabases;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in creating contact database, error is" + ex.Detail.InnerXml);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
        }

        /// <summary>
        /// get database Id by database name
        /// </summary>
        /// <param name="dbName">database name</param>
        /// <returns>database id</returns>
        public String getDatabaseIdByName(String dbName)
        {
            ContactDatabase[] allDatabase = getAllContactDatabase();
            String contactDatabaseId = "";
            if (allDatabase != null)
            {
                foreach (ContactDatabase contactDatabase in allDatabase)
                {
                    if (!String.IsNullOrEmpty(dbName) && contactDatabase.label.Equals(dbName))
                    {
                        contactDatabaseId = contactDatabase.id;
                    }
                }
            }
            if (String.IsNullOrEmpty(contactDatabaseId))
            {
                throw new Exception("contact database for the customer:" + dbName + " is not exist");
            }

            return contactDatabaseId;

        }


        /// <summary>
        /// Create database by customer, and add the email to database as the primary field
        /// </summary>
        /// <param name="customerName"> customer Name</param>
        /// <returns>database ID</returns>
        public String createContactDatabase(String customerName)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createContactDdatabase:customerName=" + customerName);
            }

            ContactDatabase[] allContactDatabase = getAllContactDatabase();
            if (allContactDatabase != null)
            {
                foreach (ContactDatabase contactDatabase in allContactDatabase)
                {
                    if (!String.IsNullOrEmpty(customerName) && contactDatabase.name.Equals(customerName,StringComparison.OrdinalIgnoreCase))
                    {
                        return contactDatabase.id;
                    }
                }
            }
            String result;

            CreateContactDatabaseRequest request = new CreateContactDatabaseRequest();
            try
            {
                request.label = customerName;
                request.name = customerName;
                request.contactDatabaseFieldGroups = new CreateDatabaseFieldGroupInContactDatabase[1];
                
                CreateDatabaseFieldGroupInContactDatabase createDatabaseFieldGroupInContactDatabase = new CreateDatabaseFieldGroupInContactDatabase
                    {
                        contactDatabaseFields = new CreateDatabaseFieldInContactDatabase[2]
                    };

                CreateDatabaseFieldInContactDatabase createDatabaseFieldInContactDatabase = new CreateDatabaseFieldInContactDatabase
                    {
                        label = "Email Id",
                        name = MailAdapter.EMAIL_ID,
                        type = contactDatabaseFieldType.STRING,
                        key = true,
                        required = true,
                        inOverview = true,
                        kindOfField = kindOfField.SUMMARY
                    };
                //createDatabaseFieldInContactDatabase.defaultValue = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
                createDatabaseFieldGroupInContactDatabase.contactDatabaseFields.SetValue(createDatabaseFieldInContactDatabase, 0);

                createDatabaseFieldInContactDatabase = new CreateDatabaseFieldInContactDatabase
                    {
                        label = "email address",
                        name = MailAdapter.EMAIL,
                        type = contactDatabaseFieldType.EMAIL,
                        key = false,
                        required = true,
                        inOverview = true,
                        kindOfField = kindOfField.SUMMARY
                    };
                createDatabaseFieldGroupInContactDatabase.contactDatabaseFields.SetValue(createDatabaseFieldInContactDatabase, 1);

                request.contactDatabaseFieldGroups.SetValue(createDatabaseFieldGroupInContactDatabase, 0);

                IDResponse response = contactDatabaseService.create(request);
                result = response.id;

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in creating contact database, error is" + ex.Detail.InnerXml);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return result;

        }
    }
}
