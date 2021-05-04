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
using YZYLibraryAzure;

namespace YZYAdminGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Log.setLogOnFile();

            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var loginDlg = new AdminLoginDialog();
            if (loginDlg.ShowDialog() == true)
            {
                string email = loginDlg.tbEmail.Text;
                string password = loginDlg.tbPassword.Password;
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    GlobalSettings.userRole = UserRoleEnum.Student;
                    GlobalSettings.userID = -1;
                }
                else
                {
                    try
                    {
                        YZYDbContextAzure ctx = new YZYDbContextAzure();
                        User loginUser = ctx.Users.ToList().Where(u => (u.Email == email && u.Password == password)).FirstOrDefault();
                        if (loginUser != null)
                        {
                            if (loginUser.UserRole == UserRoleEnum.Admin)
                            {
                                GlobalSettings.userRole = UserRoleEnum.Admin;
                                GlobalSettings.userID = loginUser.UserID;
                            }
                            else if (loginUser.UserRole == UserRoleEnum.Teacher)
                            {
                                GlobalSettings.userRole = UserRoleEnum.Teacher;
                                GlobalSettings.userID = loginUser.UserID;
                            }
                            else
                            {
                                GlobalSettings.userRole = UserRoleEnum.Student;
                                GlobalSettings.userID = -1;
                            }
                        }
                    }
                    catch (SystemException ex)
                    {
                        Log.WriteLine(ex.Message);
                        Environment.Exit(1);
                    }
                }
            }
            else
            {
                GlobalSettings.userRole = UserRoleEnum.Student;
                GlobalSettings.userID = -1;
            }

            InitializeComponent();

            switch (ConfigurationManager.AppSettings["DefaultCulture"])
            {
                case "zh-Hans":
                    LanguageToggle.IsChecked = true;
                    break;
                case "en":
                    LanguageToggle.IsChecked = false;
                    break;
            }

            ucCourse.Visibility = Visibility.Hidden;
            ucAccount.Visibility = Visibility.Hidden;
            ucTeacher.Visibility = Visibility.Hidden;

            switch (GlobalSettings.userRole)
            {
                case UserRoleEnum.Admin:
                    ucCourse.Visibility = Visibility.Visible;
                    ucAccount.Visibility = Visibility.Visible;
                    break;
                case UserRoleEnum.Teacher:
                    ucTeacher.Visibility = Visibility.Visible;
                    break;
            }

        }


        private void Button_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        private void LanguageToggle_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("DefaultCulture");
            if (LanguageToggle.IsChecked == true)
            {
                config.AppSettings.Settings.Add("DefaultCulture", "zh-Hans");// TODO: to add selected language string replacing "FIXME"
            }
            else
            {
                config.AppSettings.Settings.Add("DefaultCulture", "en");// TODO: to add selected language string replacing "FIXME"
            }
            config.Save(ConfigurationSaveMode.Modified);
        }

    }

    public class GlobalSettings
    {
        public static UserRoleEnum userRole;
        public static int userID;
    }
}
