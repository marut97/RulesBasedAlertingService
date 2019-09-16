using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.HospitalBedRepository;
using Models.HospitalBed;
using Utility.SqlHelper;

namespace DataAccessLayer.HospitalBedRepoLib
{
    public class HospitalBedRepository : IHospitalBedRepository
    {
        private readonly SqlHospitalBedReader bedReader;
        private readonly string conString;

        public HospitalBedRepository(bool test = false)
        {
            if (test)
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB.Test;Integrated Security=True";
            else
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB;Integrated Security=True";
            bedReader = new SqlHospitalBedReader();

        }
        public bool RegisterNew(HospitalBed beds)
        {
            var query = string.Format("SELECT TOP(1) * FROM HospitalBedRepository WHERE Campus='{0}' AND FloorNo='{1}' AND Wing='{2}' AND Room='{3}' order by BedNo DESC",
                beds.Campus, beds.Floor, beds.Wing, beds.RoomNumber);
            List<HospitalBed> bedList = bedReader.FetchBedNumbers(conString, query);
            InitializeBed(ref bedList, beds);
            for (int i = 0; i < beds.BedNumber; i++)
            {
                bedList.First().BedNumber++;
                var newBedQuery = string.Format("Insert into HospitalBedRepository values ('{0}','{1}','{2}','{3}','{4}',NULL)",beds.Campus, beds.Floor, beds.Wing, beds.RoomNumber, bedList.First().BedNumber);
                if (!SqlCommands.ExecuteCommand(newBedQuery, conString))
                    return false;
            }

            return true;
        }

        private void InitializeBed(ref List<HospitalBed> bedList, HospitalBed bed)
        {
            if (!bedList.Any())
            {
                bedList.Add(new HospitalBed
                {
                    BedNumber = 0,
                    Campus = bed.Campus,
                    Floor = bed.Floor,
                    Occupancy = bed.Occupancy,
                    RoomNumber = bed.RoomNumber,
                    Wing = bed.Wing
                });
            }
        }

        public bool Update(HospitalBed bed)
        {
            string query;
            if (bed.Occupancy.Any())
            {
                query = string.Format("UPDATE HospitalBedRepository SET PatientId='{0}' WHERE Campus='{1}' AND FloorNo='{2}' AND Wing='{3}' AND Room='{4}' AND BedNo='{5}'",bed.Occupancy, bed.Campus, bed.Floor, bed.Wing, bed.RoomNumber, bed.BedNumber);
            }
            else
            {
                query = string.Format("UPDATE HospitalBedRepository SET PatientId=NULL WHERE Campus='{0}' AND FloorNo='{1}' AND Wing='{2}' AND Room='{3}' AND BedNo='{4}'", bed.Campus, bed.Floor, bed.Wing, bed.RoomNumber, bed.BedNumber);
            }
            return SqlCommands.ExecuteCommand(query, conString);
        }

        public bool Delete(HospitalBed bed)
        {
            var emptyBeds = ReadEmptyBedsInRoom(bed);
            if (emptyBeds.Count >= bed.BedNumber)
            {
                var query = string.Format(
                    "DELETE TOP({0}) FROM HospitalBedRepository WHERE Campus='{1}' AND FloorNo='{2}' AND Wing='{3}' AND Room='{4}' AND PatientId is NULL ", bed.BedNumber, bed.Campus, bed.Floor, bed.Wing, bed.RoomNumber);
                return SqlCommands.ExecuteCommand(query, conString);
            }

            return false;
        }

        public List<HospitalBed> ReadOccupiedBedsInRoom(HospitalBed roomId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND FloorNo='{1}' AND Wing='{2}' AND Room='{3}' AND PatientId IS NOT NULL", roomId.Campus, roomId.Floor, roomId.Wing, roomId.RoomNumber);
            return bedReader.FetchBedNumbers(conString, query);
        }

        public List<HospitalBed> ReadOccupiedBedsInWing(HospitalBed wingId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND FloorNo='{1}' AND Wing='{2}' AND PatientId IS NOT NULL",
                wingId.Campus, wingId.Floor, wingId.Wing);
            return bedReader.FetchBedNumbers(conString, query);
        }

        public List<HospitalBed> ReadOccupiedBedsInFloor(HospitalBed floorId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND FloorNo='{1}' AND PatientId IS NOT NULL",
                floorId.Campus, floorId.Floor);
            return bedReader.FetchBedNumbers(conString, query);
        }

        public List<HospitalBed> ReadOccupiedBedsInCampus(HospitalBed campusId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND PatientId IS NOT NULL", campusId.Campus);
            return bedReader.FetchBedNumbers(conString, query);
        }

        public List<HospitalBed> ReadEmptyBedsInRoom(HospitalBed roomId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND FloorNo='{1}' AND Wing='{2}' AND Room='{3}' AND PatientId IS NULL",
                roomId.Campus, roomId.Floor, roomId.Wing, roomId.RoomNumber);
            return bedReader.FetchBedNumbers(conString, query);
        }

        public List<HospitalBed> ReadEmptyBedsInWing(HospitalBed wingId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND FloorNo='{1}' AND Wing='{2}' AND PatientId IS NULL",
                wingId.Campus, wingId.Floor, wingId.Wing);
            return bedReader.FetchBedNumbers(conString, query);
        }

        public List<HospitalBed> ReadEmptyBedsInFloor(HospitalBed floorId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND FloorNo='{1}' AND PatientId IS NULL",
                floorId.Campus, floorId.Floor);
            return bedReader.FetchBedNumbers(conString, query);
        }

        public List<HospitalBed> ReadEmptyBedsInCampus(HospitalBed campusId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND PatientId IS NULL", campusId.Campus);
            return bedReader.FetchBedNumbers(conString, query);
        }

        public List<HospitalBed> ReadAll()
        {
            var query = "SELECT * FROM HospitalBedRepository";
            return bedReader.FetchBedNumbers(conString, query);
        }

        public bool Exists(HospitalBed bed)
        {
            var query =
                $"SELECT * FROM HospitalBedRepository WHERE Campus='{bed.Campus}' AND FloorNo='{bed.Floor}' AND Wing='{bed.Wing}' AND Room='{bed.RoomNumber}'";
            var beds = bedReader.FetchBedNumbers(conString, query);
            return beds.Any();
        }

        public HospitalBed Read(HospitalBed bed)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE Campus='{0}' AND FloorNo='{1}' AND Wing='{2}' AND Room='{3}' AND BedNo='{4}'", bed.Campus, bed.Floor, bed.Wing, bed.RoomNumber, bed.BedNumber);
            var beds = bedReader.FetchBedNumbers(conString, query);
            if (beds.Any())
                return beds.First();
            return new HospitalBed();
        }

        public HospitalBed ReadPatientsBed(string patientId)
        {
            var query = string.Format("SELECT * FROM HospitalBedRepository WHERE PatientId='{0}'", patientId);
            var beds = bedReader.FetchBedNumbers(conString, query);
            if (beds.Any())
                return beds.First();
            return new HospitalBed();
        }
    }
}
