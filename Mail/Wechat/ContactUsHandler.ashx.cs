using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wechat
{
    /// <summary>
    /// Summary description for ContactUsHandler
    /// </summary>
    public class ContactUsHandler : IHttpHandler
    {

        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ContactUsHandler));
        /// <summary>
        /// 处理企业号的信息
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //公众平台上开发者设置的token, corpID, EncodingAESKey
            string sToken = "ContactUsHandler";
            string sCorpID = "wx31204de5a3ae758e";
            string sEncodingAESKey = "Mh5NDLXhiRMu1GHitnufvEZKBdSh6c1zvOZbpdUOT2T";

            logger.Info(context.Request.Url.AbsoluteUri);
            /*
            ------------使用示例一：验证回调URL---------------
            *企业开启回调模式时，企业号会向验证url发送一个get请求 
            假设点击验证时，企业收到类似请求：
            * GET /cgi-bin/wxpush?msg_signature=5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3&timestamp=1409659589&nonce=263014780&echostr=P9nAzCzyDtyTWESHep1vC5X9xho%2FqYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp%2B4RPcs8TgAE7OaBO%2BFZXvnaqQ%3D%3D 
            * HTTP/1.1 Host: qy.weixin.qq.com

            * 接收到该请求时，企业应			1.解析出Get请求的参数，包括消息体签名(msg_signature)，时间戳(timestamp)，随机数字串(nonce)以及公众平台推送过来的随机加密字符串(echostr),
            这一步注意作URL解码。
            2.验证消息体签名的正确性 
            3.解密出echostr原文，将原文当作Get请求的response，返回给公众平台
            第2，3步可以用公众平台提供的库函数VerifyURL来实现。
            */


            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            System.Collections.Specialized.NameValueCollection queryStrings = context.Request.QueryString;

            context.Request.ContentEncoding = System.Text.Encoding.UTF8;
            string sVerifyMsgSig = queryStrings["msg_signature"];
            string sVerifyTimeStamp = queryStrings["timestamp"];
            string sVerifyNonce = queryStrings["nonce"];
            string sVerifyEchoStr = queryStrings["echostr"];
            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            logger.Info("sVerifyMsgSig=" + sVerifyMsgSig);
            logger.Info("sVerifyTimeStamp=" + sVerifyTimeStamp);
            logger.Info("sVerifyNonce=" + sVerifyNonce);
            logger.Info("sVerifyEchoStr=" + sVerifyEchoStr);
            logger.Info("sEchoStr=" + sEchoStr);
            logger.Info("ret=" + ret);
            if (ret != 0)
            {

                logger.Info("ERR: VerifyURL fail, ret: " + ret);
                System.Console.WriteLine("ERR: VerifyURL fail, ret: " + ret);
                return;
            }

            //ret==0表示验证成功，sEchoStr参数表示明文，用户需要将sEchoStr作为get请求的返回参数，返回给企业号。
            context.Response.Write(sEchoStr);
            // HttpUtils.SetResponse(sEchoStr);
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