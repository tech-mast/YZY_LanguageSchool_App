using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibraryAzure;

namespace YZYStudentGUI
{
    class StudentSearchCoursViewModel
    {
        private YZYDbContextAzure ctx;
        public ObservableCollection<Course> Courses { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> CategoryStrings { get; set; }
        public YZYCommand SearchCommand { get; set; }

        public StudentSearchCoursViewModel()
        {
            Log.setLogOnFile();
            try
            {
                ctx = new YZYDbContextAzure();
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            Courses = new ObservableCollection<Course>();
            Categories = new List<Category>();
            CategoryStrings = new List<string>();
            LoadCourse();
            SearchCommand = new YZYCommand(this.OnSearch);

        }

        private void LoadCourse()
        {
            try
            {
                var CourseList = ctx.Courses.ToList();
                Courses.Clear();
                foreach (var item in CourseList)
                {
                    Courses.Add(item);
                }
                Categories = ctx.Categories.ToList();
                CategoryStrings.Clear();
                CategoryStrings.Add("Please select");
                foreach (var item in Categories)
                {
                    CategoryStrings.Add(item.CateDesc);
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }
        //private Course _selectedCourse;
        //public Course SelectedCourse
        //{
        //    get
        //    {
        //        return _selectedCourse;
        //    }
        //    set
        //    {
        //        _selectedCourse = value;
        //    }
        //}       
        
        private int _selectedCategoryID;
        public int SelectedCategoryID
        {
            get
            {
                return _selectedCategoryID;
            }
            set
            {
                _selectedCategoryID = value;
            }
        }
        public void OnSearch()
        {
            //FIXME: if not selected
            //FIXME: failed if continously delete 2nd time
            //var SearchCourseList = (from r in ctx.Courses where r.CourseID == SelectedCourse.CourseID select r).ToList<Course>();       
            var SearchCourseList = (from r in ctx.Courses where r.CategoryID == SelectedCategoryID select r).ToList<Course>();
            Courses.Clear();
            foreach (var item in SearchCourseList)
            {
                Courses.Add(item);

            }

        }
        //public bool CanExecute()
        //{
        //    if (SelectedCategoryID != 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
