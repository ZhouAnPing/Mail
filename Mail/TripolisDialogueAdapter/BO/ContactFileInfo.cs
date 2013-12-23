using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripolisDialogueAdapter.cn.tripolis.dialogue.import;

namespace TripolisDialogueAdapter.BO
{
   public class ContactFileInfo
    {
       public const String DEFAULT_CSV_DELIMIT = ";";
       public fileExtension fileType;
       public String filename;
       public byte[] fileContent;
       public String csvDilimiter=";";
    }
}
