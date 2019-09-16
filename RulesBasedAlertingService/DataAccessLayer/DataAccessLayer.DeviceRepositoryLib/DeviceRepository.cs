using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DeviceRepository;
using Models.Device;
using Utility.SqlHelper;

namespace DataAccessLayer.DeviceRepositoryLib
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly string conString;
        private readonly SqlDeviceReader deviceReader;

        public DeviceRepository(bool test = false)
        {
            if (test)
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB.Test;Integrated Security=True";
            else
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB;Integrated Security=True";
            deviceReader = new SqlDeviceReader();

        }


        public bool Delete(string deviceId)
        {
            var query = string.Format("DELETE FROM DeviceRepository WHERE DeviceId='{0}'", deviceId);
            return SqlCommands.ExecuteCommand(query, conString);
        }

        public bool Exists(string deviceId)
        {
            string query = string.Format("Select DeviceId FROM DeviceRepository WHERE DeviceId='{0}'", deviceId);
            return SqlCommands.Exists(conString, query);
        }



        public Device Read(string deviceId)
        {
            var deviceQuery = string.Format("Select * FROM DeviceRepository WHERE DeviceId='{0}'", deviceId);
            var devices = deviceReader.FetchDevices(conString, deviceQuery);
            deviceReader.FetchDefaultLimits(conString, ref devices);
            if (!devices.Any())
                return new Device();
            return devices.First();

        }



        public List<Device> ReadAll()
        {
            var deviceQuery = "Select * FROM DeviceRepository";
            var devices = deviceReader.FetchDevices(conString, deviceQuery);
            deviceReader.FetchDefaultLimits(conString, ref devices);
            if (!devices.Any())
                return new List<Device>();
            return devices;
        }




        public bool RegisterNew(Device device)
        {

            var query = string.Format("Insert into DeviceRepository values('{0}','{1}','{2}')",
                device.DeviceId, device.MinInputValue, device.MaxInputValue);

            if (SqlCommands.ExecuteCommand(query, conString))
            {

                return WriteDefaultLimits(device.Limits, device.DeviceId);

            }
            return false;
        }

        private bool WriteDefaultLimits(List<Limits> limits, string deviceId)
        {

            foreach (Limits limit in limits)
            {
                string query = string.Format("Insert into DefaultLimits values('{0}','{1}','{2}','{3}','{4}')", deviceId, limit.MinValue.ToString(), limit.MaxValue, (int)limit.Type, limit.Message);
                if (!SqlCommands.ExecuteCommand(query, conString))
                {
                    return false;
                }
            }

            return true;
        }


        public bool Update(Device device)
        {


            var query = string.Format("DELETE FROM DefaultLimits WHERE DeviceId='{0}'", device.DeviceId);
            if (SqlCommands.ExecuteCommand(query, conString))
            {
                query = string.Format("UPDATE DeviceRepository SET MinInputValue='{0}',MaxInputValue='{1}' WHERE DeviceId='{2}'", device.MinInputValue, device.MaxInputValue, device.DeviceId);
                if (SqlCommands.ExecuteCommand(query, conString))
                {
                    return WriteDefaultLimits(device.Limits, device.DeviceId);
                }

            }
            return false;
        }
    }
}
