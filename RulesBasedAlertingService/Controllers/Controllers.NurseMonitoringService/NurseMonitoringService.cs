using System.Collections.Generic;
using System.ServiceModel;
using Contracts.AdmissionRepository;
using Contracts.VitalsRepository;
using DataAccessLayer.AdmissionRepositoryLib;
using DataAccessLayer.CustomDeviceRepoLib;
using DataAccessLayer.DeviceRepositoryLib;
using DataAccessLayer.HospitalBedRepoLib;
using DataAccessLayer.PatientRepositoryLib;
using DataAccessLayer.VitalsRepositoryLib;
using Models.PatientAlert;
using Models.PatientVitals;
using Processor.AlertingUnitLib;
using SharedResources.AlertMonitorList;
using SharedResources.VitalsMonitorList;

namespace Controllers.NurseMonitoringService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class NurseMonitoringService : INurseMonitoringService
    {
        private readonly IVitalsRepository m_vitalsRepo;
        private readonly IAdmissionRepository m_admissionRepo;

        public NurseMonitoringService()
        {
            m_vitalsRepo = new VitalsRepository();
            m_admissionRepo = new AdmissionRepository(new PatientRepository(), new HospitalBedRepository(), new DeviceRepository(), new CustomDeviceRepository(new DeviceRepository()));
        }
        public void SubscribeToVitals(string patientId, string locationId)
        {
            ICallbackNurseMonitoringService callbackChannel =
                OperationContext.Current.GetCallbackChannel<ICallbackNurseMonitoringService>();
            VitalsMonitorList list = VitalsMonitorList.Instance;
            list.Subscribe(patientId, locationId, callbackChannel.ReceiveVitals);
        }

        public void UnsubscribeToVitals(string patientId, string locationId)
        {
            VitalsMonitorList list = VitalsMonitorList.Instance;
            list.Unsubscribe(patientId, locationId);
        }

        public void SubscribeToPatientAlerts(string locationId)
        {
            ICallbackNurseMonitoringService callbackChannel =
                OperationContext.Current.GetCallbackChannel<ICallbackNurseMonitoringService>();
            AlertMonitorList list = AlertMonitorList.Instance;
            list.Subscribe(locationId, callbackChannel.ReceiveAlerts);
        }

        public void UnsubscribeToPatientAlerts(string locationId)
        {
            AlertMonitorList list = AlertMonitorList.Instance;
            list.Unsubscribe(locationId);
        }

        public List<PatientVitals> ReadPreviousVitals(string patientId)
        {
            return m_vitalsRepo.ReadAllVitals(patientId);
        }

        public void AlertDoctor(PatientAlert alert)
        {
            AlertManager manager = new AlertManager(m_admissionRepo);
            var warningAlerts = alert.WarningAlerts;
            alert.CriticalAlerts.AddRange(warningAlerts);
            alert.WarningAlerts = new List<DeviceAlert>();
            manager.AlertUsers(alert);           
        }

        public void MuteAlerts(string patientId)
        {
            m_admissionRepo.MuteAlerts(patientId);
        }

        public void UnmuteAlerts(string patientId)
        {
            m_admissionRepo.UnmuteAlerts(patientId);
        }
    }
}