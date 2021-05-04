using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using YZYLibraryAzure;
using YZYStudentGUI;


namespace YZYUnitTest
{
    [TestClass]
    public class UTUserPorfileFunction
    {
        [TestMethod]
        public void UserPorfileID_test()
        {
            YZYDbContextAzure ctx;
            ctx = new YZYDbContextAzure();
            //GlobalSettings.userID = 2;
            User testUser = ctx.Users.Where(r => r.UserID == 2).FirstOrDefault();
            int expResult = 2;
            Assert.AreEqual(expResult, testUser.UserID);
        }

        [TestMethod]
        public void UserPorfile_updateMethod_test()
        {
            YZYDbContextAzure ctx;
            ctx = new YZYDbContextAzure();
            User testUser = ctx.Users.Where(r => r.UserID == 2).FirstOrDefault();
            string expName = "Olivia";
            Assert.AreEqual(expName, testUser.FName);
        }

    }
}
