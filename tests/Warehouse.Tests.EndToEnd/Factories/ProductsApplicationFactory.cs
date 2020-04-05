using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Warehouse.Tests.EndToEnd.Factories
{
    public class ProductsApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> 
        where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
            => base.CreateWebHostBuilder().UseEnvironment("tests");
    }
}
