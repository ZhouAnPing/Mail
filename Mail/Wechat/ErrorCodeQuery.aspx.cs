using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wechat
{
    public partial class ErrorCodeQuery : System.Web.UI.Page
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ErrorCodeQuery));
        protected void Page_Load(object sender, EventArgs e)
        {

            Request.ContentEncoding = Encoding.UTF8;
            string keyword = Request.QueryString["keyword"];

            logger.Info("keyword=" + keyword);

            AgentErrorCodeDao agentErrorCodeDao = new AgentErrorCodeDao();

            AgentErrorCode agentErrorCode = agentErrorCodeDao.GetByKey(keyword);
            if (agentErrorCode != null)
            {
                this.lblKeyword.Text = agentErrorCode.keyword;
                logger.Info("solution=" + agentErrorCode.solution);
                this.lblErrorSolution.Text = agentErrorCode.solution.Replace("解决办法", "<br>解决办法").Replace("解决方法", "<br>解决方法");
                this.lblErrerDesc.Text = agentErrorCode.errorDesc;
                this.lblContact.Text = agentErrorCode.contactName.Replace("联系电话", "<br><br>联系电话"); ;
                String dir = this.Server.MapPath("~/") + @"\ErrorImages\";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                String path = dir + agentErrorCode.seq + ".jpg";
                if (!File.Exists(path) || !File.GetCreationTime(path).ToString("yyyy-MM-dd").Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                {

                    System.IO.File.WriteAllBytes(path, agentErrorCode.errorImg);
                }

                this.Image1.ImageUrl = "http://115.29.229.134/Wechat/ErrorImages/" + agentErrorCode.seq + ".jpg";

            }

        }
    }
}