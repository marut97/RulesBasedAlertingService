using System.ServiceModel;
using Models.PatientAlert;
using Models.PatientVitals;

namespace Controllers.NurseMonitoringService
{
    public interface ICallbackNurseMonitoringService
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveVitals(PatientVitals vitals);

        [OperationContract(IsOneWay = true)]
        void ReceiveAlerts(PatientAlert alert);
    }
}
