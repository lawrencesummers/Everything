namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;

    public class OleDbProvider : BaseDbProvider, IDbProvider, IDisposable
    {
        public OleDbProvider()
        {
            base.FileName = "OleDbHelper.txt";
        }

        public OleDbProvider(string connectionString) : this()
        {
            base.ConnectionString = connectionString;
        }

        public string GetDBDateTime()
        {
            string commandText = " SELECT " + this.GetDBNow();
            if (this.CurrentDbType.Equals(RDIFramework.Utilities.CurrentDbType.Oracle))
            {
                commandText = commandText + " FROM DUAL ";
            }
            this.Open();
            string str2 = this.ExecuteScalar(commandText, null, CommandType.Text).ToString();
            base.Close();
            return str2;
        }

        public string GetDBNow()
        {
            string str = " Getdate() ";
            switch (this.CurrentDbType)
            {
                case RDIFramework.Utilities.CurrentDbType.Oracle:
                    return " SYSDATE ";

                case RDIFramework.Utilities.CurrentDbType.SqlServer:
                    return " GetDate() ";

                case RDIFramework.Utilities.CurrentDbType.Access:
                    return ("'" + DateTime.Now.ToString() + "'");

                case RDIFramework.Utilities.CurrentDbType.DB2:
                    return str;

                case RDIFramework.Utilities.CurrentDbType.MySql:
                    return " NOW() ";
            }
            return str;
        }

        public override DbProviderFactory GetInstance()
        {
            return OleDbFactory.Instance;
        }

        public string GetParameter(string parameter)
        {
            return " ? ";
        }

        public IDbDataParameter MakeInParam(string targetFiled, object targetValue)
        {
            return new OleDbParameter(targetFiled, targetValue);
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
            OleDbParameter parameter;
            if (parameterSize > 0)
            {
                parameter = new OleDbParameter(parameterName, (OleDbType) dbType, parameterSize);
            }
            else
            {
                parameter = new OleDbParameter(parameterName, (OleDbType) dbType);
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
                    if ((targetFileds[i] != null) && (targetValues[i] != null))
                    {
                        list.Add(this.MakeInParam(targetFileds[i], targetValues[i]));
                    }
                }
            }
            return list.ToArray();
        }

        public override string PlusSign()
        {
            return " + ";
        }

        public override string PlusSign(params string[] values)
        {
            int num;
            string str = string.Empty;
            switch (this.CurrentDbType)
            {
                case RDIFramework.Utilities.CurrentDbType.Oracle:
                    for (num = 0; num < values.Length; num++)
                    {
                        str = str + values[num] + " || ";
                    }
                    if (!string.IsNullOrEmpty(str))
                    {
                        return str.Substring(0, str.Length - 4);
                    }
                    return " || ";

                case RDIFramework.Utilities.CurrentDbType.SqlServer:
                case RDIFramework.Utilities.CurrentDbType.Access:
                    if (string.IsNullOrEmpty(str))
                    {
                        return " + ";
                    }
                    return str.Substring(0, str.Length - 3);

                case RDIFramework.Utilities.CurrentDbType.DB2:
                    return str;

                case RDIFramework.Utilities.CurrentDbType.MySql:
                    str = " CONCAT(";
                    for (num = 0; num < values.Length; num++)
                    {
                        str = str + values[num] + " ,";
                    }
                    return (str.Substring(0, str.Length - 2) + ")");
            }
            return str;
        }

        public override RDIFramework.Utilities.CurrentDbType CurrentDbType
        {
            get
            {
                return RDIFramework.Utilities.CurrentDbType.Access;
            }
        }
    }
}

