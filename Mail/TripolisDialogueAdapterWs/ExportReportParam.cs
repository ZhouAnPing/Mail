﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TripolisDialogueAdapterWs
{
    [Serializable()]
    public class ExportReportParam
    {
        public String contactDatabaseId;
        public DateTime startTime;
        public DateTime endTime;        
    }
}