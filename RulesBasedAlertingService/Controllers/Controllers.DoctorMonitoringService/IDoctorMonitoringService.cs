using System.Collections.Generic;
using System.ServiceModel;
using Models.PatientAlert;
using Models.PatientVitals;


namespace Controllers.DoctorMonitoringService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackDoctorMonitoringService))]
    public interface IDoctorMonitoringService
    {
        [OperationContract(IsOneWay = false)]
        void SubscribeToVitals(string patientId, string doctorId);

        [OperationContract(IsOneWay = false)]
        void UnsubscribeToVitals(string patientId, string doctorId);

        [OperationContract(IsOneWay = false)]
        void SubscribeToPatientAlerts(string doctorId);

        [OperationContract(IsOneWay = false)]
        void UnsubscribeToPatientAlerts(string doctorId);

        [OperationContract(IsOneWay = false)]
        List<PatientVitals> ReadPreviousVitals(string patientId);

    }


}