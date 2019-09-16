using System;
using Contracts.HospitalBedRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockRepositoryLib;
using Models.HospitalBed;
using Utility.BedParser;

namespace Test.Unit.BedParser
{
    [TestClass]
    public class BedParserUnitTest
    {
        Utility.BedParser.HospitalBedParser bedParser;

        [TestInitialize]
        public void TestInitialize()
        {
            bedParser = new HospitalBedParser(new MockBedRepository());
        }

        [TestMethod]
        public void Given_Valid_Room_Id_When_ReadEmptyBeds_Invoked_Then_Valid_Result_Asserted()
        {
            Assert.AreEqual(4,bedParser.ReadEmptyBeds("PIC-2F-2A-10").Count);
        }

        [TestMethod]
        public void Given_Valid_Wing_Id_When_ReadEmptyBeds_Invoked_Then_Valid_Result_Asserted()
        {
            Assert.AreEqual(3, bedParser.ReadEmptyBeds("PIC-2F-2A").Count);
        }

        [TestMethod]
        public void Given_Valid_Floor_Id_When_ReadEmptyBeds_Invoked_Then_Valid_Result_Asserted()
        {
            Assert.AreEqual(2, bedParser.ReadEmptyBeds("PIC-2F").Count);
        }

        [TestMethod]
        public void Given_Valid_Campus_Id_When_ReadEmptyBeds_Invoked_Then_Valid_Result_Asserted()
        {
            Assert.AreEqual(1, bedParser.ReadEmptyBeds("PIC").Count);
        }

        [TestMethod]
        public void Given_Valid_Room_Id_When_ReadOccupiedBeds_Invoked_Then_Valid_Result_Asserted()
        {
            Assert.AreEqual(4, bedParser.ReadOccupiedBeds("PIC-2F-2A-10").Count);
        }

        [TestMethod]
        public void Given_Valid_Wing_Id_When_ReadOccupiedBeds_Invoked_Then_Valid_Result_Asserted()
        {
            Assert.AreEqual(3, bedParser.ReadOccupiedBeds("PIC-2F-2A").Count);
        }

        [TestMethod]
        public void Given_Valid_Floor_Id_When_ReadOccupiedBeds_Invoked_Then_Valid_Result_Asserted()
        {
            Assert.AreEqual(2, bedParser.ReadOccupiedBeds("PIC-2F").Count);
        }

        [TestMethod]
        public void Given_Valid_Campus_Id_When_ReadOccupiedBeds_Invoked_Then_Valid_Result_Asserted()
        {
            Assert.AreEqual(1, bedParser.ReadOccupiedBeds("PIC").Count);
        }
    }
}
