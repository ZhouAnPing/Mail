using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using TripolisDialogueAdapter.cn.tripolis.dialogue.export;
using TripolisDialogueAdapter.Action;
using TripolistMailAdapter;
using TripolisDialogueAdapter.DAO;
using System.Data;
using TripolisDialogueAdapter.cn.tripolis.dialogue.import;
using TripolisDialogueAdapter.cn.tripolis.dialogue.publish;
using TripolisDialogueAdapter.BO;
using System.Threading;
using System.IO;
using TripolisDialogueAdapter.cn.tripolis.dialogue.reporting;

namespace TripolisDialogueAdapter
{
    public class DialogueService_new
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MailAdapter));
        private string userName;
        private string password;
        private string client;
        private cn.tripolis.dialogue.subscription.SubscriptionService subscriptionService = null;
        private cn.tripolis.dialogue.subscription.AuthInfo subscriptionAuthInfo = null;
        private cn.tripolis.dialogue.export.ExportService exportService = null;
        private cn.tripolis.dialogue.export.AuthInfo exportAuthInfo = null;
        private cn.tripolis.dialogue.import.ImportService importService = null;
        private cn.tripolis.dialogue.import.AuthInfo importAuthInfo = null;
        public System.Net.WebProxy oWebProxy;
        /// <summary>
        /// Initial the webservice API.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        public DialogueService_new(String client, String userName, String password, System.Net.WebProxy oWebProxy)
        {
            this.client = client;
            this.userName = userName;
            this.password = password;
            this.oWebProxy = oWebProxy;
            //string strDomain = ConfigurationSettings.AppSettings["domain"].ToString();
            ////判断是否启用代理服务器
            //if (strDomain.Trim() != "")
            //{
            //    //域访问名
            //    string strUserName = ConfigurationSettings.AppSettings["UserName"].ToString();
            //    //域访问密码
            //    string strPassWord = ConfigurationSettings.AppSettings["PassWord"].ToString();
            //    //代理地址
            //    string strHost = ConfigurationSettings.AppSettings["Host"].ToString();
            //    //代理端口
            //    int strPort = Convert.ToInt32(ConfigurationSettings.AppSettings["Port"].ToString());
            //    //设置代理
            //    System.Net.WebProxy oWebProxy = new System.Net.WebProxy(strHost, strPort);
            //    // 获取或设置提交给代理服务器进行身份验证的凭据
            //    oWebProxy.Credentials = new System.Net.NetworkCredential(strUserName, strPassWord, strDomain);
            //    objService.Proxy = oWebProxy;
            //}
            subscriptionService = new cn.tripolis.dialogue.subscription.SubscriptionService();
            subscriptionAuthInfo = new cn.tripolis.dialogue.subscription.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            subscriptionService.authInfo = subscriptionAuthInfo;
            subscriptionService.Proxy = oWebProxy;

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
        /// publish Small Scale email, mail quantity >=50,000
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="contactGroup">Contact Group information</param>
        /// <param name="contacts">Contact Information</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <returns></returns>
        public String publishingBulkEmail(DialogueSetting dialogueSetting, ContactGroup contactGroup, String fileName, DirectEmail directEmail)
        {

            logger.Debug(System.Environment.NewLine+"Begin sending Bulk Scale Email.");
            String result = "OK";
            try
            {
                String contactDatabaseId = dialogueSetting.contactDatabaseId;
                String workspaceId = dialogueSetting.workspaceId;
                String emailTypeId = dialogueSetting.emailTypeId;
              //  String directEmailId = "";

                ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password, oWebProxy);
                String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, contactGroup.groupLabel, contactGroup.groupName);

                DateTime scheduleAt = DateTime.Now;
                ImportContactAction importContactAction = new ImportContactAction(client, userName, password, oWebProxy);
                String importId = importContactAction.importContactFromFTP(contactDatabaseId, contactGroupId, fileName, directEmail.reportReceiveAddress, dialogueSetting.ftpAccountId, scheduleAt);
                Thread.Sleep(5 * 1000);
                while (true)
                {
                    importContactAction = new ImportContactAction(client, userName, password, oWebProxy);
                    importStatus status = importContactAction.getImportStatus(importId);

                    if (status.Equals(importStatus.ENDED))
                    {
                        logger.InfoFormat("import contact File " + fileName + " successfully");
                        break;
                    }
                    if (status.Equals(importStatus.STOPPED) || status.Equals(importStatus.ABORTED))
                    {
                        logger.InfoFormat("fail to import contact File " + fileName  + " as import status is " + status.ToString());
                        // throw new Exception("fail to import contact File " + fileName + " for transid " + transId + " as import status is " + status.ToString());
                        return "";
                        //break;
                    }
                    logger.InfoFormat("Checking the import status, and current import status is " + status.ToString() );
                    Thread.Sleep(60 * 1000);
                }
                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
                String directEmailId = directEmailAction.createDirectEmail(emailTypeId, directEmail.emailLabel, directEmail.emailName, directEmail.subject, directEmail.description, directEmail.fromName, directEmail.fromAddress, directEmail.htmlContent);


                PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
                String publishId = publishingAction.publishingEmail(contactGroupId, directEmailId, DateTime.Now);
                result = publishId;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                result = ex.Message;
                logger.Debug("error happen in sending Small Scale mail, error is " + result);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logger.Debug("error happen in sending Small Scale mail, error is " + result);
                // return ex.Message;

            }
            logger.Debug("Finish sending Small Scale Email. Publish Id is " + result);
            return result;
        }


        /// <summary>
        /// publish Small Scale email,, mail quantity <50,000
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="contactGroup">Contact Group information</param>
        /// <param name="contacts">Contact Information</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <returns></returns>
        public String publishingSmallScaleEmail(DialogueSetting dialogueSetting, ContactGroup contactGroup, ImportFiles importFiles, DirectEmail directEmail)
        {

            logger.Debug(System.Environment.NewLine+"Begin sending Small Scale Email.");
            String result = "OK";
            try
            {
                String contactDatabaseId = dialogueSetting.contactDatabaseId;
                String workspaceId = dialogueSetting.workspaceId;
                String emailTypeId = dialogueSetting.emailTypeId;
                //String directEmailId = dialogueSetting.directEmailId;

                ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password, oWebProxy);
                String contactGroupId = contactGroupAction.createContactGroup(contactDatabaseId, contactGroup.groupLabel, contactGroup.groupName);

                String importId = this.importContact(contactDatabaseId, contactGroupId, directEmail.reportReceiveAddress, importFiles);              

                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
                String directEmailId = directEmailAction.createDirectEmail(emailTypeId, directEmail.emailLabel, directEmail.emailName, directEmail.subject, directEmail.description, directEmail.fromName, directEmail.fromAddress, directEmail.htmlContent);


                PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
                String publishId = publishingAction.publishingEmail(contactGroupId, directEmailId, DateTime.Now);
                result = publishId;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                result = ex.Message;
                logger.Debug("error happen in sending Small Scale mail, error is " + result);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logger.Debug("error happen in sending Small Scale mail, error is " + result);
                // return ex.Message;

            }
            logger.Debug("Finish sending Small Scale Email. Publish Id is " + result);
            return result;
        }

        /// <summary>
        /// send single email 
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <param name="ContactInfos">parameters related with contact information, such as email Id.</param>
        /// <returns></returns>
        public String sendSingleEmail(DialogueSetting dialogueSetting, DirectEmail directEmail, TripolisDialogueAdapter.BO.KeyValuePair[] ContactInfos)
        {

            logger.Debug(System.Environment.NewLine + "Begin sending Single Email.");
            String result = "OK";
            try
            {

                String contactDatabaseId = dialogueSetting.contactDatabaseId;
                String workspaceId = dialogueSetting.workspaceId;
                String emailTypeId = dialogueSetting.emailTypeId;
                String directEmailId = dialogueSetting.directEmailId;


                //查找<A的html标记，如果里面没有title属性，则增加这个属性.
                directEmail.htmlContent = Util.addTitleInAFlagHtml(directEmail.htmlContent);

                cn.tripolis.dialogue.subscription.subscribeContactRequest request = new cn.tripolis.dialogue.subscription.subscribeContactRequest();
                request.contactDatabase = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.contactDatabase.id = contactDatabaseId;
                request.workspace = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.workspace.id = workspaceId;

                request.contactFields = new cn.tripolis.dialogue.subscription.ContactFieldValue[ContactInfos.Length];
                cn.tripolis.dialogue.subscription.ContactFieldValue contactFieldValue;
                int i = 0;
                foreach (TripolisDialogueAdapter.BO.KeyValuePair keyValuePair in ContactInfos)
                {
                    contactFieldValue = new cn.tripolis.dialogue.subscription.ContactFieldValue();
                    contactFieldValue.name = keyValuePair.key;
                    contactFieldValue.value = keyValuePair.value;
                    request.contactFields.SetValue(contactFieldValue, i++);
                }

                request.ip = "127.0.0.1";

                cn.tripolis.dialogue.subscription.IDResponse response = subscriptionService.subscribeContact(request);

                String contactId = response.id;

                DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
                directEmailId = directEmailAction.createDirectEmail(emailTypeId, directEmail.emailLabel, directEmail.emailName, directEmail.subject, directEmail.description, directEmail.fromName, directEmail.fromAddress, directEmail.htmlContent);
                // directEmailAction.updateDirectEmail(directEmailId, directEmail.subject, directEmail.fromName, directEmail.htmlContent);

                PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
                String publishId = publishingAction.publishTransactionalEmail(contactId, directEmailId);


                //  SendLogDao dbAccess = new SendLogDao();
                //  dbAccess.SengLog_InsertInfo(new String[] { emailId, publishId, batchNo });
                result = publishId;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);
                // return ex.Message;

            }
            logger.Debug("Finish sending Single Email. Publish Id is " + result);
            return result;
        }

        /// <summary>
        /// send Register email 
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>        
        /// <param name="ContactInfos">parameters related with contact information, such as email Id.</param>
        /// <returns></returns>
        public String registerContact(DialogueSetting dialogueSetting, TripolisDialogueAdapter.BO.KeyValuePair[] ContactInfos)
        {

            logger.Debug(System.Environment.NewLine + "Begin Register Contact.");
            String result = "OK";
            try
            {

                String contactDatabaseId = dialogueSetting.contactDatabaseId;
                String workspaceId = dialogueSetting.workspaceId;
               // String emailTypeId = dialogueSetting.emailTypeId;
                String directEmailId = dialogueSetting.directEmailId;


              
                cn.tripolis.dialogue.subscription.subscribeContactRequest request = new cn.tripolis.dialogue.subscription.subscribeContactRequest();
                request.contactDatabase = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.contactDatabase.id = contactDatabaseId;
                request.workspace = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.workspace.id = workspaceId;

                request.contactFields = new cn.tripolis.dialogue.subscription.ContactFieldValue[ContactInfos.Length];
                cn.tripolis.dialogue.subscription.ContactFieldValue contactFieldValue;
                int i = 0;
                foreach (TripolisDialogueAdapter.BO.KeyValuePair keyValuePair in ContactInfos)
                {
                    contactFieldValue = new cn.tripolis.dialogue.subscription.ContactFieldValue();
                    contactFieldValue.name = keyValuePair.key;
                    contactFieldValue.value = keyValuePair.value;
                    request.contactFields.SetValue(contactFieldValue, i++);
                }
                if (!string.IsNullOrEmpty(directEmailId))
                {
                    request.directEmail = new cn.tripolis.dialogue.subscription.IdNameModel();
                    request.directEmail.id = directEmailId;
                }
                request.ip = "127.0.0.1";

                cn.tripolis.dialogue.subscription.IDResponse response = subscriptionService.subscribeContact(request);

                String contactId = response.id;


                result = contactId;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);
                // return ex.Message;

            }
            logger.Debug("Finish register Contact. Contact Id is " + result);
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
            int sentCnt =0;
            int openCnt =0;
            int clickCnt =0;
            int bouncedCnt = 0;
            
            int pageSize = 1000;

            ArrayList contacts = new ArrayList();
            ArrayList opens = new ArrayList();
            ArrayList clicks = new ArrayList();
            ArrayList bouncedContacts = new ArrayList();

            ReportingAction ReportingAction = new ReportingAction(this.client, this.userName, this.password, this.oWebProxy);

            EmailSummary emailSummary = ReportingAction.getEmailSummary(mailJobId);
            if (emailSummary != null)
            {
                sentCnt = emailSummary.job.numberOfSend;
                openCnt = emailSummary.totalOpens;
                clickCnt = emailSummary.totalClicks;
                bouncedCnt = emailSummary.hardBounces+emailSummary.softBounces;
                int sentPageNb =(int) Math.Ceiling((float)sentCnt / pageSize);
                int openPageNb = (int)Math.Ceiling((float)openCnt / pageSize);
                int clickPageNb = (int)Math.Ceiling((float)clickCnt / pageSize);
                int bouncedPageNb = (int)Math.Ceiling((float)bouncedCnt / pageSize);
               

                for (int i = 1; i <= sentPageNb; i++)
                {
                    Contact[] tempContacts = ReportingAction.getDeliveredByMailJobId(mailJobId, startTime, endTime, pageSize, i);
                    contacts.Add(tempContacts);
                }
                for (int i = 1; i <= openPageNb; i++)
                {
                    Open[] tempOpens = ReportingAction.getOpenedByMailJobId(mailJobId, startTime, endTime, pageSize, i);
                    opens.Add(tempOpens);
                }
                for (int i = 1; i <= clickPageNb; i++)
                {
                    Click[] tempClicks = ReportingAction.getClickedByMailJobId(mailJobId, startTime, endTime, pageSize, i);
                    clicks.Add(tempClicks);
                }
                for (int i = 1; i <= bouncedPageNb; i++)
                {
                    BouncedContact[] tempBouncedContacts = ReportingAction.getBouncedByMailJobId(mailJobId, startTime, endTime, pageSize, i);
                    bouncedContacts.Add(tempBouncedContacts);
                }
            }

            reportData.sent = (Contact[])contacts.ToArray();
            reportData.opened = (Open[])opens.ToArray();
            reportData.clicked = (Click[])clicks.ToArray();
            reportData.bounced = (BouncedContact[])bouncedContacts.ToArray();

            return reportData;

        }

        /// <summary>
        /// Export Report
        /// </summary>
        /// <param name="contactDatabaseId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ExportReportData exportReport(String contactDatabaseId, DateTime startTime, DateTime endTime)
        {
            ExportReportData mailReport = new ExportReportData();
            ExportAction exportAction = new ExportAction(this.client, this.userName, this.password, this.oWebProxy);

            mailReport.sent =  exportAction.ExportReport(contactDatabaseId, startTime, endTime, ReportType.SENT);

            mailReport.opened = exportAction.ExportReport(contactDatabaseId, startTime, endTime, ReportType.OPENED);

            mailReport.clicked = exportAction.ExportReport(contactDatabaseId, startTime, endTime, ReportType.CLICKED);

            mailReport.bounced  = exportAction.ExportReport(contactDatabaseId, startTime, endTime, ReportType.BOUNCED);

            return mailReport;
        }

        public void exportReportToFtp(String contactDatabaseId, String ftpAccountId, String fileName, DateTime startTime, DateTime endTime)
        {
            ExportAction exportAction = new ExportAction(this.client, this.userName, this.password, this.oWebProxy);
            exportAction.exportReportToFtp(contactDatabaseId, ftpAccountId, fileName + "_SENT" + ".csv", startTime, endTime, ReportType.SENT);
            exportAction.exportReportToFtp(contactDatabaseId, ftpAccountId, fileName + "_OPENED" + ".csv", startTime, endTime, ReportType.OPENED);
            exportAction.exportReportToFtp(contactDatabaseId, ftpAccountId, fileName + "_CLICKED" + ".csv", startTime, endTime, ReportType.CLICKED);
            exportAction.exportReportToFtp(contactDatabaseId, ftpAccountId, fileName + "_BOUNCED" + ".csv", startTime, endTime, ReportType.BOUNCED);
        }

        #region construct mail

        /// <summary>
        /// Create contact group
        /// </summary>
        /// <param name="contactDatabaseId">contact database id</param>
        /// <param name="groupLable">contact group lable</param>
        /// <param name="groupName">contact group name</param>
        /// <returns>contact group id</returns>
        public String createContactGroup(String contactDatabaseId, String groupLable, String groupName)
        {
            ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password, oWebProxy);

            return contactGroupAction.createContactGroup(contactDatabaseId, groupLable, groupName);
        }
        /// <summary>
        /// Import contact to database
        /// </summary>
        /// <param name="contactDatabaseId">contact database id</param>
        /// <param name="contactGroupId">contact group id</param>
        /// <param name="mailData">mail data</param>
        public String importContact(String contactDatabaseId, String contactGroupId, String reportReceiverAddress, ImportFiles importFiles)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("importContact:contactDatabaseId=" + contactDatabaseId);
            }

            String result = "";
            ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password, oWebProxy);
            Hashtable contactFieldTable = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);

            cn.tripolis.dialogue.import.ImportContactsRequest request = new cn.tripolis.dialogue.import.ImportContactsRequest();
            try
            {

                importService.Timeout = System.Threading.Timeout.Infinite;
                request.reportReceiverAddress = reportReceiverAddress;
                request.importMode = cn.tripolis.dialogue.import.importMode.SYNCHRONIZE_GROUP;
                request.contactGroupIds = new[] { contactGroupId };
                request.extension = importFiles.fileType;// cn.tripolis.dialogue.import.fileExtension.CSV;
                if (importFiles.fileType.Equals(fileExtension.CSV) && !importFiles.csvDilimiter.Equals(ImportFiles.DEFAULT_CSV_DELIMIT))
                {
                    string fileContent = Encoding.UTF8.GetString(importFiles.fileContent);
                    string revisedContent = fileContent.Replace(importFiles.csvDilimiter, ImportFiles.DEFAULT_CSV_DELIMIT);
                    importFiles.fileContent = Encoding.UTF8.GetBytes(revisedContent);
                }
                request.fileName = importFiles.filename;
                request.importFile = importFiles.fileContent;//System.IO.File.ReadAllBytes("../../Contacts_new.csv");
                //importService..importContactsAsync()
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

        public String updateDirectEmail(String directEmailId, String subject, String fromName, String htmlSource)
        {
            DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
            return directEmailAction.updateDirectEmail(directEmailId, subject, fromName, htmlSource);
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
            DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
            return directEmailAction.createDirectEmail(directEmailTypeId, emailLabel, emailName, subject, description, fromName, fromAddress, htmlSource);

        }

        #endregion construct mail

    }
}