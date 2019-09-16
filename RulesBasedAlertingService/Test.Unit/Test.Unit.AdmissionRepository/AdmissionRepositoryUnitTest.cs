using System;
using System.Collections.Generic;
using Contracts.CustomDeviceRepository;
using DataAccessLayer.DeviceRepositoryLib;
using DataAccessLayer.HospitalBedRepoLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.AdmissionRepositoryLib;
using DataAccessLayer.CustomDeviceRepoLib;
using Models.Device;

namespace Test.Unit.AdmissionRepository
{
    [TestClass]
    public class AdmissionRepositoryUnitTest
    {
        private Contracts.AdmissionRepository.IAdmissionRepository _admissionRepo;
        Contracts.PatientRepository.IPatientRepository patientRepo;
        Contracts.DeviceRepository.IDeviceRepository deviceRepo;
        Contracts.DoctorRepository.IDoctorRepository doctorRepo;
        Contracts.HospitalBedRepository.IHospitalBedRepository bedRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            deviceRepo = new DataAccessLayer.DeviceRepositoryLib.DeviceRepository(true);
            patientRepo = new DataAccessLayer.PatientRepositoryLib.PatientRepository(true);
            doctorRepo = new DataAccessLayer.DoctorRepositoryLib.DoctorRepository(true);
            bedRepo = new DataAccessLayer.HospitalBedRepoLib.HospitalBedRepository(true);

            AddDevicesToDB();
            AddPatientsToDB();
            AddDoctorToDB();
            AddHospitalBedToDB();

            _admissionRepo = new DataAccessLayer.AdmissionRepositoryLib.AdmissionRepository(patientRepo, new HospitalBedRepository(true), deviceRepo, new CustomDeviceRepository(new DeviceRepository(true), true), true);

        }


        private void AddDevicesToDB()
        {
            Models.Device.Device device = CreateDevice();
            deviceRepo.RegisterNew(device);
            device.DeviceId = "D102";
            deviceRepo.RegisterNew(device);
            device.DeviceId = "D103";
            deviceRepo.RegisterNew(device);
        }



        [TestCleanup]
        public void DeleteAdmissionFromDB()
        {
            deviceRepo.Delete("D101");
            deviceRepo.Delete("D102");
            deviceRepo.Delete("D103");
            patientRepo.Delete("P100");
            patientRepo.Delete("P102");
            doctorRepo.Delete("DR100");
            var beds = bedRepo.ReadOccupiedBedsInRoom(CreateHospitalBed());
            foreach (var bed in beds)
            {
                bed.Occupancy = "";
                bedRepo.Update(bed);
            }
            bedRepo.Delete(CreateHospitalBed());
        }

        private void AddPatientsToDB()
        {
            Models.Patient.Patient patient = new Models.Patient.Patient
            {
                PatientId = "P100",
                Name = "Philips",
                Address = "Bangalore",
                Email = "abc.gmail.com",
                BloodType = Models.Patient.BloodType.ABNegative,
                EmergencyContact = 1234,
                Phone = 2345
            };
            patient.PreviousAdmissions = new System.Collections.Generic.List<Models.Patient.AdmissionHistory>();

            patientRepo.RegisterNew(patient);
            patient.PatientId = "P102";
            patientRepo.RegisterNew(patient);

        }


        private void AddDoctorToDB()
        {
            Models.Doctor.Doctor doctor = new Models.Doctor.Doctor
            {
                DoctorId = "DR100",
                Name = "Dr abc",
                Address = "Bangalore",
                Department = Models.Doctor.HospitalDepartment.Cardiology,
                Email = "abc@gmail.com",
                Phone = 12345,
                Status = Models.Doctor.DoctorStatus.Active
            };
            doctor.Patients = new System.Collections.Generic.List<string>();
            doctorRepo.RegisterNew(doctor);
        }


        private Models.HospitalBed.HospitalBed CreateHospitalBed()
        {
            Models.HospitalBed.HospitalBed hospitalBed = new Models.HospitalBed.HospitalBed
            {
                Campus = "PIc",
                Floor = "Ground",
                Wing = "A",
                RoomNumber = "1",
                BedNumber = 5,
                Occupancy = "",
            };

            return hospitalBed;
        }


        private void AddHospitalBedToDB()
        {

            bedRepo.RegisterNew(CreateHospitalBed());

        }





        private Models.Device.Device CreateDevice()
        {
            Models.Device.Device device = new Models.Device.Device
            {
                DeviceId = "D101",
                MaxInputValue = 100,
                MinInputValue = 0,

            };

            device.Limits = new System.Collections.Generic.List<Models.Device.Limits>();
            device.Limits.Add(new Models.Device.Limits { MinValue = 0, MaxValue = 100, Type = Models.Device.LimitType.Normal, Message = "normaa" });
            return device;
        }


        private Models.PatientAdmission.PatientAdmission CreatePatientAdmission()
        {
            Models.PatientAdmission.PatientAdmission patientAdmission = new Models.PatientAdmission.PatientAdmission
            {
                PatientId = "P100",
                DoctorId = "DR100",
                Illness = "cold",
                Diagnosis = "u r ok",
                AdmissionTime = DateTime.Now.ToString(),
                MuteAlert = false,
                Bed = new Models.HospitalBed.HospitalBed
                {
                    Campus = "PIc",
                    Floor = "Ground",
                    Wing = "A",
                    RoomNumber = "1",
                    BedNumber = 5,
                    Occupancy = "P100",

                }
            };

            Device device = CreateDevice();

            patientAdmission.Devices = new List<Device>();
            patientAdmission.Devices.Add(device);
            device = new Models.Device.Device();
            device.Limits = new System.Collections.Generic.List<Models.Device.Limits>();
            device.DeviceId = "D102";
            patientAdmission.Devices.Add(device);

            return patientAdmission;
        }



