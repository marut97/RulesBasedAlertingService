using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.PatientAlert;
using Models.PatientVitals;

namespace MockRepositoryLib
{
    public class Callbacks
    {
        public void CallbackFunc(PatientAlert alert)
        {
            alert.PatientId = "TestPassed";
        }

        public void CallbackFunc1(PatientAlert alert)
        {
            alert.PatientId = "TestPassed1";
        }

        public void VitalsCallback(PatientVitals vitals)
        {
            vitals.PatientId = "TestPassed";
        }

        public void VitalsCallback1(PatientVitals vitals)
        {
            vitals.PatientId = "TestPassed1";
        }
    }
}
