namespace RDIFramework.Utilities
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public abstract class BaseDbProvider : IDisposable
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private System.Data.Common.DbCommand dbCommand_0 = null;
        private System.Data.Common.DbConnection dbConnection_0 = null;
        private System.Data.Common.DbDataAdapter dbDataAdapter_0 = null;
        private DbProviderFactory dbProviderFactory_0 = null;
        private DbTransaction dbTransaction_0 = null;
        public string FileName = "BaseDbProvider.txt";
        private string string_0 = string.Empty;

        protected BaseDbProvider()
        {
        }

        public IDbTransaction BeginTransaction()
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (!this.InTransaction)
            {
                this.InTransaction = true;
                this.dbTransaction_0 = this.DbConnection.BeginTransaction();
            }
            int num2 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num2 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            return this.dbTransaction_0;
        }

        public void Close()
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.dbConnection_0 != null)
            {
                this.dbConnection_0.Close();
                this.dbConnection_0.Dispose();
            }
            this.Dispose();
            int num2 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num2 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
        }

        public void CommitTransaction()
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.InTransaction)
            {
                this.InTransaction = false;
                this.dbTransaction_0.Commit();
            }
            int num2 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num2 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
        }

        public void Dispose()
        {
            if (this.dbCommand_0 != null)
            {
                this.dbCommand_0.Dispose();
            }
            if (this.dbDataAdapter_0 != null)
            {
                this.dbDataAdapter_0.Dispose();
            }
            if (this.dbTransaction_0 != null)
            {
                this.dbTransaction_0.Dispose();
            }
            if ((this.dbConnection_0 != null) && (this.dbConnection_0.State != ConnectionState.Closed))
            {
                this.dbConnection_0.Close();
                this.dbConnection_0.Dispose();
            }
            this.dbConnection_0 = null;
        }

        public virtual int ExecuteNonQuery(string commandText)
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else if (this.DbConnection == null)
            {
                this.AutoOpenClose = true;
                this.Open();
            }
            this.dbCommand_0 = this.DbConnection.CreateCommand();
            this.dbCommand_0.CommandType = CommandType.Text;
            this.dbCommand_0.CommandText = commandText;
            if (this.InTransaction)
            {
                this.dbCommand_0.Transaction = this.dbTransaction_0;
            }
            int num2 = this.dbCommand_0.ExecuteNonQuery();
            if (this.AutoOpenClose)
            {
                this.Close();
            }
            int num3 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num3 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            this.WriteLog(commandText);
            return num2;
        }

        public virtual int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters)
        {
            return this.ExecuteNonQuery(commandText, dbParameters, CommandType.Text);
        }

        public virtual int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return this.ExecuteNonQuery(this.dbTransaction_0, commandText, null, commandType);
        }

        public virtual int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            return this.ExecuteNonQuery(this.dbTransaction_0, commandText, dbParameters, commandType);
        }

        public virtual int ExecuteNonQuery(IDbTransaction transaction, string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else if (this.DbConnection == null)
            {
                this.AutoOpenClose = true;
                this.Open();
            }
            this.dbCommand_0 = this.DbConnection.CreateCommand();
            this.dbCommand_0.CommandText = commandText;
            this.dbCommand_0.CommandType = commandType;
            if (this.dbTransaction_0 != null)
            {
                this.dbCommand_0.Transaction = this.dbTransaction_0;
            }
            if (dbParameters != null)
            {
                this.dbCommand_0.Parameters.Clear();
                foreach (DbParameter parameter in dbParameters)
                {
                    if (!(((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) ? (parameter.Value != null) : true))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    this.dbCommand_0.Parameters.Add(parameter);
                }
            }
            int num3 = this.dbCommand_0.ExecuteNonQuery();
            if (this.AutoOpenClose)
            {
                this.Close();
            }
            else
            {
                this.dbCommand_0.Parameters.Clear();
            }
            int num4 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num4 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            this.WriteLog(commandText);
            return num3;
        }

        public virtual int ExecuteProcedure(string procedureName)
        {
            return this.ExecuteNonQuery(procedureName, null, CommandType.StoredProcedure);
        }

        public virtual int ExecuteProcedure(string procedureName, IDbDataParameter[] dbParameters)
        {
            return this.ExecuteNonQuery(procedureName, dbParameters, CommandType.StoredProcedure);
        }

        public virtual DataTable ExecuteProcedureForDataTable(string procedureName, string tableName, IDbDataParameter[] dbParameters)
        {
            DataTable dataTable = new DataTable(tableName);
            this.Fill(dataTable, procedureName, dbParameters, CommandType.StoredProcedure);
            return dataTable;
        }

        public virtual IDataReader ExecuteReader(string commandText)
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else if (this.DbConnection == null)
            {
                this.AutoOpenClose = true;
                this.Open();
            }
            this.dbCommand_0 = this.DbConnection.CreateCommand();
            this.dbCommand_0.CommandType = CommandType.Text;
            this.dbCommand_0.CommandText = commandText;
            if (this.InTransaction)
            {
                this.dbCommand_0.Transaction = this.dbTransaction_0;
            }
            DbDataReader reader = null;
            if (this.AutoOpenClose)
            {
                reader = this.dbCommand_0.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                reader = this.dbCommand_0.ExecuteReader();
            }
            int num2 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num2 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            this.WriteLog(commandText);
            return reader;
        }

        public virtual IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters)
        {
            return this.ExecuteReader(commandText, dbParameters, CommandType.Text);
        }

        public virtual IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else if (this.DbConnection == null)
            {
                this.AutoOpenClose = true;
                this.Open();
            }
            this.dbCommand_0 = this.DbConnection.CreateCommand();
            this.dbCommand_0.CommandText = commandText;
            this.dbCommand_0.CommandType = commandType;
            if (this.dbTransaction_0 != null)
            {
                this.dbCommand_0.Transaction = this.dbTransaction_0;
            }
            if (dbParameters != null)
            {
                this.dbCommand_0.Parameters.Clear();
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    if (dbParameters[i] != null)
                    {
                        this.dbCommand_0.Parameters.Add(dbParameters[i]);
                    }
                }
            }
            DbDataReader reader = null;
            if (this.AutoOpenClose)
            {
                reader = this.dbCommand_0.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                reader = this.dbCommand_0.ExecuteReader();
                this.dbCommand_0.Parameters.Clear();
            }
            int num3 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num3 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            this.WriteLog(commandText);
            return reader;
        }

        public virtual object ExecuteScalar(string commandText)
        {
            return this.ExecuteScalar(commandText, null, CommandType.Text);
        }

        public virtual object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters)
        {
            return this.ExecuteScalar(commandText, dbParameters, CommandType.Text);
        }

        public virtual object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            return this.ExecuteScalar(this.dbTransaction_0, commandText, dbParameters, commandType);
        }

        public virtual object ExecuteScalar(IDbTransaction transaction, string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else if (this.DbConnection == null)
            {
                this.AutoOpenClose = true;
                this.Open();
            }
            this.dbCommand_0 = this.DbConnection.CreateCommand();
            this.dbCommand_0.CommandText = commandText;
            this.dbCommand_0.CommandType = commandType;
            if (this.dbTransaction_0 != null)
            {
                this.dbCommand_0.Transaction = this.dbTransaction_0;
            }
            if (dbParameters != null)
            {
                this.dbCommand_0.Parameters.Clear();
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    if (dbParameters[i] != null)
                    {
                        this.dbCommand_0.Parameters.Add(dbParameters[i]);
                    }
                }
            }
            object obj2 = this.dbCommand_0.ExecuteScalar();
            if (this.AutoOpenClose)
            {
                this.Close();
            }
            else
            {
                this.dbCommand_0.Parameters.Clear();
            }
            int num3 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num3 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            this.WriteLog(commandText);
            return obj2;
        }

        public virtual DataTable Fill(string commandText)
        {
            DataTable dataTable = new DataTable("RDIFramework");
            return this.Fill(dataTable, commandText, null, CommandType.Text);
        }

        public virtual DataTable Fill(DataTable dataTable, string commandText)
        {
            return this.Fill(dataTable, commandText, null, CommandType.Text);
        }

        public virtual DataTable Fill(string commandText, IDbDataParameter[] dbParameters)
        {
            DataTable dataTable = new DataTable("RDIFramework");
            return this.Fill(dataTable, commandText, dbParameters, CommandType.Text);
        }

        public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName)
        {
            return this.Fill(dataSet, CommandType.Text, commandText, tableName, null);
        }

        public virtual DataTable Fill(DataTable dataTable, string commandText, IDbDataParameter[] dbParameters)
        {
            return this.Fill(dataTable, commandText, dbParameters, CommandType.Text);
        }

        public virtual DataTable Fill(string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            DataTable dataTable = new DataTable("RDIFramework");
            return this.Fill(dataTable, commandText, dbParameters, commandType);
        }

        public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName, IDbDataParameter[] dbParameters)
        {
            return this.Fill(dataSet, CommandType.Text, commandText, tableName, dbParameters);
        }

        public virtual DataTable Fill(DataTable dataTable, string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else if (this.DbConnection == null)
            {
                this.AutoOpenClose = true;
                this.Open();
            }
            using (this.dbCommand_0 = this.DbConnection.CreateCommand())
            {
                this.dbCommand_0.CommandTimeout = this.DbConnection.ConnectionTimeout;
                this.dbCommand_0.CommandText = commandText;
                this.dbCommand_0.CommandType = commandType;
                if (this.InTransaction)
                {
                    this.dbCommand_0.Transaction = this.dbTransaction_0;
                }
                this.dbDataAdapter_0 = this.GetInstance().CreateDataAdapter();
                this.dbDataAdapter_0.SelectCommand = this.dbCommand_0;
                if ((dbParameters != null) && (dbParameters.Length > 0))
                {
                    this.dbCommand_0.Parameters.AddRange(dbParameters);
                }
                this.dbDataAdapter_0.Fill(dataTable);
                this.dbDataAdapter_0.SelectCommand.Parameters.Clear();
            }
            if (this.AutoOpenClose)
            {
                this.Close();
            }
            int num2 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num2 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            this.WriteLog(commandText);
            return dataTable;
        }

        public virtual DataSet Fill(DataSet dataSet, CommandType commandType, string commandText, string tableName, IDbDataParameter[] dbParameters)
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.AutoOpenClose)
            {
                this.Open();
            }
            else if (this.DbConnection == null)
            {
                this.AutoOpenClose = true;
                this.Open();
            }
            using (this.dbCommand_0 = this.DbConnection.CreateCommand())
            {
                this.dbCommand_0.CommandText = commandText;
                this.dbCommand_0.CommandType = commandType;
                if ((dbParameters != null) && (dbParameters.Length > 0))
                {
                    this.dbCommand_0.Parameters.AddRange(dbParameters);
                }
                this.dbDataAdapter_0 = this.GetInstance().CreateDataAdapter();
                this.dbDataAdapter_0.SelectCommand = this.dbCommand_0;
                this.dbDataAdapter_0.Fill(dataSet, tableName);
                if (this.AutoOpenClose)
                {
                    this.Close();
                }
                else
                {
                    this.dbDataAdapter_0.SelectCommand.Parameters.Clear();
                }
            }
            int num2 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num2 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            this.WriteLog(commandText);
            return dataSet;
        }

        public virtual IDbCommand GetDbCommand()
        {
            return this.DbConnection.CreateCommand();
        }

        public virtual IDbConnection GetDbConnection()
        {
            return this.dbConnection_0;
        }

        public virtual IDbTransaction GetDbTransaction()
        {
            return this.dbTransaction_0;
        }

        public virtual DbProviderFactory GetInstance()
        {
            if (this.dbProviderFactory_0 == null)
            {
                this.dbProviderFactory_0 = DbHelperFactory.GetHelper(RDIFramework.Utilities.CurrentDbType.SqlServer, null).GetInstance();
            }
            return this.dbProviderFactory_0;
        }

        public virtual DataTable GetPageList(IDbDataParameter[] dbParameters)
        {
            return this.ExecuteProcedureForDataTable("pGetPageData", "pageDataTable", dbParameters);
        }

        public virtual IDbConnection Open()
        {
            int tickCount = Environment.TickCount;
            if (string.IsNullOrEmpty(this.ConnectionString))
            {
                if (string.IsNullOrEmpty(SystemInfo.BusinessDbConnection))
                {
                    BaseConfiguration.GetSetting();
                }
                if (string.IsNullOrEmpty(SystemInfo.BusinessDbConnection))
                {
                    this.ConnectionString = SystemInfo.RDIFrameworkDbConection;
                }
                else
                {
                    this.ConnectionString = SystemInfo.BusinessDbConnection;
                }
            }
            this.Open(this.ConnectionString);
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            return this.dbConnection_0;
        }

        public virtual IDbConnection Open(string connectionString)
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (!SecretHelper.CheckRegister())
            {
                connectionString = string.Empty;
                throw new Exception(SystemInfo.RegisterException);
            }
            if ((this.dbConnection_0 == null) || (this.dbConnection_0.State == ConnectionState.Closed))
            {
                this.ConnectionString = connectionString;
                this.dbConnection_0 = this.GetInstance().CreateConnection();
                this.dbConnection_0.ConnectionString = this.ConnectionString;
                this.dbConnection_0.Open();
                int num2 = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num2 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            }
            return this.dbConnection_0;
        }

        public virtual string PlusSign()
        {
            return " + ";
        }

        public virtual string PlusSign(params string[] values)
        {
            string str = string.Empty;
            for (int i = 0; i < values.Length; i++)
            {
                str = str + values[i] + this.PlusSign();
            }
            if (!string.IsNullOrEmpty(str))
            {
                return str.Substring(0, str.Length - 3);
            }
            return this.PlusSign();
        }

        public void RollbackTransaction()
        {
            int tickCount = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
            if (this.InTransaction)
            {
                this.InTransaction = false;
                this.dbTransaction_0.Rollback();
            }
            int num2 = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds((double) (num2 - tickCount)).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
        }

        public virtual void SqlBulkCopyData(DataTable dataTable)
        {
        }

        public virtual string SqlSafe(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }

        public virtual void WriteLog(string commandText)
        {
            string fileName = DateTime.Now.ToString(SystemInfo.DateFormat) + " _ " + this.FileName;
            this.WriteLog(commandText, fileName);
        }

        public virtual void WriteLog(string commandText, string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString(SystemInfo.DateFormat) + " _ " + this.FileName;
            }
            if (SystemInfo.LogSQL)
            {
                string path = SystemInfo.StartupPath + @"\\Log\\Query";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str2 = path + @"\" + fileName;
                if (!File.Exists(str2))
                {
                    new FileStream(str2, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite).Close();
                }
                StreamWriter writer = new StreamWriter(str2, true, Encoding.Default);
                writer.WriteLine(DateTime.Now.ToString(SystemInfo.DateTimeFormat) + " " + commandText);
                writer.Close();
            }
        }

        public bool AutoOpenClose
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public virtual RDIFramework.Utilities.CurrentDbType CurrentDbType
        {
            get
            {
                return RDIFramework.Utilities.CurrentDbType.SqlServer;
            }
        }

        public System.Data.Common.DbCommand DbCommand
        {
            get
            {
                return this.dbCommand_0;
            }
            set
            {
                this.dbCommand_0 = value;
            }
        }

        public System.Data.Common.DbConnection DbConnection
        {
            get
            {
                if (this.dbConnection_0 == null)
                {
                    this.Open();
                    this.AutoOpenClose = true;
                }
                return this.dbConnection_0;
            }
            set
            {
                this.dbConnection_0 = value;
            }
        }

        public System.Data.Common.DbDataAdapter DbDataAdapter
        {
            get
            {
                return this.dbDataAdapter_0;
            }
            set
            {
                this.dbDataAdapter_0 = value;
            }
        }

        public bool InTransaction
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }
    }
}

