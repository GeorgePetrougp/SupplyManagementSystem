using SupplyMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.UseCases.PluginInterfaces
{
    public interface IProductTransactionRepository
    {
        Task ProduceAsync(string productionNumber, Product product, int quantity, double price, string doneBy);
        Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double price, string doneBy);
    }
}
