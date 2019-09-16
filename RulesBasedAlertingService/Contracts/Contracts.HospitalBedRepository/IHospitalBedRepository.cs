using System.Collections.Generic;
using Models.HospitalBed;

namespace Contracts.HospitalBedRepository
{
    public interface IHospitalBedRepository
    {
        bool RegisterNew(HospitalBed beds);
        bool Update(HospitalBed bed);
        bool Delete(HospitalBed bed);
        List<HospitalBed> ReadOccupiedBedsInRoom(HospitalBed roomId);
        List<HospitalBed> ReadOccupiedBedsInWing(HospitalBed wingId);
        List<HospitalBed> ReadOccupiedBedsInFloor(HospitalBed floorId);
        List<HospitalBed> ReadOccupiedBedsInCampus(HospitalBed campusId);
        List<HospitalBed> ReadEmptyBedsInRoom(HospitalBed roomId);
        List<HospitalBed> ReadEmptyBedsInWing(HospitalBed wingId);
        List<HospitalBed> ReadEmptyBedsInFloor(HospitalBed floorId);
        List<HospitalBed> ReadEmptyBedsInCampus(HospitalBed campusId);
        List<HospitalBed> ReadAll();
        HospitalBed Read(HospitalBed bed);
        bool Exists(HospitalBed bed);
        HospitalBed ReadPatientsBed(string patientId);
    }
}
