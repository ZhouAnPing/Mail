using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_Agent.Wechat
{
    class DepartmentList
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public IList<Department> department { get; set; }
    }
}
