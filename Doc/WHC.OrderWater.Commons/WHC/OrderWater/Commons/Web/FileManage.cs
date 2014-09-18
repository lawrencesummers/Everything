namespace WHC.OrderWater.Commons.Web
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
    using System.Web.UI.WebControls;

    public class FileManage
    {
        public static void DownLoad(string FileName)
        {
            string path = smethod_0(FileName);
            long num = 0x32000;
            byte[] buffer = new byte[0x32000L];
            long length = 0;
            FileStream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                length = stream.Length;
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(path)));
                HttpContext.Current.Response.AddHeader("Content-Length", length.ToString());
                while (length > 0)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int count = stream.Read(buffer, 0, Convert.ToInt32(num));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        length -= count;
                    }
                    else
                    {
                        length = -1;
                    }
                }
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Write("Error:" + exception.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                HttpContext.Current.Response.Close();
            }
        }

        public static bool ResponseFile(HttpRequest request, HttpResponse response, string fileName, string fullPath, long speed)
        {
            try
            {
                FileStream input = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader reader = new BinaryReader(input);
                try
                {
                    response.AddHeader("Accept-Ranges", "bytes");
                    response.Buffer = false;
                    long length = input.Length;
                    long num2 = 0;
                    int count = 0x2800;
                    int millisecondsTimeout = ((int) Math.Floor((double) (((long) (0x3e8 * 0x2800)) / speed))) + 1;
                    if (request.Headers["Range"] != null)
                    {
                        response.StatusCode = 0xce;
                        num2 = Convert.ToInt64(request.Headers["Range"].Split(new char[] { '=', '-' })[1]);
                    }
                    response.AddHeader("Content-Length", (length - num2).ToString());
                    if (num2 != 0)
                    {
                        response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", num2, length - 1, length));
                    }
                    response.AddHeader("Connection", "Keep-Alive");
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
                    reader.BaseStream.Seek(num2, SeekOrigin.Begin);
                    int num6 = ((int) Math.Floor((double) ((length - num2) / ((long) count)))) + 1;
                    for (int i = 0; i < num6; i++)
                    {
                        if (response.IsClientConnected)
                        {
                            response.BinaryWrite(reader.ReadBytes(count));
                            Thread.Sleep(millisecondsTimeout);
                        }
                        else
                        {
                            i = num6;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    reader.Close();
                    input.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string SaveUrlPics(string strHTML, string path)
        {
            string str = DateTime.Now.ToString("yyyy-MM");
            string str2 = DateTime.Now.ToString("dd");
            path = path + str + "/" + str2;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] strArray = smethod_1(strHTML);
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str3 = (DateTime.Now.ToString() + "_").Replace("-", "").Replace(":", "").Replace(" ", "");
                    new WebClient().DownloadFile(strArray[i], path + "/" + str3 + strArray[i].Substring(strArray[i].LastIndexOf("/") + 1));
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return strHTML;
        }

        private static string smethod_0(string string_0)
        {
            return HttpContext.Current.Server.MapPath(string_0);
        }

        private static string[] smethod_1(string string_0)
        {
            Regex regex = new Regex("<img.+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] strArray = new string[regex.Matches(string_0).Count];
            int index = 0;
            foreach (Match match in regex.Matches(string_0))
            {
                strArray[index] = TaetWbZiss(match.Value);
                index++;
            }
            return strArray;
        }

        private static string TaetWbZiss(string string_0)
        {
            string str = "";
            Regex regex = new Regex("http://.+.(?:jpg|gif|bmp|png)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(string_0))
            {
                str = match.Value;
            }
            return str;
        }

        public static string UploadImage(FileUpload posPhotoUpload, string saveFileName, string imagePath)
        {
            string str = "";
            if (posPhotoUpload.HasFile)
            {
                if ((posPhotoUpload.PostedFile.ContentLength / 0x400) < 0x2800)
                {
                    string contentType = posPhotoUpload.PostedFile.ContentType;
                    if (string.Equals(contentType, "image/gif") || string.Equals(contentType, "image/pjpeg"))
                    {
                        Path.GetExtension(posPhotoUpload.PostedFile.FileName);
                        posPhotoUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(imagePath));
                        return str;
                    }
                    return "上传文件类型不正确";
                }
                return "上传文件不能大于10M";
            }
            return "没有上传文件";
        }
    }
}

