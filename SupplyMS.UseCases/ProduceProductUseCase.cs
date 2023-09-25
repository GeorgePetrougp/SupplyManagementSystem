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
    public class ProduceProductUseCase : IProduceProductUseCase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryTrasactionRepository _inventoryTrasactionRepository;
        private readonly IProductTransactionRepository _productTransactionRepository;

        public ProduceProductUseCase(IInventoryRepository inventoryRepository,
            IProductRepository productRepository,
            IInventoryTrasactionRepository inventoryTrasactionRepository,
            IProductTransactionRepository productTransactionRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
            _inventoryTrasactionRepository = inventoryTrasactionRepository;
            _productTransactionRepository = productTransactionRepository;
        }
        public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            await _productTransactionRepository.ProduceAsync(productionNumber, product, quantity, product.Price, doneBy);

            product.Quantity += quantity;
            await _productRepository.UpdateProductAsync(product);
        }

    }
}
