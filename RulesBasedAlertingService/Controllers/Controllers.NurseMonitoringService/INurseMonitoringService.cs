using System.Collections.Generic;
using System.ServiceModel;
using Models.PatientAlert;
using Models.PatientVitals;

namespace Controllers.NurseMonitoringService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackNurseMonitoringService))]
    public interface INurseMonitoringService
    {
        [OperationContract(IsOneWay = true)]
        void SubscribeToVitals(string patientId, string locationId);

        [OperationContract(IsOneWay = true)]
        void UnsubscribeToVitals(string patientId, string locationId);

        [OperationContract(IsOneWay = true)]
        void SubscribeToPatientAlerts(string locationId);

        [OperationContract(IsOneWay = true)]
        void UnsubscribeToPatientAlerts(string locationId);

        [OperationContract(IsOneWay = false)]
        List<PatientVitals> ReadPreviousVitals(string patientId);

        [OperationContract(IsOneWay = true)]
        void AlertDoctor(PatientAlert alert);

        [OperationContract(IsOneWay = true)]
        void MuteAlerts(string patientId);

        [OperationContract(IsOneWay = true)]
        void UnmuteAlerts(string patientId);
    }

}
