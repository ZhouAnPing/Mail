package org.gsm.oneapi.endpoints;

public interface ServiceEndpointsInterface {
	
	public ServiceEndpointsInterface getInstance();

	/**
	Retrieves the OneAPI network endpoint for the Payment API : Charge Amount method
	*/
	public String getAmountChargeEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Refund Amount method
	 */
	public String getAmountRefundEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Reserve Amount method
	 */
	public String getAmountReserveEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Reserve Additional Amount method
	 */
	public String getAmountReserveAdditionalEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Charge Reserved method
	 */
	public String getAmountReservationChargeEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the Payment API : Release Amount Reserved method
	 */
	public String getAmountReservationReleaseEndpoint();
	
	/**
	Retrieves the OneAPI network endpoint for the Location API : Locate Terminal method
	 */
	public String getLocationEndpoint();
	
	/**
	Retrieves the OneAPI network endpoint for the SMS : Send SMS method
	 */
	public String getSendSMSEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the SMS : Query (Sent) SMS Delivery Status method
	 */
	public String getQuerySMSDeliveryEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the SMS : Subscribe To (Sent) SMS Delivery Notifications method
	 */
	public String getSMSDeliverySubscriptionsEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the SMS : Cancel Subscription to (Sent) SMS Delivery Notifications method
	 */
	public String getCancelSMSDeliverySubscriptionEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the SMS : Receive SMS method
	 */
	public String getRetrieveSMSEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the SMS : Subscribe To (Received) SMS Receipt Notifications method
	 */
	public String getSMSReceiptSubscriptionsEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the SMS : Cancel Subscription to (Received) SMS Receipt Notifications method
	 */
	public String getCancelSMSReceiptSubscriptionEndpoint();
	
	/**
	Retrieves the OneAPI network endpoint for the MMS : Send MMS method
	 */
	public String getSendMMSEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the MMS : Query (Sent) MMS Delivery Status method
	 */
	public String getQueryMMSDeliveryEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the MMS : Subscribe To (Sent) MMS Delivery Notifications method
	 */
	public String getMMSDeliverySubscriptionsEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the MMS : Cancel Subscription to (Sent) MMS Delivery Notifications method
	 */
	public String getCancelMMSDeliverySubscriptionEndpoint() ;
	/**
	Retrieves the OneAPI network endpoint for the MMS : Receive (Full) MMS message method
	 */
	public String getRetrieveMMSEndpoint() ;
	/**
	Retrieves the OneAPI network endpoint for the MMS : Receive MMS message list method
	 */
	public String getRetrieveMMSMessageEndpoint() ;
	/**
	Retrieves the OneAPI network endpoint for the MMS : Subscribe To (Received) MMS Receipt Notifications method
	 */
	public String getMMSReceiptSubscriptionsEndpoint();
	/**
	Retrieves the OneAPI network endpoint for the MMS : Cancel Subscription to (Received) MMS Receipt Notifications method
	 */
	public String getCancelMMSReceiptSubscriptionEndpoint();

}
