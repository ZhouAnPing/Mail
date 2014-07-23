package com.youzan.utils;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.InetSocketAddress;
import java.net.Proxy;
import java.net.SocketAddress;
import java.net.URL;

import org.apache.commons.codec.binary.Base64;
import org.apache.log4j.Logger;
import org.codehaus.jackson.map.ObjectMapper;
import org.gsm.oneapi.responsebean.sms.TripolisDeliveryInfoNotification;

public class TestFeedback {
	static Logger logger = Logger.getLogger(TestFeedback.class);

	public static void main(String[] args) throws IOException {
		
		String username = "sms";
		String password = "__SMSaccess";
		String credentials = username + ":" + password;
		Base64 base64encoder = new Base64();
		String authHeaderValue = base64encoder.encodeToString(credentials.getBytes()).trim();

		//https://dialogue.tripolis.cn/OneAPI/SMSDeliveryService
		//http://localhost:8080/OneAPI/SMSDeliveryService
		// Post请求的URL，与get不同的是不需要带参数
		URL postUrl = new URL("https://dialogue.tripolis.cn/OneAPI/SMSDeliveryService");
		//URL postUrl = new URL("http://localhost:8080/OneAPI/SMSDeliveryService");
		
		// postUrl = new URL("http://dialogue.tripolis.cn/public/smsreport/oneapi");
		// 打开连接
		SocketAddress addr = new InetSocketAddress("web-proxy.china.hp.com", 8088);
		Proxy proxy = new Proxy(Proxy.Type.HTTP, addr);
		HttpURLConnection connection = (HttpURLConnection) postUrl.openConnection(proxy);
		if (authHeaderValue != null) {
			connection.setRequestProperty("Authorization", "Basic " + authHeaderValue);
			logger.debug("Authorization type Basic using " + authHeaderValue);
		}
		// Output to the connection. Default is
		// false, set to true because post
		// method must write something to the
		// connection
		// 设置是否向connection输出，因为这个是post请求，参数要放在
		// http正文内，因此需要设为true
		connection.setDoOutput(true);
		// Read from the connection. Default is true.
		connection.setDoInput(true);
		// Set the post method. Default is GET
		connection.setRequestMethod("POST");
		// Post cannot use caches
		// Post 请求不能使用缓存
		connection.setUseCaches(false);
		// This method takes effects to
		// every instances of this class.
		// URLConnection.setFollowRedirects是static函数，作用于所有的URLConnection对象。
		// connection.setFollowRedirects(true);

		// This methods only
		// takes effacts to this
		// instance.
		// URLConnection.setInstanceFollowRedirects是成员函数，仅作用于当前函数
		connection.setInstanceFollowRedirects(true);
		// Set the content type to urlencoded,
		// because we will write
		// some URL-encoded content to the
		// connection. Settings above must be set before connect!
		// 配置本次连接的Content-type，配置为application/x-www-form-urlencoded的
		// 意思是正文是urlencoded编码过的form参数，下面我们可以看到我们对正文内容使用URLEncoder.encode
		// 进行编码
		connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
		// 连接，从postUrl.openConnection()至此的配置必须要在connect之前完成，
		// 要注意的是connection.getOutputStream会隐含的进行connect。
		connection.connect();

		DataOutputStream out = new DataOutputStream(connection.getOutputStream());
		// The URL-encoded contend
		// 正文，正文内容其实跟get的URL中'?'后的参数字符串一致
		//&callbackData=iJsTf8zO5OrgsN2CgcSOWXCWutx3PTdxYqE644Ub__g
		String content = "receiver=admin&pswd=12345&msgid=youzan_H_201303_30&reportTime=1012241002&mobile=13671935968&status=DELIVRD";
		// DataOutputStream.writeBytes将字符串中的16位的unicode字符以8位的字符形式写道流里面
		out.writeBytes(content);
		out.flush();
		out.close(); // flush and close	
		BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream(), "utf-8"));// 设置编码,否则中文乱码
		String line = "";
		System.out.println("=============================");
		System.out.println("Contents of post request");
		System.out.println("=============================");
		while ((line = reader.readLine()) != null) {
			// line = new String(line.getBytes(), "utf-8");
			System.out.println(line);
		}
		System.out.println("=============================");
		System.out.println("Contents of post request ends");
		System.out.println("=============================");
		reader.close();
		
		connection.disconnect();
		System.out.println("Finish Sending");
	}

	/**
	 * @param args
	 */
	public static void main1(String[] args) {

		try {
			String notifyURL = "http://dialogue.tripolis.cn/public/smsreport/oneapi";
			// notifyURL = "http://localhost:8080/OneAPI/SMSDeliveryService";
			logger.debug("Notifier Thread :: Creating connection to " + notifyURL);

			SocketAddress addr = new InetSocketAddress("web-proxy.china.hp.com", 8088);
			Proxy proxy = new Proxy(Proxy.Type.HTTP, addr);

			HttpURLConnection con = (HttpURLConnection) new URL(notifyURL).openConnection();

			con.setConnectTimeout(20 * 1000);
			con.setRequestMethod("POST");
			con.setRequestProperty("Content-Type", "application/json");
			con.setDoOutput(true);
			con.setDoInput(true);
			con.setUseCaches(false);

			// DeliveryInfoNotification deliveryInfoNotification = new DeliveryInfoNotification();
			TripolisDeliveryInfoNotification deliveryInfoNotification = new TripolisDeliveryInfoNotification();
			// DeliveryInfoNotification.DeliveryInfo deliveryInfo1 = null;
			TripolisDeliveryInfoNotification.DeliveryInfo deliveryInfo1 = null;

			deliveryInfo1 = new TripolisDeliveryInfoNotification.DeliveryInfo("tel:+13671935968", "DeliveredToTerminal");
			// callbackData = "";

			deliveryInfoNotification.setDeliveryInfo(deliveryInfo1);
			deliveryInfoNotification.setCallbackData("");

			ObjectMapper mapper = new ObjectMapper();
			String jsonResponse = "{\"deliveryInfoNotification\":" + mapper.writeValueAsString(deliveryInfoNotification) + "}";

			logger.debug("Notifier Thread :: Sending JSON data: " + jsonResponse);
			OutputStream output = con.getOutputStream();
			byte[] ba = jsonResponse.getBytes();
			output.write(ba);
			output.flush();
			output.close();
			logger.debug("Notifier Thread :: Finished output");
			logger.debug("Notifier Thread :: Reading response");

			InputStream in = con.getInputStream();
			logger.debug("Notifier Thread :: Response code: " + con.getResponseCode());
			if (in != null) {
				int c;
				StringBuffer rbuf = new StringBuffer();
				while ((c = in.read()) != -1) {
					rbuf.append((char) c);
				}
				logger.debug("Notifier Thread :: Read: " + rbuf.toString());
			}

		} catch (Exception e) {
			e.printStackTrace();
		}// TODO Auto-generated method stub

	}

}
