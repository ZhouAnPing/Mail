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
      /// get mail Report for sending
      /// </summary>
      /// <param name="mailJobId"></param>
      /// <param name="startTime"></param>
      /// <param name="endTime"></param>
      /// <param name="pageSize"></param>
      /// <param name="pageNumber"></param>
      /// <returns></returns>
        public Contact[] getDeliveredByMailJobId(String mailJobId, DateTime startTime, DateTime endTime, int pageSize, int pageNumber)
        {
            Contact[] contacts = null; ;
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getDeliveredByMailJobId:mailJobId=" + mailJobId);
            }          
          

            ReportingWithinTimeRangeByMailJobIdRequest request = new ReportingWithinTimeRangeByMailJobIdRequest();
            try
            {
                request.mailJobId = mailJobId;     
           
                request.returnContactFields = new ReturnContactFields();             
                request.returnContactFields.contactDatabaseFieldGroupNames = new string[] { "reportgroup" };              
                request.timeRange = new cn.tripolis.dialogue.reporting.TimeRange();
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;

                request.paging = new cn.tripolis.dialogue.reporting.PagingIn();
                request.paging.pageSize = pageSize;
                request.paging.pageNr = pageNumber;               

                ContactListResponse response = reportingService.getDeliveredByMailJobId(request);
                if (response != null)
                {
                    contacts = response.contacts;
                }
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
               
                    throw new Exception(ex.Detail.InnerXml);
                
            }
             return contacts;
        }

        /// <summary>
        /// get mail Report for opening
        /// </summary>
        /// <param name="mailJobId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public Open[] getOpenedByMailJobId(String mailJobId, DateTime startTime, DateTime endTime, int pageSize, int pageNumber)
        {
            Open[] opens = null; ;
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getOpenedByMailJobId:mailJobId=" + mailJobId);
            }


            ReportingWithinTimeRangeByMailJobIdRequest request = new ReportingWithinTimeRangeByMailJobIdRequest();
            try
            {
                request.mailJobId = mailJobId;

                request.returnContactFields = new ReturnContactFields();
                request.returnContactFields.contactDatabaseFieldGroupNames = new string[] { "reportgroup" };
                request.timeRange = new cn.tripolis.dialogue.reporting.TimeRange();
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;

                request.paging = new cn.tripolis.dialogue.reporting.PagingIn();
                request.paging.pageSize = pageSize;
                request.paging.pageNr = pageNumber;

                OpensListResponse response = reportingService.getOpenedByMailJobId(request);
                if (response != null)
                {
                    opens = response.opens;
                }
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                throw new Exception(ex.Detail.InnerXml);
            }
            return opens;
        }

        /// <summary>
        /// get mail Report for Clicking
        /// </summary>
        /// <param name="mailJobId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public Click[] getClickedByMailJobId(String mailJobId, DateTime startTime, DateTime endTime, int pageSize, int pageNumber)
        {
            Click[] clicks = null; ;
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getClickedByMailJobId:mailJobId=" + mailJobId);
            }


            ReportingWithinTimeRangeByMailJobIdRequest request = new ReportingWithinTimeRangeByMailJobIdRequest();
            try
            {
                request.mailJobId = mailJobId;

                request.returnContactFields = new ReturnContactFields();
                request.returnContactFields.contactDatabaseFieldGroupNames = new string[] { "reportgroup" };
                request.timeRange = new cn.tripolis.dialogue.reporting.TimeRange();
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;

                request.paging = new cn.tripolis.dialogue.reporting.PagingIn();
                request.paging.pageSize = pageSize;
                request.paging.pageNr = pageNumber;

                ClicksListResponse response = reportingService.getClickedByMailJobId(request);

              
                if (response != null)
                {
                    clicks = response.clicks;
                }
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                throw new Exception(ex.Detail.InnerXml);
            }
            return clicks;
        }


        /// <summary>
        /// get mail Report for Bounced
        /// </summary>
        /// <param name="mailJobId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public BouncedContact[] getBouncedByMailJobId(String mailJobId, DateTime startTime, DateTime endTime, int pageSize, int pageNumber)
        {
            BouncedContact[] bouncedContacts = null; ;
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getBouncedByMailJobId:mailJobId=" + mailJobId);
            }


            ReportingWithinTimeRangeByMailJobIdRequest request = new ReportingWithinTimeRangeByMailJobIdRequest();
            try
            {
                request.mailJobId = mailJobId;

                request.returnContactFields = new ReturnContactFields();
                request.returnContactFields.contactDatabaseFieldGroupNames = new string[] { "reportgroup" };
                request.timeRange = new cn.tripolis.dialogue.reporting.TimeRange();
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;

                request.paging = new cn.tripolis.dialogue.reporting.PagingIn();
                request.paging.pageSize = pageSize;
                request.paging.pageNr = pageNumber;
                
                BouncedContactListResponse response = reportingService.getBouncedByMailJobId(request);
                if (response != null)
                {
                    bouncedContacts = response.bouncedContacts;
                }               
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                throw new Exception(ex.Detail.InnerXml);
            }
            return bouncedContacts;
        }

        /// <summary>
        /// get mail Report for Skipped
        /// </summary>
        /// <param name="mailJobId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public SkippedContactModel[] getSkippedByMailJobId(String mailJobId, DateTime startTime, DateTime endTime, int pageSize, int pageNumber)
        {
            SkippedContactModel[] skippedContacts = null; ;
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getSkippedByMailJobId:mailJobId=" + mailJobId);
            }


            ReportingWithinTimeRangeByMailJobIdRequest request = new ReportingWithinTimeRangeByMailJobIdRequest();
            try
            {
                request.mailJobId = mailJobId;

                request.returnContactFields = new ReturnContactFields();
                request.returnContactFields.contactDatabaseFieldGroupNames = new string[] { "reportgroup" };
                request.timeRange = new cn.tripolis.dialogue.reporting.TimeRange();
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;

                request.paging = new cn.tripolis.dialogue.reporting.PagingIn();
                request.paging.pageSize = pageSize;
                request.paging.pageNr = pageNumber;

                SkippedContactListResponse response = reportingService.getSkippedByMailJobId(request);
                if (response != null)
                {
                    skippedContacts = response.skippedContacts;
                }
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                throw new Exception(ex.Detail.InnerXml);
            }
            return skippedContacts;
        }

        /// <summary>
        /// get mail Report for Delivery
        /// </summary>
        /// <param name="mailJobId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public Contact[] getDeliveriedByMailJobId(String mailJobId, DateTime startTime, DateTime endTime, int pageSize, int pageNumber)
        {
            Contact[] contacts = null; ;
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getDeliveriedByMailJobId:mailJobId=" + mailJobId);
            }


            ReportingWithinTimeRangeByMailJobIdRequest request = new ReportingWithinTimeRangeByMailJobIdRequest();
            try
            {
                request.mailJobId = mailJobId;

                request.returnContactFields = new ReturnContactFields();
                request.returnContactFields.contactDatabaseFieldGroupNames = new string[] { "reportgroup" };
                request.timeRange = new cn.tripolis.dialogue.reporting.TimeRange();
                request.timeRange.startTime = startTime;
                request.timeRange.endTime = endTime;

                request.paging = new cn.tripolis.dialogue.reporting.PagingIn();
                request.paging.pageSize = pageSize;
                request.paging.pageNr = pageNumber;

                ContactListResponse response = reportingService.getDeliveredByMailJobId(request);
                if (response != null)
                {
                    contacts = response.contacts;
                }
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                throw new Exception(ex.Detail.InnerXml);
            }
            return contacts;
        }

       /// <summary>
       /// get email summary.
       /// </summary>
       /// <param name="mailJobId"></param>
       /// <returns></returns>
        public EmailSummary getEmailSummary(String mailJobId)
        {
            cn.tripolis.dialogue.reporting.EmailSummary result = null;
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getEmailSummary:mailJobId=" + mailJobId);
            }
            MailJobIDRequest request = new MailJobIDRequest();
            try
            {
                request.mailJobId = mailJobId;
                EmailSummaryResponse response = reportingService.getEmailSummary(request);
                if (response != null)
                {
                    result = response.emailSummary;
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
