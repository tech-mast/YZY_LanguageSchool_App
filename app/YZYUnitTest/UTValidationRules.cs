using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YZYAdminGUI;
using YZYLibraryAzure;

namespace YZYUnitTest
{
    [TestClass]
    public class UTValidationRules
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void checkCourseID_Failed()
        {
            ValidationRules.checkCourseID(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void checkPostCode_Failed()
        {
            ValidationRules.checkPostCode("h3e666");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void checkEmail_Failed()
        {
            ValidationRules.checkEmail("&dfsef^");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void checkCourseTuition_Failed()
        {
            ValidationRules.checkCourseTuition(-50);
        }
    }
}
