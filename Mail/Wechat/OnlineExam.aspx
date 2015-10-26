<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineExam.aspx.cs" Inherits="Wechat.OnlineExam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0"/>
    <title>在线考试</title>
   
</head>
<body>
    
    <form id="form1" runat="server">
        
    <div style="text-align:center; background-color:white; color:black; font-size:large" ><asp:label id="lblUserId" runat="server" text="" Font-Bold="true" Visible="false"></asp:label><asp:label id="lblExam" runat="server" text="考试名称" Font-Bold="true"></asp:label><asp:label id="lblExamSeq" runat="server" text="" Font-Bold="true" Visible="false"></asp:label>
        <br />
        <asp:Label ID="lblMessgage" runat="server" ForeColor="Blue"></asp:Label>
        </div>
        <div>
        <asp:gridview id="dgSingle" runat="server" width="100%" BorderStyle="None" 
            autogeneratecolumns="false" OnRowDataBound="dgSingle_RowDataBound"   >
            <columns>
                <asp:templatefield>
                <headertemplate>
                    <asp:label id="label5" runat="server" text="一、单项选择题"></asp:label>
                    
                </headertemplate>
                    <itemtemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan='3'>
                                    <asp:label id="label1" Font-Bold="true" runat="server" text='<%#Container.DataItemIndex+1%>'></asp:label>
                                    <asp:label id="lblSingleQuestion" Font-Bold="true" runat="server" text='<%#Eval("question")%>'></asp:label>
                                    <asp:label id="label3" runat="server" text='<%#Eval("answer")%>' visible="true" ForeColor="Blue"></asp:label>
                                    <asp:label id="lblQuestionnSeq" runat="server" text='<%#Eval("seq")%>' visible="false"></asp:label>
                                    <asp:label id="label2" runat="server" text='<%#Eval("standardAnswer")%>' visible="true" ForeColor="red"></asp:label>
                                    </td>
                            </tr>
                            <tr>
                                
                                <td style="width:35%;">
                                    
                                    <asp:RadioButton id="rdoSingleAnswer1" runat="server" text='<%#Eval("a")%>'  GroupName="s1" ValidationGroup="s1"/>
                                </td>
                                <td style="width:35%;">
                                    <asp:RadioButton id="rdoSingleAnswer2" runat="server" text='<%#Eval("b")%>' GroupName="s1" ValidationGroup="s1"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:35%;">
                                    <asp:RadioButton id="rdoSingleAnswer3" runat="server" text='<%#Eval("c")%>' GroupName="s1" ValidationGroup="s1"/>
                                </td>
                                <td style="width:35%;">
                                    <asp:RadioButton id="rdoSingleAnswer4" runat="server" text='<%#Eval("d")%>' GroupName="s1"/>
                                </td>
                            </tr>
                             <tr>
                                <td style="width:35%;">
                                    <asp:RadioButton id="rdoSingleAnswer5" runat="server" text='<%#Eval("e")%>' GroupName="s1"/>
                                </td>
                                <td style="width:35%;">

                                    <asp:RadioButton id="rdoSingleAnswer6" runat="server" text='<%#Eval("f")%>'  GroupName="s1"/>
                                  
                                </td>
                            </tr>
                        </table>
                    </itemtemplate>
                   <HeaderStyle HorizontalAlign="left" BackColor="#006699" Font-Bold="True" ForeColor="White"/>
                </asp:templatefield>
            </columns>
            
        </asp:gridview>
      <asp:gridview id="dgMulti" runat="server" width="100%" BorderStyle="None"
            autogeneratecolumns="false">
            <columns>
                <asp:templatefield>
                <headertemplate>
                   <asp:label id="label5" runat="server" text="二、多项选择题"></asp:label>
                    
                </headertemplate>
                    <itemtemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan='3' >
                                    <asp:label id="label8" Font-Bold="true" runat="server" text='<%# Container.DataItemIndex+1%>'></asp:label>
                                    <asp:label id="label9" Font-Bold="true" runat="server" text='<%#Eval("question")%>'></asp:label>
                                    <asp:label id="label10" runat="server" text='<%#Eval("answer")%>' visible="true" ForeColor="Blue"></asp:label>
                                      <asp:label id="lblQuestionnSeq" runat="server" text='<%#Eval("seq")%>' visible="false"></asp:label>
                                    <asp:label id="label2" runat="server" text='<%#Eval("standardAnswer")%>' visible="true" ForeColor="red"></asp:label>
                                    </td>
                            </tr>
                            <tr>
                                <td style="width:35%;">
                                    <asp:checkbox id="checkbox1" runat="server" text='<%#Eval("a")%>'/>
                                </td>
                                <td style="width:35%;">
                                    <asp:checkbox id="checkbox2" runat="server" text='<%#Eval("b")%>'/>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:35%;">
                                    <asp:checkbox id="checkbox3" runat="server" text='<%#Eval("c")%>'/>
                                </td>
                                <td style="width:35%;">
                                    <asp:checkbox id="checkbox4" runat="server" text='<%#Eval("d")%>'/>
                                </td>
                            </tr>
                              <tr>
                                <td style="width:35%;">
                                    <asp:checkbox id="checkbox5" runat="server" text='<%#Eval("e")%>'/>
                                </td>
                                <td style="width:35%;">
                                    <asp:checkbox id="checkbox6" runat="server" text='<%#Eval("f")%>'/>
                                </td>
                            </tr>
                        </table>
                    </itemtemplate>
                   <HeaderStyle HorizontalAlign="left" BackColor="#006699" Font-Bold="True" ForeColor="White"/>
                </asp:templatefield>
            </columns>
              
        </asp:gridview>
       <asp:gridview id="dgJudge" runat="server" width="100%" BorderStyle="None"
            autogeneratecolumns="false">
            <columns>
                <asp:templatefield>
                <headertemplate>
                    <asp:label id="label5" runat="server" text="三、判断题"></asp:label>
                   
                </headertemplate>
                    <itemtemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan='3'>
                                    <asp:label Font-Bold="true" id="label15" runat="server" text='<%# Container.DataItemIndex+1%>'></asp:label>
                                    <asp:label Font-Bold="true" id="label16" runat="server" text='<%#Eval("question")%>'></asp:label>
                                    <asp:label id="label17" runat="server" text='<%#Eval("answer")%>' visible="true" ForeColor="Blue"></asp:label>
                                      <asp:label id="lblQuestionnSeq" runat="server" text='<%#Eval("seq")%>' visible="false"></asp:label>
                                <asp:label id="label2" runat="server" text='<%#Eval("standardAnswer")%>' visible="true" ForeColor="red"></asp:label>    
                                </td>
                            </tr>
                            <tr>
                                <td style="width:35%;">
                                    <asp:RadioButton id="radiobutton5" runat="server" text="正确"/>
                                </td>
                                <td style="width:35%;">
                                    <asp:RadioButton id="radiobutton6" runat="server" text="错误"/>
                                </td>
                            </tr>
                        </table>
                    </itemtemplate>
                    <HeaderStyle HorizontalAlign="left" BackColor="#006699" Font-Bold="True" ForeColor="White"/>
                </asp:templatefield>
            </columns>
              
        </asp:gridview>
           
        <asp:Label ID="lblMessgage0" runat="server" ForeColor="Blue"></asp:Label>
           
       <br />
            <table style="width:100%;">
                            <tr>
                                <td >
        <asp:button id="btnSave" runat="server" text="保存" 
             Height="38px" Width="135px" OnClick="btnSave_Click"  style="font-size: 25px;  color: white; font-weight: bold;height: 54px; width: 100%;font-size: 16px;background-color:#006699" /></td>
<td>
        <asp:button id="btnSubmit" runat="server" text="交卷" 
            onclientclick='return confirm("确定交卷吗?")' Height="38px" OnClick="btnSubmit_Click" Width="135px"  style="font-size: 25px;  color: white; font-weight: bold;height: 54px; width: 100%;font-size: 16px;background-color:#006699"/>
                                <td>
			                        	
			                        <input type="button" onclick="javascript: window.history.go(-1);"value="<<返回上一页" style="font-size: 25px;  color: white; font-weight: bold;height: 54px; width: 100%;font-size: 16px;background-color:#006699"/></td>
                                </tr>

                                </table>
    </div>
    </form>
</body>
</html>




