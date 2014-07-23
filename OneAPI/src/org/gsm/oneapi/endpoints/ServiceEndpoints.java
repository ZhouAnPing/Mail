package org.gsm.oneapi.endpoints;

public class ServiceEndpoints implements ServiceEndpointsInterface {
	
	private static ServiceEndpoints INSTANCE=new ServiceEndpoints();
	
	public ServiceEndpointsInterface getInstance() { return INSTANCE; }
	
	/**
	 Specifies the OneAPI network endpoint for the Payment API : Charge Amount method 
	*/  
	protected static String AmountCharge="http://localhost:8080/OneAPI/AmountChargeService/1/payment/{endUserId}/transactions/amount";
	/**
	 Specifies the OneAPI network endpoint for the Payment API : Refund Amount method 
	*/  
	protected static String AmountRefund="http://localhost:8080/OneAPI/AmountRefundService/1/payment/{endUserId}/transactions/amount";
	/**
	 Specifies the OneAPI network endpoint for the Payment API : Reserve Amount method 
	*/  
	protected static String AmountReserve="http://localhost:8080/OneAPI/AmountReserveService/1/payment/{endUserId}/transactions/amountReservation";
	/**
	 Specifies the OneAPI network endpoint for the Payment API : Reserve Additional Amount method 
	*/  
	protected static String AmountReserveAdditional="http://localhost:8080/OneAPI/AmountReserveAdditionalService/1/payment/{endUserId}/transactions/amountReservation/{transactionId}";
	/**
	 Specifies the OneAPI network endpoint for the Payment API : Charge Reserved method 
	*/  
	protected static String AmountReservationCharge="http://localhost:8080/OneAPI/AmountReserveChargeService/1/payment/{endUserId}/transactions/amountReservation/{transactionId}";
	/**
	 Specifies the OneAPI network endpoint for the Payment API : Release Amount Reserved method 
	*/  
	protected static String AmountReservationRelease="http://localhost:8080/OneAPI/AmountReserveReleaseService/1/payment/{endUserId}/transactions/amountReservation/{transactionId}";

	/**
	 Specifies the OneAPI network endpoint for the Location API : Locate Terminal method 
	*/  
	protected static String Location="http://localhost:8080/OneAPI/LocationService/1/location/queries/location";

	/**
	 Specifies the OneAPI network endpoint for the SMS : Send SMS method 
	*/  
	protected static String SendSMS="http://localhost:8080/OneAPI/SendSMSService/1/smsmessaging/outbound/{senderAddress}/requests";
	/**
	 Specifies the OneAPI network endpoint for the SMS : Query (Sent) SMS Delivery Status method 
	*/  
	protected static String QuerySMSDelivery="http://localhost:8080/OneAPI/QuerySMSService/1/smsmessaging/outbound/{senderAddress}/requests/{requestId}/deliveryInfos";
	/**
	 Specifies the OneAPI network endpoint for the SMS : Subscribe To (Sent) SMS Delivery Notifications method 
	*/  
	protected static String SMSDeliverySubscriptions="http://localhost:8080/OneAPI/SMSDeliveryService/1/smsmessaging/outbound/{senderAddress}/subscriptions";
	/**
	 Specifies the OneAPI network endpoint for the SMS : Cancel Subscription to (Sent) SMS Delivery Notifications method 
	*/  
	protected static String CancelSMSDeliverySubscription="http://localhost:8080/OneAPI/CancelSMSDeliveryService/1/smsmessaging/outbound/subscriptions/{subscriptionId}";
	/**
	 Specifies the OneAPI network endpoint for the SMS : Receive SMS method 
	*/  
	protected static String RetrieveSMS="http://localhost:8080/OneAPI/RetrieveSMSService/1/smsmessaging/inbound/registrations/{registrationId}/messages/?maxBatchSize={maxBatchSize}";
	/**
	 Specifies the OneAPI network endpoint for the SMS : Subscribe To (Received) SMS Receipt Notifications method 
	*/  
	protected static String SMSReceiptSubscriptions="http://localhost:8080/OneAPI/SMSReceiptService/1/smsmessaging/inbound/subscriptions";
	/**
	 Specifies the OneAPI network endpoint for the SMS : Cancel Subscription to (Received) SMS Receipt Notifications method 
	*/  
	protected static String CancelSMSReceiptSubscription="http://localhost:8080/OneAPI/CancelSMSReceiptService/1/smsmessaging/inbound/subscriptions/{subscriptionId}";

