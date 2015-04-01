using System;
using System.Web.Mvc;
using System.Web.Routing;
using WebPrint.Model.Helper;

namespace WebPrint.Web.Mvc.Pager
{
    public static class PagerHelper
    {
        #region Html Pager

        private static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex,
            string actionName, string controllerName,
            PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            var totalPageCount = (int) Math.Ceiling(totalItemCount/(double) pageSize);
            var builder = new PagerBuilder
                (
                helper,
                actionName,
                controllerName,
                totalItemCount,
                totalPageCount,
                pageIndex,
                pageSize,
                pagerOptions,
                routeName,
                new RouteValueDictionary(routeValues),
                new RouteValueDictionary(htmlAttributes)
                );
            return builder.RenderPager();
        }

        //private static MvcHtmlString Pager(HtmlHelper helper, PagerOptions pagerOptions,
        //    IDictionary<string, object> htmlAttributes)
        //{
        //    return new PagerBuilder(helper, pagerOptions, htmlAttributes).RenderPager();
        //}

        //public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList)
        //{
        //    if (pagedList == null)
        //        return Pager(helper, (PagerOptions) null, null);
        //    return Pager(helper, pagedList.RecordsCount, pagedList.PageSize, pagedList.PageIndex, null, null,
        //        null, null, null, null);
        //}

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
        {
            //if (pagedList == null)
            //    return Pager(helper, pagerOptions, null);
            return Pager(helper, pagedList.RecordsCount, pagedList.PageSize, pagedList.PageIndex, null, null,
                pagerOptions, null, null, null);
        }

        #endregion
    }
}