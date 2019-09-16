using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Models.Device;
using Models.PatientAdmission;

namespace Controllers.PatientAdmissionService
{

    [ServiceContract]
    public interface IPatientAdmissionService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AdmitPatient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void AdmitPatient(PatientAdmission patient);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/UpdateAdmission", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void UpdateAdmission(PatientAdmission patient);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/DischargePatient", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DischargePatient(string patientId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadAllAdmissions", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<PatientAdmission> ReadAllAdmissions();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadAdmission/{patientId}", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        PatientAdmission ReadAdmission(string patientId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddDeviceToPatient", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void AddDeviceToPatient(string patientId, string deviceId, Device device);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/RemoveDeviceFromPatient", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void RemoveDeviceFromPatient(string patientId, string deviceId);
    }

}
