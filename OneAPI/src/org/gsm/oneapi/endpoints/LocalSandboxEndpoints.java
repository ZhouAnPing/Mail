package org.gsm.oneapi.endpoints;

import org.apache.log4j.Logger;

public class LocalSandboxEndpoints extends ServiceEndpoints implements ServiceEndpointsInterface {

	static Logger logger=Logger.getLogger(LocalSandboxEndpoints.class);
	
	protected static String AmountCharge="http://localhost:8080/OneAPI/AmountChargeService/1/payment/{endUserId}/transactions/amount";
	protected static String AmountRefund="http://localhost:8080/OneAPI/AmountRefundService/1/payment/{endUserId}/transactions/amount";
	protected static String AmountReserve="http://localhost:8080/OneAPI/AmountReserveService/1/payment/{endUserId}/transactions/amountReservation";
	protected static String AmountReserveAdditional="http://localhost:8080/OneAPI/AmountReserveAdditionalService/1/payment/{endUserId}/transactions/amountReservation/{transactionId}";
	protected static String AmountReservationCharge="http://localhost:8080/OneAPI/AmountReserveChargeService/1/payment/{endUserId}/transactions/amountReservation/{transactionId}";
	protected static String AmountReservationRelease="http://localhost:8080/OneAPI/AmountReserveReleaseService/1/payment/{endUserId}/transactions/amountReservation/{transactionId}";

	protected static String Location="http://localhost:8080/OneAPI/LocationService/1/location/queries/location";

	protected static String SendSMS="http://localhost:8080/OneAPI/SendSMSService/1/smsmessaging/outbound/{senderAddress}/requests";
	protected static String QuerySMSDelivery="http://localhost:8080/OneAPI/QuerySMSService/1/smsmessaging/outbound/{senderAddress}/requests/{requestId}/deliveryInfos";
	protected static String SMSDeliverySubscriptions="http://localhost:8080/OneAPI/SMSDeliveryService/1/smsmessaging/outbound/{senderAddress}/subscriptions";
	protected static String CancelSMSDeliverySubscription="http://localhost:8080/OneAPI/CancelSMSDeliveryService/1/smsmessaging/outbound/subscriptions/{subscriptionId}";
	protected static String RetrieveSMS="http://localhost:8080/OneAPI/RetrieveSMSService/1/smsmessaging/inbound/registrations/{registrationId}/messages/?maxBatchSize={maxBatchSize}";
	protected static String SMSReceiptSubscriptions="http://localhost:8080/OneAPI/SMSReceiptService/1/smsmessaging/inbound/subscriptions";
	protected static String CancelSMSReceiptSubscription="http://localhost:8080/OneAPI/CancelSMSReceiptService/1/smsmessaging/inbound/subscriptions/{subscriptionId}";

	protected static String SendMMS="http://localhost:8080/OneAPI/SendMMSService/1/messaging/outbound/{senderAddress}/requests";
	protected static String QueryMMSDelivery="http://localhost:8080/OneAPI/QueryMMSService/1/messaging/outbound/{senderAddress}/requests/{requestId}/deliveryInfos";
	protected static String MMSDeliverySubscriptions="http://localhost:8080/OneAPI/MMSDeliveryService/1/messaging/outbound/{senderAddress}/subscriptions";
	protected static String CancelMMSDeliverySubscription="http://localhost:8080/OneAPI/CancelMMSDeliveryService/1/messaging/outbound/subscriptions/{subscriptionId}";
	protected static String RetrieveMMS="http://localhost:8080/OneAPI/RetrieveMMSService/1/messaging/inbound/registrations/{registrationId}/messages/?maxBatchSize={maxBatchSize}";
	protected static String RetrieveMMSMessage="http://localhost:8080/OneAPI/RetrieveMMSMessageService/1/messaging/inbound/registrations/{registrationId}/messages/{messageId}?resFormat={resFormat}";
	protected static String MMSReceiptSubscriptions="http://localhost:8080/OneAPI/MMSReceiptService/1/messaging/inbound/subscriptions";
	protected static String CancelMMSReceiptSubscription="http://localhost:8080/OneAPI/CancelMMSReceiptService/1/messaging/inbound/subscriptions/{subscriptionId}";
		
