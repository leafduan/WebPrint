/*
 ASP.NET MvcPager 分页组件
 Copyright@杨涛\Webdiyer (http://www.webdiyer.com)
 Source code released under Ms-PL license
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebPrint.Web.Mvc.Pager
{
    internal class PagerBuilder
    {
        private readonly HtmlHelper html;
        private readonly string actionName;
        private readonly string controllerName;
        private readonly int totalPageCount = 1;
        private readonly int totalRecordCount = 1;
        private readonly int pageIndex;
        private readonly int pageSize;
        private readonly PagerOptions pagerOptions;
        private readonly RouteValueDictionary routeValues;
        private readonly string routeName;
        private readonly int startPageIndex = 1;
        private readonly int endPageIndex = 1;
        private IDictionary<string, object> htmlAttributes;

        private const string CopyrightText =
            "\r\n<!--MvcPager v2.0 for ASP.NET MVC 4.0+ © 2009-2013 Webdiyer (http://www.webdiyer.com)-->\r\n";

        /// <summary>
        /// 适用于PagedList为null时
        /// </summary>
        //internal PagerBuilder(HtmlHelper html,PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        //{
        //    if (pagerOptions == null)
        //        pagerOptions = new PagerOptions();
        //    this.html = html;
        //    this.pagerOptions = pagerOptions;
        //    this.htmlAttributes = htmlAttributes;
        //}
        internal PagerBuilder(HtmlHelper html, string actionName, string controllerName, int totalRecordCount,
            int totalPageCount, int pageIndex, int pageSize, PagerOptions pagerOptions, string routeName,
            RouteValueDictionary routeValues,
            IDictionary<string, object> htmlAttributes)
        {
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            this.html = html;
            this.actionName = actionName;
            this.controllerName = controllerName;
            this.totalRecordCount = totalRecordCount;
            if (pagerOptions.MaxPageIndex == 0 || pagerOptions.MaxPageIndex > totalPageCount)
                this.totalPageCount = totalPageCount;
            else
                this.totalPageCount = pagerOptions.MaxPageIndex;
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.pagerOptions = pagerOptions;
            this.routeName = routeName;
            this.routeValues = routeValues;
            this.htmlAttributes = htmlAttributes;
            // start page index
            startPageIndex = pageIndex - (pagerOptions.NumericPagerItemCount/2);
            if (startPageIndex + pagerOptions.NumericPagerItemCount > this.totalPageCount)
                startPageIndex = this.totalPageCount + 1 - pagerOptions.NumericPagerItemCount;
            if (startPageIndex < 1)
                startPageIndex = 1;
            // end page index
            endPageIndex = startPageIndex + this.pagerOptions.NumericPagerItemCount - 1;
            if (endPageIndex > this.totalPageCount)
                endPageIndex = this.totalPageCount;
        }

        private void AddPrevious(ICollection<PagerItem> results)
        {
            var item = new PagerItem(pagerOptions.PrevPageText, pageIndex - 1, pageIndex == 1, PagerItemType.PrevPage);
            if (!item.Disabled || (item.Disabled && pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        private void AddFirst(ICollection<PagerItem> results)
        {
            var item = new PagerItem(pagerOptions.FirstPageText, 1, pageIndex == 1, PagerItemType.FirstPage);
            //只有导航按钮未被禁用，或导航按钮被禁用但ShowDisabledPagerItems=true时才添加
            if (!item.Disabled || (item.Disabled && pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        private void AddMoreBefore(ICollection<PagerItem> results)
        {
            if (startPageIndex > 1 && pagerOptions.ShowMorePagerItems)
            {
                var index = startPageIndex - 1;
                if (index < 1) index = 1;
                var item = new PagerItem(pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddPageNumbers(ICollection<PagerItem> results)
        {
            for (var pageIndex = startPageIndex; pageIndex <= endPageIndex; pageIndex++)
            {
                var text = pageIndex.ToString(CultureInfo.InvariantCulture);
                //if (pageIndex == this.pageIndex && !string.IsNullOrEmpty(pagerOptions.CurrentPageNumberFormatString))
                //    text = String.Format(pagerOptions.CurrentPageNumberFormatString, text);
                //else if (!string.IsNullOrEmpty(pagerOptions.PageNumberFormatString))
                //    text = String.Format(pagerOptions.PageNumberFormatString, text);
                var item = new PagerItem(text, pageIndex, false, PagerItemType.NumericPage);
                results.Add(item);
            }
        }

        private void AddMoreAfter(ICollection<PagerItem> results)
        {
            if (endPageIndex < totalPageCount)
            {
                var index = startPageIndex + pagerOptions.NumericPagerItemCount;
                if (index > totalPageCount)
                {
                    index = totalPageCount;
                }
                var item = new PagerItem(pagerOptions.MorePageText, index, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddNext(ICollection<PagerItem> results)
        {
            var item = new PagerItem(pagerOptions.NextPageText, pageIndex + 1, pageIndex >= totalPageCount,
                PagerItemType.NextPage);
            if (!item.Disabled || (item.Disabled && pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        private void AddLast(ICollection<PagerItem> results)
        {
            var item = new PagerItem(pagerOptions.LastPageText, totalPageCount, pageIndex >= totalPageCount,
                PagerItemType.LastPage);
            if (!item.Disabled || (item.Disabled && pagerOptions.ShowDisabledPagerItems))
                results.Add(item);
        }

        /// <summary>
        /// 根据页索引生成分页导航Url
        /// </summary>
        /// <param name="pageIndex">要生成导航链接的页索引</param>
        /// <returns>分页导航链接Url</returns>
        private string GenerateUrl(int pageIndex)
        {
            ViewContext viewContext = html.ViewContext;
            //若要生成url的页索引小于1或大于总页数或等于当前页索引时，无需生成分页导航链接
            if (pageIndex > totalPageCount || pageIndex == this.pageIndex)
                return null;
            var routeValues1 = new RouteValueDictionary(viewContext.RouteData.Values);
            AddQueryStringToRouteValues(routeValues1, viewContext);
            if (this.routeValues != null && this.routeValues.Count > 0)
            {
                foreach (var de in this.routeValues)
                {
                    if (!routeValues1.ContainsKey(de.Key))
                    {
                        routeValues1.Add(de.Key, de.Value);
                    }
                    else
                    {
                        routeValues1[de.Key] = de.Value; //手动添加的RouteValues具有高优先级
                    }
                }
            }
            var pageValue = viewContext.RouteData.Values[pagerOptions.PageIndexParameterName];
            string routeName = this.routeName;
            // 设置Route Value中的分页导航Url参数值，pageIndex为0时生成适用于脚本的导航链接
            if (pageIndex == 0)
                routeValues1[pagerOptions.PageIndexParameterName] = "__" + pagerOptions.PageIndexParameterName + "__";
            else
            {
                if (pageIndex == 1)
                {
                    if (!string.IsNullOrWhiteSpace(pagerOptions.FirstPageRouteName))
                        //如果显式指定了FirstPageRouteName，则使用此Route
                    {
                        routeName = pagerOptions.FirstPageRouteName;
                        routeValues1.Remove(pagerOptions.PageIndexParameterName); //去除页索引参数
                        viewContext.RouteData.Values.Remove(pagerOptions.PageIndexParameterName);
                    }
                    else
                    {
                        var curRoute = viewContext.RouteData.Route as Route;
                        //判断当前Route是否为Route类型，如果是，则判断该Route中页索引参数默认值是否为UrlParameter.Optional，或页索引参数是否存在于该Route Url的参数列表中，如果参数默认值为UrlParameter.Optional或分页参数名不存在于Route Url参数中，则从当前的RouteValue列表中去除该参数
                        if (curRoute != null &&
                            (curRoute.Defaults[pagerOptions.PageIndexParameterName] == UrlParameter.Optional ||
                             !curRoute.Url.Contains("{" + pagerOptions.PageIndexParameterName + "}")))
                        {
                            routeValues1.Remove(pagerOptions.PageIndexParameterName); //去除页索引参数
                            viewContext.RouteData.Values.Remove(pagerOptions.PageIndexParameterName);
                        }
                        else
                        {
                            routeValues1[pagerOptions.PageIndexParameterName] = pageIndex;
                        }
                    }
                }
                else
                {
                    routeValues1[pagerOptions.PageIndexParameterName] = pageIndex;
                }
            }
            var routes = html.RouteCollection;
            string url;
            if (!string.IsNullOrEmpty(routeName))
                url = UrlHelper.GenerateUrl(routeName, actionName, controllerName, routeValues1, routes,
                    viewContext.RequestContext, false);
            else
                url = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues1, routes,
                    viewContext.RequestContext, false);
            if (pageValue != null)
                viewContext.RouteData.Values[pagerOptions.PageIndexParameterName] = pageValue;
            return url;
        }

        /// <summary>
        /// 生成最终的分页Html代码
        /// </summary>
        /// <returns></returns>
        internal MvcHtmlString RenderPager()
        {
            //return null if total page count less than or equal to 1
            if (totalPageCount <= 1 && pagerOptions.AutoHide)
                return MvcHtmlString.Create(CopyrightText);
            //Display error message if pageIndex out of range
            if ((pageIndex > totalPageCount && totalPageCount > 0) || pageIndex < 1)
            {
                return
                    MvcHtmlString.Create(string.Format("{0}<div style=\"color:red;font-weight:bold\">{1}</div>{0}",
                        CopyrightText, pagerOptions.PageIndexOutOfRangeErrorMessage));
            }

            var pagerItems = new List<PagerItem>();
            //First page
            if (pagerOptions.ShowFirstLast)
                AddFirst(pagerItems);
            // Prev page
            if (pagerOptions.ShowPrevNext)
                AddPrevious(pagerItems);
            if (pagerOptions.ShowNumericPagerItems)
            {
                if (pagerOptions.AlwaysShowFirstLastPageNumber && startPageIndex > 1)
                    pagerItems.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));
                // more page before numeric page buttons
                if (pagerOptions.ShowMorePagerItems &&
                    ((!pagerOptions.AlwaysShowFirstLastPageNumber && startPageIndex > 1) ||
                     (pagerOptions.AlwaysShowFirstLastPageNumber && startPageIndex > 2)))
                    AddMoreBefore(pagerItems);
                // numeric page
                AddPageNumbers(pagerItems);
                // more page after numeric page buttons
                if (pagerOptions.ShowMorePagerItems &&
                    ((!pagerOptions.AlwaysShowFirstLastPageNumber && endPageIndex < totalPageCount) ||
                     (pagerOptions.AlwaysShowFirstLastPageNumber && totalPageCount > endPageIndex + 1)))
                    AddMoreAfter(pagerItems);
                if (pagerOptions.AlwaysShowFirstLastPageNumber && endPageIndex < totalPageCount)
                    pagerItems.Add(new PagerItem(totalPageCount.ToString(CultureInfo.InvariantCulture), totalPageCount,
                        false,
                        PagerItemType.NumericPage));
            }
            // Next page
            if (pagerOptions.ShowPrevNext)
                AddNext(pagerItems);
            //Last page
            if (pagerOptions.ShowFirstLast)
                AddLast(pagerItems);
            var sb = new StringBuilder();

            foreach (PagerItem item in pagerItems)
            {
                sb.Append(GeneratePagerElement(item));
            }

            var tb = new TagBuilder(pagerOptions.ContainerTagName);
            if (!string.IsNullOrEmpty(pagerOptions.Id))
                tb.GenerateId(pagerOptions.Id);
            if (!string.IsNullOrEmpty(pagerOptions.CssClass))
                tb.AddCssClass(pagerOptions.CssClass);
            tb.MergeAttributes(htmlAttributes, true);
            sb.Length -= pagerOptions.PagerItemsSeperator.Length;

            var startRecords = ((pageIndex - 1)*pageSize) + 1;
            var endRecords = pageIndex*pageSize;
            if (endRecords > totalRecordCount)
                endRecords = totalRecordCount;

            var recordsCountText =
                string.Format("<span class=\"summary\">{0}&nbsp;-&nbsp;{1},&nbsp;{2}&nbsp;records</span>",
                    startRecords, endRecords, totalRecordCount);
            tb.InnerHtml = sb.ToString() + recordsCountText;

            return
                MvcHtmlString.Create(CopyrightText + /*pagerScript +*/ tb.ToString(TagRenderMode.Normal) + CopyrightText);
        }

        private MvcHtmlString GeneratePagerElement(PagerItem item)
        {
            //pager item link
            string url = GenerateUrl(item.PageIndex);
            if (item.Disabled) //first,last,next or previous page
                return CreateWrappedPagerElement(item,
                    String.Format("<a class=\"disabled\" disabled=\"disabled\">{0}</a>", item.Text));
            return CreateWrappedPagerElement(item,
                string.IsNullOrEmpty(url)
                    ? HttpUtility.HtmlEncode(item.Text)
                    : String.Format("<a href=\"{0}\">{1}</a>", url, item.Text));
        }

        private MvcHtmlString CreateWrappedPagerElement(PagerItem item, string el)
        {
            string navStr = el;
            switch (item.Type)
            {
                case PagerItemType.FirstPage:
                case PagerItemType.LastPage:
                case PagerItemType.NextPage:
                case PagerItemType.PrevPage:
                    break;
                case PagerItemType.MorePage:
                    break;
                case PagerItemType.NumericPage:
                    if (item.PageIndex == pageIndex)
                    {
                        navStr = string.Format("<a href=\"javascript:void(0);\" class=\"current\">{0}</a>", el);
                    }
                    break;
            }
            return MvcHtmlString.Create(navStr + pagerOptions.PagerItemsSeperator);
        }

        private void AddQueryStringToRouteValues(RouteValueDictionary routeValues, ViewContext viewContext)
        {
            if (routeValues == null)
                routeValues = new RouteValueDictionary();
            var rq = viewContext.HttpContext.Request.QueryString;
            if (rq != null && rq.Count > 0)
            {
                var invalidParams = new[]
                {"x-requested-with", "xmlhttprequest", pagerOptions.PageIndexParameterName.ToLower()};
                foreach (string key in rq.Keys)
                {
                    // 添加url参数到路由中
                    if (!string.IsNullOrEmpty(key) && Array.IndexOf(invalidParams, key.ToLower()) < 0)
                    {
                        var kv = rq[key];
                        routeValues[key] = kv;
                    }
                }
            }
        }
    }
}