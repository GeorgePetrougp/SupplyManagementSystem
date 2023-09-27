using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
        {
            if(dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
            
            var query = from it in _context.InventoryTransactions
                        join inv in _context.Inventories on it.InventoryId equals inv.InventoryId
                        where (string.IsNullOrWhiteSpace(inventoryName) || inv.InventoryName.Contains(inventoryName, StringComparison.OrdinalIgnoreCase)) &&
                        (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) &&
                        (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                        (!transactionType.HasValue || it.ActivityType == transactionType)
                        select it;

            return await query.Include(x=>x.Inventory).ToListAsync();
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
