using System;
using System.Collections.Generic;
using System.Linq;
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
using Models.Device;
using Models.PatientAdmission;
using Unity;

namespace Controllers.PatientAdmissionService
{
    public class PatientAdmissionService : IPatientAdmissionService
    {
        private IAdmissionRepository admissionRepo;
        readonly IPatientRepository patientRepo;
        readonly IDeviceRepository deviceRepo;
        IDoctorRepository doctorRepo;
        IHospitalBedRepository bedRepo;
        
        public PatientAdmissionService()
        {
            deviceRepo = new DeviceRepository();
            patientRepo = new PatientRepository();
            doctorRepo = new DoctorRepository();
            bedRepo = new HospitalBedRepository();

            //admissionRepo = new AdmissionRepository(patientRepo, new HospitalBedRepository(), deviceRepo, new CustomDeviceRepository(new DeviceRepository() ) );

            UnityContainer _diContainer = new UnityContainer();
            _diContainer.LoadConfiguration();
            admissionRepo = _diContainer.Resolve<Contracts.AdmissionRepository.IAdmissionRepository>();

        }

        public void AdmitPatient(PatientAdmission patient)
        {
            admissionRepo.Admit(patient);
        }

        public void UpdateAdmission(PatientAdmission patient)
        {
            admissionRepo.Update(patient);
        }

        public void DischargePatient(string patientId)
        {
            admissionRepo.Discharge(patientId);
        }

        public List<PatientAdmission> ReadAllAdmissions()
        {
            return admissionRepo.ReadAll();
        }

        public PatientAdmission ReadAdmission(string patientId)
        {
            return admissionRepo.Read(patientId);
        }

        public void AddDeviceToPatient(string patientId, string deviceId, Device device)
        {
            admissionRepo.AddDevice(patientId, device);
        }

        public void RemoveDeviceFromPatient(string patientId, string deviceId)
        {
            admissionRepo.RemoveDevice(patientId, deviceId);
        }
    }
}