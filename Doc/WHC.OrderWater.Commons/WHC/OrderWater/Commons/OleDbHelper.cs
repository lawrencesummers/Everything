namespace WHC.OrderWater.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;

    public class OleDbHelper
    {
        private const string string_0 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User ID=Admin;Jet OLEDB:Database Password=;";
        private string string_1 = "";

        public OleDbHelper(string accessFilePath)
        {
            this.string_1 = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User ID=Admin;Jet OLEDB:Database Password=;", accessFilePath);
        }

        public DataSet ExecuteDataSet(string sql)
        {
            DataSet dataSet = new DataSet();
            new OleDbDataAdapter(sql, this.string_1).Fill(dataSet);
            return dataSet;
        }

        public int ExecuteNonQuery(List<string> sqlList)
        {
            int num = 0;
            using (OleDbConnection connection = new OleDbConnection(this.string_1))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand {
                    Connection = connection
                };
                foreach (string str in sqlList)
                {
                    command.CommandText = str;
                    command.CommandType = CommandType.Text;
                    try
                    {
                        if (command.ExecuteNonQuery() > 0)
                        {
                            num++;
                        }
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            return num;
        }

        public bool ExecuteNoQuery(string sql)
        {
            bool flag = false;
            using (OleDbConnection connection = new OleDbConnection(this.string_1))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand {
                    Connection = connection,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                if (command.ExecuteNonQuery() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public IDataReader ExecuteReader(string sql)
        {
            using (OleDbConnection connection = new OleDbConnection(this.string_1))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand {
                    Connection = connection,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public object ExecuteScalar(string sql)
        {
            using (OleDbConnection connection = new OleDbConnection(this.string_1))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand {
                    Connection = connection,
                    CommandText = sql,
                    CommandType = CommandType.Text
                };
                return command.ExecuteScalar();
            }
        }

        public bool TestConnection()
        {
            bool flag = false;
            using (DbConnection connection = new OleDbConnection(this.string_1))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    flag = true;
                }
            }
            return flag;
        }
    }
}

