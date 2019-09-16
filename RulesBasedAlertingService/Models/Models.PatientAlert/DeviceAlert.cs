using System.Runtime.Serialization;

namespace Models.PatientAlert
{
    [DataContract]
    public class DeviceAlert
    {
        [DataMember]
        public string DeviceId { get; set; }
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
