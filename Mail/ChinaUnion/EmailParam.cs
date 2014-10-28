using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TripolisDialogueAdapter;

namespace JobMailTools
{
    class EmailParam
    {
        public TripolisConfig tripolisConfig;
        public DataGridView dataGrid;
        public String batchNo;
        public String sender;
        public String emailSubject;
        public String emailBody;
    }
}
