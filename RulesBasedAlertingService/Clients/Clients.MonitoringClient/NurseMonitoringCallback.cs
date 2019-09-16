using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Clients.MonitoringClient.NurseMonitoringService;

namespace Clients.MonitoringClient
{
    [CallbackBehavior(UseSynchronizationContext = true)]
    class NurseMonitoringCallback : INurseMonitoringServiceCallback
    {
        public void ReceiveVitals(PatientVitals vitals)
        {
            Console.WriteLine(vitals.PatientId);
            foreach (var vital in vitals.Vitals)
            {
                Console.WriteLine(vital.DeviceId + " : " + vital.Value);
            }
        }

        public void ReceiveAlerts(PatientAlert alert)
        {
            Console.WriteLine(alert.PatientId);
            Console.WriteLine("*************");
            Console.WriteLine("Warning");
            foreach (var warningAlert in alert.WarningAlerts)
            {
                Console.WriteLine(warningAlert.DeviceId + " : " + warningAlert.Value + " : " + warningAlert.Message);
            }

            Console.WriteLine("*************");
            Console.WriteLine("Critical");
            foreach (var criticalAlert in alert.CriticalAlerts)
            {
                Console.WriteLine(criticalAlert.DeviceId + " : " + criticalAlert.Value + " : " + criticalAlert.Message);
            }

            Console.WriteLine("*************");
            Console.WriteLine("*************");
        }
    }
}
