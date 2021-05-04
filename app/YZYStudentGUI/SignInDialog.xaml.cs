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

namespace YZYStudentGUI
{
    /// <summary>
    /// Interaction logic for SignInDialog.xaml
    /// </summary>
    public partial class SignInDialog : Window
    {
        public SignInDialog()
        {
            InitializeComponent();
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "" || tbPassword.Password == "")
            {
                MessageBox.Show("Password or UserName could not be empty, Please try again", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
        }
    }
}
