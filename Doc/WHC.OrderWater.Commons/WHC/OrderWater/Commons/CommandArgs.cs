namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;

    public class CommandArgs
    {
        private Dictionary<string, string> dictionary_0 = new Dictionary<string, string>();
        private List<string> list_0 = new List<string>();

        public Dictionary<string, string> ArgPairs
        {
            get
            {
                return this.dictionary_0;
            }
        }

        public List<string> Params
        {
            get
            {
                return this.list_0;
            }
        }
    }
}

