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
using System.Windows.Shapes;
using YZYLibraryAzure;

namespace YZYAdminGUI
{
    /// <summary>
    /// Interaction logic for AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        public AddUserDialog(bool isAddNew = true)
        {
            InitializeComponent();
            if(isAddNew == true)
            {
                tbDialogTitle.Text = Properties.Resources.content_adduser_dialog;
            }
            else
            {
                tbDialogTitle.Text = Properties.Resources.content_edituser_dialog;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidationRules.checkEmail(tbNewEmail.Text);
                ValidationRules.checkPostCode(tbNewPostalCode.Text);
                DialogResult = true;
            }
            catch (InvalidParameterException ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.windows_title, MessageBoxButton.OK, MessageBoxImage.Error);
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
                }
                catch (IOException ex)
                {
                    Log.WriteLine(ex.Message);
                }
            }
        }
        public byte[] Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                _photo = value;
                showImage();
            }
        }
    }
}
