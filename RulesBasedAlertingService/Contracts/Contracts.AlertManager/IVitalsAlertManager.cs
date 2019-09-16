using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.PatientVitals;

namespace Contracts.AlertManager
{
    public interface IVitalsAlertManager
    {
        void AlertUsers(PatientVitals vitals);
    }
}
