using System;
using WebPrint.Model;
using WebPrint.Model.Services;

namespace WebPrint.Web.Core
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
        }

        protected UserGroupPermissionManager UserGroupPermission { get; private set; }
        //public string TypeCss { get; set; }
        protected override void OnInit(EventArgs e)
        {
            if (Request.Cookies["user"] == null)
                Response.Redirect("/login.aspx?frame=" + System.Web.HttpUtility.UrlEncode(Request.Url.ToString()));

            this.UserGroupPermission = new UserGroupPermissionManager();
        }

        private bool IsPage(string pageUrl,string checkUrl)
        {
            var index = pageUrl.IndexOf('?');
            if(index != -1)
            {
                pageUrl = pageUrl.Substring(0, pageUrl.Length - index);
            }

            return pageUrl.ToLower().Contains(checkUrl.ToLower());
        }
    }
}
