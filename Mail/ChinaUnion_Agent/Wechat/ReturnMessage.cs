using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_Agent.Wechat
{
    public class ReturnMessage
    {
        public string errcode { get; set; }

        public string errmsg { get; set; }
        public String getErrorDescrition()
        {
            WechatErrorConstant wechatErrorConstant = new WechatErrorConstant();
            if (wechatErrorConstant.WechatErrorHT[errcode] != null)
            {
                return wechatErrorConstant.WechatErrorHT[errcode].ToString();
            }
            else
            {
                return errmsg;
            }

        }



    }
}
