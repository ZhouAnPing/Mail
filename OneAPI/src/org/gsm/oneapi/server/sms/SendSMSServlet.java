package org.gsm.oneapi.server.sms;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.StringReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;
import org.codehaus.jackson.map.ObjectMapper;
import org.gsm.oneapi.responsebean.ResourceReference;
import org.gsm.oneapi.responsebean.sms.TripolisDeliveryInfoNotification;
import org.gsm.oneapi.server.OneAPIServlet;
import org.gsm.oneapi.server.ValidationRule;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.JDOMException;
import org.jdom.input.SAXBuilder;
import org.xml.sax.InputSource;

import com.youzan.utils.SMSConfigurator;

/**
 * Servlet implementing the OneAPI function for sending an SMS message
 */
public class SendSMSServlet extends OneAPIServlet implements Runnable {
	private static final long serialVersionUID = 6553586905656923326L;

	static Logger logger = Logger.getLogger(SendSMSServlet.class);

	// Used when the servlet is created
	public SendSMSServlet() {

	}

	private String callbackData = null;
	private String notifyURL = null;
	private String[] addresses = null;
	private String result = null;

	// Used when want to emulate sending a notification
	public SendSMSServlet(String callbackData, String notifyURL,
			String[] addresses, String result) {
		this.callbackData = callbackData;
		this.notifyURL = notifyURL;
		this.addresses = addresses;
		this.result = result;
	}

	public void init() throws ServletException {
		logger.debug("SendSMSServlet initialised");
	}

	private final String[] validationRules = { "1", "smsmessaging", "outbound",
			"*", "requests" };

