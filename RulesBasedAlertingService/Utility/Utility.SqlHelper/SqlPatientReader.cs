using Models.Patient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.SqlHelper
{
    public class SqlPatientReader
    {

        public void FetchPreviousAdmissions(string connectionString, ref List<Patient> patients)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommands.OpenConnection(con);
                foreach (var patient in patients)
                {
                    var previousAdmissionsQuery = String.Format("Select * FROM AdmissionHistory WHERE PatientId = '{0}'",
                            patient.PatientId);
                    SqlDataReader reader = SqlCommands.ExecuteRead(con, previousAdmissionsQuery);
                    patient.PreviousAdmissions = ParsePreviousAdmissions(reader);
                    reader.Close();
                }
            }
        }

        private List<AdmissionHistory> ParsePreviousAdmissions(SqlDataReader reader)
        {
            List<AdmissionHistory> history = new List<AdmissionHistory>();
            while (reader.Read())
            {
                AdmissionHistory admission = new AdmissionHistory();
                admission.AdmissionDate = reader["AdmissionDate"].ToString();
                admission.Diagnosis = reader["Diagnosis"].ToString();
                admission.Illness = reader["Illness"].ToString();
                history.Add(admission);
            }
            return history;
        }

        public List<Patient> FetchPatients(string connectionString, string patientQuery)
        {
            List<Patient> patients;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommands.OpenConnection(con);

                SqlDataReader reader = SqlCommands.ExecuteRead(con, patientQuery);
                patients = ParsePatients(reader);
                reader.Close();
            }
            return patients;
        }

        private List<Patient> ParsePatients(SqlDataReader reader)
        {
            List<Patient> patients = new List<Patient>();
            while (reader.Read())
            {
                Patient patient = new Patient();
                patient.PatientId = reader["PatientId"].ToString();
                patient.Address = reader["PatientAddress"].ToString();
                patient.BloodType = (BloodType)Convert.ToInt32(reader["BloodType"]);
                patient.Email = reader["Email"].ToString();
                patient.EmergencyContact = Convert.ToInt64(reader["EmergencyContact"]);
                patient.Name = reader["PatientName"].ToString();
                patient.Phone = Convert.ToInt64(reader["Phone"]);
                patients.Add(patient);
            }
            return patients;
        }

    }
}
