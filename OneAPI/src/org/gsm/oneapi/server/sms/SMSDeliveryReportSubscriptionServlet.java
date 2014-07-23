package org.gsm.oneapi.server.sms;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;
import org.codehaus.jackson.map.ObjectMapper;
import org.gsm.oneapi.responsebean.sms.DeliveryReceiptSubscription;
import org.gsm.oneapi.responsebean.sms.TripolisDeliveryInfoNotification;
import org.gsm.oneapi.server.OneAPIServlet;
import org.gsm.oneapi.server.ValidationRule;

import com.youzan.utils.SMSConfigurator;

/**
 * Servlet implementing the OneAPI function for creating an SMS delivery report subscription
 */
public class SMSDeliveryReportSubscriptionServlet extends OneAPIServlet implements Runnable {

	private static final long serialVersionUID = -7359556423074788912L;

	static Logger logger = Logger.getLogger(SMSDeliveryReportSubscriptionServlet.class);

	// Used when the servlet is created
	public SMSDeliveryReportSubscriptionServlet() {

	}

	private String callbackData = null;
	private String notifyURL = null;
	private String address = null;
	private String status = null;

	// Used when want to emulate sending a notification
	public SMSDeliveryReportSubscriptionServlet(String callbackData, String notifyURL, String address, String status) {
		this.callbackData = callbackData;
		this.notifyURL = notifyURL;
		this.address = address;
		this.status = status;
	}

	public void init() throws ServletException {
		logger.debug("SMSDeliveryReportSubscriptionServlet initialised");
	}

	private final String[] validationRules = { "1", "smsmessaging", "outbound", "*", "subscriptions" };

	public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException, ServletException {
		processSMS(request, response);
	}

	public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException, ServletException {
		processSMS(request, response);
	}

	private void processSMS(HttpServletRequest request, HttpServletResponse response) throws IOException, ServletException {
		dumpRequestDetails(request, logger);

		logger.debug("SMS Delivery Report Subscriptions - url appears correctly formatted");

		/*
		 * Decode the service parameters - in this case it is an HTTP POST request
		 */

		String notifyURL = nullOrTrimmed(SMSConfigurator.getInstance().getValue("notifyUrl").trim());
		String callbackData = nullOrTrimmed(request.getParameter("callbackData"));
		String mobile = nullOrTrimmed(request.getParameter("mobile"));
		String sendStatus = nullOrTrimmed(request.getParameter("status"));
		String msgId = nullOrTrimmed(request.getParameter("msgid"));
		logger.debug("notifyURL = " + notifyURL);
		logger.debug("callbackData = " + callbackData);
		logger.debug("mobile = " + mobile);
		logger.debug("status = " + status);
		logger.debug("messageId = " + msgId);
		// synchronized (this) {
		try {
			callbackData = this.getServletContext().getAttribute(msgId).toString();
			this.getServletContext().removeAttribute(msgId);
		} catch (Exception e) {

		}
		// }

		String resourceURL = getRequestHostnameAndContext(request) + request.getServletPath() + "/1/smsmessaging/outbound/subscriptions/sub789";

		DeliveryReceiptSubscription receiptSubscription = new DeliveryReceiptSubscription();
		receiptSubscription.setResourceURL(resourceURL);

		DeliveryReceiptSubscription.CallbackReference callbackReference = new DeliveryReceiptSubscription.CallbackReference();
		callbackReference.setNotifyURL(notifyURL);
		callbackReference.setCallbackData(callbackData);

		receiptSubscription.setCallbackReference(callbackReference);

		ObjectMapper mapper = new ObjectMapper();

		String jsonResponse = "{\"deliveryReceiptSubscription\":" + mapper.writeValueAsString(receiptSubscription) + "}";

		logger.debug("Sending response. ResourceURL=" + resourceURL);

		sendJSONResponse(response, jsonResponse, CREATED, resourceURL);

		if (notifyURL != null) {
			SMSDeliveryReportSubscriptionServlet t = new SMSDeliveryReportSubscriptionServlet(callbackData, notifyURL, mobile, sendStatus);
			new Thread(t).start();
		}
	}

