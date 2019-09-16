using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models.PatientVitals
{
    [DataContract]
    public class PatientVitals
    {
        [DataMember]
        public string PatientId { get; set; }

        [DataMember]
        public List<Vitals> Vitals { get; set; }

        public override string ToString()
        {
            string vitals = PatientId + "/" + Vitals.Count + "/";
            foreach (Vitals i in Vitals)
                vitals = string.Format("{0}{1}", vitals, (i + "/"));
            return vitals;
        }
    }
}
