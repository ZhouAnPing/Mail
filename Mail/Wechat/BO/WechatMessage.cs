using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wechat.BO
{
    public class WechatMessage
    {
        public string ToUserName{ get; set; }
        public string FromUserName{ get; set; }
        public int CreateTime{ get; set; }
        public string MsgType{ get; set; }
        public int AgentID{ get; set; }
        public string Event{ get; set; }
        public string EventKey{ get; set; }
        public string Content { get; set; }

        public WechatMessage()
        {
        }
        public WechatMessage(System.Xml.XmlElement DocumentElement)
        {
            FromUserName = DocumentElement.SelectSingleNode("FromUserName") == null ? "" : DocumentElement.SelectSingleNode("FromUserName").InnerText;
            ToUserName = DocumentElement.SelectSingleNode("ToUserName") == null ? "" : DocumentElement.SelectSingleNode("ToUserName").InnerText;
            CreateTime = DocumentElement.SelectSingleNode("CreateTime") == null ? 0 : int.Parse(DocumentElement.SelectSingleNode("CreateTime").InnerText);
            MsgType = DocumentElement.SelectSingleNode("MsgType") == null ? "" : DocumentElement.SelectSingleNode("MsgType").InnerText;
            AgentID = DocumentElement.SelectSingleNode("AgentID") == null ? 0 : int.Parse(DocumentElement.SelectSingleNode("AgentID").InnerText);
            Event = DocumentElement.SelectSingleNode("Event") == null ? "" : DocumentElement.SelectSingleNode("Event").InnerText;
            EventKey = DocumentElement.SelectSingleNode("EventKey") == null ? "" : DocumentElement.SelectSingleNode("EventKey").InnerText;
            Content = DocumentElement.SelectSingleNode("Content") == null ? "" : DocumentElement.SelectSingleNode("Content").InnerText;

           
        }
       
    }
}