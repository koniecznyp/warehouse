using Newtonsoft.Json;
using System;

namespace Warehouse.Application.Commands.Products
{
    public class DeleteProduct : ICommand
    {
        public Guid ProductId { get; }

        [JsonConstructor]
        public DeleteProduct(Guid productId)
        {
            ProductId = productId;
        }
    }
}
