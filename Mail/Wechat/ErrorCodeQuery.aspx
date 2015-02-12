<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorCodeQuery.aspx.cs" Inherits="Wechat.ErrorCodeQuery" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

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
    
        <table border="0"  style="border-style: solid; width:100%; font-size:14px; border-width:5px;padding:5px; border-color:#006699;margin:5px;">
           <caption style="font-size: 14px; color: white; background-color:#006699; font-weight: bold; text-align:center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px"><b><br />错误查询结果<br /><br /></b></caption>  
          
             <tr>    
                
                    
                 <td colspan="2" style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px">
                    
               
                   <asp:Image ID="Image1" runat="server" ImageAlign="Left"  width="100%"  /></td>               
            </tr>
            <tr>
                <td style="color:  black; background-color:white;">&nbsp; &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                 <td  style="font-size: 14px; color:  black; background-color:white; font-weight: bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height:normal; vertical-align: top; padding:0px" ><asp:Label ID="Label2" runat="server" Text="报错关键字:"></asp:Label></td>   
                 <td   style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px"><asp:Label ID="lblKeyword" runat="server" Text=""></asp:Label></td>               
            </tr>
            <tr>
                <td style="color:  black; background-color:white;">&nbsp; &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                 <td  style="font-size: 14px; color:  black; background-color:white; font-weight: bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px"><asp:Label ID="Label3" runat="server" Text="报错描述:"></asp:Label></td> 
                 <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px"><asp:Label ID="lblErrerDesc" runat="server" Text=""></asp:Label></td>                 
            </tr>
            <tr>
                 <td style="color:  black; background-color:white;">&nbsp; &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                 <td  style="font-size: 14px; color:  black; background-color:white; font-weight: bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px"><asp:Label ID="Label4" runat="server" Text="原因及处理方法:"></asp:Label></td>
                <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px"><asp:Label ID="lblErrorSolution" runat="server" Text=""></asp:Label></td>    
                
            </tr>
            <tr>
                 <td style="color:  black; background-color:white;">&nbsp; &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td  style="font-size: 14px; color:  black; background-color:white; font-weight: bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px"><asp:Label ID="Label5" runat="server" Text="联系方式:"></asp:Label></td>
                 <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px"><asp:Label ID="lblContact" runat="server" Text=""></asp:Label></td>  
                
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
