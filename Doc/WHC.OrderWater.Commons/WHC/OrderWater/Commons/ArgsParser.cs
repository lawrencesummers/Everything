namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class ArgsParser
    {
        [CompilerGenerated]
        private Regex regex_0;
        [CompilerGenerated]
        private string string_0;

        public ArgsParser() : this("/")
        {
        }

        public ArgsParser(string OptionStarter)
        {
            if (string.IsNullOrEmpty(OptionStarter))
            {
                throw new ArgumentNullException("OptionStarter");
            }
            this.OptionStarter = OptionStarter;
            this.OptionRegex = new Regex(string.Format("(?<Command>{0}[^\\s]+)[\\s|\\S|$](?<Parameter>\"[^\"]*\"|[^\"{0}]*)", OptionStarter));
        }

        public virtual List<Option> Parse(string[] Args)
        {
            if (Args == null)
            {
                return new List<Option>();
            }
            List<Option> list2 = new List<Option>();
            string input = "";
            string str2 = "";
            foreach (string str3 in Args)
            {
                input = input + str2 + str3;
                str2 = " ";
            }
            MatchCollection matchs = this.OptionRegex.Matches(input);
            string str4 = "";
            foreach (Match match in matchs)
            {
                if (!(!match.Value.StartsWith(this.OptionStarter) || string.IsNullOrEmpty(str4)))
                {
                    list2.Add(new Option(str4, this.OptionStarter));
                    str4 = "";
                }
                str4 = str4 + match.Value + " ";
            }
            list2.Add(new Option(str4, this.OptionStarter));
            return list2;
        }

        protected virtual Regex OptionRegex
        {
            [CompilerGenerated]
            get
            {
                return this.regex_0;
            }
            [CompilerGenerated]
            set
            {
                this.regex_0 = value;
            }
        }

        protected virtual string OptionStarter
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }
    }
}