	/**
	 * Count received SMS Count.
	 */
	private void countSMSCount() {

		Integer num = new Integer(0);
		ServletContext context = this.getServletContext();
		num = (Integer) context.getAttribute("count");
		if (num == null) {
			num = new Integer(1);

		} else {

			num = new Integer(num.intValue() + 1);
		}
		context.setAttribute("count", num);
		logger.debug("\nReceive SMS count = " + num);
	}

	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws IOException, ServletException {
		countSMSCount();
		request.setCharacterEncoding("utf-8");

		logger.debug("Parsing information begin*********V1.0");
		dumpRequestDetails(request, logger);

		logger.debug("Parsing information End*********");
		logger.debug("");

		String[] requestParts = getRequestParts(request);

		if (validateRequest(request, response, requestParts, validationRules)) {

			logger.debug("SendSMS_V1 - url appears correctly formatted");
			/*
			 * Decode the service parameters - in this case it is an HTTP POST
			 * request
			 */
			String[] addresses = request.getParameterValues("address"); // Note
																		// there
																		// can
																		// be
																		// multiple
																		// addresses
																		// specified
			String senderAddress = nullOrTrimmed(request
					.getParameter("senderAddress"));
			String message = nullOrTrimmed(request.getParameter("message"));
			String clientCorrelator = nullOrTrimmed(request
					.getParameter("clientCorrelator"));
			String notifyURL = nullOrTrimmed(request.getParameter("notifyURL"));
			String callbackData = nullOrTrimmed(request
					.getParameter("callbackData"));
			String senderName = nullOrTrimmed(request
					.getParameter("senderName"));

			if (message != null && message.trim().length() > 0) {
				message = URLEncoder.encode(message, "utf-8");
			}
			if (clientCorrelator != null
					&& clientCorrelator.trim().length() > 0) {
				clientCorrelator = URLEncoder.encode(clientCorrelator, "utf-8");
			}
			// message = new String(message.getBytes("iso-8859-1"), "utf-8");
			// senderName = new String(senderName.getBytes("iso-8859-1"),
			// "utf-8");
			// clientCorrelator = new
			// String(clientCorrelator.getBytes("iso-8859-1"), "utf-8");
			if (senderName != null && senderName.trim().length() > 0) {
				senderName = URLEncoder.encode(senderName, "utf-8");
				// message = message + senderName;
			}
			// this.getServletContext().setAttribute(arg0, arg1)

			logger.debug("");
			logger.debug("Encoding information begin*********V1.0");
			logger.debug("senderAddress = " + senderAddress);
			logger.debug("message = " + message);
			logger.debug("clientCorrelator = " + clientCorrelator);
			logger.debug("notifyURL = " + notifyURL);
			logger.debug("senderName = " + senderName);
			logger.debug("callbackData = " + callbackData);
			logger.debug("Encoding information End*********");
			logger.debug("");

			if (addresses != null)
				for (String add : addresses)
					logger.debug("address = " + add);

			String resourceURL = null;

			ValidationRule[] rules = {
					new ValidationRule(
							ValidationRule.VALIDATION_TYPE_MANDATORY_TEL,
							"senderAddress", senderAddress),
					new ValidationRule(
							ValidationRule.VALIDATION_TYPE_MANDATORY_TEL,
							"address", addresses),
					new ValidationRule(
							ValidationRule.VALIDATION_TYPE_MANDATORY,
							"message", message),
					new ValidationRule(ValidationRule.VALIDATION_TYPE_OPTIONAL,
							"clientCorrelator", clientCorrelator),
					new ValidationRule(ValidationRule.VALIDATION_TYPE_OPTIONAL,
							"senderName", senderName),
					new ValidationRule(
							ValidationRule.VALIDATION_TYPE_OPTIONAL_URL,
							"notifyURL", notifyURL),
					new ValidationRule(ValidationRule.VALIDATION_TYPE_OPTIONAL,
							"callbackData", callbackData), };

			String tempAddress = "";
			if (checkRequestParameters(response, rules)) {
				// Send SMS
				if (addresses != null) {
					for (int i = 0; i < addresses.length; i++) {
						if (addresses[i].indexOf("tel:+") > -1) {
							tempAddress = tempAddress
									+ ","
									+ addresses[i].substring(5,
											addresses[i].length());

						}
						if (tempAddress.length() > 0
								&& ((i > 0 && i % 500 == 0) || (i == addresses.length - 1))) {
							if (tempAddress.length() > 2) {
								tempAddress = tempAddress.substring(1,
										tempAddress.length());
								logger.debug("tempAddress = " + tempAddress);
							}

							resourceURL = getRequestHostnameAndContext(request)
									+ request.getServletPath()
									+ "/1/smsmessaging/outbound/"
									+ urlEncode(senderAddress) + "/requests/"
									+ urlEncode(clientCorrelator);
							// resourceURL =
							// getRequestHostnameAndContext(request) +
							// request.getServletPath() +
							// "/msg/HttpBatchSendSM";

							ResourceReference resourceReference = new ResourceReference();
							resourceReference.setResourceURL(resourceURL);

							ObjectMapper mapper = new ObjectMapper();

							String jsonResponse = "{\"resourceReference\":"
									+ mapper.writeValueAsString(resourceReference)
									+ "}";

							// logger.debug("Sending response. ResourceURL=" +
							// resourceURL);
							// String ret = this.sendSMSByPost(resourceURL,
							// tempAddress, message, true);

							// String ret = this.sendSMS("te_kpsy", "a123456",
							// "13671935968", "4", "�������ݲ��ԡ�Tripolis�Ϻ���");

							// ����ֻ���������Ӣ��,�Ÿ���
							// String PostData =
							// "account=vs_yzyy0331&password=a123456&mobile=13671935968&pid=4&time=null&content="+java.net.URLEncoder.encode("�������ݲ��ԡ�Tripolis�Ϻ���","utf-8");

							String PostData = "account=vs_yzyy0331&password=a123456&mobile="
									+ tempAddress
									+ "&pid=9&time=null&content="
									+ java.net.URLEncoder.encode(message + "��"
											+ senderName + "��", "utf-8");
							// out.println(PostData);
							String ret = SMS(PostData,
									"http://10658.cc/webservice/api?method=SendSms");
							logger.debug(ret);

							sendJSONResponse(response, jsonResponse, CREATED,
									resourceURL);

							if (notifyURL != null) {
								String status = "";
								String msg = "";
								String msgId = "";

								try {

									StringReader read = new StringReader(ret);
									// �����µ�����ԴSAX ��������ʹ�� InputSource ������ȷ����ζ�ȡ
									// XML ����
									InputSource source = new InputSource(read);
									// ����һ���µ�SAXBuilder
									SAXBuilder sb = new SAXBuilder();
									// ͨ������Դ����һ��Document

									try {
										Document doc = sb.build(source);
										// ȡ�ĸ�Ԫ��
										Element root = doc.getRootElement();

										status = ((Element) root.getChildren()
												.get(0)).getValue();
										msg = ((Element) root.getChildren()
												.get(1)).getValue();
										msgId = ((Element) root.getChildren()
												.get(2)).getValue();

									} catch (JDOMException e) {
										// TODO �Զ����� catch ��
										e.printStackTrace();
									} catch (IOException e) {
										// TODO �Զ����� catch ��
										e.printStackTrace();
									}

								} catch (Exception e) {
									// status = "-1";
								}

								callbackData = msgId + ":" + msg;
								SendSMSServlet t = new SendSMSServlet(
										callbackData, notifyURL, addresses,
										status);
								new Thread(t).start();
							}

							tempAddress = "";
						}

					}
				}
			}
		}

	}

