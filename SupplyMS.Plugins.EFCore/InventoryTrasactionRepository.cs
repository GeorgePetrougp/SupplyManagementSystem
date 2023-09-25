using SupplyMS.Domain;
using SupplyMS.Domain.Validations;
using SupplyMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.Plugins.EFCore
{
    public class InventoryTrasactionRepository : IInventoryTrasactionRepository
    {
        private readonly DataContext _context;

        public InventoryTrasactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity,double price, string doneBy)
        {
             _context.InventoryTransactions.Add(new InventoryTransaction
            {
                PoNumber = poNumber,
                InventoryId = inventory.InventoryId,
                QuantityBefore = inventory.Quantity,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = inventory.Quantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price
            });

            await _context.SaveChangesAsync();
        }
    }
}
