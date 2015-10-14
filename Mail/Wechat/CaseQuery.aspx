<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaseQuery.aspx.cs" Inherits="Wechat.CaseQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%" class="deviceWidth" border="0" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        <tr>
                            <td style="font-size: 20px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding: 0px" bgcolor="#006699">
                                 <asp:Label ID="lblAgentNo" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblTitle" runat="server" Text="投诉典型案例"></asp:Label>
                               
                                 <asp:Label ID="lblUserId" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class="deviceWidth" border="0" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        <tr>
                            <td style="font-size: 16px; color: black; font-weight: bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding: 0px" bgcolor="white">

                              
                                <asp:TextBox  ID="txtCondition" runat="server" Width="60%" Height="32px"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" Text="搜索" Width="25%" Font-Bold="True" Font-Size="16px" OnClick="btnSearch_Click" Height="36px"   />
                            </td>

                        </tr>
                    </table>
                 

                    <table width="100%" class="deviceWidth" border="0" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        <tr>
                            <td style="font-size: 16px; color: black; font-weight: normal; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding: 5px" bgcolor="white">
                                <asp:Label ID="lblMessag" runat="server" ForeColor="Red"></asp:Label>   
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            正在加载数据.............
       
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" PageSize="30" AllowPaging="True"
                                    OnPageIndexChanging="GridView1_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="seq" HeaderStyle-BorderWidth="0" HeaderText="编号" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="0" ItemStyle-BorderColor="Black" />
                                       <asp:BoundField DataField="userId" HeaderStyle-BorderWidth="0" HeaderText="登录用户" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="0" ItemStyle-BorderColor="Black" />

                                       
                                        <asp:HyperLinkField DataNavigateUrlFields="seq,userId" HeaderStyle-BorderWidth="0" DataNavigateUrlFormatString="./CaseDetail.aspx?seq={0}&userId={1}" DataTextField="content" HeaderText="投诉典型案例" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="0" ItemStyle-BorderColor="Black" />

                                      


                                    </Columns>
                                    <RowStyle ForeColor="#000066" BorderWidth="1" BorderColor="Black" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="right" Font-Size="Larger" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />


                                </asp:GridView>
                                
                            </td>
                        </tr>
                    </table>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>




        </div>
    </form>
</body>
</html>
