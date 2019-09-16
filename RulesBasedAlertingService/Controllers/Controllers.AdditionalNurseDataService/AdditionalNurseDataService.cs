using System.Collections.Generic;
using Contracts.AdmissionRepository;
using Contracts.DoctorRepository;
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
using Utility.BedParser;

namespace Controllers.AdditionalNurseDataService
{
    
    public class AdditionalNurseDataService : IAdditionalNurseDataService
    {
        private readonly IAdmissionRepository m_admissionRepo;
        private readonly IDoctorRepository m_doctorRepo;
        private readonly IPatientRepository m_patientRepo;
        private readonly HospitalBedParser m_bedParser;
        public AdditionalNurseDataService()
        {
            m_patientRepo = new PatientRepository();
            UnityContainer _diContainer = new UnityContainer();
            _diContainer.LoadConfiguration();
            m_admissionRepo = _diContainer.Resolve<Contracts.AdmissionRepository.IAdmissionRepository>();
            m_doctorRepo = new DoctorRepository();
            m_bedParser = new HospitalBedParser(new HospitalBedRepository());
        }
        public List<Patient> ReadAllPatientRegistrations(string locationId)
        {
            var beds = m_bedParser.ReadOccupiedBeds(locationId);
            List<Patient> patients = new List<Patient>();
            foreach (var bed in beds)
            {
                patients.Add(m_patientRepo.Read(bed.Occupancy));
            }

            return patients;
        }

        public Patient ReadPatientRegistration(string patientId)
        {
            return m_patientRepo.Read(patientId);
        }

        public List<PatientAdmission> ReadAllPatientAdmissions(string locationId)
        {
            var beds = m_bedParser.ReadOccupiedBeds(locationId);
            List<PatientAdmission> patients = new List<PatientAdmission>();
            foreach (var bed in beds)
            {
                patients.Add(m_admissionRepo.Read(bed.Occupancy));
            }

            return patients;
        }

        public PatientAdmission ReadPatientAdmission(string patientId)
        {
            return m_admissionRepo.Read(patientId);
        }

        public List<Doctor> ReadAllDoctors(string locationId)
        {
            var beds = m_bedParser.ReadOccupiedBeds(locationId);
            List<Doctor> doctors = new List<Doctor>();
            foreach (var bed in beds)
            {
                doctors.Add(m_doctorRepo.Read(m_admissionRepo.Read(bed.Occupancy).DoctorId));
            }

            return doctors;
        }

        public Doctor ReadDoctor(string doctorId)
        {
            return m_doctorRepo.Read(doctorId);
        }
    }
}