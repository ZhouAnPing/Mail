using System;
using TripolistMailAdapter;
using TripolisDialogueAdapter.cn.tripolis.dialogue.publish;
using TripolisDialogueAdapter.cn.tripolis.dialogue.reporting;

namespace TripolisDialogueAdapter.Action
{
   public class ReportingAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ReportingAction));
        private cn.tripolis.dialogue.reporting.ReportingService reportingService = null;
        private cn.tripolis.dialogue.reporting.AuthInfo reportingAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public ReportingAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
       {
           reportingService = new cn.tripolis.dialogue.reporting.ReportingService();
           reportingAuthInfo = new cn.tripolis.dialogue.reporting.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
           reportingService.authInfo = reportingAuthInfo;
           reportingService.Proxy = oWebProxy;
        }



        /// <summary>
        /// Get Mail reports
        /// </summary>
        /// <param name="mailJobId"></param>
        /// <param name="timeRange"></param>
        /// <returns></returns>
        public void getReport(String mailJobId, TripolisDialogueAdapter.cn.tripolis.dialogue.reporting.TimeRange timeRange)
        {
           
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getReport:mailJobId=" + mailJobId );
            }
            Open[] Opens;
            Contact[] Contacts;

            cn.tripolis.dialogue.reporting.ReportingWithinTimeRangeByMailJobIdRequest request = new cn.tripolis.dialogue.reporting.ReportingWithinTimeRangeByMailJobIdRequest();
            try
            {
                request.mailJobId = mailJobId;

                request.returnContactFields = new ReturnContactFields();
               // request.returnContactFields.returnAllContactFields = true;
                request.returnContactFields.contactDatabaseFieldNames = new string[] { "email", "username" };
                //request.returnContactFields.contactDatabaseFieldNames = new String[1];
               // request.returnContactFields.contactDatabaseFieldNames.SetValue("email", 0);               

                request.timeRange = new cn.tripolis.dialogue.reporting.TimeRange();
                request.timeRange = timeRange;

                request.paging = new cn.tripolis.dialogue.reporting.PagingIn();
                request.paging.pageSize = 1000;
                request.paging.pageNr = 1;

                

                cn.tripolis.dialogue.reporting.ContactListResponse response1 = reportingService.getDeliveredByMailJobId(request);

                Contacts = response1.contacts; ;
                         
               
                cn.tripolis.dialogue.reporting.OpensListResponse response2 = reportingService.getOpenedByMailJobId(request);

                Opens = response2.opens;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
               
                    throw new Exception(ex.Detail.InnerXml);
                
            }
          //  return result;
        }

       
    }
}
