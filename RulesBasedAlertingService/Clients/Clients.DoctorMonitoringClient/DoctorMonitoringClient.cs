using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Clients.DoctorMonitoringClient.DoctorMonitoringService;

namespace Clients.DoctorMonitoringClient
{
    public class DoctorMonitoringClient : IDoctorMonitoringServiceCallback
    {
        static void Main(string[] args)
        {
            DoctorMonitoringServiceClient client = new DoctorMonitoringServiceClient(new InstanceContext(new DoctorMonitoringClient()));
            Console.WriteLine("Service Client Running");
            client.SubscribeToVitals("111", "100");
            Console.Read();

        }

        public void ReceiveVitals(PatientVitals vitals)
        {
            Console.WriteLine(vitals.PatientId);
        }

        public void ReceiveAlerts(PatientAlert alert)
        {
            Console.WriteLine(alert.PatientId);
        }
    }
}
