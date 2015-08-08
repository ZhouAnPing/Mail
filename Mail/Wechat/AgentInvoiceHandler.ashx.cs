using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Wechat.BO;
using Wechat.Util;

namespace Wechat
{
    /// <summary>
    /// Summary description for AgentInvoiceHandler
    /// </summary>
    public class AgentInvoiceHandler : IHttpHandler
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentFeeHandler));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {


            logger.Info(context.Request.Url.AbsoluteUri);


            string sToken = Properties.Settings.Default.Wechat_AgentInvoice_Token;// "agentInvoice";
            string sCorpID = Properties.Settings.Default.Wechat_CorpId;// "wx4fe8b74e01fffcbb";
            string sEncodingAESKey = Properties.Settings.Default.Wechat_AgentInvoice_EncodingAESKey;// "omiSDTqK4GjKmsQ6eCJSWpOtmqPcz6A3B41RBcg6Ey9";

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
            String agentNo = wechatMessage.FromUserName;
            AgentDao agentDao = new AgentDao();
            Agent agent = agentDao.Get(agentNo);

            if (agent != null && !String.IsNullOrEmpty(agent.status) && agent.status.Equals("Y"))
            {
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "对不起，你的账号已被停用，请联系联通工作人员。。。\n\n");

            }
            else
            {

                switch (actionType)
                {
                    case "OtherInvoiceQuery":
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "请输入\"1.yyyymm\"查询某月发票,例如:\"1." + DateTime.Now.ToString("yyyy-MM") + "\"查询" + DateTime.Now.ToString("yyyy年MM月") + "发票\n\n");

                        break;

                    case "PreInvoiceQuery":
                    case "CurInvoiceQuery":
                        String feeMonth = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
                        if (actionType.Equals("PreInvoiceQuery"))
                        {
                            feeMonth = DateTime.Now.AddMonths(-2).ToString("yyyyMM");
                        }
                        AgentInvoiceDao agentInvoiceDao = new AgentInvoiceDao();

                         IList<AgentInvoice> agentInvoiceList = new List<AgentInvoice>();

                       //  agentNo = "";//"DL224049";
                       //  feeMonth = "201501";
                      

                         agentInvoiceList = agentInvoiceDao.GetList(agentNo, null, feeMonth);

                         if (agentInvoiceList != null && agentInvoiceList.Count>0)
                        {
                            sb.Append(this.createNewsMessages(feeMonth, wechatMessage.FromUserName, agentInvoiceList));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "发票还未受理，请稍后。。。\n\n");
                        }
                        break;


                    case "OtherInvoicePaymentQuery":
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "请输入\"2.yyyymm\"查询某月支付,例如:\"2." + DateTime.Now.ToString("yyyy-MM") + "\"查询" + DateTime.Now.ToString("yyyy年MM月") + "支付\n\n");

                        break;

                    case "PreInvoicePaymentQuery":
                    case "CurInvoicePaymentQuery":
                        feeMonth = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
                        if (actionType.Equals("PreInvoicePaymentQuery"))
                        {
                            feeMonth = DateTime.Now.AddMonths(-2).ToString("yyyyMM");
                        }
                        AgentInvoicePaymentDao agentInvoicePaymentDao = new AgentInvoicePaymentDao();

                        IList<AgentInvoicePayment> agentInvoicePaymentList = new List<AgentInvoicePayment>();

                      //  agentNo = "";//"DL224049";
                       // feeMonth = "201412";
                         logger.Info("1.feeMonth=" + feeMonth);
                        logger.Info("2.agentNo=" + agentNo);

                        agentInvoicePaymentList = agentInvoicePaymentDao.GetList(agentNo, null, feeMonth);

                        if (agentInvoicePaymentList != null && agentInvoicePaymentList.Count > 0)
                        {
                            sb.Append(this.createNewsMessages(feeMonth, agentNo, agentInvoicePaymentList));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "支付还未受理，请稍后。。。\n\n");
                        }
                        break;

                    default:

                        if ((wechatMessage.Content.StartsWith("1") || wechatMessage.Content.StartsWith("2")) && wechatMessage.Content.Length>2)
                        {

                            feeMonth = wechatMessage.Content.Substring(2) ;

                            if (wechatMessage.Content.StartsWith("1"))
                            {
                                agentInvoiceDao = new AgentInvoiceDao();

                                agentInvoiceList = new List<AgentInvoice>();

                             //   agentNo = "DL224049";
                                // feeMonth = "201501";


                                agentInvoiceList = agentInvoiceDao.GetList(agentNo, null, feeMonth);

                                if (agentInvoiceList != null && agentInvoiceList.Count > 0)
                                {
                                    sb.Append(this.createNewsMessages(feeMonth, wechatMessage.FromUserName, agentInvoiceList));
                                }
                                else
                                {
                                    sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                    sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "发票还未受理，请稍后。。。\n\n");
                                }
                            }
                            else
                            {
                                agentInvoicePaymentDao = new AgentInvoicePaymentDao();

                                agentInvoicePaymentList = new List<AgentInvoicePayment>();

                               // agentNo = "DL224049";
                                //feeMonth = "2014-12";
                                logger.Info("1.feeMonth=" + feeMonth);
                                logger.Info("2.agentNo=" + agentNo);

                                agentInvoicePaymentList = agentInvoicePaymentDao.GetList(agentNo, null, feeMonth);

                                if (agentInvoicePaymentList != null && agentInvoicePaymentList.Count > 0)
                                {
                                    sb.Append(this.createNewsMessages(feeMonth, agentNo, agentInvoicePaymentList));
                                }
                                else
                                {
                                    sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                                    sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "支付还未受理，请稍后。。。\n\n");
                                }
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

        private StringBuilder createNewsMessages(String feeMonth, String toUser, IList<AgentInvoice> agentInvoiceList)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>1</ArticleCount>");
            sb.AppendFormat("<Articles>");

            sb.AppendFormat("<item>");
            sb.Append("<Title>").AppendFormat("{0}发票查询结果", feeMonth).Append("</Title>");

            StringBuilder sbDesc = new StringBuilder();
            //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
            sbDesc.AppendFormat("总共处理了：{0}次发票信息\n", agentInvoiceList.Count);

            foreach (AgentInvoice agentInvoice in agentInvoiceList)
            {
               sbDesc.AppendFormat("\n收票日期:" + agentInvoice.invoiceDate + "\n内容:" + agentInvoice.invoiceContent + "\n金额:" + agentInvoice.invoiceFee + "\n发票类型:" + agentInvoice.invoiceType + "\n发票号:" + agentInvoice.invoiceNo + "\n备注:" + agentInvoice.comment).AppendLine();

            }
            
            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



            String url1 = String.Format("http://{0}/Wechat/AgentInvoiceQuery.aspx?agentNo={1}&feeMonth={2}", "115.29.229.134", QueryStringEncryption.Encode(toUser, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }


        private StringBuilder createNewsMessages(String feeMonth, String toUser, IList<AgentInvoicePayment> agentInvoicePaymentList)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>1</ArticleCount>");
            sb.AppendFormat("<Articles>");

            sb.AppendFormat("<item>");
            sb.Append("<Title>").AppendFormat("{0}发票支付结果", feeMonth).Append("</Title>");

            StringBuilder sbDesc = new StringBuilder();
            //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
            sbDesc.AppendFormat("总共处理了：{0}次支付信息\n", agentInvoicePaymentList.Count);
            foreach (AgentInvoicePayment agentInvoicePayment in agentInvoicePaymentList)
            {
                sbDesc.AppendFormat( "\n处理时间：" + agentInvoicePayment.processTime + "\n发票金额：" + agentInvoicePayment.invoiceFee + "\n付款金额：" + agentInvoicePayment.payFee + "\n摘要：" + agentInvoicePayment.summary + "\n付款状态：" + agentInvoicePayment.payStatus).AppendLine();

            }


            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



            String url1 = String.Format("http://{0}/Wechat/AgentInvoicePaymentQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(toUser, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }
    }
}