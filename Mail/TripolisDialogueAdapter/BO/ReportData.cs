using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripolisDialogueAdapter.cn.tripolis.dialogue.reporting;

namespace TripolisDialogueAdapter.BO
{
    public class ReportData
    {
        public Contact[] sent;
        public Open[] opened;
        public Click[] clicked;
        public BouncedContact[] bounced;        
    }
}
