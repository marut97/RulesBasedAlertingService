using System.Collections.Generic;
using Models.Device;
using Models.PatientAdmission;

namespace Contracts.AdmissionRepository
{
    public interface IAdmissionRepository
    {
        bool Admit(PatientAdmission patient);
        bool Update(PatientAdmission patient);
        bool Discharge(string patientId);
        List<PatientAdmission> ReadAll();
        PatientAdmission Read(string patientId);
        bool Exists(string patientId);
        bool AddDevice(string patientId, Device device);
        bool RemoveDevice(string patientId, string deviceId);
        bool MuteAlerts(string patientId);
        bool UnmuteAlerts(string patientId);
    }
}
