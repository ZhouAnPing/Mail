<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessPolicyQuery.aspx.cs" Inherits="Wechat.BusinessPolicyQuery" %>

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
			                    <td  style="font-size: 16px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#006699">
			                        	通知列表</td>				
							  
			                </tr> 
			                <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound" >
                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                <Columns>     
                                                    <asp:BoundField DataField="seq" HeaderText="编号" ReadOnly="True"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" />
                                                    <asp:BoundField DataField="createTime" HeaderText="发布时间" ReadOnly="True"  ItemStyle-BorderWidth="1" HeaderStyle-Width="10%" ItemStyle-BorderColor="Black" ItemStyle-HorizontalAlign="Right"/>
                                                    <asp:BoundField DataField="content" HeaderText="内容" ReadOnly="True"  ItemStyle-HorizontalAlign="Left"  ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" />
                                                   <asp:HyperLinkField DataNavigateUrlFields="seq" DataNavigateUrlFormatString="http://localhost:59032/BusinessPolicyDetail.aspx?seq={0}" DataTextField="subject" HeaderText="标题"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black"/>
                                                 
                                                     <asp:HyperLinkField DataNavigateUrlFields="seq" DataNavigateUrlFormatString="http://localhost:59032/BusinessPolicyAttachmentDetail.aspx?seq={0}" DataTextField="attachment" HeaderText="附件"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black"/>
                                                    
                                                                         
                                                </Columns>
                                                <RowStyle ForeColor="#000066" BorderWidth="1" BorderColor="Black" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />

                                                                                      
                                            </asp:GridView>
			                    </td>				
							  
			                </tr> 
            </table>
    </div>
    </form>
</body>
</html>
