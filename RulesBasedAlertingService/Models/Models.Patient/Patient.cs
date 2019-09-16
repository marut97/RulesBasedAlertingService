using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models.Patient
{
    [DataContract]
    public class Patient
    {
        [DataMember]
        public string PatientId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public long Phone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public long EmergencyContact { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public BloodType BloodType { get; set; }
        [DataMember]
        public List<AdmissionHistory> PreviousAdmissions { get; set; }
    }
}
