//import java.io.FileInputStream;
//import java.io.FileNotFoundException;

package com.youzan.utils;
import java.io.IOException;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpException;
import org.apache.commons.httpclient.NameValuePair;
import org.apache.commons.httpclient.methods.PostMethod;

public class SendSMS {
	
	private static String Url = "http://10658.cc/webservice/api?method=SendSms";
	
	public String sendSMS(String account, String password, String mobile,String Pid, String content){
		String message="";
		HttpClient client = new HttpClient(); 
		PostMethod method = new PostMethod(Url); 
			
		//client.getParams().setContentCharset("GBK");		
		client.getParams().setContentCharset("UTF-8");
		method.setRequestHeader("ContentType","application/x-www-form-urlencoded;charset=UTF-8");

	    //String content = new String("短信内容【签名】"); 
	    
		//多个手机号码请用英文,号隔开		
		NameValuePair[] data = {//提交短信
			    new NameValuePair("account", account), 
			    new NameValuePair("password", password), 			    
			    new NameValuePair("mobile", mobile),
			    new NameValuePair("pid", Pid),					
			    new NameValuePair("time", null),
				new NameValuePair("content", content),
		};
		
		method.setRequestBody(data);		
		
		
		try {
			client.executeMethod(method);	
			message= method.getResponseBodyAsString();
			System.out.println(message);
			
		} catch (HttpException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}	
		
		//System.out.println("短信提交成功");
		return message;
	}
	public static void main(String [] args) {
		SendSMS sendSMS = new SendSMS();
		sendSMS.sendSMS("te_kpsy", "a123456", "13671935968", "4", "短信内容测试【Tripolis上海】");
		
	}
	
}