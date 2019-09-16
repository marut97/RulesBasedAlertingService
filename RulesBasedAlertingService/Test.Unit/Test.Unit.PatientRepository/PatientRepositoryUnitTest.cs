using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Patient;

namespace Test.Unit.PatientRepository
{
    [TestClass]
    public class PatientRepositoryUnitTest
    {
        DataAccessLayer.PatientRepositoryLib.PatientRepository patientRepo;


        [TestInitialize]
        public void TestInitialize()
        {
            patientRepo = new DataAccessLayer.PatientRepositoryLib.PatientRepository(true);
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



        private void RegisterPatient()
        {
            Patient patient = CreatePatient();
            patientRepo.RegisterNew(patient);

        }

        private void DeletePatient()
        {
            patientRepo.Delete("100");

        }


        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterNew_Invoked_Then_true_Asserted()
        {

            Patient patient = CreatePatient();
            Assert.IsTrue(patientRepo.RegisterNew(patient));
            DeletePatient();
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_RegisterNew_Invoked_Then_false_Asserted()
        {

            Patient patient = CreatePatient();
            patientRepo.RegisterNew(patient);
            Assert.IsFalse(patientRepo.RegisterNew(patient));
            DeletePatient();
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_true_Asserted()
        {

            Patient patient = CreatePatient();
            RegisterPatient();
            Assert.IsTrue(patientRepo.Update(patient));
            DeletePatient();
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_Update_Invoked_Then_false_Asserted()
        {
            Patient patient = CreatePatient();
            Assert.IsFalse(patientRepo.Update(patient));
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Delete_Invoked_Then_true_Asserted()
        {
            RegisterPatient();
            Assert.IsTrue(patientRepo.Delete("100"));
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_Delete_Invoked_Then_false_Asserted()
        {
            Assert.IsFalse(patientRepo.Delete("100"));
        }


        [TestMethod]
        public void Given_Valid_Arguments_When_Exists_Invoked_Then_true_Asserted()
        {
            RegisterPatient();
            Assert.IsTrue(patientRepo.Exists("100"));
            DeletePatient();
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_Exists_Invoked_Then_false_Asserted()
        {
            Assert.IsFalse(patientRepo.Exists("100"));
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Invoked_Then_Valid_Result_Asserted()
        {
            RegisterPatient();
            Assert.AreEqual("100", patientRepo.Read("100").PatientId);
            DeletePatient();
        }



        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Invoked_Then_null_Asserted()
        {
            Assert.IsNull(patientRepo.Read("100").PatientId);
        }



        [TestMethod]
        public void When_ReadAll_Invoked_Then_Valid_Result_Asserted()
        {
            RegisterPatient();
            Assert.AreEqual(1, patientRepo.ReadAll().Count);
            DeletePatient();
        }


        [TestMethod]
        public void Given_Valid_Arguments_When_AddAdmissionHistory_Invoked_Then_true_Asserted()
        {
            RegisterPatient();
            var history = CreateAdmissionHistory();
            Assert.IsTrue(patientRepo.AddAdmissionHistory("100", history));
            DeletePatient();
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_AddAdmissionHistory_Invoked_Then_false_Asserte()
        {

            var history = CreateAdmissionHistory();
            Assert.IsFalse(patientRepo.AddAdmissionHistory("100", history));
        }



    }
}
