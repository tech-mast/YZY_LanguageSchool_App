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

namespace YZYAdminGUI
{
    /// <summary>
    /// Interaction logic for TeacherManagementDialog.xaml
    /// </summary>
    public partial class TeacherManagementDialog : Window
    {
        private TeacherDialogViewModel _vmTeacherDialog;
        public TeacherManagementDialog(int selectedCourseID)
        {
            InitializeComponent();
            _vmTeacherDialog = new TeacherDialogViewModel(selectedCourseID);
            this.DataContext = _vmTeacherDialog;
        }
    }
}
