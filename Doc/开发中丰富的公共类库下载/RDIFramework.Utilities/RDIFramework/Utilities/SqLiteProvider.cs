namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SQLite;

    public class SqLiteProvider : BaseDbProvider, IDbProvider, IDisposable
    {
        public SqLiteProvider()
        {
            base.FileName = "SQLite.txt";
        }

        public SqLiteProvider(string connectionString) : this()
        {
            base.ConnectionString = connectionString;
        }

        public string GetDBDateTime()
        {
            string commandText = " SELECT " + this.GetDBNow();
            this.Open();
            string str2 = this.ExecuteScalar(commandText, null, CommandType.Text).ToString();
            base.Close();
            return str2;
        }

        public string GetDBNow()
        {
            return "datetime(CURRENT_TIMESTAMP,'localtime');";
        }

        public override DbProviderFactory GetInstance()
        {
            return SQLiteFactory.Instance;
        }

        public string GetParameter(string parameter)
        {
            return (" @" + parameter);
        }

        public IDbDataParameter MakeInParam(string targetFiled, object targetValue)
        {
            return new SQLiteParameter(targetFiled, targetValue);
        }

        public IDbDataParameter MakeInParam(string paramName, DbType dbType, int size, object value)
        {
            return this.MakeParam(paramName, value, dbType, size, ParameterDirection.Input);
        }

        public IDbDataParameter MakeOutParam(string paramName, DbType dbType, int size)
        {
            return this.MakeParam(paramName, null, dbType, size, ParameterDirection.Output);
        }

        public IDbDataParameter MakeParam(string parameterName, object parameterValue, DbType dbType, int parameterSize, ParameterDirection parameterDirection)
        {
            SQLiteParameter parameter;
            if (parameterSize > 0)
            {
                parameter = new SQLiteParameter(dbType, parameterSize, parameterName);
            }
            else
            {
                parameter = new SQLiteParameter(parameterName, dbType);
            }
            parameter.Direction = parameterDirection;
            if ((parameterDirection != ParameterDirection.Output) || (parameterValue != null))
            {
                parameter.Value = parameterValue;
            }
            return parameter;
        }

        public IDbDataParameter MakeParameter(string targetFiled, object targetValue)
        {
            IDbDataParameter parameter = null;
            if ((targetFiled != null) && (targetValue != null))
            {
                parameter = this.MakeInParam(targetFiled, targetValue);
            }
            return parameter;
        }

        public IDbDataParameter[] MakeParameters(List<KeyValuePair<string, object>> parameters)
        {
            List<IDbDataParameter> list = new List<IDbDataParameter>();
            if ((parameters != null) && (parameters.Count > 0))
            {
                foreach (KeyValuePair<string, object> pair in parameters)
                {
                    if (!(((pair.Key == null) || (pair.Value == null)) || (pair.Value is Array)))
                    {
                        list.Add(this.MakeParameter(pair.Key, pair.Value));
                    }
                }
            }
            return list.ToArray();
        }

        public IDbDataParameter[] MakeParameters(string[] targetFileds, object[] targetValues)
        {
            List<IDbDataParameter> list = new List<IDbDataParameter>();
            if ((targetFileds != null) && (targetValues != null))
            {
                for (int i = 0; i < targetFileds.Length; i++)
                {
                    if (!(((targetFileds[i] == null) || (targetValues[i] == null)) || (targetValues[i] is Array)))
                    {
                        list.Add(this.MakeInParam(targetFileds[i], targetValues[i]));
                    }
                }
            }
            return list.ToArray();
        }

        public override string PlusSign(params string[] values)
        {
            string str = string.Empty;
            str = " CONCAT(";
            for (int i = 0; i < values.Length; i++)
            {
                str = str + values[i] + " ,";
            }
            return (str.Substring(0, str.Length - 2) + ")");
        }

        public override RDIFramework.Utilities.CurrentDbType CurrentDbType
        {
            get
            {
                return RDIFramework.Utilities.CurrentDbType.SQLite;
            }
        }
    }
}

