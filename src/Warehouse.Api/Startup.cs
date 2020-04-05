using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Warehouse.Application;
using Warehouse.Infrastructure;
using Warehouse.Infrastructure.Middlewares;
using Warehouse.Infrastructure.Validators;

namespace Warehouse.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)                
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<IProductValidator>());
            services.AddApplication();
            services.AddInfrastructure(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
