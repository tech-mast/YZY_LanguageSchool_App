using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace YZYLibraryAzure
{

    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string msg) : base(msg)
        {
        }
    }
    public class Log
    {
        static string logFileName = @"../../log.txt";
        public delegate void LogFailedSetterDelegate(string reason);
        static void logNothing(string reason) { }
        static void logOnConsole(string reason)
        {
            Console.WriteLine(reason);
            //Debug.WriteLine(reason);
        }
        static void logOnFile(string reason)
        {
            try
            {
                if (File.Exists(logFileName))
                {
                    using (StreamWriter sw = File.AppendText(logFileName))
                    {
                        sw.WriteLine(reason);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(logFileName))
                    {
                        sw.WriteLine(reason);
                    }
                }
            }
            catch (Exception ex)
                when ((ex is IOException) ||
                      (ex is UnauthorizedAccessException) || (ex is SecurityException) ||
                      (ex is ArgumentException) || (ex is PathTooLongException) ||
                      (ex is DirectoryNotFoundException) || (ex is NotSupportedException))
            {
                Debug.WriteLine(ex.Message);
            }
        }

        static bool setLogFilePathName(string fileName)
        {
            if (!String.IsNullOrEmpty(fileName) && !String.IsNullOrWhiteSpace(fileName))
            {
                logFileName = fileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static LogFailedSetterDelegate WriteLine = logNothing;

        public static void setLogOnConsole()
        {
            WriteLine = logOnConsole;
        }
        public static void setLogOnFile()
        {
            WriteLine = logOnFile;
        }
        public static void setLogOnFile(string fileName)
        {
            if (!setLogFilePathName(fileName))
            {
                Debug.WriteLine("file name cannot be empty or blank");
            }
            WriteLine = logOnFile;
        }
        public static void setNoLog()
        {
            WriteLine = logNothing;
        }
    }
}
