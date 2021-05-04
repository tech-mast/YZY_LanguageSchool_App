using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YZYLibraryAzure;

namespace YZYAdminGUI
{
    public class ValidationRules
    {
        public static void checkCourseID(int value)
        {
            if (value <= 0)
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
        public static void checkCourseDesc(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length<2)
            {
                throw new InvalidParameterException(Properties.Resources.error_course_name + $": {value}");
            }
        }
        public static void checkCourseTuition(decimal value)
        {
            if (value <= 0)
            {
                throw new InvalidParameterException(Properties.Resources.error_course_tuition + $": {value}");
            }
        }
        public static void checkCourseDatetime(DateTime start, DateTime end)
        {
            if (DateTime.Compare(start.Date, end.Date)>=0 || start.Year<2000 || end.Year>2100)
            {
                throw new InvalidParameterException(Properties.Resources.error_course_date + $": {start.Date}, {end.Date}");
            }
        }

        public static void checkPostCode(string value)
        {
            //Canadian Postal Code in the format of "M3A 1A5"
            //string pattern = "^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$";
            string pattern = "^[A-Z]{1}[0-9]{1}[A-Z]{1}[ ]?[0-9]{1}[A-Z]{1}[0-9]{1}$";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (!(reg.IsMatch(value)))
            {
                throw new InvalidParameterException(Properties.Resources.error_user_postcode + $": {value}");
            }
        }
        public static void checkEmail(string value)
        {
            Regex pattern = new Regex(@"^([\w\.\-]+)@([\w\-\.]+)(\.(\w){2,3})$");
            if (!pattern.IsMatch(value) || String.IsNullOrWhiteSpace(value))
            {
                throw new InvalidParameterException(Properties.Resources.error_user_email + $": {value}");
            }
        }
        public static void checkGrade(string value)
        {
            Regex pattern = new Regex(@"^[A-F]{1}[\+\-]?$");
            if (String.IsNullOrWhiteSpace(value) || !pattern.IsMatch(value))
            {
                throw new InvalidParameterException(Properties.Resources.error_user_grade + $": {value}");
            }
        }
    }
}