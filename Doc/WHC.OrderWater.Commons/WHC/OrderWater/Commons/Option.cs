namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Option
    {
        [CompilerGenerated]
        private List<string> list_0;
        [CompilerGenerated]
        private string string_0;

        public Option(string Text, string OptionStarter)
        {
            if (string.IsNullOrEmpty(Text))
            {
                throw new ArgumentNullException("Text");
            }
            if (string.IsNullOrEmpty(OptionStarter))
            {
                throw new ArgumentNullException("OptionStarter");
            }
            Regex regex = new Regex(string.Format(@"{0}(?<Command>[^\s]*)\s(?<Parameters>.*)", OptionStarter));
            Regex regex2 = new Regex("(?<Parameter>\"[^\"]*\")[\\s]?|(?<Parameter>[^\\s]*)[\\s]?");
            this.Parameters = new List<string>();
            Match match = regex.Match(Text);
            this.Command = match.Groups["Command"].Value;
            Text = match.Groups["Parameters"].Value;
            foreach (Match match2 in regex2.Matches(Text))
            {
                if (!string.IsNullOrEmpty(match2.Value))
                {
                    this.Parameters.Add(match2.Groups["Parameter"].Value);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("Command: ").Append(this.Command).Append("\n").Append("Parameters: ");
            this.Parameters.ForEach(delegate (string x) {
                Builder.Append(x).Append(" ");
            });
            return Builder.Append("\n").ToString();
        }

        public virtual string Command
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

        public virtual List<string> Parameters
        {
            [CompilerGenerated]
            get
            {
                return this.list_0;
            }
            [CompilerGenerated]
            set
            {
                this.list_0 = value;
            }
        }
    }
}

