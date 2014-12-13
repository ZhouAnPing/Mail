using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {

            String str= "<#if contact.fee{0}?has_content>"+
							"<tr>"+
								"<td nowrap"+
									"style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding: 0px\""+
                                    "bgcolor=\"#ffffff\">${contact.fee_seq{1}!}</td>" +
								"<td nowrap"+
									"style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding: 0px\""+
                                    "bgcolor=\"#ffffff\"><#if contact.fee_seq{2}?has_content>"+
								"&nbsp;&nbsp;&nbsp;&nbsp; </#if>${contact.fee_name{3}!}</td>" +
								"<td nowrap"+
									"style=\"font-size: 13px; color: black; font-weight: normal; text-align: right; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding: 0px\""+
                                    "bgcolor=\"#ffffff\">${contact.fee{4}!}</td>" +
							"</tr>"+
							"</#if>";

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 100; i++)
            {
                sb.AppendFormat("<#if contact.fee{0}?has_content>",i).AppendLine();
                sb.AppendFormat("<tr>").AppendLine();
                sb.AppendFormat("<td nowrap").AppendLine();
                sb.AppendFormat("style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding: 0px\"").AppendLine();
                sb.Append("bgcolor=\"#ffffff\">${").AppendFormat("contact.fee_seq{0}", i).Append("!}</td>").AppendLine();
                sb.AppendFormat("<td nowrap").AppendLine();
                sb.AppendFormat("style=\"font-size: 13px; color: black; font-weight: normal; text-align: left; font-family: Georgia, Times, serif; line-height: 24px; vertical-align: top; padding: 0px\"").AppendLine();
                sb.AppendFormat("bgcolor=\"#ffffff\"><#if contact.fee_seq{0}?has_content>",i).AppendLine();

                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp; </#if>${").AppendFormat("contact.fee_name{0}", i).Append("!}</td>").AppendLine();

                sb.AppendFormat("<td nowrap").AppendLine();
                sb.AppendFormat("style=\"font-size: 13px; color: black; font-weight: normal; text-align: right; font-family: Microsoft YaHei, Times, serif; line-height: 24px; vertical-align: top; padding: 0px\"").AppendLine();
                sb.Append("bgcolor=\"#ffffff\">${").AppendFormat("contact.fee{0}", i).Append("!}</td>").AppendLine();
                sb.AppendFormat("</tr>").AppendLine();
                sb.AppendFormat("</#if>").AppendLine(); ;
            }

            System.Console.WriteLine(sb.ToString());
        }
    }
}
