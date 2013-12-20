using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TripolisDialogueAdapterWs
{
    [Serializable()]
    public class ExportReportToFtpParam
    {
        public String contactDatabaseId;
        public DateTime startTime;
        public DateTime endTime;
        public String ftpAccountId;
        public String fileNamePrefix;
    }
}