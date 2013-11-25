namespace TripolisDialogueAdapter
{
    /// <summary>
    /// Mail data
    /// </summary>
    public class MailData
    {

        private System.Collections.ArrayList contactJsonList = new System.Collections.ArrayList();
        /// <summary>
        /// Contact String List, format:fieldname1:value1,fieldname2:value2. 
        /// for example, if the contact has two column:email,name, then the format should like: email:Test@hp.com.com,name:Tripolist
        /// </summary>
        public System.Collections.ArrayList ContactJsonList
        {
            get { return contactJsonList; }
            set { contactJsonList = value; }
        }

        /// <summary>
        /// Sender
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// from email address
        /// </summary>
        public string fromAddress { get; set; }

        /// <summary>
        /// reply email address
        /// </summary>
        public string replyAddress { get; set; }

        /// <summary>
        /// mail subject
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// mail body
        /// </summary>
        public string mailBody { get; set; }
    }
}
