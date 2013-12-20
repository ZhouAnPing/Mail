using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripolisDialogueAdapter.BO
{
    public class DirectEmail
    {
        public String emailLabel;
        public String emailName;
        public String subject;
        public String description;
        public String fromName;
        public String fromAddress;
        public String htmlContent;
        public String reportReceiveAddress;
        public DateTime sheduleTime = DateTime.Now;
        // public String textContentl;
       // public bool isContentSame = false;
    }
}