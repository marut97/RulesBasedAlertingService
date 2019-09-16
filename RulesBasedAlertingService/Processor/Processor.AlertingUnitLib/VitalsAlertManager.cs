using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.AdmissionRepository;
using Contracts.AlertManager;
using DataAccessLayer.AdmissionRepositoryLib;
using DataAccessLayer.HospitalBedRepoLib;
using DataAccessLayer.PatientRepositoryLib;
using Models.PatientAdmission;
using Models.PatientAlert;
using Models.PatientVitals;
using SharedResources.VitalsMonitorList;


namespace Processor.AlertingUnitLib
{
    public class VitalsAlertManager : IVitalsAlertManager
    {
        public void AlertUsers(PatientVitals vitals)
        {
            VitalsMonitorList subscribers = VitalsMonitorList.Instance;
            var subscriberList = subscribers.TryGetValue(vitals.PatientId);
            foreach (var func in subscriberList)
            {
                func.Invoke(vitals);
            }
        }
    }
}
