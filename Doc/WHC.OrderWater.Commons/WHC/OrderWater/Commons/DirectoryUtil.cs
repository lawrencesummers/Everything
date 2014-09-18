namespace WHC.OrderWater.Commons
{
    using System;
    using System.IO;

    public class DirectoryUtil
    {
        public static void AssertDirExist(string filePath)
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }
        }

        public static void ClearDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                int num;
                string[] fileNames = GetFileNames(directoryPath);
                for (num = 0; num < fileNames.Length; num++)
                {
                    FileUtil.DeleteFile(fileNames[num]);
                }
                string[] directories = GetDirectories(directoryPath);
                for (num = 0; num < directories.Length; num++)
                {
                    DeleteDirectory(directories[num]);
                }
            }
        }

        public static bool ContainFile(string directoryPath, string searchPattern)
        {
            bool flag;
            try
            {
                if (GetFileNames(directoryPath, searchPattern, false).Length == 0)
                {
                    return false;
                }
                flag = true;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool ContainFile(string directoryPath, string searchPattern, bool isSearchChild)
        {
            bool flag;
            try
            {
                if (GetFileNames(directoryPath, searchPattern, true).Length == 0)
                {
                    return false;
                }
                flag = true;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return flag;
        }

        public static ulong ConvertByteCountToKByteCount(ulong byteCount)
        {
            return (byteCount / 0x400);
        }

        public static float ConvertKByteCountToMByteCount(ulong kByteCount)
        {
            return (float) (kByteCount / 0x400);
        }

        public static float ConvertMByteCountToGByteCount(float kByteCount)
        {
            return (kByteCount / 1024f);
        }

        public static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static string CreateDirectoryByDate(string rootPath)
        {
            return CreateDirectoryByDate(rootPath, "yyyy-MM-dd");
        }

        public static string CreateDirectoryByDate(string rootPath, string formatString)
        {
            string str;
            if (!IsExistDirectory(rootPath))
            {
                throw new DirectoryNotFoundException("the rootPath is not found");
            }
            bool flag = false;
            string str3 = formatString;
            if (str3 == null)
            {
                goto Label_0043;
            }
            if (!(str3 == "yyyy-MM-dd"))
            {
                if (!(str3 == "yyyy-MM-dd-HH"))
                {
                    goto Label_0043;
                }
                flag = true;
            }
            else
            {
                flag = false;
            }
            goto Label_0046;
        Label_0043:
            flag = false;
        Label_0046:
            str = rootPath + @"\" + DateTime.Now.Year.ToString();
            CreateDirectory(str);
            str = str + @"\" + DateTime.Now.Month.ToString("00");
            CreateDirectory(str);
            str = str + @"\" + DateTime.Now.Day.ToString("00");
            CreateDirectory(str);
            if (flag)
            {
                str = str + @"\" + DateTime.Now.Hour.ToString("00");
                CreateDirectory(str);
            }
            return str;
        }

        public static void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        public static DriveInfo[] GetAllDrives()
        {
            return DriveInfo.GetDrives();
        }

        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string[] GetDirectories(string directoryPath)
        {
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(directoryPath);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return directories;
        }

        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] strArray;
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                strArray = Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return strArray;
        }

        public static string[] GetFileNames(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            return Directory.GetFiles(directoryPath);
        }

        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] strArray;
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                strArray = Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return strArray;
        }

        public static ulong GetFreeSpace(string driveName)
        {
            ulong availableFreeSpace = 0;
            try
            {
                DriveInfo info = new DriveInfo(driveName);
                availableFreeSpace = (ulong) info.AvailableFreeSpace;
            }
            catch
            {
            }
            return availableFreeSpace;
        }

        public static char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        public static string GetSpeicalFolder(Environment.SpecialFolder folderType)
        {
            return Environment.GetFolderPath(folderType);
        }

        public static string GetSystemDirectory()
        {
            return Environment.SystemDirectory;
        }

        public static string GetTempPath()
        {
            return Path.GetTempPath();
        }

        public static bool IsDiskSpaceEnough(string path, ulong requiredSpace)
        {
            ulong freeSpace = GetFreeSpace(Path.GetPathRoot(path));
            return (requiredSpace <= freeSpace);
        }

        public static bool IsEmptyDirectory(string directoryPath)
        {
            bool flag;
            try
            {
                if (GetFileNames(directoryPath).Length > 0)
                {
                    return false;
                }
                if (GetDirectories(directoryPath).Length > 0)
                {
                    return false;
                }
                flag = true;
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public static bool IsWriteable(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    return false;
                }
            }
            try
            {
                string str = ".test." + Guid.NewGuid().ToString().Substring(0, 5);
                string str2 = Path.Combine(path, str);
                File.WriteAllLines(str2, new string[] { "test" });
                File.Delete(str2);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }
    }
}

