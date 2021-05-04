using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibraryAzure;

namespace YZYStudentGUI
{
    class StudentRegisterViewModel : IDataErrorInfo  // validation
    {
            private YZYDbContextAzure database;
            public ObservableCollection<User> Users { get; set; }
            public YZYCommand RegisterCommand { get; set; }
            public StudentRegisterViewModel()
            {
                try
                {
                    database = new YZYDbContextAzure();
                }
                catch (SystemException ex)
                {
                    Log.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
                Users = new ObservableCollection<User>();
                //RegisterCommand = new YZYCommand(this.OnAdd, this.CanAdd);
                RegisterCommand = new YZYCommand(this.OnAdd);

            }

            private User _newUser = new User();

            public string FName
            {
                get
                {
                    return _newUser.FName;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }

                    try
                    {
                        _newUser.FName = value;
                        _errorMessage[0] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[0] = ex.Message;//validation
                    }

                }
            }
            public string MName
            {
                get
                {
                    return _newUser.MName;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.MName = value;
                        _errorMessage[1] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[1] = ex.Message;//validation
                    }

                }
            }

            public string LName
            {
                get
                {
                    return _newUser.LName;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.LName = value;
                        _errorMessage[2] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[2] = ex.Message;//validation
                    }

                }
            }

            public string Password
            {
                get
                {
                    return _newUser.Password;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.Password = value;
                        _errorMessage[3] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[3] = ex.Message;//validation
                    }

                }
            }

            public string UserSIN
            {
                get
                {
                    return _newUser.UserSIN;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.UserSIN = value;
                        _errorMessage[4] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[4] = ex.Message;//validation
                    }


                }
            }
            public GenderEnum Gender
            {
                get
                {
                    return _newUser.Gender;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.Gender = value;
                        _errorMessage[5] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[5] = ex.Message;//validation
                    }


                }
            }
            public UserRoleEnum UserRole
            {
                get
                {
                    return _newUser.UserRole;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.UserRole = value;
                        _errorMessage[6] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[6] = ex.Message;//validation
                    }

                }
            }
            public string StreetNo
            {
                get
                {
                    return _newUser.StreetNo;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.StreetNo = value;
                        _errorMessage[7] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[7] = ex.Message;//validation
                    }

                }
            }

            public string StreetName
            {
                get
                {
                    return _newUser.StreetName;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.StreetName = value;
                        _errorMessage[8] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[8] = ex.Message;//validation
                    }

                }
            }

            public string City
            {
                get
                {
                    return _newUser.City;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {

                        _newUser.City = value;
                        _errorMessage[9] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[9] = ex.Message;//validation
                    }

                }
            }
            public string Province
            {
                get
                {
                    return _newUser.Province;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.Province = value;
                        _errorMessage[10] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[10] = ex.Message;//validation
                    }

                }
            }

            public string PostalCode
            {
                get
                {
                    return _newUser.PostalCode;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.PostalCode = value;
                        _errorMessage[11] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[11] = ex.Message;//validation
                    }

                }
            }
            public string Phone
            {
                get
                {
                    return _newUser.Phone;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.Phone = value;
                        _errorMessage[12] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[12] = ex.Message;//validation
                    }

                }
            }
            public string Cell
            {
                get
                {
                    return _newUser.Cell;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.Cell = value;
                        _errorMessage[13] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[13] = ex.Message;//validation
                    }

                }
            }

            public string Email
            {
                get
                {
                    return _newUser.Email;
                }
                set
                {
                    if (_newUser == null)
                    {
                        _newUser = new User();
                    }
                    try
                    {
                        _newUser.Email = value;
                        _errorMessage[14] = string.Empty;//validation
                    }
                    catch (InvalidParameterException ex)
                    {
                        _errorMessage[14] = ex.Message;//validation
                    }

                }
            }



            private string[] _errorMessage = new string[16] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

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
                        case "FName":
                            if (!String.IsNullOrEmpty(_errorMessage[0]))
                                return _errorMessage[0];
                            break;
                        case "MName":
                            if (!String.IsNullOrEmpty(_errorMessage[1]))
                                return _errorMessage[1];
                            break;
                        case "LName":
                            if (!String.IsNullOrEmpty(_errorMessage[2]))
                                return _errorMessage[2];
                            break;
                        case "Password":
                            if (!String.IsNullOrEmpty(_errorMessage[3]))
                                return _errorMessage[3];
                            break;
                        //case "Password":
                        //    if (!String.IsNullOrEmpty(_errorMessage[3]))
                        //        return _errorMessage[2];
                        //    break;
                        case "UserSIN":
                            if (!String.IsNullOrEmpty(_errorMessage[4]))
                                return _errorMessage[4];
                            break;
                        case "Gender":
                            if (!String.IsNullOrEmpty(_errorMessage[5]))
                                return _errorMessage[5];
                            break;
                        case "UserRole":
                            if (!String.IsNullOrEmpty(_errorMessage[6]))
                                return _errorMessage[6];
                            break;
                        case "StreetNo":
                            if (!String.IsNullOrEmpty(_errorMessage[7]))
                                return _errorMessage[7];
                            break;
                        case "StreetName":
                            if (!String.IsNullOrEmpty(_errorMessage[8]))
                                return _errorMessage[8];
                            break;
                        case "City":
                            if (!String.IsNullOrEmpty(_errorMessage[9]))
                                return _errorMessage[9];
                            break;
                        case "Province":
                            if (!String.IsNullOrEmpty(_errorMessage[10]))
                                return _errorMessage[10];
                            break;
                        case "PostalCode":
                            if (!String.IsNullOrEmpty(_errorMessage[11]))
                                return _errorMessage[11];
                            break;
                        case "Phone":
                            if (!String.IsNullOrEmpty(_errorMessage[12]))
                                return _errorMessage[12];
                            break;
                        case "Cell":
                            if (!String.IsNullOrEmpty(_errorMessage[13]))
                                return _errorMessage[13];
                            break;
                        case "Email":
                            if (!String.IsNullOrEmpty(_errorMessage[14]))
                                return _errorMessage[14];
                            break;
                    }
                    return string.Empty;
                }
            }

            public bool CanAdd()
            {
                bool isPropertyFilledCorrectly = true;
                //FIXME: add logic to check whether all fields are properly set
                foreach (var s in _errorMessage)
                {
                    isPropertyFilledCorrectly = isPropertyFilledCorrectly && String.IsNullOrEmpty(s);
                }
                if (_newUser != null && isPropertyFilledCorrectly)
                {
                    return true;
                }
                return false;
            }

            public User NewUser
            {
                get
                {
                    return _newUser;
                }
            }

            public void OnAdd()
            {
                try
                {
                //WORKROUND: wdit a selected item to Add
                //TOFIX: add item cannot reuse controller bound with list view
                NewUser.Password = GlobalSettings.newPassword;
                NewUser.UserRole = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), "Student", true);
                if (GlobalSettings.currentPhoto != null && GlobalSettings.currentPhoto.Length > 0)
                { NewUser.Photo = GlobalSettings.currentPhoto; }
  
                    database.Users.Add(NewUser);
                    database.SaveChanges();

            }
                catch (Exception ex)
                    when ((ex is InvalidParameterException) || (ex is SystemException))
                {
                    Log.WriteLine(ex.Message);
                    Console.WriteLine(ex.Message);

                }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        }
    
}
