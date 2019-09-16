using System;
using System.Runtime.Serialization;

namespace Models.Doctor
{
    [DataContract]
    [Flags]
    public enum HospitalDepartment
    {
        [EnumMember]
        Paediatrics,
        [EnumMember]
        Cardiology,
        [EnumMember]
        Gynaecology,
        [EnumMember]
        Orthopaedic,
        [EnumMember]
        Nuerology
    }
}
