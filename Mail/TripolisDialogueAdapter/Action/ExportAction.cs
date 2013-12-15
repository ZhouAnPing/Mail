using System;
using TripolistMailAdapter;
using TripolisDialogueAdapter.cn.tripolis.dialogue.export;
using TripolisDialogueAdapter.BO;

namespace TripolisDialogueAdapter.Action
{
   public class ExportAction
    {
       private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ExportAction));
       private cn.tripolis.dialogue.export.ExportService exportService = null;
       private cn.tripolis.dialogue.export.AuthInfo exportAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public ExportAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
       {
           exportService = new cn.tripolis.dialogue.export.ExportService();
           exportAuthInfo = new cn.tripolis.dialogue.export.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
           exportService.authInfo = exportAuthInfo;
           exportService.Proxy = oWebProxy;
        }



        /// <summary>
        /// Get Mail reports
        /// </summary>
        /// <param name="mailJobId"></param>
        /// <param name="timeRange"></param>
        /// <returns></returns>
        public String ExportReport(String contactDatabaseId, DateTime startTime, DateTime endTime, ReportType reportType)
        {
            String result = "";
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getReport:contactDatabaseId=" + contactDatabaseId);
            }
            try
            {
                cn.tripolis.dialogue.export.ContactExportRequest request = new cn.tripolis.dialogue.export.ContactExportRequest();

                request.contactDatabaseId = contactDatabaseId;
                request.timeRange = new cn.tripolis.dialogue.export.TimeRange();
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;
                request.returnContactFields = new cn.tripolis.dialogue.export.ReturnContactFields();
                request.returnContactFields.contactDatabaseFieldNames = new string[] { "email", "username" };
                RawDataResponse response = null;
                switch (reportType)
                {
                    case ReportType.OPENED:
                        response = exportService.exportOpened(request);
                        break;
                    case ReportType.CLICKED:
                        response = exportService.exportClicked(request);
                        break;
                    case ReportType.BOUNCED:
                        response = exportService.exportBounced(request);
                        break;
                    case ReportType.SENT:
                        response = exportService.exportSent(request);
                        break;
                    case ReportType.SKIPPED:
                        response = exportService.exportSkipped(request);
                        break;
                    case ReportType.CONVERTED:
                        response = exportService.exportConverted(request);
                        break;
                    case ReportType.LINKED:
                        response = exportService.exportLinks(request);
                        break;
                    case ReportType.JOBS:
                        response = exportService.exportJobs(request);
                        break;
                    default:
                        break;
                }
                if (response != null)
                {
                    result = System.Text.Encoding.UTF8.GetString(response.data);
                    Console.WriteLine(result);
                }


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {

                throw new Exception(ex.Detail.InnerXml);

            }
            return result;
        }

        public String exportReportToFtp(String contactDatabaseId, String ftpAccountId, DateTime startTime, DateTime endTime, ReportType reportType)
        {
            String result = "";
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getReport:contactDatabaseId=" + contactDatabaseId);
            }
            try
            {
                cn.tripolis.dialogue.export.FtpContactExportRequest request = new cn.tripolis.dialogue.export.FtpContactExportRequest();

                request.contactDatabaseId = contactDatabaseId;
                request.timeRange = new cn.tripolis.dialogue.export.TimeRange();
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;
                request.fileName = "MailReport.csv";
                request.ftpAccountId = ftpAccountId;// "NTM5NTM5NTNW_uqPXJDMzQ";
                request.sendNotificationMail = false;

                request.returnContactFields = new cn.tripolis.dialogue.export.ReturnContactFields();
                request.returnContactFields.contactDatabaseFieldNames = new string[] { "email", "username" };
                IDResponse response = null;
                switch (reportType)
                {
                    case ReportType.OPENED:
                        response = exportService.exportBouncedToFtp(request);
                        break;
                    case ReportType.CLICKED:
                        response = exportService.exportClickedToFtp(request);
                        break;
                    case ReportType.BOUNCED:
                        response = exportService.exportBouncedToFtp(request);
                        break;
                    case ReportType.SENT:
                        response = exportService.exportSentToFtp(request);
                        break;
                    case ReportType.SKIPPED:
                        response = exportService.exportSkippedToFtp(request);
                        break;
                    //case ReportType.CONVERTED:
                    //    response = exportService.exportConverted(request);
                    //    break;
                    //case ReportType.LINKED:
                    //    response = exportService.exportLinksToFtp(request);
                    //    break;
                    //case ReportType.JOBS:
                    //    response = exportService.exportJobsToFtp(request);
                    //    break;
                    default:
                        break;
                }
                if (response != null)
                {
                    result = response.id;
                }


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {

                throw new Exception(ex.Detail.InnerXml);

            }
            return result;
        }
       
    }
}
