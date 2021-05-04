using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YZYLibraryAzure;

namespace YZYStudentGUI
{
    public class StudentValidationRules
    {
        public static string checkFirstName(string value)
        {

            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,50}$") || String.IsNullOrWhiteSpace(value))
            {
                return "first name should be 2-50 charactor only";
            }
            else
            {
                return null;
            }
        }       
        public static string checkMiddleName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,50}$") || String.IsNullOrWhiteSpace(value))
            {
                return "middle name should be 2-50 charactor only";
            }
            else
            {
                return null;
            }
        }     
        public static string checkLastName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,50}$") || String.IsNullOrWhiteSpace(value))
            {
                return "last name should be 2-50 charactor only";
            }
            else
            {
                return null;
            }
        }       
        public static string checkSIN(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]{9}$"))
            {
                return "SIN nine digis only";
            }
            else
            {
                return null;
            }
        }
        public static string checkStreetNo(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]+$"))
            {
                return "street no. should be digis only";
            }
            else
            {
                return null;
            }
        }
        public static string checkStreetName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,50}$") || String.IsNullOrWhiteSpace(value))
            {
                return "street name should be 2-50 charactor only";
            }
            else
            {
                return null;
            }
        }
        public static string checkCityName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,30}$") || String.IsNullOrWhiteSpace(value))
            {
                return "city name should be 2-30 charactor only";
            }
            else
            {
                return null;
            }
        }
        public static string checkPostalCode(string value)
        {

            if (!Regex.IsMatch(value, @"^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$"))
            {
                return "postal code format is incorrect";
            }
            else
            {
                return null;
            }
        }
        public static string checkPhone(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]{10}$"))
            {
                return "phone no. should be 10 digits only";
            }
            else
            {
                return null;
            }
        }       
        public static string checkCell(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]{10}$"))
            {
                return "cell no. should be 10 digits only";
            }
            else
            {
                return null;
            }
        }
        public static string checkEmail(string value)
        {
            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return "email format is not correct";
            }
            else
            {
                return null;
            }
        }
        public static string checkPassword(string value1, string value2)
        {
            if (!string.Equals(value1, value2))
            {
                return "confirmed password should be same as password";
            }
            else
            {
                return null;
            }
        }
        public static string checkProvince(string value)
        {
            HashSet<string> mySet = new HashSet<string>();
            mySet.Add("QC");
            mySet.Add("ON");
            mySet.Add("BC");
            mySet.Add("NL");
            mySet.Add("PE");
            mySet.Add("NS");
            mySet.Add("NB");
            mySet.Add("MB");
            mySet.Add("SK");
            mySet.Add("AB");
            mySet.Add("YT");
            mySet.Add("NT");
            mySet.Add("NU");

            if (!mySet.Contains(value))
            {
                return "QC, ON, BC, NL, PE, NS, NB, MB, SK, AB, YT, NT, NU only";
            }
            else
            {
                return null;
            }
        }

    }
}
