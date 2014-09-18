namespace RDIFramework.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public interface IDbProvider : IDisposable
    {
        IDbTransaction BeginTransaction();
        void Close();
        void CommitTransaction();
        void Dispose();
        int ExecuteNonQuery(string commandText);
        int ExecuteNonQuery(string commandText, CommandType commandType);
        int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters);
        int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters, CommandType commandType);
        int ExecuteNonQuery(IDbTransaction dbTransaction, string commandText, IDbDataParameter[] dbParameters, CommandType commandType);
        int ExecuteProcedure(string procedureName);
        int ExecuteProcedure(string procedureName, IDbDataParameter[] dbParameters);
        DataTable ExecuteProcedureForDataTable(string procedureName, string tableName, IDbDataParameter[] dbParameters);
        IDataReader ExecuteReader(string commandText);
        IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters);
        IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters, CommandType commandType);
        object ExecuteScalar(string commandText);
        object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters);
        object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters, CommandType commandType);
        object ExecuteScalar(IDbTransaction dbTransaction, string commandText, IDbDataParameter[] dbParameters, CommandType commandType);
        DataTable Fill(string commandText);
        DataTable Fill(DataTable dataTable, string commandText);
        DataTable Fill(string commandText, IDbDataParameter[] dbParameters);
        DataSet Fill(DataSet dataSet, string commandText, string tableName);
        DataTable Fill(DataTable dataTable, string commandText, IDbDataParameter[] dbParameters);
        DataTable Fill(string commandText, IDbDataParameter[] dbParameters, CommandType commandType);
        DataSet Fill(DataSet dataSet, string commandText, string tableName, IDbDataParameter[] dbParameters);
        DataTable Fill(DataTable dataTable, string commandText, IDbDataParameter[] dbParameters, CommandType commandType);
        DataSet Fill(DataSet dataSet, CommandType commandType, string commandText, string tableName, IDbDataParameter[] dbParameters);
        IDbCommand GetDbCommand();
        IDbConnection GetDbConnection();
        string GetDBDateTime();
        string GetDBNow();
        IDbTransaction GetDbTransaction();
        DbProviderFactory GetInstance();
        string GetParameter(string parameter);
        IDbDataParameter MakeParam(string parameterName, object parameterValue, DbType dbType, int parameterSize, ParameterDirection parameterDirection);
        IDbDataParameter MakeParameter(string targetFiled, object targetValue);
        IDbDataParameter[] MakeParameters(List<KeyValuePair<string, object>> parameters);
        IDbDataParameter[] MakeParameters(string[] targetFileds, object[] targetValues);
        IDbConnection Open();
        IDbConnection Open(string connectionString);
        string PlusSign();
        string PlusSign(params string[] values);
        void RollbackTransaction();
        string SqlSafe(string value);
        void WriteLog(string commandText);

        bool AutoOpenClose { get; set; }

        string ConnectionString { get; set; }

        RDIFramework.Utilities.CurrentDbType CurrentDbType { get; }

        bool InTransaction { get; set; }
    }
}

