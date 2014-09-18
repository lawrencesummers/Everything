namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class Reflect<T> where T: class
    {
        private static Hashtable Ndfmvplawo;

        static Reflect()
        {
            Reflect<T>.Ndfmvplawo = null;
        }

        public static T Create(string sName, string sFilePath)
        {
            return Reflect<T>.Create(sName, sFilePath, true);
        }

        public static T Create(string sName, string sFilePath, bool bCache)
        {
            string key = sName;
            T local = default(T);
            if (bCache)
            {
                local = (T) Reflect<T>.ObjCache[key];
                if (!Reflect<T>.ObjCache.ContainsKey(key))
                {
                    local = (T) Reflect<T>.CreateAssembly(sFilePath).CreateInstance(key);
                    Reflect<T>.ObjCache.Add(key, local);
                }
                return local;
            }
            return (T) Reflect<T>.CreateAssembly(sFilePath).CreateInstance(key);
        }

        public static Assembly CreateAssembly(string sFilePath)
        {
            Assembly assembly = (Assembly) Reflect<T>.ObjCache[sFilePath];
            if (assembly == null)
            {
                assembly = Assembly.Load(sFilePath);
                if (!Reflect<T>.ObjCache.ContainsKey(sFilePath))
                {
                    Reflect<T>.ObjCache.Add(sFilePath, assembly);
                }
            }
            return assembly;
        }

        public static Hashtable ObjCache
        {
            get
            {
                if (Reflect<T>.Ndfmvplawo == null)
                {
                    Reflect<T>.Ndfmvplawo = new Hashtable();
                }
                return Reflect<T>.Ndfmvplawo;
            }
        }
    }
}

