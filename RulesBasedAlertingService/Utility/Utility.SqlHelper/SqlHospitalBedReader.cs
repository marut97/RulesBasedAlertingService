using Models.HospitalBed;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.SqlHelper
{
    public class SqlHospitalBedReader
    {
        public List<HospitalBed> FetchBedNumbers(string conString, string query)
        {
            List<HospitalBed> bedList;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommands.OpenConnection(con);
                using (SqlDataReader reader = SqlCommands.ExecuteRead(con, query))
                {
                    bedList = ParseHospitalBeds(reader);
                }
            }

            return bedList;
        }

        private List<HospitalBed> ParseHospitalBeds(SqlDataReader reader)
        {
            List<HospitalBed> beds = new List<HospitalBed>();
            while (reader.Read())
            {
                HospitalBed bed = new HospitalBed();
                bed.Campus = reader["Campus"].ToString();
                bed.Floor = reader["FloorNo"].ToString();
                bed.Wing = reader["Wing"].ToString();
                bed.RoomNumber = reader["Room"].ToString();
                bed.BedNumber = Convert.ToInt32(reader["BedNo"]);
                bed.Occupancy = reader["PatientId"].ToString();
                beds.Add(bed);
            }
            return beds;
        }
    }
}
