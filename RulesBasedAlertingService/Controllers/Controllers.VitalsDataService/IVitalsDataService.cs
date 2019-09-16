using System.ServiceModel;
using Models.PatientVitals;

namespace Controllers.VitalsDataService
{
    [ServiceContract]
    public interface IVitalsDataService
    {
        [OperationContract]
        void WriteVitals(PatientVitals vitals);
    }
}