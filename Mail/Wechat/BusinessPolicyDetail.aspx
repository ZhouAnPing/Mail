<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessPolicyDetail.aspx.cs" Inherits="Wechat.BusinessPolicyDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%"  class="deviceWidth" border="1" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        	<tr>
			                    <td   style="font-size: 16px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#006699">
			                        	通知详情</td>				
							  
			                </tr> 
			                <tr>
			                    <td  style=" font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	主题:
			                                    	
			                            <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>
			                                    	
			                    </td>				
							  
			                </tr> 
         <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	内容:
			                            <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
			                    </td>				
							  
			                </tr> 
         <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	附件名:
			                            
			                            <asp:HyperLink ID="lblAttachment" runat="server"></asp:HyperLink>
			                            
			                    </td>				
							  
			                </tr> 
         <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	发布时间:
			                            <asp:Label ID="lblSendTime" runat="server" Text=""></asp:Label>
			                    </td>				
							  
			                </tr> 
            </table>
    </div>
    </form>
</body>
</html>
