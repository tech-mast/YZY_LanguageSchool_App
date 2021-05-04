using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using YZYLibraryAzure;

namespace YZYAdminGUI
{
    /// <summary>
    /// Interaction logic for UCTeacher.xaml
    /// </summary>
    public partial class UCTeacher : UserControl
    {

        private TeacherViewModel _vmTeacher;
        public UCTeacher()
        {
            InitializeComponent();
            _vmTeacher = new TeacherViewModel();
            this.DataContext = _vmTeacher;
        }
    }
}
