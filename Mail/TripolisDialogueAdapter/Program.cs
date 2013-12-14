using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using TripolisDialogueAdapter.BO;

namespace TripolisDialogueAdapter
{
    
    class Program
    {
        static void sendBatchMail()
        {
            String client = "Training";
            String userName = "zapjx@hotmail.com";
            String password = "Zhouanping123";


            MailAdapter mailAdapter = new MailAdapter(client, userName, password,null);
            MailData mailData = new MailData();

            mailData.fromAddress = "zapjx@hotmail.com";
            mailData.replyAddress = "zapjx@hotmail.com";
            mailData.sender = "优赞(上海)";
            mailData.subject = "邮件测试";
            mailData.mailBody = System.IO.File.ReadAllText("../../Readme.html");

            String contactJson =
                "email#an-ping.zhou@hp.com,surveylink#http://www.google.com,pid#123456-1";
            mailData.ContactJsonList.Add(contactJson);

            contactJson =
                "email#zhouyouchen@gmail.com,surveylink#http://www.google.com,pid#123456-1";
            mailData.ContactJsonList.Add(contactJson);

            mailAdapter.sendBatchMail("T1", mailData);

        }

        static void sendSingleMail()
        {
            String client = "Training";
            String userName = "zapjx@hotmail.com";
            String password = "Zhouanping123";
            MailAdapter mailAdapter = new MailAdapter(client, userName, password,null);
            String mailBody = System.IO.File.ReadAllText("../../Readme.html");
            mailAdapter.sendSingleEmail("T1", "23456789", "Test", "zhouyouchen@gmail.com", "zapjx@hotmail.com", "Test By An-Ping", mailBody);

        }
        static void Main(string[] args)
        {
            //String client = "IPSOS API";//Training";
            //String userName = "survey@ipsos.com";//zapjx@hotmail.com";
            //String password = "Test123";

            String client = "Training";
            String userName = "zapjx@hotmail.com";
            String password = "Test123";
            DialogueService_new dialogueService = new DialogueService_new(client, userName, password, null);

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
            ContactInfo.value = "zapjx@hotmail.com";
            ContactInfos[0] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "username";
            ContactInfo.value = "周安平";
            ContactInfos[1] = ContactInfo;
            ContactInfo = new KeyValuePair();
            ContactInfo.key = "password";
            ContactInfo.value = "12234234";
            ContactInfos[2] = ContactInfo;
            directEmail.subject = "单个邮件测试";
            dialogueService.sendSingleEmail(dialogueSetting, directEmail, ContactInfos);

            dialogueSetting.directEmailId = "MzczMDk1Mzc98HDFHcJFcg";
            directEmail.subject = "注册邮件测试";
            dialogueService.registerContact(dialogueSetting, ContactInfos);


            ContactGroup contactGroup = new ContactGroup();
            contactGroup.groupLabel = "Demo Group";
            contactGroup.groupName = "demogroup1";

            ImportFiles importFiles = new ImportFiles();
            importFiles.fileType = cn.tripolis.dialogue.import.fileExtension.CSV;
            importFiles.filename = "Contacts.csv";
            importFiles.csvDilimiter = ImportFiles.DEFAULT_CSV_DELIMIT;
            importFiles.fileContent = System.IO.File.ReadAllBytes("../../Example/Contacts.csv");
            directEmail.subject = "小批量邮件测试";
            dialogueService.publishingSmallScaleEmail(dialogueSetting, contactGroup, importFiles, directEmail);


            dialogueSetting.ftpAccountId = "NTM5NTM5NTNW_uqPXJDMzQ";
            directEmail.subject = "大批量邮件测试";
            dialogueService.publishingBulkEmail(dialogueSetting, contactGroup, "Contacts.xls", directEmail);

            System.Console.WriteLine("Finish Action");
            System.Console.ReadLine();
        }
    }
}
