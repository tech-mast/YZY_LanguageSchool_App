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

            InitializeComponent();

            btManageCourse.IsEnabled = false;
            btProfile.IsEnabled = false;
            btPayment.IsEnabled = false;
            tbWelcomeUsername.Text = "";
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
         
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

                    System.Windows.MessageBox.Show("Hello, Succuess! Please Login Now", "My App");
                }
            }
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



