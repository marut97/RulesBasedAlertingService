using Models.Device;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.SqlHelper
{
    public class SqlCustomDeviceReader
    {
        public List<Limits> FetchCustomLimits(string connectionString, string deviceId, string patientId)
        {
            List<Limits> limits;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommands.OpenConnection(con);
                string limitsQuery = String.Format("Select * FROM CustomLimits WHERE PatientId = '{0}' and DeviceId='{1}' order by MinValue ASC", patientId, deviceId);
                SqlDataReader reader = SqlCommands.ExecuteRead(con, limitsQuery);
                SqlDeviceReader deviceReader = new SqlDeviceReader();
                limits = deviceReader.ParseLimits(reader);
                reader.Close();
            }
            return limits;
        }
    }
}
