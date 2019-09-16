using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.HospitalBed;
using Models.Patient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test.E2E.HospitalBedRegistration
{
    [TestClass]
    public class HospitalBedRegistrationE2ETest
    {
        private WebClient client;
        private string url;

        [TestInitialize]
        public void TestInitialize()
        {
            client = new WebClient();
            url = "http://localhost:51449/HospitalBedRegistrationService.svc/HospitalBedRegistrationService/";
        }


        private string CreateHospitalBed()
        {
            //id=PIC-2F-2A-201-5
            HospitalBed beds = new HospitalBed
            {
                BedNumber = 10,
                Campus = "PIC",
                Floor = "2F",
                RoomNumber = "201",
                Wing = "2A",
                Occupancy = ""
            };
            string json = JsonConvert.SerializeObject(beds);
            json = "{\"bed\":" + json + "}";


            return json;
        }

        private string CreateHospitalBeds()
        {
            //id=PIC-2F-2A-201-5
            HospitalBed beds = new HospitalBed
            {
                BedNumber = 10,
                Campus = "PIC",
                Floor = "2F",
                RoomNumber = "201",
                Wing = "2A"
            };
            string json = JsonConvert.SerializeObject(beds);
            json = "{\"beds\":" + json + "}";


            return json;
        }

        private void HospitalBedRegistration()
        {

            string json = CreateHospitalBeds();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(url + "RegisterBeds", "POST", json);

        }

        private void DeleteHospitalBeds()
        {
            string json = CreateHospitalBed();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "DeleteBeds", "DELETE", json);
        }


        private void PatientRegistration()
        {
            string url = "http://localhost:51625/PatientRegistrationService.svc/PatientRegistrationService/";
            Patient patient = new Patient
            {
                PatientId = "111",
                Address = "Address",
                BloodType = BloodType.ABNegative,
                Email = "email@email.com",
                EmergencyContact = 123123123,
                Name = "Mohan",
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
            string json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterPatient", "POST", json);


        }


        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterNew_Invoked_Then_True_Asserted()
        {
            string json = CreateHospitalBeds();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterBeds", "POST", json);
            Assert.AreEqual("{}", response);
            DeleteHospitalBeds();

        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_True_Asserted()
        {
            HospitalBedRegistration();
            PatientRegistration();
            HospitalBed beds = new HospitalBed
            {
                BedNumber = 10,
                Campus = "PIC",
                Floor = "2F",
                RoomNumber = "201",
                Wing = "2A",
                Occupancy = "111"

            };
            string json = JsonConvert.SerializeObject(beds);
            json = "{\"bed\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "UpdateBed", "PUT", json);
            Assert.AreEqual("{}", response);


            json = CreateHospitalBed();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "UpdateBed", "PUT", json);

            DeleteHospitalBeds();

            var patientId = "{\"patientId\":\"111\"}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString("http://localhost:51625/PatientRegistrationService.svc/PatientRegistrationService/" + "DeletePatient", "DELETE", patientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Delete_Invoked_Then_true_Asserted()
        {
            HospitalBedRegistration();
            string json = CreateHospitalBed();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "DeleteBeds", "DELETE", json);
            Assert.AreEqual("{}", response);
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Invoked_Then_Valid_Result_Asserted()
        {
            HospitalBedRegistration();
            var response = client.DownloadString(url + "ReadBed/PIC-2F-2A-201-5");
            var obj = JObject.Parse(response);
            var bed = JsonConvert.DeserializeObject<HospitalBed>(obj["ReadBedResult"].ToString());
            Assert.AreEqual(5, bed.BedNumber);
            DeleteHospitalBeds();

        }


        [TestMethod]
        public void Given_Valid_Argument_When_ReadAllBeds_Invoked_Then_Valid_Result_Asserted()
        {
            HospitalBedRegistration();
            var response = client.DownloadString(url + "ReadAllBeds");
            var obj = JObject.Parse(response);
            var bed = JsonConvert.DeserializeObject<List<HospitalBed>>(obj["ReadAllBedsResult"].ToString());
            Assert.AreEqual(10, bed.Count);
            DeleteHospitalBeds();
        }


        [TestMethod]
        public void Given_Valid_Argument_When_Read_Empty_Beds_In_Room_Invoked_Then_Valid_Result_Asserted()
        {
            HospitalBedRegistration();

            var response = client.DownloadString(url + "ReadEmptyBeds/PIC-2F-2A-201");
            var obj = JObject.Parse(response);
            var bed = JsonConvert.DeserializeObject<List<HospitalBed>>(obj["ReadEmptyBedsResult"].ToString());
            Assert.AreEqual(10, bed.Count);
            DeleteHospitalBeds();
        }

    }
}
