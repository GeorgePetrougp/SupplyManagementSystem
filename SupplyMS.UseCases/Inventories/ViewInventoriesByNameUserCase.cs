using SupplyMS.Domain;
using SupplyMS.UseCases.Interfaces;
using SupplyMS.UseCases.PluginInterfaces;

namespace SupplyMS.UseCases.Inventories
{
    public class ViewInventoriesByNameUserCase : IViewInventoriesByNameUserCase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public ViewInventoriesByNameUserCase(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
        {
            return await _inventoryRepository.GetInventoriesByName(name);
        }
    }
}