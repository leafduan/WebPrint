using System;
using System.Collections.Generic;
using System.Text;

namespace WebPrint.Framework
{
    public static class ListExtensions
    {
        /// <summary>
        /// 将链表转换成以 separator 分隔的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">列表</param>
        /// <param name="separator">分隔符</param>
        /// <returns>拼接字符串</returns>
        public static string Join<T>(this IEnumerable<T> target, string separator)
        {
            var result = new StringBuilder();

            foreach (var item in target)
            {
                if (result.Length > 0) result.Append(separator);
                result.Append(item);
            }

            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="separator"></param>
        /// <param name="action">处理拼接中的链表元素</param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> target, string separator, Func<T, string> action)
        {
            var result = new StringBuilder();

            foreach (var item in target)
            {
                if (result.Length > 0) result.Append(separator);
                result.Append(action(item));
            }

            return result.ToString();
        }

        /// <summary>
        /// ForEach method for Generic IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (var item in list)
            {
                action(item);
            }
        }
    }
}
