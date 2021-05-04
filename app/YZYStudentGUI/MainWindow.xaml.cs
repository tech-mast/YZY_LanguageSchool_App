using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using System.Windows.Forms;
using YZYLibraryAzure;
//using Webcam;
using System.IO;
using System.Data.Entity.Validation;

namespace YZYStudentGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //TODO: language has to be set here before initialize window
            //CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();


            //switch (ConfigurationManager.AppSettings["DefaultCulture"])
            //{
            //    case "zh-Hans":
            //        btEnglish.Visibility = Visibility.Visible;
            //        btChinese.Visibility = Visibility.Hidden;
            //        break;
            //    case "en":
            //        btEnglish.Visibility = Visibility.Hidden;
            //        btChinese.Visibility = Visibility.Visible;
            //        break;
            //}
            btManageCourse.IsEnabled = false;
            btProfile.IsEnabled = false;
            btPayment.IsEnabled = false;
            tbWelcomeUsername.Text = "";
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            this.Close();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Search_CourseByCategory_Click(object sender, RoutedEventArgs e)
        {
            StudentSearchCoursViewModel regvmInstance = new StudentSearchCoursViewModel();
            this.DataContext = regvmInstance;
            regvmInstance.SelectedCategoryID = comboSearchCourse.SelectedIndex;
        }

        //private void LanguageToggle_Click(object sender, RoutedEventArgs e)
        //{
        //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        //    config.AppSettings.Settings.Remove("DefaultCulture");
        //    if (LanguageToggle.IsChecked == true)
        //    {
        //        //config.AppSettings.Settings.Add("DefaultCulture", "zh-Hans");// TODO: to add selected language string replacing "FIXME"
        //        //CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
        //        App.ChangeCulture(new CultureInfo("zh-Hans"));
        //    }
        //    else
        //    {
        //            //config.AppSettings.Settings.Add("DefaultCulture", "en");// TODO: to add selected language string replacing "FIXME"
        //        //CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
        //        App.ChangeCulture(new CultureInfo("en"));

        //    }
        //    config.Save(ConfigurationSaveMode.Modified);



        //}


        private void btEnglish_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeCulture(new CultureInfo("en"));
        }

        private void btChinese_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeCulture(new CultureInfo("zh-Hans"));
        }


        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {

            RegisterDialog rd = new RegisterDialog();
            rd.Owner = this;
            var user = new User();
            try
            {
                if (rd.ShowDialog() == true)
                {
                    //YZYDbContextAzure ctx = new YZYDbContextAzure();
                    //user.FName = rd.tbNewFirstName.Text;
                    //user.MName = rd.tbNewMiddleName.Text;
                    //user.LName = rd.tbNewLastName.Text;
                    //user.UserSIN = rd.tbNewSIN.Text;
                    //user.UserRole = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), "Student", true);
                    //user.Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), rd.tbNewGender.Text, true);
                    //user.StreetNo = rd.tbNewStreetNo.Text;
                    //user.StreetName = rd.tbNewStreetName.Text;
                    //user.City = rd.tbNewCity.Text;
                    //user.Province = rd.tbNewProvince.Text;
                    //user.PostalCode = rd.tbNewPostalCode.Text;
                    //user.Phone = rd.tbNewPhone.Text;
                    //user.Cell = rd.tbNewCell.Text;
                    //user.Email = rd.tbNewEmail.Text;
                    //user.Password = rd.tbNewPassword.Password;
                    //if (GlobalSettings.currentPhoto != null && GlobalSettings.currentPhoto.Length > 0)
                    //{ user.Photo = GlobalSettings.currentPhoto; }
                
                    //ctx.Users.Add(user);
                    //ctx.SaveChanges();
                    System.Windows.MessageBox.Show("Hello, Succuess! Please Login Now", "My App");
                }
            }
            //catch (SystemException ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    Environment.Exit(1); // fatal error
            //}
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
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

        private void Button_Search_Tearch_click(object sender, RoutedEventArgs e)
        {
            StudentSearchTeacherViewModel sstvmInstance = new StudentSearchTeacherViewModel();
            this.DataContext = sstvmInstance;
        }

        private void Button_Search_Course_Click(object sender, RoutedEventArgs e)
        {
            StudentSearchCoursViewModel regvmInstance = new StudentSearchCoursViewModel();
            this.DataContext = regvmInstance;
        }

        private void Button_Sign_Click(object sender, RoutedEventArgs e)
        {
            var loginDlg = new SignInDialog();
            loginDlg.Owner = this;
            if (loginDlg.ShowDialog() == true)
            {
                string email = loginDlg.tbEmail.Text;
                string password = loginDlg.tbPassword.Password;
                try
                {
                    YZYDbContextAzure ctx = new YZYDbContextAzure();
                    User loginUser = ctx.Users.ToList().Where(u => (u.Email == email && u.Password == password)).FirstOrDefault();
                    if (loginUser != null)
                    {
                        if (loginUser.UserRole == UserRoleEnum.Student)
                        {
                            GlobalSettings.userRole = UserRoleEnum.Student;
                            GlobalSettings.userID = loginUser.UserID;
                            btManageCourse.IsEnabled = true;
                            btProfile.IsEnabled = true;
                            btPayment.IsEnabled = true;
                            btLogin.Visibility = Visibility.Hidden;
                            User curUser = ctx.Users.Where(r => r.UserID == GlobalSettings.userID).FirstOrDefault();
                            tbWelcomeUsername.Text = "Welcome" +" " +curUser.FullName;
                        }
                        else
                        {
                            MessageBox.Show("You are not allowed to login from Student Desktop", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                    MessageBox.Show("Invalid Password or UserName, Please try again", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (SystemException ex)
                {
                    //System.Windows.MessageBox.Show("Hello, Succuess!", "My App");
                    Log.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
            }

            
        }

        private void ButtonPayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentManagementDialog dlg = new PaymentManagementDialog();
            dlg.Owner = this;
            dlg.ShowDialog();

        }

        private void ButtonManageCourse_Click(object sender, RoutedEventArgs e)
        {
            StudentCoursManageDialog dlg = new StudentCoursManageDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
        }

        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            StudentProfileDialog dlg = new StudentProfileDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
        }

    }


    public class GlobalSettings
    {
        public static UserRoleEnum userRole;
        public static int userID;
        public static byte[] currentPhoto;
        public static string newPassword;
    }
}



