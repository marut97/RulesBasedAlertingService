using Contracts.AdmissionRepository;
using Contracts.CustomDeviceRepository;
using Contracts.DeviceRepository;
using Contracts.HospitalBedRepository;
using Contracts.PatientRepository;
using Models.Device;
using Models.HospitalBed;
using Models.Patient;
using Models.PatientAdmission;
using System.Collections.Generic;
using System.Linq;
using Utility.SqlHelper;

namespace DataAccessLayer.AdmissionRepositoryLib
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly string conString;
        private readonly IPatientRepository m_patientRepo;
        private readonly IHospitalBedRepository m_bedRepo;
        private readonly IDeviceRepository m_defaultDeviceRepo;
        private readonly ICustomDeviceRepository m_customDeviceRepo;
        private readonly SqlPatientAdmissionReader patientAdmissionReader;
        public AdmissionRepository(IPatientRepository patientRepo, IHospitalBedRepository bedRepo, IDeviceRepository deviceRepo, ICustomDeviceRepository customRepo, bool test = false)
        {
            m_patientRepo = patientRepo;
            m_bedRepo = bedRepo;
            m_customDeviceRepo = customRepo;
            m_defaultDeviceRepo = deviceRepo;
            patientAdmissionReader = new SqlPatientAdmissionReader();

            conString = test ? @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB.Test;Integrated Security=True" : @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB;Integrated Security=True";
        }

        public bool Admit(PatientAdmission patient)
        {
            var query = $"Select * From PatientAdmission where PatientId='{patient.PatientId}'";
            if (!SqlCommands.Exists(conString, query))
            {
                return AdmitPatientAdmission(patient);
            }
            return false;
        }


        private bool AdmitPatientAdmission(PatientAdmission patient)
        {
            var query = $"Insert into PatientAdmission values('{patient.PatientId}','{patient.DoctorId}','{patient.Illness}','{patient.Diagnosis}','{patient.AdmissionTime}','0')";
            if (SqlCommands.ExecuteCommand(query, conString) && WriteDevices(patient))
                return UpdateBed(patient);
            return false;
        }

       

        private bool UpdateBed(PatientAdmission patient)
        {
            if (patient.Bed.BedNumber != m_bedRepo.ReadPatientsBed(patient.PatientId).BedNumber)
                return m_bedRepo.Update(patient.Bed);
            return true;
        }

        private bool WriteDevices(PatientAdmission patient)
        {
            foreach (var device in patient.Devices)
            {
                if (!WritePatientDevice(patient.PatientId, device))
                {
                    Discharge(patient.PatientId);
                    return false;
                }
            }
            return true;
        }

        private bool WritePatientDevice(string patientId, Device device)
        {
            bool custom = (device.Limits!=null);
            string query = $"Select * From PatientDevices where PatientId='{patientId}' AND DeviceId='{device.DeviceId}'";
            if (!SqlCommands.Exists(conString, query))
            {
                query = $"Insert into PatientDevices values('{patientId}','{device.DeviceId}','{custom}')";
                if (SqlCommands.ExecuteCommand(query, conString))
                {
                    return WriteCustomDevice(custom,patientId,device);
                }
            }
            return false;
        }

        private bool WriteCustomDevice(bool custom,string patientId,Device device)
        {
            if (custom)
            {
                return m_customDeviceRepo.RegisterCustom(device, patientId);

            }
            return true;
        }


        public bool Discharge(string patientId)
        {
            var admissionQuery = string.Format("Select * FROM PatientAdmission WHERE PatientId='{0}'", patientId);
            var admissions = patientAdmissionReader.FetchPatientAdmissions(conString, admissionQuery);
            if (admissions.Any())
            {
                m_patientRepo.AddAdmissionHistory(patientId, new AdmissionHistory
                {
                    AdmissionDate = admissions.First().AdmissionTime.ToString(),
                    Diagnosis = admissions.First().Diagnosis,
                    Illness = admissions.First().Illness
                });
                var bed = m_bedRepo.ReadPatientsBed(patientId);
                bed.Occupancy = "";
                m_bedRepo.Update(bed);


                string query = $"DELETE FROM PatientAdmission WHERE PatientId='{patientId}'";
                if (SqlCommands.ExecuteCommand(query, conString))
                    return DeletePatientDevices(patientId);
            }
            return false;
        }

        private bool DeletePatientDevices(string patientId)
        {
            var query = $"DELETE FROM PatientDevices WHERE PatientId='{patientId}'";
            if (SqlCommands.ExecuteCommand(query, conString))
            {

                if (m_customDeviceRepo.Exists(patientId))
                {

                    return m_customDeviceRepo.DeleteCustom(patientId);
                }
                return true;
            }
            return false;
        }

        public bool Exists(string patientId)
        {
            var query = $"Select PatientID FROM PatientAdmission WHERE PatientId='{patientId}'";
            return SqlCommands.Exists(conString, query);
        }

        public bool AddDevice(string patientId, Device device)
        {
            if (Exists(patientId))
                return WritePatientDevice(patientId, device);
            return false;
        }

        public bool RemoveDevice(string patientId, string deviceId)
        {
            if (Exists(patientId))
            {
                return DeletePatientDevice(patientId, deviceId);
            }
            return false;
        }


        private bool DeletePatientDevice(string patientId,string deviceId)
        {
            var query = $"DELETE FROM PatientDevices WHERE PatientId='{patientId}' AND DeviceId='{deviceId}'";
            if (SqlCommands.ExecuteCommand(query, conString))
            {
                if (m_customDeviceRepo.Exists(patientId, deviceId))
                {
                    return m_customDeviceRepo.DeleteCustom(deviceId, patientId);
                }
                return true;
            }
            return false;
        }

        public bool MuteAlerts(string patientId)
        {
            string query = $"UPDATE PatientAdmission SET MuteAlert = '1' WHERE PatientId='{patientId}'";
            return SqlCommands.ExecuteCommand(query, conString);
        }

        public bool UnmuteAlerts(string patientId)
        {
            string query = $"UPDATE PatientAdmission SET MuteAlert = '0' WHERE PatientId='{patientId}'";
            return SqlCommands.ExecuteCommand(query, conString);
        }

        public PatientAdmission Read(string patientId)
        {
            string query = $"Select * FROM PatientAdmission WHERE PatientId='{patientId}'";
            var patientAdmissions = patientAdmissionReader.FetchPatientAdmissions(conString, query);
            patientAdmissionReader.FetchPatientsDevices(conString, ref patientAdmissions);
            foreach (var patientAdmission in patientAdmissions)
            {
                patientAdmission.Devices = ReadDevices(patientAdmission);
            }

            if (patientAdmissions.Any())
            {
                patientAdmissions.First().Bed = m_bedRepo.ReadPatientsBed(patientAdmissions.First().PatientId);
                return patientAdmissions.First();
            }

            PatientAdmission admission = new PatientAdmission
            {
                Devices = new List<Device>(), Bed = new HospitalBed()
            };
            return admission;
        }

        public List<PatientAdmission> ReadAll()
        {
            string query = $"Select * FROM PatientAdmission";
            var patientAdmissions = patientAdmissionReader.FetchPatientAdmissions(conString, query);
            patientAdmissionReader.FetchPatientsDevices(conString, ref patientAdmissions);
            foreach (var patientAdmission in patientAdmissions)
            {
                patientAdmission.Devices = ReadDevices(patientAdmission);
                patientAdmission.Bed = m_bedRepo.ReadPatientsBed(patientAdmission.PatientId);
            }

            return patientAdmissions;
        }

        private List<Device> ReadDevices(PatientAdmission patientAdmission)
        {
            List<Device> deviceMap = new List<Device>();
            foreach (var device in patientAdmission.Devices)
            {
                Device dev;

                if (device.Limits!=null)
                {
                    dev = m_customDeviceRepo.ReadCustom(device.DeviceId, patientAdmission.PatientId);
                }
                else
                    dev = m_defaultDeviceRepo.Read(device.DeviceId);
                deviceMap.Add(dev);
            }
            return deviceMap;

        }


        public bool Update(PatientAdmission patient)
        {
            if (Exists(patient.PatientId))
            {
                return UpdatePatientAdmission(patient);
            }
            return false;
        }

        public bool UpdatePatientAdmission(PatientAdmission patient)
        {
            string query = $"DELETE FROM PatientAdmission WHERE PatientId='{patient.PatientId}'";
            if (SqlCommands.ExecuteCommand(query, conString) && DeletePatientDevices(patient.PatientId))
            {
                return UpdateDetails(patient);
            }
            return false;
        }

        private bool UpdateDetails(PatientAdmission patient)
        {
            if (Admit(patient))
            {
                return UpdateHospitalBed(patient);
            }

            return false;
        }

       private bool UpdateHospitalBed(PatientAdmission patient)
        {
            var oldBed = m_bedRepo.ReadPatientsBed(patient.PatientId);
            if (patient.Bed.ToString() != oldBed.ToString())
            {
                oldBed.Occupancy = "";
                if (m_bedRepo.Update(oldBed))
                    return m_bedRepo.Update(patient.Bed);
                return false;
            }

            return true;
        }
    }
}
