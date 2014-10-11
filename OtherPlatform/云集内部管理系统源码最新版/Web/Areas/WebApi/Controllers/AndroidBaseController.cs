using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using IServices.Infrastructure;
using Models.UserModels;
using Newtonsoft.Json;
using Services.Infrastructure;

namespace Web.Areas.WebApi.Controllers
{
    public class AndroidBaseController<T> :Controller
    {
        
        //将用户编辑/添加时，客户端发送过来的json实体反序列化
        public T DeserializeObject(string entity)
        {
            //string pattern1 = "\"[^\"]+\":null,";//过滤非最后一项为null的字段  此处在客户端过滤，减少提交的数据量
            const string pattern2 = ",\"[^\"]+\":null"; //过滤最后一项为null的属性
            //entity = Regex.Replace(entity, pattern1, string.Empty, RegexOptions.IgnoreCase);
            entity = Regex.Replace(entity, pattern2, string.Empty, RegexOptions.IgnoreCase);
            return (T) JsonConvert.DeserializeObject(entity, typeof (T));
        }


    }
}