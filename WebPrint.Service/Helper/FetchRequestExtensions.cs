using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebPrint.Data.Helper;


namespace WebPrint.Service.Helper
{
    public static  class FetchRequestExtensions
    {
        public static IFetchRequest<TOriginating, TRelated> Fetch<TOriginating, TRelated>(this IQueryable<TOriginating> query, Expression<Func<TOriginating, TRelated>> relatedObjectSelector)
        {
            return query.EagerFetch(relatedObjectSelector);
        }

        public static IFetchRequest<TOriginating, TRelated> FetchMany<TOriginating, TRelated>(this IQueryable<TOriginating> query, Expression<Func<TOriginating, IEnumerable<TRelated>>> relatedObjectSelector)
        {
            return query.EagerFetchMany(relatedObjectSelector);
        }

        public static IFetchRequest<TQueried, TRelated> ThenFetch<TQueried, TFetch, TRelated>(this IFetchRequest<TQueried, TFetch> query, Expression<Func<TFetch, TRelated>> relatedObjectSelector)
        {
            return query.ThenEagerFetch(relatedObjectSelector);
        }

        public static IFetchRequest<TQueried, TRelated> ThenFetchMany<TQueried, TFetch, TRelated>(this IFetchRequest<TQueried, TFetch> query, Expression<Func<TFetch, IEnumerable<TRelated>>> relatedObjectSelector)
        {
            return query.ThenEagerFetchMany(relatedObjectSelector);
        }
    }
}
