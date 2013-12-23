using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using TripolisDialogueAdapter;
using TripolisDialogueAdapter.BO;
using TripolisDialogueAdapterWs.BO;
using TripolisDialogueAdapterWs.DAO;

namespace TripolisDialogueAdapterWs
{
    /// <summary>
    /// Tripolis Mail and SMS interface.
    /// </summary>
    [WebService(Namespace = "http://ws.tripolis.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MailAdapter : System.Web.Services.WebService
    {

       /// <summary>
        /// publish Small Scale email, mail quantity >=50,000
       /// </summary>
       /// <param name="apiKey">api access Key, generate by tripolis</param>
       /// <param name="contactfileName">Contact file name that has bene ftp to file server</param>
        /// <param name="fromName">fromName</param>
        /// <param name="fromAddress">fromAddress</param>
        /// <param name="subject">subject</param>
        /// <param name="reportReceiveAddress">reportReceiveAddress</param>
        /// <param name="mailBody">mailBody</param>
       /// <returns>mail publish Id</returns>
        [WebMethod]
        public String publishingBulkEmail(String apiKey, String contactfileName, String fromName, String fromAddress, String subject, String reportReceiveAddress, String mailBody)
        {
            DialogueSettingDao DialogueSettingDao = new DialogueSettingDao();
            DialogueSettingBO DB_DialogueSetting = DialogueSettingDao.Get(apiKey);

            Authorization authorization = new Authorization();
            authorization.client = DB_DialogueSetting.client;
            authorization.username = DB_DialogueSetting.username;
            authorization.password = DB_DialogueSetting.password;

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = DB_DialogueSetting.contactDatabaseId;
            dialogueSetting.workspaceId = DB_DialogueSetting.workspaceId;
            dialogueSetting.emailTypeId = DB_DialogueSetting.emailTypeId;
            dialogueSetting.ftpAccountId = DB_DialogueSetting.ftpAccountId;

            String prefix = DB_DialogueSetting.prefix;

            String sequence = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            ContactGroup contactGroup = new ContactGroup();
            contactGroup.groupLabel = prefix + sequence;
            contactGroup.groupName = prefix + sequence;

            DirectEmail directEmail = new DirectEmail();
            directEmail.emailLabel = prefix + sequence;
            directEmail.emailName = prefix + sequence;
            directEmail.subject = subject;
            directEmail.description = subject;
            directEmail.fromName = fromName;
            directEmail.fromAddress = fromAddress;
            if (String.IsNullOrEmpty(reportReceiveAddress))
            {
                directEmail.reportReceiveAddress = fromAddress;
            }
            else
            {
                directEmail.reportReceiveAddress = reportReceiveAddress;
            }
            directEmail.htmlContent = mailBody;

            

            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.publishingBulkEmail(dialogueSetting, contactGroup, contactfileName, directEmail);
        }


       /// <summary>
       ///  publish Small Scale email,, mail quantity <50,000
       /// </summary>       
        /// <param name="apiKey">api access Key, generate by tripolis</param>
        /// <param name="contactFileInfo">conacts Information</param>
        /// <param name="fromName">fromName</param>
        /// <param name="fromAddress">fromAddress</param>
        /// <param name="subject">subject</param>
        /// <param name="reportReceiveAddress">reportReceiveAddress</param>
        /// <param name="mailBody">mailBody</param>
        /// <returns>mail publish Id</returns>
        [WebMethod]
        public String publishingSmallScaleEmail(String apiKey, ContactFileInfo contactFileInfo, String fromName, String fromAddress, String subject, String reportReceiveAddress, String mailBody)
        {
            DialogueSettingDao DialogueSettingDao = new DialogueSettingDao();
            DialogueSettingBO DB_DialogueSetting = DialogueSettingDao.Get(apiKey);

            Authorization authorization = new Authorization();
            authorization.client = DB_DialogueSetting.client;
            authorization.username = DB_DialogueSetting.username;
            authorization.password = DB_DialogueSetting.password;

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = DB_DialogueSetting.contactDatabaseId;
            dialogueSetting.workspaceId = DB_DialogueSetting.workspaceId;
            dialogueSetting.emailTypeId = DB_DialogueSetting.emailTypeId;
            String prefix = DB_DialogueSetting.prefix;

            String sequence = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            ContactGroup contactGroup = new ContactGroup();
            contactGroup.groupLabel = prefix + sequence;
            contactGroup.groupName = prefix + sequence;

            DirectEmail directEmail = new DirectEmail();
            directEmail.emailLabel = prefix + sequence;
            directEmail.emailName = prefix + sequence;
            directEmail.subject = subject;
            directEmail.description = subject;
            directEmail.fromName = fromName;
            directEmail.fromAddress = fromAddress;
            if (String.IsNullOrEmpty(reportReceiveAddress))
            {
                directEmail.reportReceiveAddress = fromAddress;
            }
            else
            {
                directEmail.reportReceiveAddress = reportReceiveAddress;
            }
            directEmail.htmlContent = mailBody;


            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.publishingSmallScaleEmail(dialogueSetting, contactGroup, contactFileInfo, directEmail);
        }

       /// <summary>
       /// Send Single Mail
       /// </summary>
        /// <param name="apiKey">api access Key, generate by tripolis</param>      
        /// <param name="fromName">fromName</param>
        /// <param name="fromAddress">fromAddress</param>
        /// <param name="subject">subject</param>
        /// <param name="reportReceiveAddress">reportReceiveAddress</param>
        /// <param name="mailBody">mailBody</param>
       /// <param name="ContactInfos">Contact infomaction</param>
       /// <returns>mail publish Id</returns>
        [WebMethod]
        public String sendSingleEmail(String apiKey, String fromName, String fromAddress, String subject, String reportReceiveAddress, String mailBody, KeyValuePair[] ContactInfos)
        {
            DialogueSettingDao DialogueSettingDao = new DialogueSettingDao();
            DialogueSettingBO DB_DialogueSetting = DialogueSettingDao.Get(apiKey);

            Authorization authorization = new Authorization();
            authorization.client = DB_DialogueSetting.client;
            authorization.username = DB_DialogueSetting.username;
            authorization.password = DB_DialogueSetting.password;

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = DB_DialogueSetting.contactDatabaseId;
            dialogueSetting.workspaceId = DB_DialogueSetting.workspaceId;
            dialogueSetting.emailTypeId = DB_DialogueSetting.emailTypeId;
            String prefix = DB_DialogueSetting.prefix;

            String sequence = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");

            DirectEmail directEmail = new DirectEmail();
            directEmail.emailLabel = prefix + sequence;
            directEmail.emailName = prefix + sequence;
            directEmail.subject = subject;
            directEmail.description = subject;
            directEmail.fromName = fromName;
            directEmail.fromAddress = fromAddress;          
            if (String.IsNullOrEmpty(reportReceiveAddress))
            {
                directEmail.reportReceiveAddress = fromAddress;
            }
            else
            {
                directEmail.reportReceiveAddress = reportReceiveAddress;
            }
            directEmail.htmlContent = mailBody;

            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.sendSingleEmail(dialogueSetting, directEmail, ContactInfos);
        }

        /// <summary>
        /// send Register email.
        /// </summary>
        /// <param name="apiKey">api access Key, generate by tripolis</param>      
        /// <param name="ContactInfos">parameters related with contact information, such as email Id.</param>
        /// <returns>Contact Id</returns>
        [WebMethod]
        public String registerContact(String apiKey, KeyValuePair[] ContactInfos)
        {
            DialogueSettingDao DialogueSettingDao = new DialogueSettingDao();
            DialogueSettingBO DB_DialogueSetting = DialogueSettingDao.Get(apiKey);

            Authorization authorization = new Authorization();
            authorization.client = DB_DialogueSetting.client;
            authorization.username = DB_DialogueSetting.username;
            authorization.password = DB_DialogueSetting.password;

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = DB_DialogueSetting.contactDatabaseId;
            dialogueSetting.workspaceId = DB_DialogueSetting.workspaceId;
            dialogueSetting.directEmailId = DB_DialogueSetting.directEmailId;

            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.registerContact(dialogueSetting, ContactInfos);
        }

        /// <summary>
        /// export mail report
        /// </summary>
        /// <param name="apiKey">api access Key, generate by tripolis</param>  
        /// <param name="startTime">Query Start time </param>
        /// <param name="endTime">Query End time </param>
        /// <returns>report data.</returns>
        [WebMethod]
        public CSVReportData exportReport(String apiKey, DateTime startTime, DateTime endTime)
        {
            DialogueSettingDao DialogueSettingDao = new DialogueSettingDao();
            DialogueSettingBO DB_DialogueSetting = DialogueSettingDao.Get(apiKey);

            Authorization authorization = new Authorization();
            authorization.client = DB_DialogueSetting.client;
            authorization.username = DB_DialogueSetting.username;
            authorization.password = DB_DialogueSetting.password;

            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);

            ExportReportData report = dialogueService_new.exportReport(DB_DialogueSetting.contactDatabaseId, startTime, endTime);
            CSVReportData csvReportData = new CSVReportData();

            csvReportData.sent = Encoding.UTF8.GetString(report.sent);
            csvReportData.opened = Encoding.UTF8.GetString(report.opened);
            csvReportData.clicked = Encoding.UTF8.GetString(report.clicked);
            csvReportData.bounced = Encoding.UTF8.GetString(report.bounced);

            return csvReportData;
        }

        /// <summary>
        /// export report to FTP server
        /// </summary>
        /// <param name="apiKey">api access Key, generate by tripolis</param>  
        /// <param name="startTime">Query Start time </param>
        /// <param name="endTime">Query End time </param>        
        [WebMethod]
        public void exportReportToFtp(String apiKey, String fileNamePrefix, DateTime startTime, DateTime endTime)
        {
            DialogueSettingDao DialogueSettingDao = new DialogueSettingDao();
            DialogueSettingBO DB_DialogueSetting = DialogueSettingDao.Get(apiKey);

            Authorization authorization = new Authorization();
            authorization.client = DB_DialogueSetting.client;
            authorization.username = DB_DialogueSetting.username;
            authorization.password = DB_DialogueSetting.password;

            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            dialogueService_new.exportReportToFtp(DB_DialogueSetting.contactDatabaseId, DB_DialogueSetting.ftpAccountId, fileNamePrefix, startTime, endTime);

        }
        /// <summary>
        /// get report by mail job Id
        /// </summary>
        /// <param name="apiKey">api access Key, generate by tripolis</param>  
        /// <param name="publishId">publishId </param>
        /// <param name="startTime">Query Start time </param>
        /// <param name="endTime">Query End time </param>
        /// <returns>report data.</returns>
        [WebMethod]
        public ReportData getRerportByJobId(String apiKey, String publishId, DateTime startTime, DateTime endTime)
        {
            DialogueSettingDao DialogueSettingDao = new DialogueSettingDao();
            DialogueSettingBO DB_DialogueSetting = DialogueSettingDao.Get(apiKey);

            Authorization authorization = new Authorization();
            authorization.client = DB_DialogueSetting.client;
            authorization.username = DB_DialogueSetting.username;
            authorization.password = DB_DialogueSetting.password;

            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.getRerportByJobId(publishId, startTime, endTime);
        }

        /// <summary>
        /// Send SMS
        /// </summary>
        /// <param name="mobile">mobile</param>       
        /// <param name="time">send time </param>
        /// <param name="content">sms content</param>
        /// <returns>send Id</returns>
        [WebMethod]
        public SendSmsResult sendSMS(String apiKey, string mobile, string time, string content)
        {
            DialogueSettingDao DialogueSettingDao = new DialogueSettingDao();
            DialogueSettingBO DB_DialogueSetting = DialogueSettingDao.Get(apiKey);
            Authorization authorization = new Authorization();
            authorization.client = DB_DialogueSetting.client;
            authorization.username = DB_DialogueSetting.username;
            authorization.password = DB_DialogueSetting.password;

            string account = DB_DialogueSetting.sms_account;
            string password = DB_DialogueSetting.sms_password;
            string pid = DB_DialogueSetting.sms_pid;

            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.sendSMS(account, password, mobile, pid, time, content);
        }
    }
}
