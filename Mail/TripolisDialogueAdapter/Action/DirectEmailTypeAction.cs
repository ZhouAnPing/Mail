using System;
using TripolisDialogueAdapter.cn.tripolis.dialogue.directEmailType;
using TripolistMailAdapter;

namespace TripolisDialogueAdapter.Action
{
    class DirectEmailTypeAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DirectEmailTypeAction));
        private DirectEmailTypeService directEmailTypeService = null;
        private AuthInfo directEmailTypeAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public DirectEmailTypeAction(String client, String userName, String password, System.Net.WebProxy oWebProxy)
       {
            directEmailTypeService = new DirectEmailTypeService();
            directEmailTypeAuthInfo = new AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            directEmailTypeService.authInfo = directEmailTypeAuthInfo;
            directEmailTypeService.Proxy = oWebProxy;
        }

        /// <summary>
        /// get email type id by email type name
        /// </summary>
        /// <param name="workspaceId">workspace id</param>
        /// <param name="emailTypeName">email type name</param>
        /// <returns>email type id</returns>
        public String getDirectEmailTypeId(String workspaceId, String emailTypeName)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getEmailTypeId:workspaceId=" + workspaceId + ",emailTypeName=" + emailTypeName);
            }

            String result = "";

            WorkspaceIDRequest request = new cn.tripolis.dialogue.directEmailType.WorkspaceIDRequest();
            try
            {
                request.workspaceId = workspaceId;
                cn.tripolis.dialogue.directEmailType.DirectEmailTypeListResponse response = directEmailTypeService.getByWorkspaceId(request);

                foreach (DirectEmailType item in response.directEmailTypes)
                {
                    if (item.name.Equals(emailTypeName, StringComparison.OrdinalIgnoreCase))
                    {
                        return item.id;
                    }
                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in get email type id, error is" + result);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return result;
        }


        /// <summary>
        /// Create direct email type.
        /// </summary>
        /// <param name="workspaceId">workspaceId</param>
        /// <param name="emailTypeLable">emailType lable</param>
        /// <param name="emailTypeName">email type name</param>
        /// <param name="fromAddress">from address</param>
        /// <param name="senderName">sender name</param>
        /// <param name="toEmailFieldId">to email field id</param>
        /// <returns>direct email type id</returns>
        public String createDirectEmailType(String workspaceId, String emailTypeLable, String emailTypeName, String fromAddress, String senderName, String toEmailFieldId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createDirectEmailType:workspaceId=" + workspaceId + ",emailTypeLable=" + emailTypeLable + ",emailTypeName=" + emailTypeName + ",fromAddress=" + fromAddress + ",senderName=" + senderName + ",toEmailFieldId=" + toEmailFieldId);
            }

            String result;
            String emailTypeId = this.getDirectEmailTypeId(workspaceId, emailTypeName);
            //Check if the email type is already exist.
            if (!emailTypeId.Equals(""))
            {
                return emailTypeId;
            }
            cn.tripolis.dialogue.directEmailType.CreateDirectEmailTypeRequest request = new cn.tripolis.dialogue.directEmailType.CreateDirectEmailTypeRequest();
            try
            {
                request.workspaceId = workspaceId;
                request.label = emailTypeLable;
                request.name = emailTypeName;
                request.fromName = senderName;
                request.fromAddress = fromAddress;
                request.toEmailFieldId = toEmailFieldId;//"MTExMzYxMTEfCGWCBaFKyA"
                request.enableWysiwygEditor = true;

                request.encoding = cn.tripolis.dialogue.directEmailType.characterEncoding.UTF8;

                cn.tripolis.dialogue.directEmailType.IDResponse response = directEmailTypeService.create(request);

                result = response.id;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in create direct email type, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);

            }
            return result;
        }
    }
}
