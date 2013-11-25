using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripolistMailAdapter;
using TripolisDialogueAdapter.cn.tripolis.dialogue.import;

namespace TripolisDialogueAdapter.Action
{
    class ImportContactAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ImportContactAction));
        private cn.tripolis.dialogue.import.ImportService importService = null;
        private cn.tripolis.dialogue.import.AuthInfo importAuthInfo = null;
        private const String OK_RESULT = MailAdapter.OK_RESULT;
        private string userName;
        private string password;
        private string client;
         /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public ImportContactAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
        {
            this.client = client;
            this.userName = userName;
            this.password = password;
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

       
        /// <summary>
        /// FTP contact file
        /// </summary>
        /// <param name="contactDatabaseId"> contactDatabaseId</param>
        /// <param name="groupId">groupId</param>
        /// <param name="fileName">fileName</param>
        /// <param name="reportReceiverAddress">reportReceiverAddress</param>
        /// <param name="ftpAccountId">ftpAccountId</param>
        /// <param name="scheduleAt">scheduleAt</param>
        /// <returns>message</returns>
        public String importContactFromFTP(String contactDatabaseId, String groupId, String fileName, String reportReceiverAddress, String ftpAccountId, DateTime scheduleAt)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("importContactFromFTP:contactDatabaseId=" + contactDatabaseId);
            }
            String result;
            cn.tripolis.dialogue.import.ImportContactsFromFtpRequest request = new cn.tripolis.dialogue.import.ImportContactsFromFtpRequest();
            try
            {

                request.contactDatabaseId = contactDatabaseId;
                string[] contactGroupIds = new string[1];
                contactGroupIds[0] = groupId;
                request.contactGroupIds = contactGroupIds;
                request.extension = cn.tripolis.dialogue.import.fileExtension.CSV;
                request.fileName = fileName;
                request.ftpAccountId = ftpAccountId;
                request.importMode = cn.tripolis.dialogue.import.importMode.ADD_TO_GROUP;
                request.reportReceiverAddress = reportReceiverAddress;
                request.scheduleAt= scheduleAt;

                cn.tripolis.dialogue.import.ImportIDResponse response = importService.importContactsFromFtp(request);

                result = response.importId;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in ftp contact, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);

            }
            return result;
        }

        public importStatus getImportStatus(String importId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getImportStatus:importId=" + importId);
            }
            importStatus result;
            cn.tripolis.dialogue.import.ImportIDRequest request = new cn.tripolis.dialogue.import.ImportIDRequest();
            try
            {
                request.importId = importId;
                cn.tripolis.dialogue.import.ImportStatusResponse response = importService.getImportStatus(request);
                result = response.importStatus;
              //  cn.tripolis.dialogue.import.importStatus.ENDED 

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                   // result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in ftp contact, error is" + ex.Detail.InnerXml);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = cn.tripolis.dialogue.import.importStatus.TRANSFERING;

            }
            return result;
        } 
    }
}
