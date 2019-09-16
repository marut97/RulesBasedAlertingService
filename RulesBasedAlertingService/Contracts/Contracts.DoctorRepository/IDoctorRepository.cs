using System.Collections.Generic;
using Models.Doctor;

namespace Contracts.DoctorRepository
{
    public interface IDoctorRepository
    {
        bool RegisterNew(Doctor doctor);
        bool Update(Doctor doctor);
        bool Delete(string doctorId);
        List<Doctor> ReadAll();
        List<Doctor> ReadDoctorsInDepartment(HospitalDepartment department);
        Doctor Read(string doctorId);
        bool Exists(string doctorId);
        bool UpdateStatus(string doctorId, DoctorStatus status);
    }
}
