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
using YZYLibraryAzure;

namespace YZYAdminGUI
{
    /// <summary>
    /// Interaction logic for UCSearchingCourse.xaml
    /// </summary>
    public partial class UCSearchingCourse : UserControl
    {
        private SearchCourseViewModel _vmSearchCourse;
        public UCSearchingCourse()
        {
            InitializeComponent();

            _vmSearchCourse = new SearchCourseViewModel();
            this.DataContext = _vmSearchCourse;
        }
    }
}
