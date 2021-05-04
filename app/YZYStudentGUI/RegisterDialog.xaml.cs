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
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YZYLibraryAzure;
//using WebcamCamera;


namespace YZYStudentGUI
{
    /// <summary>
    /// Interaction logic for RegisterDialog.xaml
    /// </summary>
    public partial class RegisterDialog : Window
    {
        private StudentRegisterViewModel regvmInstance;
        public RegisterDialog()
        {
            InitializeComponent();
            GlobalSettings.currentPhoto = null;
            regvmInstance = new StudentRegisterViewModel();
            this.DataContext = regvmInstance;
        }




        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            List<string> registereErrorList = new List<string>();
            try
            {
                string msg;
                if (tbNewFirstName.Text == "")
                {
                    registereErrorList.Add("first name could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkFirstName(tbNewFirstName.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewMiddleName.Text == "")
                {
                }
                else
                {

                    if ((msg = StudentValidationRules.checkMiddleName(tbNewMiddleName.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewLastName.Text == "")
                {
                    registereErrorList.Add("last name could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkLastName(tbNewLastName.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewSIN.Text == "")
                {
                    registereErrorList.Add("SIN could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkSIN(tbNewSIN.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewEmail.Text == "")
                {
                    registereErrorList.Add("Email could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkEmail(tbNewEmail.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewPhone.Text == "")
                {
                }
                else
                {
                    if ((msg = StudentValidationRules.checkPhone(tbNewPhone.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewCell.Text == "")
                {
                    registereErrorList.Add("Cell no. could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkCell(tbNewCell.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewStreetNo.Text == "")
                {
                    registereErrorList.Add("Street no. could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkStreetNo(tbNewStreetNo.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewStreetName.Text == "")
                {
                    registereErrorList.Add("Street name could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkStreetName(tbNewStreetName.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewCity.Text == "")
                {
                    registereErrorList.Add("City name could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkCityName(tbNewCity.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewPostalCode.Text == "")
                {
                    registereErrorList.Add("Postal Code could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkPostalCode(tbNewPostalCode.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }

                if (tbNewPassword.Password == "")
                {
                    registereErrorList.Add("Password could not be empty");
                }

                if (tbNewConfirmPassword.Password == "")
                {
                    registereErrorList.Add("Comfirmed Password could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkPassword(tbNewPassword.Password, tbNewConfirmPassword.Password)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }
                if (tbNewProvince.Text == "")
                {
                    registereErrorList.Add("Province Password could not be empty");
                }
                else
                {
                    if ((msg = StudentValidationRules.checkProvince(tbNewProvince.Text)) != null)
                    {
                        registereErrorList.Add(msg);
                    }
                }

                if (registereErrorList.Count != 0)
                {
                    MessageBox.Show(this, string.Join("\n", registereErrorList), "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                GlobalSettings.newPassword = tbNewPassword.Password;

                DialogResult = true;

            }
            catch (InvalidParameterException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private byte[] _photo;
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
                    GlobalSettings.currentPhoto = _photo;
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
            //if (MessageBox.Show("Do you want to capture picture from camera?","Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //{
            //    byte[] im = null;
            //    using (Camera camera = new Camera())
            //    {
            //        System.Windows.Forms.DialogResult dr = camera.ShowDialog();
            //        if (dr == System.Windows.Forms.DialogResult.OK)
            //        {
            //            im = camera.CameraPhoto;
            //            Console.WriteLine("this is byte" + im.ToString());
            //        }
            //    }

            //    try
            //    {
            //        _photo = im;
            //        showImage();
            //    }
            //    catch (IOException ex)
            //    {
            //        Log.WriteLine(ex.Message);
            //    }

            //    imageViewer.Source = ToImage(im);
            
            //}
            //else
            //{

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
            //}
        }
        public byte[] Photo
        {
            get
            {
                return _photo;
            }
        }
    }
}
