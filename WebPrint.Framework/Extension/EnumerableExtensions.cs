using System;
using System.Collections.Generic;
using System.Linq;

namespace WebPrint.Framework
{
    public static class ListExtensions
    {
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="separator"></param>
        /// <param name="action">处理拼接中的链表元素</param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> target, string separator = ",", Func<T, string> action = null)
        {
            /*
            var result = new StringBuilder();
            foreach (var item in target)
            {
                if (result.Length > 0) result.Append(separator);
                result.Append(action(item));
            }

            return result.ToString();
            */

            return action == null
                ? string.Join(separator, target)
                : string.Join(separator, target.Select(action));
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
