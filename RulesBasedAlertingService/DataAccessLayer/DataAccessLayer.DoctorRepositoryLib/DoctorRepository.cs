using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DoctorRepository;
using Models.Doctor;
using Utility.SqlHelper;

namespace DataAccessLayer.DoctorRepositoryLib
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly string conString;
        private readonly SqlDoctorReader doctorReader;

        public DoctorRepository(bool test = false)
        {
            if (test)
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB.Test;Integrated Security=True";
            else
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB;Integrated Security=True";
            doctorReader = new SqlDoctorReader();
        }

        public bool RegisterNew(Doctor doctor)
        {
            var query = string.Format("Insert into DoctorRepository values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", doctor.DoctorId, doctor.Name, (int)doctor.Status, doctor.Phone, doctor.Address, (int)doctor.Department, doctor.Email);
            return (SqlCommands.ExecuteCommand(query, conString));
        }

        public bool Update(Doctor doctor)
        {
            var query = string.Format("UPDATE DoctorRepository SET DoctorName='{0}', DoctorStatus='{1}', Phone='{2}',DoctorAddress='{3}',Department='{4}',EmailId='{5}' WHERE DoctorId='{6}'", doctor.Name, (int)doctor.Status, doctor.Phone, doctor.Address, (int)doctor.Department, doctor.Email, doctor.DoctorId);
            return (SqlCommands.ExecuteCommand(query, conString));
        }

        public bool Delete(string doctorId)
        {
            var query = string.Format("DELETE FROM DoctorRepository WHERE DoctorId='{0}'", doctorId);
            return SqlCommands.ExecuteCommand(query, conString);
        }

        public List<Doctor> ReadAll()
        {
            string query = "SELECT * FROM DoctorRepository";
            return doctorReader.FetchDoctors(conString, query);
        }

        public Doctor Read(string doctorId)
        {
            string query = string.Format("SELECT * FROM DoctorRepository WHERE DoctorId='{0}'", doctorId);
            var doctors = doctorReader.FetchDoctors(conString, query);
            if (doctors.Any())
                return doctors.First();
            return new Doctor();
        }

        public bool Exists(string doctorId)
        {
            string query = string.Format("SELECT * FROM DoctorRepository WHERE DoctorId='{0}'", doctorId);
            return doctorReader.FetchDoctors(conString, query).Any();
        }

        public bool UpdateStatus(string doctorId, DoctorStatus status)
        {
            if (Exists(doctorId))
            {
                Doctor doctor = Read(doctorId);
                var query = string.Format("UPDATE DoctorRepository SET DoctorName='{0}', DoctorStatus='{1}', Phone='{2}',DoctorAddress='{3}',Department='{4}',EmailId='{5}' WHERE DoctorId='{6}'", doctor.Name, (int)status, doctor.Phone, doctor.Address, (int)doctor.Department, doctor.Email, doctor.DoctorId);
                return (SqlCommands.ExecuteCommand(query, conString));
            }
            return false;
        }

        public List<Doctor> ReadDoctorsInDepartment(HospitalDepartment department)
        {
            string query = string.Format("SELECT * FROM DoctorRepository WHERE Department={0}", (int)department);
            return doctorReader.FetchDoctors(conString, query);
        }
    }
}
