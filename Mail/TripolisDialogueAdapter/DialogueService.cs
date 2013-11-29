using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using TripolisDialogueAdapter.cn.tripolis.dialogue.export;
using TripolisDialogueAdapter.Action;
using TripolistMailAdapter;
using TripolisDialogueAdapter.DAO;
using System.Data;
using TripolisDialogueAdapter.cn.tripolis.dialogue.import;
using TripolisDialogueAdapter.cn.tripolis.dialogue.publish;
namespace TripolisDialogueAdapter
{
   public   class DialogueService
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MailAdapter));
        private string userName;
        private string password;
        private string client;
        private cn.tripolis.dialogue.subscription.SubscriptionService subscriptionService = null;
        private cn.tripolis.dialogue.subscription.AuthInfo subscriptionAuthInfo = null;
        private cn.tripolis.dialogue.export.ExportService exportService = null;
        private cn.tripolis.dialogue.export.AuthInfo exportAuthInfo = null;
        private cn.tripolis.dialogue.import.ImportService importService = null;
        private cn.tripolis.dialogue.import.AuthInfo importAuthInfo = null;
        public System.Net.WebProxy oWebProxy;
        /// <summary>
        /// Initial the webservice API.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        public DialogueService(String client, String userName, String password, System.Net.WebProxy oWebProxy)
        {
            this.client = client;
            this.userName = userName;
            this.password = password;
            this.oWebProxy = oWebProxy;
            //string strDomain = ConfigurationSettings.AppSettings["domain"].ToString();
            ////判断是否启用代理服务器
            //if (strDomain.Trim() != "")
            //{
            //    //域访问名
            //    string strUserName = ConfigurationSettings.AppSettings["UserName"].ToString();
            //    //域访问密码
            //    string strPassWord = ConfigurationSettings.AppSettings["PassWord"].ToString();
            //    //代理地址
            //    string strHost = ConfigurationSettings.AppSettings["Host"].ToString();
            //    //代理端口
            //    int strPort = Convert.ToInt32(ConfigurationSettings.AppSettings["Port"].ToString());
            //    //设置代理
            //    System.Net.WebProxy oWebProxy = new System.Net.WebProxy(strHost, strPort);
            //    // 获取或设置提交给代理服务器进行身份验证的凭据
            //    oWebProxy.Credentials = new System.Net.NetworkCredential(strUserName, strPassWord, strDomain);
            //    objService.Proxy = oWebProxy;
            //}
            subscriptionService = new cn.tripolis.dialogue.subscription.SubscriptionService();
            subscriptionAuthInfo = new cn.tripolis.dialogue.subscription.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            subscriptionService.authInfo = subscriptionAuthInfo;
            subscriptionService.Proxy = oWebProxy;

            exportService = new cn.tripolis.dialogue.export.ExportService();
            exportAuthInfo = new cn.tripolis.dialogue.export.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            exportService.authInfo = exportAuthInfo;
            exportService.Proxy = oWebProxy;

            importService = new cn.tripolis.dialogue.import.ImportService();
            importAuthInfo = new cn.tripolis.dialogue.import.AuthInfo
            {
                client = client,
                username = userName,
                password = password
            };
            importService.authInfo = importAuthInfo;
            importService.Proxy = oWebProxy;
        }
       
        public String updateDirectEmail(String directEmailId, String subject, String fromName, String htmlSource)
        {
            DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
            return directEmailAction.updateDirectEmail(directEmailId, subject, fromName, htmlSource);
        }


       /// <summary>
        /// create direct email
        /// </summary>
        /// <param name="directEmailId">direct email type id</param>
        /// <param name="emailLabel">email label</param>
        /// <param name="emailName">email name</param>
        /// <param name="subject">subject</param>
        /// <param name="description">decription</param>
        /// <param name="fromName">sender name</param>
        /// <param name="fromAddress">from address</param>
        /// <param name="htmlSource">html source</param>
        /// <returns>direct email id</returns>
        public String createDirectEmail(String directEmailTypeId, String emailLabel, String emailName, String subject, String description, String fromName, String fromAddress, String htmlSource)
        {
            DirectEmailAction directEmailAction = new DirectEmailAction(client, userName, password, oWebProxy);
            return directEmailAction.createDirectEmail(directEmailTypeId, emailLabel, emailName, subject, description, fromName, fromAddress, htmlSource);

        }

         /// <summary>
        /// publishing email
        /// </summary>
        /// <param name="contactGroupId">contact group id</param>
        /// <param name="emailId">email id</param>
        /// <returns>publish status</returns>
        public String publishingEmail(String contactGroupId, String emailId)
        {
            PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
           
            return publishingAction.publishingEmail(contactGroupId, emailId,DateTime.Now);

        }
       /// <summary>
       /// send Single mail
       /// </summary>
       /// <param name="contactStr"></param>
       /// <param name="batchNo"></param>
       /// <param name="sender"></param>
       /// <param name="emailSubject"></param>
       /// <param name="emailBody"></param>
       /// <returns></returns>
        public String sendSingleEmail(TripolisConfig tripolisConfig, string contactStr, String batchNo, String sender, String emailSubject, String emailBody)
        {
            logger.Debug("************send Single Mail***************");
            String result = "OK";
            try
            {

                String contactDatabaseId = tripolisConfig.contactDatabaseId;
                String workspaceId = tripolisConfig.workspaceId;
                String directEmailId = tripolisConfig.directEmailId;
                String emailId = "";
                String EmailFileName = tripolisConfig.EmailFileName;

                

                //const string pattern = "\\{.+?\\}";
                //List<string> tempList = Regex.Matches(emailBody, pattern).Cast<Match>().Select(a => a.Value).ToList();
                //foreach (string str in tempList)
                //{
                //    emailBody = emailBody.Replace(str, str.ToLower());
                //}

                //查找<A的html标记，如果里面没有title属性，则增加这个属性.
                emailBody = Util.addTitleInAFlagHtml(emailBody);
                
                cn.tripolis.dialogue.subscription.subscribeContactRequest request = new cn.tripolis.dialogue.subscription.subscribeContactRequest();
                request.contactDatabase = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.contactDatabase.id = contactDatabaseId;
                request.workspace = new cn.tripolis.dialogue.subscription.IdNameModel();
                request.workspace.id = workspaceId;

                String[] arrContact = contactStr.Split(';');

                request.contactFields = new cn.tripolis.dialogue.subscription.ContactFieldValue[arrContact.Length];
                cn.tripolis.dialogue.subscription.ContactFieldValue contactFieldValue;
                int i = 0;
                foreach (String tempContact in arrContact)
                {
                    String[] arryFiled = tempContact.Split(new[] { "#" }, StringSplitOptions.None);

                    contactFieldValue = new cn.tripolis.dialogue.subscription.ContactFieldValue();
                    contactFieldValue.name = arryFiled[0].ToLower();
                    contactFieldValue.value = arryFiled[1];
                    if (EmailFileName.Equals(contactFieldValue.name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        emailId = contactFieldValue.value;
                    }
                    request.contactFields.SetValue(contactFieldValue, i++);

                }

                //request.directEmail = new cn.tripolis.dialogue.subscription.IdNameModel();
                //request.directEmail.id = directEmailId;

                request.ip = "127.0.0.1";
             

                //request.contactGroupSubscriptions = new cn.tripolis.dialogue.subscription.ContactGroupSubscription[1];
                //cn.tripolis.dialogue.subscription.ContactGroupSubscription contactGroupSubscription = new cn.tripolis.dialogue.subscription.ContactGroupSubscription();
                //contactGroupSubscription.confirmed = true;
                //contactGroupSubscription.contactGroup = new cn.tripolis.dialogue.subscription.IdNameModel();
                //contactGroupSubscription.contactGroup.id = contactGroupId;
                //request.contactGroupSubscriptions.SetValue(contactGroupSubscription, 0);

                cn.tripolis.dialogue.subscription.IDResponse response = subscriptionService.subscribeContact(request);
               // subscriptionService.Proxy 
                String contactId = response.id;

                this.updateDirectEmail(directEmailId, emailSubject, sender, emailBody);

                PublishingAction publishingAction = new PublishingAction(client, userName, password, oWebProxy);
                String publishId = publishingAction.publishTransactionalEmail(contactId, directEmailId);
                

              //  SendLogDao dbAccess = new SendLogDao();
              //  dbAccess.SengLog_InsertInfo(new String[] { emailId, publishId, batchNo });

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                logger.Debug("error happen in send mail, error is " + result);
                // return ex.Message;

            }
            logger.Debug("************end sendMail ************");
            return result;
        }



        public DataTable Report_GetInfo(String batchNo, int type)//从数据库中读取数据到da 再在内存中建立ds 用fill 来把da的数据填充到ds再返回ds 中的首行 
        {
           return new ReportDao().Report_GetInfo(batchNo, type);
        }
        /// <summary>
        /// Sync feedback information to client
        /// </summary>
        /// <param name="contactDatabaseId">contact DatabaseId</param>
        /// <param name="startTime">StartTime</param>
        /// <param name="endTime">End time</param>        
        /// <returns>all of the information, including sent, bounced, 
        /// clicked and opened</returns>
        public String SyncFeedbackInfo(String contactDatabaseId, DateTime startTime, DateTime endTime)
        {
            String result =  getFeedbackInfo(contactDatabaseId, startTime, endTime, null);
            return result;
        }

        /// <summary>
        /// Get feedback information
        /// </summary>
        /// <param name="customerName">contact DatabaseId</param>
        /// <param name="startTime">StartTime</param>
        /// <param name="endTime">End time</param>   
        /// <param name="searchConditions">search Conditions</param>
        /// <returns>all of the information, including sent, bounced, 
        /// clicked and opened</returns>
        public String getFeedbackInfo(String contactDatabaseId, DateTime startTime, DateTime endTime, Hashtable searchConditions)
        {
            XDocument xml = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
            DateTime tempStartTime = endTime.AddDays(-1);
            int i = 1;
            while (endTime > startTime)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("queryFeedbackInfo at the " + i + " times");
                    i++;
                }
                if (tempStartTime < startTime)
                {
                    tempStartTime = startTime;
                }
                xml = Util.mergeXml(xml, this.queryFeedbackInfo(contactDatabaseId, tempStartTime, endTime, searchConditions));

                //if (tempStartTime < startTime)
                //{
                //    break;
                //}
                endTime = tempStartTime;
                tempStartTime = endTime.AddDays(-1);
            }
            if (logger.IsDebugEnabled)
            {
                logger.Debug("Feedback Information \n" + xml);
            }
            return xml.ToString();
        }

        /// <summary>
        /// Get feedback information
        /// </summary>
        /// <param name="contactDatabaseId">contactDatabase Id</param>
        /// <param name="startTime">StartTime</param>
        /// <param name="endTime">End time</param>
        /// <param name="searchConditions">searchConditions</param>
        /// <returns>all of the information, including sent, bounced, 
        /// clicked and opened</returns>
        private XDocument queryFeedbackInfo(String contactDatabaseId, DateTime startTime, DateTime endTime, Hashtable searchConditions)
        {
            //String BouncedFormat = "email"; "name"; "Email address of bounced message"; "Bounce date"; "Bounce code"; "Bounce decription"; "Hard bounce"; "Job ID";
            //String OpenedFormat  = "email"; "name"; "Rendered"; "Opened"; "IP address"; "Browser"; "OS"; "Job ID";
            //String ClickedFormat = "email"; "name"; "clicked";  "linkid"; "IP address"; "browser";       "Job ID"
            if (logger.IsDebugEnabled)
            {
                logger.Debug("queryFeedbackInfo:contactDatabaseId=" + contactDatabaseId + ",startTime=" + startTime.ToLongTimeString() + ",endTime=" + endTime.ToLongTimeString());
            }
            String result;
            TripolisDialogueAdapter.DAO.FeedbackBO feedbackBO = null;
            FeedbackDao feedbackDao = new FeedbackDao();

            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
            try
            {
                //Action.ContactDatabaseAction contactDatabaseAction = new ContactDatabaseAction(client, userName, password);
                // String contactDatabaseId = contactDatabaseAction.getDatabaseIdByName(customerName);

                ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password, oWebProxy);
                Hashtable ht = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);
                //String toEmailFieldId = contactDatabaseFieldAction.getDatabaseEmailId(contactDatabaseId);
                cn.tripolis.dialogue.export.ContactExportRequest request = new cn.tripolis.dialogue.export.ContactExportRequest
                    {
                        contactDatabaseId = contactDatabaseId,
                        timeRange = new cn.tripolis.dialogue.export.TimeRange { startTime = startTime, endTime = endTime }
                    };

                var xroot = new XElement("FeedbackReport");

                logger.Debug("******exportSent*****");
                request.returnContactFields = new cn.tripolis.dialogue.export.ReturnContactFields
                    {
                        contactDatabaseFieldIds = new String[ht.Keys.Count]
                    };
                int index = 0;
                foreach (String value in ht.Values)
                {
                    request.returnContactFields.contactDatabaseFieldIds.SetValue(value, index++);
                }
                RawDataResponse response = exportService.exportSent(request);
                result = System.Text.Encoding.UTF8.GetString(response.data);
                XElement element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportSent", searchConditions);
                xroot.Add(element);

                System.Data.DataSet ds = Util.CXmlToDataSet(element.ToString().ToLower());
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        //[jobId],[email],[opentime],[ipAddress],[browse],[os],[rendered]
                        feedbackBO = new DAO.FeedbackBO();
                        feedbackBO.jobId = row["jobid"].ToString();
                        feedbackBO.email = row["email"].ToString();
                        //feedbackBO.opentime = row["opentime"];
                        //feedbackBO.ipAddress = row["ipAddress"];
                        //feedbackBO.browse = row["browse"];
                        //feedbackBO.os = row["os"];
                        //feedbackBO.rendered = row["rendered"];
                        //feedbackDao.Feedback_GetInfo();
                        feedbackDao.Feedback_updateInfoForSent(feedbackBO);
                    }

                }

                request.returnContactFields.contactDatabaseFieldIds = null;
                request.returnContactFields = new cn.tripolis.dialogue.export.ReturnContactFields
                    {
                        returnAllContactFields = true,
                        returnAllContactFieldsSpecified = true
                    };

                logger.Debug("******exportBounced*****");
                response = exportService.exportBounced(request);
                result = System.Text.Encoding.UTF8.GetString(response.data);
                element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportBounced", searchConditions);
                xroot.Add(element);


               ds = Util.CXmlToDataSet(element.ToString().ToLower());
               if (ds != null && ds.Tables.Count > 0)
               {
                   foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                   {
                       //  [jobId],[email],[bouncedate],[bouncecode],[bounceDecription],[hardbounce]
                       feedbackBO = new DAO.FeedbackBO();
                       feedbackBO.jobId = row["jobid"].ToString();
                       feedbackBO.email = row["email"].ToString();
                       feedbackBO.bouncedate = row["bouncedate"].ToString();
                       feedbackBO.bouncecode = row["bouncecode"].ToString();
                       feedbackBO.bounceDecription = row["bouncedescription"].ToString();
                       feedbackBO.hardbounce = row["hardbounce"].ToString();
                       feedbackDao.Feedback_updateInfoForBounced(feedbackBO);
                   }
               }
                logger.Debug("******exportOpened*****");
                response = exportService.exportOpened(request);
                result = System.Text.Encoding.UTF8.GetString(response.data);
                element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportOpened", searchConditions);
                xroot.Add(element);

                 ds = Util.CXmlToDataSet(element.ToString().ToLower());
                 if (ds != null && ds.Tables.Count > 0)
                 {
                     foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                     {
                         //[jobId],[email],[opentime],[ipAddress],[browse],[os],[rendered]
                         feedbackBO = new DAO.FeedbackBO();
                         feedbackBO.jobId = row["jobid"].ToString();
                         feedbackBO.email = row["email"].ToString();
                         feedbackBO.opentime = row["opened"].ToString();
                         feedbackBO.ipAddress = row["ipAddress"].ToString();
                         feedbackBO.browse = row["browser"].ToString();
                         feedbackBO.os = row["os"].ToString();
                         feedbackBO.rendered = row["rendered"].ToString();
                         feedbackDao.Feedback_updateInfoForOpened(feedbackBO);
                     }
                 }


                logger.Debug("******exportClicked*****");
                response = exportService.exportClicked(request);
                result = System.Text.Encoding.UTF8.GetString(response.data);
                element = Util.convertCsvToXmlElement(result.Replace("\"", ""), new[] { ";" }, "exportClicked", searchConditions);
                xroot.Add(element);

                  ds = Util.CXmlToDataSet(element.ToString().ToLower());
                 if (ds != null && ds.Tables.Count > 0)
                 {
                     foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                     {
                         //[jobId],[email],[linkid],[ipAddress],[browse],[clicked]
                         feedbackBO = new DAO.FeedbackBO();
                         feedbackBO.jobId = row["jobid"].ToString();
                         feedbackBO.email = row["email"].ToString();
                         feedbackBO.linkid = row["linkid"].ToString();
                         feedbackBO.ipAddress = row["ipAddress"].ToString();
                         feedbackBO.browse = row["browser"].ToString();
                         feedbackBO.clicked = row["clicked"].ToString();
                         feedbackDao.Feedback_updateInfoForClicked(feedbackBO);
                     }
                 }

                doc.Add(xroot);
                result = doc.ToString();
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                result = ex.Detail.InnerXml;
                throw new Exception(result);
            }

            catch (Exception ex)
            {
                result = ex.Message;
                throw new Exception(result);
            }
            if (logger.IsDebugEnabled)
            {
                logger.Debug(result);
            }
            return doc;
        }


        /// <summary>
        /// Create contact group
        /// </summary>
        /// <param name="contactDatabaseId">contact database id</param>
        /// <param name="groupLable">contact group lable</param>
        /// <param name="groupName">contact group name</param>
        /// <returns>contact group id</returns>
        public String createContactGroup(String contactDatabaseId, String groupLable, String groupName)
        {
            ContactGroupAction contactGroupAction = new ContactGroupAction(client, userName, password, oWebProxy);

            return  contactGroupAction.createContactGroup(contactDatabaseId, groupLable, groupName);
        }
        /// <summary>
        /// Import contact to database
        /// </summary>
        /// <param name="contactDatabaseId">contact database id</param>
        /// <param name="contactGroupId">contact group id</param>
        /// <param name="mailData">mail data</param>
        public String importContact(String contactDatabaseId, String contactGroupId, String reportReceiverAddress,String fileName,byte[] contacts)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("importContact:contactDatabaseId=" + contactDatabaseId);
            }

            String result = "" ;
            ContactDatabaseFieldAction contactDatabaseFieldAction = new ContactDatabaseFieldAction(client, userName, password, oWebProxy);
            Hashtable contactFieldTable = contactDatabaseFieldAction.getContactDatabaseFields(contactDatabaseId);

            cn.tripolis.dialogue.import.ImportContactsRequest request = new cn.tripolis.dialogue.import.ImportContactsRequest();
            try
            {

                importService.Timeout = System.Threading.Timeout.Infinite;
                request.reportReceiverAddress = reportReceiverAddress;
                request.importMode = cn.tripolis.dialogue.import.importMode.SYNCHRONIZE_GROUP;
                request.contactGroupIds = new[] { contactGroupId };
                request.extension = cn.tripolis.dialogue.import.fileExtension.CSV;
                request.fileName = fileName;
                request.importFile = contacts;//System.IO.File.ReadAllBytes("../../Contacts_new.csv");
                //importService..importContactsAsync()
                cn.tripolis.dialogue.import.ImportIDResponse response = importService.importContacts(request);
                
                result = response.importId;

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in get contact database fields, error is" + ex.Detail.InnerXml);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return result;
        }


    }
}