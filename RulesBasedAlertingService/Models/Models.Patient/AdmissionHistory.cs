using System;
using System.Runtime.Serialization;

namespace Models.Patient
{
    [DataContract]
    public class AdmissionHistory
    {
        [DataMember]
        public string Illness { get; set; }
        [DataMember]
        public string Diagnosis { get; set; }
        [DataMember]
        public string AdmissionDate { get; set; }
    }
}
