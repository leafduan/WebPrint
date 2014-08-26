using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebPrint.Data;

namespace WebPrint.Web.Mvc.Helper
{
    /// <summary>
    ///json.net serialize nhibernate proxy issue
    /// Cannot serialize a Session while connected
    /// https://github.com/PeteGoo/NHibernate.QueryService/blob/master/NHibernateQueryService.WebApi/Serialization/NHibernateContractResolver.cs
    /// </summary>
    public class NHibernateContractResolver : DefaultContractResolver
    {
        public NHibernateContractResolver()
            : base(true)
        {
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (NHibernateJsonHelper.IsAssignableFromNHibernateProxy(objectType))
                return base.CreateContract(objectType.BaseType);

            return base.CreateContract(objectType);
        }

        /*
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var members = base.GetSerializableMembers(objectType);

            members.RemoveAll(memberInfo =>
                (NHibernateJsonHelper.IsMemberPartOfNHibernateProxyInterface(memberInfo)) ||
                (NHibernateJsonHelper.IsMemberDynamicProxyMixin(memberInfo)) ||
                (NHibernateJsonHelper.IsMemberMarkedWithIgnoreAttribute(memberInfo, objectType)) ||
                (NHibernateJsonHelper.IsMemberInheritedFromProxySuperclass(memberInfo, objectType)));

            return members;
        }
         * */

        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var members = base.GetSerializableMembers(objectType);

            members.RemoveAll(
                 memberInfo => (NHibernateJsonHelper.IsMemberMarkedWithIgnoreAttribute(memberInfo, objectType)));

            return members;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var pinfo = member as PropertyInfo;

            if ((typeof (string) != property.PropertyType) &&
                typeof (IEnumerable<>).IsAssignableFrom(property.PropertyType) && pinfo != null)
            {
                property.ShouldSerialize =
                    instance => NHibernateJsonHelper.IsInitialized(pinfo.GetValue(instance, null));
            }

            return property;
        }

        #region camel case property name

        protected override string ResolvePropertyName(string propertyName)
        {
            return ToCamelCase(propertyName);
        }

        private static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            if (!char.IsUpper(s[0]))
            {
                return s;
            }
            var builder = new StringBuilder();
            for (var i = 0; i < s.Length; i++)
            {
                var flag = (i + 1) < s.Length;
                if (((i == 0) || !flag) || char.IsUpper(s[i + 1]))
                {
                    var ch = char.ToLower(s[i], CultureInfo.InvariantCulture);
                    builder.Append(ch);
                }
                else
                {
                    builder.Append(s.Substring(i));
                    break;
                }
            }
            return builder.ToString();
        }

        #endregion
    }
}
