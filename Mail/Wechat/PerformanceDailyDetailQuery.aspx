<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceDailyDetailQuery.aspx.cs" Inherits="Wechat.PerformanceDailyDetailQuery" %>

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
			                    <td  style="font-size: 25px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: 45px; vertical-align: top; padding:0px" bgcolor="#006699">
                                         <asp:Label ID="lblFeeMonth" runat="server"></asp:Label>
			                    </td>				
							  
			                </tr>
			                <tr>
			                    <td  style="font-size: 20px; color: black; font-weight: normal; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowHeader="false"
                                                ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound" >
                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                <Columns>  
                                                    <asp:BoundField DataField="name" HeaderStyle-Width="50%" HeaderText=""  ReadOnly="True"  ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" ItemStyle-Wrap="true" />         
                                                    <asp:BoundField DataField="value" HeaderText="" ReadOnly="True"  ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" ItemStyle-HorizontalAlign="right"/>
                                                                                                                                                        
                                                </Columns>
                                                <RowStyle ForeColor="#000066" BorderWidth="1" BorderColor="Black" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                                      
                                            </asp:GridView>
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
