package com.youzan.utils;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Properties;

public class SMSConfigurator {
	private Properties config = new Properties();
	private String fn = null;
	private static final String configLocation = "sms.properties";

	private static SMSConfigurator instance;

	public synchronized static SMSConfigurator getInstance() {
		if (instance == null) {
			try {
				instance = new SMSConfigurator(configLocation);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

		return instance;
	}

	public SMSConfigurator(String fileName) throws Exception {
		try {
			String path = SMSConfigurator.class.getClassLoader().getResource(fileName).getPath();// 通过URI形式
			FileInputStream fin = new FileInputStream(path);
			config.load(fin);
			fin.close();
		} catch (IOException ex) {
			throw new Exception("Failed loading configuration file " + fileName);
		}
		fn = fileName;
	}

	public String getValue(String itemName) {
		return config.getProperty(itemName);
	}

	public String getValue(String itemName, String defaultValue) {
		return config.getProperty(itemName, defaultValue);
	}

	public void setValue(String itemName, String value) {
		config.setProperty(itemName, value);
		return;
	}

	public void saveFile(String fileName, String description) throws Exception {
		try {
			FileOutputStream fout = new FileOutputStream(fileName);
			config.store(fout, description);
			fout.close();
		} catch (IOException ex) {
			throw new Exception("Cannot save present configuration file " + fileName);
		}
	}

	public void saveFile(String fileName) throws Exception {
		saveFile(fileName, "");
	}

	public void saveFile() throws Exception {
		if (fn.length() == 0)
			throw new Exception("Please provide the property file name");
		saveFile(fn);
	}
}
