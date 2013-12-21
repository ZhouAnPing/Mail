using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripolisDialogueAdapter.BO
{
    public enum ReportType
    {
        OPENED,
        CLICKED,
        BOUNCED,
        SENT,
        COMPLAINT,//Number of receivers who categorized the mail as SPAM.
        JOBS,
        LINKS
    };
}
