using SupplyMS.Domain;
using SupplyMS.UseCases.Interfaces;
using SupplyMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyMS.UseCases.Reports
{
    public class SearchInventoryTransactionsUseCase : ISearchInventoryTransactionsUseCase
    {
        private readonly IInventoryTrasactionRepository _inventoryTrasactionRepository;

        public SearchInventoryTransactionsUseCase(IInventoryTrasactionRepository inventoryTrasactionRepository)
        {
            _inventoryTrasactionRepository = inventoryTrasactionRepository;

        }

        public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(
            string inventoryName,
            DateTime? dateFrom,
            DateTime? dateTo,
            InventoryTransactionType? transactionType)
        {

            return await _inventoryTrasactionRepository.GetInventoryTransactionsAsync(inventoryName, dateFrom, dateTo, transactionType);

        }
    }
}
