using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Device;

namespace Test.Unit.DeviceRepository
{
    [TestClass]
    public class DeviceRepositoryUnitTest
    {

        DataAccessLayer.DeviceRepositoryLib.DeviceRepository devicerepo;

        [TestInitialize]
        public void TestInitialize()
        {
            devicerepo = new DataAccessLayer.DeviceRepositoryLib.DeviceRepository(true);
        }

        private Device CreateDevice()
        {
            Device device = new Device
            {
                DeviceId = "SPO1",
                MinInputValue = 0,
                MaxInputValue = 100,
            };

            List<Limits> limits = new List<Limits>();
            limits.Add(new Limits { MinValue = 0, MaxValue = 50, Type = LimitType.Normal, Message = "Normal" });
            limits.Add(new Limits { MinValue = 50, MaxValue = 100, Type = LimitType.Warning, Message = "Warning" });

            device.Limits = limits;
            return device;

        }

        private void RegisterDevice()
        {
            Device device = CreateDevice();
            devicerepo.RegisterNew(device);
        }

        private void DeleteDevice()
        {
            devicerepo.Delete("SPO1");

        }


        [TestMethod]
        public void Given_Valid_Arguments_When_RegisterNew_Invoked_Then_true_Asserted()
        {
            Device device = CreateDevice();
            Assert.IsTrue(devicerepo.RegisterNew(device));
            DeleteDevice();
        }


        [TestMethod]
        public void Given_Invalid_Arguments_When_RegisterNew_Invoked_Then_false_Asserted()
        {

            Device device = CreateDevice();
            devicerepo.RegisterNew(device);
            Assert.IsFalse(devicerepo.RegisterNew(device));
            DeleteDevice();
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Update_Invoked_Then_true_Asserted()
        {

            Device device = CreateDevice();
            RegisterDevice();
            Assert.IsTrue(devicerepo.Update(device));
            DeleteDevice();
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_Update_Invoked_Then_false_Asserted()
        {
            Device device = CreateDevice();
            Assert.IsFalse(devicerepo.Update(device));
        }

        [TestMethod]
        public void Given_Valid_Arguments_When_Delete_Invoked_Then_true_Asserted()
        {
            RegisterDevice();
            Assert.IsTrue(devicerepo.Delete("SPO1"));
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_Delete_Invoked_Then_false_Asserted()
        {
            Assert.IsFalse(devicerepo.Delete("SPO1"));
        }


        [TestMethod]
        public void Given_Valid_Arguments_When_Exists_Invoked_Then_true_Asserted()
        {
            RegisterDevice();
            Assert.IsTrue(devicerepo.Exists("SPO1"));
            DeleteDevice();
        }



        [TestMethod]
        public void Given_Invalid_Arguments_When_Exists_Invoked_Then_false_Asserted()
        {
            Assert.IsFalse(devicerepo.Exists("SPO1"));
        }

        [TestMethod]
        public void Given_Valid_Argument_When_Read_Invoked_Then_Valid_Result_Asserted()
        {
            RegisterDevice();
            Assert.AreEqual("SPO1", devicerepo.Read("SPO1").DeviceId);
            DeleteDevice();
        }



        [TestMethod]
        public void Given_Invalid_Argument_When_Read_Invoked_Then_null_Asserted()
        {
            Assert.IsNull(devicerepo.Read("SPO1").DeviceId);
        }



        [TestMethod]
        public void When_ReadAll_Invoked_Then_Valid_Result_Asserted()
        {
            RegisterDevice();
            Assert.AreEqual(1, devicerepo.ReadAll().Count);
            DeleteDevice();
        }

    }
}
