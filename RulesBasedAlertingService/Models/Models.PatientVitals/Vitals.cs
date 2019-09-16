using System.Runtime.Serialization;

namespace Models.PatientVitals
{
    [DataContract]
    public class Vitals
    {
        [DataMember]
        public string DeviceId { get; set; }
        [DataMember]
        public double Value { get; set; }

        public override string ToString()
        {
            return DeviceId + "/" + Value;
        }
    }
}
