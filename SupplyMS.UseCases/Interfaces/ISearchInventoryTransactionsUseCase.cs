using SupplyMS.Domain;

namespace SupplyMS.UseCases.Interfaces
{
    public interface ISearchInventoryTransactionsUseCase
    {
        Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType);
    }
}