using System.Collections.Generic;
using System.Linq;
using WebPrint.Framework;
using WebPrint.Model;
using WebPrint.Service;

namespace WebPrint.Web.Core
{
    //当用户组和权限不多时(安全性暂不考虑) 放到cookie中还行 当多的时候 应该实时数据中匹配
    public class UserGroupPermissionManager
    {
        private Dictionary<int, string> groups;
        private Dictionary<int, string> permissions;
        private User user;

        private IService<User> UserService { get; set; }

        private System.Web.HttpRequest Request
        {
            get { return System.Web.HttpContext.Current.Request; }
        }

        public static bool IsLogin()
        {
            var resquest = System.Web.HttpContext.Current.Request;
            var cookie = resquest.Cookies["user"];
            
            return cookie != null;
        }

        public Dictionary<int, string> Groups
        {
            get { return groups ?? (groups = Parser(Request.Cookies["user"]["groups"])); }
        }

        public bool IsMemberOfGroup(string groupName)
        {
            return Groups.ContainsValue(groupName);
        }

        public bool IsMemberOfGroup(int groupId)
        {
            return Groups.ContainsKey(groupId);
        }

        public Dictionary<int, string> Permissions
        {
            get { return permissions ?? (permissions = Parser(Request.Cookies["user"]["permission"])); }
        }

        private Dictionary<int, string> Parser(string cookieValue)
        {
            if (string.IsNullOrEmpty(cookieValue))
                return new Dictionary<int, string>();

            return cookieValue
                .Split('|')
                .Where(g => !string.IsNullOrEmpty(g))
                .Select(p => p.Split(','))
                .Where(a => a.Length >= 2)
                .ToDictionary(item => int.Parse(item[0]), item => item[1]);
        }

        public bool HasPermission(string permissionName)
        {
            return Permissions.ContainsValue(permissionName);
        }

        public bool HasPermission(int permissionId)
        {
            return Permissions.ContainsKey(permissionId);
        }

        public User User
        {
            get { return user ?? (user = UserService.Get(u => u.Id == Request.Cookies["user"]["userid"].AsInt())); }
        }
    }

    //为了更好的操作 当权限很多时 应该将页面与组的关系对应起来
    public enum Permission : int
    {
        CheckOrder = 1,
        CreateOrder = 2,
        EditOrder = 3
    }

    public enum Group : int
    {
        Administrators = 1
    }
}
