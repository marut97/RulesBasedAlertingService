﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clients.MonitoringClient.NurseMonitoringService {
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
        private Clients.MonitoringClient.NurseMonitoringService.Vitals[] VitalsField;
        
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
        public Clients.MonitoringClient.NurseMonitoringService.Vitals[] Vitals {
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
        private Clients.MonitoringClient.NurseMonitoringService.DeviceAlert[] CriticalAlertsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool MuteAlertField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Clients.MonitoringClient.NurseMonitoringService.DeviceAlert[] WarningAlertsField;
        
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
        public Clients.MonitoringClient.NurseMonitoringService.DeviceAlert[] CriticalAlerts {
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
        public Clients.MonitoringClient.NurseMonitoringService.DeviceAlert[] WarningAlerts {
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NurseMonitoringService.INurseMonitoringService", CallbackContract=typeof(Clients.MonitoringClient.NurseMonitoringService.INurseMonitoringServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface INurseMonitoringService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/SubscribeToVitals")]
        void SubscribeToVitals(string patientId, string locationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/SubscribeToVitals")]
        System.Threading.Tasks.Task SubscribeToVitalsAsync(string patientId, string locationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/UnsubscribeToVitals")]
        void UnsubscribeToVitals(string patientId, string locationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/UnsubscribeToVitals")]
        System.Threading.Tasks.Task UnsubscribeToVitalsAsync(string patientId, string locationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/SubscribeToPatientAlerts")]
        void SubscribeToPatientAlerts(string locationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/SubscribeToPatientAlerts")]
        System.Threading.Tasks.Task SubscribeToPatientAlertsAsync(string locationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/UnsubscribeToPatientAlerts")]
        void UnsubscribeToPatientAlerts(string locationId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/UnsubscribeToPatientAlerts")]
        System.Threading.Tasks.Task UnsubscribeToPatientAlertsAsync(string locationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INurseMonitoringService/ReadPreviousVitals", ReplyAction="http://tempuri.org/INurseMonitoringService/ReadPreviousVitalsResponse")]
        Clients.MonitoringClient.NurseMonitoringService.PatientVitals[] ReadPreviousVitals(string patientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INurseMonitoringService/ReadPreviousVitals", ReplyAction="http://tempuri.org/INurseMonitoringService/ReadPreviousVitalsResponse")]
        System.Threading.Tasks.Task<Clients.MonitoringClient.NurseMonitoringService.PatientVitals[]> ReadPreviousVitalsAsync(string patientId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/AlertDoctor")]
        void AlertDoctor(Clients.MonitoringClient.NurseMonitoringService.PatientAlert alert);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/AlertDoctor")]
        System.Threading.Tasks.Task AlertDoctorAsync(Clients.MonitoringClient.NurseMonitoringService.PatientAlert alert);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/MuteAlerts")]
        void MuteAlerts(string patientId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/MuteAlerts")]
        System.Threading.Tasks.Task MuteAlertsAsync(string patientId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/UnmuteAlerts")]
        void UnmuteAlerts(string patientId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/UnmuteAlerts")]
        System.Threading.Tasks.Task UnmuteAlertsAsync(string patientId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INurseMonitoringServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/ReceiveVitals")]
        void ReceiveVitals(Clients.MonitoringClient.NurseMonitoringService.PatientVitals vitals);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/INurseMonitoringService/ReceiveAlerts")]
        void ReceiveAlerts(Clients.MonitoringClient.NurseMonitoringService.PatientAlert alert);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INurseMonitoringServiceChannel : Clients.MonitoringClient.NurseMonitoringService.INurseMonitoringService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NurseMonitoringServiceClient : System.ServiceModel.DuplexClientBase<Clients.MonitoringClient.NurseMonitoringService.INurseMonitoringService>, Clients.MonitoringClient.NurseMonitoringService.INurseMonitoringService {
        
        public NurseMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public NurseMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public NurseMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public NurseMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public NurseMonitoringServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SubscribeToVitals(string patientId, string locationId) {
            base.Channel.SubscribeToVitals(patientId, locationId);
        }
        
        public System.Threading.Tasks.Task SubscribeToVitalsAsync(string patientId, string locationId) {
            return base.Channel.SubscribeToVitalsAsync(patientId, locationId);
        }
        
        public void UnsubscribeToVitals(string patientId, string locationId) {
            base.Channel.UnsubscribeToVitals(patientId, locationId);
        }
        
        public System.Threading.Tasks.Task UnsubscribeToVitalsAsync(string patientId, string locationId) {
            return base.Channel.UnsubscribeToVitalsAsync(patientId, locationId);
        }
        
        public void SubscribeToPatientAlerts(string locationId) {
            base.Channel.SubscribeToPatientAlerts(locationId);
        }
        
        public System.Threading.Tasks.Task SubscribeToPatientAlertsAsync(string locationId) {
            return base.Channel.SubscribeToPatientAlertsAsync(locationId);
        }
        
        public void UnsubscribeToPatientAlerts(string locationId) {
            base.Channel.UnsubscribeToPatientAlerts(locationId);
        }
        
        public System.Threading.Tasks.Task UnsubscribeToPatientAlertsAsync(string locationId) {
            return base.Channel.UnsubscribeToPatientAlertsAsync(locationId);
        }
        
        public Clients.MonitoringClient.NurseMonitoringService.PatientVitals[] ReadPreviousVitals(string patientId) {
            return base.Channel.ReadPreviousVitals(patientId);
        }
        
        public System.Threading.Tasks.Task<Clients.MonitoringClient.NurseMonitoringService.PatientVitals[]> ReadPreviousVitalsAsync(string patientId) {
            return base.Channel.ReadPreviousVitalsAsync(patientId);
        }
        
        public void AlertDoctor(Clients.MonitoringClient.NurseMonitoringService.PatientAlert alert) {
            base.Channel.AlertDoctor(alert);
        }
        
        public System.Threading.Tasks.Task AlertDoctorAsync(Clients.MonitoringClient.NurseMonitoringService.PatientAlert alert) {
            return base.Channel.AlertDoctorAsync(alert);
        }
        
        public void MuteAlerts(string patientId) {
            base.Channel.MuteAlerts(patientId);
        }
        
        public System.Threading.Tasks.Task MuteAlertsAsync(string patientId) {
            return base.Channel.MuteAlertsAsync(patientId);
        }
        
        public void UnmuteAlerts(string patientId) {
            base.Channel.UnmuteAlerts(patientId);
        }
        
        public System.Threading.Tasks.Task UnmuteAlertsAsync(string patientId) {
            return base.Channel.UnmuteAlertsAsync(patientId);
        }
    }
}
