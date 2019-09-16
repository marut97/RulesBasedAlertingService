using Models.Device;
using Models.PatientAdmission;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.SqlHelper
{
    public class SqlPatientAdmissionReader
    {

        public List<PatientAdmission> FetchPatientAdmissions(string connectionString, string admissionQuery)
        {
            List<PatientAdmission> admissions;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommands.OpenConnection(con);

                using (SqlDataReader reader = SqlCommands.ExecuteRead(con, admissionQuery))
                {
                    admissions = ParseAdmissions(reader);
                }
            }

            return admissions;
        }


        private List<PatientAdmission> ParseAdmissions(SqlDataReader reader)
        {
            List<PatientAdmission> admissions = new List<PatientAdmission>();
            while (reader.Read())
            {
                PatientAdmission admission = new PatientAdmission();
                admission.AdmissionTime = (reader["AdmissionDate"]).ToString();
                admission.Diagnosis = reader["Diagnosis"].ToString();
                admission.DoctorId = reader["DoctorId"].ToString();
                admission.Illness = reader["Illness"].ToString();
                admission.PatientId = reader["PatientId"].ToString();
                admission.MuteAlert = Convert.ToBoolean(reader["MuteAlert"]);
                admissions.Add(admission);
            }
            return admissions;

        }

        public void FetchPatientsDevices(string connectionString, ref List<PatientAdmission> patientsAdmissions)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommands.OpenConnection(con);
                foreach (var admission in patientsAdmissions)
                {
                    var previousAdmissionsQuery = String.Format("Select * FROM PatientDevices WHERE PatientId = '{0}'", admission.PatientId);
                    SqlDataReader reader = SqlCommands.ExecuteRead(con, previousAdmissionsQuery);
                    admission.Devices = ParsePatientsDevices(reader);
                    reader.Close();
                }
            }
        }

        private List<Device> ParsePatientsDevices(SqlDataReader reader)
        {
            List<Device> devices = new List<Device>();
            while (reader.Read())
            {
                Device device = new Device();
                device.DeviceId = reader["DeviceId"].ToString();
                bool custom = Convert.ToBoolean(reader["CustomLimits"]);

                if (custom)
                {
                    device.Limits = new List<Limits> { new Limits() };
                }
                devices.Add(device);
            }
            return devices;
        }
    }
}