	public void doPost_old(HttpServletRequest request, HttpServletResponse response) throws IOException, ServletException {
		dumpRequestDetails(request, logger);

		String[] requestParts = getRequestParts(request);

		if (validateRequest(request, response, requestParts, validationRules)) {

			logger.debug("SMS Delivery Report Subscriptions - url appears correctly formatted");

			String senderAddress = requestParts[3];

			/*
			 * Decode the service parameters - in this case it is an HTTP POST request
			 */
			String clientCorrelator = nullOrTrimmed(request.getParameter("clientCorrelator"));
			String notifyURL = nullOrTrimmed(request.getParameter("notifyURL"));
			String callbackData = nullOrTrimmed(request.getParameter("callbackData"));

			logger.debug("senderAddress = " + senderAddress);
			logger.debug("clientCorrelator = " + clientCorrelator);
			logger.debug("notifyURL = " + notifyURL);
			logger.debug("callbackData = " + callbackData);

			ValidationRule[] rules = { new ValidationRule(ValidationRule.VALIDATION_TYPE_MANDATORY_TEL, "senderAddress", senderAddress),
					new ValidationRule(ValidationRule.VALIDATION_TYPE_MANDATORY_URL, "notifyURL", notifyURL),
					new ValidationRule(ValidationRule.VALIDATION_TYPE_OPTIONAL, "clientCorrelator", clientCorrelator),
					new ValidationRule(ValidationRule.VALIDATION_TYPE_OPTIONAL, "callbackData", callbackData), };

			if (checkRequestParameters(response, rules)) {
				String resourceURL = getRequestHostnameAndContext(request) + request.getServletPath() + "/1/smsmessaging/outbound/subscriptions/sub789";

				DeliveryReceiptSubscription receiptSubscription = new DeliveryReceiptSubscription();
				receiptSubscription.setResourceURL(resourceURL);

				DeliveryReceiptSubscription.CallbackReference callbackReference = new DeliveryReceiptSubscription.CallbackReference();
				callbackReference.setNotifyURL(notifyURL);
				callbackReference.setCallbackData(callbackData);

				receiptSubscription.setCallbackReference(callbackReference);

				ObjectMapper mapper = new ObjectMapper();

				String jsonResponse = "{\"deliveryReceiptSubscription\":" + mapper.writeValueAsString(receiptSubscription) + "}";

				logger.debug("Sending response. ResourceURL=" + resourceURL);

				sendJSONResponse(response, jsonResponse, CREATED, resourceURL);

				if (notifyURL != null) {
					SMSDeliveryReportSubscriptionServlet t = new SMSDeliveryReportSubscriptionServlet(callbackData, notifyURL, "", "");
					new Thread(t).start();
				}

			}
		}

	}

	public void run() {
		logger.debug("Notifier Thread :: Sleeping now...");
		try {
			Thread.sleep(10000L);
		} catch (Exception e) {
		}
		logger.debug("Notifier Thread ::  Awoken");
		try {
			logger.debug("Notifier Thread :: Creating connection to " + notifyURL);
			HttpURLConnection con = (HttpURLConnection) new URL(notifyURL).openConnection();
			con.setRequestMethod("POST");
			con.setRequestProperty("Content-Type", "application/json");
			con.setDoOutput(true);
			con.setDoInput(true);
			con.setUseCaches(false);

			// DeliveryInfoNotification deliveryInfoNotification = new DeliveryInfoNotification();
			TripolisDeliveryInfoNotification deliveryInfoNotification = new TripolisDeliveryInfoNotification();
			// DeliveryInfoNotification.DeliveryInfo deliveryInfo1 = null;
			TripolisDeliveryInfoNotification.DeliveryInfo deliveryInfo1 = null;

			if (status != null && status.equals("DELIVRD")) {
				deliveryInfo1 = new TripolisDeliveryInfoNotification.DeliveryInfo("tel:+" + this.address, "DeliveredToTerminal");
				callbackData = status;
			} else {
				deliveryInfo1 = new TripolisDeliveryInfoNotification.DeliveryInfo("tel:+" + this.address, "DeliveryImpossible");
				callbackData = status;
			}
			deliveryInfoNotification.setDeliveryInfo(deliveryInfo1);
			deliveryInfoNotification.setCallbackData(callbackData);

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
		}
	}

	public static void main(String[] args) throws IOException {
		// Post请求的URL，与get不同的是不需要带参数https://dialogue.tripolis.cn/OneAPI/SMSDeliveryService
		URL postUrl = new URL("http://localhost:8080/OneAPI/SMSDeliveryService");
		// postUrl = new URL("http://dialogue.tripolis.cn/public/smsreport/oneapi");
		// 打开连接
		// System.setProperty("http.proxyHost", "web-proxy.china.hp.com");
		// System.setProperty("http.proxyPort", "8088");
		HttpURLConnection connection = (HttpURLConnection) postUrl.openConnection();

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
		String content = "receiver=admin&pswd=12345&msgid=12345&reportTime=1012241002&mobile=13900210021&status=DELIVRD";
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
	}

}
