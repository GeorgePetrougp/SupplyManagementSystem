using SupplyMS.Domain;

namespace SupplyMS.UseCases.Interfaces
{
    public interface IViewInventoriesByNameUserCase
    {
        Task<IEnumerable<Inventory>> ExecuteAsync(string name = "");
    }
}