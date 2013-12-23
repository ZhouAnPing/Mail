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
    /// Mail and Report Interface for Ctrip
    /// </summary>
    [WebService(Namespace = "http://ctrip.tripolis.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CtripMailAdapter : System.Web.Services.WebService
    {
        /// <summary>
        /// Send Mail
        /// </summary>
        /// <param name="fromName">from Name</param>
        /// <param name="fromEmail">from email address</param>
        /// <param name="subject">subject</param>
        /// <param name="mailContent">mail content</param>
        /// <param name="toEmail">to mail address</param>
        /// <param name="scheduleTime">schedule time</param>
        /// <returns></returns>
        [WebMethod]        
        public String sendMail(String APIKey, String fromName, String fromEmail, String subject, string mailContent, String toEmail, DateTime scheduleTime)
        {
            if (!APIKey.Equals("MjU4MDI1ODCzAn45YUUpJw"))
            {
                throw new Exception("API认证失败,请确认你的APIkey是否正确.");
            }
            Authorization authorization = new Authorization();

            authorization.client = "Training";
            authorization.username = "zapjx@hotmail.com";
            authorization.password = "Test123";
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = "MjU4MDI1ODCzAn45YUUpJw";
            dialogueSetting.workspaceId = "MjAzNDIwMzTIkkBLrbUiiA";
            dialogueSetting.emailTypeId = "MTgwNTE4MDVA5rEBE89J_w";


            DirectEmail directEmail = new DirectEmail();
            String sequence = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            directEmail.emailLabel = sequence;
            directEmail.emailName = sequence;
            directEmail.subject = subject;
            directEmail.description = subject;
            directEmail.fromName = fromName;
            directEmail.fromAddress = fromEmail;
            directEmail.reportReceiveAddress = fromEmail;
            directEmail.htmlContent = mailContent;
            directEmail.sheduleTime = scheduleTime;

            KeyValuePair[] ContactInfos = new KeyValuePair[1];

            KeyValuePair ContactInfo = new KeyValuePair();
            ContactInfo.key = "email";
            ContactInfo.value = toEmail;
            ContactInfos[0] = ContactInfo;

            return dialogueService_new.sendSingleEmail(dialogueSetting, directEmail, ContactInfos);
        }

        /// <summary>
        /// export report for a certain duration
        /// </summary>
        /// <param name="startTime">Starttime</param>
        /// <param name="endTime">endtime</param>
        /// <returns></returns>
        [WebMethod]
        public CSVReportData exportReport(String APIKey, DateTime startTime, DateTime endTime)
        {
            if (!APIKey.Equals("MjU4MDI1ODCzAn45YUUpJw"))
            {
                throw new Exception("API认证失败,请确认你的APIkey是否正确.");
            }
            Authorization authorization = new Authorization();

            authorization.client = "Training";
            authorization.username = "zapjx@hotmail.com";
            authorization.password = "Test123";
            if (endTime <= startTime)
            {
                throw new Exception("开始时间要小于等于结束时间");
            }
            if (startTime.AddHours(8) < endTime)
            {
                throw new Exception("时间间隔必须在8小时之内");
            }

            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
           
            String contactDatabaseId = "MjU4MDI1ODCzAn45YUUpJw";
            ExportReportData  report = dialogueService_new.exportReport(contactDatabaseId, startTime, endTime);

            CSVReportData csvReportData = new CSVReportData();

            csvReportData.sent = Encoding.UTF8.GetString(report.sent);
            csvReportData.opened = Encoding.UTF8.GetString(report.opened);
            csvReportData.clicked = Encoding.UTF8.GetString(report.clicked);
            csvReportData.bounced = Encoding.UTF8.GetString(report.bounced);

            return csvReportData;
        }
    }
}
