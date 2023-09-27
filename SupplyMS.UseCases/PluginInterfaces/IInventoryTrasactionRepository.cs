using SupplyMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.UseCases.PluginInterfaces
{
    public interface IInventoryTrasactionRepository
    {
        Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType);
        Task PurchaseAsync(string poNumber, Inventory inventory, int quantity,double price,  string doneBy);
    }
}
