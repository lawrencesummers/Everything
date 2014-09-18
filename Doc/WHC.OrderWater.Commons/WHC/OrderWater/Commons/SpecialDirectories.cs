namespace WHC.OrderWater.Commons
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Security;
    using System.Windows.Forms;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class SpecialDirectories
    {
        private static string smethod_0(string string_0, object object_0)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                throw new DirectoryNotFoundException(string.Format(CultureInfo.InvariantCulture, "The given file path ends with a directory separator character.", new object[] { object_0 }));
            }
            return smethod_1(smethod_4(Path.GetFullPath(string_0)));
        }

        private static string smethod_1(string string_0)
        {
            try
            {
                if (!smethod_3(string_0))
                {
                    DirectoryInfo info = new DirectoryInfo(smethod_2(string_0));
                    if (File.Exists(string_0))
                    {
                        return info.GetFiles(Path.GetFileName(string_0))[0].FullName;
                    }
                    if (Directory.Exists(string_0))
                    {
                        return info.GetDirectories(Path.GetFileName(string_0))[0].FullName;
                    }
                }
                return string_0;
            }
            catch (Exception exception)
            {
                if ((((!(exception is ArgumentException) && !(exception is ArgumentNullException)) && (!(exception is PathTooLongException) && !(exception is NotSupportedException))) && !(exception is DirectoryNotFoundException)) && (!(exception is SecurityException) && !(exception is UnauthorizedAccessException)))
                {
                    throw;
                }
                return string_0;
            }
        }

        private static string smethod_2(string string_0)
        {
            Path.GetFullPath(string_0);
            if (smethod_3(string_0))
            {
                throw new ArgumentException("path", string.Format(CultureInfo.InvariantCulture, "Could not get parent path since the given path is a root directory: '{0}'.", new string[] { string_0 }));
            }
            return Path.GetDirectoryName(string_0.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }));
        }

        private static bool smethod_3(string string_0)
        {
            if (!Path.IsPathRooted(string_0))
            {
                return false;
            }
            string_0 = string_0.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            return (string.Compare(string_0, Path.GetPathRoot(string_0), StringComparison.OrdinalIgnoreCase) == 0);
        }

        private static string smethod_4(string string_0)
        {
            if (Path.IsPathRooted(string_0) && string_0.Equals(Path.GetPathRoot(string_0), StringComparison.OrdinalIgnoreCase))
            {
                return string_0;
            }
            return string_0.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
        }

        private static string smethod_5(string string_0)
        {
            string str = @"{0}\{1}\{2}";
            string str2 = @"\{0}\{1}";
            string companyName = Application.CompanyName;
            string productName = Application.ProductName;
            string productVersion = Application.ProductVersion;
            if (companyName.Contains("Microsoft Corporation"))
            {
                return (string_0 + str2);
            }
            string path = smethod_6(str, new object[] { string_0, productName, productVersion });
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(productVersion);
            }
            return path;
        }

        private static string smethod_6(string string_0, params object[] args)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                return string.Empty;
            }
            if (args == null)
            {
                return string_0;
            }
            return string.Format(CultureInfo.CurrentCulture, string_0, args);
        }

        public string AllUsersApplicationData
        {
            get
            {
                return smethod_5(smethod_0(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "All users' application data"));
            }
        }

        public string CurrentUserApplicationData
        {
            get
            {
                return smethod_5(smethod_0(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Current user's application data"));
            }
        }

        public string Desktop
        {
            get
            {
                return smethod_0(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Desktop");
            }
        }

        public string MyDocuments
        {
            get
            {
                return smethod_0(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "My Documents");
            }
        }

        public string MyMusic
        {
            get
            {
                return smethod_0(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "My Music");
            }
        }

        public string MyPictures
        {
            get
            {
                return smethod_0(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "My Pictures");
            }
        }

        public string ProgramFiles
        {
            get
            {
                return smethod_0(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Program Files");
            }
        }

        public string Programs
        {
            get
            {
                return smethod_0(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "Programs");
            }
        }

        public string Temp
        {
            get
            {
                return smethod_0(Path.GetTempPath(), "Temporary directory");
            }
        }
    }
}

