﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18408.
// 
#pragma warning disable 1591

namespace TriplosDialogueWsTest.CtripMailAdapterWs {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CtripMailAdapterSoap", Namespace="http://ctrip.tripolis.com.cn/")]
    public partial class CtripMailAdapter : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback sendMailOperationCompleted;
        
        private System.Threading.SendOrPostCallback exportReportOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CtripMailAdapter() {
            this.Url = global::TriplosDialogueWsTest.Properties.Settings.Default.TriplosDialogueWsTest_CtripMailAdapterWs_CtripMailAdapter1;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event sendMailCompletedEventHandler sendMailCompleted;
        
        /// <remarks/>
        public event exportReportCompletedEventHandler exportReportCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ctrip.tripolis.com.cn/sendMail", RequestNamespace="http://ctrip.tripolis.com.cn/", ResponseNamespace="http://ctrip.tripolis.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string sendMail(string APIKey, string fromName, string fromEmail, string subject, string mailContent, string toEmail, System.DateTime scheduleTime) {
            object[] results = this.Invoke("sendMail", new object[] {
                        APIKey,
                        fromName,
                        fromEmail,
                        subject,
                        mailContent,
                        toEmail,
                        scheduleTime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void sendMailAsync(string APIKey, string fromName, string fromEmail, string subject, string mailContent, string toEmail, System.DateTime scheduleTime) {
            this.sendMailAsync(APIKey, fromName, fromEmail, subject, mailContent, toEmail, scheduleTime, null);
        }
        
        /// <remarks/>
        public void sendMailAsync(string APIKey, string fromName, string fromEmail, string subject, string mailContent, string toEmail, System.DateTime scheduleTime, object userState) {
            if ((this.sendMailOperationCompleted == null)) {
                this.sendMailOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendMailOperationCompleted);
            }
            this.InvokeAsync("sendMail", new object[] {
                        APIKey,
                        fromName,
                        fromEmail,
                        subject,
                        mailContent,
                        toEmail,
                        scheduleTime}, this.sendMailOperationCompleted, userState);
        }
        
        private void OnsendMailOperationCompleted(object arg) {
            if ((this.sendMailCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendMailCompleted(this, new sendMailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ctrip.tripolis.com.cn/exportReport", RequestNamespace="http://ctrip.tripolis.com.cn/", ResponseNamespace="http://ctrip.tripolis.com.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CSVReportData exportReport(string APIKey, System.DateTime startTime, System.DateTime endTime) {
            object[] results = this.Invoke("exportReport", new object[] {
                        APIKey,
                        startTime,
                        endTime});
            return ((CSVReportData)(results[0]));
        }
        
        /// <remarks/>
        public void exportReportAsync(string APIKey, System.DateTime startTime, System.DateTime endTime) {
            this.exportReportAsync(APIKey, startTime, endTime, null);
        }
        
        /// <remarks/>
        public void exportReportAsync(string APIKey, System.DateTime startTime, System.DateTime endTime, object userState) {
            if ((this.exportReportOperationCompleted == null)) {
                this.exportReportOperationCompleted = new System.Threading.SendOrPostCallback(this.OnexportReportOperationCompleted);
            }
            this.InvokeAsync("exportReport", new object[] {
                        APIKey,
                        startTime,
                        endTime}, this.exportReportOperationCompleted, userState);
        }
        
        private void OnexportReportOperationCompleted(object arg) {
            if ((this.exportReportCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.exportReportCompleted(this, new exportReportCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ctrip.tripolis.com.cn/")]
    public partial class CSVReportData {
        
        private string sentField;
        
        private string openedField;
        
        private string clickedField;
        
        private string bouncedField;
        
        /// <remarks/>
        public string sent {
            get {
                return this.sentField;
            }
            set {
                this.sentField = value;
            }
        }
        
        /// <remarks/>
        public string opened {
            get {
                return this.openedField;
            }
            set {
                this.openedField = value;
            }
        }
        
        /// <remarks/>
        public string clicked {
            get {
                return this.clickedField;
            }
            set {
                this.clickedField = value;
            }
        }
        
        /// <remarks/>
        public string bounced {
            get {
                return this.bouncedField;
            }
            set {
                this.bouncedField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void sendMailCompletedEventHandler(object sender, sendMailCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendMailCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal sendMailCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void exportReportCompletedEventHandler(object sender, exportReportCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class exportReportCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal exportReportCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public CSVReportData Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((CSVReportData)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591