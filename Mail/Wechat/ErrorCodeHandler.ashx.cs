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


            //string sToken = "AgentFee";
            //string sCorpID = "wx4fe8b74e01fffcbb";
            //string sEncodingAESKey = "gvGJnhpjeljcKzvfe8B8vnmMBBLkJFuzUYSjsGcDQFE";

            string sToken = Properties.Settings.Default.Wechat_ErrorCode_Token;// "ErrorCode";
            string sCorpID = Properties.Settings.Default.Wechat_CorpId;// "wx31204de5a3ae758e";
            string sEncodingAESKey = Properties.Settings.Default.Wechat_ErrorCode_EncodingAESKey;// "VcEu5ijaEa6xVklONE9APWJgfvh1UhGjXGKAdeHWAWQ";

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
                wechatQueryLog.module = Util.MyConstant.module_Error;
                wechatQueryLog.subSystem = "报错处理";
                wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                wechatQueryLog.queryString = "成员进入应用";
                wechatQueryLog.wechatId = agentWechatAccount.contactId;
                WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
                wechatQueryLogDao.Add(wechatQueryLog);
            }

            try
            {


                if (String.IsNullOrEmpty(wechatMessage.Content))
                {
                    wechatMessage.Content = "help";
                }

                switch (wechatMessage.Content.ToLower())
                {
                    case "help":
                    case "？":
                    case "?":
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "请输入错误关键字查询错误详细说明,例如:输入\"系统异常\"查询包含\"系统异常\"的错误信息");

                        break;

                    default:
                       
                        AgentErrorCodeDao agentErrorCodeDao = new AgentErrorCodeDao();
                        int maxArticleCnt = 10;
                        IList<AgentErrorCode> agentErrorCodeList = agentErrorCodeDao.GetList(wechatMessage.Content);
                        if (agentErrorCodeList != null && agentErrorCodeList.Count > 0)
                        {
                            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
                            if (agentErrorCodeList.Count > maxArticleCnt)
                            {
                                sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", maxArticleCnt);
                            }
                            else if (agentErrorCodeList.Count <= 10 && agentErrorCodeList.Count > 0)
                            {
                                sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", agentErrorCodeList.Count);
                            }
                           
                            sb.AppendFormat("<Articles>");

                            int count = 0;
                            foreach (AgentErrorCode agentErrorCode in agentErrorCodeList)
                            {
                                count++;

                                if (count > maxArticleCnt)
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
                                    if (agentErrorCode.errorImg != null)
                                    {
                                        logger.Info("path=" + path);

                                        System.IO.File.WriteAllBytes(path, agentErrorCode.errorImg);
                                    }
                                    else
                                    {
                                        logger.Info("path=no image");
                                    }
                                }
                                sb.AppendFormat("<item>");
                                sb.Append("<Title>").AppendFormat("{0}报错查询结果", agentErrorCode.keyword).Append("</Title>");
                                // String errorCondition = wechatMessage.Content.Substring("error:".Length);

                                StringBuilder sbDesc = new StringBuilder();
                                //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);

                                sbDesc.AppendFormat("问题描述：\n{0}\n\n", agentErrorCode.errorDesc);
                                sbDesc.AppendFormat("处理方法：\n{0}\n\n", agentErrorCode.solution);
                                sbDesc.AppendFormat("联系人员：{0}\n\n", agentErrorCode.contactName);
                                sbDesc.AppendFormat("备注：\n{0}\n", agentErrorCode.comment);
                                sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");
                                sb.Append("<PicUrl>").AppendFormat("<![CDATA[{0}{1}{2}]]>", "http://"+Properties.Settings.Default.Host+"/Wechat/ErrorImages/", agentErrorCode.seq, ".jpg").Append("</PicUrl>");
                                //logger.Info("path=" + "http://"http://"+Properties.Settings.Default.Host+"/Wechat/ErrorCodeQuery.aspx?keyword=" + context.Server.UrlEncode(agentErrorCode.keyword));
                                sb.Append("<Url>").AppendFormat("<![CDATA[{0}{1}{2}]]>", "http://" + Properties.Settings.Default.Host + "/Wechat/ErrorCodeQuery.aspx?keyword=", context.Server.UrlEncode(agentErrorCode.keyword), "&userId=" + wechatMessage.FromUserName).Append("</Url>");
                                //           sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                                sb.AppendFormat("</item>");
                                //  logger.Info(sb.ToString());


                            }
                            sb.AppendFormat("</Articles>");
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "没有找到与" + wechatMessage.Content+"相关的错误详细信息,请更改查询条件或者与联通相关人员确认。");
           
                        }
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