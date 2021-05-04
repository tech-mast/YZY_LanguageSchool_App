using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibraryAzure;

namespace YZYStudentGUI
{
    class StudentSearchTeacherViewModel
    {
        private YZYDbContextAzure ctx;
        public ObservableCollection<User> Users { get; set; }
        public YZYCommand SearchCommand { get; set; }

        public StudentSearchTeacherViewModel()
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
            Users = new ObservableCollection<User>();
            LoadUser();
            //SearchCommand = new YZYCommand(this.OnSearch, this.CanExecute);

        }

        private void LoadUser()
        {
            try
            {
                var UserList = ctx.Users.ToList();
                Users.Clear();
                foreach (var item in UserList)
                {
                    if (item.UserRole == (UserRoleEnum)2)
                    {

                    Users.Add(item);
                    }
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }
    }
}
