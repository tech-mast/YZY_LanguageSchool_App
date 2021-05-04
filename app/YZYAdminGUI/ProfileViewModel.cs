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
    class ProfileViewModel
    {
        private YZYDbContextAzure ctx;
        public User LoginUser { get; set; }

        public YZYCommand UpdateCommand { get; set; }

        public byte[] Photo { get; set; }
        public ProfileViewModel()
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
            LoadUser();
            UpdateCommand = new YZYCommand(this.OnUpdate, this.CanUpdate);
        }

        private void LoadUser()
        {
            try
            {
                User user = ctx.Users.ToList().Where(u=>u.UserID==GlobalSettings.userID).FirstOrDefault();
                if(user != null)
                {
                    LoginUser = user;
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }

        public bool CanUpdate()
        {
            bool isPropertyFilledCorrectly;
            try
            {
                if (LoginUser != null)
                {
                    ValidationRules.checkEmail(LoginUser.Email);
                    ValidationRules.checkPostCode(LoginUser.PostalCode);
                    isPropertyFilledCorrectly = true;
                }
                else
                {
                    isPropertyFilledCorrectly = false;
                }
            }
            catch (InvalidParameterException)
            {
                isPropertyFilledCorrectly = false;
            }
            return isPropertyFilledCorrectly;
        }
        public void OnUpdate()
        {
            try
            {
                LoginUser.Photo = this.Photo;
                ctx.SaveChanges();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }
    }
}
