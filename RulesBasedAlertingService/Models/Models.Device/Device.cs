using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models.Device
{
    [DataContract]
    public class Device
    {
        [DataMember]
        public string DeviceId { get; set; }
        [DataMember]
        public double MinInputValue { get; set; }
        [DataMember]
        public double MaxInputValue { get; set; }
        [DataMember]
        public List<Limits> Limits { get; set; }
    }
}
