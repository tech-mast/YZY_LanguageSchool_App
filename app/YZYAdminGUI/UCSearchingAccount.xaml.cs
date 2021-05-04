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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YZYAdminGUI
{
    /// <summary>
    /// Interaction logic for UCSearchingAccount.xaml
    /// </summary>
    public partial class UCSearchingAccount : UserControl
    {
        private SearchAccountViewModel _vmSearchAccount;
        public UCSearchingAccount()
        {
            InitializeComponent();

            _vmSearchAccount = new SearchAccountViewModel();
            this.DataContext = _vmSearchAccount;
        }
    }
}
