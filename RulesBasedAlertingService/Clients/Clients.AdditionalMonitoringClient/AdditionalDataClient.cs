using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clients.AdditionalMonitoringClient.AdditionalDoctorDataService;
using Clients.AdditionalMonitoringClient.AdditionalNurseDataService;

namespace Clients.AdditionalMonitoringClient
{
    class AdditionalDataClient
    {
        static void Main(string[] args)
        {
            var addDocClient1 = new AdditionalDoctorDataServiceClient();
            var patients = addDocClient1.ReadAllPatientRegistrations("100");

            foreach (var patient in patients)
            {
                Console.WriteLine(patient.PatientId);
            }



            var admissions = addDocClient1.ReadAllPatientAdmissionsAsync("100");
            while (!admissions.IsCompleted)
            {

            }

            foreach (var admission in admissions.Result)
            {
                Console.WriteLine(admission.PatientId);
            }


            var addNurseClient1 = new AdditionalNurseDataServiceClient();
            var patients1 = addNurseClient1.ReadAllPatientRegistrationsAsync("PIC-2F-2A");
            while (!patients1.IsCompleted) ;

            foreach (var patient in patients1.Result)
            {
                Console.WriteLine(patient.PatientId);
            }



            var admissions1 = addNurseClient1.ReadAllPatientAdmissionsAsync("PIC-2F-2A");
            while (!admissions1.IsCompleted)
            {
            }

            foreach (var admission in admissions1.Result)
            {
                Console.WriteLine(admission.PatientId);
            }

            Console.Read();
        }
    }
}
