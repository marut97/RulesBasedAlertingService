using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.AdmissionRepository;
using Contracts.HospitalBedRepository;
using Models.HospitalBed;

namespace Utility.BedParser
{
    public class HospitalBedParser
    {
        private readonly IHospitalBedRepository m_bedRepo;
        public HospitalBedParser(IHospitalBedRepository hospitalBedRepository)
        {
            m_bedRepo = hospitalBedRepository;
        }
        public HospitalBed ParseHospitalBed(string bed)
        {
            HospitalBed hospitalBed = new HospitalBed();

            char[] spearator = { '-', ' ' };

            String[] bedInfo = bed.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

            ParseCampusFloor(bedInfo, ref hospitalBed);
            ParseWingRoom(bedInfo, ref hospitalBed);
            if (bedInfo.Count() > 4)
                hospitalBed.BedNumber = Convert.ToInt32(bedInfo[4]);

            return hospitalBed;
        }

        private static void ParseWingRoom(string[] bedInfo, ref HospitalBed hospitalBed)
        {
            if (bedInfo.Count() > 2)
                hospitalBed.Wing = bedInfo[2];
            if (bedInfo.Count() > 3)
                hospitalBed.RoomNumber = bedInfo[3];
        }

        private static void ParseCampusFloor(string[] bedInfo, ref HospitalBed hospitalBed)
        {
            if (bedInfo.Any())
                hospitalBed.Campus = bedInfo[0];
            if (bedInfo.Count() > 1)
                hospitalBed.Floor = bedInfo[1];
        }

        public List<HospitalBed> ReadEmptyBeds(string id)
        {
            HospitalBed hospitalBed = ParseHospitalBed(id);

            Dictionary<int, Func<HospitalBed, List<HospitalBed>>> bedmap = new Dictionary<int, Func<HospitalBed, List<HospitalBed>>>();
            bedmap[1] = m_bedRepo.ReadEmptyBedsInCampus;
            bedmap[2] = m_bedRepo.ReadEmptyBedsInFloor;
            bedmap[3] = m_bedRepo.ReadEmptyBedsInWing;
            bedmap[4] = m_bedRepo.ReadEmptyBedsInRoom;

            char[] separator = { '-', ' ' };

            String[] bedInfo = id.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var beds = bedmap[bedInfo.Count()](hospitalBed);
            bedmap.Clear();
            return beds;
        }

        public List<HospitalBed> ReadOccupiedBeds(string id)
        {
            HospitalBed hospitalBed = ParseHospitalBed(id);

            Dictionary<int, Func<HospitalBed, List<HospitalBed>>> bedmap = new Dictionary<int, Func<HospitalBed, List<HospitalBed>>>();
            bedmap[1] = m_bedRepo.ReadOccupiedBedsInCampus;
            bedmap[2] = m_bedRepo.ReadOccupiedBedsInFloor;
            bedmap[3] = m_bedRepo.ReadOccupiedBedsInWing;
            bedmap[4] = m_bedRepo.ReadOccupiedBedsInRoom;

            char[] separator = { '-', ' ' };

            String[] bedInfo = id.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var beds = bedmap[bedInfo.Count()](hospitalBed);
            bedmap.Clear();
            return beds;
        }
    }
}
