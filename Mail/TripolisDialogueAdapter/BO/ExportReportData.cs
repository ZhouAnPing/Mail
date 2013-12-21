using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripolisDialogueAdapter.BO
{
    [Serializable()]
    public class ExportReportData
    {
        public byte[] sent;
        public byte[] opened;
        public byte[] clicked;
        public byte[] bounced;
        public byte[] links;
    }
}
