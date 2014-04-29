using System;
using System.Collections.Generic;
using System.Linq;

namespace WebPrint.Model.Helper
{
    public interface IPagedList
    {
        int PageIndex { get; }

        bool HasNextPage { get; }

        bool HasPreviousPage { get; }

        int PageSize { get; }

        int RecordsCount { get; }

        int PageCount { get; }
    }

    /// <summary>
    /// <para>Adapted from http://blog.wekeroad.com/2007/12/10/aspnet-mvc-pagedlistt/ </para>
    /// <para>Because every database have it's own pagination method, so method Skip and Take of IQueryable is also different</para>
    /// </summary>
    /// <typeparam name="T">The type of item this list holds</typeparam>

    public class PagedList<T> : List<T>, IPagedList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">源数据 已经是当前分页的数据</param>
        /// <param name="pageIndex">The current page number (1 based).</param>
        /// <param name="pageSize">Size of a page (number of items per page).</param>
        /// <param name="recordsCount">总记录数</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int recordsCount)
        {
            this.RecordsCount = recordsCount;
            this.PageSize = pageSize;
            this.PageIndex = Math.Min(Math.Max(1, pageIndex), PageCount);
            this.AddRange(source);
        }

        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            this.RecordsCount = source.Count();
            this.PageSize = Math.Max(pageSize, 1);
            this.PageIndex = Math.Min(Math.Max(1, pageIndex), PageCount);
            this.AddRange(source.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList());
        }

        #region IPagedList 成员

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool HasPreviousPage { get { return (PageIndex > 1); } }

        public bool HasNextPage { get { return (PageIndex * PageSize) < RecordsCount; } }

        public int PageCount { get { return (int)Math.Ceiling((double)RecordsCount / PageSize); } }

        public int RecordsCount { get; private set; }

        #endregion
    }

    public static class PaginationHepler
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex)
        {
            return new PagedList<T>(source, pageIndex, 10);
        }
    }
}
