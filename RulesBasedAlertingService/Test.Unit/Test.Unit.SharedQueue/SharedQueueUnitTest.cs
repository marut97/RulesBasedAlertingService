using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.PatientVitals;

namespace Test.Unit.SharedQueue
{
    [TestClass]
    public class SharedQueueUnitTest
    {
        private SharedResources.SharedQueue.SharedQueue m_queue;
        [TestInitialize]
        public void TestInitialize()
        {
            m_queue = SharedResources.SharedQueue.SharedQueue.Instance;
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Multiple_Instance_Invoked_Then_Valid_Result_Asserted()
        {
            m_queue.Enqueue(new PatientVitals
            {
                PatientId = "Pat000",
                Vitals = new List<Vitals>()
            });
            SharedResources.SharedQueue.SharedQueue queueList = SharedResources.SharedQueue.SharedQueue.Instance;
            Assert.IsFalse(queueList.IsEmpty());
            m_queue.Dequeue();
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Enqueue_Invoked_Then_Valid_Result_Asserted()
        {
            m_queue.Enqueue(new PatientVitals
            {
                PatientId = "Pat000",
                Vitals = new List<Vitals>()
            });
            var result = m_queue.Dequeue();
            Assert.AreEqual("Pat000", result.PatientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Multiple_Enqueue_Invoked_Then_Valid_Result_Asserted()
        {
            m_queue.Enqueue(new PatientVitals
            {
                PatientId = "Pat000",
                Vitals = new List<Vitals>()
            });
            SharedResources.SharedQueue.SharedQueue secQueue = SharedResources.SharedQueue.SharedQueue.Instance;
            secQueue.Enqueue(new PatientVitals
            {
                PatientId = "Pat1010",
                Vitals = new List<Vitals>()
            });
            var result = secQueue.Dequeue();
            Assert.IsFalse(m_queue.IsEmpty());
            result = m_queue.Dequeue();
            Assert.IsTrue(m_queue.IsEmpty());
        }

        [TestMethod]
        public void Given_Invalid_Arguments_When_Dequeue_Invoked_Then_Invalid_Result_Asserted()
        {
            Assert.IsNull(m_queue.Dequeue().PatientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Dequeue_Invoked_Then_Valid_Result_Asserted()
        {
            m_queue.Enqueue(new PatientVitals
            {
                PatientId = "Pat000",
                Vitals = new List<Vitals>()
            });
            var result = m_queue.Dequeue();
            Assert.AreEqual("Pat000", result.PatientId);
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Different_Object_Unsubscribe_Invoked_Then_Valid_Result_Asserted()
        {
            m_queue.Enqueue(new PatientVitals
            {
                PatientId = "Pat000",
                Vitals = new List<Vitals>()
            });
            SharedResources.SharedQueue.SharedQueue secQueue = SharedResources.SharedQueue.SharedQueue.Instance;
            secQueue.Enqueue(new PatientVitals
            {
                PatientId = "Pat1010",
                Vitals = new List<Vitals>()
            });
            var result = secQueue.Dequeue();
            Assert.AreEqual("Pat000", result.PatientId);
            result = m_queue.Dequeue();
            Assert.AreEqual("Pat1010", result.PatientId);
        }
    }
}
