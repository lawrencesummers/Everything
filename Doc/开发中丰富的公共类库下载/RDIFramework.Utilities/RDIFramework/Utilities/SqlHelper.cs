namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    public class SqlHelper : BaseDbProvider, IDbProvider, IDisposable
    {
        public SqlHelper()
        {
            base.FileName = "SqlHelper.txt";
        }

        public SqlHelper(string connectionString)
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
            return " Getdate() ";
        }

        public override DbProviderFactory GetInstance()
        {
            return SqlClientFactory.Instance;
        }

        public override DataTable GetPageList(IDbDataParameter[] dbParameters)
        {
            return base.ExecuteProcedureForDataTable("pGetPageData", "pageDataTable", dbParameters);
        }

        public string GetParameter(string parameter)
        {
            return (" @" + parameter + " ");
        }

        public IDbDataParameter MakeInParam(string targetFiled, object targetValue)
        {
            return new SqlParameter("@" + targetFiled, targetValue);
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
            SqlParameter parameter;
            if (parameterSize > 0)
            {
                parameter = new SqlParameter(parameterName, (SqlDbType) dbType, parameterSize);
            }
            else
            {
                parameter = new SqlParameter(parameterName, (SqlDbType) dbType);
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
            if (targetFiled == null)
            {
                return parameter;
            }
            if (targetValue == null)
            {
                return this.MakeInParam(targetFiled, DBNull.Value);
            }
            return this.MakeInParam(targetFiled, targetValue);
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
            for (int i = 0; i < values.Length; i++)
            {
                str = str + values[i] + " + ";
            }
            if (!string.IsNullOrEmpty(str))
            {
                return str.Substring(0, str.Length - 3);
            }
            return " + ";
        }

        public override void SqlBulkCopyData(DataTable dataTable)
        {
            SqlConnection dbConnection = null;
            this.Open();
            dbConnection = (SqlConnection) this.GetDbConnection();
            using (SqlTransaction transaction = dbConnection.BeginTransaction())
            {
                SqlBulkCopy copy = new SqlBulkCopy(dbConnection, SqlBulkCopyOptions.Default, transaction) {
                    DestinationTableName = dataTable.TableName,
                    BulkCopyTimeout = 0x3e8
                };
                foreach (DataColumn column in dataTable.Columns)
                {
                    copy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                try
                {
                    copy.WriteToServer(dataTable);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    copy.Close();
                }
                finally
                {
                    copy.Close();
                    base.Close();
                }
            }
        }

        public override RDIFramework.Utilities.CurrentDbType CurrentDbType
        {
            get
            {
                return RDIFramework.Utilities.CurrentDbType.SqlServer;
            }
        }
    }
}

