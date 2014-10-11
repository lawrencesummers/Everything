using System.Web;
using System.Xml;

namespace Common
{
    public class WebAppSetting
    {
        /// 
        /// 修改web.config文件appSettings配置节中的Add里的value属性
        /// 
        /// 
        /// 注意，调用该函数后，会使整个Web Application重启，导致当前所有的会话丢失
        /// 
        /// 要修改的键key
        /// 修改后的value
        /// 找不到相关的键
        /// 权限不够，无法保存到web.config文件中
        public void Modify(string key, string strValue)
        {
            const string xPath = "/appSettings/add[@key='?']";
            var domWebConfig = new XmlDocument();

            domWebConfig.Load((HttpContext.Current.Server.MapPath("~/WebAppSettings.config")));
            XmlNode addKey = domWebConfig.SelectSingleNode((xPath.Replace("?", key)));


            if (addKey != null) if (addKey.Attributes != null) addKey.Attributes["value"].InnerText = strValue;
            domWebConfig.Save((HttpContext.Current.Server.MapPath("~/WebAppSettings.config")));
        }
    }
}