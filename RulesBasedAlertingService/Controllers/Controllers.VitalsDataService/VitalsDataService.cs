using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.PatientVitals;
using SharedResources.SharedQueue;

namespace Controllers.VitalsDataService
{
    public class VitalsDataService : IVitalsDataService
    {
        private readonly SharedQueue m_queue;

        public VitalsDataService()
        {
            m_queue = SharedQueue.Instance;
        }
        public void WriteVitals(PatientVitals vitals)
        {
            m_queue.Enqueue(vitals);
        }
    }
}