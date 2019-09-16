using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts.AdmissionRepository;
using Contracts.AlertManager;
using Contracts.CustomDeviceRepository;
using Contracts.DeviceRepository;
using Contracts.HospitalBedRepository;
using Contracts.PatientRepository;
using Contracts.VitalsRepository;
using Controllers.AdditionalDoctorDataService;
using Controllers.AdditionalNurseDataService;
using Controllers.DoctorMonitoringService;
using Controllers.NurseMonitoringService;
using Controllers.VitalsDataService;
using DataAccessLayer.AdmissionRepositoryLib;
using DataAccessLayer.CustomDeviceRepoLib;
using DataAccessLayer.DeviceRepositoryLib;
using DataAccessLayer.HospitalBedRepoLib;
using DataAccessLayer.PatientRepositoryLib;
using DataAccessLayer.VitalsRepositoryLib;
using Microsoft.Practices.Unity.Configuration;
using Processor.AlertingUnitLib;
using Processor.ProcessingUnitLib;
using SharedResources.AlertMonitorList;
using SharedResources.VitalsMonitorList;
using Unity;

namespace Hosts.MonitoringServiceHost
{
    public static class MonitoringServiceHost
    {
        static void Main(string[] args)
        {
            UnityContainer _diContainer = new UnityContainer();
            _diContainer.LoadConfiguration();
            ProcessingUnit processor = _diContainer.Resolve<Processor.ProcessingUnitLib.ProcessingUnit>();
            Thread thread = new Thread(processor.Process);
            thread.Start();
            ServiceHost vitalsHost = new ServiceHost(typeof(VitalsDataService));
            vitalsHost.Open();
            Console.WriteLine("Vitals Service Hosted Successfully");
            ServiceHost doctorHost = new ServiceHost(typeof(DoctorMonitoringService));
            doctorHost.Open();
            Console.WriteLine("Doctor Monitoring Service Hosted Successfully");
            ServiceHost nurseHost = new ServiceHost(typeof(NurseMonitoringService));
            nurseHost.Open();
            Console.WriteLine("Nurse Monitoring Service Hosted Successfully");
            ServiceHost additionalNurseHost = new ServiceHost(typeof(AdditionalNurseDataService));
            additionalNurseHost.Open();
            Console.WriteLine("Additional Nurse Monitoring Data Service Hosted Successfully");
            ServiceHost additionalDoctorHost = new ServiceHost(typeof(AdditionalDoctorDataService));
            additionalDoctorHost.Open();
            Console.WriteLine("Additional Doctor Monitoring Data Service Hosted Successfully");
            Console.Read();
        }
    }
}
