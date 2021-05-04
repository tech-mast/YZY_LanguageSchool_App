using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using YZYLibraryAzure;
using YZYAdminGUI;


namespace YZYUnitTest
{
    [TestClass]
    public class UTSearchingCourseVM
    {
        private SearchCourseViewModel _vmSearchCourse = new SearchCourseViewModel();
        private static string _newCourseDesc = "SpecialCourseOfYZYUnitTest-2021";
        private static string _updatedCourseDesc = "SpecialCourseOfYZYUnitTest-2021-updated";

        private int getTeacherID()
        {
            int teacherID = -1;
            foreach(var course in _vmSearchCourse.Courses)
            {
                teacherID = course.UserID;
                if (teacherID > 0) break;
            }
            return teacherID;
        }

        private int getCategory()
        {
            int category;
            Random rnd = new Random();
            int index = rnd.Next(0, _vmSearchCourse.Categories.Count-1);
            category = _vmSearchCourse.Categories.ElementAt(index).CategoryID;
            return category;
        }
        [TestMethod]
        public void TestCase1_AddCourse_Success()
        {
            Course _newCourse = new Course();
            _newCourse.CourseDesc = _newCourseDesc;
            _newCourse.CategoryID = getCategory();
            _newCourse.UserID = getTeacherID();
            _newCourse.StartDate = DateTime.Today;
            _newCourse.EndDate = _newCourse.StartDate.AddDays(50);
            _vmSearchCourse.SelectedCourse = _newCourse;

            _vmSearchCourse.OnAdd();

            var item = _vmSearchCourse.Courses.ToList().Where(c=>c.CourseDesc.Equals(_newCourse.CourseDesc)).FirstOrDefault();
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void TestCase2_UpdarteCourse_Success()
        {
            Course _updatedCourse;
            _updatedCourse = _vmSearchCourse.Courses.ToList().Where(c => c.CourseDesc.Equals(_newCourseDesc)).FirstOrDefault();
            _updatedCourse.CourseDesc = _updatedCourseDesc;
            _vmSearchCourse.SelectedCourse = _updatedCourse;

            _vmSearchCourse.OnUpdate();

            var item = _vmSearchCourse.Courses.ToList().Where(c => c.CourseID== _updatedCourse.CourseID).FirstOrDefault();
            Assert.AreEqual(item.CourseDesc, _updatedCourseDesc);
        }

        [TestMethod]
        public void TestCase3_DeleteCourse_Success()
        {
            Course _deletedCourse;
            _deletedCourse = _vmSearchCourse.Courses.ToList().Where(c => c.CourseDesc.Equals(_updatedCourseDesc)).FirstOrDefault();
            _vmSearchCourse.SelectedCourse = _deletedCourse;

            _vmSearchCourse.OnDelete();

            var item = _vmSearchCourse.Courses.ToList().Where(c => c.CourseID == _deletedCourse.CourseID).FirstOrDefault();
            Assert.IsNull(item);
        }
    }
}
