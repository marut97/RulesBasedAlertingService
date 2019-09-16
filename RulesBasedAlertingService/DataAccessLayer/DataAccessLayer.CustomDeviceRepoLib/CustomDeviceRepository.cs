using System.Linq;
using Contracts.CustomDeviceRepository;
using Contracts.DeviceRepository;
using Models.Device;
using Utility.SqlHelper;

namespace DataAccessLayer.CustomDeviceRepoLib
{
    public class CustomDeviceRepository : ICustomDeviceRepository
    {
        private readonly string conString;

        private readonly IDeviceRepository _defaultDeviceRepo;
        private readonly SqlCustomDeviceReader customDeviceReader;

        public CustomDeviceRepository(IDeviceRepository deviceRepo, bool test = false)
        {
            if (test)
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB.Test;Integrated Security=True";
            else
                conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RulesBasedAlertingDB;Integrated Security=True";
            _defaultDeviceRepo = deviceRepo;
            customDeviceReader = new SqlCustomDeviceReader();

        }


        public bool DeleteCustom(string deviceId, string patientId)
        {

            string query = $"DELETE FROM CustomLimits WHERE PatientId='{patientId}' AND DeviceId='{deviceId}'";
            return SqlCommands.ExecuteCommand(query, conString);

        }

        public bool DeleteCustom(string patientId)
        {
            string query = $"DELETE FROM CustomLimits WHERE PatientId='{patientId}'";
            return SqlCommands.ExecuteCommand(query, conString);
        }

        public bool Exists(string patientId)
        {
            string query = $"SELECT * FROM CustomLimits WHERE PatientId='{patientId}'";
            return SqlCommands.Exists(conString, query);


        }

        public bool Exists(string patientId, string deviceId)
        {
            string query = $"SELECT * FROM CustomLimits WHERE PatientId='{patientId}' AND DeviceId='{deviceId}'";
            return SqlCommands.Exists(conString, query);
        }

        public Device ReadCustom(string deviceId, string patientId)
        {

            var device = _defaultDeviceRepo.Read(deviceId);
            if (device.DeviceId.Any())
            {
                device.Limits.Clear();
                device.Limits = customDeviceReader.FetchCustomLimits(conString, deviceId, patientId);
                return device;
            }
            return new Device();
        }

        public bool RegisterCustom(Device device, string patientId)
        {
            if (!Exists(patientId, device.DeviceId))
            {
                if (!InsertCustomLimitsList(device, patientId))
                    return false;

                return true;
            }
            return false;
        }

        private bool InsertCustomLimitsList(Device device, string patientId)
        {
            foreach (Limits limit in device.Limits)
            {
                string query =
                    $"Insert into CustomLimits values ('{patientId}', '{device.DeviceId}', '{limit.MinValue}', '{limit.MaxValue}','{(int) limit.Type}','{limit.Message}')";
                if (!SqlCommands.ExecuteCommand(query, conString))
                    return false;
            }

            return true;
        }

        public bool UpdateCustom(Device device, string patientId)
        {
            if (DeleteCustom(device.DeviceId, patientId))
            {
                return RegisterCustom(device, patientId);
            }
            return false;
        }
    }
}
