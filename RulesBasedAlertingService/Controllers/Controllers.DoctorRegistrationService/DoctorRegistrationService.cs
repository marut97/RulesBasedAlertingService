using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Contracts.DoctorRepository;
using DataAccessLayer.DoctorRepositoryLib;
using Models.Doctor;

namespace Controllers.DoctorRegistrationService
{
    public class DoctorRegistrationService : IDoctorRegistrationService
    {
        private IDoctorRepository m_repository;

        public DoctorRegistrationService()
        {
            m_repository = new DoctorRepository();
        }

        public void RegisterNewDoctor(Doctor doctor)
        {
            m_repository.RegisterNew(doctor);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            m_repository.Update(doctor);
        }

        public void DeleteDoctor(string doctorId)
        {
            m_repository.Delete(doctorId);
        }

        public List<Doctor> ReadAllDoctors()
        {
            return m_repository.ReadAll();
        }

        public Doctor ReadDoctor(string doctorId)
        {
            return m_repository.Read(doctorId);
        }

        public List<Doctor> ReadAvailableDoctors(string department)
        {
            HospitalDepartment dept;
            Enum.TryParse(department, out dept);

            List<Doctor> availableDoctors = new List<Doctor>();

            var doctors = m_repository.ReadDoctorsInDepartment(dept);
            foreach (var doctor in doctors)
            {
                if (m_repository.Read(doctor.DoctorId).Status == DoctorStatus.Active)
                {
                    availableDoctors.Add(doctor);
                }
            }
            return availableDoctors;
        }
    }
}