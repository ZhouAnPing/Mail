using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinaUnion_Agent.Wechat
{
    class WechatAction
    {
        public WechatUser getUserFromWechat()
        {
            WechatUser wechatUser = null;
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(Settings.Default.Wechat_Corpid, Settings.Default.Wechat_Secret);

            string getUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child=1&status=0";

            var addUserUrl = string.Format(getUserUrlFormat, accessToken, 1);
            
            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = addUserUrl,
                Method = "get"//URL     可选项 默认为Get

            };

            HttpResult result = httpHelper.GetHtml(item);

            //返回的Html内容
            string html = result.Html;


            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                wechatUser = JsonConvert.DeserializeObject<WechatUser>(html);
            }
            //表示StatusCode的文字说明与描述
            string statusCodeDescription = result.StatusDescription;
            return wechatUser;
        }

        public HttpResult sendMessageToWechat(String content)
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(Settings.Default.Wechat_Corpid, Settings.Default.Wechat_Secret);

            var msgUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", accessToken);


            var msgData = new
            {

                touser = "@all",
                msgtype = "text",
                agentid = 1,
                safe = 0,
                text = new
                {

                    content = content
                }
            };

            string msgJson = JsonConvert.SerializeObject(msgData, Formatting.Indented);



            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                URL = msgUrl,
                Method = "post",//URL     可选项 默认为Get
                Postdata = msgJson,
                PostEncoding = Encoding.GetEncoding("UTF-8")//可以发送中文消息了，开心

            };

            HttpResult result = httpHelper.GetHtml(item);

            return result;
        }
    }
}
