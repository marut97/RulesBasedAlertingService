﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clients.MonitoringClient.VitalsDataService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PatientVitals", Namespace="http://schemas.datacontract.org/2004/07/Models.PatientVitals")]
    [System.SerializableAttribute()]
    public partial class PatientVitals : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Clients.MonitoringClient.VitalsDataService.Vitals[] VitalsField;
        
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
        public string PatientId {
            get {
                return this.PatientIdField;
            }
            set {
                if ((object.ReferenceEquals(this.PatientIdField, value) != true)) {
                    this.PatientIdField = value;
                    this.RaisePropertyChanged("PatientId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Clients.MonitoringClient.VitalsDataService.Vitals[] Vitals {
            get {
                return this.VitalsField;
            }
            set {
                if ((object.ReferenceEquals(this.VitalsField, value) != true)) {
                    this.VitalsField = value;
                    this.RaisePropertyChanged("Vitals");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Vitals", Namespace="http://schemas.datacontract.org/2004/07/Models.PatientVitals")]
    [System.SerializableAttribute()]
    public partial class Vitals : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeviceIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ValueField;
        
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
        public string DeviceId {
            get {
                return this.DeviceIdField;
            }
            set {
                if ((object.ReferenceEquals(this.DeviceIdField, value) != true)) {
                    this.DeviceIdField = value;
                    this.RaisePropertyChanged("DeviceId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Value {
            get {
                return this.ValueField;
            }
            set {
                if ((this.ValueField.Equals(value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="VitalsDataService.IVitalsDataService")]
    public interface IVitalsDataService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVitalsDataService/WriteVitals", ReplyAction="http://tempuri.org/IVitalsDataService/WriteVitalsResponse")]
        void WriteVitals(Clients.MonitoringClient.VitalsDataService.PatientVitals vitals);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVitalsDataService/WriteVitals", ReplyAction="http://tempuri.org/IVitalsDataService/WriteVitalsResponse")]
        System.Threading.Tasks.Task WriteVitalsAsync(Clients.MonitoringClient.VitalsDataService.PatientVitals vitals);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IVitalsDataServiceChannel : Clients.MonitoringClient.VitalsDataService.IVitalsDataService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VitalsDataServiceClient : System.ServiceModel.ClientBase<Clients.MonitoringClient.VitalsDataService.IVitalsDataService>, Clients.MonitoringClient.VitalsDataService.IVitalsDataService {
        
        public VitalsDataServiceClient() {
        }
        
        public VitalsDataServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VitalsDataServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VitalsDataServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VitalsDataServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void WriteVitals(Clients.MonitoringClient.VitalsDataService.PatientVitals vitals) {
            base.Channel.WriteVitals(vitals);
        }
        
        public System.Threading.Tasks.Task WriteVitalsAsync(Clients.MonitoringClient.VitalsDataService.PatientVitals vitals) {
            return base.Channel.WriteVitalsAsync(vitals);
        }
    }
}