using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace WebPrint.Framework
{
    //Adapter from: http://blog.zhaojie.me/2009/01/dynamicpropertyaccessor-and-fasteval.html 
    //Getter/Setter 都是方法 那都可以调用 DynamicMethodExecutor 实现，试用可行
    public class DynamicPropertyAccessor
    {
        private Func<object, object> getter;
        //private DynamicMethodExecutor setter;

        public DynamicPropertyAccessor(Type type, string propertyName)
            : this(type.GetProperty(propertyName))
        { }

        public DynamicPropertyAccessor(PropertyInfo propertyInfo)
        {
            this.getter = this.GetPropertyAccessor(propertyInfo);
        }

        private Func<object, object> GetPropertyAccessor(PropertyInfo propertyInfo)
        {
            // target: (object)((({TargetType})instance).{Property})

            // preparing parameter, object type
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");

            // ({TargetType})instance
            Expression instanceCast = Expression.Convert(instance, propertyInfo.ReflectedType);

            // (({TargetType})instance).{Property}
            Expression propertyAccess = Expression.Property(instanceCast, propertyInfo);

            // (object)((({TargetType})instance).{Property})
            UnaryExpression castPropertyValue = Expression.Convert(propertyAccess, typeof(object));

            // Lambda expression
            Expression<Func<object, object>> lambda =
                Expression.Lambda<Func<object, object>>(castPropertyValue, instance);

            return lambda.Compile();
        }
    }
}
