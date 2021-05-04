using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for StudentProfileDialog.xaml
    /// </summary>
    public partial class StudentProfileDialog : Window
    {
        YZYDbContextAzure ctx;
        private User curUser;
        private byte[] curPictureArr;
        private bool isPictureModified = false;
        private bool isPasswordModified = false;
        public StudentProfileDialog()
        {
            InitializeComponent();
        }


        private void LoadData()
        {
            try
            {
                using (ctx = new YZYDbContextAzure())
                {
                    //TODO set curUser = login user in the login dialog  
                    curUser = ctx.Users.Where(r => r.UserID == GlobalSettings.userID).FirstOrDefault();

                    tbNewCell.Text = curUser.Cell;
                    tbNewCity.Text = curUser.City;
                    tbNewEmail.Text = curUser.Email;
                    tbNewPassword.Text = curUser.Password;
                    tbNewGender.Text = Enum.GetName( typeof(GenderEnum),curUser.Gender);
                    tbNewPhone.Text = curUser.Phone;
                    tbNewPostalCode.Text = curUser.PostalCode;
                    tbNewProvince.Text = curUser.Province;
                    tbNewSIN.Text = curUser.UserSIN;
                    tbNewStreetName.Text = curUser.StreetName;
                    tbNewStreetNo.Text = curUser.StreetNo;
                    tbFirstName.Text = curUser.FName;
                    tbMiddleName.Text = curUser.MName;
                    tbLastName.Text = curUser.LName;
                    tbUserRole.Text =Enum.GetName(typeof(UserRoleEnum), curUser.UserRole);

                    if (curUser.Photo != null)
                    {
                        using (MemoryStream stream = new MemoryStream(curUser.Photo))
                        {
                            imageViewer.Source = BitmapFrame.Create(stream,
                                                             BitmapCreateOptions.None,
                                                             BitmapCacheOption.OnLoad);

                        }
                    }

                }
            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message);

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btPickPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    curPictureArr = File.ReadAllBytes(dlg.FileName);
                    tbImage.Visibility = Visibility.Hidden; // hide the text box
                                                            //System.Drawing.Image bitmap  = byteArrayToImage(currOwnerImage); // ex: SystemException

                    Image image = new Image();
                    using (MemoryStream stream = new MemoryStream(curPictureArr))
                    {
                        //To show the image on the component, instead of assign an image object to the source, need a use BitmapFrame.Create to create a new BitmapFrame
                        imageViewer.Source = BitmapFrame.Create(stream,
                                                         BitmapCreateOptions.None,
                                                         BitmapCacheOption.OnLoad);

                    }

                    isPictureModified = true;

                }
                catch (Exception ex) when (ex is SystemException || ex is IOException)
                {
                    MessageBox.Show(ex.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }



        private void btSubmit_Click(object sender, RoutedEventArgs e)
        {

            //validations
            List<string> errList = new List<string>();

            if (!Regex.IsMatch(tbNewSIN.Text, @"^[0-9]{9}$"))
            {
                errList.Add("SIN must be 9 numbers");
                
            }

            if (!Enum.TryParse(tbNewGender.Text, out GenderEnum gender))
            {
                errList.Add("Gender must be: Female, Male, Other or Unknown");
            }

            if (!Regex.IsMatch(tbNewStreetName.Text, @"^[\w\d\s\.\-]{2,50}$"))
            {
                errList.Add("Street name must be 2 - 50 characters");
            }

            if (!Regex.IsMatch(tbNewStreetNo.Text, @"^[\w\d\s\.\-]{2,50}$"))
            {
                errList.Add("Street number must be 2 - 50 characters");
            }

            if (!Regex.IsMatch(tbNewCity.Text, @"^[\w\d\s\.\-]{2,30}$"))
            {
                errList.Add("City must be 2 - 30 characters");
            }

            List<string> provinceList= new List<string>() { "QC", "ON", "BC", "NL", "PE", "NS", "NB", "MB", "SK", "AB", "YT", "NT", "NU" };
            
            if (!provinceList.Any(s => tbNewProvince.Text.ToUpper().Equals(s)))
            {
                errList.Add("Province must be a 2 characters  Canadian province abbreviation");

            }

            if (!Regex.IsMatch(tbNewPostalCode.Text, @"^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$"))
            {
                errList.Add("Postal code must be 6 character, in the format L0L0L0");
            }

            if (!Regex.IsMatch(tbNewPhone.Text, @"^[0-9]{10}$"))
            {
                errList.Add("Phone must be 10 numbers");
            }

            if (!Regex.IsMatch(tbNewCell.Text, @"^[0-9]{10}$"))
            {
                errList.Add("Cell must be 10 numbers");
            }

            if (!Regex.IsMatch(tbNewEmail.Text, @"^[\w\d\s\.\-\@]{2,20}$"))
            {
                errList.Add("Email must be less than 20 characters");
            }

            if (!Regex.IsMatch(tbNewPassword.Text, @"^[\w\d\s\.\-\@]{2,20}$"))
            {
                errList.Add("Password must be less than 20 characters");
            }


            if (errList.Count >0)
            {
                string errStr = "";

                foreach (string r in errList)
                {
                    errStr += $"{r}\n";
                }

                MessageBox.Show($"Error found:\n{errStr}");
                return;
            }

            //check if password and/or confirmed password have been edited, if yes check if they are matched
            if (isPasswordModified == true)
            {
                if (!tbNewPassword.Text.Equals(tbNewConfirmPassword.Text))
                {
                    MessageBox.Show("Password didn't matched the confirmed password. please check.");
                    return;
                }
            }

            try
            {

                using (ctx = new YZYDbContextAzure())
                {
                    User udUser = ctx.Users.Where(r => r.UserID == curUser.UserID).FirstOrDefault();


                  
                        udUser.Cell = tbNewCell.Text;
                        udUser.City = tbNewCity.Text;
                        udUser.Email = tbNewEmail.Text;
                        udUser.Password = tbNewPassword.Text;
                        udUser.Gender = gender;
                        udUser.Phone = tbNewPhone.Text;
                        udUser.PostalCode = tbNewPostalCode.Text;
                        udUser.Province = tbNewProvince.Text;
                        udUser.UserSIN = tbNewSIN.Text;
                        udUser.StreetName = tbNewStreetName.Text;
                        udUser.StreetNo = tbNewStreetNo.Text;
                    


                    if (isPictureModified == true)
                    {
                        udUser.Photo = curPictureArr;
                    }

                    ctx.SaveChanges();
                    MessageBox.Show("Profile updated");
                    this.InitializeComponent();
                    LoadData();
                }
               
            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message);
            }

            


        }

        private void tbNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            isPasswordModified = true;
        }
    }
}
