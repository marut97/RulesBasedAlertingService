using System.Collections.Generic;
using System.ServiceModel;
using Models.Doctor;
using Models.Patient;
using Models.PatientAdmission;

namespace Controllers.AdditionalNurseDataService
{
    [ServiceContract]
    public interface IAdditionalNurseDataService
    {
        [OperationContract(IsOneWay = false)]
        List<Patient> ReadAllPatientRegistrations(string locationId);

        [OperationContract(IsOneWay = false)]
        Patient ReadPatientRegistration(string patientId);

        [OperationContract(IsOneWay = false)]
        List<PatientAdmission> ReadAllPatientAdmissions(string locationId);

        [OperationContract(IsOneWay = false)]
        PatientAdmission ReadPatientAdmission(string patientId);

        [OperationContract(IsOneWay = false)]
        List<Doctor> ReadAllDoctors(string locationId);

        [OperationContract(IsOneWay = false)]
        Doctor ReadDoctor(string doctorId);
    }
}
