using System;
using System.Reflection;

namespace WebPrint.Framework
{
    public static class ObjectExtensions
    {
        /*
        /// <summary>
        /// MemberwiseClone
        /// </summary>
        public static T Clone<T>(this T obj) where T : class
        {
            var method = typeof (object).GetMethod("MemberwiseClone",
                                                   BindingFlags.IgnoreCase | BindingFlags.Instance |
                                                   BindingFlags.NonPublic);

            return method == null
                       ? default(T)
                       : (T) method.Invoke(obj, new object[] {});
        }
         * */

        #region DBNull
        /// <summary>
        /// me == DBNull.Value
        /// </summary>
        public static bool IsDbNull(this object me)
        {
            return me == DBNull.Value;
        }

        /// <summary>
        /// if me is DBNull or null return null, other return me.ToString()
        /// </summary>
        public static string NullableToString(this object me)
        {
            return (me == DBNull.Value || me == null)
                ? null
                : me.ToString();
        }
        #endregion

        public static object GetPropertyValue(this object value, string propertyName)
        {
            if (value == null)
                return null;

            var property = value
                .GetType()
                .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);

            if (property == null)
                return null;

            return property.GetValue(value, null);
        }
    }
}
