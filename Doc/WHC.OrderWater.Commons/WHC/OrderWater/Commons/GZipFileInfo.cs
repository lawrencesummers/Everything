namespace WHC.OrderWater.Commons
{
    using System;

    public class GZipFileInfo
    {
        public bool AddedToTempFile = false;
        public string Folder = null;
        public int Index = 0;
        public int Length = 0;
        public string LocalPath = null;
        public DateTime ModifiedDate;
        public string RelativePath = null;
        public bool Restored = false;
        public bool RestoreRequested = false;

        public bool ParseFileInfo(string fileInfo)
        {
            bool flag = false;
            try
            {
                if (!string.IsNullOrEmpty(fileInfo))
                {
                    string[] strArray = fileInfo.Split(new char[] { ',' });
                    if ((strArray != null) && (strArray.Length == 4))
                    {
                        this.Index = Convert.ToInt32(strArray[0]);
                        this.RelativePath = strArray[1].Replace("/", @"\");
                        this.ModifiedDate = Convert.ToDateTime(strArray[2]);
                        this.Length = Convert.ToInt32(strArray[3]);
                        flag = true;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }
}

