using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSCP;
using TripolisDialogueAdapter.Action;
using TripolisDialogueAdapter.cn.tripolis.dialogue.import;
using TripolisDialogueAdapter.cn.tripolis.dialogue.publish;
using System.IO;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using TripolisDialogueAdapter.cn.tripolis.dialogue.subscription;

namespace TripolisDialogueAdapter
{
    public class DiaLogueSeriveFor51
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DiaLogueSeriveFor51));
        private string userName;
        private string password;
        private string client;
        public System.Net.WebProxy oWebProxy;
        /// <summary>
        /// Initial the webservice API.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        public DiaLogueSeriveFor51(String client, String userName, String password, System.Net.WebProxy oWebProxy)
        {
            this.client = client;
            this.userName = userName;
            this.password = password;
            this.oWebProxy = oWebProxy;

        }


        private String getHtmlTitle(String html, String tag, String attr)
        {
            String title = "";
            Regex re = new Regex(@"(<" + tag + @"[\w\W].+?>)");
            MatchCollection titleHtml = re.Matches(html);

           
            for (int i = 0; i < titleHtml.Count; i++)
            {
                title = Regex.Replace(titleHtml[i].ToString(), @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                break;
            }
            return title;
        }

        #region FTP file
        private ArrayList ListFiles(String HostName, String UserName, String Password, String remotePath)
        {
            int result = 0;
            Session session = null;
            ArrayList fileNameList = new ArrayList();
            try
            {
                // Setup session options               
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = HostName,
                    UserName = UserName,
                    Password = Password,
                    //HostName =  "119.59.73.48",
                    //UserName = "ftp@yourzine.com.cn",
                    //Password =  "989898qw",
                    //  SshHostKeyFingerprint = "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx"
                };

                using (session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);



                    RemoteDirectoryInfo dirInfo = session.ListDirectory(remotePath);

                    foreach (RemoteFileInfo file in dirInfo.Files)
                    {

                        if (!file.IsDirectory)
                        {
                            fileNameList.Add(file.Name);
                        }
                        // Console.WriteLine("File Name: {0},LastWriteTime: {1},IsDirectory: {2},File Length: {3},", file.Name, file.LastWriteTime, file.IsDirectory, file.Length);
                    }

                }

                // return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                //  return 1;
            }
            finally
            {
                if (session != null)
                {
                    session.Dispose();
                }
            }

            return fileNameList;
        }

        private int Download(String HostName, String UserName, String Password, String remoteFilePath, String localPath)
        {
            int result = 0;
            Session session = null;

            // Setup session options               
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = HostName,
                UserName = UserName,
                Password = Password,                
                //HostName =  "119.59.73.48",
                //UserName = "ftp@yourzine.com.cn",
                //Password =  "989898qw",
                //  SshHostKeyFingerprint = "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx"
            };

           
            try
            {

                using (session = new Session())
                {

                    // Connect    
                    session.ReconnectTime = new TimeSpan(0, 0, 30);
                    session.Open(sessionOptions);

                    // Download files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;
                  //  transferOptions.ResumeSupport.State = TransferResumeSupportState.Smart;
                    //transferOptions.ResumeSupport.Threshold = 1000;
                    

                    TransferOperationResult transferResult = null;
                Retry:
                    try
                    {
                        transferResult = session.GetFiles(remoteFilePath, localPath, false, transferOptions);


                        // Throw on any error
                        transferResult.Check();
                        // Print results
                        foreach (TransferEventArgs transfer in transferResult.Transfers)
                        {
                            Console.WriteLine("Download of {0} succeeded", transfer.FileName);
                        }


                        result = 0;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}", e);
                        goto Retry;

                        // result = 1;
                    }
                }
               
            }
            finally
            {
                if (session != null)
                {
                    session.Dispose();
                }
            }
            return result;
        }
        private int Upload(String HostName, String UserName, String Password, String remotePath, String localFilePath)
        {
            int result = 0;
            Session session = null;
            try
            {
                // Setup session options               
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = HostName,
                    UserName = UserName,
                    Password = Password,
                    //HostName =  "119.59.73.48",
                    //UserName = "ftp@yourzine.com.cn",
                    //Password =  "989898qw",
                    //  SshHostKeyFingerprint = "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx"
                };

                using (session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);



                    // upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Ascii;


                    TransferOperationResult transferResult = null;

                    transferResult = session.PutFiles(localFilePath, remotePath, false, transferOptions);


                    // Throw on any error
                    transferResult.Check();
                    // Print results
                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {
                        Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                    }

                }

                result = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                result = 1;
            }
            finally
            {
                if (session != null)
                {
                    session.Dispose();
                }
            }
            return result;
        }

        public int removeFTPFiles(String HostName, String UserName, String Password, String remoteFilePath)
        {
            int result = 0;
            Session session = null;
            try
            {
                // Setup session options               
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = HostName,
                    UserName = UserName,
                    Password = Password,
                    //HostName =  "119.59.73.48",
                    //UserName = "ftp@yourzine.com.cn",
                    //Password =  "989898qw",
                    //  SshHostKeyFingerprint = "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx"
                };

                using (session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    // Remove files                 

                    RemovalOperationResult removalResult = null;
                    removalResult = session.RemoveFiles(remoteFilePath);

                    // Throw on any error
                    removalResult.Check();
                    // Print results
                    foreach (RemovalEventArgs removal in removalResult.Removals)
                    {
                        Console.WriteLine("Remove of {0} succeeded", removal.FileName);
                    }

                }

                result = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                result = 1;
            }
            finally
            {
                if (session != null)
                {
                    session.Dispose();
                }
            }
            return result;
        }
        public String FTPFile(String HostName, String UserName, String Password, String remoteFilePath, String localPath)
        {
            String ftpFileName = "";
            int result = 0;
            //get file list from ftp server
            ArrayList FileNameList = ListFiles(HostName, UserName, Password, remoteFilePath);
            foreach (String fileName in FileNameList)
            {
                if (!Path.GetExtension(fileName).Equals(".CSV", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                Console.WriteLine("Get Remote File:{0}", fileName);

                if (FileNameList.Contains(Path.GetFileNameWithoutExtension(fileName) + ".html") || FileNameList.Contains(Path.GetFileNameWithoutExtension(fileName) + ".HTML"))
                {
                    String htmlFileName = Path.GetFileNameWithoutExtension(fileName) + ".html";
                    //Download HTML file from ftp server
                    result = this.Download(HostName, UserName, Password, remoteFilePath + htmlFileName, localPath);
                    if (result != 0)
                    {
                        continue;
                    }
                    //Download CSV file from ftp server
                    result = this.Download(HostName, UserName, Password, remoteFilePath + fileName, localPath);
                    if (result != 0)
                    {
                        continue;
                    }

                    String localCSVFileName = localPath + fileName;
                    String localHtmlFileName = localPath + htmlFileName;
                    string fileContent = File.ReadAllText(localCSVFileName, Encoding.UTF8);
                    string revisedContent = fileContent.Replace("||", ";");
                    File.WriteAllText(localCSVFileName, revisedContent, Encoding.UTF8);
                    result = this.Upload(HostName, UserName, Password, remoteFilePath, localCSVFileName);
                    if (result == 0)//success
                    {
                        File.Delete(localPath + "backup/" + fileName);
                        File.Delete(localPath + "backup/" + htmlFileName);
                        File.Move(localCSVFileName, localPath + "backup/" + fileName);
                        File.Move(localHtmlFileName, localPath + "backup/" + htmlFileName);
                        ftpFileName = fileName;
                        //    this.sendMail(
                        break;
                    }


                }


            }
            return ftpFileName;

        }
        #endregion
       
        public bool IsFileInUse(string fileName)
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
        public String FormatFile(String localPath)
        {
            String retFileName = "";
            ArrayList FileNameList = new ArrayList();
            string[] files = Directory.GetFiles(localPath, "*", SearchOption.TopDirectoryOnly);
            FileNameList.AddRange(files);
            foreach (String fileName in FileNameList)
            {
                if (!Path.GetExtension(fileName).Equals(".CSV", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                if(this.IsFileInUse(fileName)){
                    continue;
                }

                if (FileNameList.Contains(localPath + Path.GetFileNameWithoutExtension(fileName) + ".html") || FileNameList.Contains(localPath + Path.GetFileNameWithoutExtension(fileName) + ".HTML"))
                {

                    Console.WriteLine("Get File:{0}", fileName);
                   
                    string fileContent = File.ReadAllText(fileName, Encoding.UTF8);
                    string revisedContent = fileContent.Replace("||", ";");
                    File.WriteAllText(fileName, revisedContent, Encoding.UTF8);
                    retFileName = Path.GetFileName(fileName);
                    break;
                }
            }
            return retFileName;
        }

        public void archiveImportedFile(String srcDir, String descDir,String fileName)
        {
            logger.InfoFormat("Begin Backup file to " + descDir);
            String htmlFileName = Path.GetFileNameWithoutExtension(fileName) + ".html";          
            File.Delete(descDir + fileName);
            File.Delete(descDir + htmlFileName);
            File.Move(srcDir + fileName, descDir + fileName);
            File.Move(srcDir + htmlFileName, descDir + htmlFileName);
            logger.InfoFormat("Finish Backup file to " + descDir);
        }

        

        public String sendNotification(String contactDatabaseId, String workspaceId, String emailTypeId, String emailAddress, String subject, String emailContent)
        {
            DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
            String emailId = directEmailAction.createDirectEmail(emailTypeId, "Notification", "notification", subject, "", "Tripolis Support", emailAddress, emailContent);
            SubscribeAction subscribeAction = new SubscribeAction(client, userName, password, oWebProxy);
            String contactId = subscribeAction.subscribeSingleEmail(contactDatabaseId, workspaceId, emailAddress, emailId);
            return contactId;
        }


        public String  importContact(String transId, String fileName, String contactDatabaseId,String reportReceiverAddress, String ftpAccountId)
        {
            logger.InfoFormat("***********Begin importing contact File " + fileName + " for transid " + transId+"***********");
           
            String strDate = DateTime.Now.ToString("yyyyMMdd");
            String groupName = strDate + "_" + transId;
           
            //Create Group             
            ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password, oWebProxy);
            String groupId = contactGroupAction.createContactGroup(contactDatabaseId, groupName, groupName);
            logger.InfoFormat("Create Group " + groupName + " successfully");

          
            DateTime scheduleAt = DateTime.Now;
            ImportContactAction importContactAction = new ImportContactAction(client, userName, password, oWebProxy);
            String importId = importContactAction.importContactFromFTP(contactDatabaseId, groupId, fileName, reportReceiverAddress, ftpAccountId, scheduleAt);
            Thread.Sleep(5 * 1000);
            while (true)
            {
                importContactAction = new ImportContactAction(client, userName, password, oWebProxy);
                importStatus status = importContactAction.getImportStatus(importId);

                if (status.Equals(importStatus.ENDED))
                {
                    logger.InfoFormat("import contact File " + fileName + " for transid" + transId + " successfully");
                    break;
                }
                if (status.Equals(importStatus.STOPPED) || status.Equals(importStatus.ABORTED))
                {
                    logger.InfoFormat("fail to import contact File " + fileName + " for transid " + transId + " as import status is " + status.ToString());
                   // throw new Exception("fail to import contact File " + fileName + " for transid " + transId + " as import status is " + status.ToString());
                    return "";
                    //break;
                }
                logger.InfoFormat("Checking the import status, and current import status is " + status.ToString() + " for transid " + transId);
                Thread.Sleep(60 * 1000);
            }
            return groupId;

        }

        public void sendMail(String EmailName, String groupId, String contactDatabaseId, String workspaceId, String emailTypeId, String fromName,
            String fromAddress, String reportReceiverAddress,  String subject, String htmlSource)
        {

            logger.InfoFormat("Begin sending mail for email " + EmailName);           

            //send test mail           
            this.sendNotification(contactDatabaseId, workspaceId, emailTypeId, reportReceiverAddress, subject+"[测试,请确认]", htmlSource);
            logger.InfoFormat("send test mail for email " + EmailName + " successfully");
           
            //Create Email
            DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
            String emailId = directEmailAction.createDirectEmail(emailTypeId, EmailName, EmailName, subject, "", fromName, fromAddress, htmlSource);
            logger.InfoFormat("Create email " + EmailName +  " successfully");

          //  return;
            //Publish Email
            logger.InfoFormat("Begin sending email  " + EmailName );            
            PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
            DateTime scheduleAt = DateTime.Now.AddMinutes(15);
            String publishId = publishingAction.publishingEmail(groupId, emailId, scheduleAt);
            Thread.Sleep(5 * 1000);
            while (true)
            {               
                publishingAction = new PublishingAction(client, userName, password, oWebProxy);
                Job job = publishingAction.getPublishStatus(publishId);
                if (job.status.Contains("ENDED"))
                {                    
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Sending Report");
                    sb.AppendLine("jobName=" + job.jobName);
                    sb.AppendLine("contactDatabaseLabel=" + job.contactDatabaseLabel);
                    sb.AppendLine("workspaceLabel=" + job.workspaceLabel);
                    sb.AppendLine("contactGroupLabel=" + job.contactGroupLabel);
                    sb.AppendLine("channel=" + job.channel);
                    sb.AppendLine("requestedNumberOfSend=" + job.requestedNumberOfSend);
                    sb.AppendLine("numberOfSend=" + job.numberOfSend);
                    sb.AppendLine("numberOfSkipped=" + job.numberOfSkipped);
                    

                   // Console.WriteLine(sb.ToString());
                    logger.InfoFormat("finish sending mail  " + EmailName + ", the sending status  is " + job.status + ", sending report is " + sb.ToString());
                    this.sendNotification(contactDatabaseId, workspaceId, emailTypeId, reportReceiverAddress, EmailName + "_邮件发送报告", sb.ToString());
                  
                    break;
                }
                logger.InfoFormat("Checking the sending status, and current sending status is " + job.status.ToString() + " for email: " + EmailName);
                Thread.Sleep(60 * 1000);
            }
            logger.InfoFormat("finish sending mail for email:" + EmailName);
        }
    }
}
