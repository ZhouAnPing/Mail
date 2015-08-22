<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessPolicyDetail.aspx.cs" Inherits="Wechat.BusinessPolicyDetail" %>

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
			                    <td   style="font-size: 16px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#006699">
			                        	通知详情</td>				
							  
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
			                        	<b>附件名:</b>
			                            
			                            <asp:HyperLink ID="lblAttachment" runat="server"></asp:HyperLink>
			                            
			                    </td>				
							  
			                </tr> 
         <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>发布时间:</b>
			                            <asp:Label ID="lblSendTime" runat="server" Text=""></asp:Label>
			                    </td>				
							  
			                </tr> 
            </table>
    </div>
    </form>
</body>
</html>
