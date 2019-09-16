using System;
using System.ServiceModel;
using System.Threading;
using Contracts.AdmissionRepository;
using DataAccessLayer.AdmissionRepositoryLib;
using DataAccessLayer.CustomDeviceRepoLib;
using DataAccessLayer.DeviceRepositoryLib;
using DataAccessLayer.HospitalBedRepoLib;
using DataAccessLayer.PatientRepositoryLib;
using DataAccessLayer.VitalsRepositoryLib;
using Processor.AlertingUnitLib;
using Processor.ProcessingUnitLib;
using SharedResources.AlertMonitorList;
using SharedResources.VitalsMonitorList;

namespace Hosts.VitalsDataService
{
    public static class Host
    {
        static void Main(string[] args)
        {
            IAdmissionRepository admissionRepo = new AdmissionRepository(new PatientRepository(), new HospitalBedRepository(), new DeviceRepository(), new CustomDeviceRepository(new DeviceRepository()));
            ProcessingUnit processor = new ProcessingUnit(new VitalsRepository(), new AlertManager(admissionRepo),new VitalsAlertManager(), admissionRepo);
            AlertMonitorList list = AlertMonitorList.Instance;
            VitalsMonitorList vitalsList = VitalsMonitorList.Instance;
            Thread thread = new Thread(processor.Process);
            thread.Start();
            ServiceHost host = new ServiceHost(typeof(Controllers.VitalsDataService.VitalsDataService));
            host.Open();
            Console.WriteLine("Service Hosted Successfully");
            Console.Read();
        }
    }
}