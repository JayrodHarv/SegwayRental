using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessFakes;
using DataAccessInterfaces;

namespace LogicLayerTests {
    /// <summary>
    /// Summary description for SegwayManagerTests
    /// </summary>
    [TestClass]
    public class SegwayManagerTests {

        ISegwayManager _segwayManager = null;

        [TestInitialize]
        public void TestSetup() {
            _segwayManager = new SegwayManager(new SegwayAccessorFake());
        }


        [TestMethod]
        public void TestGetSegwaysByStatusIDReturnsCorrectList() {
            // arrange
            string status = "For Rent";
            int expectedCount = 2;
            int actualCount = 0;

            // act
            actualCount = _segwayManager.GetSegwaysByStatusID(status).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }
    }
}
