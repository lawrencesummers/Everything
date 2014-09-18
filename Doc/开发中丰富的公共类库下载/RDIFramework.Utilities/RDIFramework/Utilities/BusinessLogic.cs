namespace RDIFramework.Utilities
{
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public class BusinessLogic
    {
        public static string FieldCategoryId = "CategoryId";
        public static string FieldCode = "Code";
        public static string FieldCreateOn = "CreateOn";
        public static string FieldCreateUserId = "CreateUserId";
        public static string FieldDeleteMark = "DeleteMark";
        public static string FieldEnabled = "Enabled";
        public static string FieldFullName = "FullName";
        public static string FieldId = "Id";
        public static string FieldModifiedOn = "ModifiedOn";
        public static string FieldModifiedUserId = "ModifiedUserId";
        public static string FieldParentId = "ParentId";
        public static string FieldSortCode = "SortCode";
        public static string SelectedColumn = "Selected";
        public static string SQLLogicConditional = " AND ";

        public static string ArrayToList(string[] ids)
        {
            return ArrayToList(ids, string.Empty);
        }

        public static string ArrayToList(string[] ids, string separativeSign)
        {
            int num = 0;
            string str = string.Empty;
            foreach (string str2 in ids)
            {
                num++;
                string str3 = str;
                str = str3 + separativeSign + str2 + separativeSign + ",";
            }
            if (num == 0)
            {
                return "";
            }
            return str.TrimEnd(new char[] { ',' });
        }

        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream stream = null;
            byte[] buffer2;
            try
            {
                stream = new MemoryStream();
                Bitmap.Save(stream, Bitmap.RawFormat);
                byte[] buffer = new byte[stream.Length];
                buffer2 = stream.ToArray();
            }
            catch (ArgumentNullException exception)
            {
                throw exception;
            }
            finally
            {
                stream.Close();
            }
            return buffer2;
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream(byteArrayIn))
            {
                Image image2 = Image.FromStream(stream);
                stream.Flush();
                return image2;
            }
        }

        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            Bitmap bitmap;
            try
            {
                stream = new MemoryStream(Bytes);
                bitmap = new Bitmap(new Bitmap(stream));
            }
            catch (ArgumentNullException exception)
            {
                throw exception;
            }
            catch (ArgumentException exception2)
            {
                throw exception2;
            }
            finally
            {
                stream.Close();
            }
            return bitmap;
        }

        public static string[] Concat(params string[][] ids)
        {
            Hashtable hashtable = new Hashtable();
            if (ids != null)
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    if (ids[j] != null)
                    {
                        for (int k = 0; k < ids[j].Length; k++)
                        {
                            if ((ids[j][k] != null) && !hashtable.ContainsKey(ids[j][k]))
                            {
                                hashtable.Add(ids[j][k], ids[j][k]);
                            }
                        }
                    }
                }
            }
            string[] strArray = new string[hashtable.Count];
            IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); i++)
            {
                strArray[i] = enumerator.Key.ToString();
            }
            return strArray;
        }

        public static string[] Concat(string[] ids, string id)
        {
            return Concat(new string[][] { ids, new string[] { id } });
        }

        public static bool ConvertIntToBoolean(object targetValue)
        {
            return ((targetValue != DBNull.Value) ? (targetValue.ToString().Equals("1") || targetValue.ToString().Equals(1.ToString())) : false);
        }

        public static bool ConvertToBoolean(object targetValue)
        {
            return ((targetValue != DBNull.Value) ? targetValue.ToString().Equals(1.ToString()) : false);
        }

        public static byte[] ConvertToByte(byte[] targetValue)
        {
            return ((targetValue != DBNull.Value) ? targetValue : null);
        }

        public static DateTime? ConvertToDateTime(object targetValue)
        {
            DateTime? nullable = null;
            if (DateTimeHelper.IsDate(targetValue.ToString()))
            {
                nullable = new DateTime?(Convert.ToDateTime(targetValue.ToString()));
            }
            return nullable;
        }

        public static string ConvertToDateToString(object targetValue)
        {
            if (DateTimeHelper.IsDateTime(targetValue.ToString()))
            {
                return DateTime.Parse(targetValue.ToString()).ToString(SystemInfo.DateFormat);
            }
            return null;
        }

        public static decimal? ConvertToDecimal(object targetValue)
        {
            decimal? nullable = null;
            if (targetValue == DBNull.Value)
            {
                return nullable;
            }
            decimal result = 0M;
            decimal.TryParse(targetValue.ToString(), out result);
            return new decimal?(result);
        }

        public static double? ConvertToDouble(object targetValue)
        {
            double? nullable = null;
            if (targetValue == DBNull.Value)
            {
                return nullable;
            }
            double result = 0.0;
            double.TryParse(targetValue.ToString(), out result);
            return new double?(result);
        }

        public static int? ConvertToInt(object targetValue)
        {
            int? nullable = null;
            if ((targetValue == null) || (targetValue == DBNull.Value))
            {
                return nullable;
            }
            int result = 0;
            int.TryParse(targetValue.ToString(), out result);
            return new int?(result);
        }

        public static int? ConvertToInt32(object targetValue)
        {
            int? nullable = null;
            if (targetValue == DBNull.Value)
            {
                return nullable;
            }
            int result = 0;
            int.TryParse(targetValue.ToString(), out result);
            return new int?(result);
        }

        public static long? ConvertToInt64(object targetValue)
        {
            long? nullable = null;
            if (targetValue == DBNull.Value)
            {
                return nullable;
            }
            long result = 0L;
            long.TryParse(targetValue.ToString(), out result);
            return new long?(result);
        }

        public static long? ConvertToLong(object targetValue)
        {
            long? nullable = null;
            if (targetValue == DBNull.Value)
            {
                return nullable;
            }
            long result = 0L;
            long.TryParse(targetValue.ToString(), out result);
            return new long?(result);
        }

        public static string ConvertToString(object targetValue)
        {
            return ((targetValue != DBNull.Value) ? Convert.ToString(targetValue) : null);
        }

        public static object CopyObjectProperties(object sourceObject, object targetObject)
        {
            Type type = sourceObject.GetType();
            Type type2 = targetObject.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] infoArray2 = type2.GetProperties();
            for (int i = 0; i < infoArray2.Length; i++)
            {
                int index = 0;
                while (index < properties.Length)
                {
                    if (infoArray2[i].Name.Equals(properties[index].Name))
                    {
                        goto Label_0053;
                    }
                    index++;
                }
                continue;
            Label_0053:
                if (infoArray2[i].CanWrite)
                {
                    object obj2 = properties[index].GetValue(sourceObject, null);
                    infoArray2[i].SetValue(targetObject, obj2, null);
                }
            }
            return targetObject;
        }

        public static object CopyObjectValue(object sourceObject, object targetObject)
        {
            string name = string.Empty;
            FieldInfo[] fields = sourceObject.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++)
            {
                name = fields[i].Name;
                FieldInfo info = fields[i];
                smethod_0(targetObject, name, info.GetValue(sourceObject));
            }
            return targetObject;
        }

        public static int Delete(DataTable dataTable, string id)
        {
            return Delete(dataTable, FieldId, id);
        }

        public static int Delete(DataTable dataTable, string fieldName, string fieldValue)
        {
            int num = 0;
            using (IEnumerator enumerator = dataTable.Rows.GetEnumerator())
            {
                DataRow current;
                while (enumerator.MoveNext())
                {
                    current = (DataRow) enumerator.Current;
                    if (current[fieldName].ToString().Equals(fieldValue))
                    {
                        goto Label_003E;
                    }
                }
                return num;
            Label_003E:
                current.Delete();
                num++;
            }
            return num;
        }

        public static int EndDebug(MethodBase methodBase, int milliStart)
        {
            return EndDebug(methodBase, milliStart, ConsoleColor.White);
        }

        public static int EndDebug(MethodBase methodBase, int milliStart, ConsoleColor consoleColor)
        {
            int tickCount = Environment.TickCount;
            Console.Write(DateTime.Now.ToString("MM-dd HH:mm:ss") + " : " + TimeSpan.FromMilliseconds((double) (tickCount - milliStart)).ToString() + " : ");
            Console.ForegroundColor = consoleColor;
            Console.Write(methodBase.ReflectedType.Name + "." + methodBase.Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.DateTimeFormat) + " : " + TimeSpan.FromMilliseconds((double) (tickCount - milliStart)).ToString() + " : " + methodBase.ReflectedType.Name + "." + methodBase.Name);
            return (tickCount - milliStart);
        }

        public static bool Exists(string[] ids, string targetString)
        {
            if ((ids != null) && !string.IsNullOrEmpty(targetString))
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Equals(targetString))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Exists(DataTable dataTable, string fieldName, string fieldValue)
        {
            bool flag = false;
            if (dataTable != null)
            {
                using (IEnumerator enumerator = dataTable.Rows.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        DataRow current = (DataRow) enumerator.Current;
                        if (current[fieldName].ToString().Equals(fieldValue))
                        {
                            goto Label_004B;
                        }
                    }
                    return flag;
                Label_004B:
                    flag = true;
                }
            }
            return flag;
        }

        public static string[] FieldToArray(DataTable dataTable, string name)
        {
            string[] strArray = new string[0];
            int num = 0;
            string str = string.Empty;
            foreach (DataRow row in dataTable.Rows)
            {
                if (!string.IsNullOrEmpty(row[name].ToString()))
                {
                    num++;
                    str = str + row[name].ToString() + ",";
                }
            }
            if (num > 0)
            {
                strArray = str.TrimEnd(new char[] { ',' }).Split(new char[] { ',' });
            }
            return strArray;
        }

        public static string FieldToList(DataTable dataTable)
        {
            return FieldToList(dataTable, FieldId);
        }

        public static string FieldToList(DataTable dataTable, string name)
        {
            int num = 0;
            string str = "'";
            foreach (DataRow row in dataTable.Rows)
            {
                num++;
                str = str + row[name].ToString() + "', '";
            }
            if (num == 0)
            {
                return "''";
            }
            return str.Substring(0, str.Length - 3);
        }

        public static byte[] GetBinaryFormatData(DataTable dataTable)
        {
            byte[] buffer = null;
            dataTable.RemotingFormat = SerializationFormat.Binary;
            MemoryStream serializationStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(serializationStream, dataTable);
            buffer = serializationStream.ToArray();
            serializationStream.Close();
            serializationStream.Dispose();
            return buffer;
        }

        public static DataRow GetDataRow(DataTable dataTable, string id)
        {
            return GetDataRow(dataTable, FieldId, id);
        }

        public static DataRow GetDataRow(DataTable dataTable, string fieldName, string fieldValue)
        {
            DataRow row = null;
            using (IEnumerator enumerator = dataTable.Rows.GetEnumerator())
            {
                DataRow current;
                while (enumerator.MoveNext())
                {
                    current = (DataRow) enumerator.Current;
                    if ((current.RowState != DataRowState.Deleted) && current[fieldName].ToString().Equals(fieldValue))
                    {
                        goto Label_0049;
                    }
                }
                return row;
            Label_0049:
                row = current;
            }
            return row;
        }

        public static string GetDbHelperClass(CurrentDbType dbType)
        {
            string str = "RDIFramework.Utilities.SqlHelper";
            switch (dbType)
            {
                case CurrentDbType.Oracle:
                    return "RDIFramework.Utilities.MSOracleHelper";

                case CurrentDbType.SqlServer:
                    return "RDIFramework.Utilities.SqlHelper";

                case CurrentDbType.Access:
                    return str;

                case CurrentDbType.DB2:
                    return "RDIFramework.Utilities.DB2Helper";

                case CurrentDbType.MySql:
                    return "RDIFramework.Utilities.MySqlHelper";

                case CurrentDbType.SQLite:
                    return "RDIFramework.Utilities.SqLiteHelper";
            }
            return str;
        }

        public static string GetFriendlyFileSize(double fileSize)
        {
            if (fileSize < 1024.0)
            {
                return (fileSize.ToString("F1") + "Byte");
            }
            fileSize /= 1024.0;
            if (fileSize < 1024.0)
            {
                return (fileSize.ToString("F1") + "KB");
            }
            fileSize /= 1024.0;
            if (fileSize < 1024.0)
            {
                return (fileSize.ToString("F1") + "M");
            }
            fileSize /= 1024.0;
            return (fileSize.ToString("F1") + "GB");
        }

        public static int GetLanguageResource(object targetObject)
        {
            int num = 0;
            string key = string.Empty;
            FieldInfo[] fields = targetObject.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            string str2 = string.Empty;
            for (int i = 0; i < fields.Length; i++)
            {
                key = fields[i].Name;
                FieldInfo info = fields[i];
                str2 = ResourceManagerWrapper.Instance.Get(key);
                if (str2.Length > 0)
                {
                    info.SetValue(targetObject, str2);
                    num++;
                }
            }
            return num;
        }

        public static PermissionScope GetPermissionScope(string[] organizeIds)
        {
            PermissionScope none = PermissionScope.None;
            foreach (PermissionScope scope2 in (PermissionScope[]) Enum.GetValues(typeof(PermissionScope)))
            {
                if (Exists(organizeIds, scope2.ToString()))
                {
                    return scope2;
                }
            }
            return none;
        }

        public static string GetProperty(DataTable dataTable, string id, string targetField)
        {
            return GetProperty(dataTable, FieldId, id, targetField);
        }

        public static string GetProperty(DataTable dataTable, string fieldName, string fieldValue, string targetField)
        {
            string str = string.Empty;
            using (IEnumerator enumerator = dataTable.Rows.GetEnumerator())
            {
                DataRow current;
                while (enumerator.MoveNext())
                {
                    current = (DataRow) enumerator.Current;
                    if (current[fieldName].ToString().Equals(fieldValue))
                    {
                        goto Label_0042;
                    }
                }
                return str;
            Label_0042:
                str = current[targetField].ToString();
            }
            return str;
        }

        public static string GetPropertyDyn([Dynamic] object lstT, string id, string targetField)
        {
            return (string) GetPropertyDyn((dynamic) lstT, FieldId, id, targetField);
        }

        public static string GetPropertyDyn([Dynamic] object lstT, string fieldName, string fieldValue, string targetField)
        {
            string str = string.Empty;
            using (IEnumerator enumerator = ((IEnumerable) lstT).GetEnumerator())
            {
                object current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (<GetPropertyDyn>o__SiteContainer3.<>p__Site5 == null)
                    {
                        <GetPropertyDyn>o__SiteContainer3.<>p__Site5 = CallSite<System.Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BusinessLogic), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                    }
                    if (<GetPropertyDyn>o__SiteContainer3.<>p__Site5.Target(<GetPropertyDyn>o__SiteContainer3.<>p__Site5, ReflectHelper.GetProperty((dynamic) current, fieldName).ToString().Equals(fieldValue)))
                    {
                        goto Label_01CA;
                    }
                }
                return str;
            Label_01CA:
                if (<GetPropertyDyn>o__SiteContainer3.<>p__Site9 == null)
                {
                    <GetPropertyDyn>o__SiteContainer3.<>p__Site9 = CallSite<System.Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(BusinessLogic)));
                }
                return <GetPropertyDyn>o__SiteContainer3.<>p__Site9.Target(<GetPropertyDyn>o__SiteContainer3.<>p__Site9, ReflectHelper.GetProperty((dynamic) current, targetField).ToString());
            }
        }

        public static string GetSearchString(string searchValue)
        {
            searchValue = searchValue.Trim();
            searchValue = SqlSafe(searchValue);
            if (searchValue.Length > 0)
            {
                searchValue = searchValue.Replace('[', '_');
                searchValue = searchValue.Replace(']', '_');
            }
            if (searchValue == "%")
            {
                searchValue = "[%]";
            }
            if (((searchValue.Length > 0) && (searchValue.IndexOf('%') < 0)) && (searchValue.IndexOf('_') < 0))
            {
                searchValue = "%" + searchValue + "%";
            }
            return searchValue;
        }

        public static byte[] ImageToBytes(Image Image, ImageFormat imageFormat)
        {
            if (Image == null)
            {
                return null;
            }
            byte[] buffer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(Image))
                {
                    bitmap.Save(stream, imageFormat);
                    stream.Position = 0L;
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                    stream.Flush();
                }
            }
            return buffer;
        }

        public static bool IsAuthorized(DataTable dataTable, string permissionItemCode)
        {
            return Exists(dataTable, FieldCode, permissionItemCode);
        }

        public static bool IsKeywords(string field)
        {
            field = field.Substring(0, 1).ToUpper() + field.Substring(1);
            string[] strArray2 = new string[] { "Id", "SortCode", "DeleteMark", "Enabled", "CreateOn", "CreateUserId", "CreateBy", "ModifiedOn", "ModifiedUserId", "ModifiedBy" };
            for (int i = 0; i < strArray2.Length; i++)
            {
                if (strArray2[i].ToUpper().Equals(field.ToUpper()))
                {
                    field = strArray2[i];
                    return true;
                }
            }
            return false;
        }

        public static string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public static string ObjectsToList(object[] ids)
        {
            string str2 = "'";
            for (int i = 0; i < ids.Length; i++)
            {
                str2 = str2 + ids[i] + "', '";
            }
            if (ids.Length == 0)
            {
                return " NULL ";
            }
            return str2.Substring(0, str2.Length - 3);
        }

        public static string[] Remove(string[] ids, string id)
        {
            Hashtable hashtable = new Hashtable();
            if (ids != null)
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    if (!((ids[j] == null) || ids[j].Equals(id)) && !hashtable.ContainsKey(ids[j]))
                    {
                        hashtable.Add(ids[j], ids[j]);
                    }
                }
            }
            string[] strArray = new string[hashtable.Count];
            IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); i++)
            {
                strArray[i] = enumerator.Key.ToString();
            }
            return strArray;
        }

        public static string RepeatString(string targetString, int repeatCount)
        {
            string str = string.Empty;
            for (int i = 0; i < repeatCount; i++)
            {
                str = str + targetString;
            }
            return str;
        }

        public static DataTable RetrieveDataTable(byte[] arrayResult)
        {
            MemoryStream serializationStream = new MemoryStream(arrayResult);
            IFormatter formatter = new BinaryFormatter();
            object obj2 = formatter.Deserialize(serializationStream);
            serializationStream.Close();
            serializationStream.Dispose();
            return (DataTable) obj2;
        }

        public static DataTable SetColumnsFilter(DataTable dataTable, string[] columns)
        {
            for (int i = dataTable.Columns.Count - 1; i > 0; i--)
            {
                if (!IsKeywords(dataTable.Columns[i].ColumnName) && !Exists(columns, dataTable.Columns[i].ColumnName))
                {
                    dataTable.Columns.RemoveAt(i);
                }
            }
            return dataTable;
        }

        public static DataTable SetFilter(DataTable dataTable, string fieldName, string fieldValue, bool equals = false)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                if (equals)
                {
                    if (string.IsNullOrEmpty(fieldValue))
                    {
                        if (string.IsNullOrEmpty(row[fieldName].ToString()))
                        {
                            row.Delete();
                        }
                    }
                    else if (row[fieldName].ToString().Equals(fieldValue))
                    {
                        row.Delete();
                    }
                }
                else if (string.IsNullOrEmpty(fieldValue))
                {
                    if (!string.IsNullOrEmpty(row[fieldName].ToString()))
                    {
                        row.Delete();
                    }
                }
                else if (!row[fieldName].ToString().Equals(fieldValue))
                {
                    row.Delete();
                }
            }
            dataTable.AcceptChanges();
            return dataTable;
        }

        public static int SetProperty(DataTable dataTable, string id, string targetField, object targetValue)
        {
            return SetProperty(dataTable, FieldId, id, targetField, targetValue);
        }

        public static int SetProperty(DataTable dataTable, string fieldName, string fieldValue, string targetField, object targetValue)
        {
            int num = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                if ((row.RowState != DataRowState.Deleted) && row[fieldName].ToString().Equals(fieldValue))
                {
                    row[targetField] = targetValue;
                    num++;
                }
            }
            return num;
        }

        public static int SetPropertyDyn([Dynamic] object lstT, string id, string targetField, object targetValue)
        {
            return (int) SetPropertyDyn((dynamic) lstT, FieldId, id, targetField, targetValue);
        }

        public static int SetPropertyDyn([Dynamic] object lstT, string fieldName, string fieldValue, string targetField, object targetValue)
        {
            int num = 0;
            int num2 = 0;
            while (true)
            {
                if (<SetPropertyDyn>o__SiteContainerf.<>p__Site17 == null)
                {
                    <SetPropertyDyn>o__SiteContainerf.<>p__Site17 = CallSite<System.Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BusinessLogic), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                }
                if (!<SetPropertyDyn>o__SiteContainerf.<>p__Site17.Target(<SetPropertyDyn>o__SiteContainerf.<>p__Site17, num2 < ((dynamic) lstT).Count))
                {
                    return num;
                }
                object obj2 = ((dynamic) lstT)[num2];
                if (<SetPropertyDyn>o__SiteContainerf.<>p__Site11 == null)
                {
                    <SetPropertyDyn>o__SiteContainerf.<>p__Site11 = CallSite<System.Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BusinessLogic), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                }
                if (<SetPropertyDyn>o__SiteContainerf.<>p__Site11.Target(<SetPropertyDyn>o__SiteContainerf.<>p__Site11, ReflectHelper.GetProperty((dynamic) obj2, fieldName).ToString().Equals(fieldValue)))
                {
                    ReflectHelper.SetProperty((dynamic) obj2, targetField, targetValue);
                    ((dynamic) lstT)[num2] = obj2;
                    num++;
                }
                num2++;
            }
        }

        private static int smethod_0(object object_0, string string_0, object object_1)
        {
            int num = 0;
            Type type = object_0.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++)
            {
                if (string_0.Equals(fields[i].Name))
                {
                    type.GetField(string_0).SetValue(object_0, object_1);
                    num++;
                    return num;
                }
            }
            return num;
        }

        public static string SqlSafe(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }

        public static int StartDebug(MethodBase methodBase)
        {
            Console.WriteLine(DateTime.Now.ToString(SystemInfo.DateTimeFormat) + " :Begin: " + methodBase.ReflectedType.Name + "." + methodBase.Name);
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.DateTimeFormat) + " :Begin: " + methodBase.ReflectedType.Name + "." + methodBase.Name);
            return Environment.TickCount;
        }

        public static int StartDebug(UserInfo userInfo, MethodBase methodBase)
        {
            if (userInfo != null)
            {
                Console.WriteLine("User: " + userInfo.RealName + " IP: " + userInfo.String_0);
                Console.WriteLine(DateTime.Now.ToString(SystemInfo.DateTimeFormat) + " :Begin: " + methodBase.ReflectedType.Name + "." + methodBase.Name);
                Trace.WriteLine(DateTime.Now.ToString(SystemInfo.DateTimeFormat) + " :Begin: " + methodBase.ReflectedType.Name + "." + methodBase.Name);
            }
            return Environment.TickCount;
        }

        public static void WriteDebug(UserInfo userInfo, MethodBase methodBase)
        {
            Console.WriteLine(DateTime.Now.ToString(SystemInfo.DateTimeFormat) + " " + userInfo.String_0 + methodBase.ReflectedType.Name + "." + methodBase.Name);
            Trace.WriteLine(DateTime.Now.ToString(SystemInfo.DateTimeFormat) + " " + userInfo.String_0 + methodBase.ReflectedType.Name + "." + methodBase.Name);
        }

        public delegate bool CheckMoveEventHandler(string selectedId);

        public delegate void SelectedIndexChangedEventHandler(string selectedId);
    }
}

