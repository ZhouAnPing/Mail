
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using Tencent;
using Wechat.BO;
using Wechat.Properties;

namespace Wechat
{
    /// <summary>
    /// Summary description for Wechat
    /// </summary>
    public class Wechat : IHttpHandler
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Wechat));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {


            logger.Info(context.Request.Url.AbsoluteUri);


            /*
            ------------使用示例二：对用户回复的消息解密---------------
            用户回复消息或者点击事件响应时，企业会收到回调消息，此消息是经过公众平台加密之后的密文以post形式发送给企业，密文格式请参考官方文档
            假设企业收到公众平台的回调消息如下：
            POST /cgi-bin/wxpush? msg_signature=477715d11cdb4164915debcba66cb864d751f3e6&timestamp=1409659813&nonce=1372623149 HTTP/1.1
            Host: qy.weixin.qq.com
            Content-Length: 613
            <xml>			<ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName><Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt>
            <AgentID><![CDATA[218]]></AgentID>
            </xml>

            企业收到post请求之后应该			1.解析出url上的参数，包括消息体签名(msg_signature)，时间戳(timestamp)以及随机数字串(nonce)
            2.验证消息体签名的正确性。
            3.将post请求的数据进行xml解析，并将<Encrypt>标签的内容进行解密，解密出来的明文即是用户回复消息的明文，明文格式请参考官方文档
            第2，3步可以用公众平台提供的库函数DecryptMsg来实现。
            */


            // Post请求的密文数据            
            // string sToken = Settings.Default.Token;//"L3TqpgplkguXp1Dtpz68NkBOCHE9fT";
            //string sCorpID = Settings.Default.CorpID;// "wx4fe8b74e01fffcbb";
            //string sEncodingAESKey = Settings.Default.EncodingAESKey;// "7YgmiXBs5fAe2gANxnnYdTkJT3pHeaPrOpn2GKsuX3L";
            string sToken = "unicom";
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
            logger.Info("sVerifyMsgSig=" + sReqMsgSig);
            logger.Info("sVerifyTimeStamp=" + sReqTimeStamp);
            logger.Info("sVerifyNonce=" + sReqNonce);
            logger.Info("sReqData=" + sReqData);
            logger.Info("ret=" + ret);
            logger.Info("sMsg=" + sMsg);

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


            logger.Info("FromUserName: " + wechatMessage.FromUserName);
            logger.Info("ToUserName: " + wechatMessage.ToUserName);
            logger.Info("CreateTime: " + wechatMessage.CreateTime);
            logger.Info("Event: " + wechatMessage.Event);
            logger.Info("EventKey: " + wechatMessage.EventKey);
            logger.Info("AgentID: " + wechatMessage.AgentID);
            logger.Info("Content: " + wechatMessage.Content);
            logger.Info("Type: " + wechatMessage.MsgType);

            /*
            ------------使用示例三：企业回复用户消息的加密---------------
            企业被动回复用户的消息也需要进行加密，并且拼接成密文格式的xml串。
            假设企业需要回复用户的明文如下：
            <xml>
            <ToUserName><![CDATA[mycreate]]></ToUserName>
            <FromUserName><![CDATA[wx5823bf96d3bd56c7]]></FromUserName>
            <CreateTime>1348831860</CreateTime>
            <MsgType><![CDATA[text]]></MsgType>
            <Content><![CDATA[this is a test]]></Content>
            <MsgId>1234567890123456</MsgId>
            <AgentID>128</AgentID>
            </xml>

            为了将此段明文回复给用户，企业应：			
             * 1.自己生成时间时间戳(timestamp),随机数字串(nonce)以便生成消息体签名
             * ，也可以直接用从公众平台的post url上解析出的对应值。
            2.将明文加密得到密文。	
             *3.用密文，步骤1生成的timestamp,nonce和企业在公众平台设定的token生成消息体签名。			
             *4.将密文，消息体签名，时间戳，随机数字串拼接成xml格式的字符串，发送给企业。
            以上2，3，4步可以用公众平台提供的库函数EncryptMsg来实现。
            */
            // 需要发送的明文
            String actionType = wechatMessage.EventKey;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", wechatMessage.FromUserName);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", wechatMessage.ToUserName);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", wechatMessage.CreateTime);

            // string sRespData = "<MsgId>1234567890123456</MsgId>";
            logger.Info("EventKey: " + wechatMessage.EventKey);

            switch (actionType)
            {
                case "Previous3Months":

                    sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
                    sb.AppendFormat("<ArticleCount>4</ArticleCount>");
                    sb.AppendFormat("<Articles>");
                    sb.AppendFormat("<item>");


                    sb.AppendFormat("</item>");
                    for (int itemCount = 0; itemCount <= 3; itemCount++)
                    {
                        sb.AppendFormat("<item>");
                        sb.Append("<Title>").AppendFormat("{0}佣金告知单", DateTime.Now.AddMonths(-(itemCount + 1)).ToString("yyyy年MM月")).Append("</Title>");
                        String itemUrl = String.Format("http://115.29.229.134/Wechat/AgentFeeQuery.aspx?agentNo={0}&feeMonth={1}", wechatMessage.FromUserName, DateTime.Now.AddMonths(-(itemCount + 1)).ToString("yyyy-MM"));

                        sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", itemUrl).Append("</Url>");
                        sb.AppendFormat("</item>");
                    }
                    sb.AppendFormat("</Articles>");
                    break;
                case "CurrentFeeQuery":
                    String feeMonth = DateTime.Now.ToString("yyyy-MM");
                    AgentFeeDao agentFeeDao = new AgentFeeDao();
                    AgentFee agentFee = agentFeeDao.GetByKey(feeMonth, wechatMessage.FromUserName);

                    sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
                    sb.AppendFormat("<ArticleCount>1</ArticleCount>");
                    sb.AppendFormat("<Articles>");

                    sb.AppendFormat("<item>");
                    sb.Append("<Title>").AppendFormat("{0}佣金告知单", DateTime.Now.ToString("yyyy年MM月")).Append("</Title>");

                    StringBuilder sbDesc = new StringBuilder();
                    //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
                    sbDesc.AppendFormat("告知单编号：{0}\n\n", agentFee.agentFeeSeq);
                    sbDesc.AppendFormat("合作伙伴编号：{0}\n", agentFee.agentNo);
                    sbDesc.AppendFormat("合作伙伴名字：{0}\n", agentFee.agent.contactName);
                    sbDesc.AppendFormat("渠道类型：{0}\n", agentFee.agent.agentType);

                    // sb1.AppendFormat("佣金\n\n");
                    sbDesc.Append("佣金总计:").Append(agentFee.feeTotal).Append("\n");


                    char[] separator = "<br>".ToCharArray();

                    if (!String.IsNullOrEmpty(agentFee.agent.agentTypeComment))
                    {
                        sbDesc.AppendFormat("\n渠道说明：\n");
                        string[] agentTypeCommentList = agentFee.agent.agentTypeComment.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        for (int count = 0; count < agentTypeCommentList.Length; count++)
                        {
                            sbDesc.Append(count + 1).AppendFormat(".{0}\n", agentTypeCommentList[count]);
                        }
                    }
                    sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");

                    String url1 = String.Format("http://115.29.229.134/Wechat/AgentFeeQuery.aspx?agentNo={0}&feeMonth={1}", wechatMessage.FromUserName, feeMonth);

                    sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                    sb.AppendFormat("</item>");

                    sb.AppendFormat("</Articles>");

                    break;
                case "OtherFeeMonth":
                    sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
                    sb.AppendFormat("<ArticleCount>1</ArticleCount>");
                    sb.AppendFormat("<Articles>");
                    sb.AppendFormat("<item>");
                    sb.Append("<Title>").AppendFormat("其他月份佣金查询").Append("</Title>"); ;

                    StringBuilder sbOtherFeeContent = new StringBuilder();
                    sbOtherFeeContent.AppendFormat("按以下关键字查询\n\n");
                    sbOtherFeeContent.AppendFormat("1.佣金查询请输入\"佣金yyyy-mm\",例如:\"佣金2014-10\"查询2014年10月份佣金\n\n");



                    sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbOtherFeeContent.ToString()).Append("</Description>");

                    //           sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                    sb.AppendFormat("</item>");

                    sb.AppendFormat("</Articles>");
                    break;
                case "ErrorCodeQuery":
                    sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
                    sb.AppendFormat("<ArticleCount>1</ArticleCount>");
                    sb.AppendFormat("<Articles>");
                    sb.AppendFormat("<item>");
                    sb.Append("<Title>").AppendFormat("错误代码查询").Append("</Title>"); ;

                    StringBuilder sbErrorCodeContent = new StringBuilder();
                    sbErrorCodeContent.AppendFormat("按以下关键字查询\n\n");
                    sbErrorCodeContent.AppendFormat("1.错误代码查询请输入\"错误代码:关键字或者代码\",例如:\"错误代码:10001\"查询错误代码是10001的错误信息\n");

                    sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbErrorCodeContent.ToString()).Append("</Description>");

                    //           sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                    sb.AppendFormat("</item>");

                    sb.AppendFormat("</Articles>");
                    break;

                default:

                    if (wechatMessage.Content.ToLower().Contains("commission:"))
                    {
                        sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
                        sb.AppendFormat("<ArticleCount>1</ArticleCount>");
                        sb.AppendFormat("<Articles>");
                        sb.AppendFormat("<item>");
                        sb.Append("<Title>").AppendFormat("佣金查询结果").Append("</Title>");
                        feeMonth = wechatMessage.Content.Substring("commission:".Length);
                        feeMonth = DateTime.Now.ToString("yyyy-MM");
                        agentFeeDao = new AgentFeeDao();
                        agentFee = agentFeeDao.GetByKey(feeMonth, wechatMessage.FromUserName);
                        sbDesc = new StringBuilder();
                        //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
                        sbDesc.AppendFormat("告知单编号：{0}\n\n", agentFee.agentFeeSeq);
                        sbDesc.AppendFormat("合作伙伴编号：{0}\n", agentFee.agentNo);
                        sbDesc.AppendFormat("合作伙伴名字：{0}\n", agentFee.agent.contactName);
                        sbDesc.AppendFormat("渠道类型：{0}\n", agentFee.agent.agentType);

                        // sb1.AppendFormat("佣金\n\n");
                        sbDesc.Append("佣金总计:").Append(agentFee.feeTotal).Append("\n");


                        separator = "<br>".ToCharArray();

                        if (!String.IsNullOrEmpty(agentFee.agent.agentTypeComment))
                        {
                            sbDesc.AppendFormat("\n渠道说明：\n");
                            string[] agentTypeCommentList = agentFee.agent.agentTypeComment.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            for (int count = 0; count < agentTypeCommentList.Length; count++)
                            {
                                sbDesc.Append(count + 1).AppendFormat(".{0}\n", agentTypeCommentList[count]);
                            }
                        }
                        sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");

                        url1 = String.Format("http://115.29.229.134/Wechat/AgentFeeQuery.aspx?agentNo={0}&feeMonth={1}", wechatMessage.FromUserName, feeMonth);

                        sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                        //           sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                        sb.AppendFormat("</item>");

                        sb.AppendFormat("</Articles>");
                    }
                    else if (wechatMessage.Content.ToLower().Contains("error:"))
                    {
                        sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");

                        AgentErrorCodeDao agentErrorCodeDao = new AgentErrorCodeDao();
                        logger.Info(wechatMessage.Content.Substring(6));
                        IList<AgentErrorCode> agentErrorCodeList = agentErrorCodeDao.GetList(wechatMessage.Content.Substring(6));

                        if (agentErrorCodeList.Count > 5)
                        {
                            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", 5);
                        }
                        else if (agentErrorCodeList.Count <= 5 && agentErrorCodeList.Count > 0)
                        {
                            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", agentErrorCodeList.Count);
                        }
                        else
                        {
                            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", 1);
                        }
                        sb.AppendFormat("<Articles>");

                        int count = 0;
                        foreach (AgentErrorCode agentErrorCode in agentErrorCodeList)
                        {
                            count++;

                            if (count > 5)
                            {
                                break;
                            }
                            logger.Info("Path=" + context.Server.MapPath("~/") + @"\ErrorImages\" + agentErrorCode.seq + ".jpg");
                            String path = context.Server.MapPath("~/") + @"\ErrorImages\" + agentErrorCode.seq + ".jpg";
                            try
                            {
                                System.IO.File.WriteAllBytes(path, agentErrorCode.errorImg);
                                sb.AppendFormat("<item>");
                                sb.Append("<Title>").AppendFormat("{0}错误查询结果", agentErrorCode.keyword).Append("</Title>");
                                // String errorCondition = wechatMessage.Content.Substring("error:".Length);

                                sbDesc = new StringBuilder();
                                //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);

                                sbDesc.AppendFormat("问题描述：\n{0}\n\n", agentErrorCode.errorDesc);
                                sbDesc.AppendFormat("处理方法：\n{0}\n\n", agentErrorCode.solution);
                                sbDesc.AppendFormat("联系人员：{0}\n\n", agentErrorCode.contactName);
                                sbDesc.AppendFormat("备注：\n{0}\n", agentErrorCode.comment);
                                sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");
                                sb.Append("<PicUrl>").AppendFormat("<![CDATA[{0}{1}{2}]]>", "http://115.29.229.134/Wechat/ErrorImages/", agentErrorCode.seq, ".jpg").Append("</PicUrl>");
                                sb.Append("<Url>").AppendFormat("<![CDATA[{0}{1}{2}]]>", "http://115.29.229.134/Wechat/ErrorImages/", agentErrorCode.seq, ".jpg").Append("</Url>");
                                //           sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                                sb.AppendFormat("</item>");
                                //  logger.Info(sb.ToString());
                            }
                            catch (Exception ex)
                            {
                                logger.Info(ex.Message);
                            }
                        }





                        sb.AppendFormat("</Articles>");
                    }
                    else
                    {
                        sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
                        sb.AppendFormat("<ArticleCount>1</ArticleCount>");
                        sb.AppendFormat("<Articles>");
                        sb.AppendFormat("<item>");
                        sb.Append("<Title>").AppendFormat("联通渠道移动办公平台").Append("</Title>");
                        StringBuilder sbDefaultContent = new StringBuilder();
                        sbDefaultContent.AppendFormat("欢迎来到联通渠道移动办公平台，按以下关键字查询\n\n");
                        sbDefaultContent.AppendFormat("1.佣金查询请输入\"commission:yyyymm\",例如:\"commission:201410\"查询2014年10月份佣金\n\n");
                        sbDefaultContent.AppendFormat("2.错误代码查询请输入\"error:关键字或者代码\",例如:\"error:10001\"查询错误代码是10001的错误信息\n");
                        sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDefaultContent.ToString()).Append("</Description>");

                        //           sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                        sb.AppendFormat("</item>");

                        sb.AppendFormat("</Articles>");
                    }


                    break;
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
            // TODO:
            // 加密成功，企业需要将加密之后的sEncryptMsg返回
            // HttpUtils.SetResponse(sEncryptMsg);

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