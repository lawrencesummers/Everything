namespace WHC.OrderWater.Commons
{
    using System;

    [Serializable]
    public class TestEntity
    {
        private string string_0 = "http://www.baidu.com";
        private string string_1 = "百度一下";
        private string string_2 = "GB2312";

        public string TestUrl
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

        public string TestWebEncoding
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string TestWebTitle
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }
    }
}

