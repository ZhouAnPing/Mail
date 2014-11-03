<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorCodeQuery.aspx.cs" Inherits="Wechat.ErrorCodeQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 30%;
            height: 39px;
        }
        .auto-style2 {
            width: 70%;
            height: 39px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       
    <div>
    
        <table style="border-style: solid; width:100%; font-size:35px; border-width:5px">
           
            <tr>
                  <td>
                 
                </td>   
                <td>
                     <h1 style="align-content:center">错误查询结果</h1>
                </td> 
                </tr>
                 <tr>    
                <td>
                    <asp:Label ID="Label1" runat="server" Text="报错图片:"></asp:Label>
                </td>   
                <td>
                    <asp:Image ID="Image1" runat="server" />
                </td>               
            </tr>
            <tr>
                 <td class="auto-style1"><asp:Label ID="Label2" runat="server" Text="报错关键字:"></asp:Label></td>   
                 <td class="auto-style2"><asp:Label ID="lblKeyword" runat="server" Text=""></asp:Label></td>               
            </tr>
            <tr>
                 <td><asp:Label ID="Label3" runat="server" Text="报错描述:"></asp:Label></td> 
                 <td><asp:Label ID="lblErrerDesc" runat="server" Text=""></asp:Label></td>                 
            </tr>
            <tr>
                 <td><asp:Label ID="Label4" runat="server" Text="原因及处理方法:"></asp:Label></td>
                <td><asp:Label ID="lblErrorSolution" runat="server" Text=""></asp:Label></td>    
                
            </tr>
             <tr>
                <td><asp:Label ID="Label5" runat="server" Text="联系方式:"></asp:Label></td>
                 <td><asp:Label ID="lblContact" runat="server" Text=""></asp:Label></td>  
                
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
