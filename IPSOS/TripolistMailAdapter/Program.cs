using System;
using TripolistMailAdapter.cn.tripolis.dialogue.export;

namespace TripolistMailAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
            //log.Debug("Enter initMenus()");

            String client = "IPSOS API";//Training";
            String userName = "survey@ipsos.com";//zapjx@hotmail.com";
            String password = "Test123";

            //String client = "Training";
            //String userName = "zapjx@hotmail.com";
            //String password = "Test123";            


            //String client = "51job";//Training";
            //String userName = "api@51job.com";//zapjx@hotmail.com";
            //String password = "Test123";
     
            MailAdapter mailAdapter = new MailAdapter(client, userName, password);
            MailData mailData = new MailData
                {
                    FromAddress = "dynamicemail@163.com",
                    ReplyAddress = "dynamicemail@163.com",
                    SendName = "AnPing From .Net Platform",
                    Subject = "Test:Send mail to Vivi hotmail through .net platform",
                    Content = System.IO.File.ReadAllText("../../content.html")
                };

            // mailData.MailId = "123123sdfsdfsdfsdf2323";

            String contactJson = "email:an-ping.zhou@hp.com";
            mailData.ContactJsonList.Add(contactJson);

          //  mailAdapter.sendSingleEmail("IPSOS_BATCH", "a2b7e5ce-b179-40e2-9094-5a572f11bb3b", mailData.SendName, mailData.FromAddress, mailData.ReplyAddress, mailData.Subject, mailData.Content);
          
            //mailAdapter.sendMail("IPSOS_TEST", mailData);



            DateTime startTime = DateTime.Now.AddHours(-12);

            DateTime endTime = DateTime.Now;

            // DateTime startTime = DateTime.Parse("2014-05-06 10:00:00");

           // DateTime endTime =  DateTime.Parse("2014-05-07 10:00:00");
            mailAdapter.getFeedbackInfo("IPSOS_Batch", startTime, endTime);
            System.Console.ReadLine();
        }


    }
}
