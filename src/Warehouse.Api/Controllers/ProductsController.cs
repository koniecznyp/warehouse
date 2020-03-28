using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Warehouse.Application.Commands;
using Warehouse.Application.Commands.Products;
using Warehouse.Application.Dto;
using Warehouse.Application.Queries;
using Warehouse.Application.Queries.Products;

namespace Warehouse.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateProduct command)
        {
            await commandDispatcher.DispatchAsync(command);
            return Created($"products/{command.ProductId}", null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await commandDispatcher.DispatchAsync(new DeleteProduct(id));
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetAsync(Guid id)
        {
            var product = await queryDispatcher.QueryAsync(new GetProduct(id));
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetAsync([FromQuery] GetProducts query)
        {
            var products = await queryDispatcher.QueryAsync(query);
            return Ok(products);
        }
    }
}
