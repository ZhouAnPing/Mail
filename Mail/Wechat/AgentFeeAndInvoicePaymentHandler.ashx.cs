﻿using ChinaUnion_BO;
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
    /// Summary description for AgentFeeAndInvoicePaymentHandler
    /// </summary>
    public class AgentFeeAndInvoicePaymentHandler : IHttpHandler
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentFeeAndInvoicePaymentHandler));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {


            logger.Info(context.Request.Url.AbsoluteUri);


            // string sToken = "AgentFee";
            // string sCorpID = "wx4fe8b74e01fffcbb";
            // string sEncodingAESKey = "gvGJnhpjeljcKzvfe8B8vnmMBBLkJFuzUYSjsGcDQFE";

            string sToken ="AgentFeeAndInvoicePaymentHandler";//"AgentFee";
            string sCorpID = Properties.Settings.Default.Wechat_CorpId;// "wx31204de5a3ae758e";
            string sEncodingAESKey = "4m6avCYhQ2p4IwjtMpFWNHRd46k2uIgdLbHSAlyCQsJ";// "he8dYrZ5gLbDrDhfHVJkea1AfmHgRZQJq47kuKpQrSO";

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

            if (agent != null && !String.IsNullOrEmpty(agent.status) && agent.status.Equals("Y"))
            {
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "对不起，你的账号已被停用，请联系联通工作人员。。。\n\n");

            }
            else
            {

                switch (actionType)
                {
                    case "FeeQueryHelp":
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "佣金查询说明\n\n");

                        break;

                    case "BonusQuery":
                        AgentBonusDao agentBonusDao = new AgentBonusDao();
                        AgentBonus agentBonus = agentBonusDao.Get(wechatMessage.FromUserName);
                        if (agentBonus != null)
                        {

                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");

                            StringBuilder sbContent = new StringBuilder();


                            sbContent.AppendFormat("代理商编号:{0}", agentBonus.agentNo).Append("\n");
                            sbContent.AppendFormat("代理商名称:{0}", agentBonus.agentName).Append("\n");

                            sbContent.AppendFormat("渠道积分奖励:{0}", agentBonus.scoreBonus).Append("\n");
                            sbContent.AppendFormat("后付费奖励:{0}", agentBonus.afterFeeBonus).Append("\n");

                            sbContent.AppendFormat("渠道星级补贴:{0}", agentBonus.starBonus).Append("\n");
                            sbContent.AppendFormat("红包总金额:{0}", agentBonus.totalBonus).Append("\n");


                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", sbContent.ToString());
                            // sb.Append(sbContent.ToString());
                            // sb.Append(this.createNewsMessages(feeDate, wechatMessage.FromUserName, agentDailyPerformance));
                        }
                        else
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "红包还没发布，如有疑问，请直接与上海联通确认。。。\n\n");
                        }

                        break;

                    case "Latest6MonthFeeQuery":
                        String strList = "最近6月佣金查询\n\n";
                        for (int i = 1; i <= 6; i++)
                        {
                            String tempFeeMonth = DateTime.Now.AddMonths(0 - i).ToString("yyyy-MM");

                            String url1 = String.Format("http://{0}/Wechat/AgentFeeQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(wechatMessage.FromUserName, QueryStringEncryption.key), QueryStringEncryption.Encode(tempFeeMonth, QueryStringEncryption.key));

                            strList = strList + "<a href=\"" + url1 + "\">" + i + ":" + tempFeeMonth + "月佣金</a>";
                            strList = strList + "\n";
                        }
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", strList);
                        break;

                    case "PreMonthFeeQuery":
                    case "CurMonthFeeQuery":
                        String feeMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
                        if (actionType.Equals("PreMonthFeeQuery"))
                        {
                            feeMonth = DateTime.Now.AddMonths(-2).ToString("yyyy-MM");
                        }
                        AgentFeeDao agentFeeDao = new AgentFeeDao();
                        AgentFee agentFee = agentFeeDao.GetByKey(feeMonth, wechatMessage.FromUserName);
                        if (agentFee != null && !String.IsNullOrEmpty(agentFee.agentFeeSeq))
                        {
                            sb.Append(this.createAgentFeeNewsMessages(feeMonth, agentFee, wechatMessage.FromUserName));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "佣金还未发布，请稍后。。。\n\n");
                        }
                        break;
                    case "PaymentQueryHelp":
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "发票支付查询说明\n\n");

                        break;

                    case "Latest6MonthPaymentQuery":
                        String strList1 = "最近6月发票支付查询\n\n";
                        for (int i = 1; i <= 6; i++)
                        {
                            String tempFeeMonth = DateTime.Now.AddMonths(0 - i).ToString("yyyyMM");

                            String url1 = String.Format("http://{0}/Wechat/InvoicePaymentQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(wechatMessage.FromUserName, QueryStringEncryption.key), QueryStringEncryption.Encode(tempFeeMonth, QueryStringEncryption.key));

                            strList1 = strList1 + "<a href=\"" + url1 + "\">" + i + ":" + tempFeeMonth + "</a>";
                            strList1 = strList1 + "\n";
                        }
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", strList1);
                        break;

                    case "PreMonthPaymentQuery":
                    case "CurMonthPaymentQuery":
                        feeMonth = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
                        if (actionType.Equals("PreMonthPaymentQuery"))
                        {
                            feeMonth = DateTime.Now.AddMonths(-2).ToString("yyyyMM");
                        }
                        InvoicePaymentDao agentInvoicePaymentDao = new InvoicePaymentDao();

                        IList<InvoicePayment> agentInvoicePaymentList = new List<InvoicePayment>();

                        //  agentNo = "";//"DL224049";
                        // feeMonth = "201412";
                        logger.Info("1.feeMonth=" + feeMonth);
                        logger.Info("2.agentNo=" + wechatMessage.FromUserName);

                        agentInvoicePaymentList = agentInvoicePaymentDao.GetList(wechatMessage.FromUserName, null, feeMonth,null);

                        if (agentInvoicePaymentList != null && agentInvoicePaymentList.Count > 0)
                        {
                            sb.Append(this.createPaymentNewsMessages(feeMonth, wechatMessage.FromUserName, agentInvoicePaymentList));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "发票支付还未受理，请稍后。。。\n\n");
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
                                sb.Append(this.createAgentFeeNewsMessages(feeMonth, agentFee, wechatMessage.FromUserName));

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
        private StringBuilder createPaymentNewsMessages(String feeMonth, String toUser, IList<InvoicePayment> agentInvoicePaymentList)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>1</ArticleCount>");
            sb.AppendFormat("<Articles>");

            sb.AppendFormat("<item>");
            sb.Append("<Title>").AppendFormat("{0}发票支付结果", feeMonth).Append("</Title>");

            StringBuilder sbDesc = new StringBuilder();
            //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
            sbDesc.AppendFormat("总共处理了：{0}次发票支付信息\n", agentInvoicePaymentList.Count);
            foreach (InvoicePayment agentInvoicePayment in agentInvoicePaymentList)
            {
                sbDesc.AppendFormat("\n处理时间：" + agentInvoicePayment.processTime + "\n发票金额：" + agentInvoicePayment.invoiceFee + "\n内容：" + agentInvoicePayment.content  + "\n发票类型：" + agentInvoicePayment.invoiceType + "\n付款状态：" + agentInvoicePayment.payStatus).AppendLine();

            }


            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



            String url1 = String.Format("http://{0}/Wechat/InvoicePaymentQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(toUser, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }
        private StringBuilder createAgentFeeNewsMessages(String feeMonth, AgentFee agentFee, String toUser)
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
            sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", "过网开票金额").Append(" ").AppendFormat("{0}\n", agentFee.preInvoiceFee);




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



            String url1 = String.Format("http://{0}/Wechat/AgentFeeQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(toUser, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }
    }
}