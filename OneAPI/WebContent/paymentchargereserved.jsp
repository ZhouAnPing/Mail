<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ page import="org.gsm.oneapi.foundation.JSPUtils"%>    
<%@ page import="org.gsm.oneapi.payment.Reservation"%>
<%@ page import="org.gsm.oneapi.responsebean.payment.PaymentResponse"%>
<%@ page import="org.gsm.oneapi.responsebean.payment.AmountReservationTransaction"%>
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
	
	String endUserId=JSPUtils.nullOrTrimmed(request.getParameter("endUserId"));
	String referenceCode=JSPUtils.nullOrTrimmed(request.getParameter("referenceCode"));
	String description=JSPUtils.nullOrTrimmed(request.getParameter("description"));
	String currency=JSPUtils.nullOrTrimmed(request.getParameter("currency"));
	double amount=JSPUtils.parseDouble(request.getParameter("amount"));
	int referenceSequence=JSPUtils.parseInt(request.getParameter("referenceSequence"));
	String code=JSPUtils.nullOrTrimmed(request.getParameter("code"));
	String callbackURL=JSPUtils.nullOrTrimmed(request.getParameter("callbackURL"));
	String clientCorrelator=JSPUtils.nullOrTrimmed(request.getParameter("clientCorrelator"));
	String onBehalfOf=JSPUtils.nullOrTrimmed(request.getParameter("onBehalfOf"));
	String purchaseCategoryCode=JSPUtils.nullOrTrimmed(request.getParameter("purchaseCategoryCode"));
	String channel=JSPUtils.nullOrTrimmed(request.getParameter("channel"));
	double taxAmount=JSPUtils.parseDouble(request.getParameter("taxAmount"));
	String serviceId=JSPUtils.nullOrTrimmed(request.getParameter("serviceId"));
	String productId=JSPUtils.nullOrTrimmed(request.getParameter("productId"));
	
	LocalSandboxEndpoints sandboxEndpoints=new LocalSandboxEndpoints();

	PaymentResponse paymentResponse=null;
	AmountReservationTransaction amountReservationTransaction=null;
	
	if (submit==null) {
		
		endUserId="tel:+123456789";
		referenceCode="REF-12345";
		description="Alien Invaders Game";
		currency="USD";
		amount=25.0;
		code="C528";
		clientCorrelator="54321";
		onBehalfOf="Example Games Inc.";
		purchaseCategoryCode="Game";
		channel="Wap";
		taxAmount=2.15;
		serviceId="SID1234";
		productId="PID00008765";
		referenceSequence=3;
		callbackURL=(request.isSecure()?"https://":"http://")+
					(request.getHeader("x-forwarded-host")!=null?request.getHeader("x-forwarded-host"):request.getHeader("host"))+
					(request.getContextPath()!=null?request.getContextPath():"")+
					"/paymentchargereservednotification.jsp";
		username="Fred.Jones";
		password="1234";
		endpoint=sandboxEndpoints.getAmountReservationChargeEndpoint();

	} else {
		sandboxEndpoints.setAmountReservationCharge(endpoint);

		Reservation me=new Reservation(sandboxEndpoints, username, password);
		
		paymentResponse=me.chargeAmount(endUserId, referenceCode, description, currency, amount, referenceSequence, code, callbackURL,
				clientCorrelator, onBehalfOf, purchaseCategoryCode, channel, taxAmount, serviceId, productId);
		
		if (paymentResponse!=null && paymentResponse.getAmountReservationTransaction()!=null) amountReservationTransaction=paymentResponse.getAmountReservationTransaction();				
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
            Payment API - Charge Against Reservation
          </h3>
          <p>
      
		</p>
		
		<form method="post" action="paymentchargereserved.jsp">
			<div class="ParameterGroupHeading"><strong>Mandatory Parameters</strong></div>
			<div class="ParameterRow">
				<div class="ParameterLabel">endUserId</div>
				<div class="ParameterValue"><input type="text" name="endUserId" value="<%if (endUserId!=null) out.print(endUserId); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">referenceCode</div>
				<div class="ParameterValue"><input type="text" name="referenceCode" value="<%if (referenceCode!=null) out.print(referenceCode); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">description</div>
				<div class="ParameterValue"><input type="text" name="description" value="<%if (description!=null) out.print(description); %>" style="width:300px"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">currency</div>
				<div class="ParameterValue"><input type="text" name="currency" value="<%if (currency!=null) out.print(currency); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">amount</div>
				<div class="ParameterValue"><input type="text" name="amount" value="<%=amount%>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">referenceSequence</div>
				<div class="ParameterValue"><input type="text" name="referenceSequence" value="<%=referenceSequence%>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">code</div>
				<div class="ParameterValue"><input type="text" name="code" value="<%if (code!=null) out.print(code); %>"/></div>
			</div>
			<div class="ParameterGroupHeading"><strong>Optional Parameters</strong></div>
			<div class="ParameterRow">
				<div class="ParameterLabel">callbackURL</div>
				<div class="ParameterValue"><textarea name="callbackURL" style="width:400px;height:80px;"><%if (callbackURL!=null) out.print(callbackURL); %></textarea></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">clientCorrelator</div>
				<div class="ParameterValue"><input type="text" name="clientCorrelator" value="<%if (clientCorrelator!=null) out.print(clientCorrelator); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">onBehalfOf</div>
				<div class="ParameterValue"><input type="text" name="onBehalfOf" value="<%if (onBehalfOf!=null) out.print(onBehalfOf); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">purchaseCategoryCode</div>
				<div class="ParameterValue"><input type="text" name="purchaseCategoryCode" value="<%if (purchaseCategoryCode!=null) out.print(purchaseCategoryCode); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">channel</div>
				<div class="ParameterValue"><input type="text" name="channel" value="<%if (channel!=null) out.print(channel); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">taxAmount</div>
				<div class="ParameterValue"><input type="text" name="taxAmount" value="<%=taxAmount %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">serviceId</div>
				<div class="ParameterValue"><input type="text" name="serviceId" value="<%if (serviceId!=null) out.print(serviceId); %>"/></div>
			</div>
			<div class="ParameterRow">
				<div class="ParameterLabel">productId</div>
				<div class="ParameterValue"><input type="text" name="productId" value="<%if (productId!=null) out.print(productId); %>"/></div>
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
          
          <% if (paymentResponse!=null) { %>
          <h3>Response:</h3>
          	<div class="ParameterGroupHeading"><strong>Response Code</strong></div>          
          	<div class="ParameterRow">
				<div class="ParameterLabel">httpResponseCode</div>
				<div class="ParameterValue"><%=paymentResponse.getHTTPResponseCode() %></div>
			</div>
          	<div class="ParameterRow">
				<div class="ParameterLabel">contentType</div>
				<div class="ParameterValue"><%=paymentResponse.getContentType()%></div>				
			</div>
          	<div class="ParameterRow">
				<div class="ParameterLabel">location</div>
				<div class="ParameterValue"><%=paymentResponse.getLocation()%></div>				
			</div>
          	<div class="ParameterRow">
				<div class="ParameterLabel">paymentResponse.toString()</div>
				<div class="ParameterValue"><%=paymentResponse.toString()%></div>				
			</div>
			<% if (paymentResponse.getRequestError()!=null) { %>
          		<div class="ParameterGroupHeading"><strong>Request Error</strong></div>
          		<% if (paymentResponse.getRequestError().getPolicyException()!=null) { %>          
		          	<div class="ParameterRow">
						<div class="ParameterLabel">exceptionType</div>
						<div class="ParameterValue">POLICY EXCEPTION</div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">messageId</div>
						<div class="ParameterValue"><%=paymentResponse.getRequestError().getPolicyException().getMessageId()%></div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">text </div>
						<div class="ParameterValue"><%=paymentResponse.getRequestError().getPolicyException().getText()%></div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">variables</div>
						<div class="ParameterValue"><%=paymentResponse.getRequestError().getPolicyException().getVariables()%></div>				
					</div>
				<% } 
          		if (paymentResponse.getRequestError().getServiceException()!=null) { %>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">exceptionType</div>
						<div class="ParameterValue">SERVICE EXCEPTION</div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">messageId</div>
						<div class="ParameterValue"><%=paymentResponse.getRequestError().getServiceException().getMessageId()%></div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">text </div>
						<div class="ParameterValue"><%=paymentResponse.getRequestError().getServiceException().getText()%></div>				
					</div>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">variables</div>
						<div class="ParameterValue"><%=paymentResponse.getRequestError().getServiceException().getVariables()%></div>				
					</div>
				<% } %>
			<% } %>
			<% if (amountReservationTransaction!=null) { %>
          		<div class="ParameterGroupHeading"><strong>amountReservationTransaction Response</strong></div>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">endUserId</div>
					<div class="ParameterValue"><%=amountReservationTransaction.getEndUserId()%></div>				
				</div>
				<% if (amountReservationTransaction.getPaymentAmount()!=null) { %>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">paymentAmount.amountReserved</div>
						<div class="ParameterValue"><%=amountReservationTransaction.getPaymentAmount().getAmountReserved()%></div>				
					</div>		
					<% if (amountReservationTransaction.getPaymentAmount().getChargingInformation()!=null) { %>
			          	<div class="ParameterRow">
							<div class="ParameterLabel">paymentAmount.chargingInformation.amount</div>
							<div class="ParameterValue"><%=amountReservationTransaction.getPaymentAmount().getChargingInformation().getAmount()%></div>				
						</div>
			          	<div class="ParameterRow">
							<div class="ParameterLabel">currency</div>
							<div class="ParameterValue"><%=amountReservationTransaction.getPaymentAmount().getChargingInformation().getCurrency()%></div>				
						</div>
			          	<div class="ParameterRow">
							<div class="ParameterLabel">paymentAmount.chargingInformation.description</div>
							<div class="ParameterValue"><%=amountReservationTransaction.getPaymentAmount().getChargingInformation().getDescription()%></div>				
						</div>		
					<% } %>
		          	<div class="ParameterRow">
						<div class="ParameterLabel">paymentAmount.totalAmountCharged</div>
						<div class="ParameterValue"><%=amountReservationTransaction.getPaymentAmount().getTotalAmountCharged()%></div>				
					</div>		
				<% } %>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">referenceCode</div>
					<div class="ParameterValue"><%=amountReservationTransaction.getReferenceCode()%></div>				
				</div>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">resourceURL</div>
					<div class="ParameterValue"><%=amountReservationTransaction.getResourceURL()%></div>				
				</div>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">referenceSequence</div>
					<div class="ParameterValue"><%=amountReservationTransaction.getReferenceSequence()%></div>				
				</div>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">serverReferenceCode</div>
					<div class="ParameterValue"><%=amountReservationTransaction.getServerReferenceCode()%></div>				
				</div>
	          	<div class="ParameterRow">
					<div class="ParameterLabel">transactionOperationStatus</div>
					<div class="ParameterValue"><%=amountReservationTransaction.getTransactionOperationStatus()%></div>				
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
