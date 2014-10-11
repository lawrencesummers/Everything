using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Common
{
    public interface IPagedList
    {
        int TotalCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }

    public class PagedList<T> : List<T>, IPagedList
    {
        public PagedList(IQueryable source, int index, int pageSize)
        {
            TotalCount = source.Count();
            PageSize = pageSize;
            PageIndex = index;
            AddRange((IEnumerable<T>) source.Skip((index - 1)*pageSize).Take(pageSize));
        }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
       
    }

    public static class Pagination
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index)
        {
            int pageSize = 20;
            if (ConfigurationManager.AppSettings["DefaultPageSize"] != null)
            {
                pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPageSize"]);
            }
            return new PagedList<T>(source, index, pageSize);
        }
    }
}