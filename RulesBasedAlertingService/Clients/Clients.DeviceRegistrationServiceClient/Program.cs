using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Models.Device;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Clients.DeviceRegistrationServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {




            var client = new WebClient();
            string url = "http://localhost:51033/DeviceRegistrationService.svc/DeviceRegistrationService/";

            string device2 = "{\"device\":{\"DeviceId\":\"D100\",\"Limits\":[{\"MaxValue\":100,\"Message\":\"normaa\",\"MinValue\":0,\"Type\":1}],\"MaxInputValue\":100,\"MinInputValue\":0}}";
            var obj = JObject.Parse(device2);
            Device device = JsonConvert.DeserializeObject<Device>(obj["device"].ToString());
            //Device device = JsonConvert.DeserializeObject<Device>(device2);

            var obj1 = JsonConvert.SerializeObject(device);
            obj1 = JToken.Parse(obj1).ToString();
            //Register Device -PUT
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            Console.WriteLine("Register Device:");
            var response = client.UploadString(url + "RegisterDevice", "PUT", device2);

            Console.WriteLine(response);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");


            //Read Device - POST
            response = client.DownloadString(url + "ReadDevice/D100");
            Console.WriteLine("Read Device:");
            Console.WriteLine(response);
            var device1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Device>(response);
            Console.WriteLine(device1.DeviceId);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");



            device2 = "{\"device\":{\"DeviceId\":\"D100\",\"Limits\":[{\"MaxValue\":5,\"Message\":\"normaa\",\"MinValue\":0,\"Type\":2}],\"MaxInputValue\":5,\"MinInputValue\":2}}";
            string json = JsonConvert.SerializeObject(device2);
            json = JToken.Parse(json).ToString();
            //Update Device - POST
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "UpdateDevice", "POST", json);
            Console.WriteLine("Update Device:");
            Console.WriteLine(response);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");


            //Read All Devices -POST
            response = client.DownloadString(url + "ReadAllDevices");
            Console.WriteLine("RedAll Devices:");
            Console.WriteLine(response);



            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            var deviceId = "{\"deviceId\":\"D100\"}";

            //DElete Device - DELETE
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "DeleteDevice", "DELETE", deviceId);
            Console.WriteLine("Delete Device:");
            Console.WriteLine(response);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");


            //Read All Devices -POST
            response = client.DownloadString(url + "ReadAllDevices");
            Console.WriteLine("RedAll Devices:");
            Console.WriteLine(response);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");



        }
        //static async Task RunAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:51033/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        Console.WriteLine("Get");
        //        HttpResponseMessage response = await client.GetAsync("/DeviceRegistrationService/ReadDevice/D103");
        //        if(response.IsSuccessStatusCode)
        //        {
        //            Device device=await response.Content.ReadAsAsync<Device>();
        //            Console.WriteLine(device.DeviceId);
        //            Console.WriteLine(device.MaxInputValue);
        //        }
        //    }
        //}
    }
}
