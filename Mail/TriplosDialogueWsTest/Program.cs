using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriplosDialogueWsTest.MailAdapterWs;

namespace TriplosDialogueWsTest
{
    class Program
    {
        // private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {

            //Authorization authorization = new MailAdapterWs.Authorization();
            //authorization.client = "Training";
            //authorization.username = "zapjx@hotmail.com";
            //authorization.password = "Test123";

            Program TestProgram = new TriplosDialogueWsTest.Program();
            //for (int i = 0; i < 1; i++)
            //{
            //    DateTime startTime = DateTime.Now;
            //    TestProgram.sendmailForCtrip();
            //    DateTime endTime = DateTime.Now;

            //    System.TimeSpan ts = endTime - startTime;

            //    Console.WriteLine("时间差:" + ts.Seconds);
            //}

            //TestProgram.exportReportForCtrip();



            String apiKey = "MjU1MTI1NTFFUVus6S83qA";

            String publishId = TestProgram.registerContact(apiKey);
            Console.WriteLine("register Contact Id=" + publishId);

            publishId = TestProgram.sendSingleMail(apiKey);
            Console.WriteLine("sendSingleMai Id=" + publishId);

            //publishId = TestProgram.publishingSmallScaleEmail(apiKey);
            //Console.WriteLine("publishingSmallScaleEmail Id=" + publishId);

            //publishId = TestProgram.publishingBulkEmail(apiKey);
            //Console.WriteLine("publishingBulkEmail Id=" + publishId);

           // TestProgram.exportReport(apiKey);
           // Console.WriteLine("exportReport Complete");

            //TestProgram.exportReportToFtp(apiKey);
            //Console.WriteLine("exportReportToFtp Complete");

            //TestProgram.getRerportByJobId(apiKey);
           // Console.WriteLine("getRerportByJobId Complete");          

            Console.WriteLine("Mission Complete");
            Console.ReadLine();


        }

        private void sendmailForCtrip()
        {
            CtripMailAdapterWs.CtripMailAdapter dialogueService = new CtripMailAdapterWs.CtripMailAdapter();

            String apiKey = "MjU4MDI1ODCzAn45YUUpJw";
            String fromName = "Ctrip API Demo";
            String fromEmail = "API@Ctrip.com";
            String subject = "Ctrip API Demo";
            string mailContent = System.IO.File.ReadAllText("../../Example/template.html");
            String toEmail = "1197922021@qq.com";
            DateTime scheduleTime = DateTime.Now;
            String publishId = dialogueService.sendMail(apiKey, fromName, fromEmail, subject, mailContent, toEmail, scheduleTime);
            Console.WriteLine("PublishId=" + publishId);
        }
        private void exportReportForCtrip()
        {
            CtripMailAdapterWs.CtripMailAdapter dialogueService = new CtripMailAdapterWs.CtripMailAdapter();
            String apiKey = "MjU4MDI1ODCzAn45YUUpJw";
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddHours(-8);
            CtripMailAdapterWs.CSVReportData reportData = dialogueService.exportReport(apiKey, startTime, endTime);

            Console.WriteLine("++++++++++++sent++++++++++++++");
            Console.WriteLine(reportData.sent);

            Console.WriteLine("++++++++++++Opened++++++++++++++");
            Console.WriteLine(reportData.opened);

            Console.WriteLine("++++++++++++clicked++++++++++++++");
            Console.WriteLine(reportData.clicked);

            Console.WriteLine("++++++++++++bounced++++++++++++++");
            Console.WriteLine(reportData.bounced);
        }


        private void exportReportToFtp(String apiKey)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapter();

            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddHours(-8);

