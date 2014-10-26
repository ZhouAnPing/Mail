using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripolisDialogueAdapter.cn.tripolis.dialogue.reporting;

namespace TripolisDialogueAdapter.BO
{
    [Serializable()]
    public class ReportData
    {
        public EmailSummary emailSummary;
        
        public SkippedContactModel[] skipped;       
        public BouncedContact[] bounced;
        public Contact[] sent;
        public Open[] opened;

        public Click[] clicked;
       
        
    }
}
