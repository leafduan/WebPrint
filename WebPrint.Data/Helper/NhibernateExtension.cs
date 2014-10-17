using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;
using NHibernate.Type;
using NHibernate.Util;

namespace WebPrint.Data.Helper
{
    public static class NhibernateExtension
    {
        public static ISQLQuery SetScalars(this ISQLQuery sqlQuery, Type type)
        {
            GetProperties(type)
                .ForEach(c => sqlQuery.AddScalar(c.Key, c.Value));

            return sqlQuery;
        }

        private static IEnumerable<KeyValuePair<string, IType>> GetProperties(Type type)
        {
            var values = new List<KeyValuePair<string, IType>>();

            type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ForEach(i =>
                {
                    IType itype;
                    if (ItypeByType.TryGetValue(i.PropertyType, out itype))
                    {
                        values.Add(new KeyValuePair<string, IType>(i.Name, itype));
                    }
                });

            return values;
        }

        private static readonly IDictionary<Type, IType> ItypeByType = new Dictionary<Type, IType>
        {
            {typeof (Guid), NHibernateUtil.Guid},
            {typeof (bool), NHibernateUtil.Boolean},
            {typeof (byte), NHibernateUtil.Byte},
            {typeof (string), NHibernateUtil.String},
            {typeof (char), NHibernateUtil.Character},
            {typeof (DateTime), NHibernateUtil.DateTime},
            {typeof (DateTimeOffset), NHibernateUtil.DateTimeOffset},
            {typeof (int), NHibernateUtil.Int32},
            {typeof (short), NHibernateUtil.Int16},
            {typeof (long), NHibernateUtil.Int64},
            {typeof (uint), NHibernateUtil.UInt32},
            {typeof (ushort), NHibernateUtil.UInt16},
            {typeof (ulong), NHibernateUtil.UInt64},
            {typeof (float), NHibernateUtil.Single},
            {typeof (double), NHibernateUtil.Double},
            {typeof (decimal), NHibernateUtil.Decimal},

            {typeof (DateTime?), NHibernateUtil.DateTime},
            {typeof (DateTimeOffset?), NHibernateUtil.DateTimeOffset},
            {typeof (int?), NHibernateUtil.Int32},
            {typeof (short?), NHibernateUtil.Int16},
            {typeof (long?), NHibernateUtil.Int64},
            {typeof (uint?), NHibernateUtil.UInt32},
            {typeof (ushort?), NHibernateUtil.UInt16},
            {typeof (ulong?), NHibernateUtil.UInt64},
            {typeof (float?), NHibernateUtil.Single},
            {typeof (double?), NHibernateUtil.Double},
            {typeof (decimal?), NHibernateUtil.Decimal},
        };
    }
}
