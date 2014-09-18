namespace WHC.OrderWater.Commons
{
    using System;

    public class GZipResult
    {
        public int CompressionPercent = 0;
        public bool Errors = false;
        public int FileCount = 0;
        public GZipFileInfo[] Files = null;
        public string TempFile = null;
        public bool TempFileDeleted = false;
        public long TempFileSize = 0;
        public string ZipFile = null;
        public long ZipFileSize = 0;
    }
}

