using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models.PatientAdmission
{
    [DataContract]
    public class PatientAdmission
    {
        [DataMember]
        public HospitalBed.HospitalBed Bed { get; set; }
        [DataMember]
        public string PatientId { get; set; }
        [DataMember]
        public string DoctorId { get; set; }
        [DataMember]
        public string Illness { get; set; }
        [DataMember]
        public string Diagnosis { get; set; }
        [DataMember]
        public string AdmissionTime { get; set; }
        [DataMember]
        public bool MuteAlert { get; set; }
        [DataMember]
        public List<Device.Device> Devices { get; set; }
    }
}
