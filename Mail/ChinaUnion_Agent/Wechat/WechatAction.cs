using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChinaUnion_Agent.Wechat
{
    class WechatAction
    {
       //String corpId ="wx4fe8b74e01fffcbb";// Settings.Default.Wechat_Corpid;
      String corpId = Settings.Default.Wechat_Corpid;
        public HttpResult addUserToWechat(String secret, string userJson)
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string addUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";
            var addUserUrl = string.Format(addUserUrlFormat, accessToken);

            //var userData = new
            //{
            //    userid = "viviwu",
            //    name = "Vivi",
            //    department = 1,
            //    position = "Manager",
            //    mobile = "18621180524",
            //    gender = 1,
            //    tel = "18621180524",
            //    email = "vivi.wu@yourzine.com.cn",
            //    agentid = 1,
            //    weixinid = "HappyV_W"
            //};
           
            //string userJson = JsonConvert.SerializeObject(userData, Formatting.Indented);

            HttpHelper httpHelper = new HttpHelper();
            
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),               
                URL = addUserUrl,
                Method = "post",//URL     可选项 默认为Get
                Postdata = userJson
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
        public HttpResult updateUserToWechat( String secret, string userJson)
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string updateUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}";
            var updateUserUrl = string.Format(updateUserUrlFormat, accessToken);

           HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),
                URL = updateUserUrl,
                Method = "post",//URL     可选项 默认为Get
                Postdata = userJson
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
        public HttpResult deleteUserFromWechat(String userId, String secret)
        {
            
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string deleteUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}";
            var deleteUserUrl = string.Format(deleteUserUrlFormat, accessToken, userId);

          

            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = deleteUserUrl,
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

        public HttpResult inviteUserToWechat(String secret, string userJson)
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string updateUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token={0}";
            var updateUserUrl = string.Format(updateUserUrlFormat, accessToken);

            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),
                URL = updateUserUrl,
                Method = "post",//URL     可选项 默认为Get
                Postdata = userJson
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

        public HttpResult getUserFromWechat(String userId, String secret)
        {

            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string getUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}";
            var getUserUrl = string.Format(getUserUrlFormat, accessToken, userId);

          

            HttpHelper httpHelper = new HttpHelper();
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
     
        public WechatUser getUserFromWechatByDepartment(int department, String secret)
        {
            WechatUser wechatUser = null;
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string getUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child=1&status=0";

            var addUserUrl = string.Format(getUserUrlFormat, accessToken, department);
            
            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),     
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

        public HttpResult sendTextMessageToWechat(String toUser, String content, String Wechat_Secret, int agentid)
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, Wechat_Secret);

            var msgUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", accessToken);


            var msgData = new
            {

                touser = toUser,
                msgtype = "text",
                agentid = agentid,
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
        public HttpResult sendNewsMessageToWechat(String msgJson, String Wechat_Secret)
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, Wechat_Secret);

            var msgUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", accessToken);


         


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

        public HttpResult getDepartmentListFromWechat(String secret)
        {

            WechatUtil wechatUtil = new WechatUtil();
           String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);         

            
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}";
            var url = string.Format(urlFormat, accessToken);

         

            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),
                URL = url,
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


        public HttpResult addDepartmentToWechat(String secret, string msgJson)
        {

            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);


            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}";
            var url = string.Format(urlFormat, accessToken);



            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),
                Postdata = msgJson,
                URL = url,
                Method = "post"//URL     可选项 默认为Get

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

        public HttpResult updateDepartmentToWechat(String secret, string msgJson)
        {

            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);


            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}";
            var url = string.Format(urlFormat, accessToken);



            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),
                Postdata = msgJson,
                URL = url,
                Method = "post"//URL     可选项 默认为Get

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


        public HttpResult deleteDepartmentFromWechat(int id, String secret)
        {

            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string UrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id={1}";
            var Url = string.Format(UrlFormat, accessToken, id);

            

            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = Url,
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

        public HttpResult addTagUsers(String secret, string userJson)
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string addUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers?access_token={0}";
            var addUserUrl = string.Format(addUserUrlFormat, accessToken);

            

            HttpHelper httpHelper = new HttpHelper();

            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),
                URL = addUserUrl,
                Method = "post",//URL     可选项 默认为Get
                Postdata = userJson
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

        public HttpResult deleteTagUsers(String secret, string userJson)
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(corpId, secret);

            string delUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers?access_token={0}";
            var delUserUrl = string.Format(delUserUrlFormat, accessToken);



            HttpHelper httpHelper = new HttpHelper();

            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                PostEncoding = Encoding.GetEncoding("UTF-8"),
                URL = delUserUrl,
                Method = "post",//URL     可选项 默认为Get
                Postdata = userJson
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
