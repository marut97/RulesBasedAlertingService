using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contracts.HospitalBedRepository;
using DataAccessLayer.HospitalBedRepoLib;
using Models.HospitalBed;
using Utility.BedParser;

namespace Controllers.HospitalBedService
{
    public class HospitalBedRegistrationService : IHospitalBedRegistrationService
    {

        private readonly IHospitalBedRepository m_repository;

        public HospitalBedRegistrationService()
        {
            m_repository = new HospitalBedRepository();
        }

        public void RegisterNewBeds(HospitalBed beds)
        {
            m_repository.RegisterNew(beds);
        }

        public void UpdateBedOccupancy(HospitalBed bed)
        {
            m_repository.Update(bed);
        }

        public void DeleteBeds(HospitalBed bed)
        {
            m_repository.Delete(bed);
        }

        public List<HospitalBed> ReadAllBeds()
        {
            return m_repository.ReadAll();
        }


        //----
        public HospitalBed ReadBed(string bed)
        {
            HospitalBedParser parser = new HospitalBedParser(m_repository);
            return m_repository.Read(parser.ParseHospitalBed(bed));
        }
        
        public List<HospitalBed> ReadEmptyBeds(string id)
        {
            HospitalBedParser parser = new HospitalBedParser(m_repository);
            return parser.ReadEmptyBeds(id);
        }
    }
}