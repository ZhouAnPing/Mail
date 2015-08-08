<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceSummaryQuery.aspx.cs" Inherits="Wechat.PerformanceSummaryQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table width="100%"  class="deviceWidth" border="1" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        	<%--<tr>
			                    <td  style="font-size: 30px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: 45px; vertical-align: top; padding:0px" bgcolor="#006699">
                                         <asp:Label ID="lblFeeMonth" runat="server"></asp:Label>
			                    </td>				
							  
			                </tr> --%>
			                <tr>
			                    <td  style="font-size: 16px; color: black; font-weight: normal; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound" >
                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                <Columns>  
                                                    <asp:BoundField DataField="branchNo" HeaderText="门店编号"  ReadOnly="True"  ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" ItemStyle-Wrap="true" />         
                                                     <asp:HyperLinkField DataNavigateUrlFields="branchNo" DataNavigateUrlFormatString="http://localhost:59032/BusinessPolicyDetail.aspx?branchNo={0}" DataTextField="branchName" HeaderText="门店名称"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black"/>

                                                   
                                                    <asp:BoundField DataField="fee1" HeaderText="费用1" ReadOnly="True"  ItemStyle-HorizontalAlign="right" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" />
                                                    <asp:BoundField DataField="fee2" HeaderText="费用2" ReadOnly="True" ItemStyle-HorizontalAlign="right" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black"/>
                                                    <asp:BoundField DataField="fee3" HeaderText="费用3" ReadOnly="True"  ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" ItemStyle-HorizontalAlign="right"/>
                                                    <asp:BoundField DataField="summary" HeaderText="小计" ReadOnly="True"  ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" ItemStyle-HorizontalAlign="right"/>
                                                                                                                                                        
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