	public static String SMS(String postData, String postUrl) {
		try {
			// ����POST����
			URL url = new URL(postUrl);
			HttpURLConnection conn = (HttpURLConnection) url.openConnection();
			conn.setRequestMethod("POST");
			conn.setRequestProperty("Content-Type",
					"application/x-www-form-urlencoded");
			conn.setRequestProperty("Connection", "Keep-Alive");
			conn.setUseCaches(false);
			conn.setDoOutput(true);

			conn.setRequestProperty("Content-Length", "" + postData.length());
			OutputStreamWriter out = new OutputStreamWriter(
					conn.getOutputStream(), "UTF-8");
			out.write(postData);
			out.flush();
			out.close();

			// ��ȡ��Ӧ״̬
			if (conn.getResponseCode() != HttpURLConnection.HTTP_OK) {
				System.out.println("connect failed!");
				return "";
			}
			// ��ȡ��Ӧ������
			String line, result = "";
			BufferedReader in = new BufferedReader(new InputStreamReader(
					conn.getInputStream(), "utf-8"));
			while ((line = in.readLine()) != null) {
				result += line + "\n";
			}
			in.close();
			return result;
		} catch (IOException e) {
			e.printStackTrace(System.out);
		}
		return "";
	}

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws IOException, ServletException {
		logger.debug("Do Get Processing...");
		dumpRequestDetails(request, logger);
	}

