using System;
using System.Text;
using System.Collections.Generic;
using Contracts.AdmissionRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockRepositoryLib;
using Models.PatientVitals;
using Processor.AlertingUnitLib;
using SharedResources.VitalsMonitorList;

namespace Test.Unit.AlertingUnit
{
    [TestClass]
    public class VitalsAlertManagerUnitTest
    {
        private Processor.AlertingUnitLib.VitalsAlertManager vitalsAlertManager;
        [TestInitialize]
        public void TestInitialize()
        {
            vitalsAlertManager = new VitalsAlertManager();
        }
        [TestMethod]
        public void Given_Valid_Arguments_Doctor_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitorList subList = VitalsMonitorList.Instance;
            subList.Subscribe("Pat111", "Doc100",callbacks.VitalsCallback);
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 50
            });
            vitalsAlertManager.AlertUsers(vitals);
            Assert.AreEqual("TestPassed", vitals.PatientId);
            subList.Unsubscribe("Pat111", "Doc100");
        }

        [TestMethod]
        public void Given_Valid_Arguments_Room_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitorList subList = VitalsMonitorList.Instance;
            subList.Subscribe("Pat111", "PIC-2-2A-12", callbacks.VitalsCallback);
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 50
            });
            vitalsAlertManager.AlertUsers(vitals);
            Assert.AreEqual("TestPassed", vitals.PatientId);
            subList.Unsubscribe("Pat111", "PIC-2-2A-12");
        }

        [TestMethod]
        public void Given_Valid_Arguments_Wing_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitorList subList = VitalsMonitorList.Instance;
            subList.Subscribe("Pat111", "PIC-2-2A", callbacks.VitalsCallback);
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 50
            });
            vitalsAlertManager.AlertUsers(vitals);
            Assert.AreEqual("TestPassed", vitals.PatientId);
            subList.Unsubscribe("Pat111", "PIC-2-2A");
        }

        [TestMethod]
        public void Given_Valid_Arguments_Floor_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitorList subList = VitalsMonitorList.Instance;
            subList.Subscribe("Pat111", "PIC-2", callbacks.VitalsCallback);
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 50
            });
            vitalsAlertManager.AlertUsers(vitals);
            Assert.AreEqual("TestPassed", vitals.PatientId);
            subList.Unsubscribe("Pat111", "PIC-2");
        }

        [TestMethod]
        public void Given_Valid_Arguments_Campus_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitorList subList = VitalsMonitorList.Instance;
            subList.Subscribe("Pat111", "PIC", callbacks.VitalsCallback);
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 50
            });
            vitalsAlertManager.AlertUsers(vitals);
            Assert.AreEqual("TestPassed", vitals.PatientId);
            subList.Unsubscribe("Pat111", "PIC");
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_AlertUsers_Invoked_Then_Invalid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitorList subList = VitalsMonitorList.Instance;
            subList.Subscribe("Pat112", "Doc100", callbacks.VitalsCallback);
            PatientVitals vitals = new PatientVitals
            {
                PatientId = "Pat111",
                Vitals = new List<Vitals>()
            };
            vitals.Vitals.Add(new Vitals
            {
                DeviceId = "Dev111",
                Value = 50
            });
            vitalsAlertManager.AlertUsers(vitals);
            Assert.AreNotEqual("TestPassed", vitals.PatientId);
            subList.Unsubscribe("Pat112", "Doc100");
        }
    }
}
