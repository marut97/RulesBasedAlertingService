using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Device;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test.E2E.DeviceRegistration
{
    [TestClass]
    public class DeviceRegistrationE2ETest
    {
        private WebClient client;
        private string url;

        [TestInitialize]
        public void TestInitialize()
        {
            client = new WebClient();
            url = "http://localhost:51033/DeviceRegistrationService.svc/DeviceRegistrationService/";
        }

        private Device CreateDevice(string deviceId)
        {
            Device device = new Device
            {
                DeviceId = deviceId,
                MinInputValue = 0,
                MaxInputValue = 100,
            };

            List<Limits> limits = new List<Limits>();
            limits.Add(new Limits { MinValue = 0, MaxValue = 25, Type = LimitType.Critical, Message = "Critical" });
            limits.Add(new Limits { MinValue = 25, MaxValue = 50, Type = LimitType.Normal, Message = "Normal" });
            limits.Add(new Limits { MinValue = 50, MaxValue = 100, Type = LimitType.Warning, Message = "Warning" });

            device.Limits = limits;
            return device;

        }

        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterNew_Invoked_Then_true_Asserted()
        {
            Device device = CreateDevice("Temperature");
            string json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDevice", "POST", json);
            Assert.AreEqual("{}",response);
            var deviceId = "{\"deviceId\":\"Temperature\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDevice", "DELETE", deviceId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_true_Asserted()
        {
            Device device = CreateDevice("Temperature");
            string json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDevice", "POST", json);
            device.MaxInputValue = 75;
            device.Limits[2].MaxValue = 75;
            json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "UpdateDevice", "PUT", json);
            Assert.AreEqual("{}", response);
            var deviceId = "{\"deviceId\":\"Temperature\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDevice", "DELETE", deviceId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Delete_Invoked_Then_true_Asserted()
        {
            Device device = CreateDevice("Temperature");
            string json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDevice", "POST", json);
            Assert.AreEqual("{}", response);
            var deviceId = "{\"deviceId\":\"Temperature\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "DeleteDevice", "DELETE", deviceId);
            Assert.AreEqual("{}", response);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Read_Invoked_Then_true_Asserted()
        {
            Device device = CreateDevice("Temperature");
            string json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDevice", "POST", json);
            response = client.DownloadString(url + "ReadDevice/Temperature");
            var device1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Device>(response);
            Assert.AreEqual("Temperature",device1.DeviceId);
            var deviceId = "{\"deviceId\":\"Temperature\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDevice", "DELETE", deviceId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_ReadAll_Invoked_Then_true_Asserted()
        {
            Device device = CreateDevice("Temperature");
            string json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDevice", "POST", json);

            device = CreateDevice("HeartRate");
            json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterDevice", "POST", json);

            device = CreateDevice("SPO2");
            json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterDevice", "POST", json);

            response = client.DownloadString(url + "ReadAllDevices");
            var obj = JObject.Parse(response);
            var device1 = JsonConvert.DeserializeObject<List<Device>>(obj["ReadAllDevicesResult"].ToString());
            Assert.AreEqual(3, device1.Count);

            var deviceId = "{\"deviceId\":\"Temperature\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDevice", "DELETE", deviceId);

            deviceId = "{\"deviceId\":\"HeartRate\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDevice", "DELETE", deviceId);

            deviceId = "{\"deviceId\":\"SPO2\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDevice", "DELETE", deviceId);
        }
    }
}
