using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.AdmissionRepository;
using Models.Device;
using Models.HospitalBed;
using Models.PatientAdmission;

namespace MockRepositoryLib
{
    public class MockAdmissionRepo : IAdmissionRepository
    {
        public bool Admit(PatientAdmission patient)
        {
            throw new NotImplementedException();
        }

        public bool Update(PatientAdmission patient)
        {
            throw new NotImplementedException();
        }

        public bool Discharge(string patientId)
        {
            throw new NotImplementedException();
        }

        public List<PatientAdmission> ReadAll()
        {
            throw new NotImplementedException();
        }

        public PatientAdmission Read(string patientId)
        {
            PatientAdmission patient = new PatientAdmission
            {
                PatientId = patientId,
                AdmissionTime = DateTime.Now.ToString(),
                Bed = new HospitalBed { BedNumber = 10, Campus = "PIC", Floor = "2", Occupancy = patientId, Wing = "2A", RoomNumber = "12" },
                Devices = new List<Device>(),
                Diagnosis = "Diagnosis",
                DoctorId = "100",
                Illness = "Fever",
                MuteAlert = false
            };
            patient.Devices.Add(new Device
            {
                DeviceId = "Dev111",
                Limits = new List<Limits>
                {
                    new Limits
                    {
                        MaxValue = 50,
                        Message = "Normal",
                        MinValue = 0,
                        Type = LimitType.Normal
                    },
                    new Limits
                    {
                        MaxValue = 75,
                        Message = "Normal",
                        MinValue = 50,
                        Type = LimitType.Critical
                    },
                    new Limits
                    {
                        MaxValue = 100,
                        Message = "Normal",
                        MinValue = 75,
                        Type = LimitType.Warning
                    }
                },
                MaxInputValue = 100,
                MinInputValue = 0
            });
            return patient;
        }

        public bool Exists(string patientId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveDevice(string patientId, string deviceId)
        {
            throw new NotImplementedException();
        }

        public bool MuteAlerts(string patientId)
        {
            throw new NotImplementedException();
        }

        public bool UnmuteAlerts(string patientId)
        {
            throw new NotImplementedException();
        }

        public bool AddDevice(string patientId, Device device)
        {
            throw new NotImplementedException();
        }
    }
}
