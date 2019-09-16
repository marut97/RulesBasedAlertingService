using System.Collections.Generic;
using Contracts.AdmissionRepository;
using Contracts.HospitalBedRepository;
using Contracts.PatientRepository;
using DataAccessLayer.AdmissionRepositoryLib;
using DataAccessLayer.HospitalBedRepoLib;
using DataAccessLayer.PatientRepositoryLib;
using Models.Device;
using Models.PatientAdmission;
using Models.PatientAlert;
using Models.PatientVitals;

namespace Processor.ProcessingUnitLib
{
    public class VitalsRangeValidator
    {
        private readonly IAdmissionRepository repository;
        public VitalsRangeValidator(IAdmissionRepository admissionRepo)
        {
            repository = admissionRepo;
        }

        public PatientAlert ValidateVitalsRange(PatientVitals patientVitals)
        {
            PatientAlert patientAlert = new PatientAlert();
            patientAlert.PatientId = patientVitals.PatientId;
            patientAlert.CriticalAlerts = new List<DeviceAlert>();
            patientAlert.WarningAlerts = new List<DeviceAlert>();
            var patient = repository.Read(patientVitals.PatientId);
            if (!patient.MuteAlert)
            {
                foreach (Vitals vital in patientVitals.Vitals)
                {
                    TryValidate(patient, vital, ref patientAlert);
                }
            }

            return patientAlert;
        }

        private void TryValidate(PatientAdmission patient, Vitals vital, ref PatientAlert patientAlert)
        {
            foreach (var patientDevice in patient.Devices)
            {
                if (patientDevice.DeviceId == vital.DeviceId)
                {
                    ValidateDeviceRange(vital, patientDevice, ref patientAlert);
                }
            }
        }

        private void ValidateDeviceRange(Vitals vital, Device device, ref PatientAlert patientAlert)
        {
            if (!CheckRange(device.MinInputValue, device.MaxInputValue, vital.Value))
            {
                patientAlert.CriticalAlerts.Add(new DeviceAlert
                {
                    DeviceId = device.DeviceId,
                    Value = vital.Value,
                    Message = "Device Malfunction : Value out of valid input range."
                });
                return;
            }
            ValidateAllLimits(vital, device, ref patientAlert);
        }

        private void ValidateAllLimits(Vitals vital, Device device, ref PatientAlert patientAlert)
        {
            foreach (var limit in device.Limits)
            {
                if (ValidateLimits(vital, limit, ref patientAlert))
                    return;
            }
        }

        private bool ValidateLimits(Vitals vital, Limits limit, ref PatientAlert patientAlert)
        {
            if (CheckRange(limit.MinValue, limit.MaxValue, vital.Value))
            {
                ValidateLimitType(vital, limit, ref patientAlert);
                return true;
            }

            return false;
        }

        private static void ValidateLimitType(Vitals vital, Limits limit, ref PatientAlert patientAlert)
        {
            if (limit.Type == LimitType.Critical)
                patientAlert.CriticalAlerts.Add(new DeviceAlert
                {
                    DeviceId = vital.DeviceId,
                    Message = limit.Message,
                    Value = vital.Value
                });
            else if (limit.Type == LimitType.Warning)
                patientAlert.WarningAlerts.Add(new DeviceAlert
                {
                    DeviceId = vital.DeviceId,
                    Message = limit.Message,
                    Value = vital.Value
                });
        }

        private bool CheckRange(double deviceMinInputValue, double deviceMaxInputValue, double vitalValue)
        {
            return (vitalValue >= deviceMinInputValue && vitalValue < deviceMaxInputValue);
        }
    }
}
