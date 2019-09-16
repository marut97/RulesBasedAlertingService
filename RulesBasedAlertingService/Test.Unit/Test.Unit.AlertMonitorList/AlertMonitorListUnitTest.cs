using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockRepositoryLib;
using Models.PatientAlert;
using SharedResources.AlertMonitorList;

namespace Test.Unit.AlertMonitorList
{
    [TestClass]
    public class AlertMonitorListUnitTest
    {
        private SharedResources.AlertMonitorList.AlertMonitorList alertMonitorList;
        [TestInitialize]
        public void TestInitialize()
        {
            alertMonitorList = SharedResources.AlertMonitorList.AlertMonitorList.Instance;
        }
        [TestMethod]
        public void Given_Valid_Arguments_When_Multiple_Instance_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitoringFunction func = new AlertMonitoringFunction(callbacks.CallbackFunc);
            alertMonitorList.Subscribe("id", func);
            SharedResources.AlertMonitorList.AlertMonitorList list = SharedResources.AlertMonitorList.AlertMonitorList.Instance;
            Assert.IsFalse(list.IsEmpty());
            alertMonitorList.Unsubscribe("id");
            Assert.IsTrue(list.IsEmpty());
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Subscribe_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitoringFunction func = new AlertMonitoringFunction(callbacks.CallbackFunc);
            alertMonitorList.Subscribe("id", func);
            SharedResources.AlertMonitorList.AlertMonitorList list = SharedResources.AlertMonitorList.AlertMonitorList.Instance;
            AlertMonitoringFunction output;
            output = list.TryGetValue("id");
            PatientAlert alert = new PatientAlert();
            output?.Invoke(alert);
            Assert.AreEqual("TestPassed", alert.PatientId);
            alertMonitorList.Unsubscribe("id");
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Multiple_Subscribe_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitoringFunction func = new AlertMonitoringFunction(callbacks.CallbackFunc);
            alertMonitorList.Subscribe("id", func);
            func = new AlertMonitoringFunction(callbacks.CallbackFunc1);
            alertMonitorList.Subscribe("id1", func);
            SharedResources.AlertMonitorList.AlertMonitorList list = SharedResources.AlertMonitorList.AlertMonitorList.Instance;
            var output = list.TryGetValue("id");
            PatientAlert alert = new PatientAlert();
            output?.Invoke(alert);
            Assert.AreEqual("TestPassed", alert.PatientId);
            output = list.TryGetValue("id1");
            output?.Invoke(alert);
            Assert.AreEqual("TestPassed1", alert.PatientId);
            alertMonitorList.Unsubscribe("id");
            alertMonitorList.Unsubscribe("id1");
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_TryGetValue_Invoked_Then_Invalid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitoringFunction func = new AlertMonitoringFunction(callbacks.CallbackFunc);
            alertMonitorList.Subscribe("id", func);
            Assert.IsNull(alertMonitorList.TryGetValue("id2"));
            alertMonitorList.Unsubscribe("id");
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Unsubscribe_Invoked_Then_Invalid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitoringFunction func = new AlertMonitoringFunction(callbacks.CallbackFunc);
            alertMonitorList.Subscribe("id", func);
            alertMonitorList.Unsubscribe("id2");
            Assert.IsFalse(alertMonitorList.IsEmpty());
            alertMonitorList.Unsubscribe("id");
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Unsubscribe_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitoringFunction func = new AlertMonitoringFunction(callbacks.CallbackFunc);
            alertMonitorList.Subscribe("id", func);
            Assert.IsFalse(alertMonitorList.IsEmpty());
            alertMonitorList.Unsubscribe("id");
            Assert.IsTrue(alertMonitorList.IsEmpty());
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Different_Object_Unsubscribe_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            AlertMonitoringFunction func = new AlertMonitoringFunction(callbacks.CallbackFunc);
            alertMonitorList.Subscribe("id", func);
            SharedResources.AlertMonitorList.AlertMonitorList list = SharedResources.AlertMonitorList.AlertMonitorList.Instance;
            Assert.IsFalse(list.IsEmpty());
            list.Unsubscribe("id");
            Assert.IsTrue(list.IsEmpty());
        }
    }
}
