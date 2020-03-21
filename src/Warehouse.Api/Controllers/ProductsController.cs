using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Warehouse.Application.Commands;
using Warehouse.Application.Commands.Products;

namespace Warehouse.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateProduct command)
        {
            await commandDispatcher.DispatchAsync(command);
            return Created($"products/{command.ProductId}", null);
        }
    }
}
