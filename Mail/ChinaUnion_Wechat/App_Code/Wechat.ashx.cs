using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChinaUnion_Wechat.App_Code
{
    /// <summary>
    /// Summary description for Wechat
    /// </summary>
    public class Wechat : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}