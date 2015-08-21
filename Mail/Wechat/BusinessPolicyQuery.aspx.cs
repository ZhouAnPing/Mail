﻿using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wechat.BO;
using Wechat.Util;

namespace Wechat
{
    public partial class BusinessPolicyQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BusinessPolicyQuery));

       
        protected void Page_Load(object sender, EventArgs e)
        {
            logger.Info(this.Request.Url.AbsoluteUri);

            //  String myUrl = "http://112.64.17.80/wechat/BusinessPolicyQuery.aspx?search_scope=validate&messageType=notice";
            // myUrl = this.Server.UrlEncode(myUrl);
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];
            string search_scope = Request.QueryString["search_scope"];
            logger.Info("code=" + Request.QueryString["code"]);
            logger.Info("state=" + Request.QueryString["state"]);
            logger.Info("search_scope=" + Request.QueryString["search_scope"]);
             WechatUtil wechatUtil = new Util.WechatUtil();
            HttpResult result = wechatUtil. getUserInfoFromWechat(code, "6", "stkc3kfO0sCrIJTFCHzOhmEsRWAnVGHqrzBIy4le6mV6EcSkNbpY0Tt49Uci2Buu");
            logger.Info("result=" + result.Html);
            if (result.Html.Contains("UserId"))
            {
                WechatUserId returnMessage = (WechatUserId)JsonConvert.DeserializeObject(result.Html, typeof(WechatUserId));

            }
            search_scope = "all";
            String type = "通知公告/重点关注";
            if (!String.IsNullOrEmpty(state) && state.Equals("myNotice"))
            {
                type = "通知公告/重点关注";
            }

            if (!String.IsNullOrEmpty(state) && state.Equals("policy"))
            {
                type = "政策";
            }

            if (!String.IsNullOrEmpty(state) && state.Equals("rule"))
            {
                type = "服务规范";
            }
            bindDataToGrid("", type, search_scope);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            bindDataToGrid(this.txtCondition.Text.Trim(), this.lblType.Text, this.lblScope.Text.Trim());
        }
        void bindDataToGrid(String subject, String type, String search_scope)
        {
            logger.Info("bindDataToGrid=");
            logger.Info("subject=" + subject);
            logger.Info("type=" + type);
            logger.Info("search_scope=" + search_scope);
            PolicyDao policyDao = new ChinaUnion_DataAccess.PolicyDao();

            IList<Policy> policyList = null;

            if (!String.IsNullOrEmpty(search_scope) && search_scope.Equals("validate"))
            {
                policyList = policyDao.GetAllValidatedList(subject, type);
                logger.Info("validate=");
            }


            if (!String.IsNullOrEmpty(search_scope) && search_scope.Equals("all"))
            {
                policyList = policyDao.GetList(subject, type);
                logger.Info("all=");
            }
            this.lblType.Text = type;
            this.lblScope.Text = search_scope;
            // int index = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("seq");
            dt.Columns.Add("subject");
            dt.Columns.Add("content");
            dt.Columns.Add("attachment");
            dt.Columns.Add("createTime");
            dt.Columns.Add("validateTime");

            DataRow row = null;
            if (policyList != null)
            {
                foreach (Policy policy in policyList)
                {
                    row = dt.NewRow();
                    row["seq"] = policy.sequence;
                    row["subject"] = policy.subject;
                    if (policy.content.Length > 10)
                    {
                        row["content"] = policy.content.Substring(0, 10) + "......";
                    }
                    else
                    {
                        row["content"] = policy.content;
                    }
                    if (!String.IsNullOrEmpty(policy.attachmentName))
                    {
                        row["attachment"] = "附件";
                    }
                    row["createTime"] = policy.creatTime;
                    row["validateTime"] = policy.validateTime;
                    dt.Rows.Add(row);
                }
            }
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindDataToGrid(this.txtCondition.Text.Trim(), this.lblType.Text, this.lblScope.Text.Trim());
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //隐藏列
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].Attributes.Add("style", "display:none");   //隐藏数据列
                e.Row.Cells[3].Attributes.Add("style", "display:none");   //隐藏数据列
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                if (String.IsNullOrEmpty(e.Row.Cells[0].Text) || e.Row.Cells[0].Text.Equals("&nbsp;"))
                {
                    if (!String.IsNullOrEmpty(e.Row.Cells[1].Text) || e.Row.Cells[1].Text.Equals("&nbsp;"))
                    {
                        e.Row.Cells[0].Attributes.Add("style", "color: #000066; font-weight: bold;");
                        e.Row.Cells[1].Attributes.Add("style", "color: #000066; font-weight: bold;");
                        e.Row.Cells[2].Attributes.Add("style", "color: #000066; font-weight: bold;");
                    }
                    else
                    {
                        e.Row.Cells[0].Attributes.Add("style", "display:none");
                        e.Row.Cells[1].Attributes.Add("style", "display:none");
                        e.Row.Cells[2].Attributes.Add("style", "display:none;");
                    }
                }

                if (!String.IsNullOrEmpty(e.Row.Cells[0].Text) && !e.Row.Cells[0].Text.Equals("&nbsp;") && !e.Row.Cells[1].Text.Equals("总计"))
                {
                    e.Row.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + e.Row.Cells[1].Text;
                }


            }

        }


    }
}