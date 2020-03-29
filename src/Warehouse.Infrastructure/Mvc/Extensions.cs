using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Warehouse.Infrastructure.Mvc
{
    public static class Extensions
    {
        public static T Bind<T>(this T model, Expression<Func<T, object>> expression, object value)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            var propertyName = memberExpression.Member.Name.ToLowerInvariant();
            var modelType = model.GetType();
            var field = modelType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .SingleOrDefault(x => x.Name.ToLowerInvariant().StartsWith($"<{propertyName}>"));
            if (field == null)
            {
                return model;
            }

            field.SetValue(model, value);
            return model;
        }
    }
}
