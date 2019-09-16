using System.Runtime.Serialization;

namespace Models.HospitalBed
{
    [DataContract]
    public class HospitalBed
    {
        [DataMember]
        public string Campus { get; set; }
        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string Wing { get; set; }
        [DataMember]
        public string RoomNumber { get; set; }
        [DataMember]
        public int BedNumber { get; set; }
        [DataMember]
        public string Occupancy { get; set; }

        //Converted to string in the format "{Campus}-{Floor}-{Wing}-{RoomNumber}-{BedNumber}"
        public override string ToString()
        {
            string bed = Campus + "-";
            bed += Floor.ToString() + "-";
            bed += Wing + "-";
            bed += RoomNumber.ToString() + "-" + BedNumber.ToString();
            return bed;
        }
    }
}
