using System.Collections.Generic;
using Models.Patient;

namespace Contracts.PatientRepository
{
    public interface IPatientRepository
    {
        bool RegisterNew(Patient patient);
        bool Update(Patient patient);
        bool Delete(string patientId);
        List<Patient> ReadAll();
        Patient Read(string patientId);
        bool Exists(string patientId);
        bool AddAdmissionHistory(string patientId, AdmissionHistory history);
    }
}
