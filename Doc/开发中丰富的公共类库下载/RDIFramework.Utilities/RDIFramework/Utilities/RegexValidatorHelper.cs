namespace RDIFramework.Utilities
{
    using System;
    using System.Text.RegularExpressions;

    public class RegexValidatorHelper
    {
        private static string LwkdeSkKg = "^[A-Za-z]+$";
        private static string string_0 = @"^\w+$";
        private static string string_1 = @"^([A-Za-z0-9\~\!\@\#\$\%\^\&\*\(\)_\+\|\`\-\=\\\<\>\?\,\.\/\:\;\{\}\[\]]){6,}$";
        private static string string_10 = "^-[0-9]*[1-9][0-9]*$";
        private static string string_11 = @"^\d+$";
        private static string string_12 = @"^((-\d+)|(0+))$";
        private static string string_13 = @"^(-?\d+)(\.\d+)?$";
        private static string string_14 = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
        private static string string_15 = @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$";
        private static string string_16 = @"^\d+(\.\d+)?$";
        private static string string_17 = @"^[\u4e00-\u9fa5]+$";
        private static string string_18 = @"[^\x00-\xff]+$";
        private static string string_19 = @"^[\d]+:[\w]{32}$";
        private static string string_2 = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
        private static string string_20 = @"^[\w]{32}$";
        private static string string_21 = @"^\d+(?:\.\d{0,2})?$";
        private static string string_22 = @"^13[\d]{9}$";
        private static string string_23 = @"^([\d]{3,5}-)?[\d]{7,8}$";
        private static string string_24 = "^[1-9][0-9]{4,}$";
        private static string string_25 = @"^[1-9]\d{5}(?!\d)$";
        private static string string_26 = "^[a-zA-Z0-9]+.(aspx|Aspx|aSpx|asPx|aspX|ASpx|AsPx|AspX|aSpX|aSPx|asPX|aSpX|ASPx|ASPX|ASpX|aSPX|AsPX|aSPX)$";
        private static string string_27 = @"^[\d]{4}(-|/)[\d]{1,2}(-|/)[\d]{1,2}$";
        private static string string_28 = @"^[A-Za-z0-9\u0391-\uFFE5]{1,20}$";
        private static string string_29 = @"^(,\d+)+$";
        private static string string_3 = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        private static string string_30 = "^[a-zA-Z0-9]{3,30}$";
        private static string string_31 = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+(,[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+)*$";
        private static string string_32 = "<.+?>";
        private static string string_33 = @"^[\w\u0391-\uFFE5]+(,[\w\u0391-\uFFE5]+){0,20}$";
        private static string string_34 = @"^[\d]{1,9}(\|[\d]{1,9})*$";
        private static string string_35 = @"^[\w\u0391-\uFFE5]+(;[\w\u0391-\uFFE5]+){0,10}$";
        private static string string_36 = @"^[\w\u0391-\uFFE5]{0,10}$";
        private static string string_37 = @"^[\d]+(\|[\d]+){0,19}$";
        private static string string_38 = @"^[\d]+,[\w\u0391-\uFFE5]+$";
        private static string string_39 = @"^\d{15}|\d{17}[X|x]$";
        private static string string_4 = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        private static string string_40 = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";
        private static string string_5 = @"^\w+$";
        private static string string_6 = "^[a-z]+$";
        private static string string_7 = "^[A-Z]+$";
        private static string string_8 = @"^-?\d{1,9}$";
        private static string string_9 = "^[0-9]*[1-9][0-9]*$";
        private static string TmuaJgId2 = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";

        public static string GetBudget2BitBackDot(string budget)
        {
            string str;
            Regex regex = new Regex(@"^(?<budget>\d+.\d{1,2})$");
            try
            {
                str = regex.Match(budget).Result("${budget}");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return str;
        }

        public static bool IsMatch(string input, Pattern pattern)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(input))
            {
                return flag;
            }
            switch (pattern)
            {
                case Pattern.ACCOUNT:
                    return Regex.IsMatch(input, string_0);

                case Pattern.PASSWORD:
                    return Regex.IsMatch(input, string_1);

                case Pattern.EMAIL:
                    return Regex.IsMatch(input, string_2);

                case Pattern.URL:
                    return Regex.IsMatch(input, string_3);

                case Pattern.NUM_CHAR_UNDERLINE:
                    return Regex.IsMatch(input, string_5);

                case Pattern.CHAR:
                    return Regex.IsMatch(input, LwkdeSkKg);

                case Pattern.SMALLCHAR:
                    return Regex.IsMatch(input, string_6);

                case Pattern.BIGCHAR:
                    return Regex.IsMatch(input, string_7);

                case Pattern.INTEGER:
                    return Regex.IsMatch(input, string_8);

                case Pattern.POSITIVE_INTEGER:
                    return Regex.IsMatch(input, string_9);

                case Pattern.NEGATIVE_INTEGER:
                    return Regex.IsMatch(input, string_10);

                case Pattern.NONPOSITIVE_INTEGER:
                    return Regex.IsMatch(input, string_11);

                case Pattern.NONNEGATIVE_INTEGER:
                    return Regex.IsMatch(input, string_12);

                case Pattern.FLOAT:
                    return Regex.IsMatch(input, string_13);

                case Pattern.POSITIVE_FLOAT:
                    return Regex.IsMatch(input, string_14);

                case Pattern.NEGATIVE_FLOAT:
                    return Regex.IsMatch(input, TmuaJgId2);

                case Pattern.NONPOSITIVE_FLOAT:
                    return Regex.IsMatch(input, string_15);

                case Pattern.NONNEGATIVE_FLOAT:
                    return Regex.IsMatch(input, string_16);

                case Pattern.CHINESE:
                    return Regex.IsMatch(input, string_17);

                case Pattern.DOUBLE_BYTE:
                    return Regex.IsMatch(input, string_18);

                case Pattern.RESENDEMAIL:
                    return Regex.IsMatch(input, string_19);

                case Pattern.VALIDATECODE:
                    return Regex.IsMatch(input, string_20);

                case Pattern.NUMBERWITHTOWPOINTS:
                    return Regex.IsMatch(input, string_21);

                case Pattern.MOBILEPHONE:
                    return Regex.IsMatch(input, string_22);

                case Pattern.GUDINGPHONE:
                    return Regex.IsMatch(input, string_23);

                case Pattern.QQ:
                    return Regex.IsMatch(input, string_24);

                case Pattern.ZIP:
                    return Regex.IsMatch(input, string_25);

                case Pattern.FILENAME:
                    return Regex.IsMatch(input, string_26);

                case Pattern.DATETIME:
                    return Regex.IsMatch(input, string_27);

                case Pattern.NICKNAME:
                    return Regex.IsMatch(input, string_28);

                case Pattern.USERLOVE:
                    return Regex.IsMatch(input, string_29);

                case Pattern.SPACENAME:
                    return Regex.IsMatch(input, string_30);

                case Pattern.EMAILS:
                    return Regex.IsMatch(input, string_31);

                case Pattern.HTMLLABLE:
                    return Regex.IsMatch(input, string_32);

                case Pattern.PROJECTTAG:
                    return Regex.IsMatch(input, string_33);

                case Pattern.SHAREID:
                    return Regex.IsMatch(input, string_34);

                case Pattern.AREANAME:
                    return Regex.IsMatch(input, string_35);

                case Pattern.TRACKURL:
                    return Regex.IsMatch(input, string_4);

                case Pattern.SIGHT:
                    return Regex.IsMatch(input, string_37);

                case Pattern.SINGLEAREANAME:
                    return Regex.IsMatch(input, string_36);

                case Pattern.OUTDOOR:
                    return Regex.IsMatch(input, string_38);

                case Pattern.IDCARDNUMBER18:
                    return Regex.IsMatch(input, string_39);

                case Pattern.IDCARDNUMBER15:
                    return Regex.IsMatch(input, string_40);
            }
            return false;
        }
    }
}

