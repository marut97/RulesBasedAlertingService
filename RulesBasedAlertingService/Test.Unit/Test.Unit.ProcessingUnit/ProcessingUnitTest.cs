using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Contracts.AlertManager;
using Contracts.VitalsRepository;
using DataAccessLayer.VitalsRepositoryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockRepositoryLib;
using Models.PatientAlert;
using Models.PatientVitals;
using Processor.ProcessingUnitLib;
using SharedResources.SharedQueue;

namespace Test.Unit.ProcessingUnit
{
    [TestClass]
    public class ProcessingUnitTest
    {
        private Processor.ProcessingUnitLib.ProcessingUnit m_processor;
        private IVitalsAlertManager vitalsMock;
        private IAlertManager alertMock;
        private IVitalsRepository vitalsRepo;
        public int countFinal;
        [TestInitialize]

        public void TestInitialize()
        {
            countFinal = 0;
            alertMock = new MockAlertManager(ref countFinal);
            vitalsMock = new MockVitalsAlertManager();
            vitalsRepo = new VitalsRepository(true);
            m_processor = new Processor.ProcessingUnitLib.ProcessingUnit(vitalsRepo, alertMock, vitalsMock, new MockAdmissionRepo());
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Process_Invoked_Then_true_Asserted()
        {
            Thread thread = new Thread(new ThreadStart(m_processor.Process));
            thread.Start();
            int count = vitalsRepo.ReadAllVitals("Pat111").Count;
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

            SharedQueue queue = SharedQueue.Instance;
            queue.Enqueue(vitals);
            Thread.Sleep(10000);
            Assert.AreEqual(count+1, vitalsRepo.ReadAllVitals("Pat111").Count);
            thread.Abort();
            File.Delete("C:\\VitalsDB\\TestDatabase\\Pat111.txt");
        }
    }
}
