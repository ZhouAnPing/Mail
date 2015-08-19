using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHC.OrderWater.Commons;

namespace Wechat.Util
{
    class WechatUtil
    {

        /// <summary>
        /// 获取每次操作微信API的Token访问令牌
        /// </summary>
        /// <param name="corpid">企业Id</param>
        /// <param name="corpsecret">管理组的凭证密钥</param>
        /// <returns></returns>
        public string GetAccessTokenNoCache(string corpid, string corpsecret)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}",
                                    corpid, corpsecret);

            HttpHelper helper = new HttpHelper();
            string result = helper.GetHtml(url);
            string regex = "\"access_token\":\"(?<token>.*?)\"";

            string token = CRegex.GetText(result, regex, "token");
            return token;
        }

        public HttpResult getUserInfoFromWechat(String code, String agentId, String secret)
        {
            String corpId = Properties.Settings.Default.Wechat_CorpId;

           
            String accessToken = this.GetAccessTokenNoCache(corpId, secret);

            string getUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}&agentid={2}";
            var getUserUrl = string.Format(getUserUrlFormat, accessToken, code, agentId);



            Wechat.Util.HttpHelper httpHelper = new Wechat.Util.HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),
                URL = getUserUrl,
                Method = "get"//URL     可选项 默认为Get

            };

            HttpResult result = httpHelper.GetHtml(item);

            //返回的Html内容
            string html = result.Html;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
            }
            //表示StatusCode的文字说明与描述
            string statusCodeDescription = result.StatusDescription;

            return result;
        }
    }
}
