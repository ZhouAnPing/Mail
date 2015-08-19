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
    <table width="100%"  class="deviceWidth" border="1" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        	<tr>
			                    <td   style="font-size: 16px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#006699">
			                        	详情</td>				
							  
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
			                        	发布时间:
			                            <asp:Label ID="lblCreateTime" runat="server" Text=""></asp:Label>
			                    </td>				
							  
			                </tr> 

        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	核实情况:
			                            
			                            <asp:Label ID="lblCheckStatus" runat="server"></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	涉及部门:
			                            
			                            <asp:HyperLink ID="lblDepartment" runat="server"></asp:HyperLink>
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	涉及部门反馈情况:
			                            
			                            <asp:Label ID="lblDepartmentReply" runat="server"></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	答复时间:
			                            
			                            <asp:Label ID="lblReplyTime" runat="server"></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	答复内容:
			                            
			                            <asp:Label ID="lblReplyContent" runat="server"></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 25px;  color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	
			                        <input type="button" onclick="javascript: window.history.go(-1);"value="<<返回上一页" style="font-size: 25px;  color: white; font-weight: bold;height: 54px; width: 100%;font-size: 16px;background-color:#006699"/>
			                        	
			                    </td>				
							  
			                </tr> 
            </table>
    </div>
    </form>
</body>
</html>
