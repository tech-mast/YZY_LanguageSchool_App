using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for UCAdminProfile.xaml
    /// </summary>
    public partial class UCAdminProfile : UserControl
    {
        private ProfileViewModel _vmProfile;
        public UCAdminProfile()
        {
            InitializeComponent();
            _vmProfile = new ProfileViewModel();
            this.DataContext = _vmProfile;
            if(_vmProfile.LoginUser!=null && _vmProfile.LoginUser.Photo != null)
            {
                _vmProfile.Photo = _vmProfile.LoginUser.Photo;
                _photo = _vmProfile.LoginUser.Photo;
                showImage();
            }
        }
        private byte[] _photo = null;
        private BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void showImage()
        {
            if (_photo != null && _photo.Length > 0)
            {
                try
                {
                    tbImage.Visibility = Visibility.Hidden; // hide the text box
                    BitmapImage bitmap = ToImage(_photo); // ex: SystemException
                    imageViewer.Source = bitmap;
                }
                catch (SystemException ex)
                {
                    Log.WriteLine(ex.Message);
                }
            }

        }
        private void btPickPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _photo = File.ReadAllBytes(openFileDialog.FileName);
                    showImage();
                    _vmProfile.Photo = _photo;
                }
                catch (IOException ex)
                {
                    Log.WriteLine(ex.Message);
                }
            }
        }
    }
}
