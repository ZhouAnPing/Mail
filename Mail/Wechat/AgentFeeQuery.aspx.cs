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
    public partial class AgentFeeQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentFeeQuery));
        protected void Page_Load(object sender, EventArgs e)
        {
            string feeMonth =Request.QueryString["feeMonth"];
            string agentNo =Request.QueryString["agentNo"];
            logger.Info("feeMonth=" + Request.QueryString["feeMonth"]);
            logger.Info("agentNo=" + Request.QueryString["agentNo"]);
            try
            {
                
                Request.ContentEncoding = Encoding.UTF8;
                feeMonth = QueryStringEncryption.Decode(feeMonth, QueryStringEncryption.key);
                agentNo = QueryStringEncryption.Decode(agentNo, QueryStringEncryption.key);
                logger.Info("feeMonth=" + feeMonth);
                logger.Info("agentNo=" + agentNo);
            }
            catch (Exception)
            {
               return;
            }
            //agentNo = "DL0010";
          //  feeMonth = "2014-10";
            AgentFeeDao AgentFeeDao = new AgentFeeDao();

            AgentFee agentFee = AgentFeeDao.GetByKey(feeMonth, agentNo);
           // this.Label1.Text = agentFee.agent.agentName;
            if (agentFee == null)
            {
                return;
            }

            char[] separator = "<br>".ToCharArray();
            StringBuilder sbDesc = new StringBuilder();

            if (!String.IsNullOrEmpty(agentFee.agent.agentTypeComment))
            {
               
                string[] agentTypeCommentList = agentFee.agent.agentTypeComment.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                for (int count = 0; count < agentTypeCommentList.Length; count++)
                {
                    sbDesc.Append(count + 1).AppendFormat(".{0}<br><br>", agentTypeCommentList[count]);
                }
            }

            this.lblAgentComment.Text = sbDesc.ToString(); ;
            this.lblAgentName.Text = agentFee.agent.agentName;
            this.lblAgentNo.Text = agentFee.agentNo;
            this.lblAgentType.Text = agentFee.agent.agentType;
            this.lblFeeMonth.Text = agentFee.agentFeeMonth;
            this.lblFeeSeq.Text = agentFee.agentFeeSeq;
            this.lblSendDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");

            DataTable dt = new DataTable();
            dt.Columns.Add("seq");
            dt.Columns.Add("feeName");
            dt.Columns.Add("fee");
            int i = 1;
            DataRow row = null;

            HashSet<String> category = new HashSet<string>();
            Dictionary<String, Dictionary<String, String>> CategoryMap = new Dictionary<string, Dictionary<String, String>>();
            //按结账科目分类
            for (int j = 1; j <= 100; j++)
            {
                FieldInfo feeNameField = agentFee.GetType().GetField("feeName" + j);
                FieldInfo feeField = agentFee.GetType().GetField("fee" + j);
                if (feeNameField != null && feeField != null)
                {
                    String feeNameFieldValue = feeNameField.GetValue(agentFee) == null ? null : feeNameField.GetValue(agentFee).ToString();

                    String feeFieldValue = feeField.GetValue(agentFee) == null ? null : feeField.GetValue(agentFee).ToString();

                    if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                    {
                        String headText = feeNameFieldValue;
                        int locationIndex = headText.IndexOf("-");
                        int endIndex = headText.IndexOf("（");
                        if (locationIndex == -1)
                        {
                            locationIndex = headText.IndexOf("-");
                        }
                        if (endIndex == -1)
                        {
                            endIndex = headText.IndexOf("(");
                        }
                        String key = headText.Substring(locationIndex + 1);
                        if (endIndex != -1)
                        {
                            key = headText.Substring(locationIndex + 1, endIndex - locationIndex - 1);
                        }
                        String value = feeFieldValue;
                        if (!CategoryMap.ContainsKey(key))
                        {
                            Dictionary<String, String> valueMap = new Dictionary<string, string>();
                            valueMap.Add(headText, value);
                            CategoryMap.Add(key, valueMap);
                        }
                        else
                        {
                            Dictionary<String, String> valueMap = CategoryMap[key];
                            valueMap.Add(headText, value);
                        }
                    }
                }
            }

              int index = 1;
              foreach (String itemKey in CategoryMap.Keys)
              {
                  Dictionary<String, String> valueMap = CategoryMap[itemKey];
                  float subTotal = 0;
                  foreach (String value in valueMap.Values)
                  {
                      if (!String.IsNullOrEmpty(value))
                      {
                          subTotal = subTotal + float.Parse(value);
                      }
                  }
                  if (index > 1)
                  {
                     // row = dt.NewRow();
                    //  dt.Rows.Add(row);

                  }
                  row = dt.NewRow();
                  row["seq"] =null;
                  row["feeName"] = itemKey  ;

                  row["fee"] = subTotal;
                  dt.Rows.Add(row);
                  foreach (String subKey in valueMap.Keys)
                  {
                      row = dt.NewRow();
                      row["seq"] = index++;
                      row["feeName"] = subKey;

                      row["fee"] = valueMap[subKey];
                      dt.Rows.Add(row);
                  }

              }          

            //for (int j = 1; j <= 100;j++){
            //    FieldInfo feeNameField = agentFee.GetType().GetField("feeName" + j);
            //     FieldInfo feeField = agentFee.GetType().GetField("fee" + j);
            //     if (feeNameField != null && feeField != null)
            //     {
            //         String feeNameFieldValue = feeNameField.GetValue(agentFee)==null?null:feeNameField.GetValue(agentFee).ToString();

            //         String feeFieldValue = feeField.GetValue(agentFee) == null ? null : feeField.GetValue(agentFee).ToString();

            //         if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
            //         {
            //             row = dt.NewRow();
            //             row["seq"] = i++;
            //             row["feeName"] = feeNameFieldValue;
                        
            //             row["fee"] = feeFieldValue;
            //             dt.Rows.Add(row);
            //         }
            //     }

                
            //}
             // row = dt.NewRow();
            //  dt.Rows.Add(row);

            row = dt.NewRow();
            row["seq"] =   index++ ;
            row["feeName"] =   "总计" ;
            row["fee"] =   agentFee.feeTotal ;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["seq"] = index++;
            row["feeName"] = "开票金额";
            row["fee"] = agentFee.invoiceFee;
            dt.Rows.Add(row);
           

            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
            

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
               if (String.IsNullOrEmpty(e.Row.Cells[0].Text)||e.Row.Cells[0].Text.Equals("&nbsp;"))
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
            if(e.Row.Cells[1].Text.Equals("总计")){
                e.Row.Cells[0].Attributes.Add("style", "color: #000066; font-weight: bold;");
                e.Row.Cells[1].Attributes.Add("style", "color: #000066; font-weight: bold;");
                e.Row.Cells[2].Attributes.Add("style", "color: #000066; font-weight: bold;");
            }
            if (e.Row.Cells[1].Text.Equals("开票金额"))
            {
                e.Row.Cells[0].Attributes.Add("style", "color: #000066; font-weight: bold;");
                e.Row.Cells[1].Attributes.Add("style", "color: #000066; font-weight: bold;");
                e.Row.Cells[2].Attributes.Add("style", "color: #000066; font-weight: bold;");
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