	/**
	 Specifies the OneAPI network endpoint for the MMS : Send MMS method 
	*/  
	protected static String SendMMS="http://localhost:8080/OneAPI/SendMMSService/1/messaging/outbound/{senderAddress}/requests";
	/**
	 Specifies the OneAPI network endpoint for the MMS : Query (Sent) MMS Delivery Status method 
	*/  
	protected static String QueryMMSDelivery="http://localhost:8080/OneAPI/QueryMMSService/1/messaging/outbound/{senderAddress}/requests/{requestId}/deliveryInfos";
	/**
	 Specifies the OneAPI network endpoint for the MMS : Subscribe To (Sent) MMS Delivery Notifications method 
	*/  
	protected static String MMSDeliverySubscriptions="http://localhost:8080/OneAPI/MMSDeliveryService/1/messaging/outbound/{senderAddress}/subscriptions";
	/**
	 Specifies the OneAPI network endpoint for the MMS : Cancel Subscription to (Sent) MMS Delivery Notifications method 
	*/  
	protected static String CancelMMSDeliverySubscription="http://localhost:8080/OneAPI/CancelMMSDeliveryService/1/messaging/outbound/subscriptions/{subscriptionId}";
	/**
	 Specifies the OneAPI network endpoint for the MMS : Receive (Full) MMS message method 
	*/  
	protected static String RetrieveMMS="http://localhost:8080/OneAPI/RetrieveMMSService/1/messaging/inbound/registrations/{registrationId}/messages/?maxBatchSize={maxBatchSize}";
	/**
	 Specifies the OneAPI network endpoint for the MMS : Receive MMS message list method 
	*/  
	protected static String RetrieveMMSMessage="http://localhost:8080/OneAPI/RetrieveMMSMessageService/1/messaging/inbound/registrations/{registrationId}/messages/{messageId}?resFormat={resFormat}";
	/**
	 Specifies the OneAPI network endpoint for the MMS : Subscribe To (Received) MMS Receipt Notifications method 
	*/  
	protected static String MMSReceiptSubscriptions="http://localhost:8080/OneAPI/MMSReceiptService/1/messaging/inbound/subscriptions";
	/**
	 Specifies the OneAPI network endpoint for the MMS : Cancel Subscription to (Received) MMS Receipt Notifications method 
	*/  
	protected static String CancelMMSReceiptSubscription="http://localhost:8080/OneAPI/CancelMMSReceiptService/1/messaging/inbound/subscriptions/{subscriptionId}";
	
	
	/**
		Retrieves the OneAPI network endpoint for the Payment API : Charge Amount method
	*/
	public String getAmountChargeEndpoint() { return AmountCharge; }
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Refund Amount method
	 */
	public String getAmountRefundEndpoint() { return AmountRefund; }
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Reserve Amount method
	 */
	public String getAmountReserveEndpoint() { return AmountReserve; }
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Reserve Additional Amount method
	 */
	public String getAmountReserveAdditionalEndpoint() { return AmountReserveAdditional; }
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Charge Reserved method
	 */
	public String getAmountReservationChargeEndpoint() { return AmountReservationCharge; }
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Release Amount Reserved method
	 */
	public String getAmountReservationReleaseEndpoint() { return AmountReservationRelease; }

	/**
	Retrieves the OneAPI network endpoint for the Location API : Locate Terminal method
	 */
	public String getLocationEndpoint() { return Location; }
	
	/**
	Retrieves the OneAPI network endpoint for the SMS : Send SMS method
	 */
	public String getSendSMSEndpoint() { return SendSMS; }
	/**
	Retrieves the OneAPI network endpoint for the SMS : Query (Sent) SMS Delivery Status method
	 */
	public String getQuerySMSDeliveryEndpoint() { return QuerySMSDelivery; }
	/**
	Retrieves the OneAPI network endpoint for the SMS : Subscribe To (Sent) SMS Delivery Notifications method
	 */
	public String getSMSDeliverySubscriptionsEndpoint() { return SMSDeliverySubscriptions; }
	/**
	Retrieves the OneAPI network endpoint for the SMS : Cancel Subscription to (Sent) SMS Delivery Notifications method
	 */
	public String getCancelSMSDeliverySubscriptionEndpoint() { return CancelSMSDeliverySubscription; }
	/**
	Retrieves the OneAPI network endpoint for the SMS : Receive SMS method
	 */
	public String getRetrieveSMSEndpoint() { return RetrieveSMS; }
	/**
	Retrieves the OneAPI network endpoint for the SMS : Subscribe To (Received) SMS Receipt Notifications method
	 */
	public String getSMSReceiptSubscriptionsEndpoint() { return SMSReceiptSubscriptions; }
	/**
	Retrieves the OneAPI network endpoint for the SMS : Cancel Subscription to (Received) SMS Receipt Notifications method
	 */
	public String getCancelSMSReceiptSubscriptionEndpoint() { return CancelSMSReceiptSubscription; }

	/**
	Retrieves the OneAPI network endpoint for the MMS : Send MMS method
	 */
	public String getSendMMSEndpoint() { return SendMMS; }
	/**
	Retrieves the OneAPI network endpoint for the MMS : Query (Sent) MMS Delivery Status method
	 */
	public String getQueryMMSDeliveryEndpoint() { return QueryMMSDelivery; }
	/**
	Retrieves the OneAPI network endpoint for the MMS : Subscribe To (Sent) MMS Delivery Notifications method
	 */
	public String getMMSDeliverySubscriptionsEndpoint() { return MMSDeliverySubscriptions; }
	/**
	Retrieves the OneAPI network endpoint for the MMS : Cancel Subscription to (Sent) MMS Delivery Notifications method
	 */
	public String getCancelMMSDeliverySubscriptionEndpoint() { return CancelMMSDeliverySubscription; }
	/**
	Retrieves the OneAPI network endpoint for the MMS : Receive (Full) MMS message method
	 */
	public String getRetrieveMMSEndpoint() { return RetrieveMMS; }
	/**
	Retrieves the OneAPI network endpoint for the MMS : Receive MMS message list method
	 */
	public String getRetrieveMMSMessageEndpoint() { return RetrieveMMSMessage; }
	/**
	Retrieves the OneAPI network endpoint for the MMS : Subscribe To (Received) MMS Receipt Notifications method
	 */
	public String getMMSReceiptSubscriptionsEndpoint() { return MMSReceiptSubscriptions; }
	/**
	Retrieves the OneAPI network endpoint for the MMS : Cancel Subscription to (Received) MMS Receipt Notifications method
	 */
	public String getCancelMMSReceiptSubscriptionEndpoint() { return CancelMMSReceiptSubscription; }

//	public static final ServiceEndpoints INSTANCE=new ServiceEndpoints();
//
//	public static ServiceEndpoints getEndpoints() {
//		return INSTANCE;
//	}

}
