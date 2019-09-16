using Models.Doctor;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.SqlHelper
{
    public class SqlDoctorReader
    {

        public List<Doctor> FetchDoctors(string conString, string query)
        {
            List<Doctor> doctorList;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommands.OpenConnection(con);
                SqlDataReader reader = SqlCommands.ExecuteRead(con, query);
                doctorList = ParseDoctors(reader);

            }
            FetchDoctorsPatients(conString, ref doctorList);
            return doctorList;
        }

        public void FetchDoctorsPatients(string connectionString, ref List<Doctor> doctors)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommands.OpenConnection(con);
                foreach (var doctor in doctors)
                {
                    var previousAdmissionsQuery = String.Format("Select * FROM PatientAdmission WHERE DoctorId = '{0}'", doctor.DoctorId);
                    SqlDataReader reader = SqlCommands.ExecuteRead(con, previousAdmissionsQuery);
                    doctor.Patients = ParsePatientIds(reader);
                    reader.Close();
                }
            }
        }

        private List<Doctor> ParseDoctors(SqlDataReader reader)
        {
            List<Doctor> doctors = new List<Doctor>();
            while (reader.Read())
            {
                Doctor doctor = new Doctor();
                doctor.DoctorId = reader["DoctorId"].ToString();
                doctor.Name = reader["DoctorName"].ToString();
                doctor.Address = reader["DoctorAddress"].ToString();
                doctor.Department = (HospitalDepartment)Convert.ToInt32(reader["Department"]);
                doctor.Email = reader["EmailId"].ToString();
                doctor.Phone = Convert.ToInt64(reader["Phone"]);
                doctor.Status = (DoctorStatus)Convert.ToInt32(reader["DoctorStatus"]);
                doctors.Add(doctor);
            }
            return doctors;
        }

        private List<string> ParsePatientIds(SqlDataReader reader)
        {
            List<string> patientIds = new List<string>();
            while (reader.Read())
            {
                patientIds.Add(reader["PatientId"].ToString());
            }

            return patientIds;
        }
    }
}
