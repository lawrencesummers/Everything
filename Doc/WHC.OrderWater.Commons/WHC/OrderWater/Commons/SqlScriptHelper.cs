namespace WHC.OrderWater.Commons
{
    using Microsoft.Win32;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Security.AccessControl;
    using System.Text;

    public class SqlScriptHelper
    {
        public bool AttachDB(string connectionString, string dataBaseName, string dataBase_MDF, string dataBase_LDF)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand {
                    Connection = connection,
                    CommandText = "sp_attach_db"
                };
                command.Parameters.Add(new SqlParameter("dbname", SqlDbType.NVarChar));
                command.Parameters["dbname"].Value = dataBaseName;
                command.Parameters.Add(new SqlParameter("filename1", SqlDbType.NVarChar));
                command.Parameters["filename1"].Value = dataBase_MDF;
                command.Parameters.Add(new SqlParameter("filename2", SqlDbType.NVarChar));
                command.Parameters["filename2"].Value = dataBase_LDF;
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            return true;
        }

        public bool BackupDataBase(string connectionString, string dataBaseName, string DataBaseOfBackupPath, string DataBaseOfBackupName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand {
                    Connection = connection,
                    CommandText = "use master;backup database @dbname to disk = @backupname;"
                };
                command.Parameters.Add(new SqlParameter("dbname", SqlDbType.NVarChar));
                command.Parameters["dbname"].Value = dataBaseName;
                command.Parameters.Add(new SqlParameter("backupname", SqlDbType.NVarChar));
                command.Parameters["backupname"].Value = Path.Combine(DataBaseOfBackupPath, DataBaseOfBackupName);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            return true;
        }

        public bool DetachDB(string connectionString, string dataBaseName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand {
                    Connection = connection,
                    CommandText = "sp_detach_db"
                };
                command.Parameters.Add(new SqlParameter("dbname", SqlDbType.NVarChar));
                command.Parameters["dbname"].Value = dataBaseName;
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            return true;
        }

        public static void DoSQL(string path)
        {
            string argument = string.Format(" -E -S (local) -i \"{0}\"", path);
            RunDos("osql.exe", argument, false);
        }

        public static void DoSQL(string path, string userID, string password, string server)
        {
            string argument = string.Format(" -U {0} -P {1} -S {2} -i \"{3}\"", new object[] { userID, password, server, path });
            RunDos("osql.exe", argument, false);
        }

        public static void ReplaceDBName(string filePath, string string_0, string string_1)
        {
            if (string_1.CompareTo(string_0) != 0)
            {
                string str = string.Empty;
                using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
                {
                    str = reader.ReadToEnd();
                    str = str.Replace(string_0, string_1);
                }
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.Default))
                {
                    writer.Write(str);
                }
            }
        }

        public bool RestoreDataBase(string connectionString, string dataBaseName, string DataBaseOfBackupPath, string DataBaseOfBackupName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand {
                    Connection = connection,
                    CommandText = "use master;restore database @DataBaseName From disk = @BackupFile with replace;"
                };
                command.Parameters.Add(new SqlParameter("DataBaseName", SqlDbType.NVarChar));
                command.Parameters["DataBaseName"].Value = dataBaseName;
                command.Parameters.Add(new SqlParameter("BackupFile", SqlDbType.NVarChar));
                command.Parameters["BackupFile"].Value = Path.Combine(DataBaseOfBackupPath, DataBaseOfBackupName);
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            return true;
        }

        public static void RunDos(string fileName, string argument, bool hidden)
        {
            Process process = new Process {
                EnableRaisingEvents = false
            };
            process.StartInfo.FileName = string.Format("\"{0}\"", fileName);
            process.StartInfo.Arguments = argument;
            if (hidden)
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }
            process.Start();
            process.WaitForExit();
        }

        private static string smethod_0(string string_0, string string_1, string string_2, bool bool_0)
        {
            Process process = new Process {
                StartInfo = { FileName = string_0, Arguments = string_1, UseShellExecute = false, RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = !bool_0 }
            };
            if (bool_0)
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }
            else
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            process.Start();
            if (string_2 != null)
            {
                process.StandardInput.WriteLine(string_2);
            }
            string str = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return str;
        }

        private static void smethod_1(string string_0)
        {
            string path = @"C:\Log.txt";
            using (StreamWriter writer = new StreamWriter(path, true, Encoding.Default))
            {
                writer.Write(string_0);
            }
        }

        public static void UpdatePathEnvironment(string physicalRoot)
        {
            string name = @"SYSTEM\ControlSet001\Control\Session Manager\Environment";
            string str2 = "Path";
            string str3 = Registry.LocalMachine.OpenSubKey(name).GetValue(str2).ToString();
            if (str3.IndexOf(physicalRoot) < 0)
            {
                str3 = str3 + string.Format(";{0}", physicalRoot);
            }
            Registry.LocalMachine.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue).SetValue(str2, str3);
        }
    }
}

