using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DownLoadFile("http://192.168.5.188/WDCEService/", "DefaultFileTree.xml");
        }
        //private void DownFileExcel(string filename)
        //{//文件下载
        //    string filepath=Server.MapPath("xls/"+filename);
        //    try
        //    {
        //        WebRequest myre=WebRequest.Create(filepath);
        //        string newfilename="c:\\"+filename;//newfilename为存放本地的文件路径
        //        DownLoadFile(filepath,newfilename);
        //    }
        //    catch(System.Exception ee)
        //    {
        //        string dd=ee.ToString();
        //        Response.Write("文件无法下载");
        //    }
        //}
        #region 文件下载
        static void DownLoadFile(string address,string filename)
        {//address 文件下载路径,filename文件存放的本地路径
            WebClient client=new WebClient();
            client.DownloadFile(address,filename);
            Stream str=client.OpenRead(address);
            StreamReader reader=new StreamReader(str);
            byte[] mbyte=new byte[str.Length+1];
            int allmybyte=(int)mbyte.Length;
            int startmbyte=0;
            while(allmybyte>0)
            {
                int m=str.Read(mbyte,startmbyte,allmybyte);
                if(m==0)
                {
                    break;
                }
                startmbyte+=m;
                allmybyte-=m;
            }
            FileStream fstr=new FileStream(filename,FileMode.OpenOrCreate,FileAccess.Write);
            fstr.Write(mbyte,0,startmbyte);
            str.Close();
            fstr.Close();
        }
        #endregion
    }
}
