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
    public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
    {
        private readonly IInventoryTrasactionRepository _inventoryTransactionRepository;

        public PurchaseInventoryUseCase(IInventoryTrasactionRepository inventoryTransactionRepository)
        {
            _inventoryTransactionRepository = inventoryTransactionRepository;
        }
        public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy)
        {
            await _inventoryTransactionRepository.PurchaseAsync(poNumber, inventory, quantity, price, doneBy);
        }
    }
}
