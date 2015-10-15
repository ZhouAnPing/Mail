using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_BO
{
    public class Policy
    {
        public String sequence;
        public String subject;
        public String content;
        public String attachmentName;
        public byte[] attachment;
        public String sender;
        public String creatTime;
        public String type;
       
        public String validateStartTime;
        public String validateEndTime;
        public String isValidate;
        public String deleteTime;
        public String isDelete;
        public String toAll;
        public String agentType;
    }
}
