﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartWCFClient_2.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Human", Namespace="http://schemas.datacontract.org/2004/07/SmartWcfService1")]
    [System.SerializableAttribute()]
    public partial class Human : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime BirthDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime BirthDate {
            get {
                return this.BirthDateField;
            }
            set {
                if ((this.BirthDateField.Equals(value) != true)) {
                    this.BirthDateField = value;
                    this.RaisePropertyChanged("BirthDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetHumans", ReplyAction="http://tempuri.org/IService1/GetHumansResponse")]
        SmartWCFClient_2.ServiceReference1.Human[] GetHumans(System.DateTime from, System.DateTime to);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetHumans", ReplyAction="http://tempuri.org/IService1/GetHumansResponse")]
        System.Threading.Tasks.Task<SmartWCFClient_2.ServiceReference1.Human[]> GetHumansAsync(System.DateTime from, System.DateTime to);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetHuman", ReplyAction="http://tempuri.org/IService1/GetHumanResponse")]
        SmartWCFClient_2.ServiceReference1.Human GetHuman(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetHuman", ReplyAction="http://tempuri.org/IService1/GetHumanResponse")]
        System.Threading.Tasks.Task<SmartWCFClient_2.ServiceReference1.Human> GetHumanAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddHuman", ReplyAction="http://tempuri.org/IService1/AddHumanResponse")]
        SmartWCFClient_2.ServiceReference1.Human AddHuman(SmartWCFClient_2.ServiceReference1.Human human);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddHuman", ReplyAction="http://tempuri.org/IService1/AddHumanResponse")]
        System.Threading.Tasks.Task<SmartWCFClient_2.ServiceReference1.Human> AddHumanAsync(SmartWCFClient_2.ServiceReference1.Human human);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : SmartWCFClient_2.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<SmartWCFClient_2.ServiceReference1.IService1>, SmartWCFClient_2.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SmartWCFClient_2.ServiceReference1.Human[] GetHumans(System.DateTime from, System.DateTime to) {
            return base.Channel.GetHumans(from, to);
        }
        
        public System.Threading.Tasks.Task<SmartWCFClient_2.ServiceReference1.Human[]> GetHumansAsync(System.DateTime from, System.DateTime to) {
            return base.Channel.GetHumansAsync(from, to);
        }
        
        public SmartWCFClient_2.ServiceReference1.Human GetHuman(string id) {
            return base.Channel.GetHuman(id);
        }
        
        public System.Threading.Tasks.Task<SmartWCFClient_2.ServiceReference1.Human> GetHumanAsync(string id) {
            return base.Channel.GetHumanAsync(id);
        }
        
        public SmartWCFClient_2.ServiceReference1.Human AddHuman(SmartWCFClient_2.ServiceReference1.Human human) {
            return base.Channel.AddHuman(human);
        }
        
        public System.Threading.Tasks.Task<SmartWCFClient_2.ServiceReference1.Human> AddHumanAsync(SmartWCFClient_2.ServiceReference1.Human human) {
            return base.Channel.AddHumanAsync(human);
        }
    }
}
