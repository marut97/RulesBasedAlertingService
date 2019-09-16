﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clients.DoctorMonitoringClient.DoctorMonitoringService {
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
        private Clients.DoctorMonitoringClient.DoctorMonitoringService.Vitals[] VitalsField;
        
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
        public Clients.DoctorMonitoringClient.DoctorMonitoringService.Vitals[] Vitals {
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PatientAlert", Namespace="http://schemas.datacontract.org/2004/07/Models.PatientAlert")]
    [System.SerializableAttribute()]
    public partial class PatientAlert : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Clients.DoctorMonitoringClient.DoctorMonitoringService.DeviceAlert[] CriticalAlertsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool MuteAlertField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Clients.DoctorMonitoringClient.DoctorMonitoringService.DeviceAlert[] WarningAlertsField;
        
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
        public Clients.DoctorMonitoringClient.DoctorMonitoringService.DeviceAlert[] CriticalAlerts {
            get {
                return this.CriticalAlertsField;
            }
            set {
                if ((object.ReferenceEquals(this.CriticalAlertsField, value) != true)) {
                    this.CriticalAlertsField = value;
                    this.RaisePropertyChanged("CriticalAlerts");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool MuteAlert {
            get {
                return this.MuteAlertField;
            }
            set {
                if ((this.MuteAlertField.Equals(value) != true)) {
                    this.MuteAlertField = value;
                    this.RaisePropertyChanged("MuteAlert");
                }
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
        public Clients.DoctorMonitoringClient.DoctorMonitoringService.DeviceAlert[] WarningAlerts {
            get {
                return this.WarningAlertsField;
            }
            set {
                if ((object.ReferenceEquals(this.WarningAlertsField, value) != true)) {
                    this.WarningAlertsField = value;
                    this.RaisePropertyChanged("WarningAlerts");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DeviceAlert", Namespace="http://schemas.datacontract.org/2004/07/Models.PatientAlert")]
    [System.SerializableAttribute()]
    public partial class DeviceAlert : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeviceIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
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
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DoctorMonitoringService.IDoctorMonitoringService", CallbackContract=typeof(Clients.DoctorMonitoringClient.DoctorMonitoringService.IDoctorMonitoringServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IDoctorMonitoringService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/SubscribeToVitals")]
        void SubscribeToVitals(string patientId, string doctorId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/SubscribeToVitals")]
        System.Threading.Tasks.Task SubscribeToVitalsAsync(string patientId, string doctorId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/UnsubscribeToVitals")]
        void UnsubscribeToVitals(string patientId, string doctorId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/UnsubscribeToVitals")]
        System.Threading.Tasks.Task UnsubscribeToVitalsAsync(string patientId, string doctorId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/SubscribeToPatientAlerts")]
        void SubscribeToPatientAlerts(string doctorId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/SubscribeToPatientAlerts")]
        System.Threading.Tasks.Task SubscribeToPatientAlertsAsync(string doctorId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/UnsubscribeToPatientAlerts")]
        void UnsubscribeToPatientAlerts(string doctorId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/UnsubscribeToPatientAlerts")]
        System.Threading.Tasks.Task UnsubscribeToPatientAlertsAsync(string doctorId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDoctorMonitoringService/ReadPreviousVitals", ReplyAction="http://tempuri.org/IDoctorMonitoringService/ReadPreviousVitalsResponse")]
        Clients.DoctorMonitoringClient.DoctorMonitoringService.PatientVitals[] ReadPreviousVitals(string patientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDoctorMonitoringService/ReadPreviousVitals", ReplyAction="http://tempuri.org/IDoctorMonitoringService/ReadPreviousVitalsResponse")]
        System.Threading.Tasks.Task<Clients.DoctorMonitoringClient.DoctorMonitoringService.PatientVitals[]> ReadPreviousVitalsAsync(string patientId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDoctorMonitoringServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/ReceiveVitals")]
        void ReceiveVitals(Clients.DoctorMonitoringClient.DoctorMonitoringService.PatientVitals vitals);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/ReceiveAlerts")]
        void ReceiveAlerts(Clients.DoctorMonitoringClient.DoctorMonitoringService.PatientAlert alert);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDoctorMonitoringServiceChannel : Clients.DoctorMonitoringClient.DoctorMonitoringService.IDoctorMonitoringService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DoctorMonitoringServiceClient : System.ServiceModel.DuplexClientBase<Clients.DoctorMonitoringClient.DoctorMonitoringService.IDoctorMonitoringService>, Clients.DoctorMonitoringClient.DoctorMonitoringService.IDoctorMonitoringService {
        
        public DoctorMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public DoctorMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public DoctorMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DoctorMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DoctorMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SubscribeToVitals(string patientId, string doctorId) {
            base.Channel.SubscribeToVitals(patientId, doctorId);
        }
        
        public System.Threading.Tasks.Task SubscribeToVitalsAsync(string patientId, string doctorId) {
            return base.Channel.SubscribeToVitalsAsync(patientId, doctorId);
        }
        
        public void UnsubscribeToVitals(string patientId, string doctorId) {
            base.Channel.UnsubscribeToVitals(patientId, doctorId);
        }
        
        public System.Threading.Tasks.Task UnsubscribeToVitalsAsync(string patientId, string doctorId) {
            return base.Channel.UnsubscribeToVitalsAsync(patientId, doctorId);
        }
        
        public void SubscribeToPatientAlerts(string doctorId) {
            base.Channel.SubscribeToPatientAlerts(doctorId);
        }
        
        public System.Threading.Tasks.Task SubscribeToPatientAlertsAsync(string doctorId) {
            return base.Channel.SubscribeToPatientAlertsAsync(doctorId);
        }
        
        public void UnsubscribeToPatientAlerts(string doctorId) {
            base.Channel.UnsubscribeToPatientAlerts(doctorId);
        }
        
        public System.Threading.Tasks.Task UnsubscribeToPatientAlertsAsync(string doctorId) {
            return base.Channel.UnsubscribeToPatientAlertsAsync(doctorId);
        }
        
        public Clients.DoctorMonitoringClient.DoctorMonitoringService.PatientVitals[] ReadPreviousVitals(string patientId) {
            return base.Channel.ReadPreviousVitals(patientId);
        }
        
        public System.Threading.Tasks.Task<Clients.DoctorMonitoringClient.DoctorMonitoringService.PatientVitals[]> ReadPreviousVitalsAsync(string patientId) {
            return base.Channel.ReadPreviousVitalsAsync(patientId);
        }
    }
}
