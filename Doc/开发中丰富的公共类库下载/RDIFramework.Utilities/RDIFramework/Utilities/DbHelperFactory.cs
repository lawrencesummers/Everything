namespace RDIFramework.Utilities
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class DbHelperFactory
    {
        public static IDbProvider GetHelper(string connectionString)
        {
            return GetHelper(CurrentDbType.SqlServer, connectionString);
        }

        public static IDbProvider GetHelper(CurrentDbType dbType = 1, string connectionString = null)
        {
            string dbHelperClass = BusinessLogic.GetDbHelperClass(dbType);
            IDbProvider provider = (IDbProvider) Assembly.Load(SystemInfo.DbHelperAssmely).CreateInstance(dbHelperClass, true);
            if (!string.IsNullOrEmpty(connectionString))
            {
                provider.ConnectionString = connectionString;
            }
            return provider;
        }
    }
}

