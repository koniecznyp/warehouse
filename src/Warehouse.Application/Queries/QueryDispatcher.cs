using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Warehouse.Application.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory serviceFactory;

        public QueryDispatcher(IServiceScopeFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = serviceFactory.CreateScope())
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
                dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);
                return await handler.HandleAsync((dynamic)query);
            }
        }
    }
}
