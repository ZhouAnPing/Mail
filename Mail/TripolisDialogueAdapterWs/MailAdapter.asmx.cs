using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using TripolisDialogueAdapter;
using TripolisDialogueAdapter.BO;

namespace TripolisDialogueAdapterWs
{
    /// <summary>
    /// Summary description for MailAdapter
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
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="contactGroup">Contact Group information</param>
        /// <param name="contacts">Contact Information</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <returns></returns>
      [WebMethod]
        public String publishingBulkEmail(Authorization authorization, DialogueSetting dialogueSetting, ContactGroup contactGroup, String fileName, DirectEmail directEmail)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.publishingBulkEmail(dialogueSetting, contactGroup, fileName, directEmail);
        }

       
        /// <summary>
        /// publish Small Scale email,, mail quantity <50,000
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="contactGroup">Contact Group information</param>
        /// <param name="contacts">Contact Information</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <returns></returns>
         [WebMethod]
        public String publishingSmallScaleEmail(Authorization authorization, DialogueSetting dialogueSetting, ContactGroup contactGroup, ImportFiles importFiles, DirectEmail directEmail)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.publishingSmallScaleEmail(dialogueSetting, contactGroup, importFiles, directEmail);
        }
       
        /// <summary>
        /// send single email 
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <param name="ContactInfos">parameters related with contact information, such as email Id.</param>
        /// <returns></returns>
        [WebMethod]
        public String sendSingleEmail(Authorization authorization, DialogueSetting dialogueSetting, DirectEmail directEmail, KeyValuePair[] ContactInfos)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.sendSingleEmail(dialogueSetting, directEmail, ContactInfos);
        }
       
        /// <summary>
        /// send Register email 
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>        
        /// <param name="ContactInfos">parameters related with contact information, such as email Id.</param>
        /// <returns></returns>
         [WebMethod]
        public String registerContact(Authorization authorization, DialogueSetting dialogueSetting, KeyValuePair[] ContactInfos)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.registerContact(dialogueSetting, ContactInfos);
        }
        /// <summary>
        /// export report for certain database and duration.
        /// </summary>
         /// <param name="authorization">Authorization for login Dialoue plateform</param>
        /// <param name="exportReportParam"> </param>
        /// <returns></returns>
         [WebMethod]
         public CSVReportData exportReport(Authorization authorization, ExportReportParam exportReportParam)
         {
             DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);

             ExportReportData report = dialogueService_new.exportReport(exportReportParam.contactDatabaseId, exportReportParam.startTime, exportReportParam.endTime);
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
        /// <param name="authorization"></param>
        /// <param name="exportReportParamToFtp"></param>
        [WebMethod]
        public void exportReportToFtp(Authorization authorization, ExportReportToFtpParam exportReportParamToFtp)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            dialogueService_new.exportReportToFtp(exportReportParamToFtp.contactDatabaseId, exportReportParamToFtp.ftpAccountId, exportReportParamToFtp.fileNamePrefix, exportReportParamToFtp.startTime, exportReportParamToFtp.endTime);

        }
        /// <summary>
        /// get report by mail job Id
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="exportReportParamToFtp"></param>
        [WebMethod]
        public ReportData getRerportByJobId(Authorization authorization, String mailJobId, DateTime startTime, DateTime endTime)
        {
           DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
           return dialogueService_new.getRerportByJobId(mailJobId, startTime,endTime);
        }

        /// <summary>
        /// Send SMS
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="mobile"></param>
        /// <param name="pid"></param>
        /// <param name="time"></param>
        /// <param name="content"></param>
        /// <returns></returns>
          [WebMethod]
        public SendSmsResult sendSMS(Authorization authorization, string account, string password, string mobile, string pid, string time, string content)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.sendSMS(account, password, mobile, pid, time, content);


        }
    }
}
