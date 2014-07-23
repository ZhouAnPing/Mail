using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace TripolistMailAdapter
{
    public class Util
    {
        /// <summary>
        /// 查找<A的html标记，如果里面没有title属性，则增加这个属性.
        /// </summary>
        /// <param name="html">html文本</param>
        /// <returns></returns>
        public static String addTitleInAFlagHtml(String html)
        {
            if(String.IsNullOrEmpty(html)){
                return html;
            }
            String regStr = "<a[^>]+>";// "<a(.*)(>){1}";
            String result = html;
            MatchCollection matches = Regex.Matches(html, regStr, RegexOptions.IgnoreCase | RegexOptions.Multiline); //匹配<a  href ="" >结果的集合
            foreach (Match match in matches)
            {
                String matchStr = match.Value;
                if (!String.IsNullOrEmpty(matchStr) && matchStr.IndexOf("title", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    String newStr = matchStr.Replace("<a", "<a title =\"link\" ");
                    result = result.Replace(matchStr, newStr);
                }


            }
            return result;
        }
        /// <summary>
        /// check if the String including 401 code
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool isCodeExist(XmlNode node)
        {
           
            try
            {
                var selectSingleNode = node.SelectSingleNode("//error[errorCode='401']");
                if (selectSingleNode != null)
                {
                    XmlNode tempNode = selectSingleNode.ChildNodes[0];
                    if (tempNode != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(node.InnerText);
            }

            return false;
        }

        /// <summary>
        /// get the existed id
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static String getExistId(XmlNode node)
        {
          
            try
            {
                var selectSingleNode = node.SelectSingleNode("//error[errorCode='401']");
                if (selectSingleNode != null)
                {
                    XmlNode tempNode = selectSingleNode.SelectSingleNode("identifierId");
                    if (tempNode != null)
                    {
                        return tempNode.InnerText;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(node.InnerText);
            }
            return ""; 
        }
        /**/
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param name="xmlStr">Xml字符串</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr)
        {
            return CXmlToDataSet(xmlStr).Tables[0];
        }

        /**/
        /// <summary>
        /// 将Xml内容字符串转换成DataSet对象
        /// </summary>
        /// <param name="xmlStr">Xml内容字符串</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlToDataSet(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader strStream = null;
                XmlTextReader xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    strStream = new StringReader(xmlStr);
                    //获取StrStream中的数据
                    xmlrdr = new XmlTextReader(strStream);
                    //ds获取Xmlrdr中的数据               
                    ds.ReadXml(xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (xmlrdr != null)
                    {
                        xmlrdr.Close();
                        strStream.Close();
                        strStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Merge two Xml Document
        /// </summary>
        /// <param name="doc1">xdocument 1 </param>
        /// <param name="doc2">xdocument 2</param>
        /// <returns>merged xdocument</returns>
        public static XDocument mergeXml(XDocument doc1, XDocument doc2)
        {
            if (doc1 == null && doc2 == null)
            {
                return null;
            }
            if (doc1 == null || doc1.Root == null)
            {
                return doc2;
            }
            if (doc2 == null || doc2.Root == null)
            {
                return doc1;
            }

           XElement sendElement1 =  (from el in doc1.Root.Descendants("exportSent")
             select el).First();
           XElement sendElement2 = (from el in doc2.Root.Descendants("exportSent")
                                    select el).First();          
           sendElement1.Add(sendElement2.Nodes());

           sendElement1 = (from el in doc1.Root.Descendants("exportBounced")
                                    select el).First();
           sendElement2 = (from el in doc2.Root.Descendants("exportBounced")
                                    select el).First();
           sendElement1.Add(sendElement2.Nodes());

           sendElement1 = (from el in doc1.Root.Descendants("exportOpened")
                           select el).First();
           sendElement2 = (from el in doc2.Root.Descendants("exportOpened")
                           select el).First();
           sendElement1.Add(sendElement2.Nodes());

           sendElement1 = (from el in doc1.Root.Descendants("exportClicked")
                           select el).First();
           sendElement2 = (from el in doc2.Root.Descendants("exportClicked")
                           select el).First();
           sendElement1.Add(sendElement2.Nodes());


           return doc1;
        }
        /// <summary>
        /// Conversion the input file from csv format to XML Conversion Method
        /// </summary>
        /// <param name="csvString">cvs string to converted</param>
        /// <param name="separatorField">separator used by the csv file</param>
        /// <param name="rootElementName">rootElementName</param>
        /// <param name="searchConditions">searchConditions</param>
        /// <returns>XDocument</returns>
        public static XDocument convertCsvToXml(string csvString, string[] separatorField, string rootElementName, Hashtable searchConditions)
        {
            
            //Create the declaration
            var xsurvey = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"));
            var xroot = convertCsvToXmlElement(csvString, separatorField, rootElementName, searchConditions);
            xsurvey.Add(xroot);
            return xsurvey;
        }

        /// <summary>
        /// Conversion the input file from csv format to XML Conversion Method
        /// </summary>
        /// <param name="csvString">cvs string to converted</param>
        /// <param name="separatorField">separator used by the csv file</param>
        /// <param name="rootElementName">rootElementName</param>
        /// <param name="searchConditions">searchConditions</param>
        /// <returns>XElement</returns>
        public static XElement convertCsvToXmlElement(string csvString, string[] separatorField, string rootElementName, Hashtable searchConditions)
        {
            //split the rows
            var sep = new[] { "\n" };
            string[] rows = csvString.Split(sep, StringSplitOptions.RemoveEmptyEntries);
           
            var xroot = new XElement(rootElementName); // new XElement("root"); //Create the root


            Hashtable searchColumns = new Hashtable();
            //判断是否有查询条件，并把条件字段的索引找出来
            if (searchConditions != null)
            {
                string[] columnNames = rows[0].Split(separatorField, StringSplitOptions.None);
                for (int i = 0; i < columnNames.Length; i++)
                {

                    if (searchConditions.ContainsKey(columnNames[i]))
                    {
                        searchColumns.Add(i, searchConditions[columnNames[i]]);
                    }
                }
            }

           
          
                int index1 = 0;
                try
                {
                for (int i = 0; i < rows.Length; i++)
                {
                    index1 = i;
                    //Create each row
                    if (i > 0)
                    {
                        //有查询条件，需要过滤
                        if (searchConditions != null)
                        {
                            string[] values = rows[i].Split(separatorField, StringSplitOptions.None);
                            foreach (int index in searchColumns.Keys)
                            {
                                if (values[index].Equals(searchColumns[index]))
                                {
                                    xroot.Add(rowCreator(rows[i], rows[0], separatorField));
                                    break;
                                }

                            }
                        }
                        else
                        {
                            xroot.Add(rowCreator(rows[i], rows[0], separatorField));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                String result = ex.Message;
            }
            return xroot;
        }
        /// <summary>
        /// Private. Take a csv line and convert in a row - var node
        /// with the fields values as attributes. 
        /// <param name="row" >csv row to process</param >
        /// <param name="firstRow" >First row with the fields names</param >
        /// <param name="separatorField" >separator string use in the csv fields</param >       
        /// </summary>
        private static XElement rowCreator(string row, string firstRow, string[] separatorField)
        {
            string[] temp = row.Split(separatorField, StringSplitOptions.None);
            string[] names = firstRow.Split(separatorField, StringSplitOptions.None);

            var xrow = new XElement("row");
            for (int i = 0; i < temp.Length; i++)
            {
                //Create the element var and Attributes with the field name and value
                // var xvar = new XElement("var", new XAttribute("name", names[i]),new XAttribute("value", temp[i]));
                var xvar = new XElement(names[i].Replace(" ", ""), temp[i]);
                xrow.Add(xvar);
            }
            return xrow;
        }



    }
}
