using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    //网页抓取常用类
    public class WebGrab
    {
        #region 日期随机函数

        /**********************************
         * 函数名称:DateRndName
         * 功能说明:日期随机函数
         * 参    数:ra:随机数
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          Random ra = new Random();
         *          string s = o.DateRndName(ra);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 日期随机函数
        /// </summary>
        /// <param name="ra">随机数</param>
        /// <returns></returns>
        public string DateRndName(Random ra)
        {
            var d = DateTime.Now;
            string s = null;
            var y = d.Year.ToString(CultureInfo.InvariantCulture);
            var m = d.Month.ToString(CultureInfo.InvariantCulture);
            if (m.Length < 2) m = "0" + m;
            var dd = d.Day.ToString(CultureInfo.InvariantCulture);
            if (dd.Length < 2) dd = "0" + dd;
            var h = d.Hour.ToString(CultureInfo.InvariantCulture);
            if (h.Length < 2) h = "0" + h;
            var mm = d.Minute.ToString(CultureInfo.InvariantCulture);
            if (mm.Length < 2) mm = "0" + mm;
            var ss = d.Second.ToString(CultureInfo.InvariantCulture);
            if (ss.Length < 2) ss = "0" + ss;
            s += y + ',' + m + ',' + dd + ',' + h + "-" + mm + "-" + ss;
            s += ra.Next(1000000, 9999999).ToString(CultureInfo.InvariantCulture);
            return s;
        }

        #endregion

        #region 取得文件后缀

        /**********************************
         * 函数名称:GetFileExtends
         * 功能说明:取得文件后缀
         * 参    数:filename:文件名称
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string url = @"http://www.baidu.com/img/logo.gif";
         *          string s = o.GetFileExtends(url);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 取得文件后缀
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        public string GetFileExtends(string filename)
        {
            string ext = null;
            if (filename.IndexOf('.') > 0)
            {
                var fs = filename.Split('.');
                ext = fs[fs.Length - 1];
            }
            return ext;
        }

        #endregion

        #region 获取远程文件源代码

        /**********************************
         * 函数名称:GetRemoteHtmlCode
         * 功能说明:获取远程文件源代码
         * 参    数:Url:远程url
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string url = @"http://www.baidu.com";
         *          string s = o.GetRemoteHtmlCode(url);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 获取远程文件源代码
        /// </summary>
        /// <param name="url">远程url</param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public string GetRemoteHtmlCode(string url, string charset)
        {
            var oWebRqst = WebRequest.Create(url);

            oWebRqst.Timeout = 50000;

            var oWebRps = oWebRqst.GetResponse();

            var oStreamRd = new StreamReader(oWebRps.GetResponseStream(), Encoding.GetEncoding(charset));

            var sRslt = oStreamRd.ReadToEnd();
            oStreamRd.Close();
            oWebRps.Close();

            return sRslt;
        }

        #endregion

        #region 替换网页中的换行和引号

        /**********************************
         * 函数名称:ReplaceEnter
         * 功能说明:替换网页中的换行和引号
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.baidu.com";
         *          string HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.ReplaceEnter(HtmlCode);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 替换网页中的换行和引号
        /// </summary>
        /// <param name="HtmlCode">HTML源代码</param>
        /// <returns></returns>
        public string ReplaceEnter(string HtmlCode)
        {
            string s = "";
            if (!string.IsNullOrEmpty(HtmlCode))
            {
                s = HtmlCode.Replace("\"", "");
                s = s.Replace("\r", "");
                s = s.Replace("\n", "");
            }
            return s;
        }

        #endregion

        #region 执行正则提取出值

        /**********************************
         * 函数名称:GetRegValue
         * 功能说明:执行正则提取出值
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.baidu.com";
         *          string HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.ReplaceEnter(HtmlCode);
         *          string Reg="<title>.+?</title>";
         *          string GetValue=o.GetRegValue(Reg,HtmlCode)
         *          Response.Write(GetValue);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 执行正则提取出值
        /// </summary>
        /// <param name="RegexString">正则表达式</param>
        /// <param name="RemoteStr">HtmlCode源代码</param>
        /// <returns></returns>
        public string GetRegValue(string RegexString, string RemoteStr)
        {
            string matchVale = "";

            if (!string.IsNullOrEmpty(RegexString) && !string.IsNullOrEmpty(RemoteStr))
            {
                var r = new Regex(RegexString);
                Match m = r.Match(RemoteStr);
                if (m.Success)
                {
                    matchVale = m.Value;
                }
            }
            return matchVale;
        }

        #endregion

        #region 替换HTML源代码

        /**********************************
         * 函数名称:RemoveHTML
         * 功能说明:替换HTML源代码
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.baidu.com";
         *          string HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.ReplaceEnter(HtmlCode);
         *          string Reg="<title>.+?</title>";
         *          string GetValue=o.GetRegValue(Reg,HtmlCode)
         *          Response.Write(GetValue);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 替换HTML源代码
        /// </summary>
        /// <param name="HtmlCode">html源代码</param>
        /// <returns></returns>
        public string RemoveHTML(string HtmlCode)
        {
            string matchVale = HtmlCode;
            foreach (Match s in Regex.Matches(HtmlCode, "<.+?>"))
            {
                matchVale = matchVale.Replace(s.Value, "");
            }
            return matchVale;
        }

        #endregion

        #region 匹配页面的链接

        /**********************************
         * 函数名称:GetHref
         * 功能说明:匹配页面的链接
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.baidu.com";
         *          string HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.GetHref(HtmlCode);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 获取页面的链接正则
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <returns></returns>
        public ArrayList GetHref(string HtmlCode)
        {
            var linklist = new ArrayList();
            string Reg = @"(h|H)(r|R)(e|E)(f|F) *= *('|""|>)?((\w|\\|\/|\.|:|-|_)+)[\S]*";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                linklist.Add((m.Value).ToLower().Replace("href=", "").Trim());
            }

            Reg = @"<url>([\s\S]*?)</url>";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                linklist.Add((m.Value).ToLower().Replace("<url>", "").Replace("</url>", "").Trim());
            }
            return linklist;
        }

        #endregion

        #region 匹配页面的图片地址

        /**********************************
         * 函数名称:GetImgSrc
         * 功能说明:匹配页面的图片地址
         * 参    数:HtmlCode:html源代码;imgHttp:要补充的http.当比如:<img src="bb/x.gif">则要补充http://www.baidu.com/,当包含http信息时,则可以为空
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.baidu.com";
         *          string HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.GetImgSrc(HtmlCode,"http://www.baidu.com/");
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 匹配页面的图片地址
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <param name="imgHttp">要补充的http://路径信息</param>
        /// <returns></returns>
        public string GetImgSrc(string HtmlCode, string imgHttp)
        {
            string matchVale = "";
            string Reg = @"<img.+?>";
            foreach (Match m in Regex.Matches(HtmlCode.ToLower(), Reg))
            {
                matchVale += GetImg((m.Value).ToLower().Trim(), imgHttp) + "|";
            }

            return matchVale;
        }

        /// <summary>
        /// 匹配<img src="" />中的图片路径实际链接
        /// </summary>
        /// <param name="ImgString"><img src="" />字符串</param>
        /// <returns></returns>
        public string GetImg(string ImgString, string imgHttp)
        {
            string matchVale = "";
            string Reg = @"src=.+\.(bmp|jpg|gif|png|)";
            foreach (Match m in Regex.Matches(ImgString.ToLower(), Reg))
            {
                matchVale += (m.Value).ToLower().Trim().Replace("src=", "");
            }
            if (matchVale.IndexOf(".net") != -1 || matchVale.IndexOf(".com") != -1 || matchVale.IndexOf(".org") != -1 ||
                matchVale.IndexOf(".cn") != -1 || matchVale.IndexOf(".cc") != -1 || matchVale.IndexOf(".info") != -1 ||
                matchVale.IndexOf(".biz") != -1 || matchVale.IndexOf(".tv") != -1)
                return (matchVale);
            else
                return (imgHttp + matchVale);
        }

        #endregion

        #region 替换通过正则获取字符串所带的正则首尾匹配字符串

        /**********************************
         * 函数名称:GetHref
         * 功能说明:匹配页面的链接
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.baidu.com";
         *          string HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.RegReplace(HtmlCode,"<title>","</title>");
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/

        /// <summary>
        /// 替换通过正则获取字符串所带的正则首尾匹配字符串
        /// </summary>
        /// <param name="RegValue">要替换的值</param>
        /// <param name="regStart">正则匹配的首字符串</param>
        /// <param name="regEnd">正则匹配的尾字符串</param>
        /// <returns></returns>
        public string RegReplace(string RegValue, string regStart, string regEnd)
        {
            string s = RegValue;
            if (!string.IsNullOrEmpty(RegValue))
            {
                if (!string.IsNullOrEmpty(regStart))
                {
                    s = s.Replace(regStart, "");
                }
                if (!string.IsNullOrEmpty(regEnd))
                {
                    s = s.Replace(regEnd, "");
                }
            }
            return s;
        }

        #endregion
    }
}