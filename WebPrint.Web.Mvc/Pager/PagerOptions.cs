/*
 ASP.NET MvcPager 分页组件
 Copyright@杨涛\Webdiyer (http://www.webdiyer.com)
 Source code released under Ms-PL license
 */
namespace WebPrint.Web.Mvc.Pager
{
    public class PagerOptions
    {
        public PagerOptions()
        {
            AutoHide = true;
            PageIndexParameterName = "pageindex";
            NumericPagerItemCount = 10;
            AlwaysShowFirstLastPageNumber = false;
            ShowPrevNext = true;
            NextPageText = "&gt;";
            PrevPageText = "&lt;";
            ShowNumericPagerItems = true;
            ShowFirstLast = true;
            FirstPageText = "&lt;&lt;";
            LastPageText = "&gt;&gt";
            ShowMorePagerItems = true;
            MorePageText = "...";
            ShowDisabledPagerItems = true;
            PagerItemsSeperator = "  ";
            ContainerTagName = "div";
            InvalidPageIndexErrorMessage = "页索引无效";
            PageIndexOutOfRangeErrorMessage = "页索引超出范围";
            MaxPageIndex = 0;
            FirstPageRouteName = null;
        }
        /// <summary>
        /// 首页使用的路由名称（无页索引参数）
        /// </summary>
        public string FirstPageRouteName { get; set; }
        /// <summary>
        /// 当总页数只有一页时是否自动隐藏
        /// </summary>
        public bool AutoHide { get; set; }
        /// <summary>
        /// 页索引超出范围时显示的错误消息
        /// </summary>
        public string PageIndexOutOfRangeErrorMessage { get; set; }
        /// <summary>
        /// 页索引无效时显示的错误消息
        /// </summary>
        public string InvalidPageIndexErrorMessage { get; set; }
        /// <summary>
        /// url中页索引参数的名称
        /// </summary>
        public string PageIndexParameterName { get; set; }

        private string _containerTagName;
        /// <summary>
        /// 分页控件html容器标签名，默认为div
        /// </summary>
        public string ContainerTagName
        {
            get
            {
                return _containerTagName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new System.ArgumentException("ContainerTagName不能为null或空字符串", "ContainerTagName");
                _containerTagName = value;
            }
        }
        /// <summary>
        /// whether or not show first and last numeric page number
        /// </summary>
        public bool AlwaysShowFirstLastPageNumber { get; set; }
        /// <summary>
        /// 显示的最大数字页索引按钮数
        /// </summary>
        public int NumericPagerItemCount { get; set; }
        /// <summary>
        /// 是否显示上页和下页
        /// </summary>
        public bool ShowPrevNext { get; set; }
        /// <summary>
        /// 上一页文本
        /// </summary>
        public string PrevPageText { get; set; }
        /// <summary>
        /// 下一页文本
        /// </summary>
        public string NextPageText { get; set; }
        /// <summary>
        /// 是否显示数字页索引按钮及更多页按钮
        /// </summary>
        public bool ShowNumericPagerItems { get; set; }
        /// <summary>
        /// 是否显示第一页和最后一页
        /// </summary>
        public bool ShowFirstLast { get; set; }
        /// <summary>
        /// 第一页文本
        /// </summary>
        public string FirstPageText { get; set; }
        /// <summary>
        /// 最后一页文本
        /// </summary>
        public string LastPageText { get; set; }
        /// <summary>
        /// 是否显示更多页按钮
        /// </summary>
        public bool ShowMorePagerItems { get; set; }
        /// <summary>
        /// 更多页按钮文本
        /// </summary>
        public string MorePageText { get; set; }
        /// <summary>
        /// 包含分页控件的父容器标签的ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// CSS样式类
        /// </summary>
        public string CssClass { get; set; }
        /// <summary>
        /// whether or not show disabled navigation buttons
        /// </summary>
        public bool ShowDisabledPagerItems { get; set; }
        /// <summary>
        /// 分页元素之间的分隔符，默认为两个html空格（  ）
        /// </summary>
        public string PagerItemsSeperator { get; set; }
        /// <summary>
        /// 限制显示的最大页数，默认值为0，即根据总记录数算出的总页数
        /// </summary>
        public int MaxPageIndex { get; set; }
    }
}