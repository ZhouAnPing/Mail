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

namespace Wechat
{
    public partial class AgentFeeQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AgentFeeQuery));
        protected void Page_Load(object sender, EventArgs e)
        {

            Request.ContentEncoding = Encoding.UTF8;
            string feeMonth = Request.QueryString["feeMonth"];
            string agentNo = Request.QueryString["agentNo"];

            AgentFeeDao AgentFeeDao = new AgentFeeDao();

            AgentFee agentFee = AgentFeeDao.GetByKey(feeMonth, agentNo);
           // this.Label1.Text = agentFee.agent.agentName;

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

            for (int j = 1; j <= 100;j++){
                FieldInfo feeNameField = agentFee.GetType().GetField("feeName" + j);
                 FieldInfo feeField = agentFee.GetType().GetField("fee" + j);
                 if (feeNameField != null && feeField != null)
                 {
                     String feeNameFieldValue = feeNameField.GetValue(agentFee)==null?null:feeNameField.GetValue(agentFee).ToString();

                     String feeFieldValue = feeField.GetValue(agentFee) == null ? null : feeField.GetValue(agentFee).ToString();

                     if (!String.IsNullOrEmpty(feeFieldValue) && !String.IsNullOrWhiteSpace(feeFieldValue))
                     {
                         row = dt.NewRow();
                         row["seq"] = i++;
                         row["feeName"] = feeNameFieldValue;
                        
                         row["fee"] = feeFieldValue;
                         dt.Rows.Add(row);
                     }
                 }

                
            }
            row = dt.NewRow();
            row["seq"] = i++;
            row["feeName"] = "总计";
            row["fee"] = agentFee.feeTotal;
            dt.Rows.Add(row);
           

            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
            

        }
    }
}