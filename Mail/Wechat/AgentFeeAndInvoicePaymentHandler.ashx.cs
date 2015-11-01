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

            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            AgentWechatAccount agentWechatAccount = agentWechatAccountDao.Get(wechatMessage.FromUserName);
            if (agentWechatAccount != null && wechatMessage != null && !String.IsNullOrEmpty(wechatMessage.Event) && wechatMessage.Event.Equals("enter_agent"))
            {
                WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                wechatQueryLog.agentName = "";
                wechatQueryLog.module = Util.MyConstant.module_Commission;
                wechatQueryLog.subSystem = "佣金结算与支付查询";
                wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                wechatQueryLog.queryString = "成员进入应用";
                wechatQueryLog.wechatId = agentWechatAccount.contactId;
                WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
                try
                {
                    wechatQueryLogDao.Add(wechatQueryLog);
                }
                catch
                {
                }
            }
            if (agentWechatAccount != null && !String.IsNullOrEmpty(agentWechatAccount.status) && !agentWechatAccount.status.Equals("Y"))
            {
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "对不起，你的账号已被停用，请联系联通工作人员!\n\n");

            }
            else if (agentWechatAccount==null)
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
                switch (actionType)
                {
                    case "FeeQueryHelp":
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "佣金查询说明\n\n");

                        break;

                    case "last6MonthBonus":
                        String strBonusList = "最近6月红包查询\n\n";
                        for (int i = 1; i <= 6; i++)
                        {
                            String tempFeeMonth = DateTime.Now.AddMonths(0 - i).ToString("yyyyMM");

                            String url1 = String.Format("http://{0}/Wechat/AgentBonusDetailQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(tempFeeMonth, QueryStringEncryption.key));

                            strBonusList = strBonusList + "<a href=\"" + url1 + "\">" + i + ":" + tempFeeMonth + "</a>";
                            strBonusList = strBonusList + "\n\n";
                        }
                        sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                        sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", strBonusList);
                        break;

                    case "preMonthBonus":
                    case "curMonthBonus":
                        String feeMonthBonus = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
                        if (actionType.Equals("preMonthBonus"))
                        {
                            feeMonthBonus = DateTime.Now.AddMonths(-2).ToString("yyyyMM");
                        }
                        AgentBonusDao agentBonusDao = new AgentBonusDao();
                        AgentBonus agentBonus = agentBonusDao.GetByKey(feeMonthBonus, agentNo);
                        if (agentBonus != null && !String.IsNullOrEmpty(agentBonus.agentNo))
                        {
                            sb.Append(this.createAgentBonusNewsMessages(feeMonthBonus, agentBonus, agentNo));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "本月无红包或者红包尚未发布!\n\n");
                        }
                        break;

                    case "Latest6MonthFeeQuery":
                        String strList = "最近6月佣金查询\n\n";
                        for (int i = 1; i <= 6; i++)
                        {
                            String tempFeeMonth = DateTime.Now.AddMonths(0 - i).ToString("yyyy-MM");

                            String url1 = String.Format("http://{0}/Wechat/AgentFeeQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(tempFeeMonth, QueryStringEncryption.key));

                            strList = strList + "<a href=\"" + url1 + "\">" + i + ":" + tempFeeMonth + "</a>";
                            strList = strList + "\n\n";
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
                        AgentFee agentFee = agentFeeDao.GetByKey(feeMonth, agentNo);
                        if (agentFee != null && !String.IsNullOrEmpty(agentFee.agentFeeSeq))
                        {
                            sb.Append(this.createAgentFeeNewsMessages(feeMonth, agentFee, agentNo));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "本月无佣金或者佣金尚未发布\n\n");
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

                            String url1 = String.Format("http://{0}/Wechat/InvoicePaymentQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(tempFeeMonth, QueryStringEncryption.key));

                            strList1 = strList1 + "<a href=\"" + url1 + "\">" + i + ":" + tempFeeMonth + "</a>";
                            strList1 = strList1 + "\n\n";
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
                        logger.Info("2.agentNo=" + agentNo);

                        agentInvoicePaymentList = agentInvoicePaymentDao.GetList(agentNo, null, feeMonth, null);

                        if (agentInvoicePaymentList != null && agentInvoicePaymentList.Count > 0)
                        {
                            sb.Append(this.createPaymentNewsMessages(feeMonth, agentNo, agentInvoicePaymentList));
                        }
                        else
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth.Substring(0, 4) + "年" + feeMonth.Substring(4, 2) + "月" + "无发票受理记录或尚未完成，请耐心等候!\n\n");
                        }
                        break;
                    default:

                        if (!Regex.IsMatch(wechatMessage.Content, "((20[0-9][0-9])|(19[0-9][0-9]))-((0[1-9])|(1[0-2]))"))
                        {
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "请输入\"yyyy-mm\"查询某月佣金，支付结算和红包,例如:\"" + DateTime.Now.ToString("yyyy-MM") + "\"查询" + DateTime.Now.ToString("yyyy年MM月") + "佣金\n\n");
                        }
                        else
                        {
                            feeMonth = wechatMessage.Content;
                            agentFeeDao = new AgentFeeDao();
                            agentFee = agentFeeDao.GetByKey(feeMonth, wechatMessage.FromUserName);

                             agentBonusDao = new AgentBonusDao();
                             feeMonthBonus = feeMonth.Replace("-", "");
                             agentBonus = agentBonusDao.GetByKey(feeMonthBonus, agentNo);

                             agentInvoicePaymentDao = new InvoicePaymentDao();
                            String feeMonthInvoice = feeMonth.Replace("-", "");
                             agentInvoicePaymentList = agentInvoicePaymentDao.GetList(agentNo, null, feeMonthInvoice, null);




                             sb.Append(this.createAllNewsMessages(feeMonth, wechatMessage.FromUserName, agentFee, agentBonus, agentInvoicePaymentList));

                          
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

        private StringBuilder createAllNewsMessages(String feeMonth, String agentNo, AgentFee agentFee, AgentBonus agentBonus, IList<InvoicePayment> agentInvoicePaymentList)
        {

            WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
            wechatQueryLog.agentName = "";
            wechatQueryLog.module = Util.MyConstant.module_Commission;
            wechatQueryLog.subSystem = "所有佣金查询";
            wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            wechatQueryLog.queryString = feeMonth;
            wechatQueryLog.wechatId = agentNo;
            WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
            try
            {
                wechatQueryLogDao.Add(wechatQueryLog);
            }
            catch
            {
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>1</ArticleCount>");
            sb.AppendFormat("<Articles>");

            sb.AppendFormat("<item>");
            sb.Append("<Title>").AppendFormat("{0}佣金、支付结算和红包结果", feeMonth).Append("</Title>");

            StringBuilder sbDesc = new StringBuilder();
            if (agentFee != null)
            {

                sbDesc.AppendFormat("告知单编号：{0}\n", agentFee.agentFeeSeq);
                sbDesc.AppendFormat("合作伙伴编号：{0}\n", agentFee.agentNo);
                sbDesc.AppendFormat("合作伙伴名字：{0}\n", agentFee.agentName);
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
                    if (agentTypeCommentList.Length > 0)
                    {
                        for (int count = 0; count < agentTypeCommentList.Length; count++)
                        {
                            sbDesc.Append("        ").Append(count + 1).AppendFormat(".{0}\n", agentTypeCommentList[count]);
                        }
                    }
                    else
                    {
                        sbDesc.Append("        无");
                    }
                }
               
            }
            else
            {
                sbDesc.AppendFormat("本月无佣金或者佣金尚未发布.\n");
            }
            sbDesc.AppendLine();
            ///支付结算
            if (agentInvoicePaymentList != null && agentInvoicePaymentList.Count > 0)
            {
                //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
                sbDesc.AppendFormat("总共处理了：{0}次支付结算信息\n", agentInvoicePaymentList.Count);
                foreach (InvoicePayment agentInvoicePayment in agentInvoicePaymentList)
                {
                    sbDesc.AppendFormat("\n处理时间：" + agentInvoicePayment.processTime + "\n发票金额：" + agentInvoicePayment.invoiceFee + "\n内容：" + agentInvoicePayment.content + "\n发票类型：" + agentInvoicePayment.invoiceType + "\n付款状态：" + agentInvoicePayment.payStatus).AppendLine();

                }
                
            }
            else
            {
                sbDesc.AppendFormat("没有发布支付结算信息.\n");
            }
            sbDesc.AppendLine();
            ///红包
            if (agentBonus != null)
            {
                sbDesc.AppendFormat("红包明细：\n");
                int i = 1;
                for (int j = 1; j <= 100; j++)
                {
                    FieldInfo feeNameField = agentBonus.GetType().GetField("feeName" + j);
                    FieldInfo feeField = agentBonus.GetType().GetField("fee" + j);
                    if (feeNameField != null && feeField != null)
                    {
                        String feeNameFieldValue = feeNameField.GetValue(agentBonus) == null ? null : feeNameField.GetValue(agentBonus).ToString();

                        String feeFieldValue = feeField.GetValue(agentBonus) == null ? null : feeField.GetValue(agentBonus).ToString();

                        if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                        {
                            sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                        }
                    }


                }
            }
            else
            {
                sbDesc.AppendFormat("没有发布红包信息.\n");
            }
           // sbDesc.AppendLine();

            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



            String url1 = String.Format("http://{0}/Wechat/AgentFeeQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }

        private StringBuilder createPaymentNewsMessages(String feeMonth, String agentNo, IList<InvoicePayment> agentInvoicePaymentList)
        {
            WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
            wechatQueryLog.agentName = "";
            wechatQueryLog.module = Util.MyConstant.module_Commission;
            wechatQueryLog.subSystem = "支付结算查询";
            wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            wechatQueryLog.queryString = feeMonth;
            wechatQueryLog.wechatId = agentNo;
            WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
            try
            {
                wechatQueryLogDao.Add(wechatQueryLog);
            }
            catch
            {
            }

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

            sbDesc.Append("\n此发票为" + feeMonth.Substring(0, 4) + "年" + feeMonth.Substring(4, 2) + "月" + "受理的发票支付记录，并非" + feeMonth.Substring(0, 4) + "年" + feeMonth.Substring(4, 2) + "月" + "佣金对应的发票").AppendLine();

            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



            String url1 = String.Format("http://{0}/Wechat/InvoicePaymentQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }
        private StringBuilder createAgentFeeNewsMessages(String feeMonth, AgentFee agentFee, String agentNo)
        {
            WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
            wechatQueryLog.agentName = "";
            wechatQueryLog.module = Util.MyConstant.module_Commission;
            wechatQueryLog.subSystem = "佣金查询";
            wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            wechatQueryLog.queryString = feeMonth;
            wechatQueryLog.wechatId = agentNo;
            WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
            try
            {
                wechatQueryLogDao.Add(wechatQueryLog);
            }
            catch
            {
            }

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
            sbDesc.AppendFormat("合作伙伴名字：{0}\n", agentFee.agentName);
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



            String url1 = String.Format("http://{0}/Wechat/AgentFeeQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }

        private StringBuilder createAgentBonusNewsMessages(String feeMonth, AgentBonus agentBonus, String agentNo)
        {
            WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
            wechatQueryLog.agentName = "";
            wechatQueryLog.module = Util.MyConstant.module_Commission;
            wechatQueryLog.subSystem = "红包查询";
            wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            wechatQueryLog.queryString = feeMonth;
            wechatQueryLog.wechatId = agentNo;
            WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
            try
            {
                wechatQueryLogDao.Add(wechatQueryLog);
            }
            catch
            {
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>1</ArticleCount>");
            sb.AppendFormat("<Articles>");

            sb.AppendFormat("<item>");
            sb.Append("<Title>").AppendFormat("{0}红包", feeMonth.Substring(0,4)+"年"+feeMonth.Substring(4,2)+"月").Append("</Title>");

            StringBuilder sbDesc = new StringBuilder();
            //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
          
            sbDesc.AppendFormat("代理商编号：{0}\n", agentBonus.agentNo);
            sbDesc.AppendFormat("代理商名字：{0}\n\n", agentBonus.agentName);
           

            // sb1.AppendFormat("佣金\n\n");

            sbDesc.AppendFormat("红包明细：\n");
            int i = 1;
            for (int j = 1; j <= 100; j++)
            {
                FieldInfo feeNameField = agentBonus.GetType().GetField("feeName" + j);
                FieldInfo feeField = agentBonus.GetType().GetField("fee" + j);
                if (feeNameField != null && feeField != null)
                {
                    String feeNameFieldValue = feeNameField.GetValue(agentBonus) == null ? null : feeNameField.GetValue(agentBonus).ToString();

                    String feeFieldValue = feeField.GetValue(agentBonus) == null ? null : feeField.GetValue(agentBonus).ToString();

                    if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                    }
                }


            }
            //sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", "佣金总计").Append(" ").AppendFormat("{0}\n", agentBonus.feeTotal);
            //sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", "过网开票金额").Append(" ").AppendFormat("{0}\n", agentBonus.preInvoiceFee);




           
            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



            String url1 = String.Format("http://{0}/Wechat/AgentBonusDetailQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }

    }
}