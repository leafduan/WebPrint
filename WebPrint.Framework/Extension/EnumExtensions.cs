using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WebPrint.Framework
{
    public static class EnumExtensions
    {
        private static readonly object mutex = new object();

        private static readonly Dictionary<Type, Dictionary<object, string>> cache =
            new Dictionary<Type, Dictionary<object, string>>();

        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            string desc;
            Dictionary<object, string> typeCache;

            if (cache.TryGetValue(type, out typeCache))
            {
                if (typeCache.TryGetValue(value, out desc))
                {
                    return desc;
                }
            }

            lock (mutex)
            {
                if (!cache.ContainsKey(type))
                {
                    cache[type] = new Dictionary<object, string>();
                }

                var attribute = (DescriptionAttribute) type
                                                           .GetField(value.ToString())
                                                           .GetCustomAttributes(typeof (DescriptionAttribute), false)
                                                           .FirstOrDefault();
                desc = attribute == null ? value.ToString() : attribute.Description;
                cache[type][value] = desc;

                return desc;
            }
        }
    }
}
