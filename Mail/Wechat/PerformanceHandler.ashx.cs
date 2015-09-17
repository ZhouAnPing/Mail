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

            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            AgentWechatAccount agentWechatAccount = agentWechatAccountDao.Get(wechatMessage.FromUserName);
            if (agentWechatAccount != null && wechatMessage != null && !String.IsNullOrEmpty(wechatMessage.Event) && wechatMessage.Event.Equals("enter_agent"))
            {
                WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                wechatQueryLog.agentName = "";
                wechatQueryLog.subSystem = "业绩查询";
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
                String agentType = agentWechatAccount.type;

                AgentMonthPerformanceDao agentMonthPerformanceDao = new ChinaUnion_DataAccess.AgentMonthPerformanceDao();
                AgentDailyPerformanceDao agentDailyPerformanceDao = new ChinaUnion_DataAccess.AgentDailyPerformanceDao();
                AgentStarDao agentStarDao = new AgentStarDao();
                IList<AgentStar> agentStarList = null;

                AgentScoreDao agentScoreDao = new AgentScoreDao();
                IList<AgentScore> agentScoreList = null;
                String dateTime = "";

                DateTime dt = DateTime.Now;  //当前时间
                DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初
                if (startQuarter.Month >= 1 && startQuarter.Month <= 3)
                {
                    dateTime = startQuarter.Year + "年第一季度";
                }
                if (startQuarter.Month >= 4 && startQuarter.Month <= 6)
                {
                    dateTime = startQuarter.Year + "年第二季度";
                }
                if (startQuarter.Month >= 7 && startQuarter.Month <= 9)
                {
                    dateTime = startQuarter.Year + "年第三季度";
                }
                if (startQuarter.Month >= 10 && startQuarter.Month <= 12)
                {
                    dateTime = startQuarter.Year + "年第四季度";
                }

                logger.Info("agentNo: " + agentNo);
                logger.Info("agentType: " + agentType);
                switch (actionType)
                {

                    case "curQuaterStar":
                    case "HistoryQuaterStar":
                        if (actionType.Equals("curQuaterStar"))
                        {
                            agentStarList = agentStarDao.GetLatestByKeyword(agentNo, dateTime);
                        }
                        if (actionType.Equals("HistoryQuaterStar"))
                        {
                            agentStarList = agentStarDao.GetListByKeyword(agentNo);
                        }

                      if (agentStarList != null && agentStarList.Count > 0)
                        {
                            logger.Info("Exist Record: " + agentStarList.Count);
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");

                            StringBuilder sbContent = new StringBuilder();
                            sbContent.AppendFormat("星级查询详情").Append("\n");
                            for (int i = 0; i < agentStarList.Count;i++ )
                            {
                                AgentStar agentStar = agentStarList[i];
                                sbContent.AppendFormat("\n时间:{0}", agentStar.dateTime).Append("\n");
                               // sbContent.AppendFormat("代理商编号:{0}", agentStar.agentNo).Append("\n");
                                //sbContent.AppendFormat("代理商名称:{0}", agentStar.agentName).Append("\n");
                                if (!String.IsNullOrEmpty(agentStar.branchNo))
                                {
                                    sbContent.AppendFormat("渠道编码:{0}", agentStar.branchNo).Append("\n");
                                    sbContent.AppendFormat("渠道名称:{0}", agentStar.branchName).Append("\n");
                                }
                               
                                sbContent.AppendFormat("星级:{0}", agentStar.star).Append("\n");

                            }
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", sbContent.ToString());
                            // sb.Append(sbContent.ToString());
                            // sb.Append(this.createNewsMessages(feeDate, wechatMessage.FromUserName, agentDailyPerformance));
                        }
                        else
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "星级还没上传，请直接与上海联通确认!\n\n");
                        }

                        WechatQueryLog wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                        wechatQueryLog.agentName = "";
                        wechatQueryLog.subSystem = "星级查询";
                        wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        wechatQueryLog.queryString = dateTime;
                        wechatQueryLog.wechatId = agentNo;
                        WechatQueryLogDao wechatQueryLogDao = new WechatQueryLogDao();
                        wechatQueryLogDao.Add(wechatQueryLog);

                        break;

                    case "curScore":
                    case "HistoryScore":
                        

                        String month = DateTime.Now.ToString("yyyyMM");
                        if (actionType.Equals("curScore"))
                        {
                            agentScoreList = agentScoreDao.GetLatestByKeyword(agentNo, month);
                        }
                        if (actionType.Equals("HistoryScore"))
                        {
                            agentScoreList = agentScoreDao.GetListByKeyword(agentNo);
                        }

                        if (agentScoreList != null && agentScoreList.Count > 0)
                        {
                            logger.Info("Exist Record: " + agentScoreList.Count);
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");

                            StringBuilder sbContent = new StringBuilder();
                            sbContent.AppendFormat("积分查询详情").Append("\n");
                            for (int i = 0; i < agentScoreList.Count; i++)
                            {
                                AgentScore agentScore = agentScoreList[i];
                                sbContent.AppendFormat("\n时间:{0}", agentScore.dateTime).Append("\n");

                                sbContent.AppendFormat("代理商编号:{0}", agentScore.agentNo).Append("\n");
                                sbContent.AppendFormat("代理商名称:{0}", agentScore.agentName).Append("\n");
                                if (!String.IsNullOrEmpty(agentScore.branchNo))
                                {
                                    sbContent.AppendFormat("渠道编码:{0}", agentScore.branchNo).Append("\n");
                                    sbContent.AppendFormat("渠道名称:{0}", agentScore.branchName).Append("\n");
                                }

                                sbContent.AppendFormat("渠道积分:{0}", agentScore.score).Append("\n");
                                sbContent.AppendFormat("本月得分:{0}", agentScore.standardScore).Append("\n");

                            }
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", sbContent.ToString());
                            // sb.Append(sbContent.ToString());
                            // sb.Append(this.createNewsMessages(feeDate, wechatMessage.FromUserName, agentDailyPerformance));
                        }
                        else
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>",  "积分还没上传，请直接与上海联通确认!\n\n");
                        }

                         wechatQueryLog = new ChinaUnion_BO.WechatQueryLog();
                        wechatQueryLog.agentName = "";
                        wechatQueryLog.subSystem = "积分查询";
                        wechatQueryLog.queryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        wechatQueryLog.queryString = month;
                        wechatQueryLog.wechatId = agentNo;
                         wechatQueryLogDao = new WechatQueryLogDao();
                        wechatQueryLogDao.Add(wechatQueryLog);

                        break;

                    case "YesterdayPerformance":
                        String feeDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                        AgentDailyPerformance agentDailyPerformance = new AgentDailyPerformance();

                        agentDailyPerformance = agentDailyPerformanceDao.GetSummary(agentNo, feeDate,agentType);

                        if (agentDailyPerformance != null)
                        {
                            logger.Info("Exist Record: " + agentNo);
                            sb.Append(this.createNewsMessages(feeDate, agentNo, agentDailyPerformance, agentType));
                        }
                        else
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeDate + "业绩还未发布，请稍后!\n\n");
                        }
                        break;

                    case "HistoryDayPerformance":

                        IList<AgentDailyPerformance> agentDailyPerformanceList = agentDailyPerformanceDao.GetAllListDate(agentNo, agentType);
                        if (agentDailyPerformanceList == null || agentDailyPerformanceList.Count == 0)
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "近日业绩还未发布，请稍后!\n\n");
                        }
                        else
                        {
                            sb.Append(this.createNewsMessages(agentNo, agentDailyPerformanceList, agentType));
                        }

                        break;


                    case "LastMonthPerformance":
                        // case "YesterdayPerformance":
                        String feeMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
                        AgentMonthPerformance agentMonthPerformance = new AgentMonthPerformance();

                        agentMonthPerformance = agentMonthPerformanceDao.GetSummary(agentNo, feeMonth, agentType);

                        if (agentMonthPerformance != null)
                        {
                            logger.Info("Exist Record: " + agentMonthPerformance.agentName);
                            sb.Append(this.createNewsMessages(feeMonth, agentNo, agentMonthPerformance, agentType));
                        }
                        else
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", feeMonth + "业绩还未发布，请稍后!\n\n");
                        }
                        break;

                    case "HistoryMonthPerformance":

                        IList<AgentMonthPerformance> agentMonthPerformanceList = agentMonthPerformanceDao.GetAllListMonth(agentNo,agentType);
                        if (agentMonthPerformanceList == null || agentMonthPerformanceList.Count == 0)
                        {
                            logger.Info("is not Existed Record: ");
                            sb.AppendFormat("<MsgType><![CDATA[text]]></MsgType>");
                            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", "近期业绩还未发布，请稍后!\n\n");
                        }
                        else
                        {
                            sb.Append(this.createNewsMessages(agentNo, agentMonthPerformanceList, agentType));
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


        private StringBuilder createNewsMessages(String agentNo, IList<AgentDailyPerformance> agentPerformanceList, String type)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", agentPerformanceList.Count);
            sb.AppendFormat("<Articles>");
            for (int month = 0;month<10 && month < agentPerformanceList.Count; month++)
            {
                AgentDailyPerformance agentPerformance = agentPerformanceList[month];

                sb.AppendFormat("<item>");
                sb.Append("<Title>").AppendFormat("{0}日业绩", agentPerformance.date).Append("</Title>");

                StringBuilder sbDesc = new StringBuilder();


                if (month == 0)
                {
                    AgentDailyPerformanceDao agentPerformanceDao = new ChinaUnion_DataAccess.AgentDailyPerformanceDao();
                    agentPerformance = agentPerformanceDao.GetSummary(agentNo, agentPerformance.date,type);
                    if (!String.IsNullOrEmpty(agentPerformance.agentNo))
                    {

                        sbDesc.AppendFormat("代理商编号:" + agentPerformance.agentNo + "\n代理商名称:" + agentPerformance.agentName).Append("\n");
                    }
                    if (!String.IsNullOrEmpty(agentPerformance.branchNo))
                    {

                        sbDesc.AppendFormat("渠道编号:" + agentPerformance.branchNo + "\n渠道名称:" + agentPerformance.branchName).Append("\n");
                    }

                    sbDesc.AppendLine().AppendFormat("\n业绩汇总明细：\n");
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
                                if (feeNameFieldValue.Contains("后付费发展数") || feeNameFieldValue.Contains("预付费发展数") || feeNameFieldValue.Contains("总计"))
                                {
                                    sbDesc.Append("  ").AppendFormat("{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                                }
                                else
                                {

                                    sbDesc.Append("  ").AppendFormat("     {0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);
                                }
                            }
                        }


                    }

                    sbDesc.AppendFormat("\n查询时间：{0}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");
                }

                sb.Append("<PicUrl>").AppendFormat("<![CDATA[]]>").Append("</PicUrl>");
               String  tempType = System.Web.HttpUtility.UrlEncode(type);
               String url1 = String.Format("http://{0}/Wechat/PerformanceDailySummaryQuery.aspx?agentNo={1}&feeDate={2}&type={3}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(agentPerformance.date, QueryStringEncryption.key), QueryStringEncryption.Encode(tempType, QueryStringEncryption.key));
                logger.Info(url1);
                sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                sb.AppendFormat("</item>");

            }

            sb.AppendFormat("</Articles>");
            return sb;
        }

        private StringBuilder createNewsMessages(String feeDate, String agentNo, AgentDailyPerformance agentDailyPerformance, String type)
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


            if (!String.IsNullOrEmpty(agentDailyPerformance.agentNo))
            {

                sbDesc.AppendFormat("代理商编号:" + agentDailyPerformance.agentNo + "\n代理商名称:" + agentDailyPerformance.agentName).Append("\n");
            }
            if (!String.IsNullOrEmpty(agentDailyPerformance.branchNo))
            {

                sbDesc.AppendFormat("渠道编号:" + agentDailyPerformance.branchNo + "\n渠道名称:" + agentDailyPerformance.branchName).Append("\n");
            }

            sbDesc.AppendLine().AppendFormat("\n业绩汇总明细：\n");
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
                       // sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                        if (feeNameFieldValue.Contains("后付费发展数") || feeNameFieldValue.Contains("预付费发展数") || feeNameFieldValue.Contains("总计"))
                        {
                            sbDesc.Append("  ").AppendFormat("{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                        }
                        else
                        {

                            sbDesc.Append("  ").AppendFormat("     {0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);
                        }
                    }
                }


            }

            sbDesc.AppendFormat("\n查询时间：{0}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");


            String tempType = System.Web.HttpUtility.UrlEncode(type);
            String url1 = String.Format("http://{0}/Wechat/PerformanceDailySummaryQuery.aspx?agentNo={1}&feeDate={2}&type={3}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(feeDate, QueryStringEncryption.key), QueryStringEncryption.Encode(tempType, QueryStringEncryption.key));
            logger.Info(url1);
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }


        private StringBuilder createNewsMessages(String agentNo, IList<AgentMonthPerformance> agentMonthPerformanceList, String type)
        {
          
            //type = UrlEncode
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<MsgType><![CDATA[news]]></MsgType>");
            sb.AppendFormat("<ArticleCount>{0}</ArticleCount>", agentMonthPerformanceList.Count);
            sb.AppendFormat("<Articles>");
            for (int month = 0; month < 10 && month < agentMonthPerformanceList.Count; month++)
            {
                AgentMonthPerformance agentMonthPerformance = agentMonthPerformanceList[month];

                sb.AppendFormat("<item>");
                sb.Append("<Title>").AppendFormat("{0}月业绩", agentMonthPerformance.month).Append("</Title>");

                StringBuilder sbDesc = new StringBuilder();


                if (month == 0)
                {
                    AgentMonthPerformanceDao agentMonthPerformanceDao = new ChinaUnion_DataAccess.AgentMonthPerformanceDao();
                    agentMonthPerformance = agentMonthPerformanceDao.GetSummary(agentNo, agentMonthPerformance.month,type);

                    if (!String.IsNullOrEmpty(agentMonthPerformance.agentNo))
                    {

                        sbDesc.AppendFormat("代理商编号:" + agentMonthPerformance.agentNo + "\n代理商名称:" + agentMonthPerformance.agentName).Append("\n");
                    }
                    if (!String.IsNullOrEmpty(agentMonthPerformance.branchNo))
                    {

                        sbDesc.AppendFormat("渠道编号:" + agentMonthPerformance.branchNo + "\n渠道名称:" + agentMonthPerformance.branchName).Append("\n");
                    }

                    sbDesc.AppendLine().AppendFormat("\n业绩汇总明细：\n");
                    //int i = 1;
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
                               // sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);
                                if (feeNameFieldValue.Contains("后付费发展数") || feeNameFieldValue.Contains("预付费发展数") || feeNameFieldValue.Contains("总计"))
                                {
                                    sbDesc.Append("  ").AppendFormat("{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                                }
                                else
                                {

                                    sbDesc.Append("  ").AppendFormat("     {0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);
                                }
                            }
                        }


                    }

                    sbDesc.AppendFormat("\n查询时间：{0}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");
                }

                sb.Append("<PicUrl>").AppendFormat("<![CDATA[]]>").Append("</PicUrl>");
                String tempType = System.Web.HttpUtility.UrlEncode(type);
                String url1 = String.Format("http://{0}/Wechat/PerformanceMonthSummaryQuery.aspx?agentNo={1}&feeMonth={2}&type={3}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(agentMonthPerformance.month, QueryStringEncryption.key), QueryStringEncryption.Encode(tempType, QueryStringEncryption.key));
                logger.Info(url1);
               
                sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
                sb.AppendFormat("</item>");

            }

            sb.AppendFormat("</Articles>");
            return sb;
        }

        private StringBuilder createNewsMessages(String feeMonth, String agentNo, AgentMonthPerformance agentMonthPerformance, String type)
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
            if (!String.IsNullOrEmpty(agentMonthPerformance.agentNo))
            {

                sbDesc.AppendFormat("代理商编号:" + agentMonthPerformance.agentNo + "\n代理商名称:" + agentMonthPerformance.agentName).Append("\n");
            }
            if (!String.IsNullOrEmpty(agentMonthPerformance.branchNo))
            {

                sbDesc.AppendFormat("渠道编号:" + agentMonthPerformance.branchNo + "\n渠道名称:" + agentMonthPerformance.branchName).Append("\n");
            }

            sbDesc.AppendLine().AppendFormat("\n业绩汇总明细：\n");
           
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
                       // sbDesc.Append("  ").Append(i++).AppendFormat(".{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);
                        if (feeNameFieldValue.Contains("后付费发展数") || feeNameFieldValue.Contains("预付费发展数") || feeNameFieldValue.Contains("总计"))
                        {
                            sbDesc.Append("  ").AppendFormat("{0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);

                        }
                        else
                        {

                            sbDesc.Append("  ").AppendFormat("     {0}", feeNameFieldValue).Append(" ").AppendFormat("{0}\n", feeFieldValue);
                        }
                    }
                }


            }

            sbDesc.AppendFormat("\n查询时间：{0}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            sb.Append("<Description>").AppendFormat("<![CDATA[{0}]]>", sbDesc.ToString()).Append("</Description>");


            String tempType = System.Web.HttpUtility.UrlEncode(type);
            String url1 = String.Format("http://{0}/Wechat/PerformanceMonthSummaryQuery.aspx?agentNo={1}&feeMonth={2}&type={3}", Properties.Settings.Default.Host, QueryStringEncryption.Encode(agentNo, QueryStringEncryption.key), QueryStringEncryption.Encode(feeMonth, QueryStringEncryption.key), QueryStringEncryption.Encode(tempType, QueryStringEncryption.key));
            logger.Info(url1);
            
            sb.Append("<Url>").AppendFormat("<![CDATA[{0}]]>", url1).Append("</Url>");
            sb.AppendFormat("</item>");

            sb.AppendFormat("</Articles>");
            return sb;
        }

    }
}