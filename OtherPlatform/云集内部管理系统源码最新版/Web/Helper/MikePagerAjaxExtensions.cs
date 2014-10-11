using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Common;

namespace Web.Helper
{
    public static class MikePagerAjaxExtensions
    {
        #region MikePager 分页控件

        public static String MikePager<T>(this AjaxHelper html, PagedList<T> data)
        {
            return html.MikePager(data.PageIndex, data.PageSize, data.TotalCount);
        }

        public static String MikePager(this AjaxHelper html, int pageIndex, int pageSize, int totalCount)
        {
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            int start = (pageIndex - 5) >= 1 ? (pageIndex - 5) : 1;
            int end = (totalPage - start) > 10 ? start + 10 : totalPage;

            RouteValueDictionary vs = html.ViewContext.RouteData.Values;

            NameValueCollection queryString = html.ViewContext.HttpContext.Request.QueryString;
            foreach (string key in queryString.Keys)
                vs[key] = queryString[key];

            NameValueCollection formString = html.ViewContext.HttpContext.Request.Form;
            foreach (string key in formString.Keys)
                vs[key] = formString[key];

            vs.Remove("X-Requested-With");
            vs.Remove("X-HTTP-Method-Override");

            var builder = new StringBuilder();
            builder.AppendFormat("<div class=\"pull-right\"><ul class=\"pagination\">");

            //vs["pageSize"] = data.PageSize;
            if (pageIndex > 1)
            {
                vs["pageIndex"] = 1;

                builder.Append("<li>");
                builder.Append(html.ActionLink("|<", vs["action"].ToString(), vs,
                    new AjaxOptions { UpdateTargetId = "Main" }));
                builder.Append("</li>");

                vs["pageIndex"] = pageIndex - 1;
                builder.Append("<li class=\"ui-state-default  ui-corner-all\">");
                builder.Append(html.ActionLink("<", vs["action"].ToString(), vs,
                    new AjaxOptions { UpdateTargetId = "Main" }));
                builder.Append("</li>");
            }

            for (int i = start; i <= end; i++) //前后各显示5个数字页码
            {
                vs["pageIndex"] = i;

                if (i == pageIndex)
                {
                    builder.Append("<li class=\"active\"><a href=\"#\">");
                    builder.Append(i);
                    builder.Append("</a></li>");
                }
                else
                {
                    builder.Append("<li>");
                    builder.Append(html.ActionLink(i.ToString(CultureInfo.InvariantCulture), vs["action"].ToString(), vs,
                        new AjaxOptions { UpdateTargetId = "Main" }));
                    builder.Append("</li>");
                }
            }

            if ((pageIndex * pageSize) < totalCount)
            {
                vs["pageIndex"] = pageIndex + 1;
                builder.Append("<li>");
                builder.Append(html.ActionLink(">", vs["action"].ToString(), vs,
                    new AjaxOptions { UpdateTargetId = "Main" }));
                builder.Append("</li>");

                vs["pageIndex"] = totalPage;
                builder.Append("<li>");
                builder.Append(html.ActionLink(">|", vs["action"].ToString(), vs,
                    new AjaxOptions { UpdateTargetId = "Main" }));
                builder.Append("</li>");
            }
            builder.Append("<li>");


            var url = new UrlHelper(html.ViewContext.RequestContext);
            vs.Remove("pageIndex");
            builder.Append("<span>");
            builder.Append("<form action=\"" + url.Action(vs["action"].ToString(), vs) +
                           "\" data-ajax=\"true\"  data-ajax-mode=\"replace\" data-ajax-update=\"#Main\" id=\"form1\" method=\"post\">");
            builder.Append("每页" + pageSize + "条/共" + totalCount + "条 第");
            builder.Append("<input type=\"text\" id=\"pageIndex\" name=\"pageIndex\" size=4  style=\"border:0;border-bottom: 1px solid white;text-align: center;padding:0;  background-color:transparent;\" value=" + pageIndex + " />");
            builder.Append("页/共" + totalPage + "页");
            builder.Append("</form>");

            builder.Append("</span>");
            builder.Append("</li></ul>");
            builder.Append("</div>");
            return builder.ToString();
        }

        #endregion
    }
}