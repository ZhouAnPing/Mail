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

namespace Wechat
{
    /// <summary>
    /// Summary description for ErrorCodeHandler
    /// </summary>
    public class ErrorCodeHandler : IHttpHandler
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ErrorCodeHandler));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            
            logger.Info(context.Request.Url.AbsoluteUri);


            string sToken = "ErrorCode";
            string sCorpID = "wx4fe8b74e01fffcbb";
            string sEncodingAESKey = "KsTUiKS3INKdvPL2vupBgslE1EUEK7KzHOKIBtIqQnU";

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


            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<xml>");
            sb.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", wechatMessage.FromUserName);
            sb.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", wechatMessage.ToUserName);
            sb.AppendFormat("<CreateTime>{0}</CreateTime>", wechatMessage.CreateTime);

            try
            {

                switch (wechatMessage.Content.ToLower())
                {
                    case "help":
                    case "？":
                    case "?":
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "请输入错误代码关键字查询错误详细说明,例如:输入\"系统异常\"查询包含\"系统异常\"的错误信息");

                        break;

                    default:
                        sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
                        AgentErrorCodeDao agentErrorCodeDao = new AgentErrorCodeDao();
                       
                        IList<AgentErrorCode> agentErrorCodeList = agentErrorCodeDao.GetList(wechatMessage.Content);

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
                            String dir = context.Server.MapPath("~/") + @"\ErrorImages\";
                            logger.Info("Path=" + dir + agentErrorCode.seq + ".jpg");
                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }
                          
                            String path = dir + agentErrorCode.seq + ".jpg";
                          

                             if (!File.Exists(path) || !File.GetCreationTime(path).ToString("yyyy-MM-dd").Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                            {
                                logger.Info("path="+path);
                                System.IO.File.WriteAllBytes(path, agentErrorCode.errorImg);
                            }
                            sb.AppendFormat("<item>");
                            sb.Append("<Title>").AppendFormat("{0}错误查询结果", agentErrorCode.keyword).Append("</Title>");
                            // String errorCondition = wechatMessage.Content.Substring("error:".Length);

                            StringBuilder sbDesc = new StringBuilder();
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
                        sb.AppendFormat("</Articles>");

                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
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