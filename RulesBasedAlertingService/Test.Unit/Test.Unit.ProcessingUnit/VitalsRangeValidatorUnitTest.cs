using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.AdmissionRepositoryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockRepositoryLib;
using Models.PatientAlert;
using Models.PatientVitals;
using Processor.ProcessingUnitLib;

namespace Test.Unit.ProcessingUnit
{
    [TestClass]
    public class VitalsRangeValidatorUnitTest
    {
        private VitalsRangeValidator m_validator;

        [TestInitialize]
        public void TestInitialize()
        {
            m_validator = new VitalsRangeValidator(new MockAdmissionRepo());
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Validate_Vitals_Range_Invoked_Then_Valid_Result_Asserted()
        {
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 23
            });
            PatientAlert alert = m_validator.ValidateVitalsRange(vitals);
            Assert.AreEqual("Pat111", alert.PatientId);
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Validate_Vitals_Range_Invoked_Then_Invalid_Result_Asserted()
        {
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev112",
                Value = 51
            });
            PatientAlert alert = m_validator.ValidateVitalsRange(vitals);
            Assert.IsFalse(alert.CriticalAlerts.Any());
        }

        [TestMethod]
        public void Given_Valid_Critical_Arguments_When_Validate_Vitals_Range_Invoked_Then_Valid_Result_Asserted()
        {
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 51
            });
            PatientAlert alert = m_validator.ValidateVitalsRange(vitals);
            Assert.AreEqual(1, alert.CriticalAlerts.Count);
        }

        [TestMethod]
        public void Given_Valid_Warning_Arguments_When_Validate_Vitals_Range_Invoked_Then_Valid_Result_Asserted()
        {
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 76
            });
            PatientAlert alert = m_validator.ValidateVitalsRange(vitals);
            Assert.AreEqual(1, alert.WarningAlerts.Count);
        }
    }
}
