using System;
using System.Linq.Expressions;

namespace WebPrint.Framework
{
    public static class ExpressionExtensions
    {

    }

    public static class /*PredicateExpression*/Predicate
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return t => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return t => false;
        }
    }

    public static class PredicateExtensions
    {
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exprLeft,
                                                      Expression<Func<T, bool>> exprRight)
        {
            var invokedExpr = Expression.Invoke(exprRight, exprLeft.Parameters);
            // Expression.Or 导致以下错误
            // 无法将类型为“NHibernate.Hql.Ast.HqlBitwiseOr”的对象强制转换为类型“NHibernate.Hql.Ast.HqlBooleanExpression”。
            // Expression.Or()表示按位运算，而Expression.OrElse()才表示逻辑运算
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(exprLeft.Body, invokedExpr), exprLeft.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exprLeft,
                                                       Expression<Func<T, bool>> exprRight)
        {
            var invokedExpr = Expression.Invoke(exprRight, exprLeft.Parameters);
            // Expression.And 导致以下NHibernate错误
            // 无法将类型为“NHibernate.Hql.Ast.HqlBitwiseAnd”的对象强制转换为类型“NHibernate.Hql.Ast.HqlBooleanExpression”
            // Expression.And()表示按位运算，而Expression.AndAlso()才表示逻辑运算
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(exprLeft.Body, invokedExpr), exprLeft.Parameters);

        }
    }
}
