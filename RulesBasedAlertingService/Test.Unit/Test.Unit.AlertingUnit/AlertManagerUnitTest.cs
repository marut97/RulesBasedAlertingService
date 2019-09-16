using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockRepositoryLib;
using Models.HospitalBed;
using Models.PatientAlert;
using Models.PatientVitals;
using Processor.AlertingUnitLib;
using SharedResources.AlertMonitorList;
using SharedResources.VitalsMonitorList;

namespace Test.Unit.AlertingUnit
{
    /// <summary>
    /// Summary description for AlertManagerUnitTest
    /// </summary>
    [TestClass]
    public class AlertManagerUnitTest
    {
        private Processor.AlertingUnitLib.AlertManager alertManager;
        [TestInitialize]
        public void TestInitialize()
        {
            alertManager = new AlertManager(new MockAdmissionRepo());
        }
        [TestMethod]
        public void Given_Valid_Critical_Alert_Doctor_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitorList subList = AlertMonitorList.Instance;
            subList.Subscribe("Doctor100", callbacks.CallbackFunc);
            PatientAlert alerts = new PatientAlert
            {
                CriticalAlerts = new List<DeviceAlert>(),
                MuteAlert = false,
                PatientId = "Pat111",
                WarningAlerts = new List<DeviceAlert>()
            };
            alerts.CriticalAlerts.Add(new DeviceAlert
            {
                DeviceId = "Dev111",
                Message = "Critical",
                Value = 56
            });
            alertManager.AlertUsers(alerts);
            Assert.AreEqual("TestPassed", alerts.PatientId);
            subList.Unsubscribe("Doctor100");
        }

        [TestMethod]
        public void Given_Valid_Warning_Alert_Doctor_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitorList subList = AlertMonitorList.Instance;
            subList.Subscribe("Doctor100", callbacks.CallbackFunc);
            PatientAlert alerts = new PatientAlert
            {
                CriticalAlerts = new List<DeviceAlert>(),
                MuteAlert = false,
                PatientId = "Pat111",
                WarningAlerts = new List<DeviceAlert>()
            };
            alerts.CriticalAlerts.Add(new DeviceAlert
            {
                DeviceId = "Dev111",
                Message = "Warning",
                Value = 86
            });
            alertManager.AlertUsers(alerts);
            Assert.AreEqual("TestPassed", alerts.PatientId);
            Assert.IsFalse(alerts.WarningAlerts.Any());
            subList.Unsubscribe("Doctor100");
        }

        [TestMethod]
        public void Given_Invalid_Critical_Alert_Bed_Subscription_When_AlertUsers_Invoked_Then_Invalid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            HospitalBed bed = new HospitalBed
                {BedNumber = 10, Campus = "PIC", Floor = "2", Occupancy = "Pat111", Wing = "2A", RoomNumber = "12"};
            AlertMonitorList subList = AlertMonitorList.Instance;
            subList.Subscribe(bed.ToString(), callbacks.CallbackFunc);
            PatientAlert alerts = new PatientAlert
            {
                CriticalAlerts = new List<DeviceAlert>(),
                MuteAlert = false,
                PatientId = "Pat111",
                WarningAlerts = new List<DeviceAlert>()
            };
            alerts.CriticalAlerts.Add(new DeviceAlert
            {
                DeviceId = "Dev111",
                Message = "Critical",
                Value = 56
            });
            alertManager.AlertUsers(alerts);
            Assert.AreNotEqual("TestPassed", alerts.PatientId);
            subList.Unsubscribe(bed.ToString());
        }

        [TestMethod]
        public void Given_Valid_Critical_Alert_Room_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            string bed = "PIC-2-2A-12";
            AlertMonitorList subList = AlertMonitorList.Instance;
            subList.Subscribe(bed, callbacks.CallbackFunc);
            PatientAlert alerts = new PatientAlert
            {
                CriticalAlerts = new List<DeviceAlert>(),
                MuteAlert = false,
                PatientId = "Pat111",
                WarningAlerts = new List<DeviceAlert>()
            };
            alerts.CriticalAlerts.Add(new DeviceAlert
            {
                DeviceId = "Dev111",
                Message = "Critical",
                Value = 56
            });
            alertManager.AlertUsers(alerts);
            Assert.AreEqual("TestPassed", alerts.PatientId);
            subList.Unsubscribe(bed);
        }

        [TestMethod]
        public void Given_Valid_Critical_Alert_Wing_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            string bed = "PIC-2-2A";
            AlertMonitorList subList = AlertMonitorList.Instance;
            subList.Subscribe(bed, callbacks.CallbackFunc);
            PatientAlert alerts = new PatientAlert
            {
                CriticalAlerts = new List<DeviceAlert>(),
                MuteAlert = false,
                PatientId = "Pat111",
                WarningAlerts = new List<DeviceAlert>()
            };
            alerts.CriticalAlerts.Add(new DeviceAlert
            {
                DeviceId = "Dev111",
                Message = "Critical",
                Value = 56
            });
            alertManager.AlertUsers(alerts);
            Assert.AreEqual("TestPassed", alerts.PatientId);
            subList.Unsubscribe(bed);
        }

        [TestMethod]
        public void Given_Valid_Critical_Alert_Floor_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            string bed = "PIC-2";
            AlertMonitorList subList = AlertMonitorList.Instance;
            subList.Subscribe(bed, callbacks.CallbackFunc);
            PatientAlert alerts = new PatientAlert
            {
                CriticalAlerts = new List<DeviceAlert>(),
                MuteAlert = false,
                PatientId = "Pat111",
                WarningAlerts = new List<DeviceAlert>()
            };
            alerts.CriticalAlerts.Add(new DeviceAlert
            {
                DeviceId = "Dev111",
                Message = "Critical",
                Value = 56
            });
            alertManager.AlertUsers(alerts);
            Assert.AreEqual("TestPassed", alerts.PatientId);
            subList.Unsubscribe(bed);
        }

        [TestMethod]
        public void Given_Valid_Critical_Alert_Campus_Subscription_When_AlertUsers_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            string bed = "PIC";
            AlertMonitorList subList = AlertMonitorList.Instance;
            subList.Subscribe(bed, callbacks.CallbackFunc);
            PatientAlert alerts = new PatientAlert
            {
                CriticalAlerts = new List<DeviceAlert>(),
                MuteAlert = false,
                PatientId = "Pat111",
                WarningAlerts = new List<DeviceAlert>()
            };
            alerts.CriticalAlerts.Add(new DeviceAlert
            {
                DeviceId = "Dev111",
                Message = "Critical",
                Value = 56
            });
            alertManager.AlertUsers(alerts);
            Assert.AreEqual("TestPassed", alerts.PatientId);
            subList.Unsubscribe(bed);
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_AlertUsers_Invoked_Then_Invalid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitorList subList = VitalsMonitorList.Instance;
            subList.Subscribe("Pat112", "Doc100", callbacks.VitalsCallback);
            PatientAlert alerts = new PatientAlert
            {
                CriticalAlerts = new List<DeviceAlert>(),
                MuteAlert = false,
                PatientId = "Pat111",
                WarningAlerts = new List<DeviceAlert>()
            };
            alerts.WarningAlerts.Add(new DeviceAlert
            {
                DeviceId = "Dev111",
                Message = "Warning",
                Value = 86
            });
            alertManager.AlertUsers(alerts);
            Assert.AreNotEqual("TestPassed", alerts.PatientId);
            subList.Unsubscribe("Pat112", "Doc100");
        }
    }
}
