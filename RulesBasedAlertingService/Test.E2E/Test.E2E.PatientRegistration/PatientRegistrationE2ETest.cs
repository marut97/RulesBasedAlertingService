using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Patient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test.E2E.PatientRegistration
{
    [TestClass]
    public class PatientRegistrationE2ETest
    {
        private WebClient client;
        private string url;

        [TestInitialize]
        public void TestInitialize()
        {
            client = new WebClient();
            url = "http://localhost:51625/PatientRegistrationService.svc/PatientRegistrationService/";
        }

        private Patient CreatePatient(string patientId)
        {
            Patient patient = new Patient
            {
                PatientId = patientId,
                Address = "Address",
                BloodType = BloodType.ABNegative,
                Email = "email@email.com",
                EmergencyContact = 123123123,
                Name = "Patient"+patientId,
                Phone = 321321321,
                PreviousAdmissions = new List<AdmissionHistory>()
            };
            patient.PreviousAdmissions.Add(new AdmissionHistory
            {
                AdmissionDate = new DateTime(1999, 1, 1).ToString(),
                Diagnosis = "Very Serious",
                Illness = "Dengue"
            });
            patient.PreviousAdmissions.Add(new AdmissionHistory
            {
                AdmissionDate = new DateTime(2005, 1, 1).ToString(),
                Diagnosis = "Not so serious. Will Live",
                Illness = "Malaria"
            });
            return patient;

        }

        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterNew_Invoked_Then_true_Asserted()
        {
            Patient patient = CreatePatient("101");
            string json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterPatient", "POST", json);
            Assert.AreEqual("{}", response);
            var patientId = "{\"patientId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeletePatient", "DELETE", patientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_true_Asserted()
        {
            Patient patient = CreatePatient("101");
            string json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterPatient", "POST", json);
            patient.Address = "New House";
            patient.Phone = 5138008;
            json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "UpdatePatient", "PUT", json);
            Assert.AreEqual("{}", response);
            var patientId = "{\"patientId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeletePatient", "DELETE", patientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Delete_Invoked_Then_true_Asserted()
        {
            Patient patient = CreatePatient("101");
            string json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterPatient", "POST", json);
            Assert.AreEqual("{}", response);
            var patientId = "{\"patientId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "DeletePatient", "DELETE", patientId);
            Assert.AreEqual("{}", response);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Read_Invoked_Then_true_Asserted()
        {
            Patient patient = CreatePatient("101");
            string json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterPatient", "POST", json);
            response = client.DownloadString(url + "ReadPatient/101");
            var obj = JObject.Parse(response);
            var patient1 = JsonConvert.DeserializeObject<Patient>(obj["ReadPatientResult"].ToString());
            Assert.AreEqual("101", patient1.PatientId);
            var patientId = "{\"patientId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeletePatient", "DELETE", patientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_ReadAll_Invoked_Then_true_Asserted()
        {
            Patient patient = CreatePatient("101");
            string json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterPatient", "POST", json);

            patient = CreatePatient("202");
            json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterPatient", "POST", json);

            patient = CreatePatient("303");
            json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterPatient", "POST", json);

            response = client.DownloadString(url + "ReadAllPatients");
            var obj = JObject.Parse(response);
            var device1 = JsonConvert.DeserializeObject<List<Patient>>(obj["ReadAllPatientsResult"].ToString());
            Assert.AreEqual(3, device1.Count);

            var deviceId = "{\"patientId\":\"101\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeletePatient", "DELETE", deviceId);

            deviceId = "{\"patientId\":\"202\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeletePatient", "DELETE", deviceId);

            deviceId = "{\"patientId\":\"303\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "DeletePatient", "DELETE", deviceId);
        }
    }
}
