using System;
using System.Reflection;
using NHibernate;
using NHibernate.Proxy;
using WebPrint.Model;

namespace WebPrint.Data
{
    /// <summary>
    /// json.net serialize nhibernate proxy issue
    /// Cannot serialize a Session while connected
    /// </summary>
    public static class NHibernateJsonHelper
    {
        private static readonly MemberInfo[] NHibernateProxyInterfaceMembers = typeof (INHibernateProxy).GetMembers();

        /// <summary>
        /// IsMemberPartOfNHibernateProxyInterface
        /// http://stackoverflow.com/questions/286721/json-net-and-nhibernate-lazy-loading-of-collections/5926718#5926718
        /// </summary>
        public static bool IsAssignableFromNHibernateProxy(Type objectType)
        {
            return (typeof (INHibernateProxy).IsAssignableFrom(objectType));
        }

        /// <summary>
        /// Control serialization NHibernate collections by using JsonProperty.ShouldSerialize.
        /// Serialization should only be attempted on collections that are initialized.
        /// https://github.com/IdeaBlade/Breeze/blob/master/Breeze.ContextProvider.NH/Json/NHibernateContractResolver.cs
        /// </summary>
        public static bool IsInitialized(object proxy)
        {
            return NHibernateUtil.IsInitialized(proxy);
        }

        public static bool IsMemberMarkedWithIgnoreAttribute(MemberInfo memberInfo, Type objectType)
        {
            if (objectType.BaseType == null) return false;

            var infos = typeof (INHibernateProxy).IsAssignableFrom(objectType)
                ? objectType.BaseType.GetMember(memberInfo.Name)
                : objectType.GetMember(memberInfo.Name);

            return infos[0].GetCustomAttributes(typeof (JsonIgnoreAttribute), true).Length > 0;
        }

        public static bool IsMemberInheritedFromProxySuperclass(MemberInfo memberInfo)
        {
            return memberInfo.DeclaringType != null &&
                   memberInfo.DeclaringType.Assembly == typeof (INHibernateProxy).Assembly;
        }

        public static bool IsMemberPartOfNHibernateProxyInterface(MemberInfo memberInfo)
        {
            return Array.Exists(NHibernateProxyInterfaceMembers, mi => memberInfo.Name == mi.Name);
        }

        public static bool IsMemberDynamicProxyMixin(MemberInfo memberInfo)
        {
            return memberInfo.Name == "__interceptors";
        }
    }
}
