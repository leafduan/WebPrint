using System;
using System.Collections.Generic;

namespace WebPrint.Framework
{
    //Adapter from: http://blog.zhaojie.me/2009/01/dynamicpropertyaccessor-and-fasteval.html 
    public class DynamicPropertyAccessorCache
    {
        private object mutex = new object();
        private Dictionary<Type, Dictionary<string, DynamicPropertyAccessor>> cache =
            new Dictionary<Type, Dictionary<string, DynamicPropertyAccessor>>();

        public DynamicPropertyAccessor GetAccessor(Type type, string propertyName)
        {
            DynamicPropertyAccessor accessor;
            Dictionary<string, DynamicPropertyAccessor> typeCache;

            if (this.cache.TryGetValue(type, out typeCache))
            {
                if (typeCache.TryGetValue(propertyName, out accessor))
                {
                    return accessor;
                }
            }

            lock (mutex)
            {
                if (!this.cache.ContainsKey(type))
                {
                    this.cache[type] = new Dictionary<string, DynamicPropertyAccessor>();
                }

                accessor = new DynamicPropertyAccessor(type, propertyName);
                this.cache[type][propertyName] = accessor;

                return accessor;
            }
        }
    }
}
