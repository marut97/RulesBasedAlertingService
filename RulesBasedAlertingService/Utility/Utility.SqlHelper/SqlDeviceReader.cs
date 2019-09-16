using Models.Device;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.SqlHelper
{
    public class SqlDeviceReader
    {

        public List<Device> FetchDevices(string connectionString, string deviceQuery)
        {
            List<Device> devices;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommands.OpenConnection(con);

                SqlDataReader reader = SqlCommands.ExecuteRead(con, deviceQuery);
                devices = ParseDevices(reader);
                reader.Close();
            }

            return devices;
        }

        private List<Device> ParseDevices(SqlDataReader reader)
        {
            List<Device> devices = new List<Device>();
            while (reader.Read())
            {
                Device device = new Device();
                device.DeviceId = reader["DeviceId"].ToString();
                device.MaxInputValue = Convert.ToDouble(reader["MaxInputValue"]);
                device.MinInputValue = Convert.ToDouble(reader["MinInputValue"]);
                devices.Add(device);
            }
            return devices;
        }

        public void FetchDefaultLimits(string connectionString, ref List<Device> devices)
        {
            string limitsQuery;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommands.OpenConnection(con);
                foreach (var device in devices)
                {
                    limitsQuery =
                        String.Format("Select * FROM DefaultLimits WHERE DeviceId='{0}' order by MinValue ASC",
                            device.DeviceId);
                    SqlDataReader reader = SqlCommands.ExecuteRead(con, limitsQuery);
                    device.Limits = ParseLimits(reader);
                    reader.Close();
                }
            }
        }

        public List<Limits> ParseLimits(SqlDataReader reader)
        {
            List<Limits> limits = new List<Limits>();
            while (reader.Read())
            {
                Limits limit = new Limits();
                limit.MinValue = Convert.ToDouble(reader["MinValue"]);
                limit.MaxValue = Convert.ToDouble(reader["MaxValue"]);
                limit.Type = (LimitType)Convert.ToInt64(reader["LimitType"]);
                limit.Message = reader["LimitMessage"].ToString();
                limits.Add(limit);
            }
            return limits;
        }
    }
}
