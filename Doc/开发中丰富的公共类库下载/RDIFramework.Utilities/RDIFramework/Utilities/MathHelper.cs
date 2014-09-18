namespace RDIFramework.Utilities
{
    using System;
    using System.Text.RegularExpressions;

    public class MathHelper
    {
        public static bool IsDecimal(string s)
        {
            try
            {
                decimal.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsInteger(string Input)
        {
            if (Input == null)
            {
                return false;
            }
            return IsInteger(Input, true);
        }

        public static bool IsInteger(string Input, bool Plus)
        {
            if (Input == null)
            {
                return false;
            }
            string pattern = "^-?[0-9]+$";
            if (Plus)
            {
                pattern = "^[0-9]+$";
            }
            return Regex.Match(Input, pattern, RegexOptions.Compiled).Success;
        }

        public static int? StringCastToInteger(object objValue)
        {
            if (objValue == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(objValue.ToString().Trim()))
            {
                return null;
            }
            try
            {
                return new int?(int.Parse(objValue.ToString().Trim()));
            }
            catch
            {
                return null;
            }
        }

        public static decimal? ToDecimal(string strValue)
        {
            if (string.IsNullOrEmpty(strValue.Trim()))
            {
                return null;
            }
            try
            {
                return new decimal?(decimal.Parse(strValue.Trim()));
            }
            catch
            {
                return null;
            }
        }

        public static int? ToInteger(string strValue)
        {
            if (string.IsNullOrEmpty(strValue.Trim()))
            {
                return null;
            }
            try
            {
                return new int?(int.Parse(strValue.Trim()));
            }
            catch
            {
                return null;
            }
        }
    }
}

