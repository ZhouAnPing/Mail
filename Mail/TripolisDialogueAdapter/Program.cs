using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

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

            ArrayList list = new ArrayList();
            String contactJson =
               "email#an-ping.zhou@hp.com;username#anping;comment#hp";
            list.Add(contactJson);

            contactJson =
               "email#zapjx@hotmail.com;username#anping;comment#hotmail"; 
            list.Add(contactJson);

            contactJson =
                 "email#zhouyouchen@gmail.com;username#anping;comment#gmail"; 
            list.Add(contactJson);

            contactJson =
                 "email#1197922021@qq.com;username#anping;comment#qq";
            list.Add(contactJson);
          
            
            DialogueService dialogueService = new DialogueService(client, userName, password,null);

            DateTime startTime = DateTime.Now.AddHours(-8);

            DateTime endTime = DateTime.Now;

            String result = dialogueService.SyncFeedbackInfo("MjU0OTI1NDnmTzMfCdUq6w", startTime, endTime);
            System.Console.WriteLine(result);

            //foreach (String tempContact in list)
            //{
            //    dialogueService.sendSingleEmail("B00001",tempContact, "", "");
            //    Thread.Sleep(5000);
            //}
          
            System.Console.WriteLine("Finish Action");
            System.Console.ReadLine();
        }
    }
}
