using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSCP;
using System.IO;
using TripolisDialogueAdapter;
using System.Threading;
using TripolisDialogueAdapter.cn.tripolis.dialogue.publish;
using System.Collections;
using System.Text.RegularExpressions;
using System.Security.AccessControl;
using System.Net;
using SendMailForJob.Properties;

namespace SendMailForJob
{
    class MailJob
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MailJob));
        /// <summary>
        /// Find title value
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <param name="attr"></param>
        /// <returns></returns>

        private String getHtmlTitle(String html, String tag, String attr)
        {
            String title = "";
            Regex re = new Regex(@"(<" + tag + @"[\w\W].+?>)", RegexOptions.IgnoreCase);
            MatchCollection titleHtml = re.Matches(html);

            for (int i = 0; i < titleHtml.Count; i++)
            {
                title = Regex.Replace(titleHtml[i].ToString(), @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                break;
            }
            return title;
        }

        /// <summary>
        /// check if the file is in use.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsFileInUse(string fileName)
        {
            bool inUse = true;

            FileStream fs = null;
            try
            {

                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,

                FileShare.None);

                inUse = false;
            }
            catch
            {

            }
            finally
            {
                if (fs != null)

                    fs.Close();
            }
            return inUse;//true表示正在使用,false没有使用
        }

        public void Loop4Mail()
        {
            logger.InfoFormat("Begin loop mail for processing");
            while (true)
            {

                try
                {
                    String client = Settings.Default.client;// "training";
                    String userName = Settings.Default.userName;// "zapjx@hotmail.com";
                    String password = Settings.Default.password;// "Test123";

                    String contactDatabaseId = Settings.Default.contactDatabaseId;// "MjU1OTI1NTm6UFiCZQIokg";
                    String workspaceId = Settings.Default.workspaceId;// "MjAxNjIwMTYrcyIPWpHd*w";
                    String emailTypeId = Settings.Default.emailTypeId;// "MTc3NDE3NzR93tsKXN3m4A";
                    String ftpAccountId = Settings.Default.ftpAccountId;// "NTMzNTMzNTMdFDFVppf2iQ";

                    String fromName = "前程无忧(51Job）";// Settings.Default.fromName;// "51Job";
                    String fromAddress = Settings.Default.fromAddress;// "zapjx@hotmail.com";
                    String reportReceiverAddress = Settings.Default.reportReceiverAddress;// "zapjx@hotmail.com";

                    String FTPLocalPath = Settings.Default.FTPLocalPath;// @"C:\Users\zhouzhou\Desktop\FTP DEMO\Test\";
                    String BackupPath = Settings.Default.BackupPath;// @"C:\Users\zhouzhou\Desktop\FTP DEMO\Test\";

                    DiaLogueSeriveFor51 diaLogueSeriveFor51 = new DiaLogueSeriveFor51(client, userName, password, null);

                    String ftpFileName = diaLogueSeriveFor51.FormatFile(FTPLocalPath);
                     
                    logger.InfoFormat("begin process " + ftpFileName);
                    if (String.IsNullOrEmpty(ftpFileName))
                    {
                        logger.InfoFormat("No Contact is found" + ",wating for next Loop...");
                        Thread.Sleep(60 * 1000);
                        continue;
                    }

                    String htmlSource = File.ReadAllText(FTPLocalPath + Path.GetFileNameWithoutExtension(ftpFileName) + ".html", Encoding.Default);
                    //  String htmlSource = File.ReadAllText(FTPLocalPath + "backup/" + Path.GetFileNameWithoutExtension(ftpFileName) + ".html", Encoding.Default);
                    String subject = this.getHtmlTitle(htmlSource, "title", "");
                    if (String.IsNullOrEmpty(subject) || String.IsNullOrWhiteSpace(subject))
                    {
                        diaLogueSeriveFor51.sendNotification(contactDatabaseId, workspaceId, emailTypeId, reportReceiverAddress, "邮件没有主题，请添加html的title属性，重新上传", htmlSource);
                        Thread.Sleep(60 * 1000);
                        continue;
                    }

                    String TransId = Path.GetFileNameWithoutExtension(ftpFileName);
                    if (TransId == null)
                    {
                        logger.InfoFormat("Transaction Id is blank");
                        Thread.Sleep(60 * 1000);
                        continue;
                    }
                    String[] arr = TransId.Split('_');
                    if (arr == null || arr.Length <= 1)
                    {
                        logger.InfoFormat("FileName format is wrong");
                        Thread.Sleep(60 * 1000);
                        continue;
                    }

                    TransId = arr[arr.Length - 1];

                    String contactGroupId = diaLogueSeriveFor51.importContact(TransId, ftpFileName, contactDatabaseId, reportReceiverAddress, ftpAccountId);
                    if (String.IsNullOrEmpty(contactGroupId))
                    {
                        logger.InfoFormat("Fail to import Contact");
                        Thread.Sleep(60 * 1000);
                        continue;
                    }
                    String strDate = DateTime.Now.ToString("yyyyMMdd");
                    String EmailName = strDate + "_" + TransId;
                    diaLogueSeriveFor51.sendMail(EmailName, contactGroupId, contactDatabaseId, workspaceId, emailTypeId, fromName, fromAddress, reportReceiverAddress, subject, htmlSource);

                    diaLogueSeriveFor51.archiveImportedFile(FTPLocalPath, BackupPath, ftpFileName);
                    logger.InfoFormat("finish process " + ftpFileName + ",Next Loop...");


                }
                catch (Exception ex)
                {
                    // logger.InfoFormat("Backup file to " + descDir);
                    logger.InfoFormat(ex.Message + ",Next Loop...");
                }

                Thread.Sleep(60 * 1000);
            }


          //  return 0;

        }

        //private int testFTP()
        //{
        //    try
        //    {

        //        bool isInuse = this.IsFileInUse(@"C:\Users\zhouzhou\Desktop\FTP DEMO\Test\birthday1_10002.CSV");
        //        WebClient webClient = new WebClient();
        //        // webClient.Credentials= new Crede

        //        String client = "training";
        //        String userName = "zapjx@hotmail.com";
        //        String password = "Test123";
        //        DiaLogueSeriveFor51 diaLogueSeriveFor51 = new DiaLogueSeriveFor51(client, userName, password, null);
        //        String FTPHostName = "119.59.73.48";
        //        String FTPUserName = "ftp@yourzine.com.cn";
        //        String FTPPassword = "989898qw";
        //        String FTPRemotePath = "/Demo/";
        //        String FTPLocalPath = @"C:\Users\zhouzhou\Desktop\FTP DEMO\Test\";
        //        String ftpFileName = diaLogueSeriveFor51.FormatFile(FTPLocalPath);
        //        //  String ftpFileName = diaLogueSeriveFor51.FTPFile(FTPHostName, FTPUserName, FTPPassword, FTPRemotePath, FTPLocalPath);
        //        if (!String.IsNullOrEmpty(ftpFileName))
        //        {

        //            String contactDatabaseId = "MjU1OTI1NTm6UFiCZQIokg";
        //            String workspaceId = "MjAxNjIwMTYrcyIPWpHd*w";
        //            String emailTypeId = "MTc3NDE3NzR93tsKXN3m4A";
        //            String ftpAccountId = "NTMzNTMzNTMdFDFVppf2iQ";
        //            String reportReceiverAddress = "zapjx@hotmail.com";
        //            String htmlSource = File.ReadAllText(FTPLocalPath + Path.GetFileNameWithoutExtension(ftpFileName) + ".html", Encoding.Default);
        //            //  String htmlSource = File.ReadAllText(FTPLocalPath + "backup/" + Path.GetFileNameWithoutExtension(ftpFileName) + ".html", Encoding.Default);
        //            String subject = this.getHtmlTitle(htmlSource, "title", "");
        //            if (String.IsNullOrEmpty(subject) || String.IsNullOrWhiteSpace(subject))
        //            {
        //                diaLogueSeriveFor51.sendNotification(contactDatabaseId, workspaceId, emailTypeId, reportReceiverAddress, "邮件没有主题，将使用默认主题", htmlSource);
        //                subject = "AnPing 51Demo";

        //            }
        //            diaLogueSeriveFor51.sendMail(ftpFileName, contactDatabaseId, workspaceId, emailTypeId, "AnPing Demo51", "zapjx@hotmail.com", reportReceiverAddress, ftpAccountId, subject, htmlSource);

        //            diaLogueSeriveFor51.archiveImportedFile(FTPLocalPath, FTPLocalPath + "backup/" ,ftpFileName);
        //            //  diaLogueSeriveFor51.removeFTPFiles(FTPHostName, FTPUserName, FTPPassword, FTPRemotePath + Path.GetFileNameWithoutExtension(ftpFileName) + ".*");
        //            Console.ReadLine();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message + ",Next Loop Begin");
        //    }


        //    return 0;

        //} 
    }
}
