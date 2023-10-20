using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayer;
using DataAccessInterfaces;
using DataAccessFakes;
using DataObjects;

namespace LogicLayerTests {
    [TestClass]
    public class EmployeeManagerTests {

        IEmployeeManager _employeeManager = null;

        [TestInitialize]
        public void TestSetUp() {
            _employeeManager = new EmployeeManager(new EmployeeAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsACorrectHashValue() {
            // in TDD (Test Driven Development) you write the test before the method
            // we use the red-green-refactor workflow
            // we write the test method with the A-A-A framework
            // test the test first before doing logic in method

            // Arrange - set up the test conditions
            EmployeeManager employeeManager = new EmployeeManager();
            string testString = "newuser";
            string actualhash = "";
            string expectedHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";

            // Act - invoke the method being tested and capture results
            actualhash = employeeManager.HashSha256(testString);

            // Assert - use the assert class
            Assert.AreEqual(expectedHash, actualhash);
        }

        [TestMethod]
        public void TestAuthenticateEmployeePassesWithCorrectEmailAndPassword() {
            // arrange
            string email = "tess@company.com";
            string passwordHash = "newuser";
            bool expectedResult = true;
            bool actualResult = false;

            // act
            actualResult = _employeeManager.AuthenticateEmployee(email, passwordHash);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void TestAuthenticateEmployeeFailsWithBadEmailAndPassword() {
            // arrange
            string email = "tess@company.com";
            string passwordHash = "newloser";
            bool expectedResult = false;
            bool actualResult = true;

            // act
            actualResult = _employeeManager.AuthenticateEmployee(email, passwordHash);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void TestGetEmployeeByEmailReturnsCorrectEmployee() {
            // arrange
            string email = "tess@company.com";
            int expectedEmployeeID = 1;
            int actualEmployeeID = 0;

            // act
            Employee employee = _employeeManager.GetEmployeeVMByEmail(email);
            actualEmployeeID = employee.EmployeeID;

            // assert
            Assert.AreEqual(expectedEmployeeID, actualEmployeeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetEmployeeByEmailFailsWithBadEmail() {
            // arrange
            string email = "ness@company.com";
            int expectedEmployeeID = 1;
            int actualEmployeeID = 0;

            // act
            Employee employee = _employeeManager.GetEmployeeVMByEmail(email);
            actualEmployeeID = employee.EmployeeID;

            // assert
            // nothing to do
        }

        [TestMethod]
        public void TestGetRolesByEmployeeIDReturnsCorrectRoles() {
            // arrange
            int testID = 1;
            int expectedRoleCount = 2;
            int actualRoleCount = 0;

            // act
            actualRoleCount = _employeeManager.GetRolesByEmployeeID(testID).Count;

            // assert
            Assert.AreEqual(expectedRoleCount, actualRoleCount);
        }

        [TestMethod]
        public void TestResetPasswordWorksCorrectly() {
            // arrange
            string email = "tess@company.com";
            string password = "newuser";
            string newPassword = "password";
            bool expected = true;
            bool actual = false;

            // act
            actual = _employeeManager.ResetPassword(email, password, newPassword);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
