using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.MySQL.Extensions
{
    public static class OrderByExtensions
    {
        /// <summary>
        /// 多个OrderBy用逗号隔开,属性前面带-号表示反序排序，exp:"name,-createtime"
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<T> OrderByBatch<T>(this IEnumerable<T> query, string name, bool isAsc)
        {
            var index = 0;
            var a = name.Split(',');
            foreach (var item in a)
            {
                var m = index++ > 0 ? "ThenBy" : "OrderBy";
                if (!isAsc)
                {
                    m += "Descending";
                    name = item;
                }
                else
                {
                    name = item;
                }
                name = name.Trim();

                var propInfo = GetPropertyInfo(typeof(T), name);
                var expr = GetOrderExpression(typeof(T), propInfo);
                var method = typeof(Enumerable).GetMethods().FirstOrDefault(mt => mt.Name == m && mt.GetParameters().Length == 2);
                var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                query = (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
            }
            return query;
        }
        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
            {
                throw new ArgumentException("name");
            }
            return matchedProperty;
        }
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }
    }
}
