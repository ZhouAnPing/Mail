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

namespace Wechat
{
    /// <summary>
    /// Summary description for AgentFeeHandler
    /// </summary>
    public class AgentFeeHandler : IHttpHandler
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentFeeHandler));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {


            logger.Info(context.Request.Url.AbsoluteUri);


            string sToken = "AgentFee";
            string sCorpID = "wx4fe8b74e01fffcbb";
            string sEncodingAESKey = "gvGJnhpjeljcKzvfe8B8vnmMBBLkJFuzUYSjsGcDQFE";

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

            AgentDao agentDao = new AgentDao();
            Agent agent = agentDao.Get(wechatMessage.FromUserName);

            if (agent!=null&&!String.IsNullOrEmpty(agent.status) && agent.status.Equals("Y"))
            {
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "对不起，你的账号已被停用，请联系联通工作人员。。。\n\n");

            }
            else
            {

                switch (actionType)
                {
                    case "OtherFeeMonth":
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "请输入\"yyyy-mm\"查询某月佣金,例如:\"" + DateTime.Now.ToString("yyyy-MM") + "\"查询" + DateTime.Now.ToString("yyyy年MM月") + "佣金\n\n");

                        break;

                    case "PreMonthFeeQuery":
                    case "CurMonthFeeQuery":
                        String feeMonth = DateTime.Now.ToString("yyyy-MM");
                        if (actionType.Equals("PreMonthFeeQuery"))
                        {
                            feeMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
                        }
                        AgentFeeDao agentFeeDao = new AgentFeeDao();
                        AgentFee agentFee = agentFeeDao.GetByKey(feeMonth, wechatMessage.FromUserName);
                        if (agentFee != null && !String.IsNullOrEmpty(agentFee.agentFeeSeq))
                        {
                            sb.Append(this.createNewsMessages(feeMonth, agentFee, wechatMessage.FromUserName));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "佣金还未发布，请稍后。。。\n\n");
                        }
                        break;


                    default:

                        if (!Regex.IsMatch(wechatMessage.Content, "((20[0-9][0-9])|(19[0-9][0-9]))-((0[1-9])|(1[0-2]))"))
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "请输入\"yyyy-mm\"查询某月佣金,例如:\"" + DateTime.Now.ToString("yyyy-MM") + "\"查询" + DateTime.Now.ToString("yyyy年MM月") + "佣金\n\n");
                        }
                        else
                        {
                            feeMonth = wechatMessage.Content;
                            agentFeeDao = new AgentFeeDao();
                            agentFee = agentFeeDao.GetByKey(feeMonth, wechatMessage.FromUserName);
                            if (agentFee != null && !String.IsNullOrEmpty(agentFee.agentFeeSeq))
                            {
                                sb.Append(this.createNewsMessages(feeMonth, agentFee, wechatMessage.FromUserName));

                            }
                            else
                            {
                                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "佣金还未发布，请稍后。。。\n\n");

                            }
                        }

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

        private StringBuilder createNewsMessages(String feeMonth, AgentFee agentFee, String toUser)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>1</ArticleCount>");
            sb.AppendFormat("<Articles>");

            sb.AppendFormat("<item>");
            sb.Append("<Title>").AppendFormat("{0}佣金告知单", feeMonth).Append("</Title>");

            StringBuilder sbDesc = new StringBuilder();
            //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
            sbDesc.AppendFormat("告知单编号：{0}\n", agentFee.agentFeeSeq);
            sbDesc.AppendFormat("合作伙伴编号：{0}\n", agentFee.agentNo);
            sbDesc.AppendFormat("合作伙伴名字：{0}\n", agentFee.agent.contactName);
            sbDesc.AppendFormat("渠道类型：{0}\n", agentFee.agent.agentType);

            // sb1.AppendFormat("佣金\n\n");
          
            sbDesc.AppendFormat("佣金明细：\n");
            int i = 1;
            for (int j = 1; j <= 100; j++)
            {
                FieldInfo feeNameField = agentFee.GetType().GetField("feeName" + j);
                FieldInfo feeField = agentFee.GetType().GetField("fee" + j);
                if (feeNameField != null && feeField != null)
                {
                    String feeNameFieldValue = feeNameField.GetValue(agentFee) == null ? null : feeNameField.GetValue(agentFee).ToString();

                    String feeFieldValue = feeField.GetValue(agentFee) == null ? null : feeField.GetValue(agentFee).ToString();

                    if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);
           
                    }
                }


            }
            sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", "佣金总计").Append(" ").AppendFormat("{0}\n", agentFee.feeTotal);

          


            char[] separator = "<br>".ToCharArray();

            if (!String.IsNullOrEmpty(agentFee.agent.agentTypeComment))
            {
                sbDesc.AppendFormat("\n本月佣金说明：\n");
                string[] agentTypeCommentList = agentFee.agent.agentTypeComment.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int count = 0; count < agentTypeCommentList.Length; count++)
                {
                    sbDesc.Append("        ").Append(count + 1).AppendFormat(".{0}\n", agentTypeCommentList[count]);
                }
            }
            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");

            String url1 = String.Format("http://115.29.229.134/Wechat/AgentFeeQuery.aspx?agentNo={0}&feeMonth={1}", toUser, feeMonth);

            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }
    }
}