using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Models.PatientAlert;
using Models.PatientVitals;

namespace Controllers.DoctorMonitoringService
{
    public interface ICallbackDoctorMonitoringService
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveVitals(PatientVitals vitals);

        [OperationContract(IsOneWay = true)]
        void ReceiveAlerts(PatientAlert alert);
    }
}