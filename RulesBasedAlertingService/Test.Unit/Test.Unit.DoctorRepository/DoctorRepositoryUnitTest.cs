using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Doctor;

namespace Test.Unit.DoctorRepository
{
    [TestClass]
    public class DoctorRepositoryUnitTest
    {
        DataAccessLayer.DoctorRepositoryLib.DoctorRepository doctorRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            doctorRepository = new DataAccessLayer.DoctorRepositoryLib.DoctorRepository(true);
        }

        private Doctor CreateDoctor()
        {
            Doctor doctor = new Doctor
            {
                Address = "address",
                Department = HospitalDepartment.Cardiology,
                DoctorId = "Doc100",
                Email = "abc@abc.com",
                Name = "doc",
                Phone = 321321321,
                Status = DoctorStatus.Active,
                Patients = new List<string>()
            };
            return doctor;
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterNew_Invoked_Then_True_Asserted()
        {
            var doctor = CreateDoctor();
            Assert.IsTrue(doctorRepository.RegisterNew(doctor));
            doctorRepository.Delete("Doc100");
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_RegisterNew_Invoked_Then_false_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.IsFalse(doctorRepository.RegisterNew(doctor));
            doctorRepository.Delete("Doc100");
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_true_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            doctor.Name = "Doctor";
            Assert.IsTrue(doctorRepository.Update(doctor));
            doctorRepository.Delete("Doc100");
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Update_Invoked_Then_False_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            doctor.DoctorId = "Doctor";
            Assert.IsFalse(doctorRepository.Update(doctor));
            doctorRepository.Delete("Doc100");
        }


        [TestMethod]
        public void Given_Valid_Arguments_When_Delete_Invoked_Then_true_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.IsTrue(doctorRepository.Delete("Doc100"));
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Delete_Invoked_Then_False_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.IsFalse(doctorRepository.Delete("Doc200"));
            doctorRepository.Delete("Doc100");
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_ReadAll_Invoked_Then_true_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            doctor.DoctorId = "Doc200";
            doctorRepository.RegisterNew(doctor);
            Assert.AreEqual(2, doctorRepository.ReadAll().Count);
            doctorRepository.Delete(doctor.DoctorId);
            doctorRepository.Delete("Doc100");
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Exists_Invoked_Then_true_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.IsTrue(doctorRepository.Exists(doctor.DoctorId));
            doctorRepository.Delete(doctor.DoctorId);
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Exists_Invoked_Then_False_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.IsFalse(doctorRepository.Exists("Doc200"));
            doctorRepository.Delete(doctor.DoctorId);
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Invoked_Then_Valid_Result_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.AreEqual(doctor.Name, doctorRepository.Read("Doc100").Name);
            doctorRepository.Delete(doctor.DoctorId);
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Invoked_Then_Invalid_Result_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.AreNotEqual(doctor.Name, doctorRepository.Read("Doc200").Name);
            Assert.AreNotEqual(doctorRepository.Read("Doc200").DoctorId, "Doc200");
            doctorRepository.Delete(doctor.DoctorId);
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Update_Status_Invoked_Then_Valid_Result_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.IsTrue(doctorRepository.UpdateStatus(doctor.DoctorId, DoctorStatus.OnBreak));
            Assert.AreEqual(DoctorStatus.OnBreak, doctorRepository.Read("Doc100").Status);
            doctorRepository.Delete(doctor.DoctorId);
        }

        [TestMethod]
        public void Given_Invalid_Argument_When_Update_Status_Invoked_Then_Invalid_Result_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            Assert.IsFalse(doctorRepository.UpdateStatus("Doc200", DoctorStatus.OnBreak));
            Assert.AreNotEqual(DoctorStatus.OnBreak, doctorRepository.Read("Doc200").Status);
            doctorRepository.Delete(doctor.DoctorId);
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Doctors_In_Dept_Invoked_Then_Valid_Result_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            doctor.DoctorId = "Doc200";
            doctorRepository.RegisterNew(doctor);
            Assert.AreEqual(2, doctorRepository.ReadDoctorsInDepartment(HospitalDepartment.Cardiology).Count);
            doctorRepository.Delete(doctor.DoctorId);
            doctorRepository.Delete("Doc100");
        }
        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Doctors_In_Dept_Invoked_Then_Invalid_Result_Asserted()
        {
            var doctor = CreateDoctor();
            doctorRepository.RegisterNew(doctor);
            doctor.DoctorId = "Doc200";
            doctorRepository.RegisterNew(doctor);
            Assert.AreNotEqual(2, doctorRepository.ReadDoctorsInDepartment(HospitalDepartment.Gynaecology).Count);
            doctorRepository.Delete(doctor.DoctorId);
            doctorRepository.Delete("Doc100");
        }

    }
}
