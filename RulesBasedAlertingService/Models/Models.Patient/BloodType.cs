using System;
using System.Runtime.Serialization;

namespace Models.Patient
{
    [DataContract]
    [Flags]
    public enum BloodType
    {
        [EnumMember]
        APositive,
        [EnumMember]
        ANegative,
        [EnumMember]
        BPositive,
        [EnumMember]
        BNegative,
        [EnumMember]
        ABPositive,
        [EnumMember]
        ABNegative,
        [EnumMember]
        OPositive,
        [EnumMember]
        ONegative

    }
}
