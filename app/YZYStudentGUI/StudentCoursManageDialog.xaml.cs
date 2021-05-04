using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YZYLibraryAzure;

namespace YZYStudentGUI
{
    /// <summary>
    /// Interaction logic for StudentCoursManageDialog.xaml
    /// </summary>
    public partial class StudentCoursManageDialog : Window
    {
        YZYDbContextAzure ctx;
        private User curUser;
        private vStudentRegister curRegister;
        private vOpenCours curOpenCour;
        public StudentCoursManageDialog()
        {



            InitializeComponent();

            btAddCourse.IsEnabled = false;
            btDeleteRegister.IsEnabled = false;


        }


        private void LoadData()
        {
            try
            {
                using (ctx = new YZYDbContextAzure())
                {
                    //TODO set curUser = login user in the login dialog  
                    curUser = ctx.Users.Where(r => r.UserID == GlobalSettings.userID).FirstOrDefault();
                  


                    
                    lvRegisters.ItemsSource = ctx.vStudentRegisters.Where(r => r.UserID == curUser.UserID).OrderBy(c=>c.StartDate).ToList();
                    lvOpenCourses.ItemsSource = ctx.vOpenCourses.OrderBy(r => r.StartDate).ToList();

                }
            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message);

            }
        }

        private void lvRegisters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRegisters.SelectedItem == null)
            {
                return;
            }
            try
            {

                btAddCourse.IsEnabled = false;
                btDeleteRegister.IsEnabled = true;
                vStudentRegister selRecord = (vStudentRegister)lvRegisters.SelectedItem;
                lbCourseID.Content = selRecord.CourseID;
                lbCateDesc.Content = selRecord.CateDesc;
                lbCourseDesc.Content = selRecord.CourseDesc;
                lbTeacher.Content = selRecord.Teacher;
                lbStartDate.Content = selRecord.StartDateStr;
                lbEndDate.Content = selRecord.EndDateStr;
                lbTuition.Content = selRecord.TuitionStr;
                curRegister = selRecord;


                curRegister = selRecord;
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void lvOpenCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvOpenCourses.SelectedItem == null)
            {
                return;
            }

            try
            {

                btAddCourse.IsEnabled = true;
                btDeleteRegister.IsEnabled = false;
                vOpenCours selRecord = (vOpenCours)lvOpenCourses.SelectedItem;
                lbCourseID.Content = selRecord.CourseID;
                lbCateDesc.Content = selRecord.CateDesc;
                lbCourseDesc.Content = selRecord.CourseDesc;
                lbTeacher.Content= selRecord.Teacher;
                lbStartDate.Content = selRecord.StartDateStr;
                lbEndDate.Content = selRecord.EndDateStr;
                lbTuition.Content = selRecord.TuitionStr;
                curOpenCour = selRecord;
                
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Failed to connect to database: " + ex.Message);
            }
        }

        private void ButtonAddCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ctx = new YZYDbContextAzure())
                {
                    Register existRecord = ctx.Registers.Where(r => r.UserID == curUser.UserID && r.CourseID == curOpenCour.CourseID).FirstOrDefault();
                    if (existRecord != null)
                    {
                        MessageBox.Show("You have registered this course before.");
                        return;
                    }

                    Register newRecord = new Register { UserID = curUser.UserID, CourseID = curOpenCour.CourseID, RegisterStatus = RegisterStatusEnum.Pending };
                    ctx.Registers.Add(newRecord);
                    ctx.SaveChanges();
                    this.InitializeComponent();
                    LoadData();
                    ClearField();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Failed to connect to database: " + ex.Message);

            }
            
        }

        private void ButtonDeleteRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ctx = new YZYDbContextAzure())
                {

                    Register existRecord = ctx.Registers.Where(r => r.RegisterID == curRegister.RegisterID).FirstOrDefault();
                    ctx.Registers.Remove(existRecord);
                    ctx.SaveChanges();
                    this.InitializeComponent();
                    LoadData();
                    ClearField();
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Failed to connect to database: " + ex.Message);

            }
        }

        private void ClearField()
        {
            lbCourseID.Content = "";
            lbCateDesc.Content = "";
            lbCourseDesc.Content = "";
            lbTeacher.Content = "";
            lbStartDate.Content = "";
            lbEndDate.Content = "";
            lbTuition.Content = "";
        }


        //right click listview to clear selection
        private void lvRegisters_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (lvRegisters.SelectedItem == null)
            {
                return;
            }
            else
            {
                ClearField();
                lvRegisters.SelectedItems.Clear();
                lvOpenCourses.SelectedItems.Clear();
            }
        }

        private void lvOpenCourses_RightClick(object sender, MouseButtonEventArgs e)
        {

            if (lvOpenCourses.SelectedItem == null)
            {
                return;
            }
            else
            {
                ClearField();
                lvRegisters.SelectedItems.Clear();
                lvOpenCourses.SelectedItems.Clear();
            }
        }
    }
}
