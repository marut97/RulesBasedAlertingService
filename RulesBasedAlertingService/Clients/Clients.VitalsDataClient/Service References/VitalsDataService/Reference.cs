﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clients.VitalsDataClient.VitalsDataService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="VitalsDataService.IVitalsDataService")]
    public interface IVitalsDataService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVitalsDataService/WriteVitals", ReplyAction="http://tempuri.org/IVitalsDataService/WriteVitalsResponse")]
        void WriteVitals(Models.PatientVitals.PatientVitals vitals);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVitalsDataService/WriteVitals", ReplyAction="http://tempuri.org/IVitalsDataService/WriteVitalsResponse")]
        System.Threading.Tasks.Task WriteVitalsAsync(Models.PatientVitals.PatientVitals vitals);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IVitalsDataServiceChannel : Clients.VitalsDataClient.VitalsDataService.IVitalsDataService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VitalsDataServiceClient : System.ServiceModel.ClientBase<Clients.VitalsDataClient.VitalsDataService.IVitalsDataService>, Clients.VitalsDataClient.VitalsDataService.IVitalsDataService {
        
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
        
        public void WriteVitals(Models.PatientVitals.PatientVitals vitals) {
            base.Channel.WriteVitals(vitals);
        }
        
        public System.Threading.Tasks.Task WriteVitalsAsync(Models.PatientVitals.PatientVitals vitals) {
            return base.Channel.WriteVitalsAsync(vitals);
        }
    }
}