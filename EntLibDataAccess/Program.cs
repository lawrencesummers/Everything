using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;

namespace EntLibDataAccess
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            #region Resolve the required objects

            // Resolve the default Database object from the container
            // The actual concrete type is determined by the configuration settings.
            defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();

            // Resolve a Database object from the container using the connection string name.
            //  namedDB = EnterpriseLibraryContainer.Current.GetInstance<Database>("ExampleDatabase");

            // Resolve a SqlDatabase object from the container using the default database.
            //   sqlServerDB = EnterpriseLibraryContainer.Current.GetInstance<Database>() as SqlDatabase;

            // Resolve a SqlDatabase object that has "Asynchronous Processing=true" in the connection string.
            //  asyncDB = EnterpriseLibraryContainer.Current.GetInstance<Database>("AsyncExampleDatabase");

            #endregion
            //ReadSQLStatement();
            //ReadSQLOrSprocWithNamedParams();
            ReadDataAsObjects();
            Console.ReadLine();
        }

        static Database defaultDB = null;
        static Database namedDB = null;
        static SqlDatabase sqlServerDB = null;
        static Database asyncDB = null;


        [Description("Return rows using a SQL statement with no parameters")]
        static void ReadSQLStatement()
        {
            // Call the ExecuteReader method by specifying the command type
            // as a SQL statement, and passing in the SQL statement
            using (IDataReader reader = defaultDB.ExecuteReader(CommandType.Text, "SELECT TOP 1 * FROM TB_DictType"))
            {
                DisplayRowValues(reader);
            }
        }

        [Description("Return rows using a SQL statement or stored procedure with named parameters")]
        static void ReadSQLOrSprocWithNamedParams()
        {
            //// Read data with a SQL statement that accepts one parameter
            //string sqlStatement = "SELECT TOP 1 * FROM OrderList WHERE State LIKE @state";
            //// Create a suitable command type and add the required parameter
            //using (DbCommand sqlCmd = defaultDB.GetSqlStringCommand(sqlStatement))
            //{
            //    defaultDB.AddInParameter(sqlCmd, "state", DbType.String, "New York");
            //    // Call the ExecuteReader method with the command
            //    using (IDataReader sqlReader = namedDB.ExecuteReader(sqlCmd))
            //    {
            //        Console.WriteLine("Results from executing SQL statement:");
            //        DisplayRowValues(sqlReader);
            //    }
            //}
            // Read data with a stored procedure that accepts one parameter
            string storedProcName = "sp_QueryDictDataBySeq";
            // Create a suitable command type and add the required parameter
            using (DbCommand sprocCmd = defaultDB.GetStoredProcCommand(storedProcName))
            {
                //defaultDB.AddInParameter(sprocCmd, "state", DbType.String, "New York");
                // Call the ExecuteReader method with the command
                using (IDataReader sprocReader = defaultDB.ExecuteReader(sprocCmd))
                {
                    Console.WriteLine("Results from executing stored procedure:");
                    DisplayRowValues(sprocReader);
                }
            }
        }

        [Description("Return data as a sequence of objects using sql")]
        static void ReadDataAsObjects()
        {
            var data = defaultDB.ExecuteSqlStringAccessor<DictData>("select * from tb_dictdata");
            // Perform a client-side query on the returned data
            // Be aware that the orderby and filtering is happening on the client, not the database
            var results = from productItem in data
                          orderby productItem.Name
                          select new { productItem.Name, productItem.Seq };
            // Display the results
            foreach (var item in results)
            {
                Console.WriteLine("Product Name: {0}", item.Name);
                Console.WriteLine("Description: {0}", item.Seq);
                Console.WriteLine();
            }
        }


        private static void DisplayRowValues(DataTable table)
        {
            Console.WriteLine("Rows in the table named '{0}':", table.TableName);
            Console.WriteLine();
            DisplayRowValues(table.CreateDataReader());
        }

        private static void DisplayRowValues(IDataReader reader)
        {
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine("{0} = {1}", reader.GetName(i), reader[i].ToString());
                }
                Console.WriteLine();
            }
        }

        private static void DisplayTableNames(DataSet ds, string methodName)
        {
            Console.WriteLine("Tables in the DataSet obtained using the {0} method:", methodName);
            foreach (DataTable t in ds.Tables)
            {
                Console.WriteLine(" - Table named '{0}' contains {1} rows.", t.TableName, t.Rows.Count);
            }
            Console.WriteLine();
        }
    }
}
