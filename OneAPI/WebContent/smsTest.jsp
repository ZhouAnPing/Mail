<%@ page language="java" contentType="text/html; charset=utf-8"
	pageEncoding="utf-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Insert title here</title>
</head>
<body>
	<form action="SendSMSService/SendSMSServlet" method="get">

		senderAddress:<input type="text" name="senderAddress" value="tel:+123456789" /><br>
		address :<input type="text" name="address" value="tel:+13671935968" /> <br>
		message:<textarea name="message" style="width: 400px; height: 80px;">
		This is a test for the UTF8 encoding. 电子邮件
		Please let me know if this reaches you. Kind regards 【Sjoerd】
		</textarea>  
	  <input type="submit" name="submit" value="Submit"/>
	</form>
</body>
</html>