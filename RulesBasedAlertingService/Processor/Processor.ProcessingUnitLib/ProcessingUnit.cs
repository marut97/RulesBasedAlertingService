using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.AdmissionRepository;
using Contracts.AlertManager;
using Contracts.VitalsRepository;
using DataAccessLayer.VitalsRepositoryLib;
using Models.PatientAlert;
using Models.PatientVitals;
using Processor.AlertingUnitLib;
using SharedResources.SharedQueue;

namespace Processor.ProcessingUnitLib
{
    public class ProcessingUnit
    {
        private readonly SharedQueue m_queue;
        private readonly IVitalsRepository m_vitalsRepository;
        private readonly VitalsRangeValidator m_validator;
        private readonly IAlertManager m_alertManager;
        private readonly IVitalsAlertManager m_vitalsManager;
        public ProcessingUnit(IVitalsRepository vitalsRepo, IAlertManager alertManager, IVitalsAlertManager vitalsManager, IAdmissionRepository admissionRepository)
        {
            m_queue = SharedQueue.Instance;
            m_vitalsRepository = vitalsRepo;
            m_validator = new VitalsRangeValidator(admissionRepository);
            m_alertManager = alertManager;
            m_vitalsManager = vitalsManager;
        }

        public void Process()
        {
            while (true)
            {
                if (!m_queue.IsEmpty())
                {
                    var vitals = m_queue.Dequeue();
                    m_vitalsManager.AlertUsers(vitals);
                    PatientAlert alert = m_validator.ValidateVitalsRange(vitals);
                    m_alertManager.AlertUsers(alert);
                    m_vitalsRepository.WriteVitals(vitals);
                }
            }
        }
    }
}
