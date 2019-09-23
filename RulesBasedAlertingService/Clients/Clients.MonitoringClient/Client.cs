using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Threading;
using Clients.MonitoringClient.DoctorMonitoringService;
using Clients.MonitoringClient.NurseMonitoringService;
using Clients.MonitoringClient.VitalsDataService;
using Models.Device;
using Models.Doctor;
using Models.HospitalBed;
using Models.Patient;
using Models.PatientAdmission;
using Newtonsoft.Json;

namespace Clients.MonitoringClient
{
    public class Client
    {
        private static WebClient client = new WebClient();
        static void Main(string[] args)
        {
            DeviceRegistration();
            PatientRegistration();
            DoctorRegistration();
            HospitalBedRegistration();
            NewPatientAdmission();

            var doc1Client = new DoctorMonitoringServiceClient(new InstanceContext(new DoctorMonitoringCallback()));
            doc1Client.SubscribeToVitals("111", "100");
            doc1Client.SubscribeToPatientAlerts("100");
            
            var doc2Client = new DoctorMonitoringServiceClient(new InstanceContext(new DoctorMonitoringCallback()));
            doc2Client.SubscribeToVitals("222", "200");
            doc2Client.SubscribeToPatientAlerts("200");

            var nurse1Client = new NurseMonitoringServiceClient(new InstanceContext(new NurseMonitoringCallback()));
            nurse1Client.SubscribeToVitals("111", "PIC-2F-2A");
            nurse1Client.SubscribeToPatientAlerts("PIC-2F-2A");

            var nurse2Client = new NurseMonitoringServiceClient(new InstanceContext(new NurseMonitoringCallback()));
            nurse2Client.SubscribeToVitals("222", "PIC-2F");
            nurse2Client.SubscribeToPatientAlerts("PIC-2F");

            VitalsDataServiceClient client = new VitalsDataServiceClient();
            VitalsDataService.PatientVitals vitals = new VitalsDataService.PatientVitals();
            for (int i = 0; i < 20; i++)
            {
                vitals.PatientId = "111";
                vitals.Vitals = new VitalsDataService.Vitals[] { new VitalsDataService.Vitals { DeviceId = "Temperature", Value = 85 } };
                client.WriteVitals(vitals);
                Thread.Sleep(2000);
                vitals.PatientId = "222";
                vitals.Vitals = new VitalsDataService.Vitals[] { new VitalsDataService.Vitals { DeviceId = "Temperature", Value = 20 }, new VitalsDataService.Vitals { DeviceId = "SPO2", Value = 78 } };
                Thread.Sleep(2000);
                client.WriteVitals(vitals);
            }
        }

        private static void NewPatientAdmission()
        {
            string url = "http://localhost:51721/PatientAdmissionService.svc/PatientAdmissionService/";
            PatientAdmission admission = new PatientAdmission
            {
                PatientId = "111",
                AdmissionTime = DateTime.Today.ToString(),
                Bed = new HospitalBed
                {
                    BedNumber = 5,
                    Campus = "PIC",
                    Floor = "2F",
                    RoomNumber = "201",
                    Wing = "2A",
                    Occupancy = "111"
                },
                Devices = new List<Device>(),
                Diagnosis = "Fever",
                DoctorId = "100",
                Illness = "Fever",
                MuteAlert = false
            };
            admission.Devices.Add(new Device { DeviceId = "Temperature" });
            string json = JsonConvert.SerializeObject(admission);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "AdmitPatient", "POST", json);

