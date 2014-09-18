namespace RDIFramework.Utilities
{
    using System;

    public class EnumDescription : Attribute
    {
        private string string_0;

        public EnumDescription(string text)
        {
            this.string_0 = text;
        }

        public string Text
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

