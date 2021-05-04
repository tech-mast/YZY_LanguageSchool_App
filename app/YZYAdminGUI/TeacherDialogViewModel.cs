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
    class TeacherDialogViewModel : IDataErrorInfo
    {
        private YZYDbContextAzure ctx;
        private int _selectedCourseID;
        public ObservableCollection<Register> Registers { get; set; }
        public YZYCommand UpdateGradeCommand { get; set; }

        public TeacherDialogViewModel(int selectedCourseID)
        {
            _selectedCourseID = selectedCourseID;
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
            Registers = new ObservableCollection<Register>();
            LoadRegistrations();
            UpdateGradeCommand = new YZYCommand(this.OnUpdateGrade, this.CanUpdateGrade);
        }
        private void LoadRegistrations()
        {
            try
            {
                var items = ctx.Registers.ToList().Where(r=>r.CourseID== _selectedCourseID);
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

        public Register SelectedRegister { get; set; }
        public bool CanUpdateGrade()
        {
            if (SelectedRegister != null)
            {
                bool result;
                try
                {
                    ValidationRules.checkGrade(SelectedRegister.Grade);
                    result = true;
                }
                catch (InvalidParameterException)
                {
                    result = false;
                }
                return result;
            }
            return false;
        }
        public void OnUpdateGrade()
        {
            try
            {
                var regid = SelectedRegister.RegisterID;
                var grade = SelectedRegister.Grade;
                var item = (from r in ctx.Registers where r.RegisterID == regid select r).FirstOrDefault<Register>();
                if (item != null)
                {
                    item.Grade = grade;
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
