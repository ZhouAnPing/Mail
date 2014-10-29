using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinaUnion_Agent.Wechat
{
    class WechatUser
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public IList<User> userlist { get; set; }
    }
}
