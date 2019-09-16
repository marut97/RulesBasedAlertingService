using System.Runtime.Serialization;

namespace Models.Device
{
    [DataContract]
    public class Limits
    {
        [DataMember]
        public double MinValue { get; set; }
        [DataMember]
        public double MaxValue { get; set; }
        [DataMember]
        public LimitType Type { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
