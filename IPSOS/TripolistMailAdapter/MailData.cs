namespace TripolistMailAdapter
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



        //private ContactInformation contactInfo;

        //internal ContactInformation ContactInfo
        //{
        //    get { return contactInfo; }
        //    set { contactInfo = value; }
        //}

        /// <summary>
        /// Sender Name
        /// </summary>
        public string SendName { get; set; }

        /// <summary>
        /// from email address
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// reply email address
        /// </summary>
        public string ReplyAddress { get; set; }

        /// <summary>
        /// mail subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// mail content
        /// </summary>
        public string Content { get; set; }
    }
}
