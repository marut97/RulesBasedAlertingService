using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Models.Patient;

namespace Controllers.PatientRegistrationService
{
    [ServiceContract]
    public interface IPatientRegistrationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/RegisterPatient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void RegisterNewPatient(Patient patient);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/UpdatePatient", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void UpdatePatient(Patient patient);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/DeletePatient", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DeletePatient(string patientId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadAllPatients", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Patient> ReadAllPatients();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadPatient/{patientId}", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Patient ReadPatient(string patientId);
    }

}
