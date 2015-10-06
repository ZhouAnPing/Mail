using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Wechat.BO;
using Wechat.Util;

namespace Wechat
{
    /// <summary>
    /// Summary description for BusinessPolicyHandler
    /// </summary>
    public class BusinessPolicyHandler : IHttpHandler
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BusinessPolicyHandler));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {


            logger.Info(context.Request.Url.AbsoluteUri);


            string sToken = "BusinessPolicyHandler";
            string sCorpID = Properties.Settings.Default.Wechat_CorpId;// "wx4fe8b74e01fffcbb";
            string sEncodingAESKey = "3jJb1Xr7z6fF7LJPCESk8wX8XFf8E6mK4MYIbiOY8yt";// "omiSDTqK4GjKmsQ6eCJSWpOtmqPcz6A3B41RBcg6Ey9";

            //  string sToken = Properties.Settings.Default.Wechat_AgentFee_Token;//"AgentFee";
            //  string sCorpID = Properties.Settings.Default.Wechat_CorpId;// "wx31204de5a3ae758e";
            //  string sEncodingAESKey = Properties.Settings.Default.Wechat_AgentFee_EncodingAESKey;// "he8dYrZ5gLbDrDhfHVJkea1AfmHgRZQJq47kuKpQrSO";

            System.Collections.Specialized.NameValueCollection queryStrings = context.Request.QueryString;
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);

            context.Request.ContentEncoding = Encoding.UTF8;
            string sReqMsgSig = queryStrings["msg_signature"];
            string sReqTimeStamp = queryStrings["timestamp"];
            string sReqNonce = queryStrings["nonce"];

            // 获取Post请求的密文数据
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.GetEncoding("UTF-8"));
            string sReqData = reader.ReadToEnd();
            reader.Close();

            string sMsg = "";  // 解析之后的明文
            int ret = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, sReqData, ref sMsg);


            if (ret != 0)
            {
                logger.Info("ERR: Decrypt Fail, ret: " + ret);
                System.Console.WriteLine("ERR: Decrypt Fail, ret: " + ret);
                return;
            }
            // ret==0表示解密成功，sMsg表示解密之后的明文xml串           
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sMsg);
            WechatMessage wechatMessage = new WechatMessage(doc.DocumentElement);

            // 需要发送的明文
            String actionType = wechatMessage.EventKey;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", wechatMessage.FromUserName);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", wechatMessage.ToUserName);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", wechatMessage.CreateTime);

            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            AgentWechatAccount agentWechatAccount = agentWechatAccountDao.Get(wechatMessage.FromUserName);

            if (agentWechatAccount != null && wechatMessage != null && !String.IsNullOrEmpty(wechatMessage.Event) && wechatMessage.Event.Equals("enter_agent"))
            {
                WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                wechatQueryLog.agentName = "";
                wechatQueryLog.module = Util.MyConstant.module_Notice;
                wechatQueryLog.subSystem = "通知公告与促销政策";
                wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                wechatQueryLog.queryString = "成员进入应用";
                wechatQueryLog.wechatId = agentWechatAccount.contactId;
                WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
                wechatQueryLogDao.Add(wechatQueryLog);
            }

            // string sRespData = "<MsgId>1234567890123456</MsgId>";
            logger.Info("EventKey: " + wechatMessage.EventKey);
            String agentNo = wechatMessage.FromUserName;
            AgentDao agentDao = new AgentDao();
            Agent agent = agentDao.Get(agentNo);

            if (agent != null && !String.IsNullOrEmpty(agent.status) && agent.status.Equals("Y"))
            {
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "对不起，你的账号已被停用，请联系联通工作人员!\n\n");

            }
            else
            {

                PolicyDao policyDao = new ChinaUnion_DataAccess.PolicyDao();

                switch (actionType)
                {

                    case "LatestPolicy":

                    case "HistoryPolicy":
                    case "LatestNotice":                  

                    case "HistoryNotice":
                         IList<Policy> policyList =null;
                         if (actionType.Equals("LatestNotice"))
                         {
                             policyList = policyDao.GetAllList("");
                         }
                         else
                         {
                             policyList = policyDao.GetAllList("");
                         }






                         if (policyList != null && policyList.Count > 0)
                        {
                            sb.Append(this.createNewsMessages(wechatMessage.FromUserName, policyList));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "没有公告发布!\n\n");
                        }
                        break;


                    default:

                        break;
                }
            }
            //  sb.AppendFormat("<AgentID>{0}</AgentID>", textMessage.AgentID);

            sb.AppendFormat("</xml>");
            string sRespData = sb.ToString();
            string sEncryptMsg = ""; //xml格式的密文
            ret = wxcpt.EncryptMsg(sRespData, sReqTimeStamp, sReqNonce, ref sEncryptMsg);
            logger.Info("ret=" + ret);
            if (ret != 0)
            {
                System.Console.WriteLine("ERR: EncryptMsg Fail, ret: " + ret);


                return;
            }

            context.Response.Write(sEncryptMsg);


        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private StringBuilder createNewsMessages(String toUser, IList<Policy> policyList)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", policyList.Count);
            sb.AppendFormat("<Articles>");
            foreach (Policy policy in policyList)
            {
                sb.AppendFormat("<item>");
                sb.Append("<Title>").AppendFormat(policy.subject).Append("</Title>");

                StringBuilder sbDesc = new StringBuilder();
             


                sbDesc.AppendFormat("\n创建日期:" + policy.creatTime + "\n名称:" + policy.subject + "\n内容:" + policy.content + "\n类型:" + policy.type + "\n有效期:" + policy.validateStartTime).AppendLine();



                sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



                String url1 = String.Format("http://{0}/Wechat/BusinessPolicyDetail.aspx?seq={1}", Properties.Settings.Default.Host, policy.sequence);
                logger.Info(url1);
                sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                sb.AppendFormat("</item>");
            }

            sb.AppendFormat("</Articles>");
            return sb;
        }


      
    }
}