using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripolistMailAdapter.Action;
using System.Collections;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using TripolistMailAdapter.cn.tripolis.dialogue.export;
using TripolistMailAdapter.cn.tripolis.dialogue.contactGroup;


namespace TripolistMailAdapter
{
    public class MailAdapter
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MailAdapter));
        private cn.tripolis.dialogue.export.ExportService exportService = null;
        private cn.tripolis.dialogue.import.ImportService importService = null;
        private cn.tripolis.dialogue.export.AuthInfo exportAuthInfo = null;
        private cn.tripolis.dialogue.import.AuthInfo importAuthInfo = null;

        public const String OK_RESULT = "OK";
        public const String SUBJECT = "subject";
        public const String FROMMAIL = "from";
        public const String EMAIL = "email";
        public const String EMAIL_ID = "emailid";
        private string userName;
        private string password;
        private string client;


       /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public MailAdapter(String client, String userName, String password)
        {
            this.client = client;
            this.userName = userName;
            this.password = password;

            exportService = new cn.tripolis.dialogue.export.ExportService();
            exportAuthInfo = new cn.tripolis.dialogue.export.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
           exportService.authInfo = exportAuthInfo;

            importService = new cn.tripolis.dialogue.import.ImportService();
            importAuthInfo = new cn.tripolis.dialogue.import.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
           importService.authInfo = importAuthInfo;

        }      
      
        /// <summary>
        /// send mail
        /// </summary>
        /// <param name="customerName">customerName</param>
        /// <param name="mailData">mailData</param>
        /// <returns></returns>
        public String sendMail(String customerName,MailData mailData)
        {
            logger.Debug("************sendMail***************");
          //  System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
            String result = OK_RESULT;
            try
            {
                if (String.IsNullOrEmpty(customerName))
                {
                    return "No customer Name";
                }
                if (mailData == null || mailData.ContactJsonList == null || mailData.ContactJsonList.Count <= 0)
                {
                    return "No Contact";
                }
               ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client,userName,password);
               String contactDatabaseId = contactDatabaseAction.createContactDatabase(customerName);


               WorkspaceAction workspaceAction = new WorkspaceAction(client, userName, password);
               String workspaceId = workspaceAction.createWorkspace(contactDatabaseId, customerName);

               ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password);
               Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);

               String toEmailFieldId; //Properties.Settings.Default.toEmailFieldId;// "MTExMzYxMTEfCGWCBaFKyA";
               if (ht[EMAIL] != null)
               {
                   toEmailFieldId = ht[EMAIL].ToString();
               }
               else
               {
                   return "Database:" + customerName+" is not exist";
               }

               const string pattern = "\\{.+?\\}";
               List<string> tempList = Regex.Matches(mailData.Content, pattern).Cast<Match>().Select(a => a.Value).ToList();
                foreach (string str in tempList)
                {
                    mailData.Content = mailData.Content.Replace(str, str.ToLower());
                }
                //查找<A的html标记，如果里面没有title属性，则增加这个属性.
                mailData.Content = Util.addTitleInAFlagHtml(mailData.Content);


                String sequence = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password);

               // ContactGroup[] temp = contactGroupAction.getContactGroup(contactDatabaseId);

                String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, "IPSOS Mail Group" + sequence, "ipsos_mail_group" + sequence);

               String importId = this.importContact(contactDatabaseId, contactGroupId, mailData);
               int sleepCnt = 0;
               while (!this.getImportStatus(importId).Equals(cn.tripolis.dialogue.import.importStatus.ENDED.ToString()))
               {
                   if (sleepCnt > 10)
                   {
                       return "fail to import contacts";
                   }
                   System.Threading.Thread.Sleep(10000);
                   sleepCnt++;
               }

                DirectEmailTypeAction directEmailTypeAction = new DirectEmailTypeAction(client, userName, password);
                String directEmailTypeId = directEmailTypeAction.createDirectEmailType(workspaceId, "IPSOS Email Type for" + customerName, "ipsos_mail_type_" + customerName, mailData.FromAddress, mailData.SendName, toEmailFieldId);
                
                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password);
                String directEmailId = directEmailAction.createDirectEmail(directEmailTypeId, "IPSOS Mail" + sequence, "ipsos_mail" + sequence, mailData.Subject, "Send mail by IPSOS", mailData.SendName, mailData.FromAddress, mailData.Content);
                
                PublishingAction publishingAction = new PublishingAction(client, userName, password);
                String publishId = publishingAction.publishingEmail(contactGroupId, directEmailId);

                logger.Debug("publishId=" + publishId);

            }
            catch (Exception ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);

            }
            logger.Debug("************end sendMail ************");
            return result;
        }
       
        /// <summary>
        /// Get feedback information
        /// </summary>
        /// <param name="customerName">customer Name</param>
        /// <param name="startTime">StartTime</param>
        /// <param name="endTime">End time</param>        
        /// <returns>all of the information, including sent, bounced, 
        /// clicked and opened</returns>
        public String getFeedbackInfo(String customerName, DateTime startTime, DateTime endTime)
        {
           // System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
            return getFeedbackInfo(customerName, startTime, endTime, null);
        }
       
        /// <summary>
        /// Get feedback information
        /// </summary>
        /// <param name="customerName">customer Name</param>
        /// <param name="startTime">StartTime</param>
        /// <param name="endTime">End time</param>   
        /// <param name="searchConditions">search Conditions</param>
        /// <returns>all of the information, including sent, bounced, 
        /// clicked and opened</returns>
        public String getFeedbackInfo(String customerName, DateTime startTime, DateTime endTime, Hashtable searchConditions)
        {
            XDocument xml = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
            DateTime tempStartTime = endTime.AddDays(-1);
            int i = 1;
            while (endTime > startTime)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("queryFeedbackInfo at the " + i + " times");
                    i++;
                }
                if (tempStartTime < startTime)
                {
                    tempStartTime = startTime;
                }
                xml = Util.mergeXml(xml, this.queryFeedbackInfo(customerName, tempStartTime, endTime, searchConditions));

                //if (tempStartTime < startTime)
                //{
                //    break;
                //}
                endTime = tempStartTime;
                tempStartTime = endTime.AddDays(-1);
            }
            if (logger.IsDebugEnabled)
            {
                logger.Debug("Feedback Information \n" + xml);
            }
            return xml.ToString();
        }
      
        /// <summary>
        /// Get feedback information
        /// </summary>
        /// <param name="customerName">customer Name</param>
        /// <param name="startTime">StartTime</param>
        /// <param name="endTime">End time</param>
        /// <param name="searchConditions">searchConditions</param>
        /// <returns>all of the information, including sent, bounced, 
        /// clicked and opened</returns>
        private XDocument queryFeedbackInfo(String customerName, DateTime startTime, DateTime endTime, Hashtable  searchConditions)
        {
            //String BouncedFormat = "email"; "name"; "Email address of bounced message"; "Bounce date"; "Bounce code"; "Bounce decription"; "Hard bounce"; "Job ID";
            //String OpenedFormat  = "email"; "name"; "Rendered"; "Opened"; "IP address"; "Browser"; "OS"; "Job ID";
            //String ClickedFormat = "email"; "name"; "clicked";  "linkid"; "IP address"; "browser";       "Job ID"
            if (logger.IsDebugEnabled)
            {
                logger.Debug("queryFeedbackInfo:customerName=" + customerName + ",startTime=" + startTime.ToLongTimeString() + ",endTime=" + endTime.ToLongTimeString());
            }
            String result;            
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
            try
            {
                Action.ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client, userName, password);
                String contactDatabaseId = contactDatabaseAction.getDatabaseIdByName(customerName);

                ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password);
                Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);
                //String toEmailFieldId = contactDatabaseFieldAction.getDatabaseEmailId(contactDatabaseId);

                cn.tripolis.dialogue.export.ContactExportRequest request = new cn.tripolis.dialogue.export.ContactExportRequest
                    {
                        contactDatabaseId = contactDatabaseId,
                        timeRange = new cn.tripolis.dialogue.export.TimeRange { startTime = startTime, endTime = endTime }
                    };

                var xroot = new XElement("FeedbackReport");

                // logger.Debug("******exportSent*****");
                request.returnContactFields = new cn.tripolis.dialogue.export.ReturnContactFields
                    {
                        contactDatabaseFieldIds = new String[ht.Keys.Count]
                    };
                int index = 0;
                foreach (String value in ht.Values)
                {
                    request.returnContactFields.contactDatabaseFieldIds.SetValue(value, index++);
                }
                RawDataResponse response = null;// exportService.exportSent(request);
                XElement element = null;
                //  response = exportService.exportSent(request);
                // result = System.Text.Encoding.UTF8.GetString(response.data);
                // XElement element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportSent", searchConditions);
                // xroot.Add(element);

                request.returnContactFields.contactDatabaseFieldIds = null;


                request.returnContactFields = new cn.tripolis.dialogue.export.ReturnContactFields();
                request.returnContactFields.contactDatabaseFieldNames = new string[] { EMAIL_ID,EMAIL};

               // request.returnContactFields.returnAllContactFields = true;

             //   request.returnContactFields.returnAllContactFieldsSpecified = true;


                logger.Debug("******exportBounced*****");
                response = exportService.exportBounced(request);
                result = System.Text.Encoding.UTF8.GetString(response.data);
                element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportBounced", searchConditions);
                xroot.Add(element);


                logger.Debug("******exportOpened*****");
                response = exportService.exportOpened(request);
                result = System.Text.Encoding.UTF8.GetString(response.data);
                element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportOpened", searchConditions);
                xroot.Add(element);


                logger.Debug("******exportClicked*****");
                response = exportService.exportClicked(request);
                result = System.Text.Encoding.UTF8.GetString(response.data);
                element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportClicked", searchConditions);
                xroot.Add(element);
                doc.Add(xroot);
                result = doc.ToString();
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                result = ex.Detail.InnerXml;
                throw new Exception(result);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                throw new Exception(result);
            }
            if (logger.IsDebugEnabled)
            {
                logger.Debug(result);
            }
            return doc;
        }

        private String getImportStatus(String importId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getImportStatus:importId=" + importId);
            }

            String result = OK_RESULT;

            cn.tripolis.dialogue.import.ImportIDRequest request = new cn.tripolis.dialogue.import.ImportIDRequest();
            try
            {

                request.importId = importId;
                cn.tripolis.dialogue.import.ImportStatusResponse response = importService.getImportStatus(request);
                result = response.importStatus.ToString();               

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
        /// Import contact to database
        /// </summary>
        /// <param name="contactDatabaseId">contact database id</param>
        /// <param name="contactGroupId">contact group id</param>
        /// <param name="mailData">mail data</param>
        private String importContact(String contactDatabaseId, String contactGroupId, MailData mailData)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("importContact:contactDatabaseId=" + contactDatabaseId);
            }

            String result = OK_RESULT;
            ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password);
            Hashtable contactFieldTable = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);

            cn.tripolis.dialogue.import.ImportContactsRequest request = new cn.tripolis.dialogue.import.ImportContactsRequest();
            try
            {
              
                //Prepare contact CSV
                StringBuilder sb = new StringBuilder();
                //String[] contactFieldNames = null;

                int index = 0;
                foreach (String contactList in mailData.ContactJsonList)
                {
                    String[] contactFields = contactList.Split(',');
                    //init the CSV header
                    if (index == 0)
                    {
                        // contactFieldNames = new String[contactFields.Length];
                        foreach (String field in contactFields)
                        {
                            String fielName = field.Split(new[] { "#" }, StringSplitOptions.None)[0];
                            //add contact filed to table
                            if (contactFieldTable != null && !contactFieldTable.ContainsKey(fielName.ToLower()))
                            {
                                contactDatabaseFieldAction.addContactField(contactDatabaseId, fielName, "");

                            }
                            sb.Append("\"").Append(fielName).Append("\"").Append(";");
                        }

                        if (contactFieldTable != null && !contactFieldTable.ContainsKey(FROMMAIL.ToLower()))
                        {
                            contactDatabaseFieldAction.addContactField(contactDatabaseId, FROMMAIL, "");

                        }
                        sb.Append("\"").Append(FROMMAIL).Append("\"").Append(";");

                        if (contactFieldTable != null && !contactFieldTable.ContainsKey(SUBJECT.ToLower()))
                        {
                            contactDatabaseFieldAction.addContactField(contactDatabaseId, SUBJECT, "");

                        }
                        sb.Append("\"").Append(SUBJECT).Append("\"").Append(";");

                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("\n");
                    }
                    foreach (String field in contactFields)
                    {

                        sb.Append("\"").Append(field.Split(new[] { "#" }, StringSplitOptions.None)[1]).Append("\"").Append(";");

                    }
                    sb.Append("\"").Append(mailData.FromAddress).Append("\"").Append(";");
                    sb.Append("\"").Append(mailData.Subject).Append("\"").Append(";");

                    sb.Remove(sb.Length - 1, 1);

                    sb.Append("\n");
                    index++;
                }

                request.reportReceiverAddress = mailData.ReplyAddress;
                request.importMode = cn.tripolis.dialogue.import.importMode.SYNCHRONIZE_GROUP;
                request.contactGroupIds = new[] { contactGroupId };
                request.extension = cn.tripolis.dialogue.import.fileExtension.CSV;
                request.fileName = "Contacts.csv";
                request.importFile = System.Text.Encoding.UTF8.GetBytes(sb.ToString());//System.IO.File.ReadAllBytes("../../Contacts_new.csv");
                cn.tripolis.dialogue.import.ImportIDResponse response = importService.importContacts(request);
                result = response.importId;

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
        /// Send single mail
        /// </summary>
        /// <param name="projectName">projectName</param>
        /// <param name="emailId">emailId</param>
        /// <param name="fromName">sender</param>
        /// <param name="emailFrom">emailFrom</param>
        /// <param name="emailTo">emailTo</param>
        /// <param name="emailSubject">emailSubject</param>
        /// <param name="emailBody">emailBody</param>
        /// <returns></returns>       

        private String sendSingleEmail_Old(String projectName, String emailId, String fromName, String emailFrom, String emailTo, String emailSubject, String emailBody)
        {
            logger.Debug("************send Single Mail***************");
            String result = OK_RESULT;
            try
            {
                if (String.IsNullOrEmpty(projectName))
                {
                    return "No project Name";
                }
                if (String.IsNullOrEmpty(emailId))
                {
                    return "No Email Id";
                }
                ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client, userName, password);
                String contactDatabaseId = contactDatabaseAction.createContactDatabase(projectName);

                WorkspaceAction workspaceAction = new WorkspaceAction(client, userName, password);
                String workspaceId = workspaceAction.createWorkspace(contactDatabaseId, projectName);

                ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password);
                Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);
                String toEmailFieldId; //Properties.Settings.Default.toEmailFieldId;// "MTExMzYxMTEfCGWCBaFKyA";
                if (ht[EMAIL] != null)
                {
                    toEmailFieldId = ht[EMAIL].ToString();
                }
                else
                {
                    return "Database:" + projectName + " is not exist";
                }
                const string pattern = "\\{.+?\\}";
                List<string> tempList = Regex.Matches(emailBody, pattern).Cast<Match>().Select(a => a.Value).ToList();
                foreach (string str in tempList)
                {
                    emailBody = emailBody.Replace(str, str.ToLower());
                }

                //查找<A的html标记，如果里面没有title属性，则增加这个属性.
                emailBody = Util.addTitleInAFlagHtml(emailBody);

                String sequence = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password);
                String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, emailId, emailId);

                DirectEmailTypeAction directEmailTypeAction = new DirectEmailTypeAction(client, userName, password);
                String directEmailTypeId = directEmailTypeAction.createDirectEmailType(workspaceId, "IPSOS Email Type for " + projectName, "ipsos_mail_type_" + projectName, emailFrom, fromName, toEmailFieldId);

                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password);
                String directEmailId = directEmailAction.createDirectEmail(directEmailTypeId, "IPSOS Mail" + sequence, "ipsos_mail" + sequence, emailSubject, "Send mail by IPSOS", fromName, emailFrom, emailBody);


                cn.tripolis.dialogue.subscription.SubscriptionService subscriptionService = new cn.tripolis.dialogue.subscription.SubscriptionService();
                cn.tripolis.dialogue.subscription.AuthInfo subscriptionAuthInfo = new cn.tripolis.dialogue.subscription.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
                subscriptionService.authInfo = subscriptionAuthInfo;
                cn.tripolis.dialogue.subscription.subscribeContactRequest request = new cn.tripolis.dialogue.subscription.subscribeContactRequest();
                request.contactDatabase = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.contactDatabase.id = contactDatabaseId;
                request.workspace = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.workspace.id = workspaceId;

                request.contactFields = new cn.tripolis.dialogue.subscription.ContactFieldValue[2];
                cn.tripolis.dialogue.subscription.ContactFieldValue contactFieldValue = new cn.tripolis.dialogue.subscription.ContactFieldValue();
                contactFieldValue.name = EMAIL;
                contactFieldValue.value = emailTo;
                request.contactFields.SetValue(contactFieldValue, 0);

                contactFieldValue = new cn.tripolis.dialogue.subscription.ContactFieldValue();
                contactFieldValue.name = EMAIL_ID;
                contactFieldValue.value = emailId;
                request.contactFields.SetValue(contactFieldValue, 1);

                request.directEmail = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.directEmail.id = directEmailId;

                request.ip = "127.0.0.1";

                request.contactGroupSubscriptions = new cn.tripolis.dialogue.subscription.ContactGroupSubscription[1];
                cn.tripolis.dialogue.subscription.ContactGroupSubscription contactGroupSubscription = new cn.tripolis.dialogue.subscription.ContactGroupSubscription();
                contactGroupSubscription.confirmed = true;
                contactGroupSubscription.contactGroup = new cn.tripolis.dialogue.subscription.IdNameModel();
                contactGroupSubscription.contactGroup.id = contactGroupId;
                request.contactGroupSubscriptions.SetValue(contactGroupSubscription, 0);

                subscriptionService.subscribeContact(request);
                //result = response.id;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);
                // return ex.Message;

            }
            logger.Debug("************end sendMail ************");
            return result;
        }

        /// <summary>
        /// Send single mail
        /// </summary>
        /// <param name="projectName">projectName</param>
        /// <param name="emailId">emailId</param>
        /// <param name="fromName">sender</param>
        /// <param name="emailFrom">emailFrom</param>
        /// <param name="emailTo">emailTo</param>
        /// <param name="emailSubject">emailSubject</param>
        /// <param name="emailBody">emailBody</param>
        /// <returns></returns>  
        public String sendSingleEmail(String projectName, String emailId, String fromName, String emailFrom, String emailTo, String emailSubject, String emailBody)
        {
            logger.Debug("************send Single Mail***************");
           // System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
            String result = OK_RESULT;
            try
            {
                if (String.IsNullOrEmpty(projectName))
                {
                    return "No project Name";
                }
                if (String.IsNullOrEmpty(emailId))
                {
                    return "No Email Id";
                }
                if (String.IsNullOrEmpty(emailFrom))
                {
                    return "No from Address";
                }
                if (String.IsNullOrEmpty(emailTo))
                {
                    return "No to Address";
                }
                ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client, userName, password);
                String contactDatabaseId = contactDatabaseAction.createContactDatabase(projectName);

                WorkspaceAction workspaceAction = new WorkspaceAction(client, userName, password);
                String workspaceId = workspaceAction.createWorkspace(contactDatabaseId, projectName);

                ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password);
                Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);
                String toEmailFieldId; //Properties.Settings.Default.toEmailFieldId;// "MTExMzYxMTEfCGWCBaFKyA";
                if (ht[EMAIL] != null)
                {
                    toEmailFieldId = ht[EMAIL].ToString();
                }
                else
                {
                    return "Database:" + projectName + " is not exist";
                }

                const string pattern = "\\{.+?\\}";
                List<string> tempList = Regex.Matches(emailBody, pattern).Cast<Match>().Select(a => a.Value).ToList();
                foreach (string str in tempList)
                {
                    emailBody = emailBody.Replace(str, str.ToLower());
                }
                //查找<A的html标记，如果里面没有title属性，则增加这个属性.
                emailBody = Util.addTitleInAFlagHtml(emailBody);

                Random Rdm = new Random();
                int iRdm = Rdm.Next(10000, 99999);

                String sequence = iRdm.ToString()+"_"+ DateTime.Now.ToString("yyyyMMddHHmmssfffffff") ;
               //  ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password);
                //String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, emailId, emailId);
                ContactAction contactAction = new ContactAction(client, userName, password);
                ArrayList list = new ArrayList();
                String jsonStr = EMAIL_ID + "#" + emailId + "," + EMAIL + "#" + emailTo + "," + FROMMAIL + "#" + emailFrom + "," + SUBJECT + "#" + emailSubject;
                list.Add(jsonStr);
                String contactId = contactAction.createContact(contactDatabaseId, list);

                DirectEmailTypeAction directEmailTypeAction = new DirectEmailTypeAction(client, userName, password);
                String directEmailTypeId = directEmailTypeAction.createDirectEmailType(workspaceId, "IPSOS Email Type for " + projectName, "ipsos_mail_type_" + projectName, emailFrom, fromName, toEmailFieldId);
            //    String directEmailTypeId = directEmailTypeAction.createDirectEmailType(workspaceId, emailId, emailId, emailFrom, fromName, toEmailFieldId);

                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password);
                String directEmailId = directEmailAction.createDirectEmail(directEmailTypeId, "IPSOS Mail" + sequence, "ipsos_mail" + sequence, emailSubject, "Send mail by IPSOS", fromName, emailFrom, emailBody);
                //String directEmailId = directEmailAction.createDirectEmail(directEmailTypeId, emailId, emailId, emailSubject, "Send mail by IPSOS", fromName, emailFrom, emailBody);

                PublishingAction publishingAction = new PublishingAction(client, userName, password);
                String publishId = publishingAction.publishTransactionalEmail(contactId, directEmailId);

                logger.Debug("publishId=" + publishId);
               
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);
                // return ex.Message;

            }
            logger.Debug("************end sendMail ************");
            return result;
        }
    }

  

}
