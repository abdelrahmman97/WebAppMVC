
using System.Linq.Expressions;


namespace AppRepository
{
    public static class IQuayableExtaintion
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName, bool isAscending)
        {
            if (string.IsNullOrEmpty(columnName))
                return source;
            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");
            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);
            string methodName = isAscending ? "OrderBy" : "OrderByDescending";
            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
