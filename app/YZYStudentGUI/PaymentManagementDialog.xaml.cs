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
    /// Interaction logic for PaymentManagementDialog.xaml
    /// </summary>
    public partial class PaymentManagementDialog : Window
    {
        YZYDbContextAzure ctx;
        private User curUser;
        public PaymentManagementDialog()
        {

            try
            {
                using (ctx = new YZYDbContextAzure())
                {
                    //TODO set curUser = login user in the login dialog  
                    curUser = ctx.Users.Where(r => r.UserID == GlobalSettings.userID).FirstOrDefault();
                }

            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message); ;
            }

            InitializeComponent();

        }

        private void LoadData()
        {
            try
            {
                using (ctx = new YZYDbContextAzure())
                {

                    decimal totalTuition;
                    decimal paidTuition;

                    if (ctx.Registers.Where(r => r.UserID == curUser.UserID).FirstOrDefault() == null)
                    {
                        totalTuition = 0;
                    }
                    else
                    {
                        totalTuition = ctx.Registers.Include("Course").Where(r => r.UserID == curUser.UserID).Sum(r => r.Cours.Tuition);
                    }
                    if (ctx.Payments.Where(r => r.UserID == curUser.UserID).FirstOrDefault() == null)
                    {
                        paidTuition = 0;
                    }
                    else
                    {
                        paidTuition = ctx.Payments.Where(r => r.UserID == curUser.UserID).Sum(r => r.Amount);
                    }



                    decimal balance = totalTuition - paidTuition;

                    lbTuitionTotal.Content = totalTuition==0?"0":String.Format("{0:.##}", totalTuition);
                    lbPaidTuition.Content = paidTuition==0?"0":String.Format("{0:.##}", paidTuition);
                    lbBalance.Content = balance == 0?"0":String.Format("{0:.##}", balance);

                    lvPayments.ItemsSource = ctx.Payments.Where(r => r.UserID == curUser.UserID).OrderBy(p => p.PayDate).ToList();
                    
                }
            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message);
                
            }
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> errList = new List<string>();
            if (curUser == null)
            {
                return;
            }

            if (tbPayAccount.Text.Length < 5 || tbPayAccount.Text.Length>30)
            {
                errList.Add("Account must be 5 -30 characters");
            }


            if (tbAmount.Text.Length == 0)
            {
                errList.Add("Account can not be empty");
            }


                if (!decimal.TryParse(tbAmount.Text, out decimal amount) && tbAmount.Text.Length>0)
                {
                    errList.Add("Account must be a valid number");
                }


                if (errList.Count>0)
            {
                string errStr="";

                foreach (string r in errList)
                {
                    errStr += $"{r}\n";
                }

                MessageBox.Show($"Error found:\n{errStr}");
                return;
            }


                using (YZYDbContextAzure ctx = new YZYDbContextAzure())
                {
                    Payment payment = new Payment { UserID = curUser.UserID, PayAccount = tbPayAccount.Text, Amount = amount, PayDate = DateTime.Today };
                    ctx.Payments.Add(payment);
                    ctx.SaveChanges();
                    this.InitializeComponent();
                    LoadData();
                    ClearField();
                }
            }
            catch (SystemException ex)
            {

                MessageBox.Show($"Fetal Error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private void ClearField()
        {
            tbPayAccount.Text = "";
            tbAmount.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //can't put loaddata in the custructor, otherwise will cause 
            LoadData();
        }
    }
}
