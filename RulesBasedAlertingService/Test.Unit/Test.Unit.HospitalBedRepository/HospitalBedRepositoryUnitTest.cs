using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DataAccessLayer.PatientRepositoryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.HospitalBed;
using Models.Patient;

namespace Test.Unit.HospitalBedRepository
{
    [TestClass]
    public class HospitalBedRepositoryUnitTest
    {
        private DataAccessLayer.HospitalBedRepoLib.HospitalBedRepository hospitalBedRepository;

        private HospitalBed CreateBed()
        {
            HospitalBed bed = new HospitalBed
            {
                BedNumber = 10,
                Campus = "PIC",
                Floor = "2",
                RoomNumber = "12",
                Wing = "2A"
            };
            return bed;
        }

        private Patient CreatePatient()
        {
            Patient patient = new Patient
            {
                Address = "abc",
                BloodType = BloodType.ABNegative,
                Email = "bla@bla.com",
                EmergencyContact = 123123123,
                Name = "PatientX",
                PatientId = "1234",
                Phone = 321321321,
                PreviousAdmissions = new List<AdmissionHistory>()
            };
            return patient;
        }

        private Patient CreatePatient1()
        {
            Patient patient = new Patient
            {
                Address = "abc",
                BloodType = BloodType.ABNegative,
                Email = "bla@bla.com",
                EmergencyContact = 123123123,
                Name = "PatientX",
                PatientId = "12345",
                Phone = 321321321,
                PreviousAdmissions = new List<AdmissionHistory>()
            };
            return patient;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            hospitalBedRepository = new DataAccessLayer.HospitalBedRepoLib.HospitalBedRepository(true);
        }
        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterNew_Invoked_Then_True_Asserted()
        {
            var bed = CreateBed();
            Assert.IsTrue(hospitalBedRepository.RegisterNew(bed));
            hospitalBedRepository.Delete(bed);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_True_Asserted()
        {
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            patientRepository.RegisterNew(CreatePatient1());
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            bed.Occupancy = "12345";
            Assert.IsTrue(hospitalBedRepository.Update(bed));
            bed.Occupancy = "";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
            patientRepository.Delete("12345");

        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Update_Invoked_Then_False_Asserted()
        {
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            patientRepository.RegisterNew(CreatePatient1());
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            bed.Occupancy = "123";
            Assert.IsFalse(hospitalBedRepository.Update(bed));
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
            patientRepository.Delete("12345");

        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Delete_Invoked_Then_true_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            Assert.IsTrue(hospitalBedRepository.Delete(bed));
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Delete_Invoked_Then_False_Asserted()
        {
            var bed = CreateBed();
            Assert.IsFalse(hospitalBedRepository.Delete(bed));
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Exists_Invoked_Then_true_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            Assert.IsTrue(hospitalBedRepository.Exists(bed));
            hospitalBedRepository.Delete(bed);
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Exists_Invoked_Then_False_Asserted()
        {
            var bed = CreateBed();
            Assert.IsFalse(hospitalBedRepository.Exists(bed));
        }

        
        [TestMethod]
        public void Given_Valid_Argument_When_Read_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual("1234", hospitalBedRepository.Read(bed).Occupancy);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Occupied_Beds_In_Room_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual("1234", hospitalBedRepository.ReadOccupiedBedsInRoom(bed).First().Occupancy);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Occupied_Beds_In_Room_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.RoomNumber = "abc";
            bed.Occupancy = "";
            Assert.IsFalse(hospitalBedRepository.ReadOccupiedBedsInRoom(bed).Any());
            bed.RoomNumber = "12";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Empty_Beds_In_Room_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual(9, hospitalBedRepository.ReadEmptyBedsInRoom(bed).Count);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Empty_Beds_In_Room_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            bed.BedNumber = 1;
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.RoomNumber = "abc";
            bed.Occupancy = "";
            Assert.IsFalse(hospitalBedRepository.ReadEmptyBedsInRoom(bed).Any());
            bed.RoomNumber = "12";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Occupied_Beds_In_Wing_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual("1234", hospitalBedRepository.ReadOccupiedBedsInWing(bed).First().Occupancy);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Occupied_Beds_In_Wing_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Wing = "abc";
            bed.Occupancy = "";
            Assert.IsFalse(hospitalBedRepository.ReadOccupiedBedsInWing(bed).Any());
            bed.Wing = "2A";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Empty_Beds_In_Wing_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual(9, hospitalBedRepository.ReadEmptyBedsInWing(bed).Count);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Empty_Beds_In_Wing_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            bed.BedNumber = 1;
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Wing = "abc";
            bed.Occupancy = "";
            Assert.IsFalse(hospitalBedRepository.ReadEmptyBedsInWing(bed).Any());
            bed.Wing = "2A";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Occupied_Beds_In_Floor_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual("1234", hospitalBedRepository.ReadOccupiedBedsInFloor(bed).First().Occupancy);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Occupied_Beds_In_Floor_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Floor = "abc";
            bed.Occupancy = "";
            Assert.IsFalse(hospitalBedRepository.ReadOccupiedBedsInFloor(bed).Any());
            bed.Floor = "2";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Empty_Beds_In_Floor_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual(9, hospitalBedRepository.ReadEmptyBedsInFloor(bed).Count);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Empty_Beds_In_Floor_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            bed.BedNumber = 1;
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Floor = "abc";
            bed.Occupancy = "";
            Assert.IsFalse(hospitalBedRepository.ReadEmptyBedsInFloor(bed).Any());
            bed.Floor = "2";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }
        [TestMethod]
        public void Given_Valid_Argument_When_Read_Occupied_Beds_In_Campus_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual("1234", hospitalBedRepository.ReadOccupiedBedsInCampus(bed).First().Occupancy);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Occupied_Beds_In_Campus_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Campus = "abc";
            bed.Occupancy = "";
            Assert.IsFalse(hospitalBedRepository.ReadOccupiedBedsInCampus(bed).Any());
            bed.Campus = "PIC";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Empty_Beds_In_Campus_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Occupancy = "";
            Assert.AreEqual(9, hospitalBedRepository.ReadEmptyBedsInCampus(bed).Count);
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Empty_Beds_In_Campus_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            bed.BedNumber = 1;
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            bed.Campus = "abc";
            bed.Occupancy = "";
            Assert.IsFalse(hospitalBedRepository.ReadEmptyBedsInCampus(bed).Any());
            bed.Campus = "PIC";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Patients_Bed_Invoked_Then_Valid_Result_Asserted()
        {
            var bed = CreateBed();
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            Assert.AreEqual(bed.Occupancy, hospitalBedRepository.ReadPatientsBed("1234").Occupancy);
            bed.Occupancy = "";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }
        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Patients_Bed_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            bed.BedNumber = 1;
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            Assert.IsNull(hospitalBedRepository.ReadPatientsBed("123").Occupancy);
            bed.Occupancy = "";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_All_Invoked_Then_Invalid_Result_Asserted()
        {
            var bed = CreateBed();
            bed.BedNumber = 1;
            hospitalBedRepository.RegisterNew(bed);
            PatientRepository patientRepository = new PatientRepository(true);
            patientRepository.RegisterNew(CreatePatient());
            bed.Occupancy = "1234";
            hospitalBedRepository.Update(bed);
            Assert.AreEqual(1,hospitalBedRepository.ReadAll().Count);
            Assert.AreEqual("1234",hospitalBedRepository.ReadAll().First().Occupancy);
            bed.Occupancy = "";
            hospitalBedRepository.Update(bed);
            hospitalBedRepository.Delete(bed);
            patientRepository.Delete("1234");
        }
    }
}
