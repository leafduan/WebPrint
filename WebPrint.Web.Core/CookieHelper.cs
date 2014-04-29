using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebPrint.Model;
using WebPrint.Framework;

namespace WebPrint.Web.Core
{
    public static class CookieHelper
    {
        private const string UserCookieName = "user";

        public static void SetAuthCookieTicket(User user)
        {
            var response = HttpContext.Current.Response;
            var cookie = response.Cookies.Get(FormsAuthentication.FormsCookieName);
            if (cookie == null) return;

            var permissions = new List<string>();
            user.Groups
                .Select(g => g.Permissions.Select(p => p.Name))
                .ForEach(p => permissions = permissions.Union(p).ToList());

            var oldTicket = FormsAuthentication.Decrypt(cookie.Value);
            if (oldTicket == null) return;

            var newTicket = new FormsAuthenticationTicket(oldTicket.Version,
                                                          oldTicket.Name,
                                                          oldTicket.IssueDate,
                                                          oldTicket.Expiration,
                                                          oldTicket.IsPersistent,
                                                          permissions.Join(",")
                );

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
        }

        public static void SetUserCookie(User user)
        {
            var cookie = new HttpCookie(UserCookieName)
                {
                    Expires = DateTime.UtcNow.Add(FormsAuthentication.Timeout),
                    Secure = FormsAuthentication.RequireSSL,
                    HttpOnly = true
                };

            cookie.Values.Add("uid", user.Id.ToString(CultureInfo.InvariantCulture));

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void ClearUserCookie()
        {
            var response = HttpContext.Current.Response;
            var cookie = new HttpCookie(UserCookieName)
                {
                    HttpOnly = true,
                    Expires = new DateTime(0x7cf, 10, 12) //0x7cf 1999
                };

            response.Cookies.Remove(UserCookieName);
            response.Cookies.Add(cookie);
        }
    }
}
