using System.Collections.Generic;
using System.ServiceModel;
using Contracts.VitalsRepository;
using DataAccessLayer.VitalsRepositoryLib;
using Models.PatientAlert;
using Models.PatientVitals;
using SharedResources.AlertMonitorList;
using SharedResources.VitalsMonitorList;

namespace Controllers.DoctorMonitoringService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class DoctorMonitoringService : IDoctorMonitoringService
    {
        private readonly IVitalsRepository m_vitalsRepo = new VitalsRepository();

        public void SubscribeToVitals(string patientId, string doctorId)
        {
            ICallbackDoctorMonitoringService callbackChannel = OperationContext.Current.GetCallbackChannel<ICallbackDoctorMonitoringService>();
            VitalsMonitorList vitalsList = VitalsMonitorList.Instance;
            vitalsList.Subscribe(patientId, doctorId, callbackChannel.ReceiveVitals);
        }

        public void UnsubscribeToVitals(string patientId, string doctorId)
        {
            VitalsMonitorList vitalsList = VitalsMonitorList.Instance;
            vitalsList.Unsubscribe(patientId, doctorId);
        }

        public void SubscribeToPatientAlerts(string doctorId)
        {
            ICallbackDoctorMonitoringService callbackChannel = OperationContext.Current.GetCallbackChannel<ICallbackDoctorMonitoringService>();
            AlertMonitorList alertList = AlertMonitorList.Instance;
            alertList.Subscribe(doctorId, callbackChannel.ReceiveAlerts);
        }
        public void UnsubscribeToPatientAlerts(string doctorId)
        {
            AlertMonitorList alertList = AlertMonitorList.Instance;
            alertList.Unsubscribe(doctorId);
        }

        public List<PatientVitals> ReadPreviousVitals(string patientId)
        {
            return m_vitalsRepo.ReadAllVitals(patientId);
        }
    }
}