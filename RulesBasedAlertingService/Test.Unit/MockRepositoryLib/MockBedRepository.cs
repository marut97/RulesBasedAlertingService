using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.HospitalBedRepository;
using Models.HospitalBed;

namespace MockRepositoryLib
{
    public class MockBedRepository : IHospitalBedRepository
    {
        public bool RegisterNew(HospitalBed beds)
        {
            throw new NotImplementedException();
        }

        public bool Update(HospitalBed bed)
        {
            throw new NotImplementedException();
        }

        public bool Delete(HospitalBed bed)
        {
            throw new NotImplementedException();
        }

        public List<HospitalBed> ReadOccupiedBedsInRoom(HospitalBed roomId)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            roomId.BedNumber = 1;
            roomId.Occupancy = "111";
            beds.Add(roomId);
            roomId.BedNumber = 2;
            roomId.Occupancy = "222";
            beds.Add(roomId);
            roomId.BedNumber = 3;
            roomId.Occupancy = "333";
            beds.Add(roomId);
            roomId.BedNumber = 4;
            roomId.Occupancy = "444";
            beds.Add(roomId);
            return beds;
        }

        public List<HospitalBed> ReadOccupiedBedsInWing(HospitalBed wingId)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            wingId.RoomNumber = "11";
            wingId.BedNumber = 1;
            wingId.Occupancy = "111";
            beds.Add(wingId);
            wingId.RoomNumber = "22";
            wingId.BedNumber = 2;
            wingId.Occupancy = "222";
            beds.Add(wingId);
            wingId.RoomNumber = "33";
            wingId.BedNumber = 3;
            wingId.Occupancy = "333";
            beds.Add(wingId);
            return beds;
        }

        public List<HospitalBed> ReadOccupiedBedsInFloor(HospitalBed floorId)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            floorId.Wing = "1A";
            floorId.RoomNumber = "11";
            floorId.BedNumber = 1;
            floorId.Occupancy = "111";
            beds.Add(floorId);
            floorId.Wing = "2A";
            floorId.RoomNumber = "22";
            floorId.BedNumber = 2;
            floorId.Occupancy = "222";
            beds.Add(floorId);
            return beds;
        }

        public List<HospitalBed> ReadOccupiedBedsInCampus(HospitalBed campusId)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            campusId.Floor = "1F";
            campusId.Wing = "1A";
            campusId.RoomNumber = "11";
            campusId.BedNumber = 1;
            campusId.Occupancy = "111";
            beds.Add(campusId);
            return beds;
        }

        public List<HospitalBed> ReadEmptyBedsInRoom(HospitalBed roomId)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            roomId.BedNumber = 1;
            beds.Add(roomId);
            roomId.BedNumber = 2;
            beds.Add(roomId);
            roomId.BedNumber = 3;
            beds.Add(roomId);
            roomId.BedNumber = 4;
            beds.Add(roomId);
            return beds;
        }

        public List<HospitalBed> ReadEmptyBedsInWing(HospitalBed wingId)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            wingId.RoomNumber = "11";
            wingId.BedNumber = 1;
            beds.Add(wingId);
            wingId.RoomNumber = "22";
            wingId.BedNumber = 2;
            beds.Add(wingId);
            wingId.RoomNumber = "33";
            wingId.BedNumber = 3;
            beds.Add(wingId);
            return beds;
        }

        public List<HospitalBed> ReadEmptyBedsInFloor(HospitalBed floorId)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            floorId.Wing = "1A";
            floorId.RoomNumber = "11";
            floorId.BedNumber = 1;
            beds.Add(floorId);
            floorId.Wing = "2A";
            floorId.RoomNumber = "22";
            floorId.BedNumber = 2;
            beds.Add(floorId);
            return beds;
        }

        public List<HospitalBed> ReadEmptyBedsInCampus(HospitalBed campusId)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            campusId.Floor = "1F";
            campusId.Wing = "1A";
            campusId.RoomNumber = "11";
            campusId.BedNumber = 1;
            beds.Add(campusId);
            return beds;
        }

        public List<HospitalBed> ReadAll()
        {
            throw new NotImplementedException();
        }

        public HospitalBed Read(HospitalBed bed)
        {
            throw new NotImplementedException();
        }

        public bool Exists(HospitalBed bed)
        {
            throw new NotImplementedException();
        }

        public HospitalBed ReadPatientsBed(string patientId)
        {
            throw new NotImplementedException();
        }
    }
}
