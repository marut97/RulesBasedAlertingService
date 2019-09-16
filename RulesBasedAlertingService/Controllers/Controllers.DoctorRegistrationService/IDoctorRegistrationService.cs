using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Models.Doctor;

namespace Controllers.DoctorRegistrationService
{
    [ServiceContract]
    public interface IDoctorRegistrationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/RegisterDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void RegisterNewDoctor(Doctor doctor);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/UpdateDoctor", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void UpdateDoctor(Doctor doctor);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/DeleteDoctor", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DeleteDoctor(string doctorId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadAllDoctors", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Doctor> ReadAllDoctors();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadDoctor/{doctorId}", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Doctor ReadDoctor(string doctorId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadAvailableDoctor/{department}", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Doctor> ReadAvailableDoctors(string department);
    }

}