            admission.PatientId = "222";
            admission.Bed.BedNumber = 3;
            admission.Bed.Occupancy = "222";
            admission.DoctorId = "200";
            admission.Devices.Add(new Device
            {
                DeviceId = "SPO2",
                Limits = new List<Limits>
                {
                    new Limits
                    {
                        MaxValue = 75,
                        Message = "Normal, dont freak out",
                        MinValue = 0,
                        Type = LimitType.Normal
                    },
                    new Limits
                    {
                        MaxValue = 100,
                        Message = "Critical",
                        MinValue = 75,
                        Type = LimitType.Critical
                    }
                },
                MaxInputValue = 100,
                MinInputValue = 0
            });
            //json = JObject.Parse(admission);
            json = JsonConvert.SerializeObject(admission);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "AdmitPatient", "POST", json);
        }

        private static void HospitalBedRegistration()
        {
            string url = "http://localhost:51449/HospitalBedRegistrationService.svc/HospitalBedRegistrationService/";
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
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterBeds", "POST", json);
        }

        private static void DoctorRegistration()
        {
            string url = "http://localhost:51903/DoctorRegistrationService.svc/DoctorRegistrationService/";
            Doctor doctor = new Doctor
            {
                DoctorId = "100",
                Address = "Address",
                Email = "email@email.com",
                Name = "Alex Karev",
                Phone = 321321321,
                Department = HospitalDepartment.Cardiology,
                Patients = new List<string>(),
                Status = DoctorStatus.Active
            };
            string json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDoctor", "POST", json);

            doctor.DoctorId = "200";
            doctor.Name = "Meredith Grey";
            json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterDoctor", "POST", json);

            doctor.DoctorId = "300";
            doctor.Name = "Christina Yang";
            json = JsonConvert.SerializeObject(doctor);
            json = "{\"doctor\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterDoctor", "POST", json);
        }

        private static void PatientRegistration()
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

            patient.PatientId = "222";
            patient.Name = "Chethan";
            json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterPatient", "POST", json);

            patient.PatientId = "333";
            patient.Name = "Marut";
            json = JsonConvert.SerializeObject(patient);
            json = "{\"patient\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterPatient", "POST", json);
        }

        private static void DeviceRegistration()
        {
            string url = "http://localhost:51033/DeviceRegistrationService.svc/DeviceRegistrationService/";

            Device device = new Device
            {
                DeviceId = "Temperature",
                Limits = new List<Limits>(),
                MaxInputValue = 100,
                MinInputValue = 0
            };
            device.Limits.Add(new Limits
            {
                MaxValue = 35,
                MinValue = 0,
                Message = "WarningMessage",
                Type = LimitType.Warning
            });
            device.Limits.Add(new Limits
            {
                MaxValue = 70,
                MinValue = 35,
                Message = "NormalMessage",
                Type = LimitType.Normal
            });
            device.Limits.Add(new Limits
            {
                MaxValue = 100,
                MinValue = 70,
                Message = "CriticalMessage",
                Type = LimitType.Critical
            });
            string json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = client.UploadString(url + "RegisterDevice", "POST", json);

            device.DeviceId = "HeartRate";
            device.Limits.Add(new Limits
            {
                MaxValue = 80,
                MinValue = 0,
                Message = "WarningMessage",
                Type = LimitType.Warning
            });
            device.Limits.Add(new Limits
            {
                MaxValue = 90,
                MinValue = 80,
                Message = "NormalMessage",
                Type = LimitType.Normal
            });
            device.Limits.Add(new Limits
            {
                MaxValue = 100,
                MinValue = 90,
                Message = "CriticalMessage",
                Type = LimitType.Critical
            });
            json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterDevice", "POST", json);
            device.DeviceId = "SPO2";
            device.Limits.Add(new Limits
            {
                MaxValue = 20,
                MinValue = 0,
                Message = "WarningMessage",
                Type = LimitType.Warning
            });
            device.Limits.Add(new Limits
            {
                MaxValue = 40,
                MinValue = 20,
                Message = "NormalMessage",
                Type = LimitType.Normal
            });
            device.Limits.Add(new Limits
            {
                MaxValue = 100,
                MinValue = 40,
                Message = "CriticalMessage",
                Type = LimitType.Critical
            });
            json = JsonConvert.SerializeObject(device);
            json = "{\"device\":" + json + "}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            response = client.UploadString(url + "RegisterDevice", "POST", json);
        }
    }
}
