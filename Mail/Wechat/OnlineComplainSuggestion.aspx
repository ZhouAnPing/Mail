<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineComplainSuggestion.aspx.cs" Inherits="Wechat.OnlineComplainSuggestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="100%"  class="deviceWidth" border="0" cellpadding="5px" cellspacing="5px" align="center" bgcolor="#ffffff">
                        	<tr>
			                    <td colspan="2" style="font-size: 20px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: 45px; vertical-align: top; padding:0px" bgcolor="#006699">
                                         <asp:Label ID="lblUserId" runat="server" Visible="False"></asp:Label>
                                         <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                         <asp:Label ID="lblAgentNo" runat="server" Visible="False"></asp:Label>
			                    </td>				
							  
			                </tr> 
          <tr>
                 <td  style="font-size: 16px; width:20%;color:  black; background-color:white; font-weight: bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height:normal; vertical-align: top; padding:0px" ><asp:Label ID="Label3" runat="server" Text="主题(*):"></asp:Label>
                     <asp:RequiredFieldValidator ID="SubjectRequiredFieldValidator2" runat="server" ControlToValidate="txtSubject" Display="Dynamic" ErrorMessage="主题不能为空" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                 </td>   
                 <td   style="font-size: 16px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px">
                     <asp:TextBox  ID="txtSubject"  Width="95%" runat="server" Height="40px"></asp:TextBox>
                     <br />
                 </td>               
            </tr>
             <tr>
                 <td  style="font-size: 16px; width:20%;  color:  black; background-color:white; font-weight: bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height:normal; vertical-align: top; padding:0px" ><asp:Label ID="Label2" runat="server" Text="内容(*):"></asp:Label>
                     <asp:RequiredFieldValidator ID="ContentRequiredFieldValidator1" runat="server" ControlToValidate="txtContent" Display="Dynamic" ErrorMessage="内容不能为空" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                 </td>   
                 <td   style="font-size: 16px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px">
                     <asp:TextBox ID="txtContent" runat="server" Height="300px" TextMode="MultiLine" Width="95%"></asp:TextBox>
                 <br />
                 </td>               
            </tr>
           <tr>
                 <td  style="font-size: 16px; width:20%;color:  black; background-color:white; font-weight: bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height:normal; vertical-align: top; padding:0px" ><asp:Label ID="Label1" runat="server" Text="涉及部门:"></asp:Label></td>   
                 <td   style="font-size: 16px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px">
                     <asp:DropDownList ID="ddlDepartment" runat="server" Height="40px" Width="95%">
                         <asp:ListItem>销售部</asp:ListItem>
                         <asp:ListItem>集客部</asp:ListItem>
                         <asp:ListItem>品监部</asp:ListItem>
                     </asp:DropDownList>
                 </td>               
            </tr>
          <tr>
                  <td></td>
                 <td  style="font-size: 16px; color:  black; background-color:white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height:normal; vertical-align: middle; padding:0px" >
                     <br />
                     <asp:Button ID="btnSubmit" runat="server"  Width="90%" Text="提交" Height="38px" OnClick="btnSubmit_Click"  BackColor="#006699"/>
                     <br />
                     <br />
                 </td>   
                              
            </tr>
          <tr>
			                    <td colspan="2" style="font-size: 18px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: 30px; vertical-align: top; padding:0px" bgcolor="#006699">
                                         <asp:Label ID="lblMessage" runat="server"></asp:Label>
			                    </td>				
							  
			                </tr> 
          </table>
    </div>
        <br />
    </form>
</body>
</html>
