using System.Collections.Generic;
using Contracts.DeviceRepository;
using DataAccessLayer.DeviceRepositoryLib;
using Models.Device;

namespace Controllers.DeviceRegistrationService
{
    public class DeviceRegistrationService : IDeviceRegistrationService
    {
        private readonly IDeviceRepository m_repository;
        public DeviceRegistrationService()
        {
            m_repository = new DeviceRepository();
        }
        public void RegisterNewDevice(Device device)
        {
            m_repository.RegisterNew(device);
        }

        public void UpdateDevice(Device device)
        {
            m_repository.Update(device);
        }

        public void DeleteDevice(string deviceId)
        {
            m_repository.Delete(deviceId);
        }

        public List<Device> ReadAllDevices()
        {
            return m_repository.ReadAll();
        }

        public Device ReadDevice(string deviceId)
        {
            return
                m_repository.Read(deviceId);
        }


    }
}