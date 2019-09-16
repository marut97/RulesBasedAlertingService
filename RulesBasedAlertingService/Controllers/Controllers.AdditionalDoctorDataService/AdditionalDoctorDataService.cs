using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Contracts.AdmissionRepository;
using Contracts.CustomDeviceRepository;
using Contracts.DeviceRepository;
using Contracts.DoctorRepository;
using Contracts.HospitalBedRepository;
using Contracts.PatientRepository;
using DataAccessLayer.AdmissionRepositoryLib;
using DataAccessLayer.CustomDeviceRepoLib;
using DataAccessLayer.DeviceRepositoryLib;
using DataAccessLayer.DoctorRepositoryLib;
using DataAccessLayer.HospitalBedRepoLib;
using DataAccessLayer.PatientRepositoryLib;
using Microsoft.Practices.Unity.Configuration;
using Models.Doctor;
using Models.Patient;
using Models.PatientAdmission;
using Unity;

namespace Controllers.AdditionalDoctorDataService
{
    public class AdditionalDoctorDataService : IAdditionalDoctorDataService
    {
        private readonly IAdmissionRepository m_admissionRepo;
        private readonly IDoctorRepository m_doctorRepo;
        private readonly IPatientRepository m_patientRepo;

        public AdditionalDoctorDataService()
        {
            m_patientRepo = new PatientRepository();
            UnityContainer _diContainer = new UnityContainer();
            _diContainer.LoadConfiguration();
            m_admissionRepo = _diContainer.Resolve<Contracts.AdmissionRepository.IAdmissionRepository>();
            m_doctorRepo = new DoctorRepository();

        }
        public List<Patient> ReadAllPatientRegistrations(string doctorId)
        {
            Doctor doc = m_doctorRepo.Read(doctorId);
            List<Patient> patients = new List<Patient>();
            foreach (var patient in doc.Patients)
            {
                patients.Add(m_patientRepo.Read(patient));
            }

            return patients;
        }

        public Patient ReadPatientRegistration(string patientId)
        {
            return m_patientRepo.Read(patientId);
        }

        public List<PatientAdmission> ReadAllPatientAdmissions(string doctorId)
        {
            Doctor doc = m_doctorRepo.Read(doctorId);
            List<PatientAdmission> patients = new List<PatientAdmission>();
            foreach (var patient in doc.Patients)
            {
                patients.Add(m_admissionRepo.Read(patient));
            }
            return patients;
        }

        public PatientAdmission ReadPatientAdmission(string patientId)
        {
            return m_admissionRepo.Read(patientId);
        }

        public List<Doctor> ReadAllDoctors()
        {
            return m_doctorRepo.ReadAll();
        }

        public Doctor ReadDoctor(string doctorId)
        {
            return m_doctorRepo.Read(doctorId);
        }

        public void UpdateDoctorStatus(string doctorId, DoctorStatus status)
        {
            m_doctorRepo.UpdateStatus(doctorId, status);
        }

    }
}