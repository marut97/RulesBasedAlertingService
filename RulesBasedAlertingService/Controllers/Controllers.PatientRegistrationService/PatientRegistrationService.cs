using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contracts.PatientRepository;
using DataAccessLayer.PatientRepositoryLib;
using Models.Patient;

namespace Controllers.PatientRegistrationService
{
    public class PatientRegistrationService : IPatientRegistrationService
    {

        private readonly IPatientRepository m_repository;
        public PatientRegistrationService()
        {
            m_repository = new PatientRepository();
        }


        public void RegisterNewPatient(Patient patient)
        {
            m_repository.RegisterNew(patient);
        }

        public void UpdatePatient(Patient patient)
        {
            m_repository.Update(patient);
        }

        public void DeletePatient(string patientId)
        {
            m_repository.Delete(patientId);
        }

        public List<Patient> ReadAllPatients()
        {
            return m_repository.ReadAll();
        }

        public Patient ReadPatient(string patientId)
        {
            return m_repository.Read(patientId);
        }
    }
}