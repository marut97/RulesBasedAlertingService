using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Device;
using Models.Patient;
using Unity;

namespace Test.Unit.CustomDeviceRepository
{
    [TestClass]
    public class CustomDeviceRepositoryUnitTest
    {
        Contracts.CustomDeviceRepository.ICustomDeviceRepository customDeviceRepo;
        Contracts.DeviceRepository.IDeviceRepository devicerepo;
        Contracts.PatientRepository.IPatientRepository patientRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            devicerepo = new DataAccessLayer.DeviceRepositoryLib.DeviceRepository(true);
            patientRepo = new DataAccessLayer.PatientRepositoryLib.PatientRepository(true);
            customDeviceRepo = new DataAccessLayer.CustomDeviceRepoLib.CustomDeviceRepository(devicerepo,true);

            //UnityContainer _diContainer = new UnityContainer();
            //_diContainer.LoadConfiguration();
            //customDeviceRepo = _diContainer.Resolve<Contracts.CustomDeviceRepository.ICustomDeviceRepository>();

            InitializeDB();
        }

        

        private Device CreateDevice()
        {
            Device device = new Device
            {
                DeviceId = "SPO1",
                MinInputValue = 0,
                MaxInputValue = 100,
            };

            List<Limits> limits = new List<Limits>();
            limits.Add(new Limits { MinValue = 0, MaxValue = 50, Type = LimitType.Normal, Message = "Normal" });
            limits.Add(new Limits { MinValue = 50, MaxValue = 100, Type = LimitType.Warning, Message = "Warning" });

            device.Limits = limits;
            return device;

        }

        private void InitializeDB()
        {
            Device device = CreateDevice();
            devicerepo.RegisterNew(device);

            Patient patient = CreatePatient();
            patientRepo.RegisterNew(patient);
        }

        private void RegisterCustomDevice()
        {
            Device device = CreateDevice();
            customDeviceRepo.RegisterCustom(device,"100");
        }


        private void DeleteCustomDevice()
        {
            customDeviceRepo.DeleteCustom("SPO1","100");
        }


        private Patient CreatePatient()
        {
            Patient patient = new Patient
            {
                PatientId = "100",
                Name = "Philips",
                Phone = 1234567,
                Address = "Bangalore",
                EmergencyContact = 7654321,
                Email = "@phiips.com",
                BloodType = BloodType.ONegative
            };

            patient.PreviousAdmissions = new System.Collections.Generic.List<AdmissionHistory>();
            patient.PreviousAdmissions.Add(CreateAdmissionHistory());

            return patient;
        }

        private AdmissionHistory CreateAdmissionHistory()
        {
            AdmissionHistory history = new AdmissionHistory();
            history.Illness = "cold";
            history.Diagnosis = "... diagnosis";
            history.AdmissionDate = new DateTime(2019, 1, 1, 4, 0, 15).ToString();
            return history;
        }


        [TestCleanup]
        public void CleanDB()
        {
            devicerepo.Delete("SPO1");
            patientRepo.Delete("100");

        }

        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterCustom_Invoked_Then_true_Asserted()
        {
            Device device = CreateDevice();
            Assert.IsTrue(customDeviceRepo.RegisterCustom(device,"100"));
            DeleteCustomDevice();
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_RegisterCustom_Invoked_Then_false_Asserted()
        {

            Device device = CreateDevice();
            RegisterCustomDevice();
            Assert.IsFalse(customDeviceRepo.RegisterCustom(device,"100"));
            DeleteCustomDevice();
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_UpdateCustom_Invoked_Then_true_Asserted()
        {

            Device device = CreateDevice();
            RegisterCustomDevice();
            Assert.IsTrue(customDeviceRepo.UpdateCustom(device,"100"));
            DeleteCustomDevice();
        }


        //check test case
        [TestMethod]
        public void Given_Invalid_Arguments_When_UpdateCustom_Invoked_Then_false_Asserted()
        {
            Device device = CreateDevice();
            Assert.IsFalse(customDeviceRepo.UpdateCustom(device,"100"));
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_DeleteCustom_Invoked_with_DeviceID_And_PatientId_Then_true_Asserted()
        {
            RegisterCustomDevice();
            Assert.IsTrue(customDeviceRepo.DeleteCustom("SPO1","100"));
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_DeleteCustom_Invoked_with_DeviceID_And_PatientId_Then_false_Asserted()
        {
            Assert.IsFalse(customDeviceRepo.DeleteCustom("100", "SPO1"));
        }


        [TestMethod]
        public void Given_Valid_Arguments_When_DeleteCustom_Invoked_with_PatientId_Then_true_Asserted()
        {
            RegisterCustomDevice();
            Assert.IsTrue(customDeviceRepo.DeleteCustom("100"));
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_DeleteCustom_Invoked_PatientId_Then_false_Asserted()
        {
            Assert.IsFalse(customDeviceRepo.DeleteCustom("100"));
        }


        [TestMethod]
        public void Given_Valid_Arguments_When_Exists_Invoked_With_PatientId_and_DeviceId_Then_true_Asserted()
        {
            RegisterCustomDevice();
            Assert.IsTrue(customDeviceRepo.Exists("100","SPO1"));
            DeleteCustomDevice();
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_Exists_Invoked_With_PatientId_and_DeviceId_Then_false_Asserted()
        {
            Assert.IsFalse(customDeviceRepo.Exists("100", "SPO1"));
        }



        [TestMethod]
        public void Given_Valid_Arguments_When_Exists_Invoked_With_PatientId_Then_true_Asserted()
        {
            RegisterCustomDevice();
            Assert.IsTrue(customDeviceRepo.Exists("100"));
            DeleteCustomDevice();
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_Exists_Invoked_With_PatientId_Then_false_Asserted()
        {
            Assert.IsFalse(customDeviceRepo.Exists("100"));
        }

    }
}
