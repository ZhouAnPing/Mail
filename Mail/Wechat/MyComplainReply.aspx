<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyComplainReply.aspx.cs" Inherits="Wechat.MyComplainReply" %>

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
			                        	<strong>用户</strong><b>名:</b>
			                                    	
			                            <asp:Label ID="lblUserId" runat="server"></asp:Label>
			                                    	
			                            <asp:Label ID="lblSeq" runat="server" Visible="False"></asp:Label>
			                                    	
			                            <asp:Label ID="lblWechatUser" runat="server" Visible="False"></asp:Label>
			                                    	
			                    </td>				
							  
			                </tr> 
         <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<strong>受理</strong><b>代码:</b>
			                            <asp:Label ID="lblProcessCode" runat="server"></asp:Label>
			                    </td>				
							  
			                </tr> 
         
        
         <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>入网时间:</b>
			                            <asp:Label ID="lblJoinTime" runat="server"></asp:Label>
			                    </td>				
							  
			                </tr> 

        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>入网套餐:</b>
			                            
			                            <asp:Label ID="lblJoinMenu" runat="server"></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        
        
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<strong>投诉</strong><b>内容:</b>
			                            
			                            <asp:Label ID="lblComplainContent" runat="server"></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<strong>受理</strong><b>渠道编码:</b>
			                            
			                            <asp:Label ID="lblBranchCode" runat="server" ></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>受理渠道名称:</b>
			                            
			                            <asp:Label ID="lblBranchName" runat="server" ></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
          <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>回复截止时间:</b>
			                            
			                            <asp:Label ID="lblReplyTime" runat="server" ></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
          <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>备注:</b>
			                            
			                            <asp:Label ID="lblComment" runat="server" ></asp:Label>
			                            
			                    </td>				
							  
			                </tr> 
          <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>反馈记录:</b>
                                        <asp:Label ID="lblReplyHis" runat="server" ></asp:Label>
			                            
			                           
			                            
			                    </td>				
							  
			                </tr> 
        <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<b>反馈内容:<asp:RequiredFieldValidator ID="ContentRequiredFieldValidator1" runat="server" ControlToValidate="txtReplyContent" Display="Dynamic" ErrorMessage="内容不能为空" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </b>
                                        &nbsp;<br /><asp:TextBox ID="txtReplyContent" runat="server" Height="58px" Width="260px"></asp:TextBox>
			                            
			                           
			                            
			                    </td>				
							  
			                </tr> 

      

        <tr>
            <td  style="font-size: 25px;  color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	
			                        	                        	
			                    			                        	
			                                                        <asp:Button ID="btnSave" runat="server" Text="提交" style="font-size: 25px;   color: white; font-weight: bold;height: 40px; width: 40%;font-size: 16px;background-color:#ff6600" OnClick="btnSave_Click" />
			                    			                        	
                
			                        			                    </td>				
							  
			                </tr> 
           
            </table>
    </div>
    </form>
</body>
</html>
