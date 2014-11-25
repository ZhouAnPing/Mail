using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGenerateXml
{
    class Program
    {
        static void Main(string[] args)
        {
            String path=".";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<updateFiles>");

            String[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach (String fileName in files)
            {
              
                String name = Path.GetFileName(fileName);
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Extension.ToLower().Equals("xml"))
                {
                    continue;
                }
                sb.AppendFormat(" <file path=\"{0}\" url=\"{1}\" lastver=\"1.0.1\" size=\"{2}\" needRestart=\"false\"/> ", name, fileName, fileInfo.Length);
                sb.AppendLine(); 
            }

            sb.AppendLine("</updateFiles>");

          //  File.Create("updateservice.xml");
            File.WriteAllText("updateservice_test.xml", sb.ToString());
        }
    }
}
