using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models.Doctor
{
    [DataContract]
    public class Doctor
    {
        [DataMember]
        public string DoctorId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public long Phone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public DoctorStatus Status { get; set; }
        [DataMember]
        public HospitalDepartment Department { get; set; }
        [DataMember]
        public List<string> Patients { get; set; }
    }
}
