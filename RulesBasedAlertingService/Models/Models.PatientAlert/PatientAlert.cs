using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models.PatientAlert
{
    [DataContract]
    public class PatientAlert
    {
        [DataMember]
        public string PatientId { get; set; }
        [DataMember]
        public bool MuteAlert { get; set; }
        [DataMember]
        public List<DeviceAlert> CriticalAlerts { get; set; }
        [DataMember]
        public List<DeviceAlert> WarningAlerts { get; set; }
    }
}
