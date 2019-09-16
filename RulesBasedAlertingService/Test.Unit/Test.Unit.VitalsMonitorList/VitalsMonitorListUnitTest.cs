using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockRepositoryLib;
using Models.PatientVitals;
using SharedResources.VitalsMonitorList;

namespace Test.Unit.VitalsMonitorList
{
    [TestClass]
    public class VitalsMonitorListUnitTest
    {
        private SharedResources.VitalsMonitorList.VitalsMonitorList vitalsMonitorList;
        [TestInitialize]
        public void TestInitialize()
        {
            vitalsMonitorList = SharedResources.VitalsMonitorList.VitalsMonitorList.Instance;
        }
        [TestMethod]
        public void Given_Valid_Arguments_When_Multiple_Instance_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitoringFunction func = new VitalsMonitoringFunction(callbacks.VitalsCallback);
            vitalsMonitorList.Subscribe("patientId", "id", func);
            SharedResources.VitalsMonitorList.VitalsMonitorList list = SharedResources.VitalsMonitorList.VitalsMonitorList.Instance;
            Assert.IsFalse(list.IsEmpty());
            vitalsMonitorList.Unsubscribe("patientId", "id");
            Assert.IsTrue(list.IsEmpty());
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Subscribe_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitoringFunction func = new VitalsMonitoringFunction(callbacks.VitalsCallback);
            vitalsMonitorList.Subscribe("patientId", "id", func);
            SharedResources.VitalsMonitorList.VitalsMonitorList list = SharedResources.VitalsMonitorList.VitalsMonitorList.Instance;
            List<VitalsMonitoringFunction> output = list.TryGetValue("patientId");
            PatientVitals alert = new PatientVitals();
            foreach (var function in output)
            {
                function.Invoke(alert);
            }
            Assert.AreEqual("TestPassed", alert.PatientId);
            vitalsMonitorList.Unsubscribe("patientId", "id");
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Multiple_Subscribe_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitoringFunction func = new VitalsMonitoringFunction(callbacks.VitalsCallback);
            vitalsMonitorList.Subscribe("patientId", "id", func);
            func = new VitalsMonitoringFunction(callbacks.VitalsCallback1);
            vitalsMonitorList.Subscribe("patientId1", "id1", func);
            SharedResources.VitalsMonitorList.VitalsMonitorList list = SharedResources.VitalsMonitorList.VitalsMonitorList.Instance;
            var output = list.TryGetValue("patientId");
            PatientVitals alert = new PatientVitals();
            foreach (var function in output)
            {
                function.Invoke(alert);
            }
            Assert.AreEqual("TestPassed", alert.PatientId);
            output = list.TryGetValue("patientId1");
            foreach (var function in output)
            {
                function.Invoke(alert);
            }
            Assert.AreEqual("TestPassed1", alert.PatientId);
            vitalsMonitorList.Unsubscribe("patientId", "id");
            vitalsMonitorList.Unsubscribe("patientId1", "id1");
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_TryGetValue_Invoked_Then_Invalid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitoringFunction func = new VitalsMonitoringFunction(callbacks.VitalsCallback);
            vitalsMonitorList.Subscribe("patientId", "id", func);
            Assert.IsFalse(vitalsMonitorList.TryGetValue("id2").Any());
            vitalsMonitorList.Unsubscribe("patientId", "id");
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Unsubscribe_Invoked_Then_Invalid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitoringFunction func = new VitalsMonitoringFunction(callbacks.VitalsCallback);
            vitalsMonitorList.Subscribe("patientId", "id", func);
            vitalsMonitorList.Unsubscribe("patientId2", "id2");
            Assert.IsFalse(vitalsMonitorList.IsEmpty());
            vitalsMonitorList.Unsubscribe("patientId", "id");
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Unsubscribe_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitoringFunction func = new VitalsMonitoringFunction(callbacks.VitalsCallback);
            vitalsMonitorList.Subscribe("patientId", "id", func);
            Assert.IsFalse(vitalsMonitorList.IsEmpty());
            vitalsMonitorList.Unsubscribe("patientId", "id");
            Assert.IsTrue(vitalsMonitorList.IsEmpty());
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Different_Object_Unsubscribe_Invoked_Then_Valid_Result_Asserted()
        {
            Callbacks callbacks = new Callbacks();
            VitalsMonitoringFunction func = new VitalsMonitoringFunction(callbacks.VitalsCallback);
            vitalsMonitorList.Subscribe("patientId", "id", func);
            SharedResources.VitalsMonitorList.VitalsMonitorList list = SharedResources.VitalsMonitorList.VitalsMonitorList.Instance;
            Assert.IsFalse(list.IsEmpty());
            list.Unsubscribe("patientId", "id");
            Assert.IsTrue(list.IsEmpty());
        }
    }
}
