using System;
using TripolistMailAdapter.cn.tripolis.dialogue.workspace;

namespace TripolistMailAdapter.Action
{
    class WorkspaceAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WorkspaceAction));
        private WorkspaceService workspaceService = null;
        private AuthInfo workspaceAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        public WorkspaceAction(String client, String userName, String password)
        {
            workspaceService = new WorkspaceService();
            workspaceAuthInfo = new AuthInfo {client = client, username = userName, password = password};
            workspaceService.authInfo = workspaceAuthInfo;
        }


        /// <summary>
        /// get WorkSpace by  contactDatabase Id
        /// </summary>
        /// <param name="contactDatabaseId">contactDatabaseId</param>
        /// <returns> all workspace related the contactDatabaseId</returns>
        public Workspace[] getWorkspaceByDatabaseId(String contactDatabaseId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getWorkspaceByDatabaseId:contactDatabaseId=" + contactDatabaseId);
            }


            cn.tripolis.dialogue.workspace.ContactDatabaseIDRequest request = new cn.tripolis.dialogue.workspace.ContactDatabaseIDRequest();
            try
            {
                request.contactDatabaseId = contactDatabaseId;
                cn.tripolis.dialogue.workspace.WorkspaceListResponse response = workspaceService.getByContactDatabaseId(request);
                return response.workspaces;

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in creating workSpace, error is" + ex.Detail.InnerXml);
                }
                throw new Exception(ex.Detail.InnerXml);
            }

        }

        /// <summary>
        /// create work space 
        /// </summary>
        /// <param name="contactDatabaseId">contactDatabaseID </param>
        /// <param name="customerName">customerName </param>
        /// <returns>workspace id</returns>
        public String createWorkspace(String contactDatabaseId, String customerName)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createWorkspace:contactDatabaseID=" + contactDatabaseId + ",customerName=" + customerName);
            }

            Workspace[] allWorkSpace = this.getWorkspaceByDatabaseId(contactDatabaseId);
            if (allWorkSpace != null)
            {
                foreach (Workspace workspace in allWorkSpace)
                {
                    if (!String.IsNullOrEmpty(customerName) && workspace.label.Equals(customerName))
                    {
                        return workspace.id;
                    }
                }
            }

            String result;

            cn.tripolis.dialogue.workspace.CreateWorkspaceRequest request = new CreateWorkspaceRequest();
            try
            {
                request.label = customerName;
                request.name = customerName;
                request.contactDatabaseId = contactDatabaseId;
                cn.tripolis.dialogue.workspace.IDResponse response = workspaceService.create(request);
                result = response.id;

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("error happens in creating contact database, error is" + ex.Detail.InnerXml);
                }
                throw new Exception(ex.Detail.InnerXml);
            }
            return result;

        }
    }
}
