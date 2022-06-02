using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMSDAL;
using ClinicMSBAL;
using System.Data.SqlClient;
using System;

namespace TestingClinicMS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUserLoginMethod1()
        {
            Assert.AreEqual(true, Authentication.ValidateCredentials("krishna", "krish@123"));
        }

        [TestMethod]
        public void TestUserLoginMethod2()
        {
            Assert.AreEqual(false, Authentication.ValidateCredentials("krishna@", "krish@123"));
        }

        [TestMethod]
        public void TestUserLoginMethod3()
        {
            Assert.AreEqual(false, Authentication.ValidateCredentials("krishna", "krish123"));
        }
    }

    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestAddPatientMethod1()
        {
            Patient p = new();
            Assert.AreEqual("Vineer", p.AddPatientInDatabase("Vineer", "Cherla", "Male", 40, DateTime.ParseExact("20/12/1982","dd/MM/yyyy", null)).FirstName);
        }

    }
}
