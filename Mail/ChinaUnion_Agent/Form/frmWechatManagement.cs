﻿using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.Wechat
{
    public partial class frmWechatManagement : Form
    {
        public frmWechatManagement()
        {
            InitializeComponent();
        }

        private void frmWechatManagement_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //代理商信息            
            //   Queryworker.ReportProgress(1, "代理商信息...\r\n");
            Dao.AgentDao agentDao = new Dao.AgentDao();
            IList<BO.Agent> agentList = agentDao.GetList();

            if (agentList != null && agentList.Count > 0)
            {
                dgAgent.Rows.Clear();
                dgAgent.Columns.Clear();

                dgAgent.Columns.Add("代理商编号", "代理商编号");
                dgAgent.Columns.Add("代理商名称", "代理商名称");
                dgAgent.Columns.Add("联系人邮箱", "联系人邮箱");
                dgAgent.Columns.Add("联系人姓名", "联系人姓名");
                dgAgent.Columns.Add("联系人电话", "联系人电话");
                dgAgent.Columns.Add("联系人微信账号", "联系人微信账号");


                for (int i = 0; i < agentList.Count; i++)
                {
                    dgAgent.Rows.Add();
                    DataGridViewRow row = dgAgent.Rows[i];

                    row.Cells[0].Value = agentList[i].agentNo;
                    row.Cells[1].Value = agentList[i].agentName;
                    row.Cells[2].Value = agentList[i].contactEmail;
                    row.Cells[3].Value = agentList[i].contactName;
                    row.Cells[4].Value = agentList[i].contactTel;
                    row.Cells[5].Value = agentList[i].contactWechatAccount;


                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            sentMsgToWechat();
            getMenuInWechaht();
            deleteMenuInWechaht();
            this.addMenuToWechat();

           
        }

        private void getMenuInWechaht()
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(Settings.Default.Wechat_Corpid, Settings.Default.Wechat_Secret);

            var menuUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/menu/get?access_token={0}&agentid={1}", accessToken, 1);
            //                          {
            //                               ""type"":""view"",
            //                               ""name"":""公司简介"",
            //                               ""url"":""http://www.baidu.com""
            //                          },





            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                URL = menuUrl,
                Method = "get",//URL     可选项 默认为Get
                // Postdata = menuJson,
                PostEncoding = Encoding.GetEncoding("UTF-8")//可以发送中文消息了，开心

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
        }

        private void deleteMenuInWechaht()
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(Settings.Default.Wechat_Corpid, Settings.Default.Wechat_Secret);

            var menuUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/menu/delete?access_token={0}&agentid={1}", accessToken, 1);
            //                          {
            //                               ""type"":""view"",
            //                               ""name"":""公司简介"",
            //                               ""url"":""http://www.baidu.com""
            //                          },

         



            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                URL = menuUrl,
                Method = "get",//URL     可选项 默认为Get
               // Postdata = menuJson,
                PostEncoding = Encoding.GetEncoding("UTF-8")//可以发送中文消息了，开心

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
        }
        private void addMenuToWechat()
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(Settings.Default.Wechat_Corpid, Settings.Default.Wechat_Secret);

            var menuUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/menu/create?access_token={0}&agentid={1}", accessToken, 1);
            //                          {
            //                               ""type"":""view"",
            //                               ""name"":""公司简介"",
            //                               ""url"":""http://www.baidu.com""
            //                          },

            var menuData = @" {
                         ""button"":[
                         {	
                              ""type"":""click"",
                              ""name"":""你好!"",
                              ""key"":""Hello""
                          }]
                     } ";

            string menuJson = JsonConvert.SerializeObject(menuData, Formatting.Indented);



            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                Encoding = Encoding.GetEncoding("UTF-8"),
                URL = menuUrl,
                Method = "post",//URL     可选项 默认为Get
                Postdata = menuJson,
                PostEncoding = Encoding.GetEncoding("UTF-8")//可以发送中文消息了，开心

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
        }

        private void sentMsgToWechat()
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

                        
                        
                        content = "中文消息"
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

            //返回的Html内容
            string html = result.Html;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
            }
            //表示StatusCode的文字说明与描述
            string statusCodeDescription = result.StatusDescription;
        }

        private void addUserToWechat()
        {
            WechatUtil wechatUtil = new WechatUtil();
            String accessToken = wechatUtil.GetAccessTokenNoCache(Settings.Default.Wechat_Corpid, Settings.Default.Wechat_Secret);

            string addUserUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";
            var userData = new
            {
                userid = "viviwu",
                name = "Vivi",
                department = 1,
                position = "Manager",
                mobile = "18621180524",
                gender = 1,
                tel = "18621180524",
                email = "vivi.wu@yourzine.com.cn",
                agentid = 1,
                weixinid = "HappyV_W"
            };
            var addUserUrl = string.Format(addUserUrlFormat, accessToken);
            string userJson = JsonConvert.SerializeObject(userData, Formatting.Indented);



            HttpHelper httpHelper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
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
        }
    }
}
