using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EntLibDataAccess
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        static Database defaultDB = null;
        static Database namedDB = null;
        static SqlDatabase sqlServerDB = null;
        static Database asyncDB = null;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

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
