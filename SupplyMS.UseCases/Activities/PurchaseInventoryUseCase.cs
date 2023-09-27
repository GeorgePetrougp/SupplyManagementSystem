using SupplyMS.Domain;
using SupplyMS.UseCases.Interfaces;
using SupplyMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.UseCases.Activities
{
    public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
    {
        private readonly IInventoryTrasactionRepository _inventoryTransactionRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public PurchaseInventoryUseCase(IInventoryTrasactionRepository inventoryTransactionRepository, IInventoryRepository inventoryRepository)
        {
            _inventoryTransactionRepository = inventoryTransactionRepository;
            _inventoryRepository = inventoryRepository;
        }
        public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy)
        {
            await _inventoryTransactionRepository.PurchaseAsync(poNumber, inventory, quantity, inventory.Price, doneBy);

            inventory.Quantity += quantity;
            await _inventoryRepository.UpdateInventoryAsync(inventory);
        }
    }
}
