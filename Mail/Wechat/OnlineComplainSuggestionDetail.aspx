<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineComplainSuggestionDetail.aspx.cs" Inherits="Wechat.OnlineComplainSuggestionDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%"  class="deviceWidth" border="0" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        	<tr>
			                    <td   style="font-size: 16px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#ff6600">
			                        	<asp:Label ID="lblType" runat="server" Font-Bold="true"></asp:Label></td>				
							  
			                </tr> 
			                <tr>
			                    <td  style=" font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>主题:</b>
			                                    	
			                            <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>
			                                    	
			                    </td>				
							  
			                </tr> 
         <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>内容:</b>
			                            <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
			                    </td>				
							  
			                </tr> 
         
        
         <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>发布时间:</b>
			                            <asp:Label ID="lblCreateTime" runat="server" Text=""></asp:Label>
			                    </td>				
							  
			                </tr> 

        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>是否回复:</b>
			                            
			                            <asp:Label ID="lblIsReply" runat="server"></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        
        
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>答复时间:</b>
			                            
			                            <asp:Label ID="lblReplyTime" runat="server"></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>答复内容:</b>
			                            
			                            <asp:Label ID="lblReplyContent" runat="server" ></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 25px;  color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	
			                        <asp:button ID="btnBack" runat="server" onclick="javascript: window.history.go(-1);" value="<<返回上一页" style="font-size: 25px;  color: white; font-weight: bold;height: 54px; width: 100%;font-size: 16px;background-color:#ff6600"/>
			                        	
			                    </td>				
							  
			                </tr> 
            </table>
    </div>
    </form>
</body>
</html>
