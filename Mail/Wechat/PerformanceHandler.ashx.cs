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
    /// Summary description for PerformanceHandler
    /// </summary>
    public class PerformanceHandler : IHttpHandler
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PerformanceHandler));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {


            logger.Info(context.Request.Url.AbsoluteUri);


            string sToken = "PerformanceHandler";
            string sCorpID = Properties.Settings.Default.Wechat_CorpId;// "wx4fe8b74e01fffcbb";
            string sEncodingAESKey = "U7gOrkwP22ND4bIHSxU0WJqIestRcG2QroykyVKDUSG";

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

            agentNo = "P001";
            AgentDao agentDao = new AgentDao();
            Agent agent = agentDao.Get(agentNo);

            if (agent != null && !String.IsNullOrEmpty(agent.status) && agent.status.Equals("Y"))
            {
                sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "对不起，你的账号已被停用，请联系联通工作人员。。。\n\n");

            }
            else
            {
                AgentMonthPerformanceDao agentMonthPerformanceDao = new ChinaUnion_DataAccess.AgentMonthPerformanceDao();
                AgentDailyPerformanceDao agentDailyPerformanceDao = new ChinaUnion_DataAccess.AgentDailyPerformanceDao();
                switch (actionType)
                {
                    case "YesterdayPerformance":
                        String feeDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                        AgentDailyPerformance agentDailyPerformance = new AgentDailyPerformance();

                        agentDailyPerformance = agentDailyPerformanceDao.GetSummary(agentNo, feeDate);

                        if (agentDailyPerformance != null)
                        {
                            logger.Info("Exist Record: " + agentDailyPerformance.agentName);
                            sb.Append(this.createNewsMessages(feeDate, wechatMessage.FromUserName, agentDailyPerformance));
                        }
                        else
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeDate + "业绩还未发布，请稍后。。。\n\n");
                        }
                        break;

                    case "HistoryDayPerformance":

                        IList<AgentDailyPerformance> agentDailyPerformanceList = agentDailyPerformanceDao.GetAllListDate(agentNo);
                        if (agentDailyPerformanceList == null || agentDailyPerformanceList.Count == 0)
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "近日业绩还未发布，请稍后。。。\n\n");
                        }
                        else
                        {
                            sb.Append(this.createNewsMessages(wechatMessage.FromUserName, agentDailyPerformanceList));
                        }

                        break;


                    case "LastMonthPerformance":
                        // case "YesterdayPerformance":
                        String feeMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
                        AgentMonthPerformance agentMonthPerformance = new AgentMonthPerformance();

                        agentMonthPerformance = agentMonthPerformanceDao.GetSummary(agentNo, feeMonth);

                        if (agentMonthPerformance != null)
                        {
                            logger.Info("Exist Record: " + agentMonthPerformance.agentName);
                            sb.Append(this.createNewsMessages(feeMonth, wechatMessage.FromUserName, agentMonthPerformance));
                        }
                        else
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "业绩还未发布，请稍后。。。\n\n");
                        }
                        break;

                    case "HistoryMonthPerformance":

                        IList<AgentMonthPerformance> agentMonthPerformanceList = agentMonthPerformanceDao.GetAllListMonth(agentNo);
                        if (agentMonthPerformanceList == null || agentMonthPerformanceList.Count == 0)
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "近期业绩还未发布，请稍后。。。\n\n");
                        }
                        else
                        {
                            sb.Append(this.createNewsMessages(wechatMessage.FromUserName, agentMonthPerformanceList));
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


        private StringBuilder createNewsMessages(String toUser, IList<AgentDailyPerformance> agentPerformanceList)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", agentPerformanceList.Count);
            sb.AppendFormat("<Articles>");
            for (int month = 0;month<10 && month < agentPerformanceList.Count; month++)
            {
                AgentDailyPerformance agentPerformance = agentPerformanceList[month];

                sb.AppendFormat("<item>");
                sb.Append("<Title>").AppendFormat("{0}日业绩查询结果", agentPerformance.date).Append("</Title>");

                StringBuilder sbDesc = new StringBuilder();


                if (month == 0)
                {
                    AgentDailyPerformanceDao agentPerformanceDao = new ChinaUnion_DataAccess.AgentDailyPerformanceDao();
                    agentPerformance = agentPerformanceDao.GetSummary(agentPerformance.agentNo, agentPerformance.date);
                    sbDesc.AppendFormat("代理商编号:" + agentPerformance.agentNo + "\n代理商名称:" + agentPerformance.agentName).AppendLine();


                    sbDesc.AppendFormat("\n业绩汇总明细：\n");
                    int i = 1;
                    for (int j = 1; j <= 100; j++)
                    {
                        FieldInfo feeNameField = agentPerformance.GetType().GetField("feeName" + j);
                        FieldInfo feeField = agentPerformance.GetType().GetField("fee" + j);
                        if (feeNameField != null && feeField != null)
                        {
                            String feeNameFieldValue = feeNameField.GetValue(agentPerformance) == null ? null : feeNameField.GetValue(agentPerformance).ToString();

                            String feeFieldValue = feeField.GetValue(agentPerformance) == null ? null : feeField.GetValue(agentPerformance).ToString();

                            if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                            {
                                sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                            }
                        }


                    }

                    sbDesc.AppendFormat("\n查询时间：{0}\n", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                    sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");
                }

                sb.Append("<PicUrl>").AppendFormat("<![CDATA[]]>").Append("</PicUrl>");

                String url1 = String.Format("http://{0}/Wechat/PerformanceDailySummaryQuery.aspx?agentNo={1}&feeDate={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(toUser, QueryStringEncryption.key), QueryStringEncryption.Encode(agentPerformance.date, QueryStringEncryption.key));
                logger.Info(url1);
                sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                sb.AppendFormat("</item>");

            }

            sb.AppendFormat("</Articles>");
            return sb;
        }

        private StringBuilder createNewsMessages(String feeDate, String toUser, AgentDailyPerformance agentDailyPerformance)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>1</ArticleCount>");
            sb.AppendFormat("<Articles>");

            sb.AppendFormat("<item>");
            sb.Append("<Title>").AppendFormat("{0}日业绩详情", feeDate).Append("</Title>");

            StringBuilder sbDesc = new StringBuilder();
            //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
            // sbDesc.AppendFormat("总共处理了：{0}次发票信息\n", agentMonthPerformanceList.Count);


            sbDesc.AppendFormat("代理商编号:" + agentDailyPerformance.agentNo + "\n代理商名称:" + agentDailyPerformance.agentName).AppendLine();


            sbDesc.AppendFormat("\n业绩汇总明细：\n");
            int i = 1;
            for (int j = 1; j <= 100; j++)
            {
                FieldInfo feeNameField = agentDailyPerformance.GetType().GetField("feeName" + j);
                FieldInfo feeField = agentDailyPerformance.GetType().GetField("fee" + j);
                if (feeNameField != null && feeField != null)
                {
                    String feeNameFieldValue = feeNameField.GetValue(agentDailyPerformance) == null ? null : feeNameField.GetValue(agentDailyPerformance).ToString();

                    String feeFieldValue = feeField.GetValue(agentDailyPerformance) == null ? null : feeField.GetValue(agentDailyPerformance).ToString();

                    if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                    }
                }


            }

            sbDesc.AppendFormat("\n查询时间：{0}\n", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



            String url1 = String.Format("http://{0}/Wechat/PerformanceDailySummaryQuery.aspx?agentNo={1}&feeDate={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(toUser, QueryStringEncryption.key), QueryStringEncryption.Encode(feeDate, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }


        private StringBuilder createNewsMessages(String toUser, IList<AgentMonthPerformance> agentMonthPerformanceList)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", agentMonthPerformanceList.Count);
            sb.AppendFormat("<Articles>");
            for (int month = 0; month < 10 && month < agentMonthPerformanceList.Count; month++)
            {
                AgentMonthPerformance agentMonthPerformance = agentMonthPerformanceList[month];

                sb.AppendFormat("<item>");
                sb.Append("<Title>").AppendFormat("{0}月业绩查询结果", agentMonthPerformance.month).Append("</Title>");

                StringBuilder sbDesc = new StringBuilder();


                if (month == 0)
                {
                    AgentMonthPerformanceDao agentMonthPerformanceDao = new ChinaUnion_DataAccess.AgentMonthPerformanceDao();
                    agentMonthPerformance = agentMonthPerformanceDao.GetSummary(agentMonthPerformance.agentNo, agentMonthPerformance.month);
                    sbDesc.AppendFormat("代理商编号:" + agentMonthPerformance.agentNo + "\n代理商名称:" + agentMonthPerformance.agentName).AppendLine();


                    sbDesc.AppendFormat("\n业绩汇总明细：\n");
                    int i = 1;
                    for (int j = 1; j <= 100; j++)
                    {
                        FieldInfo feeNameField = agentMonthPerformance.GetType().GetField("feeName" + j);
                        FieldInfo feeField = agentMonthPerformance.GetType().GetField("fee" + j);
                        if (feeNameField != null && feeField != null)
                        {
                            String feeNameFieldValue = feeNameField.GetValue(agentMonthPerformance) == null ? null : feeNameField.GetValue(agentMonthPerformance).ToString();

                            String feeFieldValue = feeField.GetValue(agentMonthPerformance) == null ? null : feeField.GetValue(agentMonthPerformance).ToString();

                            if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                            {
                                sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                            }
                        }


                    }

                    sbDesc.AppendFormat("\n查询时间：{0}\n", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                    sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");
                }

                sb.Append("<PicUrl>").AppendFormat("<![CDATA[]]>").Append("</PicUrl>");

                String url1 = String.Format("http://{0}/Wechat/PerformanceMonthSummaryQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(toUser, QueryStringEncryption.key), QueryStringEncryption.Encode(agentMonthPerformance.month, QueryStringEncryption.key));
                logger.Info(url1);
                sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                sb.AppendFormat("</item>");

            }

            sb.AppendFormat("</Articles>");
            return sb;
        }

        private StringBuilder createNewsMessages(String feeMonth, String toUser, AgentMonthPerformance agentMonthPerformance)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>1</ArticleCount>");
            sb.AppendFormat("<Articles>");

            sb.AppendFormat("<item>");
            sb.Append("<Title>").AppendFormat("{0}月业绩详情", feeMonth).Append("</Title>");

            StringBuilder sbDesc = new StringBuilder();
            //sbDesc.AppendFormat("本月佣金告知单({0})", feeMonth);
            // sbDesc.AppendFormat("总共处理了：{0}次发票信息\n", agentMonthPerformanceList.Count);


            sbDesc.AppendFormat("代理商编号:" + agentMonthPerformance.agentNo + "\n代理商名称:" + agentMonthPerformance.agentName).AppendLine();


            sbDesc.AppendFormat("\n业绩汇总明细：\n");
            int i = 1;
            for (int j = 1; j <= 100; j++)
            {
                FieldInfo feeNameField = agentMonthPerformance.GetType().GetField("feeName" + j);
                FieldInfo feeField = agentMonthPerformance.GetType().GetField("fee" + j);
                if (feeNameField != null && feeField != null)
                {
                    String feeNameFieldValue = feeNameField.GetValue(agentMonthPerformance) == null ? null : feeNameField.GetValue(agentMonthPerformance).ToString();

                    String feeFieldValue = feeField.GetValue(agentMonthPerformance) == null ? null : feeField.GetValue(agentMonthPerformance).ToString();

                    if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                    }
                }


            }

            sbDesc.AppendFormat("\n查询时间：{0}\n", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");



            String url1 = String.Format("http://{0}/Wechat/PerformanceMonthSummaryQuery.aspx?agentNo={1}&feeMonth={2}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(toUser, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }

    }
}