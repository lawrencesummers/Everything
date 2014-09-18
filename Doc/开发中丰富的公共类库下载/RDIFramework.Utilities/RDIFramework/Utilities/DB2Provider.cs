namespace RDIFramework.Utilities
{
    using IBM.Data.DB2;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class DB2Provider : BaseDbProvider, IDbProvider, IDisposable
    {
        public DB2Provider()
        {
            base.FileName = "DB2Provider.txt";
        }

        public DB2Provider(string connectionString)
        {
            base.ConnectionString = connectionString;
        }

        public string GetDBDateTime()
        {
            string commandText = " SELECT current timestamp FROM sysibm.sysdummy1 ";
            this.Open();
            string str2 = this.ExecuteScalar(commandText, null, CommandType.Text).ToString();
            base.Close();
            return str2;
        }

        public string GetDBNow()
        {
            return " current timestamp ";
        }

        public override DbProviderFactory GetInstance()
        {
            return DB2Factory.Instance;
        }

        public string GetParameter(string parameter)
        {
            return (" @" + parameter + " ");
        }

        public IDbDataParameter MakeInParam(string targetFiled, object targetValue)
        {
            return new DB2Parameter("@" + targetFiled, targetValue);
        }

        public IDbDataParameter MakeInParam(string paramName, DbType dbType, int Size, object value)
        {
            return this.MakeParam(paramName, value, dbType, Size, ParameterDirection.Input);
        }

        public IDbDataParameter MakeOutParam(string paramName, DbType dbType, int size)
        {
            return this.MakeParam(paramName, null, dbType, size, ParameterDirection.Output);
        }

        public IDbDataParameter MakeParam(string parameterName, object parameterValue, DbType dbType, int parameterSize, ParameterDirection parameterDirection)
        {
            DB2Parameter parameter;
            if (parameterSize > 0)
            {
                parameter = new DB2Parameter(parameterName, (DB2Type) dbType, parameterSize);
            }
            else
            {
                parameter = new DB2Parameter(parameterName, (DB2Type) dbType);
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

        public override string PlusSign()
        {
            return " || ";
        }

        public override RDIFramework.Utilities.CurrentDbType CurrentDbType
        {
            get
            {
                return RDIFramework.Utilities.CurrentDbType.DB2;
            }
        }
    }
}

