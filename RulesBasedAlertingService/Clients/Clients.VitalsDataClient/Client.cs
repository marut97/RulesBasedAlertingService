using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clients.VitalsDataClient.VitalsDataService;
using Models.PatientVitals;


namespace Clients.VitalsDataClient
{
    public static class Client
    {
        static void Main(string[] args)
        {
            VitalsDataServiceClient client = new VitalsDataServiceClient();
            PatientVitals vitals = new PatientVitals();
            vitals.PatientId = "111";
            vitals.Vitals = new List<Vitals> {new Vitals {DeviceId = "233", Value = 222.2}};
            client.WriteVitals(vitals);
            Console.WriteLine("Completed");
        }
    }
}
