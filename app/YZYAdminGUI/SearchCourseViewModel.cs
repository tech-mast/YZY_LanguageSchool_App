using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibraryAzure;

namespace YZYAdminGUI
{
    public class SearchCourseViewModel
    {

        private YZYDbContextAzure ctx;
        public ObservableCollection<Course> Courses { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> CategoryStrings { get; set; }
        public YZYCommand DeleteCommand { get; set; }
        public YZYCommand UpdateCommand { get; set; }
        public YZYCommand AddCommand { get; set; }

        public int CourseNumber
        {
            get
            {
                return Courses.Count;
            }
        }
        public SearchCourseViewModel()
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
            DeleteCommand = new YZYCommand(this.OnDelete, this.CanDelete);
            UpdateCommand = new YZYCommand(this.OnUpdate, this.CanExecute);
            AddCommand = new YZYCommand(this.OnAdd, this.CanExecute);
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
        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get
            {
                return _selectedCourse;
            }
            set
            {
                _selectedCourse = value;
            }
        }

        public void OnDelete()
        {
            //FIXME: failed if continously delete 2nd time
            ctx.Courses.Remove(SelectedCourse);
            ctx.SaveChanges();
            LoadCourse();
        }
        public bool CanDelete()
        {
            if (SelectedCourse != null)
            {
                return true;
            }
            return false;
        }

        public bool CanExecute()
        {
            if (SelectedCourse != null)
            {
                bool isPropertyFilledCorrectly;
                try
                {
                    ValidationRules.checkCourseDesc(SelectedCourse.CourseDesc);
                    ValidationRules.checkCourseTuition(SelectedCourse.Tuition);
                    ValidationRules.checkCourseDatetime(SelectedCourse.StartDate, SelectedCourse.EndDate);
                    isPropertyFilledCorrectly = true;
                }
                catch (InvalidParameterException)
                {
                    isPropertyFilledCorrectly = false;
                }
                return isPropertyFilledCorrectly;
            }
            return false;
        }
        public void OnUpdate()
        {
            try
            {
                var item = (from r in ctx.Courses where r.CourseID == SelectedCourse.CourseID select r).FirstOrDefault<Course>();
                if (item != null)
                {
                    item.CourseID = SelectedCourse.CourseID;
                    item.CourseDesc = SelectedCourse.CourseDesc;
                    item.StartDate = SelectedCourse.StartDate;
                    item.EndDate = SelectedCourse.EndDate;
                    item.Tuition = SelectedCourse.Tuition;
                    item.UserID = SelectedCourse.UserID;//FIXME: if name is changed, we have to check related userid in database
                    item.CategoryID = SelectedCourse.CategoryID;

                }
                ctx.SaveChanges();
                LoadCourse();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }
        
        public void OnAdd()
        {
            try
            {
                //FIXME: check related userid in database
                ctx.Courses.Add(SelectedCourse);
                ctx.SaveChanges();
                LoadCourse();
                //CourseID = 0;
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }

    }
}
