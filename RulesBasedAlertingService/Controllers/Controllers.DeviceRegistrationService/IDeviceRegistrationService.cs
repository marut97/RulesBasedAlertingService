using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Models.Device;

namespace Controllers.DeviceRegistrationService
{

    [ServiceContract]
    public interface IDeviceRegistrationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/RegisterDevice", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void RegisterNewDevice(Device device);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/UpdateDevice", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void UpdateDevice(Device device);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/DeleteDevice", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DeleteDevice(string deviceId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadAllDevices", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Device> ReadAllDevices();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReadDevice/{deviceId}", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Device ReadDevice(string deviceId);
    }
}
