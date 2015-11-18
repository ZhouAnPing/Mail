using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChinaUnion_BO
{
    public class StudyReceiverLog
    {
        public String studySequence;
        public String userId;
        public String readtime;
        public Study study = new Study();
        public AgentWechatAccount agentContact = new AgentWechatAccount();


    }
}
