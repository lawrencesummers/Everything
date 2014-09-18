namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class Cache
    {
        private static object object_0 = new object();
        private static volatile Cache oToEdZxxmL = null;
        private SortedDictionary<string, object> sortedDictionary_0 = new SortedDictionary<string, object>();

        private Cache()
        {
        }

        public void Add(string key, object value)
        {
            this.sortedDictionary_0.Add(key, value);
        }

        public void Remove(string key)
        {
            this.sortedDictionary_0.Remove(key);
        }

        public static Cache Instance
        {
            get
            {
                if (oToEdZxxmL == null)
                {
                    lock (object_0)
                    {
                        if (oToEdZxxmL == null)
                        {
                            oToEdZxxmL = new Cache();
                        }
                    }
                }
                return oToEdZxxmL;
            }
        }

        public object this[string index]
        {
            get
            {
                if (this.sortedDictionary_0.ContainsKey(index))
                {
                    return this.sortedDictionary_0[index];
                }
                return null;
            }
            set
            {
                this.sortedDictionary_0[index] = value;
            }
        }
    }
}

