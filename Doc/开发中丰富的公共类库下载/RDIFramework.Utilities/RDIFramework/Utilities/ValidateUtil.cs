namespace RDIFramework.Utilities
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    public class ValidateUtil
    {
        public static bool CheckEmail(string email)
        {
            bool flag = true;
            if (email.Trim().Length == 0)
            {
                return true;
            }
            Regex regex = new Regex(@"[\w-]+@([\w-]+\.)+[\w-]+");
            if (!regex.IsMatch(email))
            {
                flag = false;
            }
            return flag;
        }

        public static bool CheckFileName(string fileName)
        {
            bool flag = true;
            if (fileName.Trim().Length == 0)
            {
                return false;
            }
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                flag = false;
            }
            return flag;
        }

        public static bool CheckFolderName(string folderName)
        {
            bool flag = true;
            if (folderName.Trim().Length == 0)
            {
                return false;
            }
            if ((folderName.IndexOfAny(Path.GetInvalidPathChars()) >= 0) || (folderName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0))
            {
                flag = false;
            }
            return flag;
        }

        public static bool EnableCheckPasswordStrength(string password)
        {
            bool flag = true;
            if (string.IsNullOrEmpty(password))
            {
                flag = false;
            }
            bool flag2 = false;
            bool flag3 = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (!flag2)
                {
                    flag2 = char.IsDigit(password[i]);
                }
                if (!flag3)
                {
                    flag3 = char.IsLetter(password[i]);
                }
            }
            flag = flag2 && flag3;
            if (password.Length < 6)
            {
                flag = false;
            }
            return flag;
        }

        public static bool IsBlank(string strInput)
        {
            return string.IsNullOrEmpty(strInput);
        }

        public static bool IsDateTime(string strDate)
        {
            Regex regex = new Regex(@"(((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9]))|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29))|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)))((\s+(0?[1-9]|1[012])(:[0-5]\d){0,2}(\s[AP]M))?$|(\s+([01]\d|2[0-3])(:[0-5]\d){0,2})?$))");
            return regex.IsMatch(strDate);
        }

        public static bool IsEmail(string email)
        {
            email = email.Trim();
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return regex.IsMatch(email);
        }

        public static bool IsNumeric(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                return false;
            }
            Regex regex = new Regex(@"^[-]?\d+[.]?\d*$");
            return regex.IsMatch(strInput);
        }
    }
}

