package com.youzan.utils;

import java.io.IOException;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

import org.apache.commons.codec.binary.Base64;
import org.apache.log4j.Logger;

public class TestSendSMS {
	static Logger logger = Logger.getLogger(TestSendSMS.class);

	public static void main(String[] args) {

		//String url = "http://localhost:8080/OneAPI/SendSMSService/1/smsmessaging/outbound/{senderAddress}/requests";
		String url = "https://dialogue.tripolis.cn/OneAPI/SendSMSService/1/smsmessaging/outbound/{senderAddress}/requests";
		
		logger.debug("Intitiating connection to URL: " + url);
		String username = "sms";
		String password = "__SMSaccess";
		String credentials = username + ":" + password;
		Base64 base64encoder = new Base64();
		String authHeaderValue = base64encoder.encodeToString(credentials.getBytes()).trim();

		URL postUrl;
		try {
			postUrl = new URL(url);

			HttpURLConnection con = (HttpURLConnection) postUrl.openConnection();
			if (authHeaderValue != null) {
				con.setRequestProperty("Authorization", "Basic " + authHeaderValue);
				logger.debug("Authorization type Basic using " + authHeaderValue);
			}
			con.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");

			con.setDoOutput(true);

			con.connect();

			OutputStreamWriter out = new OutputStreamWriter(con.getOutputStream());

			String key = "";
			String value = "";
			StringBuffer sb = new StringBuffer();
			key = "senderAddress";
			value = "tel:+123456789";
			sb.append("&").append(URLEncoder.encode(key, "utf-8")).append("=").append(URLEncoder.encode(value, "utf-8"));
			key = "address";
			value = "tel:+13671935968";
			sb.append("&").append(URLEncoder.encode(key, "utf-8")).append("=").append(URLEncoder.encode(value, "utf-8"));
			key = "message";
			value = "This is a test for the UTF8 encoding. 电子邮件中文测试 ";
			sb.append("&").append(URLEncoder.encode(key, "utf-8")).append("=").append(URLEncoder.encode(value, "utf-8"));
			key = "senderName";
			value = "【优赞】";
			sb.append("&").append(URLEncoder.encode(key, "utf-8")).append("=").append(URLEncoder.encode(value, "utf-8"));

			logger.debug("Request form parameters: " + sb.toString());

			String requestBody = sb.toString();

			out.write(requestBody);
			out.close();

			logger.debug(con.getResponseCode());
			logger.debug(con.getContentType());

			con.disconnect();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
