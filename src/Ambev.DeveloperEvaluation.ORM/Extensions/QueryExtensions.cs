using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, string propertyName, bool descending, bool firstOrder)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = firstOrder ?
                (descending ? "OrderByDescending" : "OrderBy") :
                (descending ? "ThenByDescending" : "ThenBy");

            var resultExp = Expression.Call(typeof(Queryable), methodName, [typeof(T), property.Type], query.Expression, Expression.Quote(lambda));
            return query.Provider.CreateQuery<T>(resultExp);
        }
    }
}