	public void run() {
		logger.debug("Notifier Thread :: Sleeping now...");
		try {
			Thread.sleep(10000L);
		} catch (Exception e) {
		}
		logger.debug("Notifier Thread ::  Awoken");
		try {
			logger.debug("Notifier Thread :: Creating connection to "
					+ notifyURL);
			HttpURLConnection con = (HttpURLConnection) new URL(notifyURL)
					.openConnection();
			con.setRequestMethod("POST");
			con.setRequestProperty("Content-Type", "application/json");
			con.setDoOutput(true);
			con.setDoInput(true);
			con.setUseCaches(false);

			TripolisDeliveryInfoNotification deliveryInfoNotification = new TripolisDeliveryInfoNotification();

			TripolisDeliveryInfoNotification.DeliveryInfo deliveryInfo = null;
			for (int i = 0; i < addresses.length; i++) {
				if (addresses[i] != null && addresses[i].trim().length() > 0) {
					if (result.equals("2")) {
						deliveryInfo = new TripolisDeliveryInfoNotification.DeliveryInfo(
								addresses[i], "DeliveredToNetwork");
						callbackData = result + ":" + "DeliveredToNetwork";
					} else {
						deliveryInfo = new TripolisDeliveryInfoNotification.DeliveryInfo(
								addresses[i], "DeliveryImpossible");
						callbackData = result + ":" + "DeliveryImpossible";
					}
					break;
				}
			}
			deliveryInfoNotification.setDeliveryInfo(deliveryInfo);
			deliveryInfoNotification.setCallbackData(callbackData);

			ObjectMapper mapper = new ObjectMapper();
			String jsonResponse = "{\"deliveryInfoNotification\":"
					+ mapper.writeValueAsString(deliveryInfoNotification) + "}";

			logger.debug("Notifier Thread :: Sending JSON data: "
					+ jsonResponse);
			OutputStream output = con.getOutputStream();
			byte[] ba = jsonResponse.getBytes();
			output.write(ba);
			output.flush();
			output.close();
			logger.debug("Notifier Thread :: Finished output");
			logger.debug("Notifier Thread :: Reading response");

			InputStream in = con.getInputStream();
			logger.debug("Notifier Thread :: Response code: "
					+ con.getResponseCode());
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

	// public static final String POST_URL =
	// "http://117.135.134.240/msg/HttpSendSM";

	/**
	 * Send SMS by Post
	 * 
	 * @param url
	 * @param reciever
	 * @param message
	 * @param needStatus
	 * @throws IOException
	 */
	public String sendSMSByPost(String url, String reciever, String message,
			boolean needStatus) throws IOException {

		// url = POST_URL;
		String url2 = SMSConfigurator.getInstance().getValue("url2").trim();
		if (url2.length() > 0) {
			// url = url2 + "/msg/HttpSendSM";
			url = url2 + "/msg/HttpBatchSendSM";
		}
		// Post�����URL����get��ͬ���ǲ���Ҫ������
		URL postUrl = new URL(url);

		// ���ô���
		String proxyHost = SMSConfigurator.getInstance().getValue("proxyHost");
		String proxyPort = SMSConfigurator.getInstance().getValue("proxyPort");
		String proxyUserName = SMSConfigurator.getInstance().getValue(
				"proxyUserName");
		String proxyUserPwd = SMSConfigurator.getInstance().getValue(
				"proxyUserPwd");
		if (proxyHost != null && proxyPort != null) {
			System.setProperty("http.proxyHost", proxyHost.trim()); // ����IP
			System.setProperty("http.proxyPort", proxyPort.trim()); // ����˿�
			if (proxyUserName != null) {
				// ����http����Ҫʹ�õĴ�����������û���
				System.setProperty("http.proxyUser", proxyUserName);
				// ����http����Ҫʹ�õĴ��������������
				System.setProperty("http.proxyPassword", proxyUserPwd);
			}
		}
		// ������
		HttpURLConnection connection = (HttpURLConnection) postUrl
				.openConnection();

		// Output to the connection. Default is false, set to true because post
		// method must write something to the connection
		// �����Ƿ���connection�������Ϊ�����post���󣬲���Ҫ���� http�����ڣ������Ҫ��Ϊtrue
		connection.setDoOutput(true);
		// Read from the connection. Default is true.
		connection.setDoInput(true);
		// Set the post method. Default is GET
		connection.setRequestMethod("POST");
		// Post cannot use caches
		// Post ������ʹ�û���
		connection.setUseCaches(false);
		// This method takes effects to every instances of this class.
		// URLConnection.setFollowRedirects��static���������������е�URLConnection����
		// connection.setFollowRedirects(true);

		// This methods only takes effacts to this instance.
		// URLConnection.setInstanceFollowRedirects�ǳ�Ա�������������ڵ�ǰ����
		connection.setInstanceFollowRedirects(true);
		// Set the content type to urlencoded, because we will write some
		// URL-encoded content to the
		// connection. Settings above must be set before connect!
		// ���ñ������ӵ�Content-type������Ϊapplication/x-www-form-urlencoded��
		// ��˼��������urlencoded�������form�������������ǿ��Կ������Ƕ���������ʹ��URLEncoder.encode ���б���
		connection.setRequestProperty("Content-Type",
				"application/x-www-form-urlencoded;charset=UTF-8");
		// ���ӣ���postUrl.openConnection()���˵����ñ���Ҫ��connect֮ǰ��ɣ�
		// Ҫע�����connection.getOutputStream�������Ľ���connect��
		connection.connect();

		DataOutputStream out = new DataOutputStream(
				connection.getOutputStream());
		// The URL-encoded contend ���ģ�����������ʵ��get��URL��'?'��Ĳ����ַ���һ��
		String account = SMSConfigurator.getInstance().getValue("account")
				.trim();
		String password = SMSConfigurator.getInstance().getValue("password")
				.trim();

		String content = "account=" + account + "&pswd=" + password
				+ "&mobile=" + reciever + "&msg=" + message + "&needstatus="
				+ needStatus;
		// DataOutputStream.writeBytes���ַ����е�16λ��unicode�ַ���8λ���ַ���ʽд��������
		out.writeBytes(content);
		out.flush();
		out.close(); // flush and close
		BufferedReader reader = new BufferedReader(new InputStreamReader(
				connection.getInputStream(), "utf-8"));// ���ñ���,������������
		String line = "";

		logger.debug("Contents of post request");
		logger.debug("=============================");
		logger.debug(message);
		StringBuffer sb = new StringBuffer();
		// FormParameters formParameters = new FormParameters();
		while ((line = reader.readLine()) != null) {
			// line = new String(line.getBytes(), "utf-8");
			logger.debug(line);
			sb.append(line).append(",");
			// formParameters.put("senderStatus", line);
		}
		logger.debug("=============================");
		logger.debug("Contents of post request=" + sb.toString());

		reader.close();
		connection.disconnect();
		return sb.toString();
	}

	public static void main(String[] args) throws IOException {
		// Post�����URL����get��ͬ���ǲ���Ҫ������
		URL postUrl = new URL(
				"http://localhost:8080/OneAPI/SendSMSService/1/smsmessaging/outbound/{senderAddress}/requests");
		// ������
		// System.setProperty("http.proxyHost", "web-proxy.china.hp.com");
		// System.setProperty("http.proxyPort", "8088");
		HttpURLConnection connection = (HttpURLConnection) postUrl
				.openConnection();

		// Output to the connection. Default is
		// false, set to true because post
		// method must write something to the
		// connection
		// �����Ƿ���connection�������Ϊ�����post���󣬲���Ҫ����
		// http�����ڣ������Ҫ��Ϊtrue
		connection.setDoOutput(true);
		// Read from the connection. Default is true.
		connection.setDoInput(true);
		// Set the post method. Default is GET
		connection.setRequestMethod("POST");
		// Post cannot use caches
		// Post ������ʹ�û���
		connection.setUseCaches(false);
		// This method takes effects to
		// every instances of this class.
		// URLConnection.setFollowRedirects��static���������������е�URLConnection����
		// connection.setFollowRedirects(true);

		// This methods only
		// takes effacts to this
		// instance.
		// URLConnection.setInstanceFollowRedirects�ǳ�Ա�������������ڵ�ǰ����
		connection.setInstanceFollowRedirects(true);
		// Set the content type to urlencoded,
		// because we will write
		// some URL-encoded content to the
		// connection. Settings above must be set before connect!
		// ���ñ������ӵ�Content-type������Ϊapplication/x-www-form-urlencoded��
		// ��˼��������urlencoded�������form�������������ǿ��Կ������Ƕ���������ʹ��URLEncoder.encode
		// ���б���
		connection.setRequestProperty("Content-Type",
				"application/x-www-form-urlencoded");
		// ���ӣ���postUrl.openConnection()���˵����ñ���Ҫ��connect֮ǰ��ɣ�
		// Ҫע�����connection.getOutputStream�������Ľ���connect��
		connection.connect();

		DataOutputStream out = new DataOutputStream(
				connection.getOutputStream());
		// The URL-encoded contend
		// ���ģ�����������ʵ��get��URL��'?'��Ĳ����ַ���һ��
		String content = "senderAddress=tel%3A%2B123456789&address=tel%3A%2B13671935968&address=&address=&address=&address=&message=%E9%A2%84%E7%A5%9D%E4%B8%AD%E7%A7%8B%E8%8A%82%E5%BF%AB%E4%B9%90%E5%B9%B3%E5%AE%89%EF%BC%81%E3%80%90%E4%BC%98%E8%B5%9E%E3%80%91&clientCorrelator=123456&notifyURL=http%3A%2F%2Flocalhost%3A8080%2FOneAPI%2Fsmsdeliverynotification.jsp&senderName=%E3%80%90%E4%BC%98%E8%B5%9E%E3%80%91&callbackData=some-data-useful-to-the-requester";
		// DataOutputStream.writeBytes���ַ����е�16λ��unicode�ַ���8λ���ַ���ʽд��������
		out.writeBytes(content);
		out.flush();
		out.close(); // flush and close
		BufferedReader reader = new BufferedReader(new InputStreamReader(
				connection.getInputStream(), "utf-8"));// ���ñ���,������������
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
