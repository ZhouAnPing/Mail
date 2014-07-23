using System;
using TripolistMailAdapter.cn.tripolis.dialogue.contactGroup;


namespace TripolistMailAdapter.Action
{
    class ContactGroupAction
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ContactGroupAction));
        private cn.tripolis.dialogue.contactGroup.ContactGroupService contactGroupService = null;
        private cn.tripolis.dialogue.contactGroup.AuthInfo contactGroupAuthInfo = null;
/*
        private const String OK_RESULT = MailAdapter.OK_RESULT;
*/

        /// <summary>
        /// Initial the webservice API.
       /// </summary>
       /// <param name="client">client</param>
       /// <param name="userName">user name</param>
       /// <param name="password">password</param>
        public ContactGroupAction(String client, String userName, String password)
        {

            contactGroupService = new cn.tripolis.dialogue.contactGroup.ContactGroupService();
            contactGroupAuthInfo = new cn.tripolis.dialogue.contactGroup.AuthInfo
                {
                    client = client,
                    username = userName,
                    password = password
                };
            contactGroupService.authInfo = contactGroupAuthInfo;
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
            if (logger.IsDebugEnabled)
            {
                logger.Debug("createContactGroup: contactDatabaseId=" + contactDatabaseId + ",groupLable=" + groupLable + ",groupName=" + groupName);
            }
            String result;
            cn.tripolis.dialogue.contactGroup.CreateContactGroupRequest request = new cn.tripolis.dialogue.contactGroup.CreateContactGroupRequest
                {
                    contactDatabaseId = contactDatabaseId,
                    label = groupLable,
                    name = groupName,
                    groupType = cn.tripolis.dialogue.contactGroup.groupType.STATIC
                };
            try
            {
                cn.tripolis.dialogue.contactGroup.IDResponse response = contactGroupService.create(request);
                result = response.id;
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("create new contact group, group Id=" + result);
                }
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (!Util.isCodeExist(ex.Detail) || Util.getExistId(ex.Detail).Equals(""))
                {
                    result = ex.Detail.InnerXml;
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("error happens in create contact group, error is" + result);
                    }
                    throw new Exception(ex.Detail.InnerXml);
                }
                result = Util.getExistId(ex.Detail);
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("contact group is existed, group Id=" + result);
                }
            }
            return result;
        }
        /// <summary>
        /// get contact group by database id 
        /// </summary>
        /// <param name="contactDatabaseId">contact Database Id</param>
        /// <returns></returns>
        public ContactGroup[] getContactGroup(String contactDatabaseId)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug("getContactGroup: contactDatabaseId=" + contactDatabaseId);
            }

            cn.tripolis.dialogue.contactGroup.ContactGroupsByContactDatabaseIdRequest request = new cn.tripolis.dialogue.contactGroup.ContactGroupsByContactDatabaseIdRequest
            {                
                contactDatabaseId = contactDatabaseId,
            };

            cn.tripolis.dialogue.contactGroup.ContactGroupListResponse response = contactGroupService.getByContactDatabaseId(request);
            if (response != null)
            {

                return response.contactGroups;
            }
            return null;
        }
    }
}
