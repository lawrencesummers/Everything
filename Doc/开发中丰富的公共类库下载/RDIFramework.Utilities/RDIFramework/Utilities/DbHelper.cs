namespace RDIFramework.Utilities
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;

    public class DbHelper
    {
        public static string DbConnection = SystemInfo.BusinessDbConnection;
        private static DbProviderFactory dbProviderFactory_0 = null;
        public static RDIFramework.Utilities.CurrentDbType DbType = SystemInfo.BusinessDbType;
        private static readonly IDbProvider idbProvider_0 = DbHelperFactory.GetHelper(DbType, DbConnection);

        private DbHelper()
        {
        }

        public static int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters = null, CommandType commandType = 1)
        {
            int num = 0;
            idbProvider_0.Open(DbConnection);
            num = idbProvider_0.ExecuteNonQuery(commandText, dbParameters, commandType);
            idbProvider_0.Close();
            return num;
        }

        public static IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters = null, CommandType commandType = 1)
        {
            idbProvider_0.Open(DbConnection);
            idbProvider_0.AutoOpenClose = true;
            return idbProvider_0.ExecuteReader(commandText, dbParameters, commandType);
        }

        public static object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters = null, CommandType commandType = 1)
        {
            object obj2 = null;
            idbProvider_0.Open(DbConnection);
            obj2 = idbProvider_0.ExecuteScalar(commandText, dbParameters, commandType);
            idbProvider_0.Close();
            return obj2;
        }

        public static DataTable Fill(string commandText, IDbDataParameter[] dbParameters = null, CommandType commandType = 1)
        {
            DataTable dataTable = new DataTable("RDIFramework");
            idbProvider_0.Open(DbConnection);
            idbProvider_0.Fill(dataTable, commandText, dbParameters, commandType);
            idbProvider_0.Close();
            return dataTable;
        }

        public static string GetDBDateTime()
        {
            return idbProvider_0.GetDBDateTime();
        }

        public static string GetDBNow()
        {
            return idbProvider_0.GetDBNow();
        }

        public static string GetParameter(string parameter)
        {
            return idbProvider_0.GetParameter(parameter);
        }

        public static IDbDataParameter MakeParameter(string targetFiled, object targetValue)
        {
            return idbProvider_0.MakeParameter(targetFiled, targetValue);
        }

        public static IDbDataParameter[] MakeParameters(string[] targetFileds, object[] targetValues)
        {
            return idbProvider_0.MakeParameters(targetFileds, targetValues);
        }

        public static IDbDataParameter MakeParameters(string parameterName, object parameterValue, System.Data.DbType dbType, int parameterSize, ParameterDirection parameterDirection)
        {
            return idbProvider_0.MakeParam(parameterName, parameterValue, dbType, parameterSize, parameterDirection);
        }

        public static string PlusSign()
        {
            return idbProvider_0.PlusSign();
        }

        public static string PlusSign(params string[] values)
        {
            return idbProvider_0.PlusSign(values);
        }

        public static string SqlSafe(string value)
        {
            return idbProvider_0.SqlSafe(value);
        }

        public RDIFramework.Utilities.CurrentDbType CurrentDbType
        {
            get
            {
                return idbProvider_0.CurrentDbType;
            }
        }

        public static DbProviderFactory Factory
        {
            get
            {
                if (dbProviderFactory_0 == null)
                {
                    dbProviderFactory_0 = idbProvider_0.GetInstance();
                }
                return dbProviderFactory_0;
            }
        }
    }
}

