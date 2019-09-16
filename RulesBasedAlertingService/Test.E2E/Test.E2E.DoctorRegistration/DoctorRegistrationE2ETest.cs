using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Doctor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test.E2E.DoctorRegistration
{
    [TestClass]
    public class DoctorRegistrationE2ETest
    {
        private WebClient client;
        private string url;

        [TestInitialize]
        public void TestInitialize()
        {
            client = new WebClient();
            url = "http://localhost:51903/DoctorRegistrationService.svc/DoctorRegistrationService/";
        }

        private Doctor CreateDoctor(string doctorId)
        {
            Doctor doctor = new Doctor
            {
                DoctorId = doctorId,
                Address = "Address",
                Email = "email@email.com",
                Name = "Patient" + doctorId,
                Phone = 321321321,
                Department = HospitalDepartment.Cardiology,
                Patients = new List<string>(),
                Status = DoctorStatus.Active
            };

            return doctor;

        }

        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterNew_Invoked_Then_true_Asserted()
        {
            Doctor doctor = CreateDoctor("101");
            string json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDoctor", "POST", json);
            Assert.AreEqual("{}", response);
            var doctorid = "{\"doctorId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDoctor", "DELETE", doctorid);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_true_Asserted()
        {
            Doctor doctor = CreateDoctor("101");
            string json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDoctor", "POST", json);
            doctor.Address = "New House";
            doctor.Phone = 5138008;
            json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "UpdateDoctor", "PUT", json);
            Assert.AreEqual("{}", response);
            var patientId = "{\"doctorId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDoctor", "DELETE", patientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Delete_Invoked_Then_true_Asserted()
        {
            Doctor doctor = CreateDoctor("101");
            string json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDoctor", "POST", json);
            Assert.AreEqual("{}", response);
            var patientId = "{\"doctorId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "DeleteDoctor", "DELETE", patientId);
            Assert.AreEqual("{}", response);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Read_Invoked_Then_true_Asserted()
        {
            Doctor doctor = CreateDoctor("101");
            string json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDoctor", "POST", json);
            response = client.DownloadString(url + "ReadDoctor/101");
            var obj = JObject.Parse(response);
            var patient1 = JsonConvert.DeserializeObject<Doctor>(obj["ReadDoctorResult"].ToString());
            Assert.AreEqual("101", patient1.DoctorId);
            var patientId = "{\"doctorId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDoctor", "DELETE", patientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_ReadAll_Invoked_Then_true_Asserted()
        {
            Doctor doctor = CreateDoctor("101");
            string json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDoctor", "POST", json);

            doctor = CreateDoctor("202");
            json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterDoctor", "POST", json);

            doctor = CreateDoctor("303");
            json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterDoctor", "POST", json);

            response = client.DownloadString(url + "ReadAllDoctors");
            var obj = JObject.Parse(response);
            var device1 = JsonConvert.DeserializeObject<List<Doctor>>(obj["ReadAllDoctorsResult"].ToString());
            Assert.AreEqual(3, device1.Count);

            var deviceId = "{\"doctorId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDoctor", "DELETE", deviceId);

            deviceId = "{\"doctorId\":\"202\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDoctor", "DELETE", deviceId);

            deviceId = "{\"doctorId\":\"303\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeleteDoctor", "DELETE", deviceId);
        }
    }
}
