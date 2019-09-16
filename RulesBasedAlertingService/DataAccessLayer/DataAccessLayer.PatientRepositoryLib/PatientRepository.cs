using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.PatientRepository;
using Models.Patient;
using Utility.SqlHelper;

namespace DataAccessLayer.PatientRepositoryLib
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string conString;
        private readonly SqlPatientReader patientReader;
        public PatientRepository(bool test = false)
        {
            if (test)
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB.Test;Integrated Security=True";
            else
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB;Integrated Security=True";
            patientReader = new SqlPatientReader();
        }

        public bool AddAdmissionHistory(string patientId, AdmissionHistory history)
        {
            var query = string.Format("Insert into AdmissionHistory values('{0}','{1}','{2}','{3}')", patientId, history.Illness, history.Diagnosis, history.AdmissionDate);
            return SqlCommands.ExecuteCommand(query, conString);
        }

        public bool Delete(string patientId)
        {
            var query = string.Format("DELETE FROM PatientRegistration WHERE PatientId='{0}'", patientId);
            return SqlCommands.ExecuteCommand(query, conString);
        }

        public bool Exists(string patientId)
        {
            var query = string.Format("Select PatientID FROM PatientRegistration WHERE PatientId='{0}'", patientId);
            return SqlCommands.Exists(conString, query);
        }

        public Patient Read(string patientId)
        {
            var query = string.Format("Select * FROM PatientRegistration WHERE PatientId='{0}'", patientId);
            var patient = patientReader.FetchPatients(conString, query);

            if (patient.Any())
            {
                patientReader.FetchPreviousAdmissions(conString, ref patient);
                return patient.First();
            }

            return new Patient();
        }


        public List<Patient> ReadAll()
        {

            var query = "Select * FROM PatientRegistration";
            var patient = patientReader.FetchPatients(conString, query);

            if (patient.Any())
            {
                patientReader.FetchPreviousAdmissions(conString, ref patient);
                return patient;

            }

            return new List<Patient>();
        }



        public bool RegisterNew(Patient patient)
        {
            var query = string.Format("Insert into PatientRegistration values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", patient.PatientId, patient.Name, patient.Phone, patient.Address, patient.EmergencyContact, patient.Email, (int)patient.BloodType);

            if (SqlCommands.ExecuteCommand(query, conString))
            {
                return AddPreviousAdmissionHistory(patient);
            }

            return false;

        }

        private bool AddPreviousAdmissionHistory(Patient patient)
        {
            foreach (AdmissionHistory admission in patient.PreviousAdmissions)
            {
                string query = string.Format("Insert into AdmissionHistory values('{0}','{1}','{2}','{3}')", patient.PatientId, admission.Illness, admission.Diagnosis, admission.AdmissionDate);
                if (!SqlCommands.ExecuteCommand(query, conString))
                {
                    return false;
                }
            }
            return true;
        }


        public bool Update(Patient patient)
        {
            string query;
            if (!DeleteAdmissionHistory(patient)) return false;

            query = string.Format("UPDATE PatientRegistration SET PatientName='{0}',Phone='{1}',PatientAddress='{2}',EmergencyContact='{3}',Email='{4}',BloodType='{5}' WHERE PatientId='{6}'", patient.Name, patient.Phone, patient.Address, patient.EmergencyContact, patient.Email, (int)patient.BloodType, patient.PatientId);
            if (SqlCommands.ExecuteCommand(query, conString))
            {
                return AddPreviousAdmissionHistory(patient);
            }

            return false;
        }

        private bool DeleteAdmissionHistory(Patient patient)
        {
            var query = String.Format("Select * FROM AdmissionHistory WHERE PatientId = '{0}'",
                patient.PatientId);
            if (SqlCommands.Exists(conString, query))
            {
                query = string.Format("DELETE FROM AdmissionHistory WHERE PatientId='{0}'", patient.PatientId);
                if (!SqlCommands.ExecuteCommand(query, conString))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
