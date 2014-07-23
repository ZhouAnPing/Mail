<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ page import="org.gsm.oneapi.foundation.JSPUtils"%>    
<%@ page import="org.gsm.oneapi.sms.SMSRetrieve"%>
<%@ page import="org.gsm.oneapi.responsebean.sms.RetrieveSMSResponse"%>
<%@ page import="org.gsm.oneapi.responsebean.sms.InboundSMSMessage"%>
<%@ page import="org.gsm.oneapi.endpoints.LocalSandboxEndpoints"%>
<%@ page import="org.gsm.oneapi.endpoints.ServiceEndpoints"%>
<%@ page import="org.gsm.oneapi.foundation.JSONRequest"%>

<%
	response.setHeader("Cache-Control","no-cache");
	response.setHeader("Pragma","no-cache"); 
	response.setDateHeader ("Expires", 0); 

	String submit=JSPUtils.nullOrTrimmed(request.getParameter("submit"));
	String username=JSPUtils.nullOrTrimmed(request.getParameter("username"));
	String password=JSPUtils.nullOrTrimmed(request.getParameter("password"));
	String endpoint=JSPUtils.nullOrTrimmed(request.getParameter("endpoint"));
	
	String registrationId=JSPUtils.nullOrTrimmed(request.getParameter("registrationId"));
	int maxBatchSize=JSPUtils.parseInt(request.getParameter("maxBatchSize"));
	
	LocalSandboxEndpoints sandboxEndpoints=new LocalSandboxEndpoints();

	RetrieveSMSResponse smsResponse=null;
	InboundSMSMessage[] messageList=null;
	
	if (submit==null) {
		
		registrationId="3456";
		maxBatchSize=2;
		
		username="Fred.Jones";
		password="1234";
		endpoint=sandboxEndpoints.getRetrieveSMSEndpoint();

	} else {
		sandboxEndpoints.setRetrieveSMS(endpoint);

		SMSRetrieve me=new SMSRetrieve(sandboxEndpoints, username, password);
		
		smsResponse=me.retrieveMessages(registrationId, maxBatchSize);	
		
		if (smsResponse!=null && smsResponse.getInboundSMSMessageList()!=null) messageList=smsResponse.getInboundSMSMessageList().getInboundSMSMessage();
	}
%>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
  <head>    
    <title>
      GSM World | OneAPI Toolkit
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="GSM, GSMA, GSM Association, Mobile, Mobile World Congress, Mobile Asia Congress, Mobile Awards, Global Mobile Awards, 3GSM, Mobile Broadband, Spectrum, Development Fund, Mobile Money, Mobile Innovation, Pathfinder, Open Connectivity, Fraud" />
    <meta name="description" content="The GSMA represents the interests of the worldwide mobile communications industry. Spanning 219 countries, the GSMA unites more than 750 of the world&rsquo;s mobile operators, as well as 200 companies in the broader mobile ecosystem." />
    <link rel="stylesheet" href="screen.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="toolkit.css" type="text/css" media="screen" />

    <link rel="shortcut icon" href="#" />
</head>

  <body id="home">

    <div id="container">

      <div id="masthead">
        <p id="logo">
          <a href="index.html">GSM World</a>
        </p>
        <p id="strap-line">
          <strong>Connecting the World</strong>
        </p>
      </div>
      
