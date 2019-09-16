using System.Collections.Generic;
using System.ServiceModel;
using Models.Doctor;
using Models.Patient;
using Models.PatientAdmission;

namespace Controllers.AdditionalDoctorDataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAdditionalDoctorDataService
    {
        [OperationContract(IsOneWay = false)]
        List<Patient> ReadAllPatientRegistrations(string doctorId);

        [OperationContract(IsOneWay = false)]
        Patient ReadPatientRegistration(string patientId);

        [OperationContract(IsOneWay = false)]
        List<PatientAdmission> ReadAllPatientAdmissions(string doctorId);

        [OperationContract(IsOneWay = false)]
        PatientAdmission ReadPatientAdmission(string patientId);

        [OperationContract(IsOneWay = false)]
        List<Doctor> ReadAllDoctors();

        [OperationContract(IsOneWay = false)]
        Doctor ReadDoctor(string doctorId);

        [OperationContract(IsOneWay = true)]
        void UpdateDoctorStatus(string doctorId, DoctorStatus status);

    }
}
