using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wechat
{
    public partial class BusinessPolicyAttachmentDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sequence = Request.QueryString["seq"];

            PolicyDao policyDao = new ChinaUnion_DataAccess.PolicyDao();
            Policy policy = policyDao.Get(Int32.Parse(sequence));

            if (policy != null)
            {
                Response.ContentType = "Application/pdf";
                this.Response.Clear();

                System.IO.Stream fs = this.Response.OutputStream;
                fs.Write(policy.attachment, 0, policy.attachment.Length);

                fs.Close();
                this.Response.End();
            }

        }
    }
}