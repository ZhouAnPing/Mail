using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Wechat.BO;
using Wechat.Util;

namespace Wechat
{
    /// <summary>
    /// Summary description for ServiceMonitorHandler
    /// </summary>
    public class ServiceMonitorHandler : IHttpHandler
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ServiceMonitorHandler));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
           

            logger.Info(context.Request.Url.AbsoluteUri);


            string sToken = "ServiceMonitorHandler";
            string sCorpID = Properties.Settings.Default.Wechat_CorpId;// "wx4fe8b74e01fffcbb";
            string sEncodingAESKey = "5trEQkj4dJjvcXs9I4MmUBUQty6uBOpNPGsjPEn4IgA";

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

            // string sRespData = "<MsgId>1234567890123456</MsgId>";
            logger.Info("EventKey: " + wechatMessage.EventKey);

            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            AgentWechatAccount agentWechatAccount = agentWechatAccountDao.Get(wechatMessage.FromUserName);

            if (agentWechatAccount != null && wechatMessage != null && !String.IsNullOrEmpty(wechatMessage.Event) && wechatMessage.Event.Equals("enter_agent"))
            {
                WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                wechatQueryLog.agentName = "";
                wechatQueryLog.module = Util.MyConstant.module_Service;
                wechatQueryLog.subSystem = "服务监督";
                wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                wechatQueryLog.queryString = "成员进入应用";
                wechatQueryLog.wechatId = agentWechatAccount.contactId;
                WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
                wechatQueryLogDao.Add(wechatQueryLog);
            }

            if (agentWechatAccount != null && !String.IsNullOrEmpty(agentWechatAccount.status) && !agentWechatAccount.status.Equals("Y"))
            {
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "对不起，你的账号已被停用，请联系联通工作人员!\n\n");

            }
            else if (agentWechatAccount == null)
            {
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "用户不存在，请联系联通工作人员!\n\n");
            }
            else
            {
                String agentNo = agentWechatAccount.branchNo;
                if (String.IsNullOrEmpty(agentNo))
                {
                    agentNo = agentWechatAccount.agentNo;
                }
                AgentContactDao agentContactDao = new ChinaUnion_DataAccess.AgentContactDao();

                switch (actionType)
                {
                    case "ContactPerson":
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "没有找到对应的联系人，请直接与上海联通确认!\n\n");
                        
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
    }
}