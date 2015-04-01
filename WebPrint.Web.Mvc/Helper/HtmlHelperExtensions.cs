using System.Web.Mvc;
using WebPrint.Framework;
using WebPrint.Model.Helper;
using WebPrint.Web.Mvc.Pager;

namespace WebPrint.Web.Mvc.Helper
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Paginaton(this HtmlHelper helper, IPagedList pagedList)
        {
            var pagerOptions = new PagerOptions
            {
                PageIndexParameterName = "page",
                CssClass = "pagination",
                ShowFirstLast = false,
                AlwaysShowFirstLastPageNumber = true,
                ShowMorePagerItems = true,
                NumericPagerItemCount = 5,
                PagerItemsSeperator = string.Empty,
            };

            return helper.Pager(pagedList, pagerOptions);
        }

        public static MvcHtmlString RegisterStartupScript(this HtmlHelper helper, string script = null)
        {
            if (script.IsNullOrEmpty())
            {
                script = helper.ViewBag.Script;
            }

            if (script.IsNullOrEmpty())
            {
                return new MvcHtmlString(string.Empty);
            }

            return new MvcHtmlString("<script>{0};</script>".Formatting(script));
        }

        public static MvcHtmlString ShowErrorMsg(this HtmlHelper helper, string msg = null)
        {
            if (msg.IsNullOrEmpty())
            {
                msg = helper.ViewBag.ErrorMsg;
            }

            if (msg.IsNullOrEmpty())
            {
                return new MvcHtmlString(string.Empty);
            }

            return
                new MvcHtmlString(
                    "<script>$(function(){showError('" + JavaScriptSupport.EscapeSpecialCharacters(msg) +
                    "')});</script>");

        }

        public static MvcHtmlString ShowMsg(this HtmlHelper helper, string msg = null)
        {
            if (msg.IsNullOrEmpty())
            {
                msg = helper.ViewBag.Msg;
            }

            if (msg.IsNullOrEmpty())
            {
                return new MvcHtmlString(string.Empty);
            }

            return
                new MvcHtmlString(
                    "<script>showMsg('{0}');</script>".Formatting(JavaScriptSupport.EscapeSpecialCharacters(msg)));
        }
    }
}