	private static LocalSandboxEndpoints INSTANCE=new LocalSandboxEndpoints();
	
	public LocalSandboxEndpoints () {
	}
	
	public ServiceEndpointsInterface getInstance() { return INSTANCE; }
	
	public void setAmountCharge(String AmountCharge) { LocalSandboxEndpoints.AmountCharge=AmountCharge; }
	public void setAmountRefund(String AmountRefund) { LocalSandboxEndpoints.AmountRefund=AmountRefund; }
	public void setAmountReserve(String AmountReserve) { LocalSandboxEndpoints.AmountReserve=AmountReserve; }
	public void setAmountReserveAdditional(String AmountReserveAdditional) { LocalSandboxEndpoints.AmountReserveAdditional=AmountReserveAdditional; }
	public void setAmountReservationCharge(String AmountReservationCharge) { LocalSandboxEndpoints.AmountReservationCharge=AmountReservationCharge; }
	public void setAmountReservationRelease(String AmountReservationRelease) { LocalSandboxEndpoints.AmountReservationRelease=AmountReservationRelease; }

	public void setLocation(String Location) { LocalSandboxEndpoints.Location=Location; }

	public void setSendSMS(String SendSMS) { LocalSandboxEndpoints.SendSMS=SendSMS; }
	public void setQuerySMSDelivery(String QuerySMSDelivery) { LocalSandboxEndpoints.QuerySMSDelivery=QuerySMSDelivery; }
	public void setSMSDeliverySubscriptions(String SMSDeliverySubscriptions) { LocalSandboxEndpoints.SMSDeliverySubscriptions=SMSDeliverySubscriptions; }
	public void setCancelSMSDeliverySubscription(String CancelSMSDeliverySubscription) { LocalSandboxEndpoints.CancelSMSDeliverySubscription=CancelSMSDeliverySubscription; }
	public void setRetrieveSMS(String RetrieveSMS) { LocalSandboxEndpoints.RetrieveSMS=RetrieveSMS; }
	public void setSMSReceiptSubscriptions(String SMSReceiptSubscriptions) { LocalSandboxEndpoints.SMSReceiptSubscriptions=SMSReceiptSubscriptions; }
	public void setCancelSMSReceiptSubscription(String CancelSMSReceiptSubscription) { LocalSandboxEndpoints.CancelSMSReceiptSubscription=CancelSMSReceiptSubscription; }

	public void setSendMMS(String SendMMS) { LocalSandboxEndpoints.SendMMS=SendMMS; }
	public void setQueryMMSDelivery(String QueryMMSDelivery) { LocalSandboxEndpoints.QueryMMSDelivery=QueryMMSDelivery; }
	public void setMMSDeliverySubscriptions(String MMSDeliverySubscriptions) { LocalSandboxEndpoints.MMSDeliverySubscriptions=MMSDeliverySubscriptions; }
	public void setCancelMMSDeliverySubscription(String CancelMMSDeliverySubscription) { LocalSandboxEndpoints.CancelMMSDeliverySubscription=CancelMMSDeliverySubscription; }
	public void setRetrieveMMS(String RetrieveMMS) { LocalSandboxEndpoints.RetrieveMMS=RetrieveMMS; }
	public void setRetrieveMMSMessage(String RetrieveMMSMessage) { LocalSandboxEndpoints.RetrieveMMSMessage=RetrieveMMSMessage; }
	public void setMMSReceiptSubscriptions(String MMSReceiptSubscriptions) { LocalSandboxEndpoints.MMSReceiptSubscriptions=MMSReceiptSubscriptions; }
	public void setCancelMMSReceiptSubscription(String CancelMMSReceiptSubscription) { LocalSandboxEndpoints.CancelMMSReceiptSubscription=CancelMMSReceiptSubscription; }

}
