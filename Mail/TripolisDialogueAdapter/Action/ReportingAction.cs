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
        /// publishing email
        /// </summary>
        /// <param name="contactGroupId">contact group id</param>
        /// <param name="emailId">email id</param>
        /// <returns>publish status</returns>
        public Open[] getReport(String mailJobId)
        {
            mailJobId = "MTA1Mjc5MTSMdCDprzC_oRpaAAO2LvZr";
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getReport:mailJobId=" + mailJobId );
            }
            Open[] result;

            cn.tripolis.dialogue.reporting.ReportingWithinTimeRangeByMailJobIdRequest request = new cn.tripolis.dialogue.reporting.ReportingWithinTimeRangeByMailJobIdRequest();
            try
            {
                request.mailJobId = mailJobId;



                request.returnContactFields = new ReturnContactFields();
                //request.returnContactFields.returnAllContactFields = true;

                request.returnContactFields.contactDatabaseFieldNames = new String[1];
                request.returnContactFields.contactDatabaseFieldNames.SetValue("email", 0);
               

                request.timeRange = new cn.tripolis.dialogue.reporting.TimeRange();
                DateTime startTime = DateTime.Now.AddDays(-3);
                DateTime endTime = DateTime.Now ;
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;
              //  request.paging.
                // request.              
                cn.tripolis.dialogue.reporting.OpensListResponse response = reportingService.getOpenedByMailJobId(request);

                result = response.opens;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
               
                    throw new Exception(ex.Detail.InnerXml);
                
            }
            return result;
        }

       
    }
}
