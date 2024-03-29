﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clients.DemoClient.DoctorMonitoringService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DoctorMonitoringService.IDoctorMonitoringService", CallbackContract=typeof(Clients.DemoClient.DoctorMonitoringService.IDoctorMonitoringServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
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
        Models.PatientVitals.PatientVitals[] ReadPreviousVitals(string patientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDoctorMonitoringService/ReadPreviousVitals", ReplyAction="http://tempuri.org/IDoctorMonitoringService/ReadPreviousVitalsResponse")]
        System.Threading.Tasks.Task<Models.PatientVitals.PatientVitals[]> ReadPreviousVitalsAsync(string patientId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDoctorMonitoringServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/ReceiveVitals")]
        void ReceiveVitals(Models.PatientVitals.PatientVitals vitals);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDoctorMonitoringService/ReceiveAlerts")]
        void ReceiveAlerts(Models.PatientAlert.PatientAlert alert);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDoctorMonitoringServiceChannel : Clients.DemoClient.DoctorMonitoringService.IDoctorMonitoringService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DoctorMonitoringServiceClient : System.ServiceModel.DuplexClientBase<Clients.DemoClient.DoctorMonitoringService.IDoctorMonitoringService>, Clients.DemoClient.DoctorMonitoringService.IDoctorMonitoringService {
        
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
        
        public Models.PatientVitals.PatientVitals[] ReadPreviousVitals(string patientId) {
            return base.Channel.ReadPreviousVitals(patientId);
        }
        
        public System.Threading.Tasks.Task<Models.PatientVitals.PatientVitals[]> ReadPreviousVitalsAsync(string patientId) {
            return base.Channel.ReadPreviousVitalsAsync(patientId);
        }
    }
}
