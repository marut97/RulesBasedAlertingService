using System;
using System.Runtime.Serialization;

namespace Models.Device
{
    [DataContract]
    [Flags]
    public enum LimitType
    {
        [EnumMember]
        Normal,
        [EnumMember]
        Warning,
        [EnumMember]
        Critical
    }
}
