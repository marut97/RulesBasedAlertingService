using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Models.HospitalBed;

namespace Controllers.HospitalBedService
{
    [ServiceContract]
    public interface IHospitalBedRegistrationService
    {

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/RegisterBeds", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void RegisterNewBeds(HospitalBed beds);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/UpdateBed", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void UpdateBedOccupancy(HospitalBed bed);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/DeleteBeds", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DeleteBeds(HospitalBed bed);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadAllBeds", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<HospitalBed> ReadAllBeds();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadBed/{bed}", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        HospitalBed ReadBed(string bed);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadEmptyBeds/{id}", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<HospitalBed> ReadEmptyBeds(string id);
    }
}
