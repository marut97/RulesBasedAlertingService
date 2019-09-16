using System;
using System.Runtime.Serialization;

namespace Models.Doctor
{
    [DataContract]
    [Flags]
    public enum DoctorStatus
    {
        [EnumMember]
        OnLeave,
        [EnumMember]
        OnBreak,
        [EnumMember]
        Active,
        [EnumMember]
        InSurgery
    }
}
