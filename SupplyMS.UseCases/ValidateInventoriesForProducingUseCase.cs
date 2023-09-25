using SupplyMS.Domain;
using SupplyMS.UseCases.Interfaces;
using SupplyMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.UseCases
{
    public class ValidateInventoriesForProducingUseCase : IValidateInventoriesForProducingUseCase
    {
        private readonly IProductRepository _productRepository;

        public ValidateInventoriesForProducingUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> ExecuteAsync(Product product, int quantity)
        {
            var prod = await _productRepository.GetProductByIdAsync(product.ProductId);

            foreach (var item in prod.ProductInventories)
            {
                if (item.InventoryQuantity * quantity > item.Inventory.Quantity)
                {
                    return false;
                }

            }
            return true;
        }
    }
}