<div id="content">

	<%@ include file="mainmenu.inc" %>
        
        <div class="col-760">
        

        <div id="other-main">
          <div class="other-top"></div>
          <div class="other-bottom">
            <div class="left">
          <h3>
            Inbound SMS API - Retrieve SMS Messages
          </h3>
          <p>
      
		</p>
		
		<form method="post" action="smsretrieve.jsp">
			<div class="ParameterGroupHeading"><strong>Mandatory Parameters</strong></div>
			<div class="ParameterRow">
				<div class="ParameterLabel">registrationId</div>
				<div class="ParameterValue"><input type="text" name="registrationId" value="<%if (registrationId!=null) out.print(registrationId); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">maxBatchSize</div>
				<div class="ParameterValue"><input type="text" name="maxBatchSize" value="<%=maxBatchSize%>"/></div>
			</div>
			<div class="ParameterGroupHeading"><strong>Optional Parameters</strong></div>
			<div class="ParameterRow">
				<div class="ParameterLabel">&nbsp;</div>
				<div class="ParameterValue"><em>None</em></div>
			</div>
			<div class="ParameterGroupHeading"><strong>Service Endpoint Information</strong></div>
			<div class="ParameterRow">
				<div class="ParameterLabel">Username</div>
				<div class="ParameterValue"><input type="text" name="username" value="<%if (username!=null) out.print(username); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">Password</div>
				<div class="ParameterValue"><input type="text" name="password" value="<%if (password!=null) out.print(password); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">Service Endpoint</div>
				<div class="ParameterValue"><textarea name="endpoint" style="width:400px;height:80px;"><%if (endpoint!=null) out.print(endpoint); %></textarea></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">&nbsp;</div>
				<div class="ParameterValue"><hr width="50%"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">&nbsp;</div>
				<div class="ParameterValue"><input type="submit" name="submit" value="Submit"/></div>
			</div>

		</form>
          
          <% if (smsResponse!=null) { %>
          <h3>Response:</h3>
          	<div class="ParameterGroupHeading"><strong>Response Code</strong></div>          
          	<div class="ParameterRow">
				<div class="ParameterLabel">httpResponseCode</div>
				<div class="ParameterValue"><%=smsResponse.getHTTPResponseCode() %></div>
			</div>
          	<div class="ParameterRow">
				<div class="ParameterLabel">contentType</div>
				<div class="ParameterValue"><%=smsResponse.getContentType()%></div>				
			</div>
          	<div class="ParameterRow">
				<div class="ParameterLabel">location</div>
				<div class="ParameterValue"><%=smsResponse.getLocation()%></div>				
			</div>
          	<div class="ParameterRow">
				<div class="ParameterLabel">smsResponse.toString()</div>
				<div class="ParameterValue"><%=smsResponse.toString()%></div>				
			</div>
			<% if (smsResponse.getRequestError()!=null) { %>
          		<div class="ParameterGroupHeading"><strong>Request Error</strong></div>
          		<% if (smsResponse.getRequestError().getPolicyException()!=null) { %>          
		          	<div class="ParameterRow">
						<div class="ParameterLabel">exceptionType</div>
						<div class="ParameterValue">POLICY EXCEPTION</div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">messageId</div>
						<div class="ParameterValue"><%=smsResponse.getRequestError().getPolicyException().getMessageId()%></div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">text </div>
						<div class="ParameterValue"><%=smsResponse.getRequestError().getPolicyException().getText()%></div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">variables</div>
						<div class="ParameterValue"><%=smsResponse.getRequestError().getPolicyException().getVariables()%></div>				
					</div>
				<% } 
          		if (smsResponse.getRequestError().getServiceException()!=null) { %>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">exceptionType</div>
						<div class="ParameterValue">SERVICE EXCEPTION</div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">messageId</div>
						<div class="ParameterValue"><%=smsResponse.getRequestError().getServiceException().getMessageId()%></div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">text </div>
						<div class="ParameterValue"><%=smsResponse.getRequestError().getServiceException().getText()%></div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">variables</div>
						<div class="ParameterValue"><%=smsResponse.getRequestError().getServiceException().getVariables()%></div>				
					</div>
				<% } %>
			<% } %>
			<% if (smsResponse.getInboundSMSMessageList()!=null) { %>
          		<div class="ParameterGroupHeading"><strong>inboundSMSMessageList</strong></div>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">numberOfMessagesInThisBatch</div>
					<div class="ParameterValue"><%=smsResponse.getInboundSMSMessageList().getNumberOfMessagesInThisBatch()%></div>				
				</div>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">totalNumberOfPendingMessages</div>
					<div class="ParameterValue"><%=smsResponse.getInboundSMSMessageList().getTotalNumberOfPendingMessages()%></div>				
				</div>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">resourceURL</div>
					<div class="ParameterValue"><%=smsResponse.getInboundSMSMessageList().getResourceURL()%></div>				
				</div>
				<% if (messageList!=null && messageList.length>0) {
					for (int i=0; i<messageList.length; i++) {
						if (messageList[i]!=null) {
							
							%>
				          	<div class="ParameterRow">
								<div class="ParameterLabel">&nbsp;</div>
								<div class="ParameterValue">&nbsp;</div>				
							</div>
				          	<div class="ParameterRow">
								<div class="ParameterLabel">messageId [<%=i%>]</div>
								<div class="ParameterValue"><%=messageList[i].getMessageId()%></div>				
							</div>
				          	<div class="ParameterRow">
								<div class="ParameterLabel">destinationAddress [<%=i%>]</div>
								<div class="ParameterValue"><%=messageList[i].getDestinationAddress()%></div>				
							</div>
				          	<div class="ParameterRow">
								<div class="ParameterLabel">senderAddress [<%=i%>]</div>
								<div class="ParameterValue"><%=messageList[i].getSenderAddress()%></div>				
							</div>
				          	<div class="ParameterRow">
								<div class="ParameterLabel">dateTime [<%=i%>]</div>
								<div class="ParameterValue"><%=messageList[i].getDateTime()%></div>				
							</div>
				          	<div class="ParameterRow">
								<div class="ParameterLabel">message [<%=i%>]</div>
								<div class="ParameterValue"><%=messageList[i].getMessage()%></div>				
							</div>
				          	<div class="ParameterRow">
								<div class="ParameterLabel">resourceURL [<%=i%>]</div>
								<div class="ParameterValue"><%=messageList[i].getResourceURL()%></div>				
							</div>
							<%
						}
					}
				} %>				
	          	<div class="ParameterRow">
					<div class="ParameterLabel">&nbsp;</div>
					<div class="ParameterValue">&nbsp;</div>				
				</div>
			<% } %>
          
          <% } %>

		    <div class="ParameterRow">
				<div class="ParameterLabel">&nbsp;</div>
				<div class="ParameterValue">&nbsp;</div>				
			</div>

        </div></div></div>
        
			</div>

			

      </div>
      
		<%@ include file="footer.inc" %>

    </div>
      


</body>
</html>
