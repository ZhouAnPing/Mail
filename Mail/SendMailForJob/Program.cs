using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TripolisDialogueAdapter;
using System.Threading;
using TripolisDialogueAdapter.cn.tripolis.dialogue.publish;

namespace SendMailForJob
{
    class Program
    {
        static void Main(string[] args)
        {
            MailJob mailJob = new MailJob();
            mailJob.Loop4Mail();
        }
    }
}
