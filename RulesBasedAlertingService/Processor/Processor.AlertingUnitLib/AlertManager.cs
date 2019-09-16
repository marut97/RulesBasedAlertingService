using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.AdmissionRepository;
using Contracts.AlertManager;
using Contracts.HospitalBedRepository;
using DataAccessLayer.AdmissionRepositoryLib;
using DataAccessLayer.HospitalBedRepoLib;
using DataAccessLayer.PatientRepositoryLib;
using Models.PatientAdmission;
using Models.PatientAlert;
using SharedResources.AlertMonitorList;

namespace Processor.AlertingUnitLib
{
    public class AlertManager : IAlertManager
    {
        private readonly IAdmissionRepository m_admissionRepository;

        public AlertManager(IAdmissionRepository admissionRepo)
        {
            m_admissionRepository = admissionRepo;
        }
        public void AlertUsers(PatientAlert alert)
        {
            AlertMonitorList subscribers = AlertMonitorList.Instance;
            PatientAdmission patient = m_admissionRepository.Read(alert.PatientId);
            if (patient.PatientId != alert.PatientId)
                return;
            string bedNo = patient.Bed.ToString();
            string roomNo = bedNo.Substring(0, bedNo.LastIndexOf('-'));
            TryInvoke(alert, subscribers.TryGetValue(roomNo));
            AlertNurseStations(alert, roomNo, subscribers);
            var func = subscribers.TryGetValue("Doctor" + patient.DoctorId);
            alert.WarningAlerts = new List<DeviceAlert>();
            TryInvoke(alert, func);
        }

        private void AlertNurseStations(PatientAlert alert, string roomNo, AlertMonitorList subscribers)
        {
            string wingNo = roomNo.Substring(0, roomNo.LastIndexOf('-'));
            TryInvoke(alert, subscribers.TryGetValue(wingNo));
            string floorNo = wingNo.Substring(0, wingNo.LastIndexOf('-'));
            TryInvoke(alert, subscribers.TryGetValue(floorNo));
            string campus = floorNo.Substring(0, floorNo.LastIndexOf('-'));
            TryInvoke(alert, subscribers.TryGetValue(campus));
        }

        private static void TryInvoke(PatientAlert alert, AlertMonitoringFunction func)
        {
            func?.Invoke(alert);
        }
    }
}
