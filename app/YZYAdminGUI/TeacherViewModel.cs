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
    class TeacherViewModel : IDataErrorInfo
    {
        private YZYDbContextAzure ctx;
        public ObservableCollection<Course> Courses { get; set; }
        //public ObservableCollection<Register> Registers { get; set; }
        public YZYCommand UpdateCommand { get; set; }
        //public YZYCommand UpdateGradeCommand { get; set; }
        public Course SelectedCourse { get; set; }
        public TeacherViewModel()
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
            LoadCourse();
            UpdateCommand = new YZYCommand(this.OnUpdate, this.CanUpdate);
            //UpdateGradeCommand = new YZYCommand(this.OnUpdateGrade, this.CanUpdateGrade);
        }
        private void LoadCourse()
        {
            try
            {
                var CourseList = ctx.Courses.ToList().Where(c=>c.UserID==GlobalSettings.userID);
                Courses.Clear();
                foreach (var item in CourseList)
                {
                    Courses.Add(item);
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }
        public bool CanUpdate()
        {
            if (SelectedCourse != null)
            {
                return true;
            }
            return false;
        }
        public void OnUpdate()
        {
            //Registers = SelectedCourse.Registers.ToList();
            TeacherManagementDialog gradeDlg = new TeacherManagementDialog(SelectedCourse.CourseID);
            gradeDlg.ShowDialog();
        }

        /*
        private Register _regSelected;
        public Register SelectedRegister 
        {
            get
            {
                return _regSelected;
            }
            set
            {
                _regSelected = value;
                if(_regSelected.Evaluations.Count<1)
                {
                    _evToUpdate = new Evaluation();
                    _evToUpdate.RegisterID = _regSelected.RegisterID;
                    _evToUpdate.EvDate = DateTime.Now;
                    _evToUpdate.Comment = string.Empty;
                }
                else
                {
                    _evToUpdate = _regSelected.Evaluations.ElementAt(0);
                    _evToUpdate.EvDate = DateTime.Now;
                }
            }
        }
        
        private Evaluation _evToUpdate = new Evaluation();
        public Evaluation EvaluationToUpdate
        {
            get
            {
                return _evToUpdate;
            }
            set
            {
                _evToUpdate = value;
            }
        }
        public string Grade
        {
            get
            {
                return _evToUpdate.Comment;
            }
            set
            {
                _evToUpdate.Comment = value;
            }
        }

        public DateTime GradeDate
        {
            get
            {
                return _evToUpdate.EvDate;
            }
            set
            {
                _evToUpdate.EvDate = value;
            }
        }
 
        public bool CanUpdateGrade()
        {
            if (SelectedRegister != null)
            {
                return true;
            }
            return false;
        }
        public void OnUpdateGrade()
        {
            try
            {
                var regid = _evToUpdate.RegisterID;
                var item = (from r in ctx.Evaluations where r.RegisterID == regid select r).FirstOrDefault<Evaluation>();
                if (item != null)
                {
                    item.Comment = _evToUpdate.Comment;
                    item.EvDate = _evToUpdate.EvDate;
                    ctx.SaveChanges();
                }
                else
                {
                    ctx.Evaluations.Add(_evToUpdate);
                    ctx.SaveChanges();
                }
                LoadRegistrations();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }
        private void LoadRegistrations()
        {
            try
            {
                var items = SelectedCourse.Registers.ToList();
                Registers.Clear();
                foreach (var item in items)
                {
                    Registers.Add(item);
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }
        */
        private string[] _errorMessage = new string[7] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "CourseID":
                        if (!String.IsNullOrEmpty(_errorMessage[0]))
                            return _errorMessage[0];
                        break;
                }
                return string.Empty;
            }
        }

    }
}
