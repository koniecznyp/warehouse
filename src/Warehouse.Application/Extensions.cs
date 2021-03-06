using System;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Commands;
using Warehouse.Application.Queries;

namespace Warehouse.Application
{
    public static class Extensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddCommandHandlers();
            services.AddQueryHandlers();

            services.AddCommandDispatcher();
            services.AddQueryDispatcher();
        }

        private static void AddCommandHandlers(this IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }

        private static void AddQueryHandlers(this IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }

        private static void AddCommandDispatcher(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        }

        private static void AddQueryDispatcher(this IServiceCollection services)
        {
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        }
    }
}