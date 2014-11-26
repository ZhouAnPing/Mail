using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_BO
{
   public class User
    {
        public string userId;
        public string name;
        public string password;
        public IList<UserRight> userRightList;
    }
}
