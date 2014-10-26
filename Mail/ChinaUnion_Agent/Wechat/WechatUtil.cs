using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WHC.OrderWater.Commons;

namespace ChinaUnion_Agent.Wechat
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
    }
}
