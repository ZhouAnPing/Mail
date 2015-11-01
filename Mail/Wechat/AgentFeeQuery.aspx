<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentFeeQuery.aspx.cs" Inherits="Wechat.AgentFeeQuery" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0"/>
   
    <title>合作伙伴佣金告知单</title>
                                                                                                                                                                                                                                                                                                                                                                                                        
<style type="text/css" >
	
	body	 {width: 100%; background-color: #ffffff; margin:0px; padding:0px; -webkit-font-smoothing: antialiased;font-family: Microsoft YaHei, Calibri, Arival}
	table {border-collapse: collapse; margin:0px;padding:0px;}
    div {
        margin:0px;
        
    }
	
</style>
</head>
<body  style="font-family: Microsoft YaHei, Calibri, Arival">
    <div>
    <form id="form1" runat="server">
   
    
        <!-- Wrapper -->
       <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
	<tr>
		<td width="100%" valign="top" bgcolor="#ffffff" style="padding-top:10px">
		
			
			
			<!-- One Column -->
			
			<table width="100%"  class="deviceWidth" border="0"  cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
				
               
                <tr>
                    <td style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px 16px 16px 16px" bgcolor="#ffffff">
                        <table width="100%"  class="deviceWidth" border="1" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        	<tr>
			                    <td  style="font-size: 18px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: bottom; padding:0px; background-color: #ffffff;" bgcolor="#ffffff" colspan="2" >
                                              <a href="#"><img src="./ChinaUnicom.png" width="60px" alt="" border="0"  /></a>			              
                                              <b style="font-size: 16px; text-align: center">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;上海联通合作伙伴佣金告知单</b>

			                    </td>
		       
			                </tr> 

                        	<tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#DEDBDE">
			                        	告知单编号：		
			                    </td>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#ffffff">
			                        	<asp:Label ID="lblFeeSeq" runat="server"></asp:Label>
			                    </td>           

			                    
			                </tr>

                            <tr>
                                 <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#DEDBDE">
			                        	佣金账期：	
			                    </td>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#ffffff">
			                        	<asp:Label ID="lblFeeMonth" runat="server"></asp:Label></td>
                            </tr> 

			                <tr>
			                   
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#DEDBDE">
			                        	合作伙伴编号：		
			                    </td>
			                     <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#ffffff">
			                        	<asp:Label ID="lblAgentNo" runat="server"></asp:Label>
			                    </td>
			                    
			                </tr> 

                                 <tr>
 <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#DEDBDE">
			                        	合作伙伴名称：	
			                    </td>
			                    <td style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#ffffff">
			                        	<asp:Label ID="lblAgentName" runat="server"></asp:Label>
			                    </td>

                                 </tr>
			                <tr>
			                   
			                    
			                    
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#DEDBDE">
			                        	渠道类型：		
			                    </td>
			                     <td colspan="3" style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#ffffff">
			                        	<asp:Label ID="lblAgentType" runat="server"></asp:Label>
			                    </td>
			                    
			                </tr>              
						</table>
                       			
                       	 <table width="100%"  class="deviceWidth" border="1" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        	<tr>
			                    <td  style="font-size: 16px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#006699">
			                        	（一）佣金明细		
			                    </td>				
							  
			                </tr> 
			                <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:5px" bgcolor="white">
			                        	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound" >
                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                <Columns>                                                    
                                                    <asp:BoundField DataField="seq" HeaderText="序号" ReadOnly="True"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" />
                                                    <asp:BoundField DataField="feeName" HeaderText="佣金类型" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black"/>
                                                    <asp:BoundField DataField="fee" HeaderText="金额" ReadOnly="True"  ItemStyle-BorderWidth="1" ItemStyle-BorderColor="Black" ItemStyle-HorizontalAlign="Right"/>
                                                                                                       
                                                </Columns>
                                                <RowStyle ForeColor="#000066" BorderWidth="1" BorderColor="Black" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                                      
                                            </asp:GridView>
			                    </td>				
							  
			                </tr> 
			                <tr>
			                    <td  style="font-size: 16px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#006699">
			                        	（二）本月佣金说明	
			                    </td>				
							  
			                </tr> 
			                <tr>
			                    <td  style="font-size: 16px; color: black; font-weight:bold; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: middle; padding:0px" bgcolor="white">
			                        		
										
										 <asp:Label ID="lblAgentComment" runat="server"></asp:Label>
                                               
			                    </td>				
							  
			                </tr>  
			                <tr>
			                    <td  style="font-size: 16px; color: white; font-weight: bold; text-align: center; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#006699">
			                        	（三）备注说明
			                    </td>				
							  
			                </tr>  
			                 <tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="white">
			                        	<p>1、本通知单仅用于佣金告知，不作为任何凭证使用，实际佣金以支付金额为准；<br/>
										2、本通知单仅包含公众渠道下产生的佣金，不含其它渠道佣金；<br/>
										3、如对本月佣金有疑问，请请于30日内与对应区县佣金管理员联系；<br/>
										4、本通知单作为商业秘密，请合作伙伴妥善保管，如信息泄露则按相应合同条款追究责任。<br /></p>
			                    </td>				
							  
			                </tr>             
						</table>
						
						  <table width="100%"  class="deviceWidth" border="1" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                        	<tr>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#DEDBDE">
			                        	发送人：		
			                    </td>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#ffffff">
			                        	上海联通销售部		
			                    </td>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#DEDBDE">
			                        	查询日期：	
			                    </td>
			                    <td  style="font-size: 14px; color: black; font-weight: normal; text-align: left; font-family: Microsoft YaHei, Calibri, Arival; line-height: normal; vertical-align: top; padding:0px" bgcolor="#ffffff">
			                        	<asp:Label ID="lblSendDate" runat="server"></asp:Label>
&nbsp;</td>
			                    
			                </tr> 
			                            
						</table>
						
                    </td>
                </tr>              
			</table><!-- End One Column -->
            </td>
            </tr>
     </table>
     
    </form>

    </div>

</body>
</html>