        private void AdmitPatient()
        {
            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            _admissionRepo.Admit(admission);

        }

        private void DischargePatient()
        {
            _admissionRepo.Discharge("P100");

        }




        [TestMethod]
        public void Given_Valid_Arguments_When_Admit_Invoked_Then_true_Asserted()
        {

            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            Assert.IsTrue(_admissionRepo.Admit(admission));
            DischargePatient();
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_Admit_Invoked_Then_False_Asserted()
        {

            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            AdmitPatient();
            Assert.IsFalse(_admissionRepo.Admit(admission));
            DischargePatient();
        }


        [TestMethod]
        public void Given_Invalid_PatientId_Arguments_When_Admit_Invoked_Then_false_Asserted()
        {
            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            admission.PatientId = "InvalidP101";
            Assert.IsFalse(_admissionRepo.Admit(admission));
        }

        [TestMethod]
        public void Given_Invalid_DoctorId_Arguments_When_Admit_Invoked_Then_false_Asserted()
        {
            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            admission.DoctorId = "InvalidD101";
            Assert.IsFalse(_admissionRepo.Admit(admission));
        }

        [TestMethod]
        public void Given_Invalid_DeviceId_Arguments_When_Admit_Invoked_Then_false_Asserted()
        {
            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            Models.Device.Device device = new Models.Device.Device();
            device.DeviceId = "InvalidD101";
            device.Limits = new System.Collections.Generic.List<Models.Device.Limits>();
            admission.Devices.Add(device);
            Assert.IsFalse(_admissionRepo.Admit(admission));
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Discharge_Invoked_Then_true_Asserted()
        {

            AdmitPatient();
            Assert.IsTrue(_admissionRepo.Discharge("P100"));
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_Discharge_Invoked_Then_false_Asserted()
        {
            Assert.IsFalse(_admissionRepo.Discharge("P100"));
        }


        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_true_Asserted()
        {

            AdmitPatient();
            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            Assert.IsTrue(_admissionRepo.Update(admission));
            DischargePatient();
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_Update_Invoked_Then_false_Asserted()
        {
            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            Assert.IsFalse(_admissionRepo.Update(admission));
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Invoked_Then_Valid_Result_Asserted()
        {

            AdmitPatient();
            Assert.AreEqual("P100", _admissionRepo.Read("P100").PatientId);
            DischargePatient();
        }


        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Invoked_Then_null_Asserted()
        {
            Assert.IsNull(_admissionRepo.Read("P100").PatientId);
        }

        [TestMethod]
        public void When_ReadAll_Invoked_Then_Valid_Result_Asserted()
        {
            AdmitPatient();
            Models.PatientAdmission.PatientAdmission admission = CreatePatientAdmission();
            admission.PatientId = "P102";
            _admissionRepo.Admit(admission);
            Assert.AreEqual(2, _admissionRepo.ReadAll().Count);
            _admissionRepo.Discharge("P102");
            DischargePatient();
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Exists_Invoked_Then_true_Asserted()
        {

            AdmitPatient();
            Assert.IsTrue(_admissionRepo.Exists("P100"));
            DischargePatient();
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_Exists_Invoked_Then_false_Asserted()
        {
            Assert.IsFalse(_admissionRepo.Exists("P100"));
        }


        [TestMethod]
        public void Given_Valid_Arguments_When_AddDevice_Invoked_Then_true_Asserted()
        {

            AdmitPatient();
            Models.Device.Device device = CreateDevice();
            device.DeviceId = "D103";
            Assert.IsTrue(_admissionRepo.AddDevice("P100", device));
            DischargePatient();
        }



        [TestMethod]
        public void Given_InvalidPatientId_Arguments_When_AddDevice_Invoked_Then_false_Asserted()
        {
            Models.Device.Device device = CreateDevice();
            device.DeviceId = "D103";
            Assert.IsFalse(_admissionRepo.AddDevice("InvalidP100", device));
        }

        [TestMethod]
        public void Given_InvalidDevicetId_Arguments_When_AddDevice_Invoked_Then_false_Asserted()
        {
            AdmitPatient();
            Models.Device.Device device = CreateDevice();
            device.DeviceId = "DInvalid";
            Assert.IsFalse(_admissionRepo.AddDevice("P100", device));
            DischargePatient();
        }


        [TestMethod]
        public void Given_Existing_DevicetId_Arguments_When_AddDevice_Invoked_Then_false_Asserted()
        {
            Models.Device.Device device = CreateDevice();
            device.DeviceId = "D102";
            Assert.IsFalse(_admissionRepo.AddDevice("InvalidP100", device));

        }


        [TestMethod]
        public void Given_Valid_Arguments_When_RemoveDevice_Invoked_Then_true_Asserted()
        {
            AdmitPatient();
            Assert.IsTrue(_admissionRepo.RemoveDevice("P100", "D102"));
            DischargePatient();
        }




        [TestMethod]
        public void Given_InvalidPatientId_Arguments_When_RemoveDevice_Invoked_Then_false_Asserted()
        {
            Assert.IsFalse(_admissionRepo.RemoveDevice("InvalidP100", "D102"));
        }

        [TestMethod]
        public void Given_InvalidDevicetId_Arguments_When_RemoveDevice_Invoked_Then_false_Asserted()
        {
            AdmitPatient();
            Assert.IsFalse(_admissionRepo.RemoveDevice("P100", "InvalidD103"));
            DischargePatient();
        }


    }
}
