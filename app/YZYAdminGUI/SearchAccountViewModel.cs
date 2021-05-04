using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibraryAzure;

namespace YZYAdminGUI
{
    class SearchAccountViewModel : IDataErrorInfo
    {
        private YZYDbContextAzure ctx;
        public ObservableCollection<User> Users { get; set; }

        public YZYCommand DeleteCommand { get; set; }
        public YZYCommand UpdateCommand { get; set; }
        public YZYCommand AddCommand { get; set; }
        //public YZYCommand RegisterCommand { get; set; }

        public SearchAccountViewModel()
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
            LoadCourse();
            DeleteCommand = new YZYCommand(this.OnDelete, this.CanExecute);
            UpdateCommand = new YZYCommand(this.OnUpdate, this.CanExecute);
            AddCommand = new YZYCommand(this.OnAdd, this.CanAdd);
            //RegisterCommand = new YZYCommand(this.OnRegister, this.CanRegister);
        }

        private void LoadCourse()
        {
            try
            {
                var UserList = ctx.Users.ToList();
                Users.Clear();
                foreach (var item in UserList)
                {
                    Users.Add(item);
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
            }
        }

        public void OnDelete()
        {
            //FIXME: if not selected
            //FIXME: failed if continously delete 2nd time
            try
            {
                ctx.Users.Remove(SelectedUser);
                ctx.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                Log.WriteLine(SelectedUser.UserID+"[DELETE]: "+ex.Message);
            }

            LoadCourse();
        }
        public bool CanExecute()
        {
            if (SelectedUser != null)
            {
                return true;
            }
            return false;
        }
        public void OnUpdate()
        {
            try
            {
                var dlg = new AddUserDialog();
                dlg.DataContext = SelectedUser;
                if (SelectedUser.Photo != null)
                {
                    dlg.Photo = SelectedUser.Photo;
                }
                if (dlg.ShowDialog() == true)
                {
                    var item = (from r in ctx.Users where r.UserID == SelectedUser.UserID select r).FirstOrDefault<User>();
                    if (item != null)
                    {
                        item.FName = SelectedUser.FName;
                        item.MName = SelectedUser.MName;
                        item.LName = SelectedUser.LName;
                        item.UserSIN = SelectedUser.UserSIN;
                        item.UserRole = SelectedUser.UserRole;
                        item.Gender = SelectedUser.Gender;
                        item.StreetNo = SelectedUser.StreetNo;
                        item.StreetName = SelectedUser.StreetName;
                        item.City = SelectedUser.City;
                        item.Province = SelectedUser.Province;
                        item.PostalCode = SelectedUser.PostalCode;
                        item.Phone = SelectedUser.Phone;
                        item.Cell = SelectedUser.Cell;
                        item.Email = SelectedUser.Email; 
                        item.Photo = dlg.Photo;
                        item.Password = SelectedUser.Password;
                    }
                    ctx.SaveChanges();
                    LoadCourse();
                }
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
                    case "CUserID":
                        if (!String.IsNullOrEmpty(_errorMessage[0]))
                            return _errorMessage[0];
                        break;
                }
                return string.Empty;
            }
        }
       
        public void OnAdd()
        {
            try
            {
                var dlg = new AddUserDialog();
                var user = new User();
                dlg.DataContext = user;
                if (dlg.ShowDialog() == true)
                {
                    user.Photo = dlg.Photo;
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    LoadCourse();
                }
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }
        public bool CanAdd()
        {
            return true;
        }
}
}