            dialogueService.exportReportToFtp(apiKey, "FTP_", startTime, endTime);
        }

        private void getRerportByJobId(String apiKey)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapter();

            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddHours(-8);
            String publishId = "MTA1MzU3NDKMdCDprzC_oRpaAAO2LvZr";
            ReportData reportData = dialogueService.getRerportByJobId(apiKey,publishId, startTime, endTime);

            Console.WriteLine("++++++++++++sent++++++++++++++");
            Console.WriteLine(reportData.sent.Length);

            Console.WriteLine("++++++++++++Opened++++++++++++++");
            Console.WriteLine(reportData.opened.Length);

            Console.WriteLine("++++++++++++clicked++++++++++++++");
            Console.WriteLine(reportData.clicked.Length);

            Console.WriteLine("++++++++++++bounced++++++++++++++");
            Console.WriteLine(reportData.bounced.Length);
        }

        private void exportReport(String apiKey)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapter();

            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddHours(-8);

            CSVReportData reportData = dialogueService.exportReport(apiKey, startTime, endTime);

            Console.WriteLine("++++++++++++sent++++++++++++++");
            Console.WriteLine(reportData.sent);

            Console.WriteLine("++++++++++++Opened++++++++++++++");
            Console.WriteLine(reportData.opened);

            Console.WriteLine("++++++++++++clicked++++++++++++++");
            Console.WriteLine(reportData.clicked);

            Console.WriteLine("++++++++++++bounced++++++++++++++");
            Console.WriteLine(reportData.bounced);
        }

        private String publishingBulkEmail(String apiKey)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();

            String fromName = "周安平";
            String fromAddress = "zapjx@hotmail.com";
            String subject = "publishingBulkEmail邮件测试";
            String reportReceiveAddress = "zapjx@hotmail.com";
            String mailBody = "Hello World";
            String contactFileName = "Contact.csv";
            return dialogueService.publishingBulkEmail(apiKey, contactFileName, fromName, fromAddress, subject, reportReceiveAddress, mailBody);

        }
        private String publishingSmallScaleEmail(String apiKey)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();


            String fromName = "周安平";
            String fromAddress = "zapjx@hotmail.com";
            String subject = "publishingSmallScaleEmail邮件测试";
            String reportReceiveAddress = "zapjx@hotmail.com";
            String mailBody = "Hello World";

            ContactFileInfo contactFileInfo = new ContactFileInfo();
            contactFileInfo.fileType = fileExtension.CSV;
            contactFileInfo.filename = "Contacts.csv";
            contactFileInfo.csvDilimiter = ";";
            contactFileInfo.fileContent = System.IO.File.ReadAllBytes("../../Example/Contacts.csv");

            return dialogueService.publishingSmallScaleEmail(apiKey, contactFileInfo, fromName, fromAddress, subject, reportReceiveAddress, mailBody);
        }

        private String sendSingleMail(String apiKey)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();
            String fromName = "周安平";
            String fromAddress = "zapjx@hotmail.com";
            String subject = "sendSingleMail邮件测试";
            String reportReceiveAddress = "zapjx@hotmail.com";
            String mailBody = "Hello World";

            KeyValuePair[] ContactInfos = new KeyValuePair[3];
            KeyValuePair ContactInfo = new KeyValuePair();
            ContactInfo.key = "email";
            ContactInfo.value = "dynamicemail@163.com";
            ContactInfos[0] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "username";
            ContactInfo.value = "周安平";
            ContactInfos[1] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "password";
            ContactInfo.value = "12234234";
            ContactInfos[2] = ContactInfo;
            return dialogueService.sendSingleEmail(apiKey, fromName, fromAddress, subject, reportReceiveAddress, mailBody, ContactInfos);
        }
        private String registerContact(String apiKey)
        {

            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();

            KeyValuePair[] ContactInfos = new KeyValuePair[3];
            KeyValuePair ContactInfo = new KeyValuePair();
            ContactInfo.key = "email";
            ContactInfo.value = "dynamicemail@163.com";
            ContactInfos[0] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "username";
            ContactInfo.value = "周安平";
            ContactInfos[1] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "password";
            ContactInfo.value = "12234234";
            ContactInfos[2] = ContactInfo;
            return dialogueService.registerContact(apiKey, ContactInfos);
        }
        private SendSmsResult sendSms(String apiKey)
        {

            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();

            String dateTime = DateTime.Now.ToShortTimeString();
            return dialogueService.sendSMS(apiKey, "13671935968", dateTime, "Hello World");
        }
    }
}
