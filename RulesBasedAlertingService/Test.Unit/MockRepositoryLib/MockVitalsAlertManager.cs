using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.AlertManager;
using Models.PatientVitals;

namespace MockRepositoryLib
{
    public class MockVitalsAlertManager : IVitalsAlertManager
    {
        public int Count { get; set; }

        public MockVitalsAlertManager()
        {
            Count = 0;
        }
        public void AlertUsers(PatientVitals vitals)
        {
            Count++;
        }
    }
}
