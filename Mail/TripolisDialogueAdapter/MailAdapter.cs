using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using TripolisDialogueAdapter.Action;
using TripolistMailAdapter;
using TripolisDialogueAdapter.cn.tripolis.dialogue.export;



namespace TripolisDialogueAdapter
{
    public class MailAdapter
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MailAdapter));
        private cn.tripolis.dialogue.export.ExportService exportService = null;
        private cn.tripolis.dialogue.import.ImportService importService = null;
        private cn.tripolis.dialogue.export.AuthInfo exportAuthInfo = null;
        private cn.tripolis.dialogue.import.AuthInfo importAuthInfo = null;

        public const String OK_RESULT = "OK";
        public const String EMAIL = "email";
        public const String PID = "pid";
        private string userName;
        private string password;
        private string client;
        public System.Net.WebProxy oWebProxy;

        /// <summary>
        /// Initial the webservice API.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        public MailAdapter(String client, String userName, String password , System.Net.WebProxy oWebProxy)
        {
            this.client = client;
            this.userName = userName;
            this.password = password;
            this.oWebProxy = oWebProxy;
            exportService = new cn.tripolis.dialogue.export.ExportService();
            exportAuthInfo = new cn.tripolis.dialogue.export.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            exportService.authInfo = exportAuthInfo;
            exportService.Proxy = oWebProxy;

            importService = new cn.tripolis.dialogue.import.ImportService();
            importAuthInfo = new cn.tripolis.dialogue.import.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            importService.authInfo = importAuthInfo;
            importService.Proxy = oWebProxy;
        }
        /// <summary>
        /// Send single mail
        /// </summary>
        /// <param name="databaseName">database Name</param>
        /// <param name="pid">Primary Key</param>
        /// <param name="fromName">sender</param>
        /// <param name="emailFrom">emailFrom</param>
        /// <param name="emailTo">emailTo</param>
        /// <param name="emailSubject">emailSubject</param>
        /// <param name="emailBody">emailBody</param>
        /// <returns></returns>  
        public String sendSingleEmail(String databaseName, String pid, String fromName, String emailFrom, String emailTo, String emailSubject, String emailBody)
        {
            logger.Debug("************send Single Mail***************");
            String result = OK_RESULT;
            try
            {
                if (String.IsNullOrEmpty(databaseName))
                {
                    return "No project Name";
                }
                if (String.IsNullOrEmpty(pid))
                {
                    return "No primary Id";
                }
                if (String.IsNullOrEmpty(emailFrom))
                {
                    return "No from Address";
                }
                if (String.IsNullOrEmpty(emailTo))
                {
                    return "No to Address";
                }
                ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client, userName, password, oWebProxy);
                String contactDatabaseId = contactDatabaseAction.createContactDatabase(databaseName);

                WorkspaceAction workspaceAction = new WorkspaceAction(client, userName, password, oWebProxy);
                String workspaceId = workspaceAction.createWorkspace(contactDatabaseId, databaseName);

                ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password, oWebProxy);
                Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);
                String toEmailFieldId; //Properties.Settings.Default.toEmailFieldId;// "MTExMzYxMTEfCGWCBaFKyA";
                if (ht[EMAIL] != null)
                {
                    toEmailFieldId = ht[EMAIL].ToString();
                }
                else
                {
                    return "Database:" + databaseName + " is not exist";
                }

                const string pattern = "\\{.+?\\}";
                List<string> tempList = Regex.Matches(emailBody, pattern).Cast<Match>().Select(a => a.Value).ToList();
                foreach (string str in tempList)
                {
                    emailBody = emailBody.Replace(str, str.ToLower());
                }
                //查找<A的html标记，如果里面没有title属性，则增加这个属性.
                emailBody = Util.addTitleInAFlagHtml(emailBody);

                String sequence = DateTime.Now.ToString("yyyyMMddhhmmss");
                //  ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password);
                //String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, emailId, emailId);
                ContactAction contactAction = new ContactAction(client, userName, password, oWebProxy);
                ArrayList list = new ArrayList();
                String jsonStr = PID + "#" + pid + "," + EMAIL + "#" + emailTo;
                list.Add(jsonStr);
                String contactId = contactAction.createContact(contactDatabaseId, list);

                DirectEmailTypeAction directEmailTypeAction = new DirectEmailTypeAction(client, userName, password, oWebProxy);
                String directEmailTypeId = directEmailTypeAction.createDirectEmailType(workspaceId, "Email Type for " + databaseName, "mail_type_" + databaseName, emailFrom, fromName, toEmailFieldId);

                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
                String directEmailId = directEmailAction.createDirectEmail(directEmailTypeId, "Mail" + sequence, "mail" + sequence, emailSubject, "Send mail", fromName, emailFrom, emailBody);

                PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
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
        /// <summary>
        /// send batch mail
        /// </summary>
        /// <param name="databaseName">databaseName</param>
        /// <param name="mailData">mailData</param>
        /// <returns></returns>
        public String sendBatchMail(String databaseName, MailData mailData)
        {
            logger.Debug("************send Batch Mail***************");
            String result = OK_RESULT;
            try
            {
                if (String.IsNullOrEmpty(databaseName))
                {
                    return "No database Name";
                }
                if (mailData == null || mailData.ContactJsonList == null || mailData.ContactJsonList.Count <= 0)
                {
                    return "No Contact";
                }
                ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client, userName, password, oWebProxy);
                String contactDatabaseId = contactDatabaseAction.createContactDatabase(databaseName);


                WorkspaceAction workspaceAction = new WorkspaceAction(client, userName, password, oWebProxy);
                String workspaceId = workspaceAction.createWorkspace(contactDatabaseId, databaseName);

                ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password, oWebProxy);
                Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);

                String toEmailFieldId; //Properties.Settings.Default.toEmailFieldId;// "MTExMzYxMTEfCGWCBaFKyA";
                if (ht[EMAIL] != null)
                {
                    toEmailFieldId = ht[EMAIL].ToString();
                }
                else
                {
                    return "Database:" + databaseName + " is not exist";
                }

                const string pattern = "\\{.+?\\}";
                List<string> tempList = Regex.Matches(mailData.mailBody, pattern).Cast<Match>().Select(a => a.Value).ToList();
                foreach (string str in tempList)
                {
                    mailData.mailBody = mailData.mailBody.Replace(str, str.ToLower());
                }
                //查找<A的html标记，如果里面没有title属性，则增加这个属性.
                mailData.mailBody = Util.addTitleInAFlagHtml(mailData.mailBody);


                String sequence = DateTime.Now.ToString("yyyyMMddhhmmss");
                ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password, oWebProxy);

                // ContactGroup[] temp = contactGroupAction.getContactGroup(contactDatabaseId);

                String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, "Mail Group" + sequence, "mail_group" + sequence);

                this.importContact(contactDatabaseId, contactGroupId, mailData);

                DirectEmailTypeAction directEmailTypeAction = new DirectEmailTypeAction(client, userName, password, oWebProxy);
                String directEmailTypeId = directEmailTypeAction.createDirectEmailType(workspaceId, "Email Type for" + databaseName, "mail_type_" + databaseName, mailData.fromAddress, mailData.sender, toEmailFieldId);

                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
                String directEmailId = directEmailAction.createDirectEmail(directEmailTypeId, "Mail" + sequence, "mail" + sequence, mailData.subject, "Send mail", mailData.sender, mailData.fromAddress, mailData.mailBody);

                PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
                String publishId = publishingAction.publishingEmail(contactGroupId, directEmailId, DateTime.Now);

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
        /// Send single sms
        /// </summary>
        /// <param name="databaseName">database Name</param>      
        /// <param name="pid">primary key</param>  
        /// <param name="fromName">sender</param>
        /// <param name="emailFrom">emailFrom</param>
        /// <param name="emailTo">emailTo</param>
        /// <param name="emailSubject">emailSubject</param>
        /// <param name="emailBody">emailBody</param>
        /// <returns></returns>  
        public String sendSingleSms(String databaseName, String pid, String fromName, String smsTo, String message, String alternativeMessage)
        {
            logger.Debug("************send Single Mail***************");
            String result = OK_RESULT;
            try
            {
                if (String.IsNullOrEmpty(databaseName))
                {
                    return "No database Name";
                }
                if (String.IsNullOrEmpty(pid))
                {
                    return "No primary key";
                }

                if (String.IsNullOrEmpty(smsTo))
                {
                    return "No to Address";
                }
                ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client, userName, password, oWebProxy);
                String contactDatabaseId = contactDatabaseAction.createContactDatabase(databaseName);

                WorkspaceAction workspaceAction = new WorkspaceAction(client, userName, password, oWebProxy);
                String workspaceId = workspaceAction.createWorkspace(contactDatabaseId, databaseName);

                ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password, oWebProxy);
                Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);
                String toEmailFieldId; //Properties.Settings.Default.toEmailFieldId;// "MTExMzYxMTEfCGWCBaFKyA";
                if (ht[EMAIL] != null)
                {
                    toEmailFieldId = ht[EMAIL].ToString();
                }
                else
                {
                    return "Database:" + databaseName + " is not exist";
                }

                String sequence = DateTime.Now.ToString("yyyyMMddhhmmss");
                //  ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password);
                //String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, emailId, emailId);
                ContactAction contactAction = new ContactAction(client, userName, password, oWebProxy);
                ArrayList list = new ArrayList();
                String jsonStr = PID + "#" + pid + "," + EMAIL + "#" + "" + "," + "Mobile" + "#" + smsTo;
                list.Add(jsonStr);
                String contactId = contactAction.createContact(contactDatabaseId, list);

                SmsTypeAction smsTypeAction = new SmsTypeAction(client, userName, password, oWebProxy);
                String smsTypeId = smsTypeAction.createSmsType(workspaceId, "SMS Type for " + databaseName, "sms_type_" + databaseName, fromName, toEmailFieldId);

                //  SmsMessageAction smsMessageAction = new SmsMessageAction(client, userName, password);
                //  String directEmailId = directEmailAction.createDirectEmail(directEmailId, "IPSOS SMS" + sequence, "ipsos_mail" + sequence, emailSubject, "Send mail by IPSOS", fromName, emailFrom, emailBody);

                //  PublishingAction publishingAction = new PublishingAction(client, userName, password);
                //  String publishId = publishingAction.publishTransactionalEmail(contactId, directEmailId);

                //  logger.Debug("publishId=" + publishId);

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
        /// Get feedback information
        /// </summary>
        /// <param name="customerName">customer Name</param>
        /// <param name="startTime">StartTime</param>
        /// <param name="endTime">End time</param>        
        /// <returns>all of the information, including sent, bounced, 
        /// clicked and opened</returns>
        public String getFeedbackInfo(String customerName, DateTime startTime, DateTime endTime)
        {
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
        private XDocument queryFeedbackInfo(String customerName, DateTime startTime, DateTime endTime, Hashtable searchConditions)
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
                Action.ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client, userName, password, oWebProxy);
                String contactDatabaseId = contactDatabaseAction.getDatabaseIdByName(customerName);

                ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password, oWebProxy);
                Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);
                //String toEmailFieldId = contactDatabaseFieldAction.getDatabaseEmailId(contactDatabaseId);

                cn.tripolis.dialogue.export.ContactExportRequest request = new cn.tripolis.dialogue.export.ContactExportRequest
                    {
                        contactDatabaseId = contactDatabaseId,
                        timeRange = new cn.tripolis.dialogue.export.TimeRange { startTime = startTime, endTime = endTime }
                    };

                var xroot = new XElement("FeedbackReport");

                logger.Debug("******exportSent*****");
                request.returnContactFields = new cn.tripolis.dialogue.export.ReturnContactFields
                    {
                        contactDatabaseFieldIds = new String[ht.Keys.Count]
                    };
                int index = 0;
                foreach (String value in ht.Values)
                {
                    request.returnContactFields.contactDatabaseFieldIds.SetValue(value, index++);
                }
                RawDataResponse response = exportService.exportSent(request);
                result = System.Text.Encoding.UTF8.GetString(response.data);
                XElement element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportSent", searchConditions);
                xroot.Add(element);

                request.returnContactFields.contactDatabaseFieldIds = null;


                request.returnContactFields = new cn.tripolis.dialogue.export.ReturnContactFields
                    {
                        returnAllContactFields = true,
                        returnAllContactFieldsSpecified = true
                    };

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
             ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password, oWebProxy);
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
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("\n");
                    }
                    foreach (String field in contactFields)
                    {

                        sb.Append("\"").Append(field.Split(new[] { "#" }, StringSplitOptions.None)[1]).Append("\"").Append(";");

                    }

                    sb.Remove(sb.Length - 1, 1);

                    sb.Append("\n");
                    index++;
                }

                request.reportReceiverAddress = mailData.replyAddress;
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


    }

  

}
