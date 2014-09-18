namespace WHC.OrderWater.Commons
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class Singleton<T> where T: class
    {
        private static volatile T gparam_0;
        private static object object_0;

        static Singleton()
        {
            Singleton<T>.object_0 = new object();
        }

        public static T Instance
        {
            get
            {
                if (Singleton<T>.gparam_0 == null)
                {
                    lock (Singleton<T>.object_0)
                    {
                        if (Singleton<T>.gparam_0 == null)
                        {
                            ConstructorInfo info = null;
                            try
                            {
                                info = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
                            }
                            catch (Exception exception)
                            {
                                throw new InvalidOperationException(exception.Message, exception);
                            }
                            if ((info == null) || info.IsAssembly)
                            {
                                throw new InvalidOperationException(string.Format("在'{0}'里面没有找到private或者protected的构造函数。", typeof(T).Name));
                            }
                            Singleton<T>.gparam_0 = (T) info.Invoke(null);
                        }
                    }
                }
                return Singleton<T>.gparam_0;
            }
        }
    }
}

