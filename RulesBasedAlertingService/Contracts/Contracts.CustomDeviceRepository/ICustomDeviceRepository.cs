using Models.Device;

namespace Contracts.CustomDeviceRepository
{
    public interface ICustomDeviceRepository
    {
        Device ReadCustom(string deviceId, string patientId);
        bool RegisterCustom(Device device, string patientId);
        bool UpdateCustom(Device device, string patientId);
        bool DeleteCustom(string deviceId, string patientId);
        bool DeleteCustom(string patientId);
        bool Exists(string patientId);
        bool Exists(string patientId, string deviceId);
    }
}
