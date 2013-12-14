using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TripolisDialogueAdapter;
using TripolisDialogueAdapter.BO;

namespace TripolisDialogueAdapterWs
{
    /// <summary>
    /// Summary description for MailAdapter
    /// </summary>
    [WebService(Namespace = "http://ws.tripolis.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MailAdapter : System.Web.Services.WebService
    {

        [WebMethod]
        /// <summary>
        /// publish Small Scale email, mail quantity >=50,000
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="contactGroup">Contact Group information</param>
        /// <param name="contacts">Contact Information</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <returns></returns>
        public String publishingBulkEmail(Authorization authorization, DialogueSetting dialogueSetting, ContactGroup contactGroup, String fileName, DirectEmail directEmail)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.publishingBulkEmail(dialogueSetting, contactGroup, fileName, directEmail);
        }

        [WebMethod]
        /// <summary>
        /// publish Small Scale email,, mail quantity <50,000
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="contactGroup">Contact Group information</param>
        /// <param name="contacts">Contact Information</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <returns></returns>
        public String publishingSmallScaleEmail(Authorization authorization, DialogueSetting dialogueSetting, ContactGroup contactGroup, ImportFiles importFiles, DirectEmail directEmail)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.publishingSmallScaleEmail(dialogueSetting, contactGroup, importFiles, directEmail);
        }
        [WebMethod]
        /// <summary>
        /// send single email 
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>
        /// <param name="directEmail">parameters related with direct email, such as subject, fromaddress</param>
        /// <param name="ContactInfos">parameters related with contact information, such as email Id.</param>
        /// <returns></returns>
        public String sendSingleEmail(Authorization authorization, DialogueSetting dialogueSetting, DirectEmail directEmail, KeyValuePair[] ContactInfos)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.sendSingleEmail(dialogueSetting, directEmail, ContactInfos);
        }
        [WebMethod]
        /// <summary>
        /// send Register email 
        /// </summary>
        /// <param name="dialogueSetting">Dialogue setting, such as database Id, workspaceId</param>        
        /// <param name="ContactInfos">parameters related with contact information, such as email Id.</param>
        /// <returns></returns>
        public String registerContact(Authorization authorization, DialogueSetting dialogueSetting, KeyValuePair[] ContactInfos)
        {
            DialogueService_new dialogueService_new = new DialogueService_new(authorization.client, authorization.username, authorization.password, null);
            return dialogueService_new.registerContact(dialogueSetting,  ContactInfos);
        }
    }
}
