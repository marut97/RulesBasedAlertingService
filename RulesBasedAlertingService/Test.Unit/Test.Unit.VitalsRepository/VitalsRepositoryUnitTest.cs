using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Contracts.VitalsRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.PatientVitals;

namespace Test.Unit.VitalsRepository
{
    [TestClass]
    public class VitalsRepositoryUnitTest
    {
        [TestMethod]
        public void Given_Valid_Arguments_When_ReadAllVitals_Invoked_Then_Valid_Result_Asserted()
        {
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "111",
                Vitals = new List<Vitals>
                {
                    new Vitals {DeviceId = "100", Value = 100.0},
                    new Vitals {DeviceId = "101", Value = 10.0}
                }
            };
            IVitalsRepository repo = new DataAccessLayer.VitalsRepositoryLib.VitalsRepository(true);
            repo.WriteVitals(vitals);
            var result = repo.ReadAllVitals(vitals.PatientId);
            Assert.IsNotNull(result);
            vitals = result[result.Count - 1];
            Assert.AreEqual("111", vitals.PatientId);
            Assert.AreEqual("100", vitals.Vitals[0].DeviceId);
            Assert.AreEqual("101", vitals.Vitals[1].DeviceId);
            Assert.AreEqual(100.0, vitals.Vitals[0].Value);
            Assert.AreEqual(10.0, vitals.Vitals[1].Value);
            File.Delete("C:\\VitalsDB\\TestDatabase\\111.txt");
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_ReadAllVitals_Invoked_Then_Invalid_Result_Asserted()
        {
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "555",
                Vitals = new List<Vitals>
                {
                    new Vitals {DeviceId = "100", Value = 100.0},
                    new Vitals {DeviceId = "101", Value = 10.0}
                }
            };
            IVitalsRepository repo = new DataAccessLayer.VitalsRepositoryLib.VitalsRepository(true);
            var result = repo.ReadAllVitals(vitals.PatientId);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_WriteVitals_Invoked_Then_File_Created_Valid_Result_Asserted()
        {
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "333",
                Vitals = new List<Vitals>
                {
                    new Vitals {DeviceId = "100", Value = 100.0},
                    new Vitals {DeviceId = "101", Value = 10.0}
                }
            };
            IVitalsRepository repo = new DataAccessLayer.VitalsRepositoryLib.VitalsRepository(true);
            repo.WriteVitals(vitals);
            var result = repo.ReadAllVitals(vitals.PatientId);
            Assert.IsNotNull(result);
            vitals = result[0];
            Assert.AreEqual("333", vitals.PatientId);
            Assert.AreEqual("100", vitals.Vitals[0].DeviceId);
            Assert.AreEqual("101", vitals.Vitals[1].DeviceId);
            Assert.AreEqual(100.0, vitals.Vitals[0].Value);
            Assert.AreEqual(10.0, vitals.Vitals[1].Value);
            File.Delete("C:\\VitalsDB\\TestDatabase\\333.txt");
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_WriteVitals_Invoked_Then_File_Appended_Valid_Result_Asserted()
        {
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "111",
                Vitals = new List<Vitals>
                {
                    new Vitals {DeviceId = "100", Value = 100.0},
                    new Vitals {DeviceId = "101", Value = 10.0}
                }
            };

            IVitalsRepository repo = new DataAccessLayer.VitalsRepositoryLib.VitalsRepository(true);
            repo.WriteVitals(vitals);
            repo.WriteVitals(vitals);
            repo.WriteVitals(vitals);
            int count = repo.ReadAllVitals("111").Count;
            repo.WriteVitals(vitals);
            Assert.AreEqual(count + 1, repo.ReadAllVitals(vitals.PatientId).Count);
            File.Delete("C:\\VitalsDB\\TestDatabase\\111.txt");
        }
    }
}
