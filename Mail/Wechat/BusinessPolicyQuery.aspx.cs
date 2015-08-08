using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Wechat.BO;
using Wechat.Properties;
using Wechat.Util;

namespace Wechat
{
    public partial class BusinessPolicyQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentFeeQuery));
        protected void Page_Load(object sender, EventArgs e)
        {

            PolicyDao policyDao = new ChinaUnion_DataAccess.PolicyDao();
            IList<Policy> policyList = policyDao.GetAllValidatedList();

           // int index = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("seq");
            dt.Columns.Add("subject");
            dt.Columns.Add("content");
            dt.Columns.Add("attachment");
            dt.Columns.Add("createTime");

            DataRow row = null;

            foreach (Policy policy in policyList)
            {
                row = dt.NewRow();
                row["seq"] = policy.sequence;
                row["subject"] = policy.subject;
                if (policy.content.Length > 10)
                {
                    row["content"] = policy.content.Substring(0,10)+"......";
                }
                else
                {
                    row["content"] = policy.content;
                }
                row["attachment"] = policy.attachmentName;
                row["createTime"] = policy.creatTime;
                dt.Rows.Add(row);
            }
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
           
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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
                //隐藏列

                //   e.Row.Cells[6].Attributes.Add("style", "display:none");   //隐藏数据列

            }

        }
    }
}