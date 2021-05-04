using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YZYLibraryAzure;
using YZYStudentGUI;

namespace YZYUnitTest
{
    [TestClass]
    public class UTUserLoginTest
    {
        [TestMethod]
        public void Login_Porvince_Test()
        {
            string expStr = "QC, ON, BC, NL, PE, NS, NB, MB, SK, AB, YT, NT, NU only";
            string test = StudentValidationRules.checkProvince("qc");
            Assert.AreEqual(expStr, test);
        }
        [TestMethod]
        public void Login_Phone_Test()
        {
            string expStr = "phone no. should be 10 digits only";
            string test = StudentValidationRules.checkPhone("12345678901");
            Assert.AreEqual(expStr, test);
        }
        [TestMethod]
        public void Login_SIN_Test()
        {
            string expStr = "SIN nine digis only";
            string test = StudentValidationRules.checkSIN("qc");
            Assert.AreEqual(expStr, test);
        }
        [TestMethod]
        public void Login_Password_Test()
        {
            string expStr = "confirmed password should be same as password";
            string test = StudentValidationRules.checkPassword("5685","lkinh");
            Assert.AreEqual(expStr, test);
        }
    }
}
