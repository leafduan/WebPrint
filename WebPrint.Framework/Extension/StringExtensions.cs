using System;
using System.Collections.Generic;
using System.Linq;

namespace WebPrint.Framework
{
    public static class StringExtension
    {
        /// <summary>
        /// 截取适当长度字符串 从 0 开始
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="length">最大长度</param>
        /// <param name="padding">填充（末尾 如"…"）</param>
        /// <returns>处理后的字符串</returns>
        public static string Cut(this string source, int length, string padding)
        {
            if (source.Length == 0) return string.Empty;

            if (source.Length > length)
            {
                source = source.Substring(0, length) + padding;
            }

            return source;
        }


        /// <summary>
        /// 字符串拆分成链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">被拆分的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>链表</returns>
        public static IEnumerable<T> Split<T>(this string target, params char[] separator)
        {
            if (target.Length == 0) return new List<T>();
            /*
            var array = target.Split(separator);
            if (array.Length > 0 && !string.IsNullOrEmpty(array[0]))
            {
                List<T> list = new List<T>();
                foreach (var l in array)
                {
                    list.Add((T)Convert.ChangeType(l, typeof(T)));
                }
                return list;
            }

            return new List<T>();
            */
            return target
                .Split(separator)
                .Select(value => string.IsNullOrEmpty(value) ? default(T) : (T) Convert.ChangeType(value, typeof (T)));
        }

        /// <summary>
        /// 拆分字符串并按模板拼接
        /// </summary>
        /// <param name="target"></param>
        /// <param name="formatTemplate">模板</param>
        /// <param name="separator">拆分分隔符</param>
        /// <returns></returns>
        public static string Join(this string target, string formatTemplate, params char[] separator)
        {
            return Join(target, formatTemplate, string.Empty, separator);
        }

        /// <summary>
        /// 拆分字符串并按模板拼接
        /// </summary>
        /// <param name="target"></param>
        /// <param name="formatTemplate">模板</param>
        /// <param name="joinSeparator">拼接分隔符</param>
        /// <param name="splitSeparator">拆分分隔符</param>
        /// <returns>拼接后字符串</returns>
        public static string Join(this string target, string formatTemplate, string joinSeparator, params char[] splitSeparator)
        {
            if (target.Length == 0) return string.Empty;

            var array = target
                .Split(splitSeparator)
                .Select(value => string.Format(formatTemplate, value))
                .ToArray();

            return string.Join(joinSeparator, array);
        }

        /// <summary>
        /// 去掉字符串前后空格并判断是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            /*
           if (value != null)
           {
               for (int i = 0; i < value.Length; i++)
               {
                   if (!char.IsWhiteSpace(value[i]))
                   {
                       return false;
                   }
               }
                 
               return value.All(char.IsWhiteSpace);
           }
           return true;
           * */

            return value == null || value.All(char.IsWhiteSpace);
            //.Net 4.0
            //return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 如果为Null则返回Null 否则返回s.Trim
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NullableTrim(this string value)
        {
            return value == null ? null : value.Trim();
        }

        #region EqualTo
        /// <summary>
        /// string.Compare(me, other) == 0;
        /// </summary>
        /// <param name="me"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool EqualTo(this string me, string other)
        {
            return string.CompareOrdinal(me, other) == 0;
        }

        /// <summary>
        /// string.Compare(me, other, ignoreCase) == 0;
        /// </summary>
        /// <param name="me"></param>
        /// <param name="other"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool EqualTo(this string me, string other, bool ignoreCase)
        {
            return string.Compare(me, other, ignoreCase) == 0;
        }
        #endregion

        #region Format

        /// <summary>
        ///  string.Format, if format is null will return null
        /// </summary>
        public static string Formatting(this string format, object arg0)
        {
            return format.IsNullOrEmpty() ? format : string.Format(format, arg0);
        }

        /// <summary>
        ///  string.Format, if format is null will return null
        /// </summary>
        public static string Formatting(this string format, object arg0, object arg1)
        {
            return format.IsNullOrEmpty() ? format : string.Format(format, arg0, arg1);
        }

        /// <summary>
        ///  string.Format, if format is null will return null
        /// </summary>
        public static string Formatting(this string format, object arg0, object arg1, object arg2)
        {
            return format.IsNullOrEmpty() ? format : string.Format(format, arg0, arg1, arg2);
        }

        /// <summary>
        ///  string.Format, if format is null will return null
        /// </summary>
        public static string Formatting(this string format, params object[] args)
        {
            return format.IsNullOrEmpty() ? format : string.Format(format, args);
        }

        #endregion

        #region TryParse
        /// <summary>
        /// Enum.TryParse
        /// </summary>
        public static T AsEnum<T>(this string value) where T : struct
        {
            return value.AsEnum(default(T));
        }

        public static T AsEnum<T>(this string value, T defaultValue) where T : struct
        {
            T t;
            if (!Enum.TryParse(value, true, out t))
            {
                return defaultValue;
            }
            return t;
        }

        public static bool AsBool(this string value)
        {
            return value.AsBool(false);
        }

        public static bool AsBool(this string value, bool defaultValue)
        {
            bool flag;
            if (!bool.TryParse(value, out flag))
            {
                return defaultValue;
            }
            return flag;
        }

        public static DateTime AsDateTime(this string value)
        {
            return value.AsDateTime(new DateTime());
        }

        public static DateTime AsDateTime(this string value, DateTime defaultValue)
        {
            DateTime time;
            if (!DateTime.TryParse(value, out time))
            {
                return defaultValue;
            }
            return time;
        }

        public static decimal AsDecimal(this string value)
        {
            return value.AsDecimal(0m);
        }

        public static decimal AsDecimal(this string value, decimal defaultValue)
        {
            decimal num;
            if (!decimal.TryParse(value, out num))
            {
                return defaultValue;
            }
            return num;
        }

        public static double AsDouble(this string value)
        {
            return value.AsDouble(0d);
        }

        public static double AsDouble(this string value, double defaultValue)
        {
            double num;
            if (!double.TryParse(value, out num))
            {
                return defaultValue;
            }
            return num;
        }

        public static float AsFloat(this string value)
        {
            return value.AsFloat(0f);
        }

        public static float AsFloat(this string value, float defaultValue)
        {
            float num;
            if (!float.TryParse(value, out num))
            {
                return defaultValue;
            }
            return num;
        }

        public static long AsLong(this string value)
        {
            return value.AsLong(0L);
        }

        public static long AsLong(this string value, long defaultValue)
        {
            long num;
            if (!long.TryParse(value, out num))
            {
                return defaultValue;
            }
            return num;
        }

        public static int AsInt(this string value)
        {
            return value.AsInt(0);
        }

        public static int AsInt(this string value, int defaultValue)
        {
            int num;
            if (!int.TryParse(value, out num))
            {
                return defaultValue;
            }
            return num;
        }

        public static short AsShort(this string value)
        {
            return value.AsShort(0);
        }

        public static short AsShort(this string value, short defaultValue)
        {
            short num;
            if (!short.TryParse(value, out num))
            {
                return defaultValue;
            }
            return num;
        }
        #endregion
    }
}
