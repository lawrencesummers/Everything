namespace WHC.OrderWater.Commons.Collections
{
    using System;
    using System.Collections.Generic;
    using WHC.OrderWater.Commons;

    public class CollectionHelper<T> where T: class
    {
        public static List<T> Fill(int pID, int level, List<T> list, string pidName, string idName, string name)
        {
            List<T> list2 = new List<T>();
            foreach (T local in list)
            {
                int property = (int) ReflectionUtil.GetProperty(local, pidName);
                int num2 = (int) ReflectionUtil.GetProperty(local, idName);
                string str = ReflectionUtil.GetProperty(local, name) as string;
                if (pID == property)
                {
                    string str2 = new string('　', level * 2) + str;
                    ReflectionUtil.SetProperty(local, name, str2);
                    list2.Add(local);
                    list2.AddRange(CollectionHelper<T>.Fill(num2, level + 1, list, pidName, idName, name));
                }
            }
            return list2;
        }

        public static List<T> Fill(string pID, int level, List<T> list, string pidName, string idName, string name)
        {
            List<T> list2 = new List<T>();
            foreach (T local in list)
            {
                string property = (string) ReflectionUtil.GetProperty(local, pidName);
                string str2 = (string) ReflectionUtil.GetProperty(local, idName);
                string str3 = ReflectionUtil.GetProperty(local, name) as string;
                if (pID == property)
                {
                    string str4 = new string('　', level * 2) + str3;
                    ReflectionUtil.SetProperty(local, name, str4);
                    list2.Add(local);
                    list2.AddRange(CollectionHelper<T>.Fill(str2, level + 1, list, pidName, idName, name));
                }
            }
            return list2;
        }
    }
}

