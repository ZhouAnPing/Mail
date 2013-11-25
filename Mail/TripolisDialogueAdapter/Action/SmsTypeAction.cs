using System;
using TripolisDialogueAdapter.cn.tripolis.dialogue.smsType;
using TripolistMailAdapter;

namespace TripolisDialogueAdapter.Action
{
    class SmsTypeAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SmsTypeAction));
        private SmsTypeService smsTypeService = null;
        private AuthInfo smsTypeAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public SmsTypeAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
       {
            smsTypeService = new SmsTypeService();
            smsTypeAuthInfo = new AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            
            smsTypeService.authInfo = smsTypeAuthInfo;
            smsTypeService.Proxy = oWebProxy;
        }

        /// <summary>
        /// get email type id by sms type name
        /// </summary>
        /// <param name="workspaceId">workspace id</param>
        /// <param name="smsTypeName">sms type name</param>
        /// <returns>sms type id</returns>
        public String getSmsTypeId(String workspaceId, String smsTypeName)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getSmsTypeId:workspaceId=" + workspaceId + ",smsTypeName=" + smsTypeName);
            }

            String result = "";

            WorkspaceIDRequest request = new cn.tripolis.dialogue.smsType.WorkspaceIDRequest();
            try
            {
                request.workspaceId = workspaceId;
                cn.tripolis.dialogue.smsType.SmsTypeListResponse response = smsTypeService.getByWorkspaceId(request);

                foreach (SmsType item in response.smsTypes)
                {
                    if (item.name.Equals(smsTypeName, StringComparison.OrdinalIgnoreCase))
                    {
                        return item.id;
                    }
                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in get sms type id, error is" + result);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return result;
        }


        /// <summary>
        /// Create direct sms type.
        /// </summary>
        /// <param name="workspaceId">workspaceId</param>
        /// <param name="smsTypeLable">smsType lable</param>
        /// <param name="smsTypeName">sms type name</param>       
        /// <param name="senderName">sender name</param>
        /// <param name="toMobileFieldId">to mobile field id</param>
        /// <returns>direct Sms type id</returns>
        public String createSmsType(String workspaceId, String smsTypeLable, String smsTypeName, String senderName, String toMobileFieldId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createSmsType:workspaceId=" + workspaceId + ",smsTypeLable=" + smsTypeLable + ",smsTypeName=" + smsTypeName + ",senderName=" + senderName + ",toMobileFieldId=" + toMobileFieldId);
            }

            String result;
            String smsTypeId = this.getSmsTypeId(workspaceId, smsTypeName);
            //Check if the email type is already exist.
            if (!smsTypeId.Equals(""))
            {
                return smsTypeId;
            }
            cn.tripolis.dialogue.smsType.CreateSmsTypeRequest request = new cn.tripolis.dialogue.smsType.CreateSmsTypeRequest();
            try
            {
                request.workspaceId = workspaceId;
                request.label = smsTypeLable;
                request.name = smsTypeName;
                request.defaultOriginator = senderName;
                request.toMobileFieldId = toMobileFieldId;//"MTExMzYxMTEfCGWCBaFKyA"
                request.longSmsEnabled = true;
                cn.tripolis.dialogue.smsType.IDResponse response = smsTypeService.create(request);

                result = response.id;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in create direct sms type, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);

            }
            return result;
        }
    }
}
