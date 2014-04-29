using System.Web;
using System.Web.Security;
using WebPrint.Model;

namespace WebPrint.Web.Core
{
    public static class FormsAuth
    {
        public static bool IsAuthenticated
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated; }
        }

        public static void SignIn(User user, bool isPersistent)
        {
            // 如果登录成功 则做此设置 表明验证通过
            // 输入的 username 用来作为登录验证票据 tick 的一部分
            // persistence cookie 如果为false 则为回话cookie 浏览器关闭就失效，如果为 true 则有效期为配置中的 timeout
            FormsAuthentication.SetAuthCookie(user.UserName, isPersistent); // 此方法设置要安全可靠

            CookieHelper.SetAuthCookieTicket(user);
            CookieHelper.SetUserCookie(user);
        }

        public static void SignOut()
        {
            // 退出登录 做此标记 表明用户验证已到期
            FormsAuthentication.SignOut();
            CookieHelper.ClearUserCookie();
        }
    }
}
