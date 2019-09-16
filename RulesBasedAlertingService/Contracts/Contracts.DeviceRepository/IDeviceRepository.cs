using System.Collections.Generic;
using Models.Device;

namespace Contracts.DeviceRepository
{
    public interface IDeviceRepository
    {
        bool RegisterNew(Device device);
        bool Update(Device device);
        bool Delete(string deviceId);
        List<Device> ReadAll();
        Device Read(string deviceId);
        bool Exists(string deviceId);
    }
}
