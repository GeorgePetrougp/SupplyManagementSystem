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
        Task PurchaseAsync(string poNumber, Inventory inventory, int quantity,double price,  string doneBy);
    }
}
