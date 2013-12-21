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
            for (int i = 0; i < 1; i++)
            {
                DateTime startTime = DateTime.Now;
               // TestProgram.sendmailForCtrip();
                DateTime endTime = DateTime.Now;

                System.TimeSpan ts = endTime - startTime;

                Console.WriteLine("时间差:" + ts.Seconds);
            }

            TestProgram.exportReport();

            Console.ReadLine();
            //String publishId =  TestProgram.registerContact(authorization);
            //Console.WriteLine("registerContact Id=" + publishId);

            //publishId = TestProgram.sendSingleMail(authorization);
            //Console.WriteLine("sendSingleMai Id=" + publishId);

            //publishId = TestProgram.publishingSmallScaleEmail(authorization);
            //Console.WriteLine("publishingSmallScaleEmail Id=" + publishId);

            //publishId = TestProgram.publishingBulkEmail(authorization);
            //Console.WriteLine("publishingBulkEmail Id=" + publishId);


        }

        private void sendmailForCtrip()
        {
            CtripMailAdapterWs.CtripMailAdapter1 dialogueService = new CtripMailAdapterWs.CtripMailAdapter1();
     
            String apiKey = "MjU4MDI1ODCzAn45YUUpJw";
            String fromName = "Ctrip API Demo";
            String fromEmail = "API@Ctrip.com";
            String subject = "Ctrip API Demo";
            string mailContent = System.IO.File.ReadAllText("../../Example/template.html");
            String toEmail = "1197922021@qq.com";
            DateTime scheduleTime = DateTime.Now;
             String publishId =  dialogueService.sendMail(apiKey, fromName, fromEmail, subject, mailContent, toEmail, scheduleTime);
           Console.WriteLine("PublishId="+publishId);
        }
        private void exportReport()
        {
            CtripMailAdapterWs.CtripMailAdapter1 dialogueService = new CtripMailAdapterWs.CtripMailAdapter1();
            String apiKey = "MjU4MDI1ODCzAn45YUUpJw";
            DateTime endTime = DateTime.Now;
            DateTime startTime = endTime.AddHours(-8);
            CtripMailAdapterWs.CSVReportData reportData= dialogueService.exportReport(apiKey, startTime, endTime);

            Console.WriteLine("++++++++++++sent++++++++++++++");
            Console.WriteLine(reportData.sent);

            Console.WriteLine("++++++++++++Opened++++++++++++++");
            Console.WriteLine(reportData.opened);

            Console.WriteLine("++++++++++++clicked++++++++++++++");
            Console.WriteLine(reportData.clicked);

            Console.WriteLine("++++++++++++bounced++++++++++++++");
            Console.WriteLine(reportData.bounced);
        }

        private String publishingBulkEmail(Authorization authorization)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = "MjU1MTI1NTFFUVus6S83qA";
            dialogueSetting.workspaceId = "MjAwNzIwMDdKRmT4g3bWOg";
            dialogueSetting.emailTypeId = "MTc2MDE3NjBcUp_pC*h71w";

            DirectEmail directEmail = new DirectEmail();
            String sequence = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            directEmail.emailLabel = sequence;
            directEmail.emailName = sequence;
            directEmail.subject = "大批量邮件测试";
            directEmail.description = "API邮件测试";
            directEmail.fromName = "周安平";
            directEmail.fromAddress = "zapjx@hotmail.com";
            directEmail.reportReceiveAddress = "zapjx@hotmail.com";
            directEmail.htmlContent = "Hello World";

            ContactGroup contactGroup = new ContactGroup();
            contactGroup.groupLabel = "Demo Group";
            contactGroup.groupName = "demogroup1";

            dialogueSetting.ftpAccountId = "NTM5NTM5NTNW_uqPXJDMzQ";
            
            return dialogueService.publishingBulkEmail(authorization,dialogueSetting, contactGroup, "Contacts.xls", directEmail);

        }

        private String publishingSmallScaleEmail(Authorization authorization)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = "MjU1MTI1NTFFUVus6S83qA";
            dialogueSetting.workspaceId = "MjAwNzIwMDdKRmT4g3bWOg";
            dialogueSetting.emailTypeId = "MTc2MDE3NjBcUp_pC*h71w";

            DirectEmail directEmail = new DirectEmail();
            String sequence = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            directEmail.emailLabel = sequence;
            directEmail.emailName = sequence;
            directEmail.subject = "小批量邮件测试";
            directEmail.description = "API邮件测试";
            directEmail.fromName = "周安平";
            directEmail.fromAddress = "zapjx@hotmail.com";
            directEmail.reportReceiveAddress = "zapjx@hotmail.com";
            directEmail.htmlContent = "Hello World";

            ContactGroup contactGroup = new ContactGroup();
            contactGroup.groupLabel = "Demo Group";
            contactGroup.groupName = "demogroup1";

            ImportFiles importFiles = new ImportFiles();
            importFiles.fileType = fileExtension.CSV;
            importFiles.filename = "Contacts.csv";
            importFiles.csvDilimiter = ";";
            importFiles.fileContent = System.IO.File.ReadAllBytes("../../Example/Contacts.csv");

           return  dialogueService.publishingSmallScaleEmail(authorization,dialogueSetting, contactGroup, importFiles, directEmail);
        }

        private String sendSingleMail(Authorization authorization)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = "MjU1MTI1NTFFUVus6S83qA";
            dialogueSetting.workspaceId = "MjAwNzIwMDdKRmT4g3bWOg";
            dialogueSetting.emailTypeId = "MTc2MDE3NjBcUp_pC*h71w";

            DirectEmail directEmail = new DirectEmail();
            String sequence = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            directEmail.emailLabel = sequence;
            directEmail.emailName = sequence;
            directEmail.subject = "单个邮件测试";
            directEmail.description = "API邮件测试";
            directEmail.fromName = "周安平";
            directEmail.fromAddress = "zapjx@hotmail.com";
            directEmail.reportReceiveAddress = "zapjx@hotmail.com";
            directEmail.htmlContent = "Hello World";

            KeyValuePair[] ContactInfos = new KeyValuePair[3];
            KeyValuePair ContactInfo = new KeyValuePair();
            ContactInfo.key = "email";
            ContactInfo.value = "1197922021@qq.com";
            ContactInfos[0] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "username";
            ContactInfo.value = "周安平";
            ContactInfos[1] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "password";
            ContactInfo.value = "12234234";
            ContactInfos[2] = ContactInfo;           

            return dialogueService.sendSingleEmail(authorization, dialogueSetting, directEmail, ContactInfos);
        }


        private String registerContact(Authorization authorization)
        {
            MailAdapterWs.MailAdapter dialogueService = new MailAdapterWs.MailAdapter();

            DialogueSetting dialogueSetting = new DialogueSetting();
            dialogueSetting.contactDatabaseId = "MjU1MTI1NTFFUVus6S83qA";
            dialogueSetting.workspaceId = "MjAwNzIwMDdKRmT4g3bWOg";
            dialogueSetting.directEmailId = "MzczMDk1Mzc98HDFHcJFcg";       

            KeyValuePair[] ContactInfos = new KeyValuePair[3];
            KeyValuePair ContactInfo = new KeyValuePair();
            ContactInfo.key = "email";
            ContactInfo.value = "1197922021@qq.com";
            ContactInfos[0] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "username";
            ContactInfo.value = "周安平";
            ContactInfos[1] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "password";
            ContactInfo.value = "12234234";
            ContactInfos[2] = ContactInfo;       
           

            return dialogueService.registerContact(authorization,dialogueSetting, ContactInfos);
        }

       
    }
}
