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
using TripolisDialogueAdapter.BO;
using TripolisDialogueAdapter.cn.tripolis.dialogue.reporting;
using System.Threading;
using TripolisDialogueAdapter.cn.tripolis.dialogue.import;



namespace TripolisDialogueAdapter
{
    public class ChinaUnionAdapter
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
        public ChinaUnionAdapter(String client, String userName, String password, System.Net.WebProxy oWebProxy)
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
        /// send batch mail
        /// </summary>
        /// <param name="databaseName">databaseName</param>
        /// <param name="mailData">mailData</param>
        /// <returns></returns>
        public String sendBatchMail(String contactDatabaseId,String workspaceId, String directEmailId,MailData mailData, DateTime scheduleAt)
        {
            logger.Debug("************send Batch Mail***************");
            String result = OK_RESULT;
            try
            {
                if (String.IsNullOrEmpty(contactDatabaseId))
                {
                    return "No database ";
                }
                if (mailData == null || mailData.ContactJsonList == null || mailData.ContactJsonList.Count <= 0)
                {
                    return "No Contact";
                }     

                //const string pattern = "\\{.+?\\}";
                //List<string> tempList = Regex.Matches(mailData.mailBody, pattern).Cast<Match>().Select(a => a.Value).ToList();
                //foreach (string str in tempList)
                //{
                //    mailData.mailBody = mailData.mailBody.Replace(str, str.ToLower());
                //}
                ////查找<A的html标记，如果里面没有title属性，则增加这个属性.
                //mailData.mailBody = Util.addTitleInAFlagHtml(mailData.mailBody);


                String sequence = DateTime.Now.ToString("yyyyMMddhhmmss");
                ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password, oWebProxy);               

                String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, "月度佣金" + sequence, "monthly_fee" + sequence);

                String importId= this.importContact(contactDatabaseId, contactGroupId, mailData);

                Thread.Sleep(5 * 1000);
                while (true)
                {
                    ImportContactAction importContactAction = new ImportContactAction(client, userName, password, oWebProxy);
                    importStatus status = importContactAction.getImportStatus(importId);

                    if (status.Equals(importStatus.ENDED))
                    {
                        logger.InfoFormat("import contact File  successfully");
                        break;
                    }
                    if (status.Equals(importStatus.STOPPED) || status.Equals(importStatus.ABORTED))
                    {
                        logger.InfoFormat("fail to import contact File  as import status is " + status.ToString());
                         throw new Exception("fail to import contact File  as import status is " + status.ToString());
                      
                        //break;
                    }
                    logger.InfoFormat("Checking the import status, and current import status is " + status.ToString() );
                    Thread.Sleep(5 * 1000);
                }

              
                PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
                String publishId = publishingAction.publishingEmail(contactGroupId, directEmailId, scheduleAt);

                logger.Debug("publishId=" + publishId);
                result = "OK:"+publishId;

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
        public String sendSingleEmail(String contactDatabaseId, String workspaceId, String directEmailTypeId,String fromName, String emailFrom, String emailTo,String agent_no, String emailSubject, String emailBody)
        {
            logger.Debug("************send Single Mail***************");
            String result = OK_RESULT;
            try
            {
               
                if (String.IsNullOrEmpty(emailFrom))
                {
                    return "No from Address";
                }
                if (String.IsNullOrEmpty(emailTo))
                {
                    return "No to Address";
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
                String jsonStr = EMAIL + "#" + emailTo + ",agent_no#" + agent_no;
                list.Add(jsonStr);
                String contactId = contactAction.createContact(contactDatabaseId, list);

              
                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
                String directEmailId = directEmailAction.createDirectEmail(directEmailTypeId, "测试邮件" + sequence, "mail" + sequence, emailSubject, "测试邮件", fromName, emailFrom, emailBody);

                PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
                String publishId = publishingAction.publishTransactionalEmail(contactId, directEmailId, DateTime.Now);

                logger.Debug("publishId=" + publishId);

                result = "OK:" + publishId;

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



        /// <summary>       
        /// getRerportByJobId
        /// </summary>
        /// <param name="mailJobId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ReportData getRerportByJobId(String mailJobId, DateTime startTime, DateTime endTime)
        {
            ReportData reportData = new BO.ReportData();
            int sentCnt = 0;
            int skipCnt = 0;
            int openCnt = 0;
           
            int bouncedCnt = 0;

            int pageSize = 1000;

            ArrayList sents = new ArrayList();
            ArrayList opens = new ArrayList();
            ArrayList skips = new ArrayList();
           
            ArrayList bouncedContacts = new ArrayList();

            ReportingAction ReportingAction = new ReportingAction(this.client, this.userName, this.password, this.oWebProxy);

            EmailSummary emailSummary = ReportingAction.getEmailSummary(mailJobId);
            if (emailSummary != null)
            {
                sentCnt = emailSummary.job.numberOfSend;
                skipCnt = emailSummary.job.numberOfSkipped;
                openCnt = emailSummary.totalOpens;               
                bouncedCnt = emailSummary.hardBounces + emailSummary.softBounces;

                int sentPageNb = (int)Math.Ceiling((float)sentCnt / pageSize);
                int skipPageNb = (int)Math.Ceiling((float)skipCnt / pageSize);
                int openPageNb = (int)Math.Ceiling((float)openCnt / pageSize);                
                int bouncedPageNb = (int)Math.Ceiling((float)bouncedCnt / pageSize);


                for (int i = 1; i <= sentPageNb; i++)
                {
                    Contact[] tempContacts = ReportingAction.getDeliveredByMailJobId(mailJobId, startTime, endTime, pageSize, i);
                    sents.AddRange(tempContacts);
                }
                for (int i = 1; i <= skipPageNb; i++)
                {
                    SkippedContactModel[] tempContacts = ReportingAction.getSkippedByMailJobId(mailJobId, startTime, endTime, pageSize, i);
                    skips.AddRange(tempContacts);
                }
                for (int i = 1; i <= openPageNb; i++)
                {
                    Open[] tempOpens = ReportingAction.getOpenedByMailJobId(mailJobId, startTime, endTime, pageSize, i);
                    opens.AddRange(tempOpens);
                }
              
                for (int i = 1; i <= bouncedPageNb; i++)
                {
                    BouncedContact[] tempBouncedContacts = ReportingAction.getBouncedByMailJobId(mailJobId, startTime, endTime, pageSize, i);
                    bouncedContacts.AddRange(tempBouncedContacts);
                }
                
            }

            reportData.emailSummary = emailSummary;

            if (sents != null && sents.Count > 0)
            {
                reportData.sent = new Contact[sents.Count];
                for (int i = 0; i < sents.Count; i++)
                {
                    reportData.sent[i] = (Contact)sents[i];
                }
            }

            if (opens != null && opens.Count > 0)
            {
                reportData.opened = new Open[opens.Count];
                for (int i = 0; i < opens.Count; i++)
                {
                    reportData.opened[i] = (Open)opens[i];
                }

            }
            if (skips != null && skips.Count > 0)
            {
                reportData.skipped = new SkippedContactModel[skips.Count];
                for (int i = 0; i < skips.Count; i++)
                {
                    reportData.skipped[i] = (SkippedContactModel)skips[i];
                }

            }
            if (bouncedContacts != null && bouncedContacts.Count > 0)
            {
                reportData.bounced = new BouncedContact[bouncedContacts.Count];
                for (int i = 0; i < bouncedContacts.Count; i++)
                {
                    reportData.bounced[i] = (BouncedContact)bouncedContacts[i];
                }

            }
            return reportData;

        }
    }

  

}
