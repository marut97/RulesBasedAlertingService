using System.Collections.Generic;
using Models.PatientVitals;

namespace Contracts.VitalsRepository
{
    public interface IVitalsRepository
    {
        void WriteVitals(PatientVitals vitals);
        List<PatientVitals> ReadAllVitals(string patientId);
    }